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
    public class ArmController : ControllerBase
    {

        private readonly ILogger<ArmController> _logger;

        public ArmController(ILogger<ArmController> logger)
        {
            _logger = logger;
        }

        [HttpPost("TurnOnArm")]
        public async Task<string> TurnOnArm()
        {
            try
            {
                var entity = new ArmBLL();
                await entity.Initialize();
                _logger.LogInformation("Se inicializo el Brazo Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de inicializar el brazo.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpPost("TurnOffArm")]
        public async Task<string> TurnOffArm()
        {
            try
            {
                var entity = new ArmBLL();
                await entity.Finalize();
                _logger.LogInformation("Se finalizo el Brazo Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de finalizar el brazo.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpGet("GetArmStatus")]
        public async Task<string> GetArmStatus()
        {
            try
            {
                var entity = new ArmBLL();
                return JsonSerializer.Serialize(await entity.GetStatus());
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de obtener el estado del brazo.");
                _logger.LogError(e.Message);
                return "0";
            }
        }
    }
}
