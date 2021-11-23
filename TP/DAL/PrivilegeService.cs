using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;

namespace DAL
{
    public class PrivilegeService
    {

        #region Public Methods
        
        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Privilege>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.Privileges.ToListAsync();
        }
        

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<Privilege> Get(int entityId)
        {
            var repo = new DataContext();
            return await repo.Privileges.FirstAsync(x => x.Id == entityId);
        }

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<Privilege> GetByDescription(string description)
        {
            var repo = new DataContext();
            return await repo.Privileges.FirstAsync(x => x.Description == description);
        }

        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(Privilege entity)
        {
            var repo = new DataContext();
            var duplicated = await repo.Privileges.AnyAsync(x => x.Description == entity.Description);
            if (duplicated)
            {
                throw new DuplicationException();
            }
            await repo.Privileges.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que actualiza la entidad en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update(Privilege entity)
        {
            var repo = new DataContext();
            repo.Privileges.Update(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que elimina la entidad mediante el id pasado.
        /// </summary>
        /// <param name="entityId"></param>
        public async Task Delete(int entityId)
        {
            var repo = new DataContext();
            var entity = await Get(entityId);
            repo.Privileges.Remove(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
