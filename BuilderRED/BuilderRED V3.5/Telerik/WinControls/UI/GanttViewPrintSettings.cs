// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewPrintSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class GanttViewPrintSettings : IDisposable
  {
    private GanttPrintDirection printDirection;
    private bool timelineVisibleOnEveryPage;
    private bool printTextViewPart;
    private Color linksColor;
    private Color summaryTaskFill;
    private Color summaryTaskBorder;
    private Color milestoneTaskFill;
    private Color milestoneTaskBorder;
    private Color taskFill;
    private Color taskBorder;
    private Color taskForeColor;
    private Font taskFont;
    private Color dataCellFill;
    private Color dataCellForeColor;
    private Color dataCellBorder;
    private Font dataCellFont;
    private Color headerCellBorder;
    private Color headerCellForeColor;
    private Color headerCellFill;
    private Font headerCellFont;
    private Color timelineTopRowFill;
    private Color timelineTopRowBorder;
    private Color timelineTopRowForeColor;
    private Font timelineTopRowFont;
    private Color timelineBottomRowFill;
    private Color timelineBottomRowBorder;
    private Color timelineBottomRowForeColor;
    private Font timelineBottomRowFont;

    public GanttViewPrintSettings()
    {
      this.printDirection = GanttPrintDirection.ColumnMajorOrder;
      this.timelineVisibleOnEveryPage = true;
      this.printTextViewPart = true;
      this.linksColor = Color.Black;
      this.summaryTaskFill = Color.LightBlue;
      this.summaryTaskBorder = Color.LightBlue;
      this.milestoneTaskFill = Color.Pink;
      this.milestoneTaskBorder = Color.Pink;
      this.taskFill = Color.White;
      this.taskBorder = Color.Black;
      this.taskForeColor = Color.Black;
      this.dataCellFill = Color.White;
      this.dataCellForeColor = Color.Black;
      this.dataCellBorder = Color.Black;
      this.headerCellBorder = Color.Black;
      this.headerCellForeColor = Color.Black;
      this.headerCellFill = Color.White;
      this.timelineTopRowFill = Color.White;
      this.timelineTopRowBorder = Color.Black;
      this.timelineTopRowForeColor = Color.Black;
      this.timelineBottomRowFill = Color.White;
      this.timelineBottomRowBorder = Color.Black;
      this.timelineBottomRowForeColor = Color.Black;
    }

    [DefaultValue(typeof (Color), "White")]
    public Color TimelineTopRowFill
    {
      get
      {
        return this.timelineTopRowFill;
      }
      set
      {
        this.timelineTopRowFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TimelineTopRowBorder
    {
      get
      {
        return this.timelineTopRowBorder;
      }
      set
      {
        this.timelineTopRowBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TimelineTopRowForeColor
    {
      get
      {
        return this.timelineTopRowForeColor;
      }
      set
      {
        this.timelineTopRowForeColor = value;
      }
    }

    [DefaultValue(null)]
    public Font TimelineTopRowFont
    {
      get
      {
        return this.timelineTopRowFont;
      }
      set
      {
        this.timelineTopRowFont = value;
      }
    }

    [DefaultValue(typeof (Color), "White")]
    public Color TimelineBottomRowFill
    {
      get
      {
        return this.timelineBottomRowFill;
      }
      set
      {
        this.timelineBottomRowFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TimelineBottomRowBorder
    {
      get
      {
        return this.timelineBottomRowBorder;
      }
      set
      {
        this.timelineBottomRowBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TimelineBottomRowForeColor
    {
      get
      {
        return this.timelineBottomRowForeColor;
      }
      set
      {
        this.timelineBottomRowForeColor = value;
      }
    }

    [DefaultValue(null)]
    public Font TimelineBottomRowFont
    {
      get
      {
        return this.timelineBottomRowFont;
      }
      set
      {
        this.timelineBottomRowFont = value;
      }
    }

    [DefaultValue(typeof (Color), "White")]
    public Color HeaderCellFill
    {
      get
      {
        return this.headerCellFill;
      }
      set
      {
        this.headerCellFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color HeaderCellBorder
    {
      get
      {
        return this.headerCellBorder;
      }
      set
      {
        this.headerCellBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color HeaderCellForeColor
    {
      get
      {
        return this.headerCellForeColor;
      }
      set
      {
        this.headerCellForeColor = value;
      }
    }

    [DefaultValue(null)]
    public Font HeaderCellFont
    {
      get
      {
        return this.headerCellFont;
      }
      set
      {
        this.headerCellFont = value;
      }
    }

    [DefaultValue(typeof (Color), "White")]
    public Color DataCellFill
    {
      get
      {
        return this.dataCellFill;
      }
      set
      {
        this.dataCellFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color DataCellForeColor
    {
      get
      {
        return this.dataCellForeColor;
      }
      set
      {
        this.dataCellForeColor = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color DataCellBorder
    {
      get
      {
        return this.dataCellBorder;
      }
      set
      {
        this.dataCellBorder = value;
      }
    }

    [DefaultValue(null)]
    public Font DataCellFont
    {
      get
      {
        return this.dataCellFont;
      }
      set
      {
        this.dataCellFont = value;
      }
    }

    [DefaultValue(typeof (Color), "White")]
    public Color TaskFill
    {
      get
      {
        return this.taskFill;
      }
      set
      {
        this.taskFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TaskBorder
    {
      get
      {
        return this.taskBorder;
      }
      set
      {
        this.taskBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color TaskForeColor
    {
      get
      {
        return this.taskForeColor;
      }
      set
      {
        this.taskForeColor = value;
      }
    }

    [DefaultValue(null)]
    public Font TaskFont
    {
      get
      {
        return this.taskFont;
      }
      set
      {
        this.taskFont = value;
      }
    }

    [DefaultValue(typeof (Color), "LightBlue")]
    public Color SummaryTaskFill
    {
      get
      {
        return this.summaryTaskFill;
      }
      set
      {
        this.summaryTaskFill = value;
      }
    }

    [DefaultValue(typeof (Color), "LightBlue")]
    public Color SummaryTaskBorder
    {
      get
      {
        return this.summaryTaskBorder;
      }
      set
      {
        this.summaryTaskBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Pink")]
    public Color MilestoneTaskFill
    {
      get
      {
        return this.milestoneTaskFill;
      }
      set
      {
        this.milestoneTaskFill = value;
      }
    }

    [DefaultValue(typeof (Color), "Pink")]
    public Color MilestoneTaskBorder
    {
      get
      {
        return this.milestoneTaskBorder;
      }
      set
      {
        this.milestoneTaskBorder = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    public Color LinksColor
    {
      get
      {
        return this.linksColor;
      }
      set
      {
        this.linksColor = value;
      }
    }

    [DefaultValue(true)]
    public bool PrintTextViewPart
    {
      get
      {
        return this.printTextViewPart;
      }
      set
      {
        this.printTextViewPart = value;
      }
    }

    [DefaultValue(true)]
    public bool TimelineVisibleOnEveryPage
    {
      get
      {
        return this.timelineVisibleOnEveryPage;
      }
      set
      {
        this.timelineVisibleOnEveryPage = value;
      }
    }

    [DefaultValue(GanttPrintDirection.ColumnMajorOrder)]
    public GanttPrintDirection PrintDirection
    {
      get
      {
        return this.printDirection;
      }
      set
      {
        this.printDirection = value;
      }
    }

    public void Dispose()
    {
      this.TaskFont = (Font) null;
      this.DataCellFont = (Font) null;
      this.HeaderCellFont = (Font) null;
      this.TimelineTopRowFont = (Font) null;
      this.TimelineBottomRowFont = (Font) null;
    }
  }
}
