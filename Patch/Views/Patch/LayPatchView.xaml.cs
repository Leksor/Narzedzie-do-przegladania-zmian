using Patch.Models;
using Patch.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for LayPatchView.xaml
    /// </summary>
    public partial class LayPatchView : UserControl
    {
        // Layout ViewModel
        private LayPatchViewModel layPatchVM;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayPatchView()
        {
            layPatchVM = new LayPatchViewModel();
            InitializeComponent();

            // Sets current data context
            this.DataContext = layPatchVM;
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
                    layPatchVM.FileInfo = new FileProperties(new FileInfo(files[0]));
                    layPatchVM.LoadFileFromBehindCode(layPatchVM.FileData, layPatchVM.FileInfo);
                    layPatchVM.IsFileLoaded = true;
                }
                else if (a.Name.Equals("SecondStack"))
                {
                    FileInfo file = new FileInfo(files[0]);

                    if (file.Extension.Equals(".patch"))
                    {
                        layPatchVM.PatchInfo = new FileProperties(file);
                        layPatchVM.LoadFileFromBehindCode(layPatchVM.PatchData, layPatchVM.PatchInfo);
                        layPatchVM.IsPatchLoaded = true;
                    }                    
                }
            }
        }
    }
}
