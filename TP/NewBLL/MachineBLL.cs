using DAL;
using Helpers.Enum;
using System.Threading.Tasks;

namespace BLL
{
    public class MachineBLL
    {
        private string _machineName;
        private MachineService _repo;

        public MachineBLL(string machineName)
        {
            _machineName = machineName;
            InicializeRepos();
        }

        private void InicializeRepos()
        {
            _repo = new MachineService(_machineName);
        }

        public async Task Initialize() => await _repo.ChangeStatus(MachineStatusEnum.On);

        public async Task Finalize() => await _repo.ChangeStatus(MachineStatusEnum.Off);

        public async Task<MachineStatusEnum> GetStatus() => await _repo.GetStatus();

        public async Task<bool> IsIdle() => await _repo.IsIdle();

        public async Task<bool> CanOperate() => await this.GetStatus() == MachineStatusEnum.On;
    }
}
