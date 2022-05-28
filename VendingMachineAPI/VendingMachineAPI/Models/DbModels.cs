namespace VendingMachineAPI.Models
{
    public class ProductType
    {
        public ProductType() { }
        public int Id { get; set; }
        public string? Type { get; set; }
        public decimal Cost { get; set; }
    }

    public class Product
    {
        public Product() { }
        public int Id { get; set; }
        public ProductType? ProductType { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime RefundDate { get; set; }
    }

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
        public DateTime InsertDate { get; set; }
        public bool RefundRequested { get; set; }
    }

    public class CreditCardVerification
    {
        public CreditCardVerification() { }
        public int Id { get; set; }
        public bool Approved { get; set; }
        public decimal OriginalTransactionAmount { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
