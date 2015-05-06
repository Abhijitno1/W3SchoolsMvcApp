using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;
using System.Data.OleDb;

namespace W3SchoolsMvcApp.Models
{
    public class SampleData: DropCreateDatabaseIfModelChanges<MoviesDbEntities>
    {
        protected override void Seed(MoviesDbEntities context)
        {
            string seedDataFile = "SeedData.csv";
            string seedDataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string sql = @"SELECT * FROM [" + seedDataFile + "]";

            using (OleDbConnection connection = new OleDbConnection(
              @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + seedDataDirectory +
              ";Extended Properties=\"Text;HDR=yes\""))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                using (OleDbDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Movie movie = new Movie();
                        movie.ID = reader.GetInt32(0);
                        movie.Title = reader.GetString(1);
                        if (reader.GetString(2)!="NULL")
                        movie.Director = reader.GetString(2);
                        if (reader.GetString(3) != "NULL")
                        movie.ReleaseDate = DateTime.Parse(reader.GetString(3));

                        context.Movies.Add(movie);
                    }
                }
                connection.Close();
            }

            base.Seed(context);
        }
    }
}