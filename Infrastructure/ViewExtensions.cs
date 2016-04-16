using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace W3SchoolsMvcApp.Infrastructure
{
    public static class ViewExtensions
    {
        public static string RenderViewToString(this ControllerBase controller, string partialViewName, object model)
        {
            using (var sw = new StringWriter())
            {
                controller.ViewData.Model = model;
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.ToString();
            }
        }
    }
}