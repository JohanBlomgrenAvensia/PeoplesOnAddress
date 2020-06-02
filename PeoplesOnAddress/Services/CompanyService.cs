using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace PersonsOnAddress.Services
{
    public class CompanyService
    {
        private readonly IConfiguration _configuration;
        public CompanyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CreateCompany(string name)
        {
            var success = false;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO dbo.Companies (CompanyId, CompanyName) VALUES(@id , @name)";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@name",
                    Value = name
                });
                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = Guid.NewGuid()
                });
                success = command.ExecuteNonQuery() == 1 ? true : false;
                con.Close();
            }
            return success;
        }
    }
}
