using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using BDC_V1.Converters;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Regions;

namespace BDC_V1.Classes
{
    public class ImagesModelBase : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public    ICommand CmdPhotosButton  { get; }
        protected ICommand CmdSelectedImage { get; }

        [CanBeNull]
        protected virtual ItemsControl ImageItemsControl
        {
            get => _imageItemsControl;
            set => SetProperty(ref _imageItemsControl, value);
        }
        [CanBeNull] private ItemsControl _imageItemsControl;

        protected virtual Size ImageSize
        {
            get => _imageSize;
            set => SetProperty(ref _imageSize, value);
        }
        private Size _imageSize = new Size
        {
            Height = 120, // ImageItemsControl.ActualHeight, ???
            Width  = 20   // minimum width ???
        };

        [NotNull]
        private INotifyingCollection<Button> ImagesItem
        {
            get
            {
                if (!(ImageItemsControl?.ItemsSource is INotifyingCollection<Button> items))
                    return _imagesItem ?? (_imagesItem = new NotifyingCollection<Button>());

                return items;
            }

            set
            {
                if (ImageItemsControl == null) SetProperty(ref _imagesItem, value);
                else 
                {
                    ImageItemsControl.ItemsSource = value;
                    RaisePropertyChanged(nameof(ImageItemsControl));
                }
            }
        }
        private INotifyingCollection<Button> _imagesItem;

        // **************** Class constructors ********************************************** //

        public ImagesModelBase()
        {
            RegionManagerName = "FacilityItemControl";

            CmdPhotosButton  = new DelegateCommand        (OnPhotosButton );
            CmdSelectedImage = new DelegateCommand<object>(OnSelectedImage);
        }

        // **************** Class members *************************************************** //

        protected virtual void OnPhotosButton()
        {
#if false
            // this almost works... just have to close all the dangling windows on App exit
            // ??? should we open this in a new thread ???
            var view = CameraView.Instance;
            if(view.IsVisible) view.Topmost = true;
            else               view.Show();
#else
            //var view = new CameraView();
            //view.ShowDialog();
            OnSelectedImage(null);
#endif
        }

        [CanBeNull]
        protected ItemsPanelTemplate ImagePanelTemplate()
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
        protected ItemsControl GetIImageItemControl([NotNull] IRegionManager regionManager)
        {
            var imageItems = new ItemsControl {ItemsPanel = ImagePanelTemplate()};

            regionManager.Regions[RegionManagerName].RemoveAll();
            regionManager.Regions[RegionManagerName].Add(imageItems);

            return imageItems;
        }

        [NotNull]
        protected Image CreateImage([NotNull] ImageSource source)
        {
            return new Image
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment   = VerticalAlignment.Stretch,
                Stretch = Stretch.Uniform,
                Source  = source
            };
        }

        [NotNull]
        protected Button CreateButton(Size imageSize)
        {
            var button = new Button
            {
                BorderThickness = new Thickness(0),
                BorderBrush = Brushes.Black,
                Background  = Brushes.Transparent, //Brushes.White, 
                Height      = imageSize.Height,
                MinWidth    = imageSize.Width,
                Margin      = new Thickness(0) {Right = 5},
                Tag         = Guid.NewGuid(),    // hack to identify this button
                Command     = CmdSelectedImage
            };

            // ReSharper disable once UseObjectOrCollectionInitializer
            var binds = new MultiBinding();
            binds.Converter = new ImageButtonConverter();
            binds.Bindings.AddRange(new[]
            {
                new Binding("Content") { RelativeSource = new RelativeSource(RelativeSourceMode.Self) },
                new Binding("Tag"    ) { RelativeSource = new RelativeSource(RelativeSourceMode.Self) }
            });

            button.SetBinding(ButtonBase.CommandParameterProperty, binds);

            return button;
        }

        [NotNull]
        protected Button CreateImageButton(Size imageSize, [NotNull] ImageSource source)
        {
            var button = CreateButton(imageSize);
            button.Content = CreateImage(source);

            return button;
        }

        [NotNull]
        protected IEnumerable<Button> CreateImageButtons(Size imageSize, [NotNull] IEnumerable<ImageSource> images)
        {
            var items = images.Select(item => CreateImageButton(imageSize, item)).ToList();
            return items;
        }

        private void OnSelectedImage([CanBeNull] object obj)
        {
            var view = new CameraView();
            if (!(view.DataContext is CameraViewModel model)) return;

            var param = ((IEnumerable) obj)?.Cast<object>().ToArray();
            var image = (param?.Length >= 1)? param[0] as Image : null;
            var guid  = (param?.Length >= 2)? param[1] as Guid? : null;

            model.SourceImage = image?.Source;
            if (view.ShowDialog() != true) return;

            var imageItems = ImagesItem;

            // ??? this is a hack, there should be a better way to get the button that issues this command ???
            var button = (guid != null) ? imageItems.FirstOrDefault(g => (g.Tag as Guid?) == guid) : null;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                    if (button != null) imageItems.Remove(button);
                    break;

                case EnumControlResult.ResultDeferred:
                case EnumControlResult.ResultSaveNow:
                    if (image != null) image.Source = model.SourceImage;
                    else if (model.SourceImage != null)
                    {
                        if (button != null)
                        {
                            button.Content = CreateImage(model.SourceImage);
                        }
                        else
                        {
                            button = CreateImageButton(ImageSize, model.SourceImage);
                            imageItems.Add(button);
                        }
                    }

                    break;
            }

            ImagesItem = imageItems;
        }

        protected virtual void CreateImages([CanBeNull] INotifyingCollection<ImageSource> srcImages)
        {
            if (ImageItemsControl == null) return;

            var imageItems = ImagesItem;
            imageItems.Clear();

            if (srcImages != null)
            {
                // make sure we don't double up on the notifications
                srcImages.CollectionChanged -= OnImageCollectionChanged;   
                srcImages.CollectionChanged += OnImageCollectionChanged;

                var itemList = CreateImageButtons(ImageSize, srcImages);
                imageItems.AddRange(itemList);
            }

            // ReSharper disable once PossibleNullReferenceException
            ImagesItem = imageItems;
        }

        protected virtual void OnImageCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var imageItems = ImagesItem;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    var addImageSourceList = e.NewItems?.Cast<ImageSource>().ToArray();
                    if (addImageSourceList?.Any() == true)
                    {
                        var addImageList = CreateImageButtons(ImageSize, addImageSourceList);
                        imageItems.AddRange(addImageList);
                    }            
                        
                    break;
                }

                case NotifyCollectionChangedAction.Remove:
                {
                    var delImageSourceList = e.OldItems?.Cast<ImageSource>().ToArray();
                    if (delImageSourceList?.Any() == true)
                    {
                        foreach (var imageSource in delImageSourceList)
                        {
                            var delImages = imageItems
                                .Where(image => (image.Content as Image)?.Source == imageSource)
                                .ToArray();

                            imageItems.RemoveRange(delImages);
                        }
                    }            
                        
                    break;
                }

                case NotifyCollectionChangedAction.Replace:
                {
                    var delArg = new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove, 
                        new List<ImageSource>(), e.OldItems);

                    OnImageCollectionChanged(sender, delArg);

                    var addArg = new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, 
                        e.NewItems, new List<ImageSource>());

                    OnImageCollectionChanged(sender, addArg);
                        
                    break;
                }

                case NotifyCollectionChangedAction.Reset:
                    imageItems.Clear();
                    break;
            }

            ImagesItem = imageItems;
        }

        protected virtual void AddImage([NotNull] Button image) => 
            ImagesItem.Add(image);

    }
}
