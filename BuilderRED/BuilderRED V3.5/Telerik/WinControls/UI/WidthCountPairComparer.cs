// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WidthCountPairComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class WidthCountPairComparer : IComparer<WidthCountPair>
  {
    private bool compareWidth;

    public WidthCountPairComparer(bool compareWidth)
    {
      this.compareWidth = compareWidth;
    }

    public bool CompareWidth
    {
      get
      {
        return this.compareWidth;
      }
      set
      {
        this.compareWidth = value;
      }
    }

    public int Compare(WidthCountPair x, WidthCountPair y)
    {
      if (this.CompareWidth)
        return x.Width.CompareTo(y.Width);
      return x.Count.CompareTo(y.Count);
    }
  }
}
