using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BDC_V1.Enumerations;
using BDC_V1.ViewModels;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for QcGridView.xaml
    /// </summary>
    public partial class QcGridView : UserControl
    {
        public bool HasRatings { get; }

        public EnumSortingFilter CommentType => HasRatings
            ? EnumSortingFilter.InspectionIssue
            : EnumSortingFilter.InventoryIssue;

        protected QcGridView(bool hasRatings)
        {
            HasRatings = hasRatings;

            InitializeComponent();

            if (!(DataContext is QcGridViewModel model))
                throw new InvalidCastException($@"Cannot obtain model");

            model.HasRatings = hasRatings;
        }
    }
}
