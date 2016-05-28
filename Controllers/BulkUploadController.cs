using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using W3SchoolsMvcApp.Services;
using W3SchoolsMvcApp.Models;

namespace W3SchoolsMvcApp.Controllers
{
    public class BulkUploadController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        //ToDo: Convert to async method
        public ActionResult UploadCSV(HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload != null)
                {
                    //fileUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/"), fileUpload.FileName));
                    if (!fileUpload.FileName.ToLower().EndsWith(".csv"))
                    {
                        ModelState.AddModelError("fileUpload", "Please select a CSV file to upload");
                        return PartialView("Index");
                    }
                    CSVImporter importer = new CSVImporter();
                    List<Movie> newmovies = importer.ParseCSVFile(fileUpload.InputStream);
                    importer.SaveMovies2DB(newmovies);
                    //return RedirectToAction("Index", "Movie");
                    var response = new { Success = true, Message = "Movies uploaded successfully" };
                    return Json(response);
                }
                else
                {
                    ModelState.AddModelError("fileUpload", "Please select a file to upload");
                    return PartialView("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("fileUpload", ex);
                return PartialView("Index");
            }
        }
    }
}
