using EmployeeManagement.Client;
using EmployeeManagement.UI.MessageServices;
using EmployeeManagement.UI.Models;
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
        public ObservableCollection<EmployeeDetail> EmployeeDetailList { get; } = new();
        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<CreateOrUpdateEmployeeViewModel, EmployeeDetail?>();
            DeleteEmployeeCommand = ReactiveCommand.Create<int>(id => DeleteEmployee(id));
            CreateOrUpdateEmployeeCommand = ReactiveCommand.Create<EmployeeDetail>(employee => CreateOrUpdateEmployee(employee));
           
            RxApp.MainThreadScheduler.Schedule(LoadEmployee);
        }       

        private async void LoadEmployee()
        {
            var employees = await EmployeeClient.Instance.GetEmployeesASync();
            foreach (var employee in employees)
            {
                string dob = employee.DateOfBirth.HasValue ? employee.DateOfBirth.Value.ToString("d") : string.Empty;
                EmployeeDetailList.Add(new EmployeeDetail()
                {
                    Name = $"{employee.FirstName} {employee.LastName}",
                    DateofBirth = dob,
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

        private async void CreateOrUpdateEmployee(EmployeeDetail employee)
        {
            var store = new CreateOrUpdateEmployeeViewModel(employee);

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
        public ReactiveCommand<EmployeeDetail, Unit> CreateOrUpdateEmployeeCommand { get; }
        public Interaction<CreateOrUpdateEmployeeViewModel, EmployeeDetail?> ShowDialog { get; }
    }
}
