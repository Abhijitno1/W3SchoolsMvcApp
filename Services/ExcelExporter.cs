using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using W3SchoolsMvcApp.Models;
using System.Xml.Xsl;
using System.Xml.XPath;


namespace W3SchoolsMvcApp.Services
{
    public class ExcelExporter
    {
        public Stream ExportMoviesList(IEnumerable<Movie> movieList)
        {
            var dataFilePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "MoviesListDummyData.xml");
            var reader = new StreamReader(dataFilePath);

            return reader.BaseStream;
            //return Transform2SpreadsheetMLUsingXSL(reader);
        }

        private Stream Transform2SpreadsheetMLUsingXSL(StreamReader reader)
        {
            //Assumption: input stream is an XML Document
            XPathDocument document = new XPathDocument(reader);
            XslCompiledTransform transform = new XslCompiledTransform();
            var xslFilePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "MoviesList2HTML.xsl");
            transform.Load(xslFilePath);
            MemoryStream outputStream = new MemoryStream();
            transform.Transform(document, null, outputStream);
            return outputStream;
        }
    }
}