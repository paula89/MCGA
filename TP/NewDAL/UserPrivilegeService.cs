using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;
using System;
using System.Linq;

namespace DAL
{
    public class UserPrivilegeService
    {

        #region Public Methods
        
        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPrivilege>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.UsersPrivileges.ToListAsync();
        }
        

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<UserPrivilege> Get(int entityId)
        {
            var repo = new DataContext();
            return await repo.UsersPrivileges.FirstAsync(x => x.Id == entityId);
        }

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<UserPrivilege> GetUserPrivilege(int userId, int privilegeId)
        {
            var repo = new DataContext();
            return await repo.UsersPrivileges.FirstOrDefaultAsync(x => x.UserId == userId && x.PrivilegeId == privilegeId);
        }

        public async Task<List<UserPrivilege>> GetPrivilegesByUserId(int userId)
        {
            var repo = new DataContext();
            return await repo.UsersPrivileges.Where(x => x.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(UserPrivilege entity)
        {
            var repo = new DataContext();
            await repo.UsersPrivileges.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que actualiza la entidad en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update(UserPrivilege entity)
        {
            var repo = new DataContext();
            repo.UsersPrivileges.Update(entity);
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
            repo.UsersPrivileges.Remove(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
