using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Patch.Views
{
    /// <summary>
    /// Interaction logic for PreviewFile.xaml
    /// </summary>
    public partial class PreviewFile : MetroWindow
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public PreviewFile(List<string> data, string title)
        {
            InitializeComponent();

            /// Display window with text
            renderData(data, title);

            // Shows window on the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Method renders file text in new window
        /// </summary>
        /// <param name="data"></param>
        private void renderData(List<string> data, string title)
        {
            FlowDocument flowDocNumbers = new FlowDocument();
            FlowDocument flowDocLines = new FlowDocument();

            int i = 1;

            foreach(string s in data)
            {
                /// Set properties for line RichTextBox window
                Paragraph paragLines = new Paragraph();
                paragLines.Margin = new Thickness(10, 0, 10, 0);
                paragLines.Inlines.Add(new Run(s));

                /// Set properties for numbers RichTextBox window
                Paragraph paragNumbers = new Paragraph();
                paragNumbers.Margin = new Thickness(10, 0, 10, 0);
                paragNumbers.Foreground = Brushes.Gray;
                paragNumbers.TextAlignment = TextAlignment.Right;
                paragNumbers.Inlines.Add(new Run(i.ToString()));

                /// Add lines to components
                flowDocNumbers.Blocks.Add(paragNumbers);
                flowDocLines.Blocks.Add(paragLines);

                ++i;
            }

            /// Set window title
            Title.Content = title;

            /// Set components
            richTextBoxNumbers.Document = flowDocNumbers;
            richTextBoxLines.Document = flowDocLines;
        }

        /// <summary>
        /// Method synchronizes vertical scrollbar in RichTextBoxes components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var textToSync = (sender == richTextBoxLines) ? richTextBoxNumbers : richTextBoxLines;

            textToSync.ScrollToVerticalOffset(e.VerticalOffset);
            textToSync.ScrollToHorizontalOffset(e.HorizontalOffset);
        }
    }
}
