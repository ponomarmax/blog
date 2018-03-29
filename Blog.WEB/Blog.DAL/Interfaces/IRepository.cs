using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(int id);
        T Get(int id);
        void Create(T item);
        void Modify(T item);
        void Delete(int id);
        void Dispose();
    }
}
