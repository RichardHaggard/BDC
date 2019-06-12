// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Range
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class Range
  {
    public static Range Full = new Range(int.MinValue, int.MaxValue);
    private int from;
    private int to;
    public static Range Empty;

    public Range()
    {
    }

    public Range(int from, int to)
    {
      this.SetRange(from, to);
    }

    public static Range Copy(Range range)
    {
      return new Range(range.from, range.to);
    }

    public static Range Create(int from, int to)
    {
      return new Range(from, to);
    }

    public int From
    {
      get
      {
        return this.from;
      }
      set
      {
        if (this.from == value)
          return;
        this.from = value;
        this.Update();
      }
    }

    public int To
    {
      get
      {
        return this.to;
      }
      set
      {
        if (this.to == value)
          return;
        this.to = value;
        this.Update();
      }
    }

    public int Length
    {
      get
      {
        return this.to - this.from;
      }
    }

    public int Center
    {
      get
      {
        return (this.from + this.to) / 2;
      }
    }

    public void Intersect(Range range)
    {
      this.from = Math.Max(this.from, Math.Min(this.to, range.from));
      this.to = Math.Min(this.to, Math.Max(this.from, range.to));
    }

    public void Intersect(int from, int to)
    {
      this.Intersect(new Range(from, to));
    }

    public Range Intersection(Range range)
    {
      return Range.Intersection(this, range);
    }

    public Range Intersection(int from, int to)
    {
      return Range.Intersection(this, from, to);
    }

    public static void Intersect(Range target, Range source)
    {
      target.Intersect(source);
    }

    public static void Intersect(Range target, int srcFrom, int srcTo)
    {
      Range.Intersect(target, new Range(srcFrom, srcTo));
    }

    public static Range Intersection(Range set1, Range set2)
    {
      Range range = new Range(set1.from, set1.to);
      range.Intersect(set2);
      return range;
    }

    public static Range Intersection(Range set1, int set2from, int set2to)
    {
      return Range.Intersection(set1, new Range(set2from, set2to));
    }

    public static Range Intersection(int set1from, int set1to, int set2from, int set2to)
    {
      return Range.Intersection(new Range(set1from, set1to), new Range(set2from, set2to));
    }

    public bool Extend(int left, int right)
    {
      return ((((true ? 1 : 0) & (left > 0 ? (this.ExtendLeft(left) ? 1 : 0) : (this.ShrinkLeft(-left) ? 1 : 0))) != 0 ? 1 : 0) & (right > 0 ? (this.ExtendRight(right) ? 1 : 0) : (this.ShrinkRight(-right) ? 1 : 0))) != 0;
    }

    public bool ExtendLeft(int left)
    {
      if (int.MinValue + left > this.from)
      {
        this.from = int.MinValue;
        return false;
      }
      this.from -= left;
      return true;
    }

    public bool ExtendRight(int right)
    {
      if (int.MaxValue - right < this.to)
      {
        this.to = int.MaxValue;
        return false;
      }
      this.to += right;
      return true;
    }

    public static Range Extend(Range range, int left, int right)
    {
      Range range1 = Range.Copy(range);
      range1.Extend(left, right);
      return range1;
    }

    public bool Shrink(int left, int right)
    {
      if (this.from + left > this.to - right)
      {
        this.to = this.from = (this.from + this.to) / 2;
        return false;
      }
      this.from += left;
      this.to -= right;
      return true;
    }

    public bool ShrinkLeft(int left)
    {
      if (this.from + left > this.to)
      {
        this.from = this.to;
        return false;
      }
      this.from += left;
      return true;
    }

    public bool ShrinkRight(int right)
    {
      if (this.to - right < this.from)
      {
        this.to = this.from;
        return false;
      }
      this.to -= right;
      return true;
    }

    public static Range Shrink(Range range, int left, int right)
    {
      Range range1 = Range.Copy(range);
      range1.Shrink(left, right);
      return range1;
    }

    public int Shift(int positions)
    {
      switch (positions.CompareTo(0))
      {
        case -1:
          return -this.ShiftLeft(-positions);
        case 1:
          return this.ShiftRight(positions);
        default:
          return 0;
      }
    }

    public int ShiftLeft(int positions)
    {
      if (positions <= 0)
        return 0;
      if (int.MinValue + positions > this.from)
        positions = this.from - int.MinValue;
      this.from -= positions;
      this.to -= positions;
      return positions;
    }

    public int ShiftRight(int positions)
    {
      if (positions <= 0)
        return 0;
      if (int.MaxValue - positions < this.to)
        positions = int.MaxValue - this.to;
      this.from += positions;
      this.to += positions;
      return positions;
    }

    public int CenterAt(int center)
    {
      int center1 = this.Center;
      int num = this.Shift(center - center1);
      return center1 + num;
    }

    public static Range CenterAt(Range range, int center)
    {
      Range range1 = Range.Copy(range);
      range1.CenterAt(center);
      return range1;
    }

    public static Range CenterAt(int from, int to, int center)
    {
      Range range = new Range(from, to);
      range.CenterAt(center);
      return range;
    }

    public void SetRange(int from, int to)
    {
      if (from <= to)
        this.Set(from, to);
      else
        this.Set(to, from);
    }

    public int FromRangeIndex(int index)
    {
      if (!this.IsInRange(index))
        return -1;
      return index - this.from;
    }

    public int FromRangeIndexRestricted(int index)
    {
      if (index < this.from)
        return 0;
      if (index >= this.to)
        return this.to - this.from - 1;
      return index - this.from;
    }

    public int ToRangeIndex(int index)
    {
      int num = this.from + index;
      if (num >= this.to)
        return -1;
      return num;
    }

    public bool IsInRange(int index)
    {
      if (this.from <= index)
        return index < this.to;
      return false;
    }

    public static bool IsInRange(Range range, int index)
    {
      if (range == null)
        return false;
      return range.IsInRange(index);
    }

    private void Update()
    {
      if (this.from <= this.to)
        return;
      this.Set(this.to, this.from);
    }

    private void Set(int from, int to)
    {
      this.from = from;
      this.to = to;
    }

    public override string ToString()
    {
      if (this.from != this.to)
        return string.Format(" [{0}, {1}) ", (object) this.from, (object) this.to);
      return string.Format(" {0} ", (object) this.from);
    }
  }
}
