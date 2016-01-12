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
    class LayPatchFolderViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        // Fields
        private bool _isFolderLoaded = false;
        private bool _isPatchLoaded = false;
        private bool _isAnimationActive = false;
        private bool _isDownPanelAvaiable = false;
        private bool _isLayPatchButtonEnable = true;
        private string _layPatchStatus = string.Empty;
        private DirectoryInfo _folderInfo;
        private FileProperties _patchInfo;

        // Manager objects
        private FileManager _fileManager;
        private PatchManager _patchManager;
        private ExtensionManager _extManager;

        // List with data from comparered files
        private List<FileProperties> _folderFiles;
        public List<string> PatchData;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayPatchFolderViewModel()
        {
            PatchData = new List<string>();
            _fileManager = new FileManager();
            _extManager = new ExtensionManager();
            _folderFiles = new List<FileProperties>();
        }

        #region Commands

        // Folder commands
        private ICommand _loadFolderCommand;
        private ICommand _resetFolderCommand;
        private ICommand _previewFolderCommand;

        // Patch commands
        private ICommand _loadPatchCommand;
        private ICommand _resetPatchCommand;
        private ICommand _previewPatchCommand;
        private ICommand _layPatchFolderCommand;

        private ICommand _previewGeneratedFileCommand;
        private ICommand _saveGeneratedFileCommand;

        /// <summary>
        /// Saves updated files
        /// </summary>
        public ICommand SaveGeneratedFileCommand
        {
            get
            {
                if (_saveGeneratedFileCommand == null)
                {
                    _saveGeneratedFileCommand = new RelayCommand(p => SaveUpdatedFiles());
                }

                return _saveGeneratedFileCommand;
            }
        }

        /// <summary>
        /// Lays patch on loaded folder
        /// </summary>
        public ICommand LayPatchFolderCommand
        {
            get
            {
                if (_layPatchFolderCommand == null)
                {
                    _layPatchFolderCommand = new RelayCommand(p => LayPatchOnFile());
                }

                return _layPatchFolderCommand;
            }
        }

        /// <summary>
        /// Preview file structure
        /// </summary>
        public ICommand PreviewGeneratedFileCommand
        {
            get
            {
                if (_previewGeneratedFileCommand == null)
                {
                    _previewGeneratedFileCommand = new RelayCommand(p => PreviewLoadedFolder());
                }

                return _previewGeneratedFileCommand;
            }
        }

        #region FirstFileCommand

        /// <summary>
        /// Load folder command
        /// </summary>
        public ICommand LoadFolderCommand
        {
            get
            {
                if (_loadFolderCommand == null)
                {
                    _loadFolderCommand = new RelayCommand(p => LoadFolder());
                }

                return _loadFolderCommand;
            }
        }

        /// <summary>
        /// Reset folder command
        /// </summary>
        public ICommand ResetFolderCommand
        {
            get
            {
                if (_resetFolderCommand == null)
                {
                    _resetFolderCommand = new RelayCommand(p => ResetFolder());
                }

                return _resetFolderCommand;
            }
        }

        /// <summary>
        /// Preview folder command
        /// </summary>
        public ICommand PreviewFolderCommand
        {
            get
            {
                if (_previewFolderCommand == null)
                {
                    _previewFolderCommand = new RelayCommand(p => PreviewLoadedFolder());
                }

                return _previewFolderCommand;
            }
        }

        #endregion // First File Commands

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
        /// Object keeps folder information
        /// </summary>
        public DirectoryInfo FolderInfo
        {
            get
            {
                if (_folderInfo != null) return _folderInfo;
                return null;
            }
            set
            {
                _folderInfo = value;
                NotifyPropertyChanged("FolderInfo");
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
        /// Keeps file status
        /// </summary>
        public bool IsFolderLoaded
        {
            get { return _isFolderLoaded; }
            set
            {
                _isFolderLoaded = value;
                NotifyPropertyChanged("IsFolderLoaded");
                NotifyPropertyChanged("IsFolderLoadedNegation");
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
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return "Nałóż na folder";
            }
        }

        /// <summary>
        /// File status negation
        /// </summary>
        public bool IsFileLoadedNegation
        {
            get { return !_isFolderLoaded; }
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
            get { return _isFolderLoaded && _isPatchLoaded; }
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

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Method loads data and patch from files and lays patch on choosen file
        /// </summary>
        private void LayPatchOnFile()
        {
            // Set start properties
            LayPatchStatus = "Trwa pobieranie danych...";  // set generation patch status
            IsLayPatchButtonEnable = false;               // we set generate button on unactive
            IsAnimationActive = true;                            // we enable animation

            if (_folderFiles[0].children.Count != 0 && PatchData.Count != 0)
            {
                // Create PatchManager object
                _patchManager = new PatchManager();

                LayPatchStatus = "Trwa nakładanie łatki...";  // set generation patch status

                // Create object responsible for background work
                BackgroundWorker bw = new BackgroundWorker();

                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    // Use PatchManager to generate patch
                    _patchManager.GenerateUpdatedFileFromFolder(_folderFiles, PatchData);

                    Thread.Sleep(300);
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    // Restore defaults properties
                    IsLayPatchButtonEnable = true;
                    IsAnimationActive = false;
                    LayPatchStatus = "Nakładanie łatki zakończone.";

                    MessageBox.Show("Nakładanie łatki zakończone sukcesem !");

                    IsDownPanelAvaiable = true;
                });

                bw.RunWorkerAsync();
            }
            else
            {
                // Restore defaults properties
                IsLayPatchButtonEnable = true;
                IsAnimationActive = false;

                MessageBox.Show("Jeden z wybranych plików jest pusty. Utworznie łatki nie jest możliwe.", "Błąd podczas generowania łatki", MessageBoxButton.OK);

                LayPatchStatus = string.Empty;
            }
        }

        #region FolderMethods

        private void SaveUpdatedFiles()
        {
            SaveUpdatedFiles(_folderFiles);

            MessageBox.Show("Aktualizowanie plików zakończone powodzeniem.", "Aktualizowanie plików", MessageBoxButton.OK);
        }

        private void SaveUpdatedFiles(List<FileProperties> files)
        {
            foreach (FileProperties fp in files)
            {
                if (fp.updatedFile != null && fp.updatedFile.Count > 0)
                {
                    // save new data to file
                    _fileManager.SaveUpdatedFile(fp.updatedFile, fp.FilePath);
                }
                else if (fp.children != null)
                {
                    SaveUpdatedFiles(fp.children);
                }
            }
        }

        /// <summary>
        /// Loads folder by open dialog
        /// </summary>
        private void LoadFolder()
        {
            string result = _fileManager.LoadFolder();

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                FolderInfo = new DirectoryInfo(result);
                IsFolderLoaded = true;

                // Clears lists and loads data from files to arrays
                _folderFiles.Clear();

                // Load files from folder and get their information
                GetFilesFromFolder(_folderFiles, FolderInfo, "*", FolderInfo.FullName + @"\");
            }
        }

        /// <summary>
        /// Resets first file loaded
        /// </summary>
        private void ResetFolder()
        {
            FolderInfo = null;
            IsFolderLoaded = false;
            IsDownPanelAvaiable = false;
            LayPatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews first loaded folder
        /// </summary>
        private void PreviewLoadedFolder()
        {
            RenderNewWindow("FolderLoadedFiles", _folderFiles);
        }

        #endregion // Folder Methods

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
        private void RenderNewWindow(string windowName, List<FileProperties> collection)
        {
            if (!IsWindowOpen<MetroWindow>(windowName))
            {
                PreviewTreeFolder previewFileWindow = new PreviewTreeFolder(collection);
                previewFileWindow.Name = windowName;
                previewFileWindow.Show();
            }
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

        /// <summary>
        /// Method load all files from folder with specyfied extension
        /// </summary>
        /// <param name="filesCollection"></param>
        /// <param name="dir"></param>
        /// <param name="searchPattern"></param>
        private void GetFilesFromFolder(List<FileProperties> filesCollection, DirectoryInfo dir, string searchPattern, string dirName)
        {
            filesCollection.Add(new FileProperties(dir.Name));

            // get list of files
            try
            {
                foreach (FileInfo f in dir.GetFiles(searchPattern))
                {
                    if (_extManager.Extensions.Contains(f.Extension))
                    {
                        // Get information about files
                        filesCollection.Last().children.Add(new FileProperties(f, dirName));
                        filesCollection.Last().children.Last().fileData.AddRange(_fileManager.LoadAndReadFile(f.FullName));
                    }
                }
            }
            catch (Exception ex)
            {
                LogsManager.SaveErrorToLogFile(ex.Message);
            }

            // process each directory
            // If I have been able to see the files in the directory I should also be able 
            // to look at its directories so I dont think I should place this in a try catch block
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                GetFilesFromFolder(filesCollection.Last().children, d, searchPattern, dirName);
            }
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
