using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlSever
{
    public class CountrieDAL : ICountrieDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public CountrieDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Countrie data)
        {
            int countryId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Countries (CountryName) VALUES(@CountryName)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryName", data.CountryName);


                countryId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return countryId;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) from Countries 
                                                    where @searchValue = N'' or CountryName like @searchValue	";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return count;
        }

        public int Delete(string[] Countries)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Countries
                                            WHERE(CountryName = @Countries)
                                              AND(CountryName NOT IN(SELECT Country FROM Customers))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@Countries", SqlDbType.NChar);
                foreach (string country in Countries)
                {
                    cmd.Parameters["@Countries"].Value = country;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public Countrie Get(string Country)
        {
            Countrie data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Countries WHERE CountryName = @CountryName";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryName", Country);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Countrie()
                        {                         
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Countrie> List()
        {
            List<Countrie> data = new List<Countrie>();
  
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;           

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Countrie()
                        {                           
                            CountryName = Convert.ToString(dbReader["CountryName"]),                         
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public List<Countrie> List(int page, int pagesize, string searchValue)
        {
            List<Countrie> data = new List<Countrie>();

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (select *, ROW_NUMBER() over(order by CountryName) as RowNumber
                                                    from Countries
                                                    where (@searchValue = N'') or (CountryName like @searchValue))as t
                                                    where t.RowNumber between (@page-1)*@pageSize+1 and @page*@pageSize
                                                    order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Countrie()
                        {                        
                            CountryName = Convert.ToString(dbReader["CountryName"]),
                           
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Countrie data, string country)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Countries
                                           SET CountryName = @CountryName                                                                                       
                                          WHERE CountryName = @country";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryName", data.CountryName);
                cmd.Parameters.AddWithValue("@country", country);
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
