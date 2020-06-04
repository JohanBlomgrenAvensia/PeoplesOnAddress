using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
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
                    var readerString = reader.ReadToEnd();

                    Encoding encoding = Encoding.Unicode;

                    var bytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encoding.GetBytes(readerString));

                    var utf8 = Encoding.UTF8.GetString(encoding.GetBytes(readerString));
                    var unicode = Encoding.Unicode.GetString(encoding.GetBytes(readerString));
                    var def = Encoding.Default.GetString(encoding.GetBytes(readerString));
                    var asc = Encoding.ASCII.GetString(encoding.GetBytes(readerString));



                    var memStream = new MemoryStream(bytes);
                    using (TextReader reader2 = new StreamReader(memStream))
                    {



                        //TODO CHECK IF VALID CSV
                        var csvReader = new CsvReader(reader2, configuration: new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ";"

                        });
                        //csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                        //csvReader.Configuration.HasHeaderRecord = true;
                        //csvReader.Configuration.RegisterClassMap<AddressCsvMap>();
                        var records = csvReader.GetRecords<AddressCsv>();


                        //_logger.LogError(ex, "Could not parse file");
                        if (AnalyzerFileReference(file))
                        {
                            success = true;
                        }
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
            Map(m => m.FirstName).Name("förnamn");
            Map(m => m.LastName).Name("efternamn");
            Map(m => m.PersonNumber).Name("personnummer");
            Map(m => m.Address).Name("adress");
            Map(m => m.ApartmentNumber).Name("lägenhetsnummer");
            Map(m => m.PostalCode).Name("postnummer");
            Map(m => m.City).Name("stad");

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
