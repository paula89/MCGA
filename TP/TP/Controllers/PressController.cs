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

        [HttpPost("TurnOnPress")]
        public async Task<string> TurnOnPress()
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

        [HttpPost("TurnOffPress")]
        public async Task<string> TurnOffPress()
        {
            try
            {
                var entity = new PressBLL();
                await entity.Finalize();
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

        [HttpGet("GetPressStatus")]
        public async Task<string> GetPressStatus()
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

        [HttpGet("PressIsIdle")]
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

        [HttpGet("PressSensorStatus")]
        public async Task<string> SensorStatus()
        {
            try
            {
                var entity = new PressBLL();
                return JsonSerializer.Serialize(await entity.Sensor());
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
