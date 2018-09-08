using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutofacInterceptors.Models;
using AutofacInterceptors.Repositories;

namespace AutofacInterceptors.Logics
{
    public class ProductLogic : IProductLogic
    {
        private Lazy<IProductRepository> _repository;

        protected IProductRepository Repository
        {
            get { return _repository.Value; }
        }

        public ProductLogic(Lazy<IProductRepository> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllActive()
        {
            return Repository.GetAllActive();
        }

        public Product GetById(int id)
        {
            return Repository.GetById(id);
        }

        public void Add(Product model)
        {
            Repository.Add(model);

            Repository.SaveChanges();
        }

        public void Update(Product model)
        {
            Repository.Update(model);

            Repository.SaveChanges();
        }

        public void Delete(int id)
        {
            Repository.Delete(id);

            Repository.SaveChanges();
        }
    }
}