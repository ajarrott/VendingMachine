using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Services
{
    public interface IVendingService
    {
        ProductResponseDto AddProduct(InventoryDto product);
        AllStockDto ListProducts();

        TransactionResponseDto CreateTransaction(PurchaseDto transaction);
        RefundResponseDto RefundTransaction(RefundDto refund);
        List<LedgerDto> GetAllTransactions();
    }
}
