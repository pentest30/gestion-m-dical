using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gc.Core.DataManage.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        TEntity Last();
        TEntity First();
        TEntity GetLastBy(Expression<Func<TEntity, int>> predicate);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> QueryWith(Expression<Func<TEntity, bool>> predicate, string[] param);
        TEntity SingleQueryWith(Expression<Func<TEntity, bool>> predicate, string[] param);
        void Update(TEntity entity);
        void Update(TEntity entity, object id);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        void InserOrUpdate(TEntity entity);
        IList<TEntity> Query();
        void SaveChanges();
        IList<TEntity> FromCachQyery();
        IQueryable<TEntity> QueryWith(string[] param);
        bool DoesExist(Expression<Func<TEntity, bool>> predicate);
        void Update(object id);

    }
}