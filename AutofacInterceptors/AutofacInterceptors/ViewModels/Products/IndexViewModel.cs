using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacInterceptors.ViewModels.Products
{
    public class IndexViewModel : BaseViewModel
    {
        public IndexViewModel()
        {
            PageTitle = "Lista produktów";
        }

        public IList<IndexItemViewModel> Items { get; set; }
    }

    public class IndexItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}