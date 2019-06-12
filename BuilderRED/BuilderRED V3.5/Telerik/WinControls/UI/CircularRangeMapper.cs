// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CircularRangeMapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CircularRangeMapper : RangeMapper
  {
    public override int MapFromRangeIndex(int index)
    {
      if (this.ItemsCount < 1)
        return 0;
      index %= this.ItemsCount;
      if (index >= 0)
        return index;
      return index + this.ItemsCount;
    }

    public override Range Normalize(Range source)
    {
      if (this.ItemsCount < 1)
        return Range.Create(0, 0);
      int length = source.Length;
      int from = source.From % this.ItemsCount;
      return new Range(from, from + Math.Min(length, this.ItemsCount));
    }

    public override int Normalize(Range range, int index)
    {
      if (this.ItemsCount < 1 || range.Length < 1)
        return 0;
      int num = index % this.ItemsCount;
      if (num < range.From)
        num += this.ItemsCount;
      if (num >= range.To)
        num -= this.ItemsCount;
      return num;
    }

    public override int Closest(int curVal, int newValue)
    {
      if (this.ItemsCount == 0)
        return newValue;
      int num1 = curVal / this.ItemsCount;
      int num2 = newValue % this.ItemsCount + num1 * this.ItemsCount;
      int num3 = newValue % this.ItemsCount + (num1 - 1) * this.ItemsCount;
      int num4 = newValue % this.ItemsCount + (num1 + 1) * this.ItemsCount;
      int num5 = Math.Abs(curVal - num2);
      int num6 = Math.Abs(curVal - num3);
      int num7 = Math.Abs(curVal - num4);
      if (num5 < num6)
      {
        if (num5 >= num7)
          return num4;
        return num2;
      }
      if (num6 >= num7)
        return num4;
      return num3;
    }

    public override bool IsInRange(Range range, int index)
    {
      if (this.ItemsCount < 1)
        return false;
      int num1 = Math.Min(range.Length, this.ItemsCount);
      int num2 = range.From % this.ItemsCount;
      if (num2 < 0)
        num2 += this.ItemsCount;
      int num3 = (num2 + num1) % this.ItemsCount;
      if (num2 < num3)
      {
        if (num2 <= index)
          return index < num3;
        return false;
      }
      if (0 <= index && index < num3)
        return true;
      if (num2 <= index)
        return index < this.ItemsCount;
      return false;
    }
  }
}
