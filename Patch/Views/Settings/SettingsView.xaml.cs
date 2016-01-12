using Patch.ViewModels;
using System.Windows.Controls;

namespace Patch.Views.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            // set current context
            this.DataContext = new SettingsViewModel();
        }
    }
}
