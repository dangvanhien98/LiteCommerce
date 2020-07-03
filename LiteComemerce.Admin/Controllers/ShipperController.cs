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
    public class ShipperController : Controller
    {
        // GET: Shippers
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Shipper> ListOfShipper = CatalogBLL.ListOfShippers(page, pageSize, searchValue, out rowCount);
            var model = new Models.ShipperPaginationResult() {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = ListOfShipper
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
                ViewBag.Title = "Creat new shipper";
                Shipper newShipper = new Shipper()
                {
                    ShipperID = 0
                };
                return View(newShipper);
            }
            else
            {
                ViewBag.Title = "Edit a shipper";
                Shipper editShipper = CatalogBLL.GetShipper(Convert.ToInt32(id));
                if (editShipper == null)
                    return RedirectToAction("Index");
                return View(editShipper);
            }

            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }

        }

        [HttpPost]
        public ActionResult Input(Shipper model)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CompanyName))
                ModelState.AddModelError("CompanyName", "CompanyName Expected");


            if (string.IsNullOrEmpty(model.Phone))
                model.Phone = "";


            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ShipperID == 0 ? "Create new Shipper" : "Edit Shipper";
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 


            if (model.ShipperID == 0)
            {
                CatalogBLL.AddShipper(model);
            }
            else
            {
                CatalogBLL.UpdateShipper(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int[] shipperIDs = null)
        {
            if (shipperIDs != null)
            {
                CatalogBLL.DeleteShippers(shipperIDs);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}