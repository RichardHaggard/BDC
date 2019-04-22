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
using BDC_V1.Utils;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Regions;

namespace BDC_V1.Classes
{
    public abstract class ImagesModelBase : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdCommentDoubleClicked { get; }
        [NotNull] public ICommand CmdImageDoubleClicked   { get; }

        [CanBeNull] private IndexedCollection<ICommentBase> _commentContainer;
        [CanBeNull] protected abstract ObservableCollection<ICommentBase> CommentContainerSource { get; }
        [NotNull] public IndexedCollection<ICommentBase> CommentContainer 
        {
            get
            {
                if (_commentContainer == null)
                {
                    var source = CommentContainerSource;
                    if ((source?.Any()).Equals(true))
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        _commentContainer = new IndexedCollection<ICommentBase>(source)
                        {
                            SelectedIndex = 0
                        };
                    }
                }

                return _commentContainer ?? 
                    new IndexedCollection<ICommentBase>(new List<ICommentBase>());
            }
        }

        [CanBeNull] private IndexedCollection<ImageSource> _imageContainer;
        [CanBeNull] protected abstract ObservableCollection<ImageSource> ImageContainerSource { get; }
        [NotNull] public IndexedCollection<ImageSource> ImageContainer 
        {
            get
            {
                if (_imageContainer == null)
                {
                    var source = ImageContainerSource;
                    if ((source?.Any()).Equals(true))
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        _imageContainer = new IndexedCollection<ImageSource>(source)
                        {
                            SelectedIndex = 0
                        };
                    }
                }

                return _imageContainer ?? 
                    new IndexedCollection<ImageSource>(new List<ImageSource>());
            }
        }

        // **************** Class constructors ********************************************** //

        protected ImagesModelBase()
        {
            RegionManagerName = "FacilityItemControl";

            CmdCommentDoubleClicked = new DelegateCommand<object>(OnCommentDoubleClicked);
            CmdImageDoubleClicked   = new DelegateCommand<object>(OnImageDoubleClicked  );
        }

        // **************** Class members *************************************************** //

        // TODO: Have to figure out how to get the selected item and it's index into here
        private void OnCommentDoubleClicked([CanBeNull] object obj)
        {
            OnSelectedComment(obj as ICommentBase ?? CommentContainer.FirstOrDefault());
        }

        private void OnImageDoubleClicked([CanBeNull] object obj) 
        {             
            OnSelectedImage(obj as ImageSource ?? ImageContainer.FirstOrDefault());
        }

        [NotNull]
        public abstract string DetailHeaderText { get; }

        protected virtual void OnSelectedComment([CanBeNull] ICommentBase comment, bool isInspection = false)
        {
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            model.FacilityBaseInfo = null;              //TODO: Put real data in here
            model.CommentText = comment?.CommentText;
            model.WindowTitle = $@"{TabName} COMMENTS";
            model.HeaderText = DetailHeaderText;
            model.IsDistressedEnabled = isInspection;

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
            DoSelectedComment(model.Result, comment, model.CommentText);
        }

        // these two members are separated so they can be overriden separately
        protected virtual void DoSelectedComment(EnumControlResult result, [CanBeNull] ICommentBase itemBase, [CanBeNull] string modelText)
        {
            switch (result)
            {
                case EnumControlResult.ResultDeleteItem:
                    // TODO: ???
                    break;

                case EnumControlResult.ResultDeferred:
                case EnumControlResult.ResultSaveNow:
                    if (! string.IsNullOrEmpty(modelText))
                    {
                        // TODO: Change the timestamp OR should we add this comment with a new timestamp ???
                        // TODO: Need to add the proper kind of comment here...
                        var newComment = new CommentBase
                        {
                            EntryUser = new Person() {FirstName = "John", LastName = "Doe"},
                            EntryTime = DateTime.Now,
                            CommentText = modelText
                        };

                        CommentContainer?.Items.AddNewItem(newComment);
                    }

                    break;

                case EnumControlResult.ResultCancelled:
                default: break;
            }
        }

        public abstract string TabName       { get; }
        public abstract string PhotoTypeText { get; }

        // these two members are separated so they can be overriden separately
        protected virtual void OnSelectedImage([CanBeNull] ImageSource image)
        {
            //var view = new CameraView();
            //if (!(view.DataContext is CameraViewModel model))       
            //    throw new InvalidCastException("Invalid View Model");

            //model.SourceImage = image;
            //if (view.ShowDialog() != true) return;

            //DoSelectedImage(model.Result, image, model.SourceImage);

            // Simplistic launcher of the PM view. If this were real we'd pass in
            // info from the caller and upon return there would be an update of the photo carousel.
            var view = new PhotoManagementView( TabName, PhotoTypeText);
            //if (!(view.DataContext is PhotoManagementView model))
            //    throw new InvalidCastException("model is not the expected data type");

            //model.Title = $"{TabName} - {PhotoTypeText}";

            //view.ShowDialog();
            view.ShowDialogInParent(true);
        }

        protected virtual void DoSelectedImage(
            EnumControlResult result, 
            [CanBeNull] ImageSource itemImage, 
            [CanBeNull] ImageSource modelImage)
        {
            switch (result)
            {
                case EnumControlResult.ResultDeleteItem:
                    if (itemImage != null) ImageContainer?.Remove(itemImage);
                    break;

                case EnumControlResult.ResultDeferred:
                case EnumControlResult.ResultSaveNow:
                    if ((modelImage != null) && (ImageContainer != null))
                    {
                        if ((itemImage != null) && 
                            (modelImage != itemImage) &&
                            ImageContainer.Contains(itemImage))
                        {
                            var imageIdx = ImageContainer.IndexOf(itemImage);
                            ImageContainer[imageIdx] = modelImage;
                        }

                        else ImageContainer.Add(modelImage);
                    }

                    break;

                case EnumControlResult.ResultCancelled:
                default: break;
            }
        }
    }
}
