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
using Microsoft.AspNet.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IUOW _uow;

        public OrdersController(IUOW uow)
        {
            _uow = uow;
        }

        [ActionName("MyOrders")]
        public ActionResult Orders()
        {
            var vm = new OrderIndexViewModel()
            {
                Orders = _uow.Orders.AllForUser(User.Identity.GetUserId<int>())
            };
            return View("MyOrders", vm);
        }

        public ActionResult Invoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _uow.Invoices.GetInvoiceForUser(id.Value, User.Identity.GetUserId<int>());
            if (invoice == null) //User trying to access someone's else invoice OR invoice is missing
            {
                return View("Error");
            }
            var vm = new InvoiceDetailViewModel()
            {
                Invoice = invoice,
                Order = _uow.Orders.GetOrderByInvoiceId(invoice.InvoiceId)
            };
            return View("~/Views/Orders/Details.cshtml", vm);
        }
    }

}
