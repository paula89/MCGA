using BLL;
using Helpers.Request;
using Helpers.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrivilegeController : ControllerBase
    {
        private readonly ILogger<PrivilegeController> _logger;

        public PrivilegeController(ILogger<PrivilegeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> GetAllAsync()
        {
            try
            {
                var entity = new PrivilegesBLL(UserKey());
                return JsonSerializer.Serialize(await entity.GetAllAsync());
            }
            catch
            {
                return "0";
            }
        }

        [HttpGet("{id}")]
        public async Task<string> GetAsync(int id)
        {
            try
            {
                var entity = new PrivilegesBLL(UserKey());
                return JsonSerializer.Serialize(await entity.GetAsync(id));
            }
            catch
            {
                return "0";
            }
        }

        [HttpPost]
        public async Task<string> Post(CreatePrivilegeRequest request)
        {
            try
            {
                var entity = new PrivilegesBLL(UserKey());
                entity.Create(request);
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        [HttpPut]
        public async Task<string> Put(UpdatePrivilegeRequest request)
        {
            try
            {
                var entity = new PrivilegesBLL(UserKey());
                await entity.UpdateAsync(request);
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            try
            {
                var entity = new PrivilegesBLL(UserKey());
                await entity.DeleteAsync(id);
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        private string UserKey() => Request.Headers.First(x => x.Key == "token").Value;
    }
}
