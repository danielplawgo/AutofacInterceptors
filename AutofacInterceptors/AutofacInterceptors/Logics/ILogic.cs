using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacInterceptors.Models;

namespace AutofacInterceptors.Logics
{
    public interface ILogic<T> where T : BaseModel
    {
        IEnumerable<T> GetAllActive();

        T GetById(int id);

        void Add(T model);

        void Update(T model);

        void Delete(int id);
    }
}
