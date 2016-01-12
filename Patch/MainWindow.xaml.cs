using MahApps.Metro.Controls;
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

namespace Patch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            ApplicationViewModel mainContext = new ApplicationViewModel();

            // Loads all pages
            mainContext.LoadPages();

            InitializeComponent();
            this.DataContext = mainContext;

            // Shows window on the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
