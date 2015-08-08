using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W3SchoolsMvcApp.Models;
using W3SchoolsMvcApp.Services;

namespace W3SchoolsMvcApp.Controllers
{ 
    public class MovieController : Controller
    {
        private MoviesDbEntities db;

        //Ctor
        public MovieController(MoviesDbEntities dbNtts)
        {
            db = dbNtts;
        }
        //
        // GET: /Movie/
        public ViewResult Index()
        {
            return View(db.Movies.ToList());
        }

        public PartialViewResult List()
        {
            return PartialView(db.Movies.ToList());
        }

        //
        // GET: /Movie/Details/5

        public PartialViewResult Details(int id)
        {
            Movie movie = db.Movies.Single(m => m.ID == id);
            return PartialView(movie);
        }

        //
        // GET: /Movie/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Movie/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                var response = new { Success = true, Message = "Movie created successfully" };
                return Json(response);
            }
            catch (Exception ex)
            {
                var response = new { Success = false, Message = ex.Message };
                return Json(response);
            }
        }
        
        //
        // GET: /Movie/Edit/5
 
        public PartialViewResult Edit(int id)
        {
            Movie movie = db.Movies.Single(m => m.ID == id);
            return PartialView(movie);
        }

        //
        // POST: /Movie/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Movies.Attach(movie);
                    db.Entry<Movie>(movie).State = EntityState.Modified;
                    //db.ObjectStateManager.ChangeObjectState(movie, EntityState.Modified);
                    db.SaveChanges();
                    var response = new { Success = true, Message = "Movie updated successfully" };
                    return Json(response);
                }
                else
                {
                    return PartialView(movie);
                }
            }
            catch (Exception ex)
            {
                var response = new { Success = false, Message = ex.Message };
                return Json(response);
            }
        }

        //
        // GET: /Movie/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Movie movie = db.Movies.Single(m => m.ID == id);
                db.Movies.Remove(movie);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Movie deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Export2XL()
        {
            try
            {
                ExcelExporter xlXporter = new ExcelExporter();
                var outputStream = xlXporter.ExportMoviesList(null);
                return File(outputStream, "application/vnd.ms-excel", "MovieList.xls");
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
 
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}