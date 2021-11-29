using DAL;
using Helpers.Enum;
using System.Threading.Tasks;

namespace BLL
{
    public class MachineBLL
    {
        private string _machineName;
        public MachineService _repo;
        public LogBLL _repoL;

        public MachineBLL(string machineName)
        {
            _machineName = machineName;
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new MachineService(_machineName);
            _repoL = new LogBLL(_machineName);
        }

        public async Task Initialize()
        {
            await _repo.ChangeStatus(MachineStatusEnum.On);
            await _repoL.Log("Se prendio el equipo.");
        }

        public async Task Finalize()
        {
            await _repo.ChangeStatus(MachineStatusEnum.Off);
            await _repoL.Log("Se apago el equipo.");
        }

        public async Task<MachineStatusEnum> GetStatus() => await _repo.GetStatus();

        public async Task<bool> IsIdle() => await _repo.IsIdle();

        public async Task<bool> Sensor() => await _repo.Sensor();

        public async Task<bool> CanOperate() => await this.GetStatus() == MachineStatusEnum.On;
    }
}
