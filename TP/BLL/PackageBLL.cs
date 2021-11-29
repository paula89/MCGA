 using DAL;
using Helpers;
using Helpers.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class PackageBLL
    {
        private PackageService _repo;
        public PackageBLL()
        {
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new PackageService();
        }
        
        public async Task<List<Package>> GetAllAsync() => (await _repo.DisplayAll()).ToList();

        public async Task<List<Package>> GetAllByStatusAsync(PackageStatusEnum status) => (await _repo.DisplayAllByStatus(status)).ToList();

        public async Task<Package> GetAsync(int id) => await _repo.Get(id);
    }
}
