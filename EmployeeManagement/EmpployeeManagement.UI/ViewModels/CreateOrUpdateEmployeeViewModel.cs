using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Client;
using EmployeeManagement.Client.RequestDto;
using EmployeeManagement.Client.Responses;
using EmployeeManagement.UI.Models;
using ReactiveUI;

namespace EmployeeManagement.UI.ViewModels
{
	public class CreateOrUpdateEmployeeViewModel : ReactiveObject
	{
        public CreateOrUpdateEmployeeViewModel(EmployeeDetail employeeDetail)
        {
            Title=employeeDetail==null ? "Create Employee" : "Update Employee";
            SaveEmployeeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                EmployeeDetail newEmployeDetail = await CreateOrUpdateEmployee(employeeDetail);
                return newEmployeDetail;
            });

            CloseCreateOrEditCommand=ReactiveCommand.CreateFromTask(async () =>
            {
                if (employeeDetail !=null)
                {
                    employeeDetail = null;
                }
                return employeeDetail;
            });
            if(employeeDetail != null)
            {
               Email=employeeDetail.Email;
                dateOfBirth=!string.IsNullOrEmpty(employeeDetail.DateofBirth) ? DateTime.Parse(employeeDetail.DateofBirth) : DateTime.Now;
                Department=employeeDetail.Department;
                var fullName = employeeDetail.Name.Split(' ');
                if(fullName.Length > 1)
                {
                    FirstName=fullName[0];
                    LastName=fullName[1];
                }
                else
                {
                    FirstName=fullName[0];
                }
            }
        }
        private async Task<EmployeeDetail> CreateOrUpdateEmployee(EmployeeDetail employeeDetail)
        {
            if (employeeDetail == null)
            {
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    FirstName=this.FirstName,
                    LastName=this.LastName,
                    Department=this.Department,
                    Email=this.Email,
                    DateOfBirth=this.DateOfBirth
                };
                var response = await EmployeeClient.Instance.CreateEmployeeAsync(employeeDto);
                if (response != null)
                {
                    employeeDetail=new EmployeeDetail()
                    {
                        Name=$"{response.FirstName} {response.LastName}",
                        Email=response.Email,
                        Id=response.Id,
                        Department =response.Department,
                        DateofBirth= response.DateOfBirth.HasValue ? response.DateOfBirth.Value.ToString("d") : string.Empty
                    };
                }
                return employeeDetail;
            }
            else
            {
                //updating change here
                var updateEmployee = new Employee()
                {
                    Id=employeeDetail.Id,
                    DateOfBirth=DateOfBirth,
                    Department=Department,
                    Email=this.Email,
                    FirstName=this.FirstName,
                    LastName=this.LastName
                };

                var response = await EmployeeClient.Instance.UpdateEmployeeAsync(employeeDetail.Id, updateEmployee);
                if (response != null)
                {
                    employeeDetail.Id = response.Id;
                    employeeDetail.Email = response.Email;
                    employeeDetail.Department = response.Department;
                    employeeDetail.Name=$"{response.FirstName} {response.LastName}";
                    employeeDetail.DateofBirth=response.DateOfBirth.HasValue? response.DateOfBirth.Value.ToString("d") : string.Empty;
                }
            }
            return employeeDetail;
        }
        private string title;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        private string firstName;
        [Required(ErrorMessage ="First Name is required")]        
        public string FirstName
        {
            get => firstName;
            set => this.RaiseAndSetIfChanged(ref firstName, value);
        }
        private string lastName;
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName
        {
            get => lastName;
            set => this.RaiseAndSetIfChanged(ref lastName, value);
        }
        private string email;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email
        {
            get => email;
            set => this.RaiseAndSetIfChanged(ref email, value);
        }
        private string department;
        public string Department
        {
            get => department;
            set => this.RaiseAndSetIfChanged(ref department, value);
        }
        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => this.RaiseAndSetIfChanged(ref dateOfBirth, value);
        }

        public ReactiveCommand<Unit, EmployeeDetail?> SaveEmployeeCommand { get; }
        public ReactiveCommand<Unit, EmployeeDetail?> CloseCreateOrEditCommand { get; }
    }
}