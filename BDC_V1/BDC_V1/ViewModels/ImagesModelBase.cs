using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using BDC_V1.Classes;
using JetBrains.Annotations;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ImagesModelBase : ViewModelBase
    {
        [NotNull]
        protected ItemsControl GetItemsControl([NotNull] IRegionManager regionManager)
        {
            var itemsControl = new ItemsControl() {ItemsPanel = ItemsPanelTemplate()};

            regionManager.Regions[RegionManagerName].Add(itemsControl);

            return itemsControl;
        }

        [CanBeNull]
        protected ItemsPanelTemplate ItemsPanelTemplate()
        {
            const string xaml = "<ItemsPanelTemplate\r\n" +
                                "  xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'\r\n" +
                                "  xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>\r\n" +
                                "  <StackPanel Orientation=\"Horizontal\"\r\n" +
                                "              VerticalAlignment=\"Center\"\r\n" +
                                "              HorizontalAlignment=\"Left\"/>\r\n" +
                                "</ItemsPanelTemplate>";

            return XamlReader.Parse(xaml) as ItemsPanelTemplate;
        }

        [NotNull]
        protected IEnumerable<Border> CreateImages(Size imageSize, [NotNull] IEnumerable<ImageSource> images)
        {
            return images.Select(item => 
                new Image
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment   = System.Windows.VerticalAlignment.Center,
                    Height   = imageSize.Height,
                    MinWidth = imageSize.Width,
                    Source   = item
                })
                .Select(image => new Border()
                {
                    Background      = Brushes.White, 
                    BorderThickness = new System.Windows.Thickness(1), 
                    Margin          = new System.Windows.Thickness() {Right = 5}, 
                    Child           = image
                })
                .ToList();
        }
    }
}
