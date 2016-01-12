using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch.ViewModels
{
    interface IPatchViewModel
    {
        // Fields using to control view controls behaviour
        bool _isFirstFileLoaded { get; set; }
        bool _isSecondFileLoaded { get; set; }
        bool _isAnimationActive { get; set; }
        bool _isDownPanelAvaiable { get; set; }
        bool _isGeneratePatchButtonEnable { get; set; }
        string _generatePatchStatus { get; set; }


    }
}
