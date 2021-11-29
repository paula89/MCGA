using DAL;
using Helpers.Enum;
using System;
using System.Threading.Tasks;

namespace BLL
{
    public class PressBLL : MachineBLL
    {
        private PackageService _repoP;
        private LogBLL _log;

        public PressBLL() : base ("Press")
        {
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repoP = new PackageService();
            _log = new LogBLL("PressBLL");
        }

        public async Task Busy()
        {
            await _repo.ChangeIdle(false);
            await _log.Log($"Prensa Cargada.");
        }

        public async Task Free()
        {
            await _repo.ChangeIdle(true);
            await _log.Log($"Prensa Descargada.");
        }

        public async Task UpPress()
        {
            await _repo.ChangeSensor(false);
            await _log.Log($"Prensa Arriba.");
        }

        public async Task DownPress()
        {
            await _repo.ChangeSensor(true);
            await _log.Log($"Prensa Abajo.");
        }

        public async Task<string> PackagePressed(int idPackage)
        {
            var package = await _repoP.Get(idPackage);
            if (package.Status != PackageStatusEnum.Delivered)
            {
                return $"El Paquete {package.Id} no se encuentra en la Prensa.";
            }
            package.Status = PackageStatusEnum.Pressed;
            await _repoP.ChangeStatus(package);
            return $"Se prenso correctamente el paquete {package.Id}.";
        }
    }
}
