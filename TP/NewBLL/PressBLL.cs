using DAL;
using Helpers.Enum;
using System.Threading.Tasks;

namespace BLL
{
    public class PressBLL : MachineBLL
    {
        private PackageService _repoP;

        public PressBLL() : base ("Press")
        {
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repoP = new PackageService();
        }

        public async Task PutPackage() => await _repoP.Add();

        public async Task TakePackage(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            package.Status = PackageStatusEnum.Ready;
            await _repoP.ChangeStatus(package);
            // TODO Enviar mensaje a la cola
        }

        public async Task CancelPackage(int entityId) => await _repoP.Delete(entityId);
    }
}
