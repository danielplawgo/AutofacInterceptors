using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutofacInterceptors.Models;
using AutofacInterceptors.ViewModels.Products;
using AutoMapper;

namespace AutofacInterceptors.Mappers
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();

            CreateMap<Product, IndexItemViewModel>();
        }
    }
}