﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// hiển thị employee có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// đếm số employee tìm kiếm đc
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        Employee Get(int employeeID);

        int Add(Employee data);

        int Delete(int[] employeeIDs);

        bool Update(Employee data);

        int CheckExist(string email);
    }
}
