using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    [Authorize(Roles =WebUserRoles.ACCOUNTADMIN)]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Employee> ListOfEmployees = CatalogBLL.ListOfEmployees(page,pageSize,searchValue, out rowCount);
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = ListOfEmployees
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {           
                if(string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create New Employee";
                    Employee newEmployee = new Employee()
                    {
                        EmployeeID = 0,
                        PhotoPath = "5a053a74-a925-4e01-b686-cfb7dac5f8c0avatar.png"
                    };
                    return View(newEmployee);
                }
                else
                {
                    ViewBag.Title = "Edit a Employee";
                    Employee editEmployee = CatalogBLL.GetEmployee(Convert.ToInt32(id));             
                    if (editEmployee == null)
                        return RedirectToAction("Index");
                    return View(editEmployee);
                }
               
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }

        [HttpPost]
        public ActionResult Input(Employee model,string[] Roles, HttpPostedFileBase uploadFile)
        {
            if(string.IsNullOrEmpty(model.LastName))
                ModelState.AddModelError("LastName", "LastName Expected");
            if (string.IsNullOrEmpty(model.FirstName))
                ModelState.AddModelError("FirstName", "FirstName Expected");
            if (string.IsNullOrEmpty(model.Title))
                ModelState.AddModelError("Title", "Title Expected");
            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Email Expected");
            if (model.EmployeeID == 0)
            {
                if (CatalogBLL.CheckExist(model.Email) == 1)
                    ModelState.AddModelError("Email", "Email Existed");
            }

            if (string.IsNullOrEmpty(model.Password))
                ModelState.AddModelError("Password", "Password Expected");
            if(Roles != null)
            {
                for(int i=1; i<Roles.Length; i++)
                {
                    model.Roles += "," + Roles[i];
                }
            }
            else
            {
                model.Roles = WebUserRoles.ANONYMOUS;
            }
            DateTime birthDate = model.BirthDate;
            var datestring = "01/01/0001";
            var time1 = "01/01/2002";
            var time2 = "01/01/1900";
            var datecurrent = DateTime.Now;
            DateTime date = DateTime.Parse(datestring, System.Globalization.CultureInfo.InvariantCulture);
            if (birthDate == date)
                ModelState.AddModelError("BirthDate", "BirthDate Expected");

            DateTime hireDate = model.HireDate;
            if (hireDate == date)
                ModelState.AddModelError("HireDate", "HireDate Expected");

            //birthdate từ 1990 đên 2002
            if(DateTime.Compare(birthDate, DateTime.Parse(time2)) < 0)
            {
                ModelState.AddModelError("BirthDate", "BirthDate Expected");
            }
            if (DateTime.Compare(birthDate, DateTime.Parse(time1)) > 0)
            {
                ModelState.AddModelError("BirthDate", "BirthDate Expected");
            }

            if (DateTime.Compare(hireDate, datecurrent) > 0)
            {
                ModelState.AddModelError("hireDate", "BirthDate Expected");
            }

            if (string.IsNullOrEmpty(model.Country))
                model.Country = "";
            
            if (string.IsNullOrEmpty(model.Address))
                model.Address = "";
            if (string.IsNullOrEmpty(model.City))
                model.City = "";
            if (string.IsNullOrEmpty(model.Country))
                model.Country = "";
            if (string.IsNullOrEmpty(model.HomePhone))
                model.HomePhone = "";
            if (string.IsNullOrEmpty(model.Notes))
                model.Notes = "";
            if (string.IsNullOrEmpty(model.PhotoPath))
                model.PhotoPath = "";
            //if (string.IsNullOrEmpty(model.Password))
            //    model.Password = model.Password;

            
            //upload ảnh
            if (uploadFile != null)
            {
                string folder = Server.MapPath("~/Images/Uploads");
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(uploadFile.FileName)}";
              //  string fileName = Guid.NewGuid() + uploadFile.FileName;
                string filePath = Path.Combine(folder, fileName);
                uploadFile.SaveAs(filePath);
                model.PhotoPath = fileName;
            }
            

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "Create new Employee" : "Edit Employee";
                return View(model);
            }
            //Lưu vào DB
            //TODO: Lưu dữ liệu vao DB 


            if (model.EmployeeID == 0)
            {            
                CatalogBLL.AddEmployee(model);
            }
            else
            {
                CatalogBLL.UpdateEmployee(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int[] employeeIDs = null)
        {
            if (employeeIDs != null)
            {
                CatalogBLL.DeleteEmployees(employeeIDs);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword(string id = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "ChangePassword";
                ChangePass changePass = CatalogBLL.GetEmployeeChange(Convert.ToInt32(id));
                if (changePass != null)
                    return View(changePass);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePass model)
        {
            
            if(string.IsNullOrEmpty(model.NewPassWord))
                ModelState.AddModelError("NewPassWord", "NewPassWord Expected");
            if (string.IsNullOrEmpty(model.EnterAgain))
                ModelState.AddModelError("EnterAgain", "EnterAgain Expected");
            if (model.OldPassWord == LiteCommerce.DataLayers.SqlSever.EncodePass.EncodeMD5(model.NewPassWord))
                ModelState.AddModelError("NewPassWord", "The new password must not be the same as the old password");
            if (model.NewPassWord != model.EnterAgain)
                ModelState.AddModelError("EnterAgain", "Password do not match");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "ChangePassword";
                return View(model);
            }
            if(model.EmployeeID != 0)
            {
                CatalogBLL.UpdatePass(model);
                if (!CatalogBLL.UpdatePass(model))
                {                
                    return RedirectToAction("ChangePassword");
                }                          
            }       
            return RedirectToAction("Index");
        }


    }
}