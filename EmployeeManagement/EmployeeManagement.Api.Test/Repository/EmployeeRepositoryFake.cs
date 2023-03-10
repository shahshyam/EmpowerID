using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Test.Repository
{
    internal class EmployeeRepositoryFake : IEmployeeRepository
    {
        private  List<Employee> _employees;
        public EmployeeRepositoryFake() {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                     Id = 1,
                     FirstName = "Lexicon",
                     LastName = "Lenda",
                      DateOfBirth = DateTime.Now,
                      Department="Engineering",
                      Email="enigneering@emailservice.com"
                },
                new Employee()
                {
                     Id = 1,
                     FirstName = "Swallon",
                     LastName = "Rose",
                      DateOfBirth = DateTime.Now,
                      Department="Marketing",
                      Email="marketing@emailservice.com"
                },
                new Employee()
                {
                     Id = 1,
                     FirstName = "Harris",
                     LastName = "Metro",
                      DateOfBirth = DateTime.Now,
                      Department="Engineering",
                      Email="engineering@emailservice.com"
                }
                ,new Employee()
                {
                     Id = 1,
                     FirstName = "Michel",
                     LastName = "Crawn",
                      DateOfBirth = DateTime.Now,
                      Department="IT",
                      Email="It@emailservice.com"
                },new Employee()
                {
                     Id = 1,
                     FirstName = "George",
                     LastName = "Temp",
                      DateOfBirth = DateTime.Now,
                      Department="Support",
                      Email="Support@emailservice.com"
                }
            };
        }
        public Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployeeAsync(int Id)
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployeeAsync(int Id, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
