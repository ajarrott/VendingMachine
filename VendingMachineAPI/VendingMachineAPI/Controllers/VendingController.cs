using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Models.DTO;
using VendingMachineAPI.Services;

namespace VendingMachineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendingController : ControllerBase
    {
        private readonly ILogger<VendingController> _logger;
        private readonly IVendingService _vendingService;

        public VendingController(ILogger<VendingController> logger, IVendingService repository, ICreditCardServicing cardServicing)
        {
            _logger = logger;
            _vendingService = repository;
        }

        /// <summary>
        /// Get all stock currently in vending machine
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AllStockDto> GetVendingMachineStock()
        {
            _logger.LogInformation("Returning all in stock products");

            return Ok(_vendingService.ListProducts());
        }

        /// <summary>
        /// Make purchase
        /// </summary>
        /// <param name="itemsToPurchase"></param>
        /// <returns></returns>
        [HttpPost("purchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TransactionResponseDto> MakePurchase(PurchaseDto purchase)
        {
            _logger.LogInformation($"Purchase Requested");
            var tranResp = _vendingService.CreateTransaction(purchase);

            if(tranResp.Error)
            {
                _logger.LogInformation($"Transaction failed");
                return BadRequest(tranResp);
            }

            return Ok(tranResp);
        }

        [HttpPost("refund")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RefundResponseDto> RefundPurchase(RefundDto refund)
        {

            var resp = _vendingService.RefundTransaction(refund);

            if(resp.Error)
            {
                _logger.LogInformation($"Refund failed");
                return BadRequest(resp);
            }

            return Ok(resp);
        }
    }
}