using Patch.Models;
using Patch.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for LayPatchFolderView.xaml
    /// </summary>
    public partial class LayPatchFolderView : UserControl
    {
        // Layout ViewModel
        private LayPatchFolderViewModel layPatchFolderVM;

        public LayPatchFolderView()
        {
            layPatchFolderVM = new LayPatchFolderViewModel();
            InitializeComponent();

            // Sets current data context
            this.DataContext = layPatchFolderVM;
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
                        layPatchFolderVM.PatchInfo = new FileProperties(file);
                        layPatchFolderVM.LoadFileFromBehindCode(layPatchFolderVM.PatchData, layPatchFolderVM.PatchInfo);
                        layPatchFolderVM.IsPatchLoaded = true;
                    }
                }
            }
        }
    }
}
