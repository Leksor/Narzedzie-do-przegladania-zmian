using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch.Models.Managers
{
    class ExtensionManager
    {
        private List<string> _extensions;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensionManager()
        {
            LoadExtensions();
        }

        public List<string> Extensions
        {
            get { return _extensions; }
        }

        /// <summary>
        /// Load avaiable extensio
        /// </summary>
        private void LoadExtensions()
        {
            _extensions = new List<string>();

            _extensions.Add(".cs");
            _extensions.Add(".txt");
            _extensions.Add(".xml");
            _extensions.Add(".config");
        }
    }
}
