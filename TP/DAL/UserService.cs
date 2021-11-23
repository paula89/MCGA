using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;

namespace DAL
{
    public class UserService
    {

        #region Public Methods
        
        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.User.ToListAsync();
        }
        

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<User> Get(int entityId)
        {
            var repo = new DataContext();
            return await repo.User.FirstAsync(x => x.Id == entityId);
        }

        public async Task<User> GetByUserAndPassword(string username, string password)
        {
            var repo = new DataContext();
            return await repo.User.FirstOrDefaultAsync(x => x.Username == username && x.PasswordHash == password);
        }

        public async Task<User> GetByUser(string username)
        {
            var repo = new DataContext();
            return await repo.User.FirstOrDefaultAsync(x => x.Username == username);
        }


        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(User entity)
        {
            var repo = new DataContext();
            var duplicated = await repo.User.AnyAsync(x => x.Username == entity.Username);
            if (duplicated)
            {
                throw new DuplicationException();
            }
            await repo.User.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que actualiza la entidad en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update(User entity)
        {
            var repo = new DataContext();
            repo.User.Update(entity);
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
            repo.User.Remove(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
