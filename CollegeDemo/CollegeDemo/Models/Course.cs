using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeDemo.Models
{
    public class Course
    {
        public string Title { get; set; }

        public int Room { get; set; }

        public int Id { get; set; }
    }



    public class CourseRepository
    {
        private static CourseRepository _instance;

        private CourseRepository()
        {
            courses = new List<Course>();
            Add(new Course() { Title = "Math", Room = 111 });
            Add(new Course() { Title = "Phy", Room = 222 });
        }

        public static CourseRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new CourseRepository();
            }
            return _instance;
        }

        private List<Course> courses;
        public Course Add(Course c)
        {
            if (c != null)
            {
                if (c.Title.Equals("bad"))
                {
                    return null;
                }
                else
                {
                    c.Id = courses.Count + 1;
                    courses.Add(c);
                    return c;
                }
            }
            else
                return null;
        }

        

        public void Update(int id, Course c)
        {
            courses.ForEach(x =>
            {
                if (x.Id == id)
                {
                    x.Title = c.Title;
                    x.Room = c.Room;
                }
            });
        }

        public Course Get(int id)
        {
            foreach (var c in courses)
            {
                if (id == c.Id)
                    return c;
            }
            return null;
        }

        
        public void Delete(Course c)
        {
            courses.Remove(c);
        }

        public void Delete(int id)
        {
            foreach(var c in courses)
            {
                if (c.Id == id)
                {
                    courses.Remove(c);
                    break;
                }
            }
        }
        public IEnumerable<Course> GetAll() { return courses; }
    }
}