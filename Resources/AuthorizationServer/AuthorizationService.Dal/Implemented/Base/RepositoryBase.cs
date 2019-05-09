using AuthorizationService.Dal.Base;
using AuthorizationService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService.Dal.Implemented.Base
{
    // <summary>
    ///     Implementing generic repository
    /// </summary>

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly LogInDbContext _dbContext;

        public RepositoryBase(LogInDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// It simply insert record to db and save changes async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// It simply delete record to db and save changes async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Return a list able to querry know as lazyloading
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return this._dbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// It simply gets the data for given id and finds the entity with this unique id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        ///  It simply update record to db and save changes async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

