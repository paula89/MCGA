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
