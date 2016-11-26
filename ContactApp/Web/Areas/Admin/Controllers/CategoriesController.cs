using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain;
using Interfaces.UOW;
using NLog;
using Web.Areas.Admin.ViewModels;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly IUOW _uow;
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();


        public CategoriesController(IUOW uow, ILogger logger)
        {
            _uow = uow;
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);
        }
        // GET: Admin/Categories
        public ActionResult Index()
        {
            var vm = new CategoryIndexViewModel()
            {
                Categories = _uow.Categories.All
            };
            return View(vm);
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _uow.Categories.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Category = new Category();

                vm.Category.CategoryName = new MultiLangString(vm.CategoryName, CultureHelper.GetCurrentNeutralUICulture(),
                    vm.CategoryName, nameof(vm.Category) + "." + nameof(vm.CategoryName));
                _uow.Categories.Add(vm.Category);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _uow.Categories.GetById(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            var vm = new CategoryCreateEditViewModel()
            {
                Category = category,
                CategoryName = category.CategoryName.Value
            };
            return View(vm);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Category = _uow.Categories.GetById(vm.Category.CategoryId);
                vm.Category.CategoryName.Value = vm.CategoryName;
                _uow.Categories.Update(vm.Category);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _uow.Categories.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _uow.Categories.GetById(id);
            _uow.Categories.Delete(category);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
