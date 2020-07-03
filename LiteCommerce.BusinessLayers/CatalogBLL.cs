using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlSever;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Các chức năng quản lý nghiệp vụ liên quan đến
    /// quản lý dữ liệu chung của hệ thống như : nhà cung cấp , khách hàng , mặt hàng ....
    /// </summary>
    public static class CatalogBLL
    {
        /// <summary>
        /// Hàm phải được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SqlSever.SupplierDAL(connectionString);
            CustomerDB = new DataLayers.SqlSever.CustomerDAL(connectionString);
            ShipperDB = new DataLayers.SqlSever.ShipperDAL(connectionString);
            CategorieDB = new DataLayers.SqlSever.CategorieDAL(connectionString);
            EmployeeDB = new DataLayers.SqlSever.EmployeeDAL(connectionString);
            ChangePassDB = new DataLayers.SqlSever.ChangePassDAL(connectionString);
            ProductDB = new DataLayers.SqlSever.ProductDAL(connectionString);
            CountrieDB = new DataLayers.SqlSever.CountrieDAL(connectionString);
            ProductAttributeDB = new DataLayers.SqlSever.ProductAttributeDAL(connectionString);
            AttributeDB = new DataLayers.SqlSever.AttributeDAL(connectionString);
            OrderDB = new DataLayers.SqlSever.OrderDAL(connectionString);
            OrderDetailDB = new DataLayers.SqlSever.OrderDetailDAL(connectionString);
            OrderNewDB = new DataLayers.SqlSever.OrderNewDAL(connectionString);
            OrderDetailNewDB = new DataLayers.SqlSever.OrderDetailNewDAL(connectionString);
        }

       // Khai báo các thuộc tính giao tiếp với DAL
        private static ISupplierDAL SupplierDB { get; set; }

        private static ICustomerDAL CustomerDB { get; set; }

        private static IShipperDAL ShipperDB { get; set; }

        private static ICategorieDAL CategorieDB { get; set; }

        private static IEmployeeDAL EmployeeDB { get; set; }

        private static IChangePassDAL ChangePassDB { get; set; }

        private static IProductDAL ProductDB { get; set; }

        private static ICountrieDAL CountrieDB { get; set; }

        private static IProductAttributeDAL ProductAttributeDB { get; set; }

        private static IAttributeDAL AttributeDB { get; set; }

        private static IOrderDAL OrderDB { get; set; }

        private static IOrderDetailDAL OrderDetailDB { get; set; }

        private static IOrderNewDAL OrderNewDB { get; set; }

        private static IOrderDetailNewDAL OrderDetailNewDB { get; set; }

        public static int AddOrderDetailNew(OrderDetailNew data)
        {
            return OrderDetailNewDB.Add(data);
        }
        //ordernew
        public static List<OrderNew> ListOfOrderNew(int page, int pagesize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagesize < 0)
                pagesize = 20;
            rowCount = OrderNewDB.Count(searchValue);
            return OrderNewDB.List(page, pagesize, searchValue);
        }

        public static int AddOrderNew(OrderNew data)
        {
            return OrderNewDB.Add(data);
        }

        //order detail
        public static List<OrderDetail> ListOfOrderDetail(int id)
        {
            return OrderDetailDB.ListDetail(id);
        }


        //order
        public static List<Order> ListOfOrder()
        {
            return OrderDB.List();
        }

        public static List<Order> ListOfOrderAccepted()
        {
            return OrderDB.ListAccepted();
        }

        public static int DeleteOrderDetail(int[] orderIDs)
        {
            return OrderDB.Delete(orderIDs);
        }


        public static int DeleteOrder(int[] orderIDs)
        {
            return OrderDB.DeleteOrder(orderIDs);
        }

        public static bool UpdateOrder(int orderID)
        {
            return OrderDB.Update(orderID);
        }

        //country
        public static List<Countrie> ListOfCountries()
        {          
            return CountrieDB.List();
        }

        public static List<Countrie> ListOfCountries(int page, int pagesize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagesize < 0)
                pagesize = 20;
            rowCount = CountrieDB.Count(searchValue);
            return CountrieDB.List(page, pagesize, searchValue);
        }

        public static int Add(Countrie data)
        {
            return CountrieDB.Add(data);
        }

        public static Countrie Get(string Country)
        {
            return CountrieDB.Get(Country);
        }

        public static bool Update(Countrie data, string country)
        {
            return CountrieDB.Update(data, country);
        }

        public static int Delete(string[] Countries)
        {
            return CountrieDB.Delete(Countries);
        }

        //attribute
        public static List<Attributes> ListOfAttributes()
        {
            return AttributeDB.List();
        }
        #region Supplier
        // Khai báo các chức năng xử lý nghiệp vụ
        /// <summary>
        /// Hiển thị suppliers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);      
        }

        public static List<Supplier> ListOfSuppliers()
        {
            return SupplierDB.List(1, -1, "");
        }

        /// <summary>
        /// lấy supplier by id
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        
        /// <summary>
        /// thêm supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }

        /// <summary>
        /// cập nhật supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }

        /// <summary>
        /// delete 1 hoặc nhiều supplier by id
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public static int DeleteSuppliers(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        #endregion

        #region Customer
        // Khai báo các chức năng xử lý nghiệp vụ
        /// <summary>
        /// Hiển thị customers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount, string country)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = CustomerDB.Count(searchValue,country);
            return CustomerDB.List(page, pageSize, searchValue, country);
        }

        public static Customer GetCustomer(string customerId)
        {
            return CustomerDB.Get(customerId);
        }

        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }

        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }


        public static int DeleteCustomers(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }

        public static List<Customer> ListOfCustomer()
        {
            return CustomerDB.List(1, -1, "", "");
        }
        #endregion

        public static List<Employee> ListOfEmployees()
        {
            return EmployeeDB.List(1, -1, "");
        }

        public static List<Product> ListOfProducts()
        {
            return ProductDB.List(1, -1, "","","");
        }
        #region shippers
        // Khai báo các chức năng xử lý nghiệp vụ
        /// <summary>
        /// HIển thị shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }

        public static List<Shipper> ListOfShippers()
        {
            return ShipperDB.List(1, -1, "");
        }

        public static Shipper GetShipper(int shipperId)
        {
            return ShipperDB.Get(shipperId);
        }

        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }


        public static int DeleteShippers(int[] shipperIDs)
        {
            return ShipperDB.Delete(shipperIDs);
        }
        #endregion

        #region categories
        //Khai báo các chức năng xử lý nghiệp vụ
        public static List<Categorie> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = CategorieDB.Count(searchValue);
            return CategorieDB.List(page, pageSize, searchValue);

        }
        public static List<Categorie> ListOfCategories()
        {
            return CategorieDB.List(1, -1, "");
        }
        public static Categorie GetCategorie(int categoryId)
        {
            return CategorieDB.Get(categoryId);
        }


        public static int AddCategorie(Categorie data)
        {
            return CategorieDB.Add(data);
        }


        public static bool UpdateCategorie(Categorie data)
        {
            return CategorieDB.Update(data);
        }


        public static int DeleteCategories(int[] categoryIDs)
        {
            return CategorieDB.Delete(categoryIDs);
        }
        #endregion
        //khai báo các chức năng xử lý nghiệp vụ
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }

        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }

        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }

        public static int DeleteEmployees(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }

        public static int CheckExist(string email)
        {
            return EmployeeDB.CheckExist(email);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }

        // change pass
        public static ChangePass GetEmployeeChange(int employeeId)
        {
            return ChangePassDB.Get(employeeId);
        }

        public static bool UpdatePass(ChangePass data)
        {
            return ChangePassDB.UpdatePass(data);
        }


        ///// product

        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, out int rowCount, string supplier, string category)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 20;
            rowCount = ProductDB.Count(searchValue, supplier, category);
            return ProductDB.List(page, pageSize, searchValue, supplier, category);
        }

        //public static List<Product> ListOfProducts()
        //{
        //    return ProductDB.List(1, -1 , "");
        //}

        public static Product GetProduct(int productId)
        {
            return ProductDB.Get(productId);
        }

        public static int AddProduct(Product data)
        {
            return ProductDB.Add(data);
        }

        public static bool UpdateProduct(Product data)
        {
            return ProductDB.Update(data);
        }


        public static int DeleteProducts(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }

        // product attribute

        public static List<ProductAttribute> ListOfProductAttribute(int page, int pageSize, string searchValue, out int rowCount, int productId)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 20;
            rowCount = ProductAttributeDB.Count(searchValue, productId);
            return ProductAttributeDB.List(page, pageSize, searchValue, productId);
        }

        public static int AddProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Add(data);
        }

        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Update(data);
        }


        public static int DeleteProductAttributes(int[] attrubuteIDs)
        {
            return ProductAttributeDB.Delete(attrubuteIDs);
        }

        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return ProductAttributeDB.Get(attributeID);
        }
    }
}
