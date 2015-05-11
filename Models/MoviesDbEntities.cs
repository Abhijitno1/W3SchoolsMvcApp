using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace W3SchoolsMvcApp.Models
{
    public class MoviesDbEntities: DbContext
    {
        public IDbSet<Movie> Movies { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
            //throw new UnintentionalCodeFirstException();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            base.OnModelCreating(modelBuilder);
        }
        */
    }
}