using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class QuickObservableCollection<T> : ObservableCollection<T>
    {
        //protected bool SuppressNotification { get; set; }
 
        // !!! This gets occasional out of range exceptions !!!
        //protected override void OnCollectionChanged([CanBeNull] NotifyCollectionChangedEventArgs e)
        //{
        //    if (!SuppressNotification && (e != null)) 
        //        base.OnCollectionChanged(e);
        //}
 
        //public void AddRange([CanBeNull] IEnumerable<T> list)
        //{
        //    if (list == null) return;

        //    var oldSuppressNotification = SuppressNotification;
        //    SuppressNotification = true;

        //    foreach (var item in list)
        //        base.Add(item);

        //    SuppressNotification = oldSuppressNotification;

        //    var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list);
        //    OnCollectionChanged(args);
        //}

        //public void RemoveRange([CanBeNull] IEnumerable<T> list)
        //{
        //    if (list == null) return;

        //    var oldSuppressNotification = SuppressNotification;
        //    SuppressNotification = true;

        //    foreach (var item in list)
        //    {
        //        if (Contains(item)) Remove(item);
        //    }

        //    SuppressNotification = oldSuppressNotification;

        //    var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, list);
        //    OnCollectionChanged(args);
        //}
    }
}
