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
using Web.Areas.Admin.ViewModels;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : BaseController
    {
        private readonly IUOW _uow;

        public ProductsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var vm = new ProductIndexViewModel()
            {
                Products = _uow.Products.All
            };
            return View(vm);
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _uow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            var vm = new ProductCreateEditViewModel()
            {
                Categories = new SelectList(_uow.Categories.All.Select(s => new { s.CategoryId, s.CategoryName }), nameof(Category.CategoryId), nameof(Category.CategoryName)),
                Ingredients = new MultiSelectList(_uow.Ingredients.All.Select(s => new { s.IngredientId, s.IngredientName }), nameof(Ingredient.IngredientId), nameof(Ingredient.IngredientName)),
                ImageVM = new ImageCreateViewModel()
            };
            return View(vm);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Product.ProductName = new MultiLangString(vm.ProductName, CultureHelper.GetCurrentNeutralUICulture(),
                    vm.ProductName, nameof(Product) + "." + nameof(Product.ProductName));

                vm.Product.ProductDescription = new MultiLangString(vm.ProductDescription, CultureHelper.GetCurrentNeutralUICulture(),
                    vm.ProductDescription, nameof(Product) + "." + nameof(Product.ProductDescription));

                foreach (var id in vm.IngredientsIds)
                {
                    var ingredient = _uow.Ingredients.GetById(id);
                    if (ingredient != null)
                    {
                        vm.Product.Ingredients.Add(ingredient);
                    }

                }

                if (vm.ImageVM.Attachment != null && vm.ImageVM.Attachment.ContentLength > 0)
                {
                    if (!vm.ImageVM.Attachment.ContentType.ToLower().StartsWith("image"))
                    {
                        ModelStateHelper.AddFor<ImageCreateViewModel>(ModelState, s => s.Attachment,
                            "File is not of image type");
                        return View();
                    }
                    try
                    {
                        ImageHelper.SaveAs(vm.ImageVM.Attachment,
                            vm.ImageVM.ImageUrl);
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Something went wrong!");
                        return View();
                    }
                    vm.Product.Image = new Image()
                    {
                        ImageUrl = vm.ImageVM.ImageUrl + Path.GetExtension(vm.ImageVM.Attachment.FileName)
                    };
                }
                _uow.Products.Add(vm.Product);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            vm.Categories = new SelectList(_uow.Categories.All.Select(s => new { s.CategoryId, s.CategoryName }),
                nameof(Category.CategoryId), nameof(Category.CategoryName));
            vm.Ingredients = new MultiSelectList(_uow.Ingredients.All.Select(s => new { s.IngredientId, s.IngredientName }),
                nameof(Ingredient.IngredientId), nameof(Ingredient.IngredientName));

            return View(vm);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _uow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var vm = new ProductCreateEditViewModel()
            {
                Product = product,
                ProductName = product.ProductName.Value,
                ProductDescription = product.ProductDescription.Value,
                ProductBasePrice = product.BasePrice,
                ProductCategoryId = product.CategoryId,
                Categories = new SelectList(_uow.Categories.All.Select(s => new { s.CategoryId, s.CategoryName }), nameof(Category.CategoryId), nameof(Category.CategoryName)),
                Ingredients = new MultiSelectList(_uow.Ingredients.All.Select(s => new { s.IngredientId, s.IngredientName }), nameof(Ingredient.IngredientId), nameof(Ingredient.IngredientName)),
                IngredientsIds = product.Ingredients.Select(x => x.IngredientId).ToList()
            };

            return View(vm);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Product = _uow.Products.GetById(vm.Product.ProductId);

                vm.Product.ProductName.Value = vm.ProductName;
                vm.Product.ProductDescription.Value = vm.ProductDescription;
                vm.Product.BasePrice = vm.ProductBasePrice;
                vm.Product.CategoryId = vm.ProductCategoryId;

                vm.Product.Ingredients.Clear();
                foreach (var id in vm.IngredientsIds)
                {
                    vm.Product.Ingredients.Add(_uow.Ingredients.GetById(id));
                }
                _uow.Products.Update(vm.Product);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            vm.Categories = new SelectList(_uow.Categories.All.Select(s => new { s.CategoryId, s.CategoryName }),
                nameof(Category.CategoryId), nameof(Category.CategoryName));
            vm.Ingredients =
                new MultiSelectList(_uow.Ingredients.All.Select(s => new { s.IngredientId, s.IngredientName }),
                    nameof(Ingredient.IngredientId), nameof(Ingredient.IngredientName));
            vm.IngredientsIds = vm.Product.Ingredients.Select(x => x.IngredientId).ToList();
            return View(vm);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _uow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _uow.Products.GetById(id);
            if (product.Image != null)
            {
                ImageHelper.Delete(product.Image.ImageUrl);
            }
            _uow.Products.DeleteWithImage(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
