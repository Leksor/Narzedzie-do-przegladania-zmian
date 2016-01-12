using MahApps.Metro.Controls;
using Patch.Models;
using Patch.Models.Managers;
using Patch.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Patch.ViewModels
{
    class RollbackChangesViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        // Fields
        private bool _isFileLoaded = false;
        private bool _isPatchLoaded = false;
        private bool _isAnimationActive = false;
        private bool _isDownPanelAvaiable = false;
        private bool _isLayPatchButtonEnable = true;
        private string _layPatchStatus = string.Empty;
        private FileProperties _fileInfo;
        private FileProperties _patchInfo;

        // Update file
        private List<string> _updatedFile;

        // Manager objects
        private FileManager _fileManager;
        private PatchManager _patchManager;

        // List with data from comparered files
        public List<string> FileData;
        public List<string> PatchData;

        /// <summary>
        /// Constructor
        /// </summary>
        public RollbackChangesViewModel()
        {
            _fileManager = new FileManager();
            FileData = new List<string>();
            PatchData = new List<string>();
        }

        #region Commands

        // File commands
        private ICommand _loadFileCommand;
        private ICommand _resetFileCommand;
        private ICommand _previewFileCommand;

        // Patch commands
        private ICommand _loadPatchCommand;
        private ICommand _resetPatchCommand;
        private ICommand _previewPatchCommand;
        private ICommand _rollbackChangesCommand;

        private ICommand _previewGeneratedFileCommand;

        /// <summary>
        /// Downloads teams data from website
        /// </summary>
        public ICommand RollbackChangesCommand
        {
            get
            {
                if (_rollbackChangesCommand == null)
                {
                    _rollbackChangesCommand = new RelayCommand(p => RollbackChanges());
                }

                return _rollbackChangesCommand;
            }
        }

        /// <summary>
        /// Command invoke method which preview updated file
        /// </summary>
        public ICommand PreviewGeneratedFileCommand
        {
            get
            {
                if (_previewGeneratedFileCommand == null)
                {
                    _previewGeneratedFileCommand = new RelayCommand(p => PreviewChanges());
                }

                return _previewGeneratedFileCommand;
            }
        }

        #region FileCommands

        /// <summary>
        /// Loads file
        /// </summary>
        public ICommand LoadFileCommand
        {
            get
            {
                if (_loadFileCommand == null)
                {
                    _loadFileCommand = new RelayCommand(p => LoadFile());
                }

                return _loadFileCommand;
            }
        }

        /// <summary>
        /// Resets file
        /// </summary>
        public ICommand ResetFileCommand
        {
            get
            {
                if (_resetFileCommand == null)
                {
                    _resetFileCommand = new RelayCommand(p => ResetFile());
                }

                return _resetFileCommand;
            }
        }

        /// <summary>
        /// Loads file
        /// </summary>
        public ICommand PreviewFileCommand
        {
            get
            {
                if (_previewFileCommand == null)
                {
                    _previewFileCommand = new RelayCommand(p => PreviewFile());
                }

                return _previewFileCommand;
            }
        }

        #endregion // File Commands

        #region PatchCommands

        /// <summary>
        /// Loads file
        /// </summary>
        public ICommand LoadPatchCommand
        {
            get
            {
                if (_loadPatchCommand == null)
                {
                    _loadPatchCommand = new RelayCommand(p => LoadPatch());
                }

                return _loadPatchCommand;
            }
        }

        /// <summary>
        /// Resets file
        /// </summary>
        public ICommand ResetPatchCommand
        {
            get
            {
                if (_resetPatchCommand == null)
                {
                    _resetPatchCommand = new RelayCommand(p => ResetPatch());
                }

                return _resetPatchCommand;
            }
        }

        /// <summary>
        /// Previews patch
        /// </summary>
        public ICommand PreviewPatchCommand
        {
            get
            {
                if (_previewPatchCommand == null)
                {
                    _previewPatchCommand = new RelayCommand(p => PreviewPatch());
                }

                return _previewPatchCommand;
            }
        }

        #endregion // Patch Commands

        #endregion // Commands

        #region Fields

        /// <summary>
        /// Keeps file status
        /// </summary>
        public bool IsFileLoaded
        {
            get { return _isFileLoaded; }
            set
            {
                _isFileLoaded = value;
                NotifyPropertyChanged("IsFileLoaded");
                NotifyPropertyChanged("IsFileLoadedNegation");
                NotifyPropertyChanged("IsLayPatchButtonVisible");
            }
        }

        /// <summary>
        /// Keeps patch status
        /// </summary>
        public bool IsPatchLoaded
        {
            get { return _isPatchLoaded; }
            set
            {
                _isPatchLoaded = value;
                NotifyPropertyChanged("IsPatchLoaded");
                NotifyPropertyChanged("IsPatchLoadedNegation");
                NotifyPropertyChanged("IsLayPatchButtonVisible");
            }
        }

        /// <summary>
        /// File status negation
        /// </summary>
        public bool IsFileLoadedNegation
        {
            get { return !_isFileLoaded; }
        }

        /// <summary>
        /// Patch status negation
        /// </summary>
        public bool IsPatchLoadedNegation
        {
            get { return !_isPatchLoaded; }
        }

        /// <summary>
        /// Get status about down panel
        /// </summary>
        public bool IsDownPanelAvaiable
        {
            get { return _isDownPanelAvaiable; }
            set
            {
                _isDownPanelAvaiable = value;
                NotifyPropertyChanged("IsDownPanelAvaiable");
            }
        }

        /// <summary>
        /// Get animation status
        /// </summary>
        public bool IsAnimationActive
        {
            get { return _isAnimationActive; }
            set
            {
                _isAnimationActive = value;
                NotifyPropertyChanged("IsAnimationActive");
            }
        }

        /// <summary>
        /// Get patch generate status
        /// </summary>
        public string LayPatchStatus
        {
            get { return _layPatchStatus; }
            set
            {
                _layPatchStatus = value;
                NotifyPropertyChanged("LayPatchStatus");
            }
        }

        /// <summary>
        /// Patch generate button visible status
        /// </summary>
        public bool IsLayPatchButtonVisible
        {
            get { return _isFileLoaded && _isPatchLoaded; }
        }

        /// <summary>
        /// Patch generate button enable status
        /// </summary>
        public bool IsLayPatchButtonEnable
        {
            get { return _isLayPatchButtonEnable; }
            set
            {
                _isLayPatchButtonEnable = value;
                NotifyPropertyChanged("IsLayPatchButtonEnable");
            }
        }

        /// <summary>
        /// Object with loaded file information
        /// </summary>
        public FileProperties FileInfo
        {
            get
            {
                if (_fileInfo != null) return _fileInfo;
                return null;
            }
            set
            {
                _fileInfo = value;
                NotifyPropertyChanged("FileInfo");
            }
        }

        /// <summary>
        /// Object with loaded patch information
        /// </summary>
        public FileProperties PatchInfo
        {
            get
            {
                if (_patchInfo != null) return _patchInfo;
                return null;
            }
            set
            {
                _patchInfo = value;
                NotifyPropertyChanged("PatchInfo");
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return "Cofnij zmiany w pliku";
            }
        }

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Method loads data and patch from files and lays patch on choosen file
        /// </summary>
        private void RollbackChanges()
        {
            // Set start properties
            LayPatchStatus = "Trwa pobieranie danych...";  // set generation patch status
            IsLayPatchButtonEnable = false;               // we set generate button on unactive
            IsAnimationActive = true;                            // we enable animation

            if (FileData.Count != 0 && PatchData.Count != 0)
            {
                // Create PatchManager object
                _patchManager = new PatchManager(FileData, PatchData);

                LayPatchStatus = "Trwa cofanie zmian...";  // set generation patch status

                // Create object responsible for background work
                BackgroundWorker bw = new BackgroundWorker();

                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    // Use PatchManager to generate patch
                    _updatedFile = _patchManager.RollbackChangesFromFile(FileData, PatchData);

                    _fileManager.SaveUpdatedFile(_updatedFile, FileInfo.FilePath);

                    Thread.Sleep(300);
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    // Restore defaults properties
                    IsLayPatchButtonEnable = true;
                    IsAnimationActive = false;
                    LayPatchStatus = "Cofanie zmian zakończone.";

                    MessageBox.Show("Cofanie zmian zakończone sukcesem !");

                    IsDownPanelAvaiable = true;
                });

                bw.RunWorkerAsync();
            }
            else
            {
                // Restore defaults properties
                IsLayPatchButtonEnable = true;
                IsAnimationActive = false;

                MessageBox.Show("Jeden z wybranych plików jest pusty. Cofnięcie zmian jest możliwe.", "Błąd podczas cofania zmian", MessageBoxButton.OK);

                LayPatchStatus = string.Empty;
            }
        }

        /// <summary>
        /// Methods renders patch preview window
        /// </summary>
        private void PreviewUpdatedFile()
        {
            RenderNewWindow(_updatedFile, "Podgląd pliku z nałożoną łatką", "PreviewUpdatedFile");
        }

        /// <summary>
        /// Method renders files preview window
        /// </summary>
        private void PreviewChanges()
        {
            RenderNewWindow(_updatedFile, FileData, PatchData, "Zmiany w pliku " + FileInfo.Name, "Zmiany w pliku " + "Plik z nałożoną łatką", "PreviewFiles");
        }

        /// <summary>
        /// Method saves patch to choosen location
        /// </summary>
        private void SaveUpdatedFile()
        {
            if (_fileManager.SaveUpdatedFile(_updatedFile, FileInfo.FilePath))
            {
                MessageBox.Show("Plik został zaktualizowany poprawnie", "", MessageBoxButton.OK);
            }
        }

        #region FileMethods

        /// <summary>
        /// Loads first file by open dialog
        /// </summary>
        private void LoadFile()
        {
            string result = _fileManager.LoadFile();

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                FileInfo = new FileProperties(new FileInfo(result));
                IsFileLoaded = true;

                // Clears lists and loads data from files to arrays
                FileData.Clear();
                FileData.AddRange(_fileManager.LoadAndReadFile(FileInfo.FilePath));
            }
        }

        /// <summary>
        /// Resets first file loaded
        /// </summary>
        private void ResetFile()
        {
            FileInfo = null;
            IsFileLoaded = false;
            IsDownPanelAvaiable = false;
            LayPatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews file
        /// </summary>
        private void PreviewFile()
        {
            RenderNewWindow(FileData, "Podgląd pliku " + FileInfo.Name, "FilePreview");
        }

        #endregion // File Methods

        #region PatchMethods

        /// <summary>
        /// Loads first file by open dialog
        /// </summary>
        private void LoadPatch()
        {
            string result = _fileManager.LoadFile(true);

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                PatchInfo = new FileProperties(new FileInfo(result));
                IsPatchLoaded = true;

                // Clears lists and loads data from files to arrays
                PatchData.Clear();
                PatchData.AddRange(_fileManager.LoadAndReadFile(PatchInfo.FilePath));
            }
        }

        /// <summary>
        /// Resets first file loaded
        /// </summary>
        private void ResetPatch()
        {
            PatchInfo = null;
            IsPatchLoaded = false;
            IsDownPanelAvaiable = false;
            LayPatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews patch
        /// </summary>
        private void PreviewPatch()
        {
            RenderNewWindow(PatchData, "Podgląd łatki " + PatchInfo.Name, "PatchPreview");
        }

        #endregion // Patch Methods

        /// <summary>
        /// Method loads file from drag&drop area
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="properties"></param>
        public void LoadFileFromBehindCode(List<string> collection, FileProperties properties)
        {
            collection.Clear();
            collection.AddRange(_fileManager.LoadAndReadFile(properties.FilePath));
        }

        /// <summary>
        /// Method renders new window with passed data, window's title and name
        /// </summary>
        /// <param name="fielData"></param>
        /// <param name="windowTitle"></param>
        /// <param name="windowName"></param>
        private void RenderNewWindow(List<string> fileData, string windowTitle, string windowName)
        {
            if (!IsWindowOpen<MetroWindow>(windowName))
            {
                PreviewFile previewFileWindow = new PreviewFile(fileData, windowTitle);
                previewFileWindow.Name = windowName;
                previewFileWindow.Show();
            }
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
