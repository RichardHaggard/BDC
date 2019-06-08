// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ValueMapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ValueMapper
  {
    private List<ValueMapper.RangePair> ranges = new List<ValueMapper.RangePair>();

    public ValueMapper.RangePair this[int index]
    {
      get
      {
        return this.ranges[index];
      }
    }

    public int GetIndexFromSource(double value)
    {
      for (int index = 0; index < this.ranges.Count; ++index)
      {
        if (this.ranges[index].source.InRange(value))
          return index;
      }
      return -1;
    }

    public int GetIndexFromTarget(double value)
    {
      for (int index = 0; index < this.ranges.Count; ++index)
      {
        if (this.ranges[index].target.InRange(value))
          return index;
      }
      return -1;
    }

    public ValueMapper.MapperRange GetTarget(double value)
    {
      int indexFromSource = this.GetIndexFromSource(value);
      if (indexFromSource != -1)
        return this.ranges[indexFromSource].target;
      return (ValueMapper.MapperRange) null;
    }

    public ValueMapper.MapperRange GetSource(double value)
    {
      int indexFromTarget = this.GetIndexFromTarget(value);
      if (indexFromTarget != -1)
        return this.ranges[indexFromTarget].source;
      return (ValueMapper.MapperRange) null;
    }

    public void Add(ValueMapper.MapperRange source, ValueMapper.MapperRange target)
    {
      this.ranges.Add(new ValueMapper.RangePair(source, target));
    }

    public double MapInTarget(double value)
    {
      int indexFromSource = this.GetIndexFromSource(value);
      if (indexFromSource == -1)
        return double.NaN;
      ValueMapper.RangePair range = this.ranges[indexFromSource];
      if (range.target.Length == 0.0)
        return range.target.From;
      if (range.source.Length == 0.0)
      {
        if (!double.IsNaN(range.target.MappedValue))
          return range.target.MappedValue;
        return (range.target.From + range.target.To) / 2.0;
      }
      double num = (value - range.source.From) / range.source.Length;
      return range.target.From + num * range.target.Length;
    }

    public double MapInSource(double value)
    {
      int indexFromTarget = this.GetIndexFromTarget(value);
      if (indexFromTarget == -1)
        return double.NaN;
      ValueMapper.RangePair range = this.ranges[indexFromTarget];
      if (range.source.Length == 0.0)
        return range.source.From;
      if (range.target.Length == 0.0)
      {
        if (!double.IsNaN(range.source.MappedValue))
          return range.source.MappedValue;
        return (range.source.From + range.source.To) / 2.0;
      }
      double num = (value - range.target.From) / range.target.Length;
      return range.source.From + num * range.source.Length;
    }

    public double MapTargetToUnit(double value)
    {
      int indexFromTarget = this.GetIndexFromTarget(value);
      if (indexFromTarget == -1)
        return double.NaN;
      return this.ranges[indexFromTarget].target.MapToUnit(value);
    }

    public interface MapperRange
    {
      bool InRange(double value);

      double From { get; }

      double To { get; }

      double Length { get; }

      double MappedValue { get; }

      double MapToUnit(double value);
    }

    public class RangePair
    {
      public ValueMapper.MapperRange source;
      public ValueMapper.MapperRange target;

      public RangePair(ValueMapper.MapperRange source, ValueMapper.MapperRange target)
      {
        this.source = source;
        this.target = target;
      }
    }

    public class Range : ValueMapper.MapperRange
    {
      public double value = double.NaN;
      public int includeBorders;
      private double from;
      private double to;

      public Range()
      {
      }

      public Range(bool includeLeft, double from, double to, bool includeRight)
      {
        this.Set(includeLeft, from, to, includeRight);
      }

      public Range(
        bool includeLeft,
        double from,
        double to,
        bool includeRight,
        double defaultValue)
        : this(includeLeft, from, to, includeRight)
      {
        this.value = defaultValue;
      }

      public void Set(bool includeLeft, double from, double to, bool includeRight)
      {
        this.includeBorders = 0;
        if (includeLeft)
          this.includeBorders |= 1;
        if (includeRight)
          this.includeBorders |= 2;
        this.from = from;
        this.to = to;
      }

      public bool InRange(double value)
      {
        bool flag1 = ((this.from < value ? 1 : 0) | ((this.includeBorders & 1) != 1 ? 0 : (this.from == value ? 1 : 0))) != 0;
        bool flag2 = ((value < this.to ? 1 : 0) | ((this.includeBorders & 2) != 2 ? 0 : (this.to == value ? 1 : 0))) != 0;
        if (flag1)
          return flag2;
        return false;
      }

      public double From
      {
        get
        {
          return this.from;
        }
      }

      public double To
      {
        get
        {
          return this.to;
        }
      }

      public double Length
      {
        get
        {
          return this.to - this.from;
        }
      }

      public double MappedValue
      {
        get
        {
          return this.value;
        }
      }

      public double MapToUnit(double value)
      {
        if (value == this.to)
          return 1.0;
        double num = (value - this.from) % this.Length;
        if (num < 0.0)
          num += this.Length;
        return num / this.Length;
      }
    }

    public class Value : ValueMapper.MapperRange
    {
      public double value;

      public Value()
      {
      }

      public Value(double value)
      {
        this.value = value;
      }

      public bool InRange(double value)
      {
        return object.Equals((object) this.value, (object) value);
      }

      public double From
      {
        get
        {
          return this.value;
        }
      }

      public double To
      {
        get
        {
          return this.value;
        }
      }

      public double Length
      {
        get
        {
          return 0.0;
        }
      }

      public double MappedValue
      {
        get
        {
          return this.value;
        }
      }

      public double MapToUnit(double value)
      {
        return this.value;
      }
    }
  }
}
