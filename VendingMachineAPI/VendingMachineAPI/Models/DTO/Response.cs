namespace VendingMachineAPI.Models.DTO
{
    /// <summary>
    /// All stock in vending machine
    /// </summary>
    public class AllStockDto
    {
        public AllStockDto() => Inventory = new List<InventoryDto>();

        public List<InventoryDto> Inventory { get; set; }
    }

    /// <summary>
    /// Name, price, and amount of a specific item
    /// </summary>
    public class InventoryDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class TransactionResponseDto : ErrorDto
    {
        public int TransactionId { get; set; }
        public bool CreditCardApproved { get; set; }
        public bool TransactionProcessed { get; set; }
        public decimal AmountCharged { get; set; }
    }

    public class RefundResponseDto : ErrorDto
    {
        public int TransactionId { get; set; }

        public decimal AmountRefunded { get; set; }
        public DateTime? RefundDate { get; set; }
    }

    public class ProductResponseDto : ErrorDto
    {
        public List<int> ProductIds { get; set; } = new List<int>();
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Provides the information for a specific transaction on a specific date
    /// this also displays the updated price if an item has been refunded
    /// </summary>
    public class LedgerDto
    {
        public LedgerDto() => LineItems = new List<InventoryDto>();
        public decimal SalePrice { get; set; }
        public DateTime SaleDateTime { get; set; }
        public DateTime RefundDateTime { get; set; }
        public List<InventoryDto> LineItems { get; set; }
        public int TransactionId { get; set; }
    }

    /// <summary>
    /// Credit Card Transaction data
    /// </summary>
    public class CreditCardTransactionResponseDto
    {
        public bool Approved { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class ErrorDto
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
