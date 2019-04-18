using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class SectionInfo : PropertyBase, ISectionInfo
    {
        /// <inheritdoc />
        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;

        public SectionInfo(string sectionName = null)
        {
            SectionName = sectionName;
        }
    }
}
