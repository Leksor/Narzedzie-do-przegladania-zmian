using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patch.Models
{
    public static class LogsManager
    {
        /// <summary>
        /// Saves error to file log.txt
        /// </summary>
        /// <param name="error"></param>
        public static void SaveErrorToLogFile(string error)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Log.txt", true))
                {
                    writer.WriteLine("Data: " + DateTime.Now.ToString());
                    writer.WriteLine(error);
                    writer.WriteLine();

                    /// Close writer
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd przy zapisywaniu do pliku Log.txt. Naciśnij OK aby kontynuować.", "Log Error", MessageBoxButton.OK);
            }
        }
    }
}
