using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EmployeeManagement.UI.Views
{
    public partial class EmployeeListView : UserControl
    {
        public EmployeeListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
