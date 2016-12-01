using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain;
using Interfaces.UOW;
using NLog.LogReceiverService;
using Web.Areas.Admin.ViewModels;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImagesController : BaseController
    {
        private readonly IUOW _uow;


        public ImagesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/Images
        public ActionResult Index()
        {
            var vm = new ImageIndexViewModel()
            {
                Images = _uow.Images.All
            };
            return View(vm);
        }

        // GET: Admin/Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = _uow.Images.GetById(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Admin/Images/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Admin/Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(ImageCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Attachment != null && vm.Attachment.ContentLength > 0)
                {
                    if (!vm.Attachment.ContentType.ToLower().StartsWith("image"))
                    {
                        ModelStateHelper.AddFor<ImageCreateViewModel>(ModelState, s => s.Attachment,
                            "File is not of image type");
                        return View();
                    }
                    try
                    {
                        ImageHelper.SaveAs(vm.Attachment, vm.Image.ImageUrl);
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Something went wrong!");
                        return View();
                    }
                    Image image = new Image()
                    {
                        ImageUrl = vm.Image.ImageUrl + Path.GetExtension(vm.Attachment.FileName)
                    };
                    _uow.Images.Add(image);
                    _uow.Commit();
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }

        // GET: Admin/Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = _uow.Images.GetById(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Admin/Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Images.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
