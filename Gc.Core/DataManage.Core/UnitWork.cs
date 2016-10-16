using System;
using System.Data.Entity;
using Gc.Core.Data;
using InteractivePreGeneratedViews;

namespace Gc.Core.DataManage.Core
{
    public class UnitWork:IUnitWork
    {
       public  DbContext Context { get; set; }
        private bool _disposed;
        public UnitWork(DbManager manager)
        {
            Context = manager;
            try
            {
                InteractiveViews.SetViewCacheFactory(Context, new FileViewCacheFactory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cach.xml"));
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
 
        public void Dispose(){
            Dispose(true);
           // GC.SuppressFinalize(this);
        }
 
        public void Save()
        {
            Context.SaveChanges();
        }
 
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)Context.Dispose();
 
            _disposed = true;
        }
 
       

        public DbSet<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }
    }
}