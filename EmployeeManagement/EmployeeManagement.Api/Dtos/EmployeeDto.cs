﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.Dtos
{
    public class EmployeeDto
    {        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
