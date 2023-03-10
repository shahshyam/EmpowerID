using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using EmployeeManagement.UI.ViewModels;
using ReactiveUI;
using System;

namespace EmployeeManagement.UI.Views
{
    public partial class CreateOrUpdateEmployeeWindow : ReactiveWindow<CreateOrUpdateEmployeeViewModel>
    {
        public CreateOrUpdateEmployeeWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.SaveEmployeeCommand.Subscribe(Close)));
            this.WhenActivated(d => d(ViewModel!.CloseCreateOrEditCommand.Subscribe(Close)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
