using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)

                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();

        }

        //get by id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        // update
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); //updated locally
            return _dbContext.SaveChanges();
        }

        // delete
        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity); //updated locally

            return _dbContext.SaveChanges();


        }

        // insert
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity); //updated locally

            return _dbContext.SaveChanges();


        }

        public IEnumerable<TEntity> GetIEnumerable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return _dbContext.Set<TEntity>();

        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
        {
            return _dbContext.Set<TEntity>()
                             .Where(e => e.IsDeleted != true)
                             .Select(Selector)
                             .ToList();
        }
       

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                                        .Where(Predicate)
                                        .ToList();
        }
    }
}
