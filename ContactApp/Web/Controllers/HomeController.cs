﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Interfaces.UOW;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUOW _uow;

        public HomeController(IUOW uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            var vm = new HomeIndexViewModel()
            {
                Article = _uow.Articles.FindArticleByName("HomeIndex")
            };
            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var vm = new MenuIndexViewModel()
            {
                Categories = _uow.Categories.AllIncluding(p => p.Products)
            };
            return View(vm);
        }

        public ActionResult ApiDemo()
        {
            return View();
        }

    }
}