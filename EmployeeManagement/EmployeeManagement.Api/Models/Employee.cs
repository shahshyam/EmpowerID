using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }   
        
        public DateTime DateOfBirth { get; set; }    
        public string Department { get; set; }      
    }
}
