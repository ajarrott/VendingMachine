namespace VendingMachineAPI.Models.DTO
{
    public class PurchaseDto
    {
        public PurchaseDto()
        {
            ItemsToPurchase = new List<InventoryDto>();
        }

        public CardDto Card { get; set; }
        public List<InventoryDto> ItemsToPurchase { get; set; }
        public decimal AmountToCharge { get; set; }

    }

    /// <summary>
    /// Card Data
    /// </summary>
    public class CardDto
    {
        public string Number { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class RefundDto
    {
        public int TransactionId { get; set; }
    }

    public class StockDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
