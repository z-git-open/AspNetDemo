using CollegeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeDemo.Controllers
{
    public class CourseController : ApiController
    {
        
        // GET api/Course/
        public HttpResponseMessage Get()
        {
            IEnumerable<Course> courses = CourseRepository.GetInstance().GetAll();
            var response = Request.CreateResponse<IEnumerable<Course>>(HttpStatusCode.OK, courses);
            return response;
        }

        // GET api/Course/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = null;
            Course course = CourseRepository.GetInstance().Get(id);
            if(course == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Course {0} not found", id));
            }
            else
            {
                response = Request.CreateResponse<Course>(HttpStatusCode.OK, course);
            }
            return response;
        }

        // POST api/Course
        public HttpResponseMessage Post([FromBody]Course c)
        {
            var result = CourseRepository.GetInstance().Add(c);
            if (result != null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri + result.Id.ToString());
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "cannot create a new course");
            }

        }

        // PUT api/Course/5
        public void Put(int id, [FromBody]Course c)
        {
            CourseRepository.GetInstance().Update(id, c);
        }

        // DELETE api/Course/5
        public void Delete(int id)
        {
            CourseRepository.GetInstance().Delete(id);
        }
    }
}
