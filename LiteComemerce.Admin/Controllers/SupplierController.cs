using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.ADMINISTRATOR)]
    public class SupplierController : Controller
    {
        // GET: Suppliers
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            //int pageSize = 3; 
            //int rowCount = 0;
            //List<Supplier> model = CatalogBLL.ListOfSuppliers(page,pageSize,searchValue, out rowCount);
            //ViewBag.rowCount = rowCount;           
            //return View(model);

            int pageSize = 3;
            int rowCount = 0;
            List<Supplier> ListOfSupplier = CatalogBLL.ListOfSuppliers(page, pageSize, searchValue, out rowCount);

            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = ListOfSupplier
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Creat new supplier";
                    Supplier newSupplier = new Supplier()
                    {
                        SupplierID = 0,
                    };
                    return View(newSupplier);
                }
                else
                {
                    ViewBag.Title = "Edit a supplier";
                    Supplier editSupplier = CatalogBLL.GetSupplier(Convert.ToInt32(id));
                    if (editSupplier == null)
                        return RedirectToAction("Index");
                    return View(editSupplier);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Supplier model)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CompanyName))
                ModelState.AddModelError("CompanyName", "CompanyName Expected");
            if (string.IsNullOrEmpty(model.ContactName))
                ModelState.AddModelError("ContactName", "ContactName Expected");
            if (string.IsNullOrEmpty(model.ContactTitle))
                ModelState.AddModelError("ContactTitle", "ContactTitle Expected");

            if (string.IsNullOrEmpty(model.Address))
                model.Address = "";
            if (string.IsNullOrEmpty(model.Country))
                model.Country = "";
            if (string.IsNullOrEmpty(model.City))
                model.City = "";
            if (string.IsNullOrEmpty(model.Address))
                model.Address = "";
            if (string.IsNullOrEmpty(model.Phone))
                model.Phone = "";
            if (string.IsNullOrEmpty(model.Fax))
                model.Fax = "";
            if (string.IsNullOrEmpty(model.HomePage))
                model.HomePage = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierID == 0 ? "Create new Supplier" : "Edit Supplier";
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 


            if (model.SupplierID == 0)
            {
                CatalogBLL.AddSupplier(model);
            }
            else
            {
                CatalogBLL.UpdateSupplier(model);
            }
            return RedirectToAction("Index");


        }

        /// <summary>
        /// xóa suppliers by id
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] supplierIDs = null)
        {
            if(supplierIDs != null)
            {
                CatalogBLL.DeleteSuppliers(supplierIDs);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}