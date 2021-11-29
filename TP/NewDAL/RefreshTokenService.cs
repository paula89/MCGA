using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;

namespace DAL
{
    public class RefreshTokenService
    {

        #region Public Methods
        
        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RefreshToken>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.RefreshTokens.ToListAsync();
        }
        

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<RefreshToken> Get(int entityId)
        {
            var repo = new DataContext();
            return await repo.RefreshTokens.FirstAsync(x => x.Id == entityId);
        }


        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<RefreshToken> GetByToken(string token)
        {
            var repo = new DataContext();
            return await repo.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        }

        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(RefreshToken entity)
        {
            var repo = new DataContext();
            await repo.RefreshTokens.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que actualiza la entidad en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update(RefreshToken entity)
        {
            var repo = new DataContext();
            repo.RefreshTokens.Update(entity);
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
            repo.RefreshTokens.Remove(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
