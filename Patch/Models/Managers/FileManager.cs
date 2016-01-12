using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Patch.Models
{
    class FileManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileManager()
        {
        }

        #region Methods

        public string LoadFolder()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            DialogResult result = folderDialog.ShowDialog();


            // Get the selected folder path
            if (result == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }

            // Returns empty string if folder was not loaded
            return string.Empty;
        }

        public string LoadFile(bool patchFilter = false)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();

            string a = @"Ada file |*.ada;*.ads;adb|Assembly anguage source file |*.asm|Unix script file |*.bsh;*.sh|Batch file |*.bat;*.cmd;*.nt|
C source file |*.c;*.cpp;*.cs|C++ source file |*.h;*.hpp;*.hxx;*.cpp;*.cxx;*.cc|C# source file |*.cs|Cascade Style Sheets file |*.css|D programming language |*.d|
Hyper Text Markup Language |*.html;*.htm;*.shtml;*.shtm;*.xhtml;*.hta|MS ini file |*.ini;*.inf;*.reg;*.url|Inno Setup script |*.iss|Java source file |*.java|JavaScript file |*.js|
JavaServer Pages script file |*.jsp|Lua source |*.lua|Makefile |*.mak|Pascal source file |*.pas;*.inc|Perl source file |*.pl;*.pm;*.plx|PHP file |*.php;*.php3;*.phtml|
Postscript file |*.ps|Properties file |*.properties|Python file |*.py;*.pyw|R programming language |*.r|Windows Resource file |*.rc|Ruby file |*.rb;*.rbw|
Scheme file |*.scm;*.smd;*.ss|Structured Query Language |*.sql|Tool Command Language file |*.tcl|TXT file |*.txt|Visual Basic file |*.vb;*.vbs|Verilog |*.v;*.sv:*.vh;*.svh|
VHSIC Hardware Description Language file |*.vhd;*.vhdl|eXtensible Markup Language file |*.xml;*.xslm;*.xsl;*.xsd;*.kml;*.wsdl;*.xlf;*.xliff|YAML Ain't Markup Language |*.yml";

            // Set filter for file extension
            fileDialog.Filter = a;
                
                //@"Ada file |*.ada;*.ads;adb|Assembly anguage source file |*.asm|Unix script file |*.bsh;*.sh|Batch file |*.bat;*.cmd;*.nt|C file |*.c;*.cpp;*.cs|PHP file |*.php|TXT file |*.txt";

            if (patchFilter)
            {
                fileDialog.Filter = "PATCH Files | *.patch";
            }
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = fileDialog.ShowDialog();


            // Get the selected file name
            if (result == true)
            {
                return fileDialog.FileName;
            }

            // Returns empty string if file was not loaded
            return string.Empty;
        }

        // Loads data from first file
        public List<string> LoadAndReadFile(string filePath)
        {
            try
            {
                // Read in a file line-by-line, and store it all in a List.
                List<string> list = new List<string>();

                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        list.Add(line); // Add to list.
                    }
                }

                return list;

            }
            catch(Exception ex)
            {
                // If any error occure save it to log file
                LogsManager.SaveErrorToLogFile(ex.Message);
            }

            return new List<string>();
        }

        public bool SaveFile(List<string> fileData)
        {
            // creates save fiale dialog object and set its properties
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();

            saveDialog.Title = "Zapisz wygenerowaną łatkę";
            saveDialog.Filter = "PATCH files (*.patch)|*.patch";

            try
            { 
                // show save filedialog
                if (saveDialog.ShowDialog() == true)
                {
                    // save file if path was choosen correctly
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        foreach(string s in fileData)
                        {
                            writer.WriteLine(s);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // If any error occure save it to log file
                LogsManager.SaveErrorToLogFile(ex.Message);

                return false;
            }

            return false;
        }

        /// <summary>
        /// Updates data in file and save changes
        /// </summary>
        /// <param name="updatedFileData"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SaveUpdatedFile(List<string> updatedFileData, string filePath)
        {
            try
            {
                // Clear file
                using (var stream = new FileStream(filePath, FileMode.Truncate))
                {
                    // Add new content to file
                    using (var writer = new StreamWriter(stream))
                    {
                        foreach(string s in updatedFileData)
                        {
                            writer.WriteLine(s);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // If any error occure save it to log file
                LogsManager.SaveErrorToLogFile(ex.Message);

                return false;
            }

            return false;
        }

        #endregion // Methods
    }
}
