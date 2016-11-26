using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;
using Interfaces.UOW;
using NLog;
using Web.Areas.Admin.ViewModels;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TranslationsController : BaseController
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUOW _uow;

        public TranslationsController(ILogger logger, IUOW uow)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

            _uow = uow;
        }

        // GET: Admin/Translations
        public ActionResult Index(bool viewHtml = false)
        {
            var vm = new TranslationIndexViewModel()
            {
                //TODO: .Include(t => t.MultiLangString),
                Translations = _uow.Translations.All.OrderBy(a => a.MultiLangStringId).ToList(),
                ViewHtml = viewHtml
            };
            return View(vm);
        }


        // GET: Admin/Translations/Create
        public ActionResult Create()
        {
            var vm = new TranslationCreateEditViewModel()
            {
                MultiLangStrings =
                    new SelectList(_uow.MultiLangStrings.All.Select(s => new {s.MultiLangStringId, s.Value}),
                        nameof(MultiLangString.MultiLangStringId), nameof(MultiLangString.Value))
            };
            return View(vm);
        }

        // POST: Admin/Translations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TranslationCreateEditViewModel vm)
        {

            if (ModelState.IsValid)
            {
                _uow.Translations.Add(vm.Translation);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            vm.MultiLangStrings =
                new SelectList(_uow.MultiLangStrings.All.Select(s => new { s.MultiLangStringId, s.Value }),
                    nameof(MultiLangString.MultiLangStringId), nameof(MultiLangString.Value));
            return View(vm);
        }

        // GET: Admin/Translations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = _uow.Translations.GetById(id);
            if (translation == null)
            {
                return HttpNotFound();
            }

            var vm = new TranslationCreateEditViewModel()
            {
                Translation = translation,
                MultiLangStrings =
                                new SelectList(_uow.MultiLangStrings.All.Select(s => new { s.MultiLangStringId, s.Value }),
                                    nameof(MultiLangString.MultiLangStringId), nameof(MultiLangString.Value))
            };
            return View(vm);
        }

        // POST: Admin/Translations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(TranslationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Translations.Update(vm.Translation);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            vm.MultiLangStrings =
                new SelectList(_uow.MultiLangStrings.All.Select(s => new {s.MultiLangStringId, s.Value}),
                    nameof(MultiLangString.MultiLangStringId), nameof(MultiLangString.Value));
            return View(vm);
        }

        // GET: Admin/Translations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = _uow.Translations.GetById(id);
            if (translation == null)
            {
                return HttpNotFound();
            }
            return View(translation);
        }

        // POST: Admin/Translations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Translations.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Debug("InstanceId: " + _instanceId + " Disposing:" + disposing);
            if (disposing)
            {
                //_db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}