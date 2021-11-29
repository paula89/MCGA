using BLL;
using Helpers;
using Helpers.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;

        public PackageController(ILogger<PackageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllPackages")]
        public async Task<string> GetAllPackagesAsync()
        {
            try
            {
                var entity = new PackageBLL();
                return JsonSerializer.Serialize(await entity.GetAllAsync());
            }
            catch
            {
                return "0";
            }            
        }

        [HttpGet("GetAllPackagesByStatus/{status}")]
        public async Task<string> GetAllPackagesByStatusAsync(int status)
        {
            try
            {
                var entity = new PackageBLL();
                return JsonSerializer.Serialize(await entity.GetAllByStatusAsync((PackageStatusEnum)status));
            }
            catch
            {
                return "0";
            }            
        }

        [HttpGet("GetAllPackagesByStatusOnList/{status}")]
        public async Task<List<Package>> GetAllPackagesByStatusOnListAsync(int status)
        {
            try
            {
                var entity = new PackageBLL();
                return await entity.GetAllByStatusAsync((PackageStatusEnum)status);
            }
            catch
            {
                return new List<Package>();
            }
        }

        [HttpGet("GetPackage/{id}")]
        public async Task<string> GetPackageAsync(int id)
        {
            try
            {
                var entity = new PackageBLL();
                return JsonSerializer.Serialize(await entity.GetAsync(id));
            }
            catch
            {
                return "0";
            }
        }
    }
}
