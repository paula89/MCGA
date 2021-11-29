using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;
using Helpers.Enum;

namespace DAL
{
    public class LogService
    {

        #region Public Methods

        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Log>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.Logs.ToListAsync();
        }

        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(Log entity)
        {
            var repo = new DataContext();
            await repo.Logs.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
