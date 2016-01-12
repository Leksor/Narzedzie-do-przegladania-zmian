using Patch.Models;
using Patch.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Patch.Views.Patch
{
    /// <summary>
    /// Interaction logic for GeneratePathView.xaml
    /// </summary>
    public partial class GeneratePatchView : UserControl
    {
        GeneratePatchViewModel generatePatchVM;

        public GeneratePatchView()
        {
            generatePatchVM = new GeneratePatchViewModel();
            InitializeComponent();

            // Sets current data context
            this.DataContext = generatePatchVM;
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
                    generatePatchVM.FirstFileInfo = new FileProperties(new FileInfo(files[0]));
                    generatePatchVM.LoadFileFromBehindCode(generatePatchVM.FirstFileData, generatePatchVM.FirstFileInfo);
                    generatePatchVM.IsFirstFileLoaded = true;
                }
                else if(a.Name.Equals("SecondStack"))
                {
                    generatePatchVM.SecondFileInfo = new FileProperties(new FileInfo(files[0]));
                    generatePatchVM.LoadFileFromBehindCode(generatePatchVM.SecondFileData, generatePatchVM.SecondFileInfo);
                    generatePatchVM.IsSecondFileLoaded = true;

                }
            }
        }
    }
}
