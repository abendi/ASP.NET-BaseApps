using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
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

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("QuestionSubmit")]
        public ActionResult QuestionSubmit(QuestionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Question question = new Question()
                {
                    QuestionAsker = vm.Question.QuestionAsker,
                    QuestionAskerEmail = vm.Question.QuestionAskerEmail,
                    QuestionSubject = vm.Question.QuestionSubject,
                    QuestionBody = vm.Question.QuestionBody,
                    QuestionSubmittedTime = DateTime.Now
                };
                _uow.Questions.Add(question);
                _uow.Commit();
                return RedirectToAction("QuestionReceived");
            }
            return View("Contact", vm);
        }

        public ActionResult QuestionReceived()
        {
            Response.AddHeader("REFRESH", "3;URL=/");
            return View("QuestionSubmitted");
        }

    }
}