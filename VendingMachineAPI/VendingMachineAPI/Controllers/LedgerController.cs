using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachineAPI.Models.DTO;
using VendingMachineAPI.Services;

namespace VendingMachineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IVendingService _vendingService;
        public LedgerController(ILogger<LedgerController> logger, IVendingService vendingService)
        {
            _logger = logger;
            _vendingService = vendingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LedgerDto>> GetTransactions()
        {
            var resp = _vendingService.GetAllTransactions();
            
            if(resp.Count == 0)
            {
                return NoContent();
            }

            return Ok(resp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RefundResponseDto> RequestRefund([FromBody] RefundDto refund)
        {
            var resp = _vendingService.RefundTransaction(refund);

            if (resp.Error)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }
    }
}
