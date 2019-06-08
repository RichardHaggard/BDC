// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PriorityWeakReferenceList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  internal class PriorityWeakReferenceList : WeakReferenceList<IGridViewEventListener>
  {
    public PriorityWeakReferenceList()
      : base(true, false)
    {
    }

    protected override void InsertCore(int index, IGridViewEventListener value)
    {
      index = this.FindInsertIndex(value);
      base.InsertCore(index, value);
    }

    private int FindInsertIndex(IGridViewEventListener listener)
    {
      int num1 = this.Count;
      if (num1 == 0)
        return 0;
      int num2 = 0;
      do
      {
        int index = num2 + num1 >> 1;
        IGridViewEventListener viewEventListener = this[index];
        if (viewEventListener != null)
        {
          switch (listener.Priority.CompareTo((object) viewEventListener.Priority))
          {
            case -1:
              num1 = index;
              break;
            case 0:
              return index;
            default:
              num2 = index + 1;
              break;
          }
        }
        else
          break;
      }
      while (num2 < num1);
      return num2;
    }

    internal IEnumerable<IGridViewEventListener> ReverseForEach()
    {
      List<WeakReference> list = this.List;
      for (int i = list.Count - 1; i >= 0; --i)
      {
        if (i >= list.Count)
          i = list.Count - 1;
        WeakReference reference = list[i];
        if (!reference.IsAlive)
          list.RemoveAt(i);
        else
          yield return reference.Target as IGridViewEventListener;
      }
    }
  }
}
