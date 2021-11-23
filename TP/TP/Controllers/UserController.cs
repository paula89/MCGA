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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        [HttpPost("LogIn")]
        public async Task<string> LogIn(LogInRequest request)
        {
            try
            {
                var entity = new UsersBLL();
                return JsonSerializer.Serialize(await entity.LogIn(request));
            }
            catch
            {
                return "0";
            }
        }

        [HttpGet]
        public async Task<string> GetAllAsync()
        {
            try
            {
                var entity = new UsersBLL(UserKey());
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
                var entity = new UsersBLL(UserKey());
                return JsonSerializer.Serialize(await entity.GetAsync(id));
            }
            catch
            {
                return "0";
            }            
        }

        [HttpPost]
        public async Task<string> Post(CreateUserRequest request)
        {
            try
            {
                var entity = new UsersBLL(UserKey());
                entity.Create(request);
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        [HttpPut]
        public async Task<string> Put(UpdateUserRequest request)
        {
            try
            {
                var entity = new UsersBLL(UserKey());
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
                var entity = new UsersBLL(UserKey());
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
