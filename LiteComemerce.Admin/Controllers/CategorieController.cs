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
    public class CategorieController : Controller
    {
        // GET: Categories
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Categorie> ListOfCategorie = CatalogBLL.ListOfCategories(page, pageSize, searchValue, out rowCount);
            var model = new Models.CategoriePaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = ListOfCategorie
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
                    ViewBag.Title = "Create new Category";
                    Categorie newCategorie = new Categorie()
                    {
                        CategoryID = 0,
                    };
                    return View(newCategorie);
                }
                else
                {
                    ViewBag.Title = "Edit a Category";
                    Categorie editCategorie = CatalogBLL.GetCategorie(Convert.ToInt32(id));
                    if (editCategorie == null)
                        return RedirectToAction("Index");
                    return View(editCategorie);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }

        [HttpPost]
        public ActionResult Input(Categorie model)
        {

            //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CategoryName))
                ModelState.AddModelError("CategoryName", "CategoryName Expected");
            

            if (string.IsNullOrEmpty(model.Description))
                model.Description = "";
           

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryID == 0 ? "Create new Categorie" : "Edit Categorie";
                return View(model);
            }
            //TODO: Lưu dữ liệu vao DB 


            if (model.CategoryID == 0)
            {
                CatalogBLL.AddCategorie(model);
            }
            else
            {
                CatalogBLL.UpdateCategorie(model);
            }
            return RedirectToAction("Index");


        }

        [HttpPost]
        public ActionResult Delete(int[] categoryIDs = null)
        {
            if (categoryIDs != null)
            {
                CatalogBLL.DeleteCategories(categoryIDs);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}