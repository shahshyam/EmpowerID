using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EmployeeManagement.UI.Views
{
    public partial class CreateOrUpdateEmployeeView : UserControl
    {
        public CreateOrUpdateEmployeeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
