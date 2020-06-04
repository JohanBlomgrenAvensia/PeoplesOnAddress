using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PeoplesOnAddress.Features.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsOnAddress.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public bool ConnectUserToCompany(string companyId, string userId)
        {
            var success = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "INSERT INTO dbo.CompanyUser (CompanyId, UserId) VALUES(@CompanyId , @UserId)";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@CompanyId",
                    Value = companyId
                });
                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@UserId",
                    Value = userId
                });
                success = command.ExecuteNonQuery() == 1 ? true : false;
                con.Close();
            }
            return success;
        }


        public List<User> GetUsersByCompany(string companyId)
        {
            var users = new List<User>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = @"SELECT UserName FROM [dbo].[AspNetUsers]
                    JOIN CompanyUser ON CompanyUser.UserId = AspNetUsers.Id
                    JOIN Companies ON CompanyUser.CompanyId = Companies.CompanyId
                    WHERE Companies.CompanyId = @CompanyId";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@CompanyId",
                    Value = companyId
                });
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var userName = reader.GetString(0);
                    var user = new User()
                    {
                        Email = userName,
                    };
                    users.Add(user);
                }
                con.Close();
            }
            return users;
        }
    }
}
