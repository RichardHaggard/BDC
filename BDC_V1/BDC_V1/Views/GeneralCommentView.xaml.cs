using System.Windows;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for GeneralCommentView.xaml
    /// </summary>
    public partial class GeneralCommentView : Window
    {
        public GeneralCommentView()
        {
            InitializeComponent();
        }

        // singleton instance to block multiple instances 
        private static GeneralCommentView _instance;
        public static GeneralCommentView Instance => _instance ?? (_instance = new GeneralCommentView());
    }
}
