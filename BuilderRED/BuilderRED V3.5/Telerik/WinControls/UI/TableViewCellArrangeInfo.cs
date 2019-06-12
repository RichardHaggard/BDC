// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableViewCellArrangeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TableViewCellArrangeInfo : CellArrangeInfo
  {
    public int OffsetX;
    public int ResizeStartWidth;
    public double ResizeStartScaleFactor;
    public double TempScaleFactor;
    public double ScaleFactor;
    public double CachedWidth;

    public TableViewCellArrangeInfo(GridViewColumn column)
      : base(column)
    {
      this.CachedWidth = (double) column.Width;
    }

    public int ClampWidth(int width)
    {
      width = Math.Max(this.Column.MinWidth, width);
      if (this.Column.MaxWidth > 0)
        width = Math.Min(this.Column.MaxWidth, width);
      return width;
    }

    public void SetWidth(int width, bool suspendNotify, bool cacheOnly)
    {
      width = this.ClampWidth(width);
      if (cacheOnly)
      {
        this.CachedWidth = (double) width;
      }
      else
      {
        if (suspendNotify)
          this.Column.SuspendPropertyNotifications();
        this.Column.Width = (int) Math.Round((double) width / (double) this.Column.DpiScale.Width);
        this.CachedWidth = (double) width;
        if (!suspendNotify)
          return;
        this.Column.ResumePropertyNotifications();
      }
    }
  }
}
