﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PeoplesOnAddress.Features.Admin;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PeoplesOnAddress.Services
{
    public class CompanyService
    {
        private readonly string _connectionString;
        private readonly ILogger<CompanyService> _logger;
        public CompanyService(IConfiguration configuration, ILogger<CompanyService> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public bool CreateCompany(string name)
        {
            bool success;
            using (SqlConnection con = new SqlConnection(_connectionString))
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
                    Value = Guid.NewGuid().ToString()
                });
                success = command.ExecuteNonQuery() == 1;
                if (!success)
                {
                    _logger.LogCritical("Could not create company");
                }
                con.Close();
            }
            return success;
        }

        public List<Company> GetCompanies()
        {
            var companies = new List<Company>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "SELECT * FROM dbo.Companies";

                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var companyId = reader.GetString(0);
                    var companyName = reader.GetString(1);
                    var company = new Company()
                    {
                        Id = companyId,
                        Name = companyName
                    };
                    companies.Add(company);
                }
                con.Close();
            }
            return companies;
        }
    }
}
