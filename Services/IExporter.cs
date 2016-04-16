using System;
using System.Collections.Generic;
using W3SchoolsMvcApp.Models;

namespace W3SchoolsMvcApp.Services
{
    public interface IExporter
    {
        byte[] ExportMoviesList(IEnumerable<Movie> moviesList);
    }
}
