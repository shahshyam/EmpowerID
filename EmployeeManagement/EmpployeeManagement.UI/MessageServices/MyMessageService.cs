using Avalonia.Styling;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.MessageServices
{
    internal class MyMessageService
    {
        public static async Task<bool> AllowToProcessActionDialog(string message)
        {
            var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.YesNo,
                ContentTitle = "Delete",
                ContentMessage = message,
                Icon = Icon.Plus,
                WindowStartupLocation=Avalonia.Controls.WindowStartupLocation.CenterOwner
            });
            var result = await msBoxStandardWindow.Show();
            if (result==ButtonResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
