using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface ICommentBase : ITimeStamp, INotifyPropertyChanged
    {
        string CommentText { get; set; }

        string ToString();
    }
}
