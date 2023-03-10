using AutoMapper;
using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Api.Test.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _controller;
        private List<Employee> _employees;
        public EmployeeControllerTest()
        {
            _employees= GetEmployees();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(m => m.GetAllEmployeeAsync())
                .ReturnsAsync(_employees);
            _controller = new EmployeeController(
                employeeRepositoryMock.Object, null);
        }

        [Fact]
        public async Task GetEmployees_MustReturnNumberOfEmployees()
        {
            // Arrange

            // Act
            var result = await _controller.GetEmployee();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_employees.Count(), result.Count());
        }       

        private List<Employee> GetEmployees()
        {
            return new List<Employee>() {
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
            };
        }
    }
}
