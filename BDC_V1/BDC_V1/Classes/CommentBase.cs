using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class CommentBase : TimeStamp, ICommentBase
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
            base.ToString() + ":\n" + CommentText;

        public static bool TryParse(string formattedString, out CommentBase results)
        {
            if (TimeStamp.TryParse(formattedString, out var stamp))
            {
                results = new CommentBase
                {
                    EntryUser = stamp.EntryUser, 
                    EntryTime = stamp.EntryTime
                };

                var commentStart = formattedString.IndexOf(']');
                if ((commentStart >= 0) && 
                    (commentStart < formattedString.Length - 2) && 
                    (formattedString[commentStart + 1] == ' '))
                {
                    results.CommentText = formattedString.Substring(commentStart + 2);
                }

                return true;
            }

            results = null;
            return false;
        }
    }
}
