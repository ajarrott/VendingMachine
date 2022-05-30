using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Models.DAL
{
    /// <summary>
    /// Product type information
    /// </summary>
    public class ProductType
    {
        public ProductType() { }
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
    }

    /// <summary>
    /// Product details
    /// </summary>
    public class Product
    {
        public Product() { }
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? SaleDate { get; set; }
    }

    /// <summary>
    /// Transaction details
    /// </summary>
    public class Transaction
    {
        public Transaction()
        {
            CCVerification = new CreditCardVerification();
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public CreditCardVerification CCVerification { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime RefundDate { get; set; }
    }

    /// <summary>
    /// Credit card purchase details
    /// </summary>
    public class CreditCardVerification
    {
        public CreditCardVerification() { }
        public CreditCardVerification(CreditCardTransactionResponseDto ccResp) 
        {
            Approved = ccResp.Approved;
            OriginalTransactionAmount = ccResp.TransactionAmount;
            InsertDate = ccResp.TransactionDate;
        }

        public int Id { get; set; }
        public bool Approved { get; set; }
        public decimal OriginalTransactionAmount { get; set; }
        public DateTime InsertDate { get; set; }
    }

}
