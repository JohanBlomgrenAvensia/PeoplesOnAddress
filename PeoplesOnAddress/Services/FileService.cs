using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;

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
                using TextReader reader = new StreamReader(file.OpenReadStream());
                //TODO CHECK IF VALID CSV
                //FILE MUST BE SAVED IN UTF-8 TO HANDLE ÅÄÖ CHARACTERS
                var csvReader = new CsvReader(reader, configuration: new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"

                });
                csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                var records = csvReader.GetRecords<AddressCsv>();


                //_logger.LogError(ex, "Could not parse file");
                if (AnalyzerFileReference(file))
                {
                    success = true;
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

    public class AddressCsv
    {
        [Name("Förnamn")]
        public string FirstName { get; set; }
        [Name("Efternamn")]
        public string LastName { get; set; }
        [Name("Personnummer")]
        public string PersonNumber { get; set; }
        [Name("Adress")]
        public string Address { get; set; }
        [Name("Lägenhetsnummer")]
        public string ApartmentNumber { get; set; }
        [Name("Postnummer")]
        public string PostalCode { get; set; }
        [Name("Stad")]
        public string City { get; set; }
    }
}
