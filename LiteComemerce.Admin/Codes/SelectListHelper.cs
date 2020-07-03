using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin
{
    public class SelectListHelper
    {
        /// <summary>
        /// selectlist các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--All Countries" });
            }
            List<Countrie> data = CatalogBLL.ListOfCountries();
            foreach (var country in data)
            {
                list.Add(new SelectListItem() { Value = country.CountryName, Text = country.CountryName });
            }

            return list;

        }

        public static List<SelectListItem> Categories(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--All Categories--" });
            }
            // lấy ds category db
            List<Categorie> data = CatalogBLL.ListOfCategories();
            foreach(var category in data)
            {
                list.Add(new SelectListItem() { Value = category.CategoryID.ToString() , Text = category.CategoryName });
            }
            
            return list;

        }

        public static List<SelectListItem> Suppliers(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--All Suppliers--" });
            }
            List<Supplier> data = CatalogBLL.ListOfSuppliers();
            foreach (var supplier in data)
            {
                list.Add(new SelectListItem() { Value = supplier.SupplierID.ToString(), Text = supplier.CompanyName });
            }
            return list;

        }

        public static List<SelectListItem> Attributes(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "--All AttributeName--" });
            }
            List<Attributes> data = CatalogBLL.ListOfAttributes();
            foreach (var att in data)
            {
                list.Add(new SelectListItem() { Value = att.AttributeName, Text = att.AttributeName });
            }
            return list;
        }

        public static List<SelectListItem> Customer(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--All Customer--" });
            }
            List<Customer> data = CatalogBLL.ListOfCustomer();
            foreach (var att in data)
            {
                list.Add(new SelectListItem() { Value = att.CustomerID, Text = att.CompanyName });
            }
            return list;
        }

        public static List<SelectListItem> Employee(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "--All Customer--" });
            }
            List<Employee> data = CatalogBLL.ListOfEmployees();
            foreach (var att in data)
            {
                list.Add(new SelectListItem() { Value = att.EmployeeID.ToString(), Text = att.LastName });
            }
            return list;
        }

        public static List<SelectListItem> Shipper(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "--All Shipper--" });
            }
            List<Shipper> data = CatalogBLL.ListOfShippers();
            foreach (var att in data)
            {
                list.Add(new SelectListItem() { Value = att.ShipperID.ToString(), Text = att.CompanyName });
            }
            return list;
        }

        public static List<SelectListItem> Product(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "--All Product--" });
            }
            List<Product> data = CatalogBLL.ListOfProducts();
            foreach (var att in data)
            {
                list.Add(new SelectListItem() { Value = att.ProductID.ToString(), Text = att.ProductName });
            }
            return list;
        }
    }
}