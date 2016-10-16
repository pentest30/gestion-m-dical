using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Gc.Core.Data;

namespace Gc.Core.DataManage.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UnitWork _work;
        //private readonly DbManager _manager;

        public Repository(UnitWork work)
        {
            _work = work;
           // _manager = manager;
        }


        public TEntity FindById(object id)
        {
            return _work.Set<TEntity>().Find(id);
        }

        public TEntity Last()
        {
            var q = (from p in _work.Set<TEntity>()
                      
                     select p).LastOrDefault();
            return q;
        }
        public TEntity First()
        {
            return _work.Set<TEntity>().FirstOrDefault();
        }

        public TEntity GetLastBy(Expression<Func<TEntity, int>> predicate)
        {

            return _work.Set<TEntity>().OrderByDescending(predicate).FirstOrDefault();
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _work.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> QueryWith(Expression<Func<TEntity, bool>> predicate, string[] param)
        {
          //  _work.Context = new DbManager();
            var list = _work.Set<TEntity>().Where(predicate);
            return param.Aggregate(list, (current, s) => current.Include(s).Where(predicate));
        }
        public IQueryable<TEntity> QueryWith( string[] param)
        {
            IQueryable<TEntity> list = _work.Set<TEntity>();
            return param.Aggregate(list, (current, s) => current.Include(s).AsQueryable());
        }

        public bool DoesExist(Expression<Func<TEntity, bool>> predicate)
        {
            return _work.Set<TEntity>().Any(predicate);
        }

        public void Update(object id)
        {
            var item = FindById(id);
            if (item != null)
            {
              //  _work.Context.Entry(item).State = EntityState.Detached;
                _work.Context.Entry(item).State = EntityState.Modified;
            }

        }

        public TEntity SingleQueryWith(Expression<Func<TEntity, bool>> predicate, string[] param)
        {
            return _work.Set<TEntity>().FirstOrDefault(predicate);
        }

     

        public void Update(TEntity entity)
        {
           // var local = FindById()
           // _work.Context.Entry(entity).State = EntityState.Detached;
            _work.Context.Entry(entity).State = EntityState.Modified;
            
        }

        public void Update(TEntity entity, object id)
        {
            var local = FindById(id);
            if (local != null)
                _work.Context.Entry(local).State = EntityState.Detached;
            _work.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var item = FindById(id);
            if (item != null)
            {
                _work.Context.Entry(item).State = EntityState.Detached;
                _work.Context.Entry(item).State= EntityState.Deleted;
            }
        }
        public void Delete(TEntity entity)
        {
            _work.Context.Entry(entity).State = EntityState.Deleted;
        }

        public void Insert(TEntity entity)
        {
            _work.Set<TEntity>().Add(entity);
        }

        public void InserOrUpdate(TEntity entity)
        {
            _work.Context.Set<TEntity>().AddOrUpdate();
        }

        public IList<TEntity> Query()
        {
            return _work.Set<TEntity>().ToList();
        }

        public IList<TEntity> FromCachQyery()
        {
           // return _work.Set<TEntity>().FromMemoryCache().ToList();
            return null;
        } 
        public void SaveChanges()
        {
            _work.Save();
        }
    }
}