using MahApps.Metro.Controls;
using Patch.Models;
using Patch.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Patch.Views
{
    /// <summary>
    /// Interaction logic for PreviewTreeFolder.xaml
    /// </summary>
    public partial class PreviewTreeFolder : MetroWindow, INotifyPropertyChanged
    {
        /// <summary>
        /// Collection with loaded files from folder
        /// </summary>
        private List<FileProperties> _treeCollection;

        public PreviewTreeFolder(List<FileProperties> treeRoot)
        {
            InitializeComponent();

            _treeCollection = treeRoot;

            this.DataContext = this;

            // Shows window on the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public List<FileProperties> TreeCollection
        {
            get { return _treeCollection; }
            set
            {
                _treeCollection = value;
                NotifyPropertyChanged("TreeCollection");
            }
        }

        // Events definition
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
