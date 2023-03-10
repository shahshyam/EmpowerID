using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Threading.Tasks;
using EmployeeManagement.Client;
using EmployeeManagement.Client.RequestDto;
using EmployeeManagement.Client.Responses;
using ReactiveUI;

namespace EmployeeManagement.UI.ViewModels
{
    public class CreateOrUpdateEmployeeViewModel : ReactiveObject
	{
        public CreateOrUpdateEmployeeViewModel(EmployeeViewModel employeeVm)
        {
            Title= employeeVm == null ? "Create Employee" : "Update Employee";
            SaveEmployeeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                EmployeeViewModel newEmployeVM = await CreateOrUpdateEmployee(employeeVm);
                return newEmployeVM;
            });

            CloseCreateOrEditCommand=ReactiveCommand.CreateFromTask(async () =>
            {
                if (employeeVm !=null)
                {
                    employeeVm = null;
                }
                return employeeVm;
            });
            if(employeeVm != null)
            {
               Email=employeeVm.Email;
                dateOfBirth=!string.IsNullOrEmpty(employeeVm.DateOfBirth) ? DateTime.Parse(employeeVm.DateOfBirth) : DateTime.Now;
                Department=employeeVm.Department;
                var fullName = employeeVm.FullName.Split(' ');
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
        private async Task<EmployeeViewModel> CreateOrUpdateEmployee(EmployeeViewModel employeVm)
        {
            if (employeVm == null)
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
                    employeVm =new EmployeeViewModel()
                    {
                        FullName=$"{response.FirstName} {response.LastName}",
                        Email=response.Email,
                        Id=response.Id,
                        Department =response.Department,
                        DateOfBirth= response.DateOfBirth.HasValue && response.DateOfBirth.Value!=DateTime.MinValue ? response.DateOfBirth.Value.ToString("d") : string.Empty
                    };
                }
                return employeVm;
            }
            else
            {
                //updating change here
                var updateEmployee = new Employee()
                {
                    Id=employeVm.Id,
                    DateOfBirth=DateOfBirth,
                    Department=Department,
                    Email=this.Email,
                    FirstName=this.FirstName,
                    LastName=this.LastName
                };

                var response = await EmployeeClient.Instance.UpdateEmployeeAsync(employeVm.Id, updateEmployee);
                if (response != null)
                {
                    employeVm.Id = response.Id;
                    employeVm.Email = response.Email;
                    employeVm.Department = response.Department;
                    employeVm.FullName=$"{response.FirstName} {response.LastName}";
                    employeVm.DateOfBirth=response.DateOfBirth.HasValue? response.DateOfBirth.Value.ToString("d") : string.Empty;
                }
            }
            return employeVm;
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

        public ReactiveCommand<Unit, EmployeeViewModel?> SaveEmployeeCommand { get; }
        public ReactiveCommand<Unit, EmployeeViewModel?> CloseCreateOrEditCommand { get; }
    }
}