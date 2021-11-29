using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Press.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PressController : ControllerBase
    {
        private readonly ILogger<PressController> _logger;

        public PressController(ILogger<PressController> logger)
        {
            _logger = logger;
        }

        [HttpPost("on")]
        public async Task<string> On()
        {
            try
            {
                var entity = new PressBLL();
                await entity.Initialize();
                _logger.LogInformation("Se inicializo la Prensa Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de inicializar la Prensa.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpPost("off")]
        public async Task<string> Off()
        {
            try
            {
                var entity = new PressBLL();
                await entity.Initialize();
                _logger.LogInformation("Se finalizo la Prensa Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de finalizar la Prensa.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpGet("status")]
        public async Task<string> GetStatus()
        {
            try
            {
                var entity = new PressBLL();
                return JsonSerializer.Serialize(await entity.GetStatus());
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de obtener el estado de la Prensa.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpGet("is_idle")]
        public async Task<string> IsIdle()
        {
            try
            {
                var entity = new PressBLL();
                return JsonSerializer.Serialize(await entity.IsIdle());
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de obtener si la prensa esta libre.");
                _logger.LogError(e.Message);
                return "0";
            }
        }
    }
}
