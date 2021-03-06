﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class UsersManagmentController : ApiController
    {
        // GET: api/UsersManagment
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/UsersManagment/5
        public HttpResponseMessage Get([FromUri]int id)
        {
            return UsersHelper.GetToUserCsv(id);
        }

        // GET: api/UsersManagment
        public HttpResponseMessage Get(int userId, bool getAll)
        {
            return UsersHelper.GetToUserCsv(userId, getAll);
        }

        // POST: api/UsersManagment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UsersManagment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UsersManagment/5
        public void Delete(int id)
        {
        }
    }
}
