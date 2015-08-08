using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using W3SchoolsMvcApp.Models;

namespace W3SchoolsMvcApp.Services
{
    public class ExcelExporter
    {
        public Stream ExportMoviesList(IEnumerable<Movie> movieList)
        {
            var dataFilePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "MoviesList.xml");
            var reader = new StreamReader(dataFilePath);
            return reader.BaseStream;
        }
    }
}