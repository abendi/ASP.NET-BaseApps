using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class TranslationIndexViewModel
    {
        public IEnumerable<Translation> Translations { get; set; }
        public bool ViewHtml { get; set; }
    }

    public class TranslationCreateEditViewModel
    {
        public SelectList MultiLangStrings { get; set; }

        public Translation Translation { get; set; }
    }
}