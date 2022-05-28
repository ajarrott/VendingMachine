using Microsoft.AspNetCore.Mvc;

namespace VendingMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingController : ControllerBase
    {
        private readonly ILogger<VendingController> _logger;

        public VendingController(ILogger<VendingController> logger)
        {
            _logger = logger;
        }
    }
}