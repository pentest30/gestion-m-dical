using System.Data.Entity;

namespace Gc.Core.DataManage.Core
{
    public interface IUnitWork
    {
        void Dispose();
        void Save();
        void Dispose(bool disposing);
        DbSet<T> Set<T>() where T : class;
       // DbContext Context;

    }
}