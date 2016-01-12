using Patch.Models;
using Patch.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for RollbackChangesFolderView.xaml
    /// </summary>
    public partial class RollbackChangesFolderView : UserControl
    {
        private RollbackChangesFolderViewModel _rollbackFolderVM;

        public RollbackChangesFolderView()
        {
            _rollbackFolderVM = new RollbackChangesFolderViewModel();
            InitializeComponent();

            // Sets current data context
            this.DataContext = _rollbackFolderVM;
        }

        /// <summary>
        /// Method responsible for drag&drop zone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                Grid a = (Grid)sender;

                // Select what drag&drop zone we have choosen
                if (a.Name.Equals("PatchStack"))
                {
                    FileInfo file = new FileInfo(files[0]);

                    if (file.Extension.Equals(".patch"))
                    {
                        _rollbackFolderVM.PatchInfo = new FileProperties(file);
                        _rollbackFolderVM.LoadFileFromBehindCode(_rollbackFolderVM.PatchData, _rollbackFolderVM.PatchInfo);
                        _rollbackFolderVM.IsPatchLoaded = true;
                    }
                }
            }
        }
    }
}
