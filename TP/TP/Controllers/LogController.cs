using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {

        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> GetAllAsync()
        {
            try
            {
                var entity = new LogBLL("LogController");
                return JsonSerializer.Serialize(await entity.GetAllAsync());
            }
            catch
            {
                return "0";
            }
        }
    }
}
