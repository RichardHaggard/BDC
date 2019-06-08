// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElementZOrderComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class RadElementZOrderComparer : Comparer<RadElement>
  {
    private bool reverse;

    public RadElementZOrderComparer(bool reverse)
    {
      this.reverse = reverse;
    }

    public override int Compare(RadElement x, RadElement y)
    {
      if (x == null || y == null)
        return 0;
      int num = this.reverse ? -1 : 1;
      if (x.ZIndex > y.ZIndex)
        return num;
      if (x.ZIndex < y.ZIndex)
        return -1 * num;
      return 0;
    }
  }
}
