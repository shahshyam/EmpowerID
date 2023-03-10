using Avalonia.Controls;
using Avalonia.ReactiveUI;
using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<CreateOrUpdateEmployeeViewModel, EmployeeDetail?> interaction)
        {
            var dialog = new CreateOrUpdateEmployeeWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<EmployeeDetail?>(this);
            interaction.SetOutput(result);
        }
    }
}
