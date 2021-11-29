using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers;
using Helpers.Enum;

namespace DAL
{
    public class MachineService
    {
        private string _machineName;

        public MachineService(string machineName)
        {
            _machineName = machineName;
        }

        /// <summary>
        /// Metodo para cambiar el estado
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task ChangeStatus(MachineStatusEnum status)
        {
            var repo = new DataContext();
            var machine = await GetMachine(repo);
            machine.Status = status;
            repo.Machines.Update(machine);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para obtener el estado de la entidad
        /// </summary>
        /// <returns></returns>
        public async Task<MachineStatusEnum> GetStatus()
        {
            var repo = new DataContext();
            return (await GetMachine(repo)).Status;
        }

        /// <summary>
        /// Metodo para cambiar el estado
        /// </summary>
        /// <param name="isIdle"></param>
        /// <returns></returns>
        public async Task ChangeIdle(bool isIdle)
        {
            var repo = new DataContext();
            var machine = await GetMachine(repo);
            machine.IsIdle = isIdle;
            repo.Machines.Update(machine);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para obtener el estado de la entidad
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsIdle()
        {
            var repo = new DataContext();
            var machine = await GetMachine(repo);
            return machine.IsIdle && machine.Status == MachineStatusEnum.On;
        }


        /// <summary>
        /// Metodo para cambiar el estado
        /// </summary>
        /// <param name="isDown"></param>
        /// <returns></returns>
        public async Task ChangeSensor(bool isDown)
        {
            var repo = new DataContext();
            var machine = await GetMachine(repo);
            machine.Sensor = isDown;
            repo.Machines.Update(machine);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para obtener el estado de la entidad
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Sensor()
        {
            var repo = new DataContext();
            var machine = await GetMachine(repo);
            return machine.Sensor;
        }

        private async Task<Machine> GetMachine(DataContext repo)
        {
            if (await CheckIfExist(repo))
            {
                return await repo.Machines.FirstAsync(x => x.Name == _machineName);
            }
            await Create(repo, new Machine { Name = _machineName, Status = MachineStatusEnum.Off });
            return await GetMachine(repo);
        }
        
        private async Task Create(DataContext repo, Machine entity)
        {
            await repo.Machines.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        private Task<bool> CheckIfExist(DataContext repo) => repo.Machines.AnyAsync(x => x.Name == _machineName);
    }
}
