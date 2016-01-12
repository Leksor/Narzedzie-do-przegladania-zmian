using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Patch.Models
{
    /// <summary>
    /// Class represents difference between compared files
    /// </summary>
    class Difference
    {
        /// <summary>
        /// Start Line number in first file.
        /// </summary>
        private int _deletedIndex;

        /// <summary>
        /// Start Line number in second file.
        /// </summary>
        private int _insertedIndex;

        /// <summary>
        /// Number of deleted lines from first file
        /// </summary>
        private int _deletedAmount;

        /// <summary>
        /// Number of inserted lines from second file
        /// </summary>
        private int _insertedAmount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startFirst"></param>
        /// <param name="startSecond"></param>
        /// <param name="deletedFromFirst"></param>
        /// <param name="insertedFromSecond"></param>
        public Difference(int startFirst, int startSecond, int deletedFromFirst, int insertedFromSecond)
        {
            _deletedIndex = startFirst;
            _insertedIndex = startSecond;
            _deletedAmount = deletedFromFirst;
            _insertedAmount = insertedFromSecond;
        }

        #region Fields

        public int deletedIndex
        {
            get { return _deletedIndex; }
            set { _deletedIndex = value; }
        }

        public int insertedIndex
        {
            get { return _insertedIndex; }
            set { _insertedIndex = value; }
        }

        public int deletedAmount
        {
            get { return _deletedAmount; }
            set { _deletedAmount = value; }
        }

        public int insertedAmount
        {
            get { return _insertedAmount; }
            set { _insertedAmount = value; }
        }

        #endregion // Fields

        /// <summary>
        /// Method return difference in specified format as string
        /// </summary>
        /// <returns></returns>
        //public List<string> getDifference(List<string> file)
        //{
        //    StringBuilder diff = new StringBuilder();
        //    List<string> diffs = new List<string>();
            
        //    /// Inserts deleted section
        //    diff.Append("d" + _startFirst.ToString() + ":" + _deletedFromFirst.ToString());
        //    diffs.Add(diff.ToString());
        //    diff.Clear();
            
        //    /// Inserts added section including changed lines
        //    diff.Append("i" + _startSecond.ToString() + ":" + _insertedFromSecond.ToString());
        //    diffs.Add(diff.ToString());
        //    diff.Clear();

        //    try
        //    {
        //        for (int i = _startSecond; i < _startSecond + _insertedFromSecond; ++i)
        //        {
        //            diffs.Add(Regex.Replace(file[i], @"\t|\n|\r", ""));
        //        }
        //    }
        //    /// Program should never come this place
        //    catch (Exception ex)
        //    {
        //        LogsManager.SaveErrorToLogFile(ex.Message);
        //    }               

        //    return diffs;
        //}
    }
}
