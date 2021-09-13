using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WEBAPI_Project.Models;

namespace WEBAPI_Project.Controllers
{

    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [Route("list")]
        [HttpGet]
        public IHttpActionResult get()
        {
            string json;
            EmployeeEnterpriseEntities db = new EmployeeEnterpriseEntities();
            var result = db.Employees.ToList();
            // json = JsonConvert.SerializeObject(result);
            json = JsonConvert.SerializeObject(new { Employee = result});
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

         
    }
}
