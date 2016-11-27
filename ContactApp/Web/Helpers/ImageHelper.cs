using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public static class ImageHelper
    {
        private static string rootPath = HttpContext.Current.Server.MapPath("~/Content/Uploads/");
        public static void SaveAs(HttpPostedFileBase image, string fileName)
        {
            var extension = Path.GetExtension(image.FileName);
            image.SaveAs(rootPath + fileName + extension);
        }

        public static void Delete(string fileName)
        {
            if (File.Exists(rootPath + fileName))
            {
                File.Delete(rootPath + fileName);
            }
        }
    }
}