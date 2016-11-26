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
using Web.Areas.Admin.ViewModels;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientsController : BaseController
    {
        private readonly IUOW _uow;


        public IngredientsController(IUOW uow)
        {
            _uow = uow;
        }
        // GET: Admin/Ingredients
        public ActionResult Index()
        {
            var vm = new IngredientIndexViewModel()
            {
                Ingredients = _uow.Ingredients.All
            };
            return View(vm);
        }

        // GET: Admin/Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = _uow.Ingredients.GetById(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Admin/Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Ingredient.IngredientName = new MultiLangString(vm.IngredientName, CultureHelper.GetCurrentNeutralUICulture(), vm.IngredientName, nameof(vm.Ingredient) + "." + nameof(vm.IngredientName));
                _uow.Ingredients.Add(vm.Ingredient);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Admin/Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = _uow.Ingredients.GetById(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            var vm = new IngredientCreateEditViewModel()
            {
                Ingredient = ingredient,
                IngredientName = ingredient.IngredientName.Value,
                HasGluten = ingredient.HasGluten,
                IsAllergenic = ingredient.IsAllergenic
            };
            return View(vm);
        }

        // POST: Admin/Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IngredientCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Ingredient = _uow.Ingredients.GetById(vm.Ingredient.IngredientId);
                vm.Ingredient.IngredientName.Value = vm.IngredientName;
                vm.Ingredient.HasGluten = vm.HasGluten;
                vm.Ingredient.IsAllergenic = vm.IsAllergenic;

                _uow.Ingredients.Update(vm.Ingredient);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Admin/Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = _uow.Ingredients.GetById(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Admin/Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Ingredients.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
