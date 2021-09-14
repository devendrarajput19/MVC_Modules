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


        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee empobj)
        {
            string json;
            EmployeeEnterpriseEntities db = new EmployeeEnterpriseEntities();
            db.Employees.Add(empobj);
            db.SaveChanges();
           
             json = JsonConvert.SerializeObject(empobj);
           // json = JsonConvert.SerializeObject(new { Employee = result });
            var response = this.Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri + empobj.EmployeeID.ToString());
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        [Route("Edit")]
        [HttpPut]
        public IHttpActionResult EditEmployee(Employee empobj)
        {
            EmployeeEnterpriseEntities db = new EmployeeEnterpriseEntities();

            if(!ModelState.IsValid)
            {
                return BadRequest("Not a Valida Model");
            }

            var check_emp = db.Employees.Where(x => x.EmployeeID == empobj.EmployeeID).FirstOrDefault();

            if(check_emp != null)
            {
                check_emp.FullName = empobj.FullName;
                check_emp.Email = empobj.Email;
                check_emp.Address = empobj.Address;
                check_emp.JoiningDate = empobj.JoiningDate;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok(check_emp);
        }

    }
}
