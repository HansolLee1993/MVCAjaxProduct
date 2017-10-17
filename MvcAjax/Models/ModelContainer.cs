using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAjax.Models.NW;
using System.Web.Mvc;

namespace MvcAjax.Models
{
    public class ModelContainer
    {
        private NorthwindEntities ctx = new NorthwindEntities();

        public List<SelectListItem> Categories;
        public List<SelectListItem> Suppliers;
        public List<Product> Products;


        public String SupID { get; set; }
        public String CatID { get; set; }

        public void initModel()
        {
            Products = ctx.Products.ToList();
        }


    }
}