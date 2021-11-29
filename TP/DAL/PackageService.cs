using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helpers.Request;
using Helpers;
using Helpers.CustomExeptions;
using Helpers.Enum;
using System;

namespace DAL
{
    public class PackageService
    {
        #region Public Methods
        
        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Package>> DisplayAll()
        {
            var repo = new DataContext();
            return await repo.Packages.ToListAsync();
        }

        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Package>> DisplayAllByStatus(PackageStatusEnum status)
        {
            var repo = new DataContext();
            return await repo.Packages.FromSqlRaw($"SELECT * FROM [dbo].Packages WHERE status = {(int)status}").ToListAsync();
        }

        /// <summary>
        /// Metodo que trae todas las entidades y las transforma para ser vistas.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountByStatus(PackageStatusEnum status)
        {
            var repo = new DataContext();
            return await repo.Packages.CountAsync(x => x.Status == status);
        }

        public Task ChangeStatus(PackageStatusEnum packageStatusEnum)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo para obtener la entidad por su Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<Package> Get(int entityId)
        {
            var repo = new DataContext();
            return await repo.Packages.FirstAsync(x => x.Id == entityId);
        }

        /// <summary>
        /// Metodo que agrega la entidad a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add()
        {
            var repo = new DataContext();
            await repo.Packages.AddAsync(new Package() { Status = PackageStatusEnum.Joined});
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo que actualiza la entidad en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        public async Task ChangeStatus(Package entity)
        {
            var repo = new DataContext();
            repo.Packages.Update(entity);
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
            repo.Packages.Remove(entity);
            await repo.SaveChangesAsync();
        }

        #endregion

    }
}
