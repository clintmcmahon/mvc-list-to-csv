using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ListToCSV.Models;
using System.IO;
using CsvHelper;

namespace ListToCSV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            //Create the test list
            var list = new List<ListItem>()
    {
        new ListItem(){Id = 1, Name = "Jerry"},
        new ListItem(){Id = 2, Name="George"},
        new ListItem(){Id = 3, Name="Kramer"},
        new ListItem(){Id = 4, Name = "Elaine"}
     };

            byte[] result;
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture))
                    {
                        csvWriter.WriteRecords(list);
                        streamWriter.Flush();
                        result = memoryStream.ToArray();
                    }
                }
            }

            return new FileStreamResult(new MemoryStream(result), "text/csv") { FileDownloadName = "filename.csv" };
        }

    }
}
