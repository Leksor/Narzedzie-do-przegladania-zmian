using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch.Models
{
    /// <summary>
    /// Class responsible for algorithm object Shortest Middle Snake Return Data 
    /// </summary>
    class SMSRD
    {
        private int _X;
        private int _Y;

        /// <summary>
        /// Constructor
        /// </summary>
        public SMSRD(int x, int y)
        {
            _X = x;
            _Y = y;
        }

        #region Fields

        /// <summary>
        /// Return X coordinate
        /// </summary>
        public int GetX
        {
            get { return _X; }
        }

        /// <summary>
        /// Return Y coordinate
        /// </summary>
        public int GetY
        {
            get { return _Y; }
        }

        #endregion // Fields
    }
}
