// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RestrictedRangeMapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RestrictedRangeMapper : RangeMapper
  {
    public override int MapFromRangeIndex(int index)
    {
      return this.Normalize(Range.Create(0, this.ItemsCount), index);
    }

    public override Range Normalize(Range source)
    {
      return Range.Intersection(source, 0, this.ItemsCount);
    }

    public override int Normalize(Range source, int index)
    {
      if (index < source.From)
        return source.From;
      if (index >= source.To)
        return Math.Max(0, source.To - 1);
      return index;
    }

    public override int Closest(int curVal, int newValue)
    {
      return newValue;
    }

    public override bool IsInRange(Range range, int index)
    {
      if (range.From <= index)
        return index < range.To;
      return false;
    }
  }
}
