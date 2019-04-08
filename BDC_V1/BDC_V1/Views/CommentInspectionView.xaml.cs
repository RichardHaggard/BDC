﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BDC_V1.Events;

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

            //EventAggregator.GetEvent<Prism.Events.PubSubEvent<CloseWindowEvent>>()
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