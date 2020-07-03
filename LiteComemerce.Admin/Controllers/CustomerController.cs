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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue = "", string country = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Customer> ListOfCustomer = CatalogBLL.ListOfCustomers(page, pageSize, searchValue, out rowCount, country);

            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Coutries = country,
                Data = ListOfCustomer
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
                    ViewBag.Title = "Creat new customer";
                    ViewBag.Method = "Add";
                    Customer newCustomer = new Customer()
                    {
                        CustomerID = "",
                    };
                    return View(newCustomer);
                }
                else
                {
                    ViewBag.Title = "Edit a customer";
                    ViewBag.Method = "Edit";
                    Customer editCustomer = CatalogBLL.GetCustomer(id);
                    if (editCustomer == null)
                        return RedirectToAction("Index");
                    return View(editCustomer);
                }          
            }catch(Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }

        [HttpPost]
        public ActionResult Input(Customer model, string Method)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CustomerID) || model.CustomerID.Length != 5)
                ModelState.AddModelError("CustomerID", "CompanyName Expected");
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

            if (!ModelState.IsValid)
            {
               // ViewBag.Title = model.CustomerID == "" ? "Creat new customer" : "Edit a customer";

               // Method = ViewBag.Method == "Add" ? "Add" : "Edit";
                ViewBag.Title = Method == "Add" ? "Creat new customer" : "Edit a customer";
                ViewBag.Method = Method;
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 

            Customer exitsCustomer = CatalogBLL.GetCustomer(model.CustomerID);

            if (Method == "Add")
            {
                if(exitsCustomer != null)
                {
                    ViewBag.Title = Method == "Add" ? "Creat new customer" : "Edit a customer";
                    ViewBag.Method = Method;
                    ModelState.AddModelError("CustomerID", "CustomerID exits");
                    return View(model);
                }              
                CatalogBLL.AddCustomer(model);              
                
            }
            else
            {
                
                CatalogBLL.UpdateCustomer(model);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(string[] customerIDs = null)
        {
            if (customerIDs != null)
            {
                CatalogBLL.DeleteCustomers(customerIDs);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}