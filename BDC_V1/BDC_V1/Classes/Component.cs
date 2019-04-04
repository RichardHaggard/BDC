using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    class Component : SystemElement, IComponent
    {
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        public override bool? HasInspections => _inspections?.Any();
        public ObservableCollection<IInspection> Inspections
        {
            get
            {
                if (_inspections == null)
                {
                    _inspections = new QuickObservableCollection<IInspection>();
                    _qaIssueList.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasInspections));

                    RaisePropertyChanged(nameof(HasInspections));
                }

                return _inspections;
            }
        }
        private ObservableCollection<IInspection> _inspections;

        public override bool? HasDetails => _details?.Any();
        public ObservableCollection<IDetails> Details
        {
            get
            {
                if (_details == null)
                {
                    _details = new QuickObservableCollection<IDetails>();
                    _qaIssueList.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasDetails));

                    RaisePropertyChanged(nameof(HasDetails));
                }

                return _details;
            }
        }
        private ObservableCollection<IDetails> _details;

        public override bool? HasQaIssues => _details?.Any();
        public ObservableCollection<IQaIssue> QaIssueList
        {
            get
            {
                if (_qaIssueList == null)
                {
                    _qaIssueList = new QuickObservableCollection<IQaIssue>();
                    _qaIssueList.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasQaIssues));

                    RaisePropertyChanged(nameof(HasQaIssues));
                }

                return _qaIssueList;
            }
        }
        private ObservableCollection<IQaIssue> _qaIssueList;
    }
}
