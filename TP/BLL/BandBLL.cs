using DAL;
using Helpers.Enum;
using System;
using System.Threading.Tasks;

namespace BLL
{
    public class BandBLL : MachineBLL
    {
        private PackageService _repoP;

        public BandBLL() : base ("Band")
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
            package.Status = PackageStatusEnum.Queued;
            await _repoP.ChangeStatus(package);
        }

        public async Task CancelPackage(int entityId) => await _repoP.Delete(entityId);
        public async Task<string> PackageOnBand(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.Joined)
            {
                return $"El Paquete {package.Id} no se encuentra en el ingreso de la Cinta.";
            }
            package.Status = PackageStatusEnum.OnBand;
            await _repoP.ChangeStatus(package);
            return $"Se agarro el paquete {package.Id} y se encuentra siendo transportado en la cinta.";
        }

        public async Task<string> PackageQueued(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.OnBand)
            {
                return $"El Paquete {package.Id} no se encuentra siendo transportado en la cinta.";
            }
            package.Status = PackageStatusEnum.Queued;
            await _repoP.ChangeStatus(package);
            return $"Se informo el ingreso del paquete {package.Id}.";
        }
    }
}
