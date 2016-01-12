using Patch.Models;
using Patch.Views.Help;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace Patch.ViewModels
{
    class HelpViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        private HelpView _helpView;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpViewModel"/> class.
        /// </summary>
        public HelpViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpViewModel"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public HelpViewModel(HelpView help)
        {
            Help = help;
        }

        #region Commands

        // EmployeeModel commands
        private ICommand _showGeneratePatchCommand;
        private ICommand _showLayPatchCommand;
        private ICommand _showShareHelpCommand;
        private ICommand _showSettingsHelpCommand;

        /// <summary>
        /// Command shows manage patch help
        /// </summary>
        public ICommand ShowGeneratePatchCommand
        {
            get
            {
                if (_showGeneratePatchCommand == null)
                {
                    _showGeneratePatchCommand = new RelayCommand(p => ShowGeneratePatchHelp());
                }

                return _showGeneratePatchCommand;
            }
        }

       /// <summary>
       /// Command shows lay patch helper
       /// </summary>
        public ICommand ShowLayPatchCommand
        {
            get
            {
                if (_showLayPatchCommand == null)
                {
                    _showLayPatchCommand = new RelayCommand(p => ShowLayPatchHelp());
                }

                return _showLayPatchCommand;
            }
        }

        /// <summary>
        /// Command shows settings help
        /// </summary>
        public ICommand ShowShareHelpCommand
        {
            get
            {
                if (_showShareHelpCommand == null)
                {
                    _showShareHelpCommand = new RelayCommand(p => ShowShareFileHelp());
                }

                return _showShareHelpCommand;
            }
        }
        
        /// <summary>
        /// Shows settings helper.
        /// </summary>
        public ICommand ShowSettingsHelpCommand
        {
            get
            {
                if (_showSettingsHelpCommand == null)
                {
                    _showSettingsHelpCommand = new RelayCommand(p => ShowOptionsHelp());
                }

                return _showSettingsHelpCommand;
            }
        }

        #endregion // Commands

        #region Fields

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
                return "Pomoc";
            }
        }

        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>
        /// The helper.
        /// </value>
        public HelpView Help
        {
            get { return _helpView; }
            set
            {
                _helpView = value;
            }
        }

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Shows generate patch help
        /// </summary>
        private void ShowGeneratePatchHelp()
        {
            Help.WebBrowser.Navigate(new Uri(Directory.GetCurrentDirectory() + @"\help\GeneratePatch.html"));
        }

        /// <summary>
        /// Shows lay patch help.
        /// </summary>
        private void ShowLayPatchHelp()
        {
            Help.WebBrowser.Navigate(new Uri(Directory.GetCurrentDirectory() + @"\help\LayPatch.html"));
        }

        /// <summary>
        /// Shows share file help.
        /// </summary>
        private void ShowShareFileHelp()
        {
            Help.WebBrowser.Navigate(new Uri(Directory.GetCurrentDirectory() + @"\help\Rollback.html"));
        }

        /// <summary>
        /// Shows options help.
        /// </summary>
        private void ShowOptionsHelp()
        {
            Help.WebBrowser.Navigate(new Uri(Directory.GetCurrentDirectory() + @"\help\Options.html"));
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
