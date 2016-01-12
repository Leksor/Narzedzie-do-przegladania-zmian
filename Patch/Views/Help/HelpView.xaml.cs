using Patch.ViewModels;
using System.Windows.Controls;

namespace Patch.Views.Help
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            // Create VM object
            HelpViewModel helpVM = new HelpViewModel(this);

            InitializeComponent();

            // Sets current data context
            this.DataContext = helpVM;
        }
    }
}
