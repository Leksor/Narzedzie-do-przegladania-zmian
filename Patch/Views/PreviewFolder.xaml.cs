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
    /// Interaction logic for PreviewFolder.xaml
    /// </summary>
    public partial class PreviewFolder : MetroWindow, INotifyPropertyChanged
    {
        /// <summary>
        /// Collection with loaded files from folder
        /// </summary>
        private List<FileProperties> _fileCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection"></param>
        public PreviewFolder(List<FileProperties> collection)
        {
            InitializeComponent();

            _fileCollection = collection;

            this.DataContext = this;

            // Shows window on the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Get collection with loaded files
        /// </summary>
        public List<FileProperties> FileCollection
        {
            get { return _fileCollection; }
            set
            {
                _fileCollection = value;
                NotifyPropertyChanged("FileCollection");
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
