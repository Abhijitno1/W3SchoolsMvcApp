using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace W3SchoolsMvcApp.Models
{
    [Bind(Exclude = "ID")]
    public class Movie
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Director { get; set; }
        [DisplayName("Release Date")]        
        public DateTime? ReleaseDate { get; set; }
    }
}