using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public interface IRepository<T>: IDisposable
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task Insert(T data);

        Task Delete(int id);

        Task Update(int id, T data);
    }
}