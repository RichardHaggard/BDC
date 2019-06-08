// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListFilterComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.UI
{
  public class ListFilterComparer : IComparer
  {
    private IDictionary dictionary;

    public ListFilterComparer(IDictionary d)
    {
      this.dictionary = d;
    }

    public int Compare(object keyX, object keyY)
    {
      ArrayList arrayList1 = (ArrayList) this.dictionary[keyX];
      ArrayList arrayList2 = (ArrayList) this.dictionary[keyY];
      int num = 0;
      foreach (object obj1 in arrayList1)
      {
        foreach (object obj2 in arrayList2)
        {
          if (!(obj1 is IComparable) || !(obj2 is IComparable))
            return string.Compare((string) keyX, (string) keyY);
          num += ((IComparable) obj1).CompareTo(obj2);
        }
      }
      return num;
    }
  }
}
