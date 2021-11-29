using BLL;
using Helpers.Request;
using Helpers.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BandController : ControllerBase
    {

        private readonly ILogger<BandController> _logger;

        public BandController(ILogger<BandController> logger)
        {
            _logger = logger;
        }

        [HttpPost("TurnOnBand")]
        public async Task<string> TurnOnBand()
        {
            try
            {
                var entity = new BandBLL();
                await entity.Initialize();
                _logger.LogInformation("Se inicializo la Cinta Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de inicializar la Cinta.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpPost("TurnOffBand")]
        public async Task<string> TurnOffBand()
        {
            try
            {
                var entity = new BandBLL();
                await entity.Initialize();
                _logger.LogInformation("Se finalizo la Cinta Correctamente.");
                return "1";
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de finalizar la Cinta.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpGet("GetBandStatus")]
        public async Task<string> GetBandStatus()
        {
            try
            {
                var entity = new BandBLL();
                return JsonSerializer.Serialize(await entity.GetStatus());
            }
            catch (Exception e)
            {
                _logger.LogError("Se genero un error al tratar de obtener el estado de la Cinta.");
                _logger.LogError(e.Message);
                return "0";
            }
        }

        [HttpPost("PutPackageOnBand")]
        public async Task<string> PutPackageOnBand()
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

        [HttpPut("TakePackageFromBand/{idPackage}")]
        public async Task<string> TakePackageFromBand(int id)
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
