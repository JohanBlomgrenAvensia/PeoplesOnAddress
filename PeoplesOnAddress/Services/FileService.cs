using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

namespace PeoplesOnAddress.Services
{
    public class FileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public bool SaveToDataBase(IFormFile file)
        {
            bool success = false;
            if (file != null)
            {
                using (TextReader reader = new StreamReader(file.OpenReadStream()))
                {
                    //TODO CHECK IF VALID CSV
                    var csvReader = new CsvReader(reader, configuration: new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    });
                    //csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                    //csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.RegisterClassMap<AddressCsvMap>();
                    var records = csvReader.GetRecords<AddressCsvMap>();

                    //var obj = csvReader.GetRecords<SeoUrlsCsvModel>();
                    //_logger.LogError(ex, "Could not parse file");
                    if (AnalyzerFileReference(file))
                    {
                        success = true;
                    }
                }
            }
            return success;
        }

        private bool AnalyzerFileReference(IFormFile file)
        {
            //TODO Analyze data
            return true;
        }
    }

    public sealed class AddressCsvMap : ClassMap<AddressCsv>
    {
        public AddressCsvMap()
        {
            Map(m => m.FirstName).Name("Förnamn");
            Map(m => m.LastName).Name("Efternamn");
            Map(m => m.PersonNumber).Name("Personnummer");
            Map(m => m.Address).Name("Adress");
            Map(m => m.ApartmentNumber).Name("Lägenhetsnummer");
            Map(m => m.PostalCode).Name("Postnummer");
            Map(m => m.City).Name("Stad");

        }
       
    }

    public class AddressCsv
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PersonNumber { get; set; }
        
        public string Address { get; set; }
        
        public string ApartmentNumber { get; set; }
        
        public string PostalCode { get; set; }
        
        public string City { get; set; }
    }
}
