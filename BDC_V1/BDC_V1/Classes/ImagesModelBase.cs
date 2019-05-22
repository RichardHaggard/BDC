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
using BDC_V1.Models;
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
                if ((_commentContainer == null) && (CommentContainerSource != null))
                {
                    _commentContainer = new IndexedCollection<ICommentBase>(CommentContainerSource);
                    _commentContainer.SelectedIndex = _commentContainer.Count > 0 ? 0 : -1;
                }

                return _commentContainer ?? 
                       new IndexedCollection<ICommentBase>(new ObservableCollection<ICommentBase>());
            }
        }

        [CanBeNull] private IndexedCollection<ImageSource> _imageContainer;
        [CanBeNull] protected abstract ObservableCollection<ImageSource> ImageContainerSource { get; }
        [NotNull] public IndexedCollection<ImageSource> ImageContainer 
        {
            get
            {
                if ((_imageContainer == null) && (ImageContainerSource != null))
                {
                    _imageContainer = new IndexedCollection<ImageSource>(ImageContainerSource);
                    _imageContainer.SelectedIndex = _imageContainer.Count > 0 ? 0 : -1;
                }

                return _imageContainer ?? 
                       new IndexedCollection<ImageSource>(new ObservableCollection<ImageSource>());
            }
        }

        [NotNull] public abstract string DetailHeaderText { get; }
        [NotNull] public abstract string TabName          { get; }
        [NotNull] public abstract string PhotoTypeText    { get; }

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
            OnSelectedComment(obj as ICommentBase ?? 
                              CommentContainer.FirstOrDefault());
        }

        private void OnImageDoubleClicked([CanBeNull] object obj) 
        {             
            OnSelectedImage(obj as ImageSource ?? 
                            ImageContainer.FirstOrDefault());
        }

        protected abstract IFacilityBase FacilityBaseInfo { get; }

        protected virtual void OnSelectedComment(
            [CanBeNull] ICommentBase comment, 
            bool isInspection = false,
            bool isFacility   = false)
        {
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            model.FacilityBaseInfo = FacilityBaseInfo;
            model.CommentText = comment?.CommentText;
            model.WindowTitle = $@"{TabName} COMMENT";
            model.HeaderText  = DetailHeaderText;
            model.IsDistressedEnabled = isInspection;
            model.IsCopyEnabled       = !isFacility;

            // TODO: remove this MOCK data
            model.CommentaryList.Clear();

            if (!isInspection)
            {
                model.CommentaryList.Add(new Commentary
                {
                    FacilityId  = "11057",
                    CodeIdText  = "C102001",
                    Rating      = EnumRatingType.R,
                    CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage. " +
                                  "CRACKED - All of the doors have 65% severe cracking and splintering"
                });
            }

            else
            {
                model.CommentaryList.AddRange(new []
                {
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.RMinus,
                        CommentText = "OPERATIONALLY IMPAIRED - The hot water pump is severely operationally impaired, as the pump housing cover has been removed. " +
                                      "CORRODED - There is mild corrosion on more than thirty percent of the surface of the pump housing cover."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.AMinus,
                        CommentText = "MOISTURE/DEBRIS CONTAMINATED- There is mild moisture/debris contamination along 10 percent along the bottom of the east wall."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.A,
                        CommentText = "DAMAGED- Twenty percent of the wall has minor damage from dents and scratches along the bottom."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.A,
                        CommentText = "DAMAGED - There are major scratches, peeling, and chipping of 90 percent of the floor sealant."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.R,
                        CommentText = "OPERATIONALLY IMPAIRED - The fire alarm control panel is significantly operationally impaired due to security trouble " +
                                      "light and beeping alarm sound."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.RMinus,
                        CommentText = "The enclosed breaker is approaching the end of its design life. Repair recommended."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.RPlus,
                        CommentText = "The exhaust fan located in the Mech room has significant surface corrosion. Recommend replace."
                    },
                    new Commentary
                    {
                        FacilityId  = "11507",
                        CodeIdText  = "X####1",
                        Rating      = EnumRatingType.YMinus,
                        CommentText = "Sink in men's room has no handle or faucet and moderate rust around handle opening. " +
                                      "Sink in woman's room has no handle."
                    },
                });
            }

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
            DoSelectedComment(model.Result, comment, model.CommentText);
        }

        // these two members are separated so they can be overriden separately
        protected virtual void DoSelectedComment(
            EnumControlResult result, 
            [CanBeNull] ICommentBase itemBase, 
            [CanBeNull] string modelText)
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
                            EntryUser   = new Person() {FirstName = "John", LastName = "Doe"},
                            EntryTime   = DateTime.Now,
                            CommentText = modelText
                        };

                        // adding to the base collection prevents the filter getting in the way
                        CommentContainer.Collection.Add(newComment);

                        // this selection will be filtered, it may become a -1 (not found)
                        CommentContainer.SelectedItem = newComment;
                    }
                    break;

                case EnumControlResult.ResultCancelled:
                default: break;
            }
        }

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
            //if (!(view.DataContext is PhotoManagementView model))
            //    throw new InvalidCastException("model is not the expected data type");

            //model.Title = $"{TabName} - {PhotoTypeText}";

            //view.ShowDialog();
            var view = new PhotoManagementView(TabName, PhotoTypeText);
            if (! (view.DataContext is PhotoManagementViewModel model))
                throw new InvalidCastException("Invalid View Model");

            // TODO: convert this to real data Probably means changing all images into PhotoModel
            model.PendingList.Clear();
            model.PendingList.AddRange(new[]
            {
                new PhotoModel("EmeraldHils.jpg", "Emerald Hills", "3/13/2019"),
                new PhotoModel("FlamingoWater.jpg.jpg", "FlamingoWater.jpg", "3/12/2019"),
                new PhotoModel("th7.jpg", "Fans", "3/11/2019")
            });
            model.PendingItem = model.PendingList[0];

            if (view.ShowDialogInParent(true) != true) return;

            DoSelectedImage(model.Result, model.PendingList, model.PendingItem);
        }

        protected virtual void DoSelectedImage(
            EnumControlResult result, 
            [NotNull] IEnumerable<PhotoModel> itemImages, 
            [NotNull] PhotoModel selectedImage)
        {
            switch (result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultDeferred:
                case EnumControlResult.ResultSaveNow:
                case EnumControlResult.ResultCancelled:
                default: break;
            }
        }
    }
}

