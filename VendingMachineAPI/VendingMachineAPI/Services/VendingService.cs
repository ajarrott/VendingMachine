using VendingMachineAPI.Models.DAL;
using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Services
{
    public class VendingService : IVendingService
    {
        private readonly ILogger _logger;
        private readonly VendingMachineContext _context;
        private readonly ICreditCardServicing _creditCardServicing;
        public VendingService(ILogger<VendingService> logger, VendingMachineContext context, ICreditCardServicing creditCardServicing)
        {
            _logger = logger;
            _context = context;
            _creditCardServicing = creditCardServicing;
        }

        public ProductResponseDto AddProduct(StockDto product)
        {
            _logger.LogInformation($"Adding {product.Quantity} {product.Name} each.");

            var timeNow = DateTime.Now;
            var productType = _context.FindProductType(product.Name);

            if (productType == null)
            {
                string errorString = $"Product name {product.Name} has not been defined";
                _logger.LogInformation("No inventory added");

                return ReturnProductError(errorString);
            }

            var respDto = new ProductResponseDto();

            for (int i = 0; i < product.Quantity; i++)
            {
                var newProduct = new Product()
                {
                    InsertDate = timeNow,
                    ProductType = productType
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                respDto.ProductIds.Add(newProduct.Id);
            }

            _logger.LogInformation($"Added {product.Quantity} {product.Name}");

            respDto.Quantity = respDto.ProductIds.Count();

            return respDto;
        }

        public List<Product> GetProductsByType(string type)
        {
            return _context.FindAvailableProductByType(type); ;
        }

        private ProductResponseDto SellProduct(InventoryDto product)
        {
            _logger.LogInformation($"Selling {product.Quantity} {product.Name} for {product.Price} each.");

            var availableProducts = GetProductsByType(product.Name);

            // none available
            if (availableProducts == null || availableProducts.Count == 0)
            {
                string errorMessage = $"No {product.Name} available";

                return ReturnProductError(errorMessage);
            }

            // not enough available
            if (availableProducts.Count < product.Quantity)
            {
                string errorMessage = $"{product.Quantity} requested, only {availableProducts.Count} available";

                return ReturnProductError(errorMessage);
            }

            var timeNow = DateTime.Now;

            List<int> soldProductIds = new List<int>();

            // can sell all
            for(int i = 0; i < product.Quantity; i++)
            {
                // get next availableProduct
                var soldProduct = _context.Products.First(p => p.Id == availableProducts[i].Id);

                // mark as sold
                soldProduct.SaleDate = timeNow;
                _context.Products.Update(soldProduct);

                soldProductIds.Add(soldProduct.Id);
            }

            // save all updated items
            _context.SaveChanges();

            return new ProductResponseDto()
            {
                ProductIds = soldProductIds,
                Quantity = soldProductIds.Count
            };

        }

        private ProductResponseDto ReturnProductError(string errorMessage)
        {
            _logger.LogError(errorMessage);

            return new ProductResponseDto()
            {
                Error = true,
                ErrorMessage = errorMessage,
                Quantity = 0
            };
        }

        public TransactionResponseDto CreateTransaction(PurchaseDto purchase)
        {
            var totalPrice = purchase.ItemsToPurchase.Sum(x => x.Price);

            // validate price
            if(totalPrice <= 0.00m)
            {
                string errorMessage = $"Transaction for 0 or less is not valid";

                return ReturnTransactionError(errorMessage);
            }

            // validate front end math
            if(totalPrice != purchase.AmountToCharge)
            {
                string errorMessage = $"Provided sale price {purchase.AmountToCharge} does not match price the sum of items {totalPrice}";

                return ReturnTransactionError(errorMessage);
            }

            bool productsAvailable = true;

            // ensure all products available for purchase
            foreach (var item in purchase.ItemsToPurchase)
            {
                var products = GetProductsByType(item.Name);

                if (products == null
                   || products.Count == 0
                   || products.Count < item.Quantity) productsAvailable = false;
            }
            
            if (!productsAvailable)
            {
                string errorMessage = "Unable to sell all items requested";

                ReturnTransactionError(errorMessage);
            }

            var ccResp = _creditCardServicing.ChargeCard(purchase.Card, totalPrice);

            // verify card charges correctly
            if (!ccResp.Approved)
            {
                _logger.LogInformation($"Payment failed");

                return new TransactionResponseDto()
                {
                    CreditCardApproved = false,
                    TransactionProcessed = false,
                    Error = true,
                    ErrorMessage = "Credit Card failed to process",
                    AmountCharged = 0m,
                };
            }

            var soldItems = new List<InventoryDto>();
            var soldIds = new List<int>();

            // now can save all items
            foreach (var item in purchase.ItemsToPurchase)
            {
                var resp = SellProduct(item);

                if(resp.Error)
                {
                    return new TransactionResponseDto()
                    {
                        Error = true,
                        ErrorMessage = resp.ErrorMessage
                    };
                }

                soldItems.Add(item);
                soldIds.AddRange(resp.ProductIds);
            }


            var productsSold = _context.Products.Where(p => soldIds.Contains(p.Id)).ToList();
            var newTran = new Transaction()
            {
                CreditCardVerification = new CreditCardVerification(ccResp),
                PurchaseDate = DateTime.Now,
                Products = productsSold
            };

            _context.Transactions.Add(newTran);
            _context.SaveChanges();

            return new TransactionResponseDto() 
            { 
                AmountCharged = totalPrice,
                TransactionProcessed = true,
                CreditCardApproved = ccResp.Approved,
                Error = false,
                TransactionId = newTran.Id
            };
        }

        private TransactionResponseDto ReturnTransactionError(string errorMessage)
        {
            _logger.LogError(errorMessage);

            return new TransactionResponseDto()
            {
                Error = true,
                ErrorMessage = errorMessage
            };
        }

        public List<LedgerDto> GetAllTransactions()
        {
            var transactions = _context.GetAllTransactions();
            List<LedgerDto> resultDto = new List<LedgerDto>();

            foreach(var transaction in transactions)
            {
                var dtoToAdd = new LedgerDto()
                {
                    RefundDateTime = transaction.RefundDate,
                    SaleDateTime = transaction.PurchaseDate,
                    SalePrice = transaction.Products.Sum(x => x.ProductType.Cost),
                };

                var lineItems = ConvertProductToInventoryDto(transaction.Products);

                dtoToAdd.LineItems.AddRange(lineItems);

                resultDto.Add(dtoToAdd);
            }

            return resultDto;
        }

        private List<InventoryDto> ConvertProductToInventoryDto(List<Product> products)
        {
            if (products == null || products.Count == 0) return new List<InventoryDto>();

            var lineItems = products
                .GroupBy(prod => prod.ProductType)
                .Select(lineItemDto => new InventoryDto()
                {
                    Name = lineItemDto.Key.Type, // price for type and cost will be the same based on groupby
                    Price = lineItemDto.Key.Cost,
                    Quantity = lineItemDto.Select(item => item.ProductType.Type).Count() // get count of items sold 
                }).ToList();

            return lineItems;
        }

        public AllStockDto ListProducts()
        {
            _logger.LogInformation("Listing Products");

            AllStockDto allStockDto = new AllStockDto();

            var products = _context.GetAllUnsoldProducts();

            var inventory = ConvertProductToInventoryDto(products);

            allStockDto.Inventory.AddRange(inventory);

            return allStockDto;
        }

        public RefundResponseDto RefundTransaction(RefundDto refund)
        {
            var transaction = _context.Transactions.Where(x => x.Id == refund.TransactionId).FirstOrDefault();

            if(transaction == null)
            {
                return new RefundResponseDto()
                {
                    AmountRefunded = 0m,
                    Error = true,
                    ErrorMessage = $"An error occurred while refunding transaction id {refund.TransactionId}",
                    TransactionId = refund.TransactionId
                };
            }

            transaction.RefundDate = DateTime.Now;

            _context.Transactions.Update(transaction);
            _context.SaveChanges();

            return new RefundResponseDto()
            {
                AmountRefunded = transaction.Products.Sum(x => x.ProductType.Cost),
                RefundDate = transaction.RefundDate,
                TransactionId = transaction.Id
            };
        }
    }
}
