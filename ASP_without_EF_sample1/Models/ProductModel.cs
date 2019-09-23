using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP_without_EF_sample1.Models
{
    public class ProductModel
    {
            public int ProductID { get; set; }
            [DisplayName("Product Name")]
            public String ProductName { get; set; }

            public decimal Price { get; set; }

            public int Stock { get; set; }

    }
}