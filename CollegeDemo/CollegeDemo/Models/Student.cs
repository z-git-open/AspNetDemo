using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeDemo.Models
{
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }

        public string FamilyHistory { get; set; }

        public string Gender { get; set; }
    }



    public class StudentRepository
    {
        private static StudentRepository _instance;

        private StudentRepository()
        {
            students = new List<Student>();
            Add(new Student() { Name = "Mary", Age = 19, FamilyHistory = "Mary's family history" , Gender = "female", });
            Add(new Student() { Name = "John", Age = 22, FamilyHistory = "John's family history", Gender = "male", });
            Add(new Student() { Name = "ALvin", Age = 3, FamilyHistory = "Alvin's family history", Gender = "male", });
            Add(new Student() { Name = "Nathan", Age = 5, FamilyHistory = "Nathan's family history", Gender = "male", });
        }

        public static StudentRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StudentRepository();
            }
            return _instance;
        }

        private List<Student> students;
        public Student Add(Student c)
        {
            if (c != null)
            {

                c.Id = students.Count + 1;
                students.Add(c);
                return c;
            }
            else
                return null;
        }



        public void Update(int id, Student c)
        {
            students.ForEach(x =>
            {
                if (x.Id == id)
                {
                    x.Name = c.Name;
                    x.Age = c.Age;
                    x.FamilyHistory = c.FamilyHistory;
                    x.Gender = c.Gender;
                }
            });
        }

        public Student Get(int id)
        {
            foreach (var c in students)
            {
                if (id == c.Id)
                    return c;
            }
            return null;
        }


        public void Delete(Student c)
        {
            students.Remove(c);
        }

        public void Delete(int id)
        {
            foreach (var c in students)
            {
                if (c.Id == id)
                {
                    students.Remove(c);
                    break;
                }
            }
        }
        public IEnumerable<Student> GetAll() { return students; }
    }
}