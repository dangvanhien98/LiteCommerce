using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.STAFF)]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Queue Order";
                ViewBag.SmallTitle = "Đơn Hàng Chờ";
                List<Order> ListOfOrder = CatalogBLL.ListOfOrder();
                var model = new Models.OrderResult()
                {
                    Data = ListOfOrder,
                };

                return View(model);
            }
            else
            {
                ViewBag.Title = "Accepted Order";
                ViewBag.SmallTitle = "Đơn Hàng Đã Xác Nhận";
                List<Order> ListOfOrder = CatalogBLL.ListOfOrderAccepted();
                var model = new Models.OrderResult()
                {
                    Data = ListOfOrder,
                };

                return View(model);
            }
            
        }

        [HttpGet]
        public ActionResult OrderDetail(string id = "")
        {
            if (!string.IsNullOrEmpty(id))
            {              
                ViewBag.Title = "Order Detail";
                ViewBag.SmallTitle = "Chi tiết đơn hàng";
                List<OrderDetail> ListDetail = CatalogBLL.ListOfOrderDetail(Convert.ToInt32(id));
                var model = new Models.OrderDetailResult()
                {
                    Data = ListDetail,
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int[] orderIDs = null)
        {
            if (orderIDs != null)
            {
                if(CatalogBLL.DeleteOrderDetail(orderIDs) != 0)
                {
                    CatalogBLL.DeleteOrder(orderIDs);
                }
                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Accept(string id = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                CatalogBLL.UpdateOrder(Convert.ToInt32(id));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<OrderNew> ListOfOrderNew = CatalogBLL.ListOfOrderNew(page, pageSize, searchValue, out rowCount);
            var model = new Models.OrderNewPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = ListOfOrderNew
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrder(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create Order";
                ViewBag.SmallTitle = "Thêm Order";
                OrderNew newOrder = new OrderNew()
                {
                    OrderID = 0,
                };
                return View(newOrder);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(OrderNew model)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CustomerID))
                ModelState.AddModelError("CustomerID", "CustomerID Expected");
            if (model.EmployeeID == 0)
                ModelState.AddModelError("EmployeeID", "EmployeeID Expected");
            if (model.ShipperID == 0)
                ModelState.AddModelError("ShipperID", "ShipperID Expected");
            if (string.IsNullOrEmpty(model.ShipCountry))
                ModelState.AddModelError("ShipCountry", "ShipCountry Expected");

            if (model.OrderDate == null)
                ModelState.AddModelError("OrderDate", "OrderDate Expected");
            if (model.RequiredDate == null)
                ModelState.AddModelError("RequiredDate", "RequiredDate Expected");
            if (model.ShippedDate == null)
                ModelState.AddModelError("ShippedDate", "ShippedDate Expected");

            if (string.IsNullOrEmpty(model.Freight.ToString()))
                model.Freight = 0;
            if (string.IsNullOrEmpty(model.ShipAddress))
                model.ShipAddress = "";
            if (string.IsNullOrEmpty(model.ShipCity))
                model.ShipCity = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.OrderID == 0 ? "Create new Order" : "Edit Order";
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 


            if (model.OrderID == 0)
            {
                CatalogBLL.AddOrderNew(model);
            }
            
            return RedirectToAction("Create");


        }

        [HttpGet]
        public ActionResult AddDetail(string id = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create OrderDetail";
                ViewBag.SmallTitle = "Thêm Chi Tiết Đơn Hàng";
                OrderDetailNew newOrder = new OrderDetailNew()
                {
                    OrderID = Convert.ToInt32(id)
                };
                return View(newOrder);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddDetail(OrderDetailNew model)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (model.ProductID == 0)
                ModelState.AddModelError("ProductID", "ProductID Expected");
            if (model.UnitPrice == 0)
                ModelState.AddModelError("UnitPrice", "UnitPrice Expected");
            if (model.Quantity == 0)
                ModelState.AddModelError("Quantity", "Quantity Expected");
            if (model.Discount == 0)
                model.Discount = 0;
            if (!ModelState.IsValid)
            {
                model.OrderID = model.OrderID;
                ViewBag.Title = model.OrderID != 0 ? "Create new OrderDetail" : "Edit Order";
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 


            if (model.OrderID != 0)
            {
                CatalogBLL.AddOrderDetailNew(model);
            }

            return RedirectToAction("Create");


        }
    }


}