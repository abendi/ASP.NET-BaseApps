using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class ImageIndexViewModel
    {
        public List<Image> Images { get; set; }
    }

    public class ImageCreateViewModel
    {
        public HttpPostedFileBase Attachment { get; set; }

        public string ImageUrl { get; set; }
    }
}