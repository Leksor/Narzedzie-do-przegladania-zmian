using MahApps.Metro.Controls;
using Patch.Models;
using Patch.Models.Managers;
using Patch.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Patch.ViewModels
{
    public class GeneratePatchFolderViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        // Fields
        private bool _isFirstFolderLoaded = false;
        private bool _isSecondFolderLoaded = false;
        private bool _isAnimationActive = false;
        private bool _isDownPanelAvaiable = false;
        private bool _isGeneratePatchButtonEnable = true;
        private string _generatePatchStatus = string.Empty;
        private DirectoryInfo _firstFolderInfo;
        private DirectoryInfo _secondFolderInfo;

        /// Final version of patch
        private List<string> _patch;

        // Manager objects
        private FileManager _fileManager;
        private PatchManager _patchManager;
        private ExtensionManager _extManager;

        // List with data from comparered files
        private List<FileProperties> _firstFolderFiles;
        private List<FileProperties> _secondFolderFiles;

        /// <summary>
        /// Constructor
        /// </summary>
        public GeneratePatchFolderViewModel()
        {
            _fileManager = new FileManager();
            _extManager = new ExtensionManager();
            _firstFolderFiles = new List<FileProperties>();
            _secondFolderFiles = new List<FileProperties>();
        }

        #region Commands

        // Folder commands
        private ICommand _loadFirstFolderCommand;
        private ICommand _resetFirstFolderCommand;
        private ICommand _previewFirstFolderCommand;
        private ICommand _loadSecondFolderCommand;
        private ICommand _resetSecondFolderCommand;
        private ICommand _previewSecondFolderCommand;

        // Patch commands
        private ICommand _generatePatchCommand;
        private ICommand _savePatchCommand;
        private ICommand _previewPatchCommand;

        #region FirstFileCommand

        /// <summary>
        /// Loads first file
        /// </summary>
        public ICommand LoadFirstFolderCommand
        {
            get
            {
                if (_loadFirstFolderCommand == null)
                {
                    _loadFirstFolderCommand = new RelayCommand(p => LoadFirstFolder());
                }

                return _loadFirstFolderCommand;
            }
        }

        /// <summary>
        /// Resets first file command
        /// </summary>
        public ICommand ResetFirstFolderCommand
        {
            get
            {
                if (_resetFirstFolderCommand == null)
                {
                    _resetFirstFolderCommand = new RelayCommand(p => ResetFirstFolder());
                }

                return _resetFirstFolderCommand;
            }
        }

        /// <summary>
        /// Previews first file
        /// </summary>
        public ICommand PreviewFirstFolderCommand
        {
            get
            {
                if (_previewFirstFolderCommand == null)
                {
                    _previewFirstFolderCommand = new RelayCommand(p => PreviewFirstLoadedFolder());
                }

                return _previewFirstFolderCommand;
            }
        }

        #endregion // First File Commands

        #region SecondFileCommands

        /// <summary>
        /// Loads second file
        /// </summary>
        public ICommand LoadSecondFolderCommand
        {
            get
            {
                if (_loadSecondFolderCommand == null)
                {
                    _loadSecondFolderCommand = new RelayCommand(p => LoadSecondFolder());
                }

                return _loadSecondFolderCommand;
            }
        }

        /// <summary>
        /// Resets first folder command
        /// </summary>
        public ICommand ResetSecondFolderCommand
        {
            get
            {
                if (_resetSecondFolderCommand == null)
                {
                    _resetSecondFolderCommand = new RelayCommand(p => ResetSecondFolder());
                }

                return _resetSecondFolderCommand;
            }
        }

        /// <summary>
        /// Previews first file
        /// </summary>
        public ICommand PreviewSecondFolderCommand
        {
            get
            {
                if (_previewSecondFolderCommand == null)
                {
                    _previewSecondFolderCommand = new RelayCommand(p => PreviewSecondLoadedFolder());
                }

                return _previewSecondFolderCommand;
            }
        }

        #endregion // Second FIle Commands

        /// <summary>
        /// Command generates new patch from loaded files
        /// </summary>
        public ICommand GeneratePatchCommand
        {
            get
            {
                if (_generatePatchCommand == null)
                {
                    _generatePatchCommand = new RelayCommand(p => GeneratePatch());
                }

                return _generatePatchCommand;
            }
        }

        /// <summary>
        /// Command allows preciew generated patch
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

        /// <summary>
        /// Command saves patch to choosen location
        /// </summary>
        public ICommand SavePatchCommand
        {
            get
            {
                if (_savePatchCommand == null)
                {
                    _savePatchCommand = new RelayCommand(p => SavePatch());
                }

                return _savePatchCommand;
            }
        }

        #endregion // Commands

        #region Fields

        /// <summary>
        /// Get patch generate status
        /// </summary>
        public string GeneratePatchStatus
        {
            get { return _generatePatchStatus; }
            set
            {
                _generatePatchStatus = value;
                NotifyPropertyChanged("GeneratePatchStatus");
            }
        }

        /// <summary>
        /// Patch generate button visible status
        /// </summary>
        public bool IsGeneratePatchButtonVisible
        {
            get { return _isFirstFolderLoaded && _isSecondFolderLoaded; }
        }

        /// <summary>
        /// Patch generate button enable status
        /// </summary>
        public bool IsGeneratePatchButtonEnable
        {
            get { return _isGeneratePatchButtonEnable; }
            set
            {
                _isGeneratePatchButtonEnable = value;
                NotifyPropertyChanged("IsGeneratePatchButtonEnable");
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
        /// Object keeps first folder information
        /// </summary>
        public DirectoryInfo FirstFolderInfo
        {
            get
            {
                if (_firstFolderInfo != null) return _firstFolderInfo;
                return null;
            }
            set
            {
                _firstFolderInfo = value;
                NotifyPropertyChanged("FirstFolderInfo");
            }
        }

        /// <summary>
        /// Object keeps second file information
        /// </summary>
        public DirectoryInfo SecondFolderInfo
        {
            get
            {
                if (_secondFolderInfo != null) return _secondFolderInfo;
                return null;
            }
            set
            {
                _secondFolderInfo = value;
                NotifyPropertyChanged("SecondFolderInfo");
            }
        }

        /// <summary>
        /// Keeps status of first folder
        /// </summary>
        public bool IsFirstFolderLoaded
        {
            get { return _isFirstFolderLoaded; }
            set
            {
                _isFirstFolderLoaded = value;
                NotifyPropertyChanged("IsFirstFolderLoaded");

                NotifyPropertyChanged("IsFirstFileLoadedNegation");
                NotifyPropertyChanged("IsGeneratePatchButtonVisible");
            }
        }

        /// <summary>
        /// Keeps status of second folder
        /// </summary>
        public bool IsSecondFolderLoaded
        {
            get { return _isSecondFolderLoaded; }
            set
            {
                _isSecondFolderLoaded = value;
                NotifyPropertyChanged("IsSecondFolderLoaded");

                NotifyPropertyChanged("IsSecondFileLoadedNegation");
                NotifyPropertyChanged("IsGeneratePatchButtonVisible");
            }
        }

        /// <summary>
        /// Negation of first folder status
        /// </summary>
        public bool IsFirstFolderLoadedNegation
        {
            get { return !_isFirstFolderLoaded; }
        }

        /// <summary>
        /// Negation of second folder status
        /// </summary>
        public bool IsSecondFolderLoadedNegation
        {
            get { return !_isSecondFolderLoaded; }
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
                return "Generuj z folderu";
            }
        }

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Method loads data from files and generate patch
        /// </summary>
        private void GeneratePatch()
        {
            // Set start properties
            GeneratePatchStatus = "Trwa pobieranie danych...";  // set generation patch status
            IsGeneratePatchButtonEnable = false;               // we set generate button on unactive
            IsAnimationActive = true;                            // we enable animation

            if (_firstFolderFiles[0].children.Count != 0 && _secondFolderFiles[0].children.Count != 0)
            {
                // Create PatchManager object
                _patchManager = new PatchManager();

                GeneratePatchStatus = "Trwa generowanie łatki...";  // set generation patch status

                // Create object responsible for background work
                BackgroundWorker bw = new BackgroundWorker();

                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    // Use PatchManager to generate patch
                    _patch = _patchManager.GeneratePatchFromFolder(_firstFolderFiles, _secondFolderFiles);

                    Thread.Sleep(300);
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    // Restore defaults properties
                    IsGeneratePatchButtonEnable = true;
                    IsAnimationActive = false;
                    GeneratePatchStatus = "Generowanie łatki zakończone.";

                    MessageBox.Show("Pobieranie i zapisywanie danych zakończone !");

                    IsDownPanelAvaiable = true;
                });

                bw.RunWorkerAsync();
            }
            else
            {
                // Restore defaults properties
                IsGeneratePatchButtonEnable = true;
                IsAnimationActive = false;

                MessageBox.Show("Jeden z wybranych folderów jest pusty. Utworznie łatki nie jest możliwe.", "Błąd podczas generowania łatki", MessageBoxButton.OK);

                GeneratePatchStatus = "";
            }
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

        #region PatchMethods

        /// <summary>
        /// Methods renders patch preview window
        /// </summary>
        private void PreviewPatch()
        {
            RenderNewWindow(_patch, "Podgląd wygenerowanej łatki", "PreviewPatch");
        }

        /// <summary>
        /// Method saves patch to choosen location
        /// </summary>
        private void SavePatch()
        {
            if (_fileManager.SaveFile(_patch))
            {
                MessageBox.Show("Łatka została zapisana poprawnie", "", MessageBoxButton.OK);
            }
        }

        #endregion // Patch Methods

        #region FirstFolderMethods

        /// <summary>
        /// Loads first file by open dialog
        /// </summary>
        private void LoadFirstFolder()
        {
            string result = _fileManager.LoadFolder();

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                FirstFolderInfo = new DirectoryInfo(result);
                IsFirstFolderLoaded = true;

                // Clears lists and loads data from files to arrays
                _firstFolderFiles.Clear();

                // Load files from folder and get their information
                GetFilesFromFolder(_firstFolderFiles, FirstFolderInfo, "*", FirstFolderInfo.FullName + @"\");
            }
        }

        /// <summary>
        /// Resets first file loaded
        /// </summary>
        private void ResetFirstFolder()
        {
            FirstFolderInfo = null;
            IsFirstFolderLoaded = false;
            IsDownPanelAvaiable = false;
            GeneratePatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews first loaded folder
        /// </summary>
        private void PreviewFirstLoadedFolder()
        {
            RenderNewWindow("FirstFolderLoaded", _firstFolderFiles);
        }

        #endregion // First File Methods

        #region SecondFileMethods

        /// <summary>
        /// Loads second folder by open dialog
        /// </summary>
        private void LoadSecondFolder()
        {
            string result = _fileManager.LoadFolder();

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                SecondFolderInfo = new DirectoryInfo(result);
                IsSecondFolderLoaded = true;

                // Clears lists and loads data from files to arrays
                _secondFolderFiles.Clear();

                // Load files from folder and get their information
                GetFilesFromFolder(_secondFolderFiles, SecondFolderInfo, "*", SecondFolderInfo.FullName + @"\");
            }
        }

        // Resets second folder loaded
        private void ResetSecondFolder()
        {
            SecondFolderInfo = null;
            IsSecondFolderLoaded = false;
            IsDownPanelAvaiable = false;
            GeneratePatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews second loaded folder
        /// </summary>
        private void PreviewSecondLoadedFolder()
        {
            RenderNewWindow("SecondFolderLoaded", _secondFolderFiles);
        }

        #endregion // Second File Methods

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
