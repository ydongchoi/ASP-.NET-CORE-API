using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Info Message");
            _logger.LogDebug("Debug Message");
            _logger.LogWarn("Warn Message");
            _logger.LogError("Error Message");

            return new string[] { "data1", "data2" };
        }
    }
}
