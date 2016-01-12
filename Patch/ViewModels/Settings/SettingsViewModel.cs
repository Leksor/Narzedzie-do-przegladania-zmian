using Patch.Models;
using System.ComponentModel;

namespace Patch.ViewModels
{
    class SettingsViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsViewModel()
        {
        }

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
                return "Ustawienia";
            }
        }

        #endregion // Fields

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
