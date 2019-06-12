// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.SplitContainerLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.Docking
{
  public class SplitContainerLayoutInfo
  {
    internal SplitPanel fillPanel;
    internal List<SplitPanel> layoutTargets;
    internal List<SplitPanel> autoSizeTargets;
    internal List<SplitPanel> absoluteSizeTargets;
    internal Orientation orientation;
    internal float autoSizeCountFactor;
    internal int availableLength;
    internal int autoSizeLength;
    internal int totalSplitterLength;
    internal int splitterLength;
    internal int absoluteSizeLength;
    internal int totalMeasuredLength;
    internal int totalMinLength;
    internal Rectangle contentRect;

    public SplitContainerLayoutInfo()
    {
      this.layoutTargets = new List<SplitPanel>();
      this.autoSizeTargets = new List<SplitPanel>();
      this.absoluteSizeTargets = new List<SplitPanel>();
    }

    public void Reset()
    {
      this.fillPanel = (SplitPanel) null;
      this.layoutTargets.Clear();
      this.autoSizeTargets.Clear();
      this.absoluteSizeTargets.Clear();
      this.totalMeasuredLength = 0;
      this.totalMinLength = 0;
      this.autoSizeCountFactor = 0.0f;
      this.availableLength = 0;
      this.contentRect = Rectangle.Empty;
      this.autoSizeLength = 0;
      this.absoluteSizeLength = 0;
      this.totalSplitterLength = 0;
    }

    public List<SplitPanel> LayoutTargets
    {
      get
      {
        return this.layoutTargets;
      }
    }

    public List<SplitPanel> AutoSizeTargets
    {
      get
      {
        return this.layoutTargets;
      }
    }

    public float AutoSizeCountFactor
    {
      get
      {
        return this.autoSizeCountFactor;
      }
      set
      {
        this.autoSizeCountFactor = value;
      }
    }

    public int AvailableLength
    {
      get
      {
        return this.availableLength;
      }
      set
      {
        this.availableLength = value;
      }
    }

    public int AutoSizeLength
    {
      get
      {
        return this.autoSizeLength;
      }
      set
      {
        this.autoSizeLength = value;
      }
    }

    public int AbsoluteSizeLength
    {
      get
      {
        return this.absoluteSizeLength;
      }
      set
      {
        this.absoluteSizeLength = value;
      }
    }

    public int SplitterLength
    {
      get
      {
        return this.splitterLength;
      }
      set
      {
        this.splitterLength = value;
      }
    }

    public int TotalSplitterLength
    {
      get
      {
        return this.totalSplitterLength;
      }
      set
      {
        this.totalSplitterLength = value;
      }
    }

    public Rectangle ContentRect
    {
      get
      {
        return this.contentRect;
      }
      set
      {
        this.contentRect = value;
      }
    }

    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        this.orientation = value;
      }
    }
  }
}
