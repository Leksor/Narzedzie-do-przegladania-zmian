using MahApps.Metro.Controls;
using Patch.Models;
using Patch.Models.Managers;
using System;
using System.Collections.Generic;
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

namespace Patch.Views
{
    /// <summary>
    /// Interaction logic for PreviewFiles.xaml
    /// </summary>
    public partial class PreviewFiles : MetroWindow
    {
        /// <summary>
        /// keeps informations about line difference between files
        /// </summary>
        private int linesLevel;
        string lastAction = string.Empty;

        public PreviewFiles(List<string> firstData, List<string> secondData, List<string> patch, string firstTitle, string secondTitle, string windowName)
        {
            InitializeComponent();

            // set init values
            linesLevel = 0;

            // renders window with data
            renderData(firstData, secondData, patch, firstTitle, secondTitle);

            // Shows window on the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Method renders file text in new window
        /// </summary>
        /// <param name="data"></param>
        private void renderData(List<string> firstData, List<string> secondData, List<string> patch, string firstTitle, string secondTitle)
        {
            // Create patch parser
            PatchParser patchParser = new PatchParser();

            // First file FlowDocument objects 
            FlowDocument firstFlowDocNumbers = new FlowDocument();
            FlowDocument firstFlowDocLines = new FlowDocument();

            // Second file FlowDocument objects 
            FlowDocument secondFlowDocNumbers = new FlowDocument();
            FlowDocument secondFlowDocLines = new FlowDocument();

            // List with PatchData objects
            var parsedData = patchParser.ParsePatchToDiffs(patch);

            int firstIndex = 0;
            int secondIndex = 0;

            try
            {

                // Iterate every diff object and update first file
                foreach (Difference data in parsedData)
                {
                    // diffrence between deleted and inserted lines
                    // result give us 3 possible operations
                    int res = data.deletedAmount - data.insertedAmount;

                    // difference between deletedIndex and inserted index with take into account line level difference
                    //int indexRes = data.DeletedIndex - data.InsertedIndex + linesLevel;

                    // 1. if deleted and inserted are equal
                    if (res.Equals(0))
                    {
                        // complete start lines
                        for (int i = firstIndex; i < data.deletedIndex; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(firstData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++firstIndex;
                        }

                        // complete start lines
                        for (int i = secondIndex; i < data.insertedIndex; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(secondData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++secondIndex;
                        }

                        // Insert changed lines with modyfied style
                        for (int i = data.deletedIndex; i < data.deletedIndex + data.deletedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(firstData[i]));
                            paragNumbers.Inlines.Add(new Run((firstIndex + 1).ToString()));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++firstIndex;
                        }

                        // Insert changed lines with modyfied style
                        for (int i = data.insertedIndex; i < data.insertedIndex + data.insertedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(secondData[i]));
                            paragNumbers.Inlines.Add(new Run((secondIndex + 1).ToString()));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++secondIndex;
                        }
                    }

                    // 2. if deleted < inserted
                    if (res < 0)
                    {
                        if (lastAction.Equals("d"))
                        {
                            linesLevel *= -1;
                        }
                        // complete start lines to first file
                        for (int i = firstIndex; i < data.insertedIndex + linesLevel; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(firstData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++firstIndex;
                        }

                        // complete start lines to second file
                        for (int i = secondIndex; i < data.insertedIndex; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(secondData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++secondIndex;
                        }

                        // if there is some changed lines
                        if (data.deletedAmount > 0)
                        {
                            // we add changed lines to first file
                            for (int i = 0; i < data.deletedAmount; ++i)
                            {
                                Paragraph paragLines = new Paragraph();
                                Paragraph paragNumbers = new Paragraph();

                                // set properties
                                paragLines.Margin = new Thickness(0, 1, 0, 1);
                                paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                                paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                                paragNumbers.Foreground = Brushes.Gray;
                                paragNumbers.TextAlignment = TextAlignment.Right;

                                paragLines.Inlines.Add(new Run(firstData[firstIndex]));
                                paragNumbers.Inlines.Add(new Run((firstIndex + 1).ToString()));

                                firstFlowDocLines.Blocks.Add(paragLines);
                                firstFlowDocNumbers.Blocks.Add(paragNumbers);

                                ++firstIndex;
                            }

                            // we add changed lines to second file
                            for (int i = 0; i < data.deletedAmount; ++i)
                            {
                                Paragraph paragLines = new Paragraph();
                                Paragraph paragNumbers = new Paragraph();

                                // set properties
                                paragLines.Margin = new Thickness(0, 1, 0, 1);
                                paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                                paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                                paragNumbers.Foreground = Brushes.Gray;
                                paragNumbers.TextAlignment = TextAlignment.Right;

                                paragLines.Inlines.Add(new Run(secondData[secondIndex]));
                                paragNumbers.Inlines.Add(new Run((secondIndex + 1).ToString()));

                                secondFlowDocLines.Blocks.Add(paragLines);
                                secondFlowDocNumbers.Blocks.Add(paragNumbers);

                                ++secondIndex;
                            }
                        }

                        // here add empty lines to first
                        // if d = 0 we dont have information about d index in patch note 
                        // so we keep information about added lines in this class
                        for (int i = 0; i < data.insertedAmount - data.deletedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CCCCCC"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(string.Empty));
                            paragNumbers.Inlines.Add(new Run(string.Empty));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);
                        }

                        // here add inserted lines to second file
                        int tmp = secondIndex;

                        for (int i = secondIndex; i < tmp + data.insertedAmount - data.deletedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#82FF82"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(secondData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++secondIndex;
                        }

                        /// set actual line lvl
                        linesLevel += res;
                        lastAction = "i";
                    }

                    // 3. if deleted > inserted
                    if (res > 0)
                    {
                        if (lastAction.Equals("i"))
                        {
                            linesLevel *= -1;
                        }
                        // complete start lines to first file
                        for (int i = firstIndex; i < data.deletedIndex; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(firstData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++firstIndex;
                        }

                        // complete start lines to second file
                        for (int i = secondIndex; i < data.deletedIndex + linesLevel; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(secondData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++secondIndex;
                        }

                        // if there is some changed lines
                        if (data.insertedAmount > 0)
                        {
                            // we add changed lines to first file
                            for (int i = 0; i < data.insertedAmount; ++i)
                            {
                                Paragraph paragLines = new Paragraph();
                                Paragraph paragNumbers = new Paragraph();

                                // set properties
                                paragLines.Margin = new Thickness(0, 1, 0, 1);
                                paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                                paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                                paragNumbers.Foreground = Brushes.Gray;
                                paragNumbers.TextAlignment = TextAlignment.Right;

                                paragLines.Inlines.Add(new Run(firstData[firstIndex]));
                                paragNumbers.Inlines.Add(new Run((firstIndex + 1).ToString()));

                                firstFlowDocLines.Blocks.Add(paragLines);
                                firstFlowDocNumbers.Blocks.Add(paragNumbers);

                                ++firstIndex;
                            }

                            // we add changed lines to second file
                            for (int i = 0; i < data.insertedAmount; ++i)
                            {
                                Paragraph paragLines = new Paragraph();
                                Paragraph paragNumbers = new Paragraph();

                                // set properties
                                paragLines.Margin = new Thickness(0, 1, 0, 1);
                                paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF74F"));
                                paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                                paragNumbers.Foreground = Brushes.Gray;
                                paragNumbers.TextAlignment = TextAlignment.Right;

                                paragLines.Inlines.Add(new Run(secondData[secondIndex]));
                                paragNumbers.Inlines.Add(new Run((secondIndex + 1).ToString()));

                                secondFlowDocLines.Blocks.Add(paragLines);
                                secondFlowDocNumbers.Blocks.Add(paragNumbers);

                                ++secondIndex;
                            }
                        }

                        int tmp = firstIndex;

                        // add deleted lines to first file
                        for (int i = firstIndex; i < tmp + data.deletedAmount - data.insertedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ED121D"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(firstData[i]));
                            paragNumbers.Inlines.Add(new Run((i + 1).ToString()));

                            firstFlowDocLines.Blocks.Add(paragLines);
                            firstFlowDocNumbers.Blocks.Add(paragNumbers);

                            ++firstIndex;
                        }

                        tmp = secondIndex;

                        // add empty lines to second file
                        for (int i = 0; i < data.deletedAmount - data.insertedAmount; ++i)
                        {
                            Paragraph paragLines = new Paragraph();
                            Paragraph paragNumbers = new Paragraph();

                            // set properties
                            paragLines.Margin = new Thickness(0, 1, 0, 1);
                            paragLines.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CCCCCC"));
                            paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                            paragNumbers.Foreground = Brushes.Gray;
                            paragNumbers.TextAlignment = TextAlignment.Right;

                            paragLines.Inlines.Add(new Run(string.Empty));
                            paragNumbers.Inlines.Add(new Run(string.Empty));

                            secondFlowDocLines.Blocks.Add(paragLines);
                            secondFlowDocNumbers.Blocks.Add(paragNumbers);
                        }

                        linesLevel -= res;
                        lastAction = "d";
                    }
                }

                /// complete last missing linesc to first file
                for (int i = firstIndex; i < firstData.Count; ++i)
                {
                    Paragraph paragLines = new Paragraph();
                    Paragraph paragNumbers = new Paragraph();

                    // set properties
                    paragLines.Margin = new Thickness(0, 1, 0, 1);
                    paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                    paragNumbers.Foreground = Brushes.Gray;
                    paragNumbers.TextAlignment = TextAlignment.Right;

                    paragLines.Inlines.Add(new Run(firstData[i]));
                    paragNumbers.Inlines.Add(new Run((firstIndex + 1).ToString()));

                    firstFlowDocLines.Blocks.Add(paragLines);
                    firstFlowDocNumbers.Blocks.Add(paragNumbers);

                    ++firstIndex;
                }

                /// complete last missing linesc to second file
                for (int i = secondIndex; i < secondData.Count; ++i)
                {
                    Paragraph paragLines = new Paragraph();
                    Paragraph paragNumbers = new Paragraph();

                    // set properties
                    paragLines.Margin = new Thickness(0, 1, 0, 1);
                    paragNumbers.Margin = new Thickness(5, 1, 5, 1);
                    paragNumbers.Foreground = Brushes.Gray;
                    paragNumbers.TextAlignment = TextAlignment.Right;

                    paragLines.Inlines.Add(new Run(secondData[i]));
                    paragNumbers.Inlines.Add(new Run((secondIndex + 1).ToString()));

                    secondFlowDocLines.Blocks.Add(paragLines);
                    secondFlowDocNumbers.Blocks.Add(paragNumbers);

                    ++secondIndex;
                }

                /// Set window title
                TitleFirst.Content = firstTitle;
                TitleSecond.Content = secondTitle;

                /// Set components
                //richTextBoxNumbers1.Document = flowDocNumbers;
                richTextBoxLines1.Document = firstFlowDocLines;
                richTextBoxNumbers1.Document = firstFlowDocNumbers;

                richTextBoxLines2.Document = secondFlowDocLines;
                richTextBoxNumbers2.Document = secondFlowDocNumbers;
            } 
            catch(IndexOutOfRangeException ex)
            {
                MessageBox.Show("Źródła projektu są aktualne", "Błąd podczas nakładania łatki", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Method synchronizes vertical scrollbar in RichTextBoxes components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_ScrollChanged1(object sender, ScrollChangedEventArgs e)
        {
            var textToSync = (sender == richTextBoxLines1) ? richTextBoxNumbers1 : richTextBoxLines1;
            var textToSync1 = (sender == richTextBoxLines2) ? richTextBoxNumbers2 : richTextBoxLines2;

            // synchronize scroll for left view
            textToSync.ScrollToVerticalOffset(e.VerticalOffset);
            textToSync.ScrollToHorizontalOffset(e.HorizontalOffset);

            // synchronize scroll for right view
            textToSync1.ScrollToVerticalOffset(e.VerticalOffset);
            textToSync1.ScrollToHorizontalOffset(e.HorizontalOffset);
        }
    }
}
