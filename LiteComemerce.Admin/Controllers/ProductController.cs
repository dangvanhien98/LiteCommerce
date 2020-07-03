using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.ADMINISTRATOR)]
    public class ProductController : Controller
    {
        // GET: Products
        public ActionResult Index(int page = 1, string searchValue = "", string supplier = "", string category = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Product> ListOfProduct = CatalogBLL.ListOfProducts(page, pageSize, searchValue, out rowCount, supplier, category);
            var model = new Models.ProductPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Category = category.ToString(),
                Supplier = supplier.ToString(),
                Data = ListOfProduct
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if(string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create New Product";
                Product newProduct = new Product()
                {
                    ProductID = 0,
                    PhotoPath = "637272543497933169.jpg"
                };
                return View(newProduct);
            }
            else
            {
                ViewBag.Title = "Edit a Product";
                Product editProduct = CatalogBLL.GetProduct(Convert.ToInt32(id));
                if (editProduct == null)
                    return RedirectToAction("Index");
                return View(editProduct);
                
            }
           
        }

        [HttpPost]
        public ActionResult Input(Product model, HttpPostedFileBase uploadFile)
        {
            if (string.IsNullOrEmpty(model.ProductName))
                ModelState.AddModelError("ProductName", "ProductName Expected");           
            if (model.CategoryID.ToString() == "0")
                ModelState.AddModelError("CategoryID", "CategoryName Expected");
            if (model.SupplierID.ToString() == "0")
                ModelState.AddModelError("SupplierID", "SupplierName Expected");
            if (model.UnitPrice == 0)
                ModelState.AddModelError("UnitPrice", "UnitPrice Expected");
            if (string.IsNullOrEmpty(model.QuantityPerUnit))
                model.QuantityPerUnit = "";
            if (string.IsNullOrEmpty(model.Descriptions))
                model.Descriptions = "";

            if (uploadFile != null)
            {
                string folder = Server.MapPath("~/Images/Uploads/Products");
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(uploadFile.FileName)}";
                //  string fileName = Guid.NewGuid() + uploadFile.FileName;
                string filePath = Path.Combine(folder, fileName);
                uploadFile.SaveAs(filePath);
                model.PhotoPath = fileName;
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ProductID == 0 ? "Create new Product" : "Edit a Product";
                return View(model);
            }

            if(model.ProductID == 0)
            {
                CatalogBLL.AddProduct(model);
            }
            else
            {
                CatalogBLL.UpdateProduct(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int[] productIDs = null)
        {
            if(productIDs != null)
            {
                CatalogBLL.DeleteProducts(productIDs);
            }
            return RedirectToAction("Index");
        }

        //add att
        [HttpGet]
        public ActionResult AddAttribute(string id = "")
        {
            ViewBag.Title = "Create New ProductAttribute";
            ProductAttribute newproductAttribute = new ProductAttribute()
            {
                AttributeID = 0,
                ProductID = Convert.ToInt32(id),

            };
            return View(newproductAttribute);

        }

        [HttpPost]
        public ActionResult AddAttribute(ProductAttribute model)
        {
            if (model.AttributeName == "0")
                ModelState.AddModelError("AttributeName", "AttributeName Expected");
            if (string.IsNullOrEmpty(model.AttributeValues))
                ModelState.AddModelError("AttributeValues", "AttributeValues Expected");
            if (model.DisplayOrder == 0)
                model.DisplayOrder = 0;



            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.AttributeID == 0 ? "Create New ProductAttribute" : "Edit a ProductAttribute";
                return View(model);
            }

            if (model.AttributeID == 0)
            {
                CatalogBLL.AddProductAttribute(model);
            }
         
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult InputAttributes(string id = "")
        {
            
                ViewBag.Title = "Edit a ProductAttribute";
                ProductAttribute editProductAttribute = CatalogBLL.GetProductAttribute(Convert.ToInt32(id));
                if (editProductAttribute == null)
                    return RedirectToAction("Index");
                return View(editProductAttribute);              
           
        }

        [HttpPost]
        public ActionResult InputAttributes(ProductAttribute model)
        {
            if (string.IsNullOrEmpty(model.AttributeName))
                ModelState.AddModelError("AttributeName", "AttributeName Expected");
            if (string.IsNullOrEmpty(model.AttributeValues))
                ModelState.AddModelError("AttributeValues", "AttributeValues Expected");
            if (model.DisplayOrder == 0)
                model.DisplayOrder = 0;


            
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.AttributeID == 0 ? "Create New ProductAttribute" : "Edit a ProductAttribute";
                return View(model);
            }

            if (model.AttributeID == 0)
            {
                CatalogBLL.AddProductAttribute(model);
            }
            else
            {
                CatalogBLL.UpdateProductAttribute(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteAtt(int[] productAttributeIDs = null)
        {
            if (productAttributeIDs != null)
            {
                CatalogBLL.DeleteProductAttributes(productAttributeIDs);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetailAttributes(int page = 1, string searchValue = "", int id = 0)
        {
            int pageSize = 3;
            int rowCount = 0;
            List<ProductAttribute> ListOfProductAttribute = CatalogBLL.ListOfProductAttribute(page, pageSize, searchValue, out rowCount, id);
            var model = new Models.ProductAttributePaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                ProductID = id.ToString(),
                Data = ListOfProductAttribute
            };
            return View(model);

        }
    }
}