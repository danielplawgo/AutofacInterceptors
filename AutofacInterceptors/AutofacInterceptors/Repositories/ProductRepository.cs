using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutofacInterceptors.Models;

namespace AutofacInterceptors.Repositories
{
    public partial class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Lazy<DataContext> db)
            : base(db)
        {

        }
    }
}