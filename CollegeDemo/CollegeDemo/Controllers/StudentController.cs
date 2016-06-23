using CollegeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace CollegeDemo.Controllers
{
    [RoutePrefix("api/GoodStudent")]
    public class StudentController : ApiController
    {

        // Get api/GoodStudent/female
        // Get api/GoodStudent/male
        [Route("{gender}")]
        public HttpResponseMessage GetSameGenderStudents(string gender)
        {
            IEnumerable<Student> students = StudentRepository.GetInstance().GetAll();

            var result = students.Where(x => { return x.Gender.Equals(gender, StringComparison.InvariantCultureIgnoreCase); });
            
            var response = Request.CreateResponse<IEnumerable<Student>>(HttpStatusCode.OK, result);
            return response;
        }


        // Get api/GoodStudent/2/history
        [Route("{id:int}/history")]
        public HttpResponseMessage GetStudentHistory(int id)
        {
            HttpResponseMessage response = null;
            Student student = StudentRepository.GetInstance().Get(id);
            if (student == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("student {0} not found", id));
            }
            else
            {
                response = Request.CreateResponse<String>(HttpStatusCode.OK, student.FamilyHistory);
            }
            return response;
        }


        // Get api/GoodStudent/2, to get student 2
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = null;
            Student student = StudentRepository.GetInstance().Get(id);
            if (student == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("student {0} not found", id));
            }
            else
            {
                response = Request.CreateResponse<Student>(HttpStatusCode.OK, student);
            }
            return response;
        }

        // Get api/GoodStudent/,  to get all students
        [Route("")]
        public HttpResponseMessage Get()
        {
            IEnumerable<Student> students = StudentRepository.GetInstance().GetAll();
            var response = Request.CreateResponse<IEnumerable<Student>>(HttpStatusCode.OK, students);
            return response;
        }





        // DELETE api/GoodStudent/5, to delete student 5
        
        [Route("{id:int}")]
        public void Delete(int id)
        {
            StudentRepository.GetInstance().Delete(id);
        }


        // PUT api/GoodStudent/3, to modify student 3
        [Route("{id:int}")]
        public void Put(int id, [FromBody]Student s)
        {
            StudentRepository.GetInstance().Update(id, s);
        }



        // POST api/GoodStudent, to create a new student
        [Route("")]
        public HttpResponseMessage Post([FromBody]Student s)
        {
            var result = StudentRepository.GetInstance().Add(s);
            if (result != null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "Student", id = s.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "cannot create a new course");
            }

        }







    }
}
