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

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : BaseController
    {
        private readonly IUOW _uow;

        public OrdersController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var vm = new OrderIndexViewModel()
            {
                Orders = _uow.Orders.All
            };
            return View(vm);
        }

        [HttpPost]
        [ActionName("Elevate")]
        [ValidateAntiForgeryToken]
        public ActionResult ElevateStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order o = _uow.Orders.GetById(id);
            if (o != null)
            {
                OrderStatus nextStatus = _uow.OrderStatuses.GetNextLogicalStatusFor(o.OrderStatus.LogicalOrder);
                if (nextStatus != null)
                {
                    o.OrderStatus = nextStatus;
                    switch (nextStatus.LogicalOrder)
                    {
                        case (int)OrderStatus.StatusOrder.InProgress:
                            o.Invoice.InvoicePaymentReceivedDate = DateTime.Now;
                            break;
                        case (int)OrderStatus.StatusOrder.WaitingHandOver:
                            o.OrderCompletedDate = DateTime.Now;
                            break;
                    }
                    _uow.Orders.Update(o);
                    _uow.Commit();
                }
            }
            return RedirectToAction("Index");
        }
        /*
        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.InvoiceId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "OrderStatusId", order.OrderStatusId);
            ViewBag.OrderUserId = new SelectList(db.UserInts, "Id", "Email", order.OrderUserId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderUserId,OrderStatusId,OrderPlacedDate,OrderForDate,OrderCompletedDate,ProductQuantity,InvoiceId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.InvoiceId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatus, "OrderStatusId", "OrderStatusId", order.OrderStatusId);
            ViewBag.OrderUserId = new SelectList(db.UserInts, "Id", "Email", order.OrderUserId);
            return View(order);
        }
        */
    }
}
