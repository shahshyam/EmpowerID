using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Client;
using EmployeeManagement.UI.MessageServices;
using EmployeeManagement.UI.Models;
using ReactiveUI;

namespace EmployeeManagement.UI.ViewModels
{
	public class EmployeeListViewModel : ReactiveObject
	{
        public ObservableCollection<EmployeeDetail> EmployeeDetails { get; } = new();
        public EmployeeListViewModel()
        {
            DeleteEmployeeCommand = ReactiveCommand.Create<int>(id => DeleteEmployee(id));
            UpdateEmployeeCommand = ReactiveCommand.Create<EmployeeDetail>(employee => UpdateEmployee(employee));
            CreateEmployeeCommand=  ReactiveCommand.Create(() => { CreateEmployee(); });
            RxApp.MainThreadScheduler.Schedule(LoadEmployee);
        }
        private async void LoadEmployee()
        {
            var employees = await EmployeeClient.Instance.GetEmployees();
            foreach (var employee in employees)
            {
                EmployeeDetails.Add(new EmployeeDetail()
                {
                    Name = $"{employee.FirstName} {employee.LastName}",
                    DateofBirth = employee.DateOfBirth,
                    Department = employee.Department,
                    Email = employee.Email,
                    Id = employee.Id
                });
            }
        }
       
        private async void DeleteEmployee(int id) 
        {
            var result = await MyMessageService.AllowToProcessActionDialog("Do you want to delete this employee");
            if (result)
            {
                result= await EmployeeClient.Instance.DeleteEmployee(id);
                if (result)
                {
                    var employee = EmployeeDetails.FirstOrDefault(x => x.Id == id);
                    if (employee != null)
                        EmployeeDetails.Remove(employee);
                }
            }
        }
        private void UpdateEmployee(EmployeeDetail employee)
        {
            CreateOrUpdateEmployee(employee);
        }
        private void CreateOrUpdateEmployee(EmployeeDetail employee)
        {

        }
        private void CreateEmployee()
        {
            CreateOrUpdateEmployee(new EmployeeDetail());
        }
        public ICommand CreateEmployeeCommand { get; }
       public ReactiveCommand<int, Unit> DeleteEmployeeCommand { get; }
       public ReactiveCommand<EmployeeDetail, Unit> UpdateEmployeeCommand { get; }        
    }
}