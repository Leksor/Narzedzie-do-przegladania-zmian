using MahApps.Metro.Controls;
using Patch.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Patch.Models
{
    public class FileProperties : ObservableObject, INotifyPropertyChanged
    {
        // File properties
        private string _name;
        private string _extension;
        private string _size;
        private string _createDate;
        private string _directory;
        private string _filePath;
        private string _absolutePath;
        private bool _isFile = true;
        private bool _isChanged = false;

        // File data
        public List<string> fileData;
        public List<string> updatedFile;
        private List<string> patch;

        // Children collection
        public List<FileProperties> children;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="folderName"></param>
        public FileProperties(string folderName)
        {
            _name = folderName;
            _isFile = false;
            _absolutePath = string.Empty;
            children = new List<FileProperties>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file"></param>
        public FileProperties(FileInfo file, string absolutePath)
        {
            _name = file.Name;
            _extension = file.Extension;
            _size = file.Length.ToString() + " bajtów";
            _createDate = file.CreationTime.ToShortDateString() + " " + file.CreationTime.ToShortTimeString();
            _directory = file.DirectoryName;
            _filePath = file.FullName;
            _absolutePath = _filePath.Remove(0, absolutePath.Length);

            fileData = new List<string>();
            children = new List<FileProperties>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file"></param>
        public FileProperties(FileInfo file)
        {
            _name = file.Name;
            _extension = file.Extension;
            _size = file.Length.ToString() + " bajtów";
            _createDate = file.CreationTime.ToShortDateString() + " " + file.CreationTime.ToShortTimeString();
            _directory = file.DirectoryName;
            _filePath = file.FullName;

            fileData = new List<string>();
            children = new List<FileProperties>();
        }

        #region Commands

        private ICommand _showFileCommand;
        private ICommand _showFileChanges;

        /// <summary>
        /// Shows file
        /// </summary>
        public ICommand ShowFileCommand
        {
            get
            {
                if (_showFileCommand == null)
                {
                    _showFileCommand = new RelayCommand(p => ShowFile());
                }

                return _showFileCommand;
            }
        }

        /// <summary>
        /// Shows changes between files
        /// </summary>
        public ICommand ShowFileChanges
        {
            get
            {
                if (_showFileChanges == null)
                {
                    _showFileChanges = new RelayCommand(p => ShowChanges());
                }

                return _showFileChanges;
            }
        }

        #endregion // Commands

        #region Fields

        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                NotifyPropertyChanged("IsChanged");
            }
        }

        public bool IsFile
        {
            get { return _isFile; }
            set
            {
                _isFile = value;
                NotifyPropertyChanged("IsFile");
            }
        }

        public List<FileProperties> TreeCollection
        {
            get { return children; }
            set
            {
                children = value;
                NotifyPropertyChanged("TreeCollection");
            }
        }

        /// <summary>
        /// File name field
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        /// <summary>
        /// File extension field
        /// </summary>
        public string Extension
        {
            get { return _extension; }
        }

        /// <summary>
        /// File size field
        /// </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Size");
            }
        }

        /// <summary>
        /// File created date
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
        }

        /// <summary>
        /// File directory
        /// </summary>
        public string Directory
        {
            get { return _directory; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Directory");
            }
        }

        /// <summary>
        /// File path
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _name = value;
                NotifyPropertyChanged("FilePath");
            }
        }

        /// <summary>
        /// File absolute path
        /// </summary>
        public string AbsolutePath
        {
            get { return _absolutePath; }
            set
            {
                _absolutePath = value;
            }
        }

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Add updated file
        /// </summary>
        /// <param name="updatedFile"></param>
        public void AddUpdatedFile(List<string> updatedFile, List<string> patch)
        {
            this.updatedFile = new List<string>(updatedFile);
            this.patch = new List<string>(patch);
            IsChanged = true;
        }

        /// <summary>
        /// Show file with its content
        /// </summary>
        private void ShowFile()
        {
            if (!IsWindowOpen<MetroWindow>(Name.Substring(0, Name.IndexOf('.'))))
            {
                PreviewFile previewFileWindow = new PreviewFile(fileData, "Podgląd pliku " + Name);
                previewFileWindow.Name = Name.Substring(0, Name.IndexOf('.')).Replace(' ', '_');
                previewFileWindow.Show();
            }
        }

        /// <summary>
        /// Shows changes between files
        /// </summary>
        private void ShowChanges()
        {
            RenderNewWindow(updatedFile, fileData, patch, _name, _name + " - old version", "PreviewFiles");
        }

        /// <summary>
        /// Method renders new window with passed two files data, window's title and name
        /// </summary>
        /// <param name="firstFileData"></param>
        /// <param name="secondFileData"></param>
        /// <param name="windowTitle"></param>
        /// <param name="windowName"></param>
        private void RenderNewWindow(List<string> firstFileData, List<string> secondFileData, List<string> patch, string firstWindowTitle, string secondWindowTitle, string windowName)
        {
            if (!IsWindowOpen<MetroWindow>(windowName))
            {
                PreviewFiles previewFilesWindow = new PreviewFiles(firstFileData, secondFileData, patch, firstWindowTitle, secondWindowTitle, windowName);
                previewFilesWindow.Name = windowName;
                previewFilesWindow.Show();
            }
        }

        /// <summary>
        /// Method checks if window is actually opened
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        #endregion // Methods

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
