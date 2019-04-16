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
    public abstract class ImagesModelBase : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdPhotosButton         { get; }
        public ICommand CmdCommentDoubleClicked { get; }
        public ICommand CmdImageDoubleClicked   { get; }

        // **************** Class constructors ********************************************** //

        protected ImagesModelBase()
        {
            RegionManagerName = "FacilityItemControl";

            CmdPhotosButton         = new DelegateCommand(OnPhotosButton );
            CmdCommentDoubleClicked = new DelegateCommand<object>(OnCommentDoubleClicked);
            CmdImageDoubleClicked   = new DelegateCommand<object>(OnImageDoubleClicked  );
        }

        // **************** Class members *************************************************** //

        // TODO: Have to figure out how to get the selected item and it's index into here
        private void OnCommentDoubleClicked([CanBeNull] object obj)
        {
            OnSelectedComment(obj as CommentBase);
        }

        private void OnImageDoubleClicked([CanBeNull] object obj) 
        {             
            OnSelectedImage(obj as ImageSource);
        }

        protected virtual void OnPhotosButton()
        {
            OnSelectedImage(null);
        }

        [CanBeNull]
        public abstract ObservableCollection<CommentBase> CommentContainer { get; }

        protected virtual void OnSelectedComment([CanBeNull] CommentBase comment, bool isInspection = false)
        {
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            model.IsDistressedEnabled = isInspection;
            model.CommentText = comment?.CommentText;
            model.FacilityBaseInfo = null;              //TODO: Put real data in here
            model.WindowTitle = isInspection
                ? "INSPECTION COMMENTS"
                : "COMMENTS";

            if (view.ShowDialog() != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
            DoSelectedComment(model.Result, comment, model.CommentText);
        }

        // these two members are separated so they can be overriden separately
        protected virtual void DoSelectedComment(EnumControlResult result, [CanBeNull] CommentBase itemBase, [CanBeNull] string modelText)
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

                        CommentContainer?.Add(newComment);
                    }

                    break;

                case EnumControlResult.ResultCancelled:
                default:
                    break;
            }
        }

        [CanBeNull]
        public abstract ObservableCollection<ImageSource> ImageContainer { get; }

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
            PhotoManagementView View = new PhotoManagementView();
            View.ShowDialog();
        }

        protected virtual void DoSelectedImage(EnumControlResult result, [CanBeNull] ImageSource itemImage, [CanBeNull] ImageSource modelImage)
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
                default:
                    break;
            }
        }
    }
}
