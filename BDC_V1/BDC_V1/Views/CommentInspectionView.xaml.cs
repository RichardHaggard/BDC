using System.Windows;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for CommentInspectionView.xaml
    /// </summary>
    public partial class CommentInspectionView : Window
    {
        public CommentInspectionView()
        {
            InitializeComponent();

            //EventTypeAggregator.GetEvent<Prism.Events.PubSubEvent<CloseWindowEvent>>()
            //    .Subscribe((item) =>
            //    {
            //        if (item?.WindowName==this.GetType().Name)
            //            Close();
            //    });
        }

        // singleton instance to block multiple instances 
        private static CommentInspectionView _instance;
        public static CommentInspectionView Instance => _instance ?? (_instance = new CommentInspectionView());
    }
}
