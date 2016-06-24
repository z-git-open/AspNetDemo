using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeDemo.Models
{
    public class Employee
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }

        public string SSN { get; set; }

        public int Salary { get; set; }

        public string Gender { get; set; }

        public ICollection<LinkModel> Links { get; set; }
    }



    public class EmployeeRepository
    {
        private static EmployeeRepository _instance;

        private EmployeeRepository()
        {
            employees = new List<Employee>();
            Add(new Employee() { Name = "Prof. Andy", Age = 45, SSN = "123-22-7894", Salary = 100000, Gender = "male", });
            Add(new Employee() { Name = "Prof. Mike", Age = 60, SSN = "123-66-5489", Salary = 170000, Gender = "male", });
            Add(new Employee() { Name = "Prof. Alice", Age = 55, SSN = "458-78-3215", Salary = 230000, Gender = "female", });
            Add(new Employee() { Name = "Prof. Li", Age = 35, SSN = "749-32-1346", Salary = 600000, Gender = "male", });
        }

        public static EmployeeRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeRepository();
            }
            return _instance;
        }

        private List<Employee> employees;
        public Employee Add(Employee c)
        {
            if (c != null)
            {

                c.Id = employees.Count + 1;
                employees.Add(c);
                return c;
            }
            else
                return null;
        }



        public void Update(int id, Employee c)
        {
            employees.ForEach(x =>
            {
                if (x.Id == id)
                {
                    x.Name = c.Name;
                    x.Age = c.Age;
                    x.Salary = c.Salary;
                    x.SSN = c.SSN;
                    x.Gender = c.Gender;
                }
            });
        }

        public Employee Get(int id)
        {
            foreach (var c in employees)
            {
                if (id == c.Id)
                    return c;
            }
            return null;
        }


        public void Delete(Employee c)
        {
            employees.Remove(c);
        }

        public void Delete(int id)
        {
            foreach (var c in employees)
            {
                if (c.Id == id)
                {
                    employees.Remove(c);
                    break;
                }
            }
        }
        public IEnumerable<Employee> GetAll() { return employees; }
    }
}