using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP.Controllers
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

        [HttpPost("press_package")]
        public async Task<string> PutPackage()
        {
            try
            {
                var entity = new BandBLL();
                await entity.PutPackage();
                _logger.LogInformation("Se Agrego un nuevo bulto correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al agregar el bulto.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpPut("takePackage/{idPackage}")]
        public async Task<string> TakePackage(int id)
        {
            try
            {
                var entity = new BandBLL();
                await entity.TakePackage(id);
                _logger.LogInformation("Se quito el bulto correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al quitar el bulto.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpDelete("CancelPackage/{id}")]
        public async Task<string> CancelPackage(int id)
        {
            try
            {
                var entity = new BandBLL();
                await entity.CancelPackage(id);
                _logger.LogInformation("Se calcelo el bulto correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al cancelar el bulto.");
                _logger.LogError(e.Message);
                return "0";
            }
        }
    }
}
