using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IComponent : ISystemElement
    {
        string Description { get; set; }

        ObservableCollection<IInspection> Inspections { get; }
        ObservableCollection<IDetails   > Details     { get; }
        ObservableCollection<IQaIssue   > QaIssueList { get; }
    }
}
