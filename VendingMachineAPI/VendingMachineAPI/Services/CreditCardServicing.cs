using VendingMachineAPI.Models.DTO;

namespace VendingMachineAPI.Services
{
    public class CreditCardServicing : ICreditCardServicing
    {
        private readonly ILogger _logger;
        public CreditCardServicing(ILogger<CreditCardServicing> logger)
        {
            _logger = logger;
        }
        public CreditCardTransactionResponseDto ChargeCard(CardDto card, decimal amountToCharge)
        {
            _logger.LogInformation("charging card");
            var timeNow = DateTime.Now;

            // arbitrary amounts to set maximum for purchase from vending machine
            if (amountToCharge <= 0 
                || amountToCharge > 20
                || card.Expiration.Date <= timeNow.Date)
            {
                _logger.LogError($"Error charging card, expiration {card.Expiration}, amount requested {amountToCharge}");

                return new CreditCardTransactionResponseDto()
                {
                    Approved = false,
                    TransactionAmount = 0,
                    TransactionDate = timeNow
                };
            }

            // typically actual connection would go here, this just a mockup

            _logger.LogInformation($"Charged card successfully");

            // return true if amount is less than $50 and more than 0
            return new CreditCardTransactionResponseDto()
            {
                Approved = true,
                TransactionAmount = amountToCharge,
                TransactionDate = timeNow
            };
        }
    }
}
