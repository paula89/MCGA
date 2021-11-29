using DAL;
using Helpers.Enum;
using System.Threading.Tasks;

namespace BLL
{
    public class SystemBLL
    {
        private PackageService _repoP;

        public SystemBLL()
        {
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repoP = new PackageService();
        }

        public async Task<string> DeliverPackage(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.Pressed)
            {
                return $"El Paquete {package.Id} no se encuentra prensado.";
            }
            package.Status = PackageStatusEnum.Stacked;
            await _repoP.ChangeStatus(package);
            return $"Se agrego el paquete prensado {package.Id} a la pila.";
        }
    }
}
