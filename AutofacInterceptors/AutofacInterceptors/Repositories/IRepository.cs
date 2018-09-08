using System.Collections.Generic;
using System.Linq;
using AutofacInterceptors.Models;

namespace AutofacInterceptors.Repositories
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        T GetById(int id);

        IEnumerable<T> GetAllActive();

        IEnumerable<T> GetAll();

        void SaveChanges();
    }
}