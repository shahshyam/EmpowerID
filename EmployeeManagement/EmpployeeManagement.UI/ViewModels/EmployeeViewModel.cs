using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace EmployeeManagement.UI.ViewModels
{
	public class EmployeeViewModel : ReactiveObject
	{
        public int Id { get; set; }

        private string fullName;        
        public string FullName
        {
            get => fullName;
            set => this.RaiseAndSetIfChanged(ref fullName, value);
        }
        private string email;
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
        private string dateOfBirth;
        public string DateOfBirth
        {
            get => dateOfBirth;
            set => this.RaiseAndSetIfChanged(ref dateOfBirth, value);
        }
       
    }
}