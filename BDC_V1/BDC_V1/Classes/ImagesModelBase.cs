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

        protected virtual void OnSelectedComment([CanBeNull] CommentBase comment)
        {
            var view = new CommentView();
            if (!(view.DataContext is CommentViewModel model)) return;

            if (view.ShowDialog() != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
            DoSelectedComment(model.Result, comment, null);
        }

        // these two members are separated so they can be overriden separately
        protected virtual void DoSelectedComment(EnumControlResult result, [CanBeNull] CommentBase itemText, [CanBeNull] CommentBase modelText)
        {
            // TODO: Do something useful here...
        }

        [CanBeNull]
        public abstract ObservableCollection<ImageSource> ImageContainer { get; }

        // these two members are separated so they can be overriden separately
        protected virtual void OnSelectedImage([CanBeNull] ImageSource image)
        {
            var view = new CameraView();
            if (!(view.DataContext is CameraViewModel model)) return;

            model.SourceImage = image;
            if (view.ShowDialog() != true) return;

            DoSelectedImage(model.Result, image, model.SourceImage);
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
