using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Services
{
    public interface IVendingService
    {
        ProductResponseDto AddProduct(StockDto product);
        AllStockDto ListProducts();

        TransactionResponseDto CreateTransaction(PurchaseDto transaction);
        RefundResponseDto RefundTransaction(RefundDto refund);
        List<LedgerDto> GetAllTransactions();
    }
}
