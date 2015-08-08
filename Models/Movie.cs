using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using W3SchoolsMvcApp.Infrastructure;

namespace W3SchoolsMvcApp.Models
{
    //[Bind(Exclude = "ID")]
    public class Movie
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Director { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Release Date")]  
        [DateRange("1960/01/01", "2020/12/12")]
        public DateTime? ReleaseDate { get; set; }
    }
}