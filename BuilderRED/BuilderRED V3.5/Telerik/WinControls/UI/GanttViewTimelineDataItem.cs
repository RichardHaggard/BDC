// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineDataItem
  {
    private TimeSpan onePixelTime;
    private DateTime start;
    private DateTime end;
    private TimeRange range;

    public GanttViewTimelineDataItem(
      DateTime start,
      DateTime end,
      TimeRange range,
      TimeSpan onePixelTime)
    {
      this.end = end;
      this.start = start;
      this.range = range;
      this.onePixelTime = onePixelTime;
    }

    public DateTime Start
    {
      get
      {
        return this.start;
      }
      set
      {
        this.start = value;
      }
    }

    public DateTime End
    {
      get
      {
        return this.end;
      }
      set
      {
        this.end = value;
      }
    }

    public TimeRange Range
    {
      get
      {
        return this.range;
      }
      set
      {
        this.range = value;
      }
    }

    public TimeSpan OnePixelTime
    {
      get
      {
        return this.onePixelTime;
      }
      set
      {
        this.onePixelTime = value;
      }
    }

    public float Width
    {
      get
      {
        return (float) ((this.end - this.start).TotalSeconds / this.OnePixelTime.TotalSeconds);
      }
    }
  }
}
