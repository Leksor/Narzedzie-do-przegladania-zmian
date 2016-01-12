using Patch.Models;
using Patch.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for RollbackChangesView.xaml
    /// </summary>
    public partial class RollbackChangesView : UserControl
    {
        // Layout ViewModel
        private RollbackChangesViewModel rollbackVM;

        public RollbackChangesView()
        {
            rollbackVM = new RollbackChangesViewModel();

            InitializeComponent();

            // Sets current data context
            this.DataContext = rollbackVM;   
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
                if (a.Name.Equals("FirstStack"))
                {
                    rollbackVM.FileInfo = new FileProperties(new FileInfo(files[0]));
                    rollbackVM.LoadFileFromBehindCode(rollbackVM.FileData, rollbackVM.FileInfo);
                    rollbackVM.IsFileLoaded = true;
                }
                else if (a.Name.Equals("SecondStack"))
                {
                    FileInfo file = new FileInfo(files[0]);

                    if (file.Extension.Equals(".patch"))
                    {
                        rollbackVM.PatchInfo = new FileProperties(file);
                        rollbackVM.LoadFileFromBehindCode(rollbackVM.PatchData, rollbackVM.PatchInfo);
                        rollbackVM.IsPatchLoaded = true;
                    }
                }
            }
        }
    }
}
