// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeMapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public abstract class RangeMapper
  {
    private int itemsCount;

    public int ItemsCount
    {
      get
      {
        return this.itemsCount;
      }
      set
      {
        this.itemsCount = value;
      }
    }

    public abstract int MapFromRangeIndex(int index);

    public abstract Range Normalize(Range source);

    public abstract int Normalize(Range range, int index);

    public Range CreateLeft(Range target, Range source)
    {
      return this.Normalize(Range.Intersection(target, int.MinValue, source.From));
    }

    public Range CreateRight(Range target, Range source)
    {
      return this.Normalize(Range.Intersection(target, source.To, int.MaxValue));
    }

    public abstract int Closest(int curVal, int newValue);

    public abstract bool IsInRange(Range range, int index);
  }
}
