using DAL;
using Helpers;
using Helpers.Enum;
using System.Threading.Tasks;

namespace BLL
{
    public class ArmBLL : MachineBLL
    {
        private PackageService _repoP;

        public ArmBLL() : base ("Arm")
        {
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repoP = new PackageService();
        }

        public async Task<string> CarryPackage(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.Queued)
            {
                return $"El Paquete {package.Id} no se encuentra en Espera de transporte.";
            }
            package.Status = PackageStatusEnum.Carring;
            await _repoP.ChangeStatus(package);
            return $"Se agarro el paquete {package.Id} y se encuentra siendo transportado.";
        }

        public async Task<string> DeliverPackage(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.Carring)
            {
                return $"El Paquete {package.Id} no se encuentra en Transporte.";
            }
            package.Status = PackageStatusEnum.Delivered;
            await _repoP.ChangeStatus(package);
            return $"Se Transporto el paquete {package.Id}.";
        }
    }
}
