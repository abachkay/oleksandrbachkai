using System;
using System.Collections.Generic;

namespace oleksandrbachkai.DataAccess
{
    public interface IRepository<T>: IDisposable
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Insert(T data);

        void Delete(int id);

        void Update(int id, T data);
    }
}
