using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Services
{
    public interface ICreditCardServicing
    {
        CreditCardTransactionResponseDto ChargeCard(CardDto card, decimal amountToCharge);
    }
}
