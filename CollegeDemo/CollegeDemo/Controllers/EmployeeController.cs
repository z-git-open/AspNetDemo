using CollegeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeDemo.Controllers
{
    [CollegeDemo.Filter.Authorize]
    [RoutePrefix("api/MyEmployee")]
    public class EmployeeController : ApiController
    {

        // Get api/MyEmployee/,  to get all students
        [Route("")]
        public HttpResponseMessage Get()
        {
            IEnumerable<Employee> employees = EmployeeRepository.GetInstance().GetAll();
            var response = Request.CreateResponse<IEnumerable<Employee>>(HttpStatusCode.OK, employees);
            return response;
        }


        // Get api/MyEmployee/2, to get employee 2
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = null;
            Employee employee = EmployeeRepository.GetInstance().Get(id);
            if (employee == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("employee {0} not found", id));
            }
            else
            {
                response = Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
            }
            return response;
        }


        // Get api/MyEmployee/2/salary
        [Route("{id:int}/salary")]
        public HttpResponseMessage GetEmployeeSalary(int id)
        {
            HttpResponseMessage response = null;
            Employee employee = EmployeeRepository.GetInstance().Get(id);
            if (employee == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Employee {0} not found", id));
            }
            else
            {
                response = Request.CreateResponse<int>(HttpStatusCode.OK, employee.Salary);
            }
            return response;
        }


        // DELETE api/MyEmployee/5, to delete employee 5
        [Route("{id:int}")]
        public void Delete(int id)
        {
            EmployeeRepository.GetInstance().Delete(id);
        }


        // PUT api/MyEmployee/3, to modify employee 3
        [Route("{id:int}")]
        public void Put(int id, [FromBody]Employee e)
        {
            EmployeeRepository.GetInstance().Update(id, e);
        }



        // POST api/MyEmployee, to create a new Employee
        [Route("")]
        public HttpResponseMessage Post([FromBody]Employee newone)
        {
            var result = EmployeeRepository.GetInstance().Add(newone);
            if (result != null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "Employee", id = result.Id }));
                result.Links = new List<LinkModel>()
                {
                    CreateLink(new Uri(Url.Link("DefaultApi", new { controller = "MyEmployee", id = result.Id })).ToString(), "self"),
                    CreateLink(new Uri(Url.Link("DefaultApi", new { controller = "MyEmployee", id = result.Id })).ToString()+"/Salary", "salary"),
                };
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "cannot create a new employee");
            }
        }

        private LinkModel CreateLink(string href, string rel, string method = "GET", bool istemplte = false)
        {
            return new LinkModel()
            {
                Href = href,
                Rel = rel,
                Method = method, 
                IsTemplated = istemplte,
            };
        }



    }
}
