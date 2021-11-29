using DAL;
using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class LogBLL
    {
        private string _endpoint;
        private LogService _repo;

        public LogBLL(string endpoint)
        {
            _endpoint = endpoint;
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new LogService();
        }

        public async Task Log(string message)
        {   
            await _repo.Add(new Log { DateTime = DateTime.Now, EndPoint = _endpoint, Message = message });
            Console.WriteLine(message);
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return (await _repo.DisplayAll());
        }
    }
}
