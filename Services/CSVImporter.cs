using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W3SchoolsMvcApp.Models;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace W3SchoolsMvcApp.Services
{
    public class CSVImporter
    {

        public List<Movie> ParseCSVFile(Stream inputStream)
        {
            List<Movie> output = new List<Movie>();
            bool HAS_HEADERS = true;
            using (CsvReader reader = new CsvReader(new StreamReader(inputStream), HAS_HEADERS))
            {
                while (reader.ReadNextRecord())
                {
                    Movie newMovie = new Movie();
                    newMovie.Title = reader["Title"].Trim();
                    newMovie.Director = reader["Director"].Trim();
                    DateTime releaseDate;
                    if (DateTime.TryParse(reader["ReleaseDate"].Trim(), out releaseDate))
                        newMovie.ReleaseDate = releaseDate;

                    output.Add(newMovie);
                }
            }
            return output;
        }

        public void SaveMovies2DB(IEnumerable<Movie> movies)
        {
            MoviesDbEntities ntt = new MoviesDbEntities();
            foreach (Movie movie in movies)
                ntt.Movies.Add(movie);
            ntt.SaveChanges();
        }
    }
}