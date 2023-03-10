using EmployeeManagement.Client;
using EmployeeManagement.UI.MessageServices;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace EmployeeManagement.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Title => "Welcome to Employee Management System!";
        public ObservableCollection<EmployeeViewModel> EmployeeDetailList { get; } = new();
        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<CreateOrUpdateEmployeeViewModel, EmployeeViewModel?>();
            DeleteEmployeeCommand = ReactiveCommand.Create<int>(id => DeleteEmployee(id));
            CreateOrUpdateEmployeeCommand = ReactiveCommand.Create<EmployeeViewModel>(employee => CreateOrUpdateEmployee(employee));
           
            RxApp.MainThreadScheduler.Schedule(LoadEmployee);
        }       

        private async void LoadEmployee()
        {
            var employees = await EmployeeClient.Instance.GetEmployeesASync();
            foreach (var employee in employees)
            {
                string dob = employee.DateOfBirth.HasValue && employee.DateOfBirth.Value!=DateTime.MinValue ? employee.DateOfBirth.Value.ToString("d") : string.Empty;
                EmployeeDetailList.Add(new EmployeeViewModel()
                {
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    DateOfBirth = dob,
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
                result = await EmployeeClient.Instance.DeleteEmployee(id);
                if (result)
                {
                    var employee = EmployeeDetailList.FirstOrDefault(x => x.Id == id);
                    if (employee != null)
                        EmployeeDetailList.Remove(employee);
                }
            }
        }

        private async void CreateOrUpdateEmployee(EmployeeViewModel employeeVm)
        {
            var store = new CreateOrUpdateEmployeeViewModel(employeeVm);

            var latestEmployeeDetail = await ShowDialog.Handle(store);
            if (latestEmployeeDetail!=null)
            {
                var itemIndex = EmployeeDetailList.IndexOf(latestEmployeeDetail);
                if (itemIndex<0)
                {
                    EmployeeDetailList.Add(latestEmployeeDetail);
                }
                else
                {
                    EmployeeDetailList[itemIndex]= latestEmployeeDetail;
                }

            }
        }        
       
        public ReactiveCommand<int, Unit> DeleteEmployeeCommand { get; }
        public ReactiveCommand<EmployeeViewModel, Unit> CreateOrUpdateEmployeeCommand { get; }
        public Interaction<CreateOrUpdateEmployeeViewModel, EmployeeViewModel?> ShowDialog { get; }
    }
}
