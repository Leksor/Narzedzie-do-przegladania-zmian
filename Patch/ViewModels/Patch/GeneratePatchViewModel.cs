using MahApps.Metro.Controls;
using Patch.Models;
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
    class GeneratePatchViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        // Fields
        private bool _isFirstFileLoaded = false;
        private bool _isSecondFileLoaded = false;
        private bool _isAnimationActive = false;
        private bool _isDownPanelAvaiable = false;
        private bool _isGeneratePatchButtonEnable = true;
        private string _generatePatchStatus = string.Empty;
        private FileProperties _firstFileInfo;
        private FileProperties _secondFileInfo;

        /// Final version of patch
        private List<string> _patch;    

        // Manager objects
        private FileManager _fileManager;
        private PatchManager _patchManager;

        // List with data from comparered files
        public List<string> FirstFileData;
        public List<string> SecondFileData;

        /// <summary>
        /// Constructor
        /// </summary>
        public GeneratePatchViewModel()
        {
            _fileManager = new FileManager();
            FirstFileData = new List<string>();
            SecondFileData = new List<string>();
        }

        #region Commands

        // Files commands
        private ICommand _loadFirstFileCommand;
        private ICommand _resetFirstFileCommand;
        private ICommand _previewFirstFileCommand;
        private ICommand _loadSecondFileCommand;
        private ICommand _resetSecondFileCommand;
        private ICommand _previewSecondFileCommand;
        private ICommand _previewFilesChangeCommand;

        // Patch commands
        private ICommand _generatePatchCommand;
        private ICommand _savePatchCommand;
        private ICommand _previewPatchCommand;

        #region FirstFileCommand

        /// <summary>
        /// Loads first file
        /// </summary>
        public ICommand LoadFirstFileCommand
        {
            get
            {
                if (_loadFirstFileCommand == null)
                {
                    _loadFirstFileCommand = new RelayCommand(p => LoadFirstFile());
                }

                return _loadFirstFileCommand;
            }
        }

        /// <summary>
        /// Resets first file command
        /// </summary>
        public ICommand ResetFirstFileCommand
        {
            get
            {
                if (_resetFirstFileCommand == null)
                {
                    _resetFirstFileCommand = new RelayCommand(p => ResetFirstFile());
                }

                return _resetFirstFileCommand;
            }
        }

        /// <summary>
        /// Previews first file
        /// </summary>
        public ICommand PreviewFirstFileCommand
        {
            get
            {
                if (_previewFirstFileCommand == null)
                {
                    _previewFirstFileCommand = new RelayCommand(p => PreviewFirstLoadedFile());
                }

                return _previewFirstFileCommand;
            }
        }

        #endregion // First File Commands

        #region SecondFileCommands

        /// <summary>
        /// Loads second file
        /// </summary>
        public ICommand LoadSecondFileCommand
        {
            get
            {
                if (_loadSecondFileCommand == null)
                {
                    _loadSecondFileCommand = new RelayCommand(p => LoadSecondFile());
                }

                return _loadSecondFileCommand;
            }
        }

        /// <summary>
        /// Resets first file command
        /// </summary>
        public ICommand ResetSecondFileCommand
        {
            get
            {
                if (_resetSecondFileCommand == null)
                {
                    _resetSecondFileCommand = new RelayCommand(p => ResetSecondFile());
                }

                return _resetSecondFileCommand;
            }
        }

        /// <summary>
        /// Previews first file
        /// </summary>
        public ICommand PreviewSecondFileCommand
        {
            get
            {
                if (_previewSecondFileCommand == null)
                {
                    _previewSecondFileCommand = new RelayCommand(p => PreviewSecondLoadedFile());
                }

                return _previewSecondFileCommand;
            }
        }

        #endregion // Second FIle Commands

        /// <summary>
        /// Preview changes between files
        /// </summary>
        public ICommand PreviewFilesChangeCommand
        {
            get
            {
                if (_previewFilesChangeCommand == null)
                {
                    _previewFilesChangeCommand = new RelayCommand(p => PreviewChanges());
                }

                return _previewFilesChangeCommand;
            }
        }

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
            get { return _isFirstFileLoaded && _isSecondFileLoaded; }
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
        /// Object keeps first file information
        /// </summary>
        public FileProperties FirstFileInfo
        {
            get
            {
                if (_firstFileInfo != null) return _firstFileInfo;
                return null;
            }
            set
            {
                _firstFileInfo = value;
                NotifyPropertyChanged("FirstFileInfo");
            }
        }

        /// <summary>
        /// Object keeps second file information
        /// </summary>
        public FileProperties SecondFileInfo
        {
            get
            {
                if (_secondFileInfo != null) return _secondFileInfo;
                return null; 
            }
            set
            {
                _secondFileInfo = value;
                NotifyPropertyChanged("SecondFileInfo");
            }
        }

        /// <summary>
        /// Keeps status of first file
        /// </summary>
        public bool IsFirstFileLoaded
        {
            get { return _isFirstFileLoaded; }
            set
            {
                _isFirstFileLoaded = value;
                NotifyPropertyChanged("IsFirstFileLoaded");
                NotifyPropertyChanged("IsFirstFileLoadedNegation");
                NotifyPropertyChanged("IsGeneratePatchButtonVisible");
            }
        }

        /// <summary>
        /// Keeps status of second file
        /// </summary>
        public bool IsSecondFileLoaded
        {
            get { return _isSecondFileLoaded; }
            set
            {
                _isSecondFileLoaded = value;
                NotifyPropertyChanged("IsSecondFileLoaded");
                NotifyPropertyChanged("IsSecondFileLoadedNegation");
                NotifyPropertyChanged("IsGeneratePatchButtonVisible");
            }
        }

        /// <summary>
        /// Negation of first file status
        /// </summary>
        public bool IsFirstFileLoadedNegation
        {
            get { return !_isFirstFileLoaded; }
        }

        /// <summary>
        /// Negation of second file status
        /// </summary>
        public bool IsSecondFileLoadedNegation
        {
            get { return !_isSecondFileLoaded; }
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
                return "Generuj z pliku";
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

            if (FirstFileData.Count != 0 && SecondFileData.Count != 0)
            {
                // Create PatchManager object
                _patchManager = new PatchManager(FirstFileData, SecondFileData);

                GeneratePatchStatus = "Trwa generowanie łatki...";  // set generation patch status

                // Create object responsible for background work
                BackgroundWorker bw = new BackgroundWorker();

                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    // Use PatchManager to generate patch
                    _patch = _patchManager.GeneratePatch(FirstFileInfo, SecondFileInfo);

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

                MessageBox.Show("Jeden z wybranych plików jest pusty. Utworznie łatki nie jest możliwe.", "Błąd podczas generowania łatki", MessageBoxButton.OK);

                GeneratePatchStatus = "";
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
        /// Method renders files preview window
        /// </summary>
        private void PreviewChanges()
        {
            RenderNewWindow(FirstFileData, SecondFileData, _patch, "Zmiany w pliku " + FirstFileInfo.Name, "Zmiany w pliku " + SecondFileInfo.Name, "PreviewFiles");
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

        #region FirstFileMethods

        /// <summary>
        /// Loads first file by open dialog
        /// </summary>
        private void LoadFirstFile()
        {
            string result = _fileManager.LoadFile();

            // File was loaded correctly so we are getting its information and setting view
            if(!string.IsNullOrEmpty(result))
            {
                FirstFileInfo = new FileProperties(new FileInfo(result));
                IsFirstFileLoaded = true;

                // Clears lists and loads data from files to arrays
                FirstFileData.Clear();                
                FirstFileData.AddRange(_fileManager.LoadAndReadFile(_firstFileInfo.FilePath));
            }
        }

        /// <summary>
        /// Resets first file loaded
        /// </summary>
        private void ResetFirstFile()
        {
            FirstFileInfo = null;
            IsFirstFileLoaded = false;
            IsDownPanelAvaiable = false;
            GeneratePatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews first loaded file
        /// </summary>
        private void PreviewFirstLoadedFile()
        {
            RenderNewWindow(FirstFileData, "Podgląd pliku " + FirstFileInfo.Name, "FirstLoadedFile");
        }

        #endregion // First File Methods

        #region SecondFileMethods

        /// <summary>
        /// Loads second file by open dialog
        /// </summary>
        private void LoadSecondFile()
        {
            string result = _fileManager.LoadFile();

            // File was loaded correctly so we are getting its information and setting view
            if (!string.IsNullOrEmpty(result))
            {
                SecondFileInfo = new FileProperties(new FileInfo(result));
                IsSecondFileLoaded = true;

                // Clears lists and loads data from files to arrays
                SecondFileData.Clear();
                SecondFileData.AddRange(_fileManager.LoadAndReadFile(_secondFileInfo.FilePath));
            }
        }

        // Resets second file loaded
        private void ResetSecondFile()
        {
            SecondFileInfo = null;
            IsSecondFileLoaded = false;
            IsDownPanelAvaiable = false;
            GeneratePatchStatus = string.Empty;
        }

        /// <summary>
        /// Previews first loaded file
        /// </summary>
        private void PreviewSecondLoadedFile()
        {
            RenderNewWindow(SecondFileData, "Podgląd pliku " + SecondFileInfo.Name, "SecondLoadedFile");
        }

        #endregion // Second File Methods

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
