using Patch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for GeneratePatchFolderView.xaml
    /// </summary>
    public partial class GeneratePatchFolderView : UserControl
    {
        public GeneratePatchFolderView()
        {
            InitializeComponent();

            // Set current content to view
            this.DataContext = new GeneratePatchFolderViewModel();
        }
    }
}
