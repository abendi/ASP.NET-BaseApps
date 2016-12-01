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
using Domain.Identity;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IUOW _uow;

        public ProductsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Products/Details/5
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

        [Authorize]
        public ActionResult Order(int? id)
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
            var vm = new ProductOrderViewModel()
            {
                Product = product,
                Order = new Order()
            };
            vm.Order.ProductQuantity = 1;
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(ProductOrderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Product p = _uow.Products.GetById(vm.Product.ProductId);
                Order o = new Order()
                {
                    OrderUserId = User.Identity.GetUserId<int>(),
                    ProductQuantity = vm.Order.ProductQuantity,
                    OrderForDate = vm.Order.OrderForDate,
                    OrderStatus = _uow.OrderStatuses.GetFirstLogicalStatus(),
                    OrderPlacedDate = DateTime.Now,
                    Invoice = new Invoice()
                    {
                        InvoiceTotalSum = p.BasePrice * (decimal) vm.Order.ProductQuantity
                    }
                };
                o.Products.Add(p);
                _uow.Orders.Add(o);
                _uow.Commit();
                return RedirectToAction("OrderSubmitted", new { id = o.OrderId });
            }
            Product product = _uow.Products.GetById(vm.Product.ProductId);
            vm.Product = product;
            return View(vm);
        }

        [Authorize]
        public ActionResult OrderSubmitted(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order o = _uow.Orders.GetOrderForUser(id.Value, User.Identity.GetUserId<int>());
            if (o == null)
            {
                return View("Error");
            }
            return View("OrderSubmitted", id.Value);
        }
    }
}
