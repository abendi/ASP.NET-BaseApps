using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using DAL;
using Domain;
using Interfaces.UOW;

namespace WebApi.Server.Controllers.Api
{
    public class ArticleController : ApiController
    {
        private readonly IUOW _uow;

        public ArticleController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Article
        public IHttpActionResult Index(string articleName)
        {
            var article = _uow.Articles.FindArticleByName(articleName);
            return Ok(article);
        }
    }
}