using System.Collections.Generic;

namespace Patch.Models
{
    /// <summary>
    /// Class contains data of loaded file which being used during comparing files 
    /// </summary>
    class FileData
    {
        /// <summary>
        /// Amount of lines from loaded file
        /// </summary>
        private int _lineAmount;

        /// <summary>
        /// List contains numbers which represents every line from loaded file
        /// </summary>
        private List<int> _linesNumbers;

        /// <summary>
        /// List of booleans that represents compared lines state.
        /// </summary>
        private List<bool> _modifiedLinesState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initLinesNumbers"></param>
        public FileData(List<int> initLinesNumbers)
        {
            _linesNumbers = new List<int>(initLinesNumbers);
            _lineAmount = initLinesNumbers.Count;
            _modifiedLinesState = new List<bool>();

            /// Sets default modyfied lines state
            for(int i = 0; i < initLinesNumbers.Count + 2; ++i)
            {
                _modifiedLinesState.Add(false);
            }
        }

        #region Fields

        /// <summary>
        /// LineAmount getter
        /// </summary>
        public int LineAmount
        {
            get { return _lineAmount; }
        }

        /// <summary>
        /// LinesNumbers getter
        /// </summary>
        public List<int> LinesNumbers
        {
            get { return _linesNumbers; }
        }

        /// <summary>
        /// ModifiedLinesState getter
        /// </summary>
        public List<bool> ModifiedLinesState
        {
            get { return _modifiedLinesState; }
        }

        #endregion // Fields
    }
}
