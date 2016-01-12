using Patch.Models;
using Patch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Patch.ViewModels
{
    class ApplicationViewModel : ObservableObject
    {
        private IPageViewModel _currentPageViewModel;

        private List<IPageViewModel> _pageViewModels;           // main container with all pages
        private List<IPageViewModel> _generatePatchViewModels;
        private List<IPageViewModel> _layPatchViewModels;
        private List<IPageViewModel> _rollbackViewModels;
        private List<IPageViewModel> _helpViewModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {
        }

        #region Commands

        private ICommand _changePageCommand;

        /// <summary>
        /// Gets the change page command.
        /// </summary>
        /// <value>
        /// The change page command.
        /// </value>
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        #endregion // Commands

        #region Fields

        /// <summary>
        /// Gets the page view models.
        /// </summary>
        /// <value>
        /// The page view models.
        /// </value>
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        /// <summary>
        /// Gets the patches view models
        /// </summary>
        public List<IPageViewModel> GeneratePatchViewModels
        {
            get
            {
                if (_generatePatchViewModels == null)
                    _generatePatchViewModels = new List<IPageViewModel>();

                return _generatePatchViewModels;
            }
        }

        /// <summary>
        /// Get patches view models
        /// </summary>
        public List<IPageViewModel> LayPatchViewModels
        {
            get
            {
                if (_layPatchViewModels == null)
                    _layPatchViewModels = new List<IPageViewModel>();

                return _layPatchViewModels;
            }
        }

        /// <summary>
        /// Gets settings view models
        /// </summary>
        public List<IPageViewModel> RollbackViewModels
        {
            get
            {
                if (_rollbackViewModels == null)
                    _rollbackViewModels = new List<IPageViewModel>();

                return _rollbackViewModels;
            }
        }

        /// <summary>
        /// Gets the helper view models.
        /// </summary>
        /// <value>
        /// The helper view models.
        /// </value>
        public List<IPageViewModel> HelpViewModels
        {
            get
            {
                if (_helpViewModels == null)
                    _helpViewModels = new List<IPageViewModel>();

                return _helpViewModels;
            }
        }

        /// <summary>
        /// Gets or sets the current page view model.
        /// </summary>
        /// <value>
        /// The current page view model.
        /// </value>
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion // Fields

        #region Methods

        /// <summary>
        /// Loads all pages to main container
        /// </summary>
        public void LoadPages()
        {
            try
            {
                // Adds view model site's to each container
                GeneratePatchViewModels.Add(new GeneratePatchViewModel());
                GeneratePatchViewModels.Add(new GeneratePatchFolderViewModel());
                LayPatchViewModels.Add(new LayPatchViewModel());
                LayPatchViewModels.Add(new LayPatchFolderViewModel());
                RollbackViewModels.Add(new RollbackChangesViewModel());
                RollbackViewModels.Add(new RollbackChangesFolderViewModel());                
                HelpViewModels.Add(new HelpViewModel());

                PageViewModels.AddRange(_generatePatchViewModels);
                PageViewModels.AddRange(_layPatchViewModels);
                PageViewModels.AddRange(_rollbackViewModels);
                PageViewModels.AddRange(_helpViewModels);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Set starting page
                CurrentPageViewModel = GeneratePatchViewModels[0];
            }
        }

        /// <summary>
        /// Changes the view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion // Methods
    }
}
