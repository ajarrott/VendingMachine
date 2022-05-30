using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VendingMachineAPI.Models.Options;

namespace VendingMachineAPI.Models.DAL
{
    public class VendingMachineContext : DbContext
    {
        DbConnection _dbConnection;
        public VendingMachineContext(IOptions<DbConnection> dbConnection)
        {
            _dbConnection = dbConnection.Value;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CreditCardVerification> CreditCardsVerification { get; set; }

        public ProductType? FindProductType(string type)
        {
            return ProductTypes.FirstOrDefault(x => x.Type == type);
        }

        public List<Product> FindAvailableProductByType(string type)
        {
            var returnList = new List<Product>();
            returnList.AddRange(Products.Where(x => x.ProductType.Type == type && x.SaleDate == null));

            return returnList;
        }

        public List<Product> GetAllUnsoldProducts()
        {
            return Products
                .Include(x => x.ProductType)
                .Where(x => x.SaleDate == null).ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_dbConnection.ConnectionString);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType() { Cost = 0.95m, Type = "Soda", Id = 1 },
                new ProductType() { Cost = 0.60m, Type = "Candy Bar", Id = 2 },
                new ProductType() { Cost = 0.99m, Type = "Chips", Id = 3 });

            modelBuilder.Entity<Transaction>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Transaction);

        }
    }
}
