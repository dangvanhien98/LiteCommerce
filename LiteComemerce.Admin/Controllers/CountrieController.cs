using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    public class CountrieController : Controller
    {
        // GET: Countrie
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Countrie> ListOfCountries = CatalogBLL.ListOfCountries(page, pageSize, searchValue, out rowCount);
            var model = new Models.CountriePaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = ListOfCountries
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new country";
                ViewBag.SmallTitle = "Thêm Quốc Gia";
                Countrie newCountry = new Countrie()
                {
                    CountryName = ""
                };
                return View(newCountry);
            }
            else
            {
                ViewBag.Title = "Edit a country";
                ViewBag.SmallTitle = "Sửa Quốc Gia";
                Countrie editCountry = CatalogBLL.Get(id);
                return View(editCountry);
            }

        }

        [HttpPost]
        public ActionResult Input(Countrie model, string method, string country)
        {
            if (string.IsNullOrEmpty(model.CountryName))
            {
                ModelState.AddModelError("CountryName", "CountryName Expected");
            }
           
           

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CountryName == "" ? "Create new Country" : "Edit a Country";
                return View(model);
            }

            if (method == "Edit a country")
            {
                CatalogBLL.Update(model, country);
            }
            else
            {
                if (CatalogBLL.Get(model.CountryName) != null)
                {
                    ModelState.AddModelError("CountryName", "CountryName Existed");
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Title = model.CountryName == "" ? "Create new Country" : "Edit a Country";
                        return View(model);
                    }
                }
                else
                    CatalogBLL.Add(model);


            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string[] Countries = null)
        {
            if (Countries != null)
            {
                CatalogBLL.Delete(Countries);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}