using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="IQcIssueBase"/>
    public abstract class QcIssueBase : PropertyBase, IQcIssueBase
    {
        /// <inheritdoc />
        public string FacilityId
        {
            get => _facilityId;
            set => SetProperty(ref _facilityId, value);
        }
        private string _facilityId;

        /// <inheritdoc />
        public string SystemId
        {
            get => _systemId;
            set => SetProperty(ref _systemId, value);
        }
        private string _systemId;

        /// <inheritdoc />
        public string ComponentId
        {
            get => _componentId;
            set => SetProperty(ref _componentId, value);
        }
        private string _componentId;

        /// <inheritdoc />
        public string TypeName
        {
            get => _typeName;
            set => SetProperty(ref _typeName, value);
        }
        private string _typeName;

        /// <inheritdoc />
        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;

        /// <inheritdoc />
        public abstract CommentBase Comment { get; set; }

        /// <inheritdoc />
        public abstract bool HasRating { get; }

        /// <inheritdoc />
        public abstract EnumRatingType Rating { get; set; }
    }
}
