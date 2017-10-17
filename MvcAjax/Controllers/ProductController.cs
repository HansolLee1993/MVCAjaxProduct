using MvcAjax.Models.NW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAjax.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();

        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Message = "Products";

            return View();

        }

        public ActionResult DailyDeal()
        {
            return View();
        }

        public PartialViewResult DailyDealPartial()
        {
            var product = ctx.Products
                           .OrderByDescending(p => p.UnitPrice)
                           .First();

            return PartialView("_DailyDealPartial", product);
        }

        public ActionResult AjaxForm()
        {
            // Get most popular products
            var products = GetTopSellingProducts(5);

            return View(products);
        }

        public PartialViewResult ProductSearch(string q)
        {
            if (q.ToLower() == "error") throw new Exception("Bad data");

            var products = GetProducts(q);
            return PartialView("_ProductSearchPartial", products);
        }

        public ActionResult AutoComplete()
        {
            // Get most popular products
            var products = GetTopSellingProducts(5);

            return View(products);
        }

        public ActionResult AutoCompleteSearch(string term)
        {
            var products = GetProducts(term).Select(a => new { value = a.ProductName });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TemplatePlugin()
        {
            return View();
        }

        public ActionResult JsonProductSearch(string q)
        {
            if (!String.IsNullOrEmpty(q) && q.ToLower() == "error")
                throw new InvalidOperationException();

            var products = GetProducts(q)
                .Select(p => new { p.ProductID, p.ProductName, p.Category.Description, p.UnitPrice, p.Supplier.CompanyName });

            return Json(products, "application/json", System.Text.Encoding.UTF8,
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductTable(string query)
        {
            if (!String.IsNullOrEmpty(query) && query.ToLower() == "error")
                throw new InvalidOperationException();

            var ctx = new NorthwindEntities();
            var prod = ctx.Products
                .Where(c => c.ProductName.Contains(query.Trim()) || query == null)
                .OrderByDescending(c => c.ProductID)
                .Take(10)
                .ToList();

            /* START AJAX next three lines */
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductTablePartial", prod);
            }                       

            return View(prod);
        }
        public ActionResult CategorySupplierProductTable(string query)
        {
            var models = new Models.ModelContainer();

            models.initModel();
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "Select Category Name", Value = "" });
            foreach (var c in ctx.Categories)
            {
                categories.Add(new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString()
                });
            }
            models.Categories = categories;

            List<SelectListItem> suppliers = new List<SelectListItem>();
            suppliers.Add(new SelectListItem { Text = "Select Supplier Name", Value = ""});
            foreach (var s in ctx.Suppliers)
            {
                suppliers.Add(new SelectListItem()
                {
                    Text = s.CompanyName,
                    Value = s.SupplierID.ToString()
                });
            }
            models.Suppliers = suppliers;



            return View(models);
        }

        /* START AJAX next three lines */
        public PartialViewResult _CategorySupplierSearchPartial(string catID, string supID)
        {
            var prod = new List<Product>();
            if (catID == "" && supID == "")
            {
                prod = ctx.Products.ToList();
            }
            int sup, cat;
            int.TryParse(catID,out cat);
            int.TryParse(supID, out sup);
   
            if (catID == "" && supID != "")
            {
                prod = ctx.Products.Where(p => p.SupplierID == (sup)).ToList();
            }
            else if (catID != "" && supID == "")
            {
                prod = ctx.Products.Where(p => p.CategoryID == (cat)).ToList();
            }
            else if (catID != "" && supID != "")
            {
                prod = ctx.Products.Where(p => p.CategoryID == (cat)).Where(p => p.SupplierID == (sup)).ToList();
            }
            return PartialView("_CategorySupplierSearchPartial", prod);            
        }


#region Helper Methods
        private List<Product> GetProducts(string searchString)
        {
            var products = ctx.Products
                .Where(a => a.ProductName.Contains(searchString))
                .ToList();

            return products;
        }

        private List<Product> GetTopSellingProducts(int count)
        {
            // Group the order details by product and return
            // the product with the highest count
            return ctx.Products
                .OrderByDescending(p => p.Order_Details.Count())
                .Take(count)
                .ToList();
        }

#endregion

    }
} 

