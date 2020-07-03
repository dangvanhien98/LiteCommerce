﻿using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlSever;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public class UserAccountBLL
    {
        private static string _connectionString;

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static UserAccount Authorize(string username, string password, UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDB;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    userAccountDB = new EmployeeUserAccountDAL(_connectionString);
                    break;
                case UserAccountTypes.Customer:
                    userAccountDB = new CustomerUserAccountDAL(_connectionString);
                    break;
                default:
                    return null;      
            }
            return userAccountDB.Authorize(username, password);
        }
    }
}
