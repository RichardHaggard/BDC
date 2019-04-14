using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;

namespace BDC_V1.ViewModels
{
    public class AboutBdcViewModel : CloseableWindow
    {
        public System.Version ProgramVersion => Assembly.GetExecutingAssembly().GetName().Version;
        
#if DEBUG
        public string BuildType => "Debug";
#else
        public string BuildType => "Release";
#endif

        public AboutBdcViewModel()
        {

        }
    }
}
