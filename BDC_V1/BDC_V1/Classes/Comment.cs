﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Comment : TimeStamp, IComment
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }
        private string _commentText;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

        public override string ToString() =>
            "[" + EntryUser.FirstLast + " " + EntryTime.ToString("M/d/yyyy hh:mm tt") + "] " + CommentText;
    }
}
