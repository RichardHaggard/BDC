// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class CalendarView : INotifyPropertyChanged
  {
    private System.Windows.Forms.VisualStyles.ContentAlignment titleAlign = System.Windows.Forms.VisualStyles.ContentAlignment.Center;
    private Orientation? orientation = new Orientation?();
    private string titleContent = string.Empty;
    private string name = string.Empty;
    private int depth = -1;
    private bool? readOnly = new bool?();
    private string conditionsErrorMessage = string.Empty;
    private DateTime viewRenderEndDate = DateTime.MinValue;
    private DateTime viewRenderStartDate = DateTime.MinValue;
    private DateTime viewEndDate = DateTime.MinValue;
    private DateTime viewStartDate = DateTime.MinValue;
    private string viewSelectorText = string.Empty;
    private string columnHeaderText = "";
    private string rowHeaderText = "";
    private string titleFormat = string.Empty;
    private int columns = 1;
    private int rows = 1;
    private DateTime currentViewEndDate = DateTime.MinValue;
    private DateTime currentViewBeginDate = DateTime.MinValue;
    private Rectangle bounds = Rectangle.Empty;
    private bool? allowFishEye = new bool?(true);
    private Padding? cellMargin;
    private Padding? cellPadding;
    private RadCalendar calendar;
    private CalendarView parent;
    private CalendarViewCollection children;
    private bool visible;
    private bool? showHeader;
    private bool? showSelector;
    private bool? showColumnHeaders;
    private bool? showRowHeaders;
    private bool? allowRowHeaderSelectors;
    private bool? allowColumnHeaderSelectors;
    private bool? allowViewSelector;
    private bool isMultiView;
    private string cellToolTipFormat;
    private Image viewSelectorImage;
    private Image columnHeaderImage;
    private Image rowHeaderImage;
    private MonthLayout? monthLayout;
    private int? headerWidth;
    private int? headerHeight;
    private float? zoomFactor;
    private bool? showOtherMonthDays;
    private int? cellVerticalSpacing;
    private int? cellHorizontalSpacing;
    private int? multiViewRows;
    private int? multiViewColumns;

    internal CalendarView(RadCalendar parent)
      : this(parent, (CalendarView) null)
    {
    }

    internal CalendarView(RadCalendar parent, CalendarView parentView)
      : this(parent, parentView, false, parent != null ? parent.Rows : 6, parent != null ? parent.Columns : 7)
    {
    }

    internal CalendarView(
      RadCalendar parent,
      CalendarView parentView,
      bool isMultiView,
      int rows,
      int columns)
    {
      this.calendar = parent;
      this.parent = parentView;
      this.isMultiView = isMultiView;
      this.rows = rows;
      this.columns = columns;
      if ((this.calendar.MultiViewColumns > 1 || this.calendar.MultiViewRows > 1) && (object) this.GetType() == (object) typeof (MultiMonthView))
      {
        this.Calendar.PropertyChanged += new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
      }
      else
      {
        if (this.calendar.MultiViewColumns != 1 || this.calendar.MultiViewRows != 1)
          return;
        this.Calendar.PropertyChanged += new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
      }
    }

    public void Dispose()
    {
      this.calendar = (RadCalendar) null;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(null)]
    [Description("Gets the parent calendar that the current view is assigned to.")]
    public RadCalendar Calendar
    {
      get
      {
        if (this.parent != null)
          return this.parent.Calendar;
        return this.calendar;
      }
      internal set
      {
        if (this.calendar == value)
          return;
        this.calendar = value;
        this.OnNotifyPropertyChanged(nameof (Calendar));
      }
    }

    [Browsable(false)]
    [Description("Gets the parent tree node of the current tree node.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual CalendarView Parent
    {
      get
      {
        return this.parent;
      }
      internal set
      {
        if (this.parent == value)
          return;
        this.parent = value;
        this.OnNotifyPropertyChanged(nameof (Parent));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("Gets the collection of nodes that are assigned to the tree view control.")]
    public virtual CalendarViewCollection Children
    {
      get
      {
        this.EnsureChildViews();
        return this.children;
      }
    }

    [Description("Gets or sets the name of the node.")]
    [Category("Appearance")]
    [DefaultValue("")]
    public virtual string Name
    {
      get
      {
        if (string.IsNullOrEmpty(this.name))
          return string.Empty;
        return this.name;
      }
      set
      {
        if (!(this.name != value))
          return;
        this.name = value;
        this.OnNotifyPropertyChanged(nameof (Name));
      }
    }

    [DefaultValue("dddd, MMMM dd, yyyy")]
    [NotifyParentProperty(true)]
    [Category("Title Settings")]
    [Localizable(true)]
    [Description("Gets or sets the format string that is applied to the days cells tooltip.")]
    public string CellToolTipFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.cellToolTipFormat))
          return "dddd, MMMM dd, yyyy";
        return this.cellToolTipFormat;
      }
      set
      {
        if (!(this.cellToolTipFormat != value))
          return;
        this.cellToolTipFormat = value;
        this.OnNotifyPropertyChanged(nameof (CellToolTipFormat));
      }
    }

    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [DefaultValue(Orientation.Horizontal)]
    [Description("Gets or sets the orientation (rendering direction) of the calendar component.")]
    public Orientation Orientation
    {
      get
      {
        if (this.orientation.HasValue)
          return this.orientation.Value;
        if (this.Calendar != null)
          return this.Calendar.Orientation;
        return Orientation.Horizontal;
      }
      set
      {
        Orientation? orientation1 = this.orientation;
        Orientation orientation2 = value;
        if ((orientation1.GetValueOrDefault() != orientation2 ? 1 : (!orientation1.HasValue ? 1 : 0)) == 0)
          return;
        this.orientation = new Orientation?(value);
        this.OnNotifyPropertyChanged(nameof (Orientation));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Title Settings")]
    [DefaultValue(System.Windows.Forms.VisualStyles.ContentAlignment.Center)]
    [Description("Gets or sets the horizontal alignment of the view title.")]
    public virtual System.Windows.Forms.VisualStyles.ContentAlignment TitleAlign
    {
      get
      {
        if (this.titleAlign != System.Windows.Forms.VisualStyles.ContentAlignment.Center)
          return this.titleAlign;
        if (this.Parent != null)
          return this.Parent.TitleAlign;
        if (this.Calendar != null)
          return this.Calendar.TitleAlign;
        return this.titleAlign;
      }
      set
      {
        if (this.titleAlign == value)
          return;
        this.titleAlign = value;
        this.OnNotifyPropertyChanged(nameof (TitleAlign));
      }
    }

    [DefaultValue(true)]
    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        if (this.visible == value)
          return;
        this.visible = value;
        this.OnNotifyPropertyChanged(nameof (Visible));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the root parent node for this instance.")]
    [DefaultValue(null)]
    public CalendarView RootView
    {
      get
      {
        CalendarView calendarView = this;
        while (calendarView.Parent != null)
          calendarView = calendarView.Parent;
        return calendarView;
      }
    }

    [Browsable(false)]
    public virtual bool IsRootView
    {
      get
      {
        return this.Parent == null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(-1)]
    public int Level
    {
      get
      {
        if (this.parent != null)
          return this.parent.depth + 1;
        return this.calendar != null ? 0 : -1;
      }
    }

    [Description("Gets or sets a value indicating whether the calendar view is in read-only mode.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public virtual bool ReadOnly
    {
      get
      {
        if (this.readOnly.HasValue)
          return this.readOnly.Value;
        if (this.Calendar != null)
          return this.Calendar.ReadOnly;
        return false;
      }
      set
      {
        bool? nullable = this.readOnly;
        bool flag = value;
        if ((nullable.GetValueOrDefault() != flag ? 1 : (!nullable.HasValue ? 1 : 0)) == 0)
          return;
        this.readOnly = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    [Description("The text displayed in the view selector cell.")]
    [Bindable(false)]
    [Category("Header Settings")]
    [DefaultValue("")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public string ViewSelectorText
    {
      get
      {
        if (!string.IsNullOrEmpty(this.viewSelectorText))
          return this.viewSelectorText;
        if (this.Calendar != null)
          return this.Calendar.ViewSelectorText;
        return "";
      }
      set
      {
        if (!(this.viewSelectorText != value))
          return;
        this.viewSelectorText = value;
        this.OnNotifyPropertyChanged(nameof (ViewSelectorText));
      }
    }

    [Bindable(false)]
    [Localizable(true)]
    [Category("Header Settings")]
    [DefaultValue("")]
    [Description("Provides custom text for the row header cells.")]
    [NotifyParentProperty(true)]
    public string RowHeaderText
    {
      get
      {
        if (this.rowHeaderText != "")
          return this.rowHeaderText;
        if (this.Calendar != null)
          return this.Calendar.RowHeaderText;
        return "";
      }
      set
      {
        if (!(this.rowHeaderText != value))
          return;
        this.rowHeaderText = value;
        this.OnNotifyPropertyChanged(nameof (RowHeaderText));
      }
    }

    [Localizable(true)]
    [Bindable(false)]
    [Category("Header Settings")]
    [DefaultValue("")]
    [Description("Provides custom text for the column header cells.")]
    [NotifyParentProperty(true)]
    public string ColumnHeaderText
    {
      get
      {
        if (this.columnHeaderText != "")
          return this.columnHeaderText;
        if (this.Calendar != null)
          return this.Calendar.ColumnHeaderText;
        return "";
      }
      set
      {
        if (!(this.columnHeaderText != value))
          return;
        this.columnHeaderText = value;
        this.OnNotifyPropertyChanged(nameof (ColumnHeaderText));
      }
    }

    [DefaultValue("")]
    [Category("Header Settings")]
    [Bindable(false)]
    [Description("The image displayed for the column header cells.")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public Image ColumnHeaderImage
    {
      get
      {
        if (this.columnHeaderImage != null)
          return this.columnHeaderImage;
        if (this.Calendar != null)
          return this.Calendar.ColumnHeaderImage;
        return (Image) null;
      }
      set
      {
        if (this.columnHeaderImage == value)
          return;
        this.columnHeaderImage = value;
        this.OnNotifyPropertyChanged(nameof (ColumnHeaderImage));
      }
    }

    [Category("Header Settings")]
    [DefaultValue("")]
    [Description("The image displayed for the <strong>CalendarView</strong> row header element.")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public Image RowHeaderImage
    {
      get
      {
        if (this.rowHeaderImage != null)
          return this.rowHeaderImage;
        if (this.Calendar != null)
          return this.Calendar.RowHeaderImage;
        return (Image) null;
      }
      set
      {
        if (this.rowHeaderImage == value)
          return;
        this.rowHeaderImage = value;
        this.OnNotifyPropertyChanged(nameof (RowHeaderImage));
      }
    }

    [Description("Gets or sets the margin of the view cells")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public Padding CellMargin
    {
      get
      {
        if (this.cellMargin.HasValue)
          return this.cellMargin.Value;
        if (this.Calendar != null)
          return this.Calendar.CellMargin;
        return Padding.Empty;
      }
      set
      {
        Padding? cellMargin = this.cellMargin;
        Padding padding = value;
        if ((!cellMargin.HasValue ? 1 : (cellMargin.GetValueOrDefault() != padding ? 1 : 0)) == 0)
          return;
        this.cellMargin = new Padding?(value);
        this.OnNotifyPropertyChanged(nameof (CellMargin));
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the margin of the view cells")]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public Padding CellPadding
    {
      get
      {
        if (this.cellPadding.HasValue)
          return this.cellPadding.Value;
        if (this.Calendar != null)
          return this.Calendar.CellPadding;
        return Padding.Empty;
      }
      set
      {
        Padding? cellPadding = this.cellPadding;
        Padding padding = value;
        if ((!cellPadding.HasValue ? 1 : (cellPadding.GetValueOrDefault() != padding ? 1 : 0)) == 0)
          return;
        this.cellPadding = new Padding?(value);
        this.OnNotifyPropertyChanged(nameof (CellPadding));
      }
    }

    [Category("Header Settings")]
    [Bindable(false)]
    [DefaultValue("")]
    [Description("The image displayed in the view selector cell.")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public Image ViewSelectorImage
    {
      get
      {
        if (this.viewSelectorImage != null)
          return this.viewSelectorImage;
        if (this.Calendar != null)
          return this.Calendar.ViewSelectorImage;
        return (Image) null;
      }
      set
      {
        if (this.viewSelectorImage == value)
          return;
        this.viewSelectorImage = value;
        this.OnNotifyPropertyChanged(nameof (ViewSelectorImage));
      }
    }

    [Category("MonthView Specific Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue(true)]
    [Description("Gets or sets whether the month matrix, when rendered will show days from other (previous or next) months or will render only blank cells.")]
    public virtual bool ShowOtherMonthsDays
    {
      get
      {
        if (this.showOtherMonthDays.HasValue)
          return this.showOtherMonthDays.Value;
        if (this.Calendar != null)
          return this.Calendar.ShowOtherMonthsDays;
        return false;
      }
      set
      {
        bool? showOtherMonthDays = this.showOtherMonthDays;
        bool flag = value;
        if ((showOtherMonthDays.GetValueOrDefault() != flag ? 1 : (!showOtherMonthDays.HasValue ? 1 : 0)) == 0)
          return;
        this.showOtherMonthDays = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (ShowOtherMonthsDays));
      }
    }

    [NotifyParentProperty(true)]
    [Description("Gets or sets whether the fish eye functionality is enabled ")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool AllowFishEye
    {
      get
      {
        if (this.allowFishEye.HasValue)
          return this.allowFishEye.Value;
        if (this.Calendar != null)
          return this.Calendar.AllowFishEye;
        return false;
      }
      set
      {
        bool? allowFishEye = this.allowFishEye;
        bool flag = value;
        if ((allowFishEye.GetValueOrDefault() != flag ? 1 : (!allowFishEye.HasValue ? 1 : 0)) == 0)
          return;
        this.allowFishEye = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowFishEye));
      }
    }

    [DefaultValue(1.3)]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the zooming factor of a cell which is handled by the fish eye functionality")]
    [Category("Behavior")]
    public virtual float ZoomFactor
    {
      get
      {
        if (this.zoomFactor.HasValue)
          return this.zoomFactor.Value;
        if (this.Calendar != null)
          return this.Calendar.ZoomFactor;
        return 1.3f;
      }
      set
      {
        float? zoomFactor = this.zoomFactor;
        float num = value;
        if (((double) zoomFactor.GetValueOrDefault() != (double) num ? 1 : (!zoomFactor.HasValue ? 1 : 0)) == 0)
          return;
        this.zoomFactor = new float?(value);
        this.OnNotifyPropertyChanged(nameof (ZoomFactor));
      }
    }

    [Category("Month View Settings")]
    [NotifyParentProperty(true)]
    [Description("This property allows using presets, regarding the layout of the view area. Sets or gets predefined pairs of rows and columns, so that the product of the two values is exactly 42, which guarantees valid view layout.")]
    [DefaultValue(MonthLayout.Layout_7columns_x_6rows)]
    public virtual MonthLayout MonthLayout
    {
      get
      {
        if (this.monthLayout.HasValue)
          return this.monthLayout.Value;
        if (this.Calendar != null)
          return this.Calendar.MonthLayout;
        return MonthLayout.Layout_7columns_x_6rows;
      }
      set
      {
        MonthLayout? monthLayout1 = this.monthLayout;
        MonthLayout monthLayout2 = value;
        if ((monthLayout1.GetValueOrDefault() != monthLayout2 ? 1 : (!monthLayout1.HasValue ? 1 : 0)) == 0)
          return;
        this.monthLayout = new MonthLayout?(value);
        this.OnNotifyPropertyChanged(nameof (MonthLayout));
      }
    }

    [NotifyParentProperty(true)]
    [Description("The Width applied to a Header")]
    [Category("Behavior")]
    [DefaultValue(0)]
    public virtual int HeaderWidth
    {
      get
      {
        if (this.headerWidth.HasValue)
          return this.headerWidth.Value;
        if (this.Calendar != null)
          return this.Calendar.HeaderWidth;
        return 15;
      }
      set
      {
        int? headerWidth = this.headerWidth;
        int num = value;
        if ((headerWidth.GetValueOrDefault() != num ? 1 : (!headerWidth.HasValue ? 1 : 0)) == 0 || value < 15)
          return;
        this.headerWidth = new int?(value);
        this.OnNotifyPropertyChanged(nameof (HeaderWidth));
      }
    }

    [DefaultValue(0)]
    [Description("The Height applied to a Header")]
    [NotifyParentProperty(true)]
    [Category("Behavior")]
    public virtual int HeaderHeight
    {
      get
      {
        if (this.headerHeight.HasValue)
          return this.headerHeight.Value;
        if (this.Calendar != null)
          return this.Calendar.HeaderHeight;
        return 15;
      }
      set
      {
        int? headerHeight = this.headerHeight;
        int num = value;
        if ((headerHeight.GetValueOrDefault() != num ? 1 : (!headerHeight.HasValue ? 1 : 0)) == 0 || value < 15)
          return;
        this.headerHeight = new int?(value);
        this.OnNotifyPropertyChanged(nameof (HeaderHeight));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Header Settings")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a single CalendarView object will display a selector.")]
    public virtual bool ShowSelector
    {
      get
      {
        if (this.showSelector.HasValue)
          return this.showSelector.Value;
        if (this.Calendar != null)
          return this.Calendar.ShowViewSelector;
        return false;
      }
      set
      {
        bool? showSelector = this.showSelector;
        bool flag = value;
        if ((showSelector.GetValueOrDefault() != flag ? 1 : (!showSelector.HasValue ? 1 : 0)) == 0)
          return;
        this.showSelector = new bool?(value);
        this.OnNotifyPropertyChanged("ShowViewSelector");
      }
    }

    [Category("General View Settings")]
    [Description("Gets or sets the number of month rows in a multi view calendar.")]
    [NotifyParentProperty(true)]
    [DefaultValue(1)]
    public int MultiViewRows
    {
      get
      {
        if (this.multiViewRows.HasValue)
          return this.multiViewRows.Value;
        if (this.Calendar != null)
          return this.Calendar.MultiViewRows;
        return 1;
      }
      set
      {
        int? multiViewRows = this.multiViewRows;
        int num = value;
        if ((multiViewRows.GetValueOrDefault() != num ? 1 : (!multiViewRows.HasValue ? 1 : 0)) == 0)
          return;
        this.multiViewRows = new int?(value);
        this.OnNotifyPropertyChanged(nameof (MultiViewRows));
      }
    }

    [Category("General View Settings")]
    [DefaultValue(1)]
    [Description("Gets or sets the number of month columns in a multi view calendar.")]
    [NotifyParentProperty(true)]
    public int MultiViewColumns
    {
      get
      {
        if (this.multiViewColumns.HasValue)
          return this.multiViewColumns.Value;
        if (this.Calendar != null)
          return this.Calendar.MultiViewColumns;
        return 1;
      }
      set
      {
        int? multiViewColumns = this.multiViewColumns;
        int num = value;
        if ((multiViewColumns.GetValueOrDefault() != num ? 1 : (!multiViewColumns.HasValue ? 1 : 0)) == 0)
          return;
        this.multiViewColumns = new int?(value);
        this.OnNotifyPropertyChanged(nameof (MultiViewColumns));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Header Settings")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a single CalendarView object will display a header row.")]
    public virtual bool ShowHeader
    {
      get
      {
        if (this.showHeader.HasValue)
          return this.showHeader.Value;
        if (this.Calendar != null)
          return this.Calendar.ShowViewHeader;
        return false;
      }
      set
      {
        bool? showHeader = this.showHeader;
        bool flag = value;
        if ((showHeader.GetValueOrDefault() != flag ? 1 : (!showHeader.HasValue ? 1 : 0)) == 0)
          return;
        this.showHeader = new bool?(value);
        this.OnNotifyPropertyChanged("ShowViewHeader");
      }
    }

    [DefaultValue("")]
    [Category("Header Settings")]
    [Description("Gets or sets the format string used to format the text inside the header row.")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public virtual string TitleFormat
    {
      get
      {
        if (!string.IsNullOrEmpty(this.titleFormat))
          return this.titleFormat;
        if (this.Calendar != null && !string.IsNullOrEmpty(this.Calendar.TitleFormat))
          return this.Calendar.TitleFormat;
        return (string) null;
      }
      set
      {
        if (!(this.titleFormat != value))
          return;
        this.titleFormat = value;
        this.OnNotifyPropertyChanged(nameof (TitleFormat));
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(false)]
    [Description("Gets or sets whether a CalendarView object will display a header row.")]
    [Category("Header Settings")]
    public virtual bool ShowColumnHeaders
    {
      get
      {
        if (this.showColumnHeaders.HasValue)
          return this.showColumnHeaders.Value;
        if (this.Calendar != null)
          return this.Calendar.ShowColumnHeaders;
        return false;
      }
      set
      {
        bool? showColumnHeaders = this.showColumnHeaders;
        bool flag = value;
        if ((showColumnHeaders.GetValueOrDefault() != flag ? 1 : (!showColumnHeaders.HasValue ? 1 : 0)) == 0)
          return;
        this.showColumnHeaders = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (ShowColumnHeaders));
      }
    }

    [Description("Gets or sets whether a CalendarView object will display a header column.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    public virtual bool ShowRowHeaders
    {
      get
      {
        if (this.showRowHeaders.HasValue)
          return this.showRowHeaders.Value;
        if (this.Calendar != null)
          return this.Calendar.ShowRowHeaders;
        return false;
      }
      set
      {
        bool? showRowHeaders = this.showRowHeaders;
        bool flag = value;
        if ((showRowHeaders.GetValueOrDefault() != flag ? 1 : (!showRowHeaders.HasValue ? 1 : 0)) == 0)
          return;
        this.showRowHeaders = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (ShowRowHeaders));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether row headers ( if displayed by a MonthView object) will act as row selectors.")]
    [NotifyParentProperty(true)]
    public virtual bool AllowRowHeaderSelectors
    {
      get
      {
        if (this.allowRowHeaderSelectors.HasValue)
          return this.allowRowHeaderSelectors.Value;
        if (this.Calendar != null)
          return this.Calendar.AllowRowHeaderSelectors;
        return false;
      }
      set
      {
        bool? rowHeaderSelectors = this.allowRowHeaderSelectors;
        bool flag = value;
        if ((rowHeaderSelectors.GetValueOrDefault() != flag ? 1 : (!rowHeaderSelectors.HasValue ? 1 : 0)) == 0)
          return;
        this.allowRowHeaderSelectors = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowRowHeaderSelectors));
      }
    }

    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [DefaultValue(false)]
    [Description("Gets or sets whether column headers ( if displayed by a MonthView object) will act as column selectors.")]
    public virtual bool AllowColumnHeaderSelectors
    {
      get
      {
        if (this.allowColumnHeaderSelectors.HasValue)
          return this.allowColumnHeaderSelectors.Value;
        if (this.Calendar != null)
          return this.Calendar.AllowColumnHeaderSelectors;
        return false;
      }
      set
      {
        bool? columnHeaderSelectors = this.allowColumnHeaderSelectors;
        bool flag = value;
        if ((columnHeaderSelectors.GetValueOrDefault() != flag ? 1 : (!columnHeaderSelectors.HasValue ? 1 : 0)) == 0)
          return;
        this.allowColumnHeaderSelectors = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowColumnHeaderSelectors));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a selector for the entire CalendarView (MonthView) will appear on the calendar.")]
    public virtual bool AllowViewSelector
    {
      get
      {
        if (this.allowViewSelector.HasValue)
          return this.allowViewSelector.Value;
        if (this.Calendar != null)
          return this.Calendar.AllowViewSelector;
        return false;
      }
      set
      {
        bool? allowViewSelector = this.allowViewSelector;
        bool flag = value;
        if ((allowViewSelector.GetValueOrDefault() != flag ? 1 : (!allowViewSelector.HasValue ? 1 : 0)) == 0)
          return;
        this.allowViewSelector = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowViewSelector));
      }
    }

    public virtual bool IsMultipleView
    {
      get
      {
        return this.isMultiView;
      }
    }

    protected internal virtual DateTime ViewRenderStartDate
    {
      get
      {
        return this.viewRenderStartDate;
      }
      set
      {
        if (!(this.viewRenderStartDate != value))
          return;
        this.viewRenderStartDate = value;
        this.OnNotifyPropertyChanged(nameof (ViewRenderStartDate));
      }
    }

    protected internal virtual DateTime ViewRenderEndDate
    {
      get
      {
        return this.viewRenderEndDate;
      }
      set
      {
        if (!(this.viewRenderEndDate != value))
          return;
        this.viewRenderEndDate = value;
        this.OnNotifyPropertyChanged(nameof (ViewRenderEndDate));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Data")]
    [DefaultValue(typeof (DateTime), "1/1/1980")]
    [Description("Gets or sets a DateTime value specifying the starting date for the period handled by a CalendarView instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual DateTime ViewStartDate
    {
      get
      {
        return this.viewStartDate;
      }
      set
      {
        if (!(this.viewStartDate != value))
          return;
        this.viewStartDate = value;
        this.OnNotifyPropertyChanged(nameof (ViewStartDate));
      }
    }

    [Category("Data")]
    [DefaultValue(typeof (DateTime), "2/1/1980")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets a DateTime value specifying the ending date for the period handled by a CalendarView instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual DateTime ViewEndDate
    {
      get
      {
        return this.viewEndDate;
      }
      set
      {
        if (!(this.viewEndDate != value))
          return;
        this.viewEndDate = value;
        this.OnNotifyPropertyChanged(nameof (ViewEndDate));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
      set
      {
        if (!(this.bounds != value))
          return;
        this.bounds = value;
        this.OnNotifyPropertyChanged(nameof (Bounds));
      }
    }

    [NotifyParentProperty(true)]
    [Description("The the count of rows to be displayed by a CalendarView")]
    [Category("General View Settings")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Rows
    {
      get
      {
        return this.rows;
      }
      set
      {
        this.rows = value;
      }
    }

    [Category("General View Settings")]
    [NotifyParentProperty(true)]
    [Description("The the count of columns to be displayed by a CalendarView")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Columns
    {
      get
      {
        return this.columns;
      }
      set
      {
        this.columns = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual CalendarView PreviousView
    {
      get
      {
        CalendarViewCollection calendarViewCollection = (CalendarViewCollection) null;
        if (this.Parent != null)
          calendarViewCollection = this.Parent.Children;
        if (calendarViewCollection != null)
        {
          int index = calendarViewCollection.IndexOf(this) - 1;
          if (index > -1)
            return calendarViewCollection[index];
        }
        return (CalendarView) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual CalendarView NextView
    {
      get
      {
        CalendarViewCollection calendarViewCollection = (CalendarViewCollection) null;
        if (this.Parent != null)
          calendarViewCollection = this.Parent.Children;
        if (calendarViewCollection != null)
        {
          int index = calendarViewCollection.IndexOf(this) + 1;
          if (index < calendarViewCollection.Count)
            return calendarViewCollection[index];
        }
        return (CalendarView) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Localization Settings")]
    [Description("Gets the default System.Globalization.Calendar instance as speified by the default culture.")]
    [NotifyParentProperty(true)]
    public System.Globalization.Calendar CurrentCalendar
    {
      get
      {
        if (this.Parent != null)
          return this.Parent.CurrentCalendar;
        if (this.Calendar != null && this.Calendar.DateTimeFormat.Calendar != null)
          return this.Calendar.DateTimeFormat.Calendar;
        return DateTimeFormatInfo.CurrentInfo.Calendar;
      }
    }

    [DefaultValue(1)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the vertical spacing between the calendar cells")]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int CellVerticalSpacing
    {
      get
      {
        if (this.cellVerticalSpacing.HasValue)
          return this.cellVerticalSpacing.Value;
        if (this.Calendar != null)
          return this.Calendar.CellVerticalSpacing;
        return 1;
      }
      set
      {
        int? cellVerticalSpacing = this.cellVerticalSpacing;
        int num = value;
        if ((cellVerticalSpacing.GetValueOrDefault() != num ? 1 : (!cellVerticalSpacing.HasValue ? 1 : 0)) == 0)
          return;
        this.cellVerticalSpacing = new int?(value);
        this.OnNotifyPropertyChanged(nameof (CellVerticalSpacing));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [DefaultValue(1)]
    [Description("Gets or sets the horizontal spacing between the calendar cells")]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int CellHorizontalSpacing
    {
      get
      {
        if (this.cellHorizontalSpacing.HasValue)
          return this.cellHorizontalSpacing.Value;
        if (this.Calendar != null)
          return this.Calendar.CellHorizontalSpacing;
        return 1;
      }
      set
      {
        int? horizontalSpacing = this.cellHorizontalSpacing;
        int num = value;
        if ((horizontalSpacing.GetValueOrDefault() != num ? 1 : (!horizontalSpacing.HasValue ? 1 : 0)) == 0)
          return;
        this.cellHorizontalSpacing = new int?(value);
        this.OnNotifyPropertyChanged(nameof (CellHorizontalSpacing));
      }
    }

    public bool IsDateInView(DateTime date)
    {
      if (date >= this.ViewStartDate)
        return this.ViewEndDate >= date;
      return false;
    }

    public virtual void Select(DateTime date)
    {
      if (this.Calendar == null || this.Calendar.SelectedDates.Contains(date))
        return;
      this.Calendar.SelectedDates.Add(date);
    }

    public virtual void SelectRange(DateTime[] dates)
    {
      for (int index = 0; index < dates.Length; ++index)
        this.Select(dates[index]);
    }

    public virtual void SelectRange(DateTime startDate, DateTime endDate)
    {
      DateTime date = startDate;
      while (date < endDate)
      {
        this.Select(date);
        date.AddDays(1.0);
      }
    }

    protected virtual DateTime GetPreviousViewDate()
    {
      return this.AddViewPeriods(this.ViewStartDate, -1);
    }

    protected virtual DateTime GetNextViewDate()
    {
      return this.AddViewPeriods(this.ViewStartDate, 1);
    }

    protected virtual DateTime AddViewPeriods(DateTime startDate, int periods)
    {
      return DateTime.MinValue;
    }

    protected virtual void EnsureChildViews()
    {
      if (this.children != null)
        return;
      this.children = new CalendarViewCollection(this);
    }

    protected virtual void SetDateRange()
    {
    }

    protected virtual void HandlePageDownKey(Keys keys)
    {
    }

    protected virtual void HandlePageUpKey(Keys keys)
    {
    }

    protected virtual void HandleDownKey(Keys keys)
    {
    }

    protected virtual void HandleUpKey(Keys keys)
    {
    }

    protected virtual void HandleEndKey(Keys keys)
    {
    }

    protected virtual void HandleHomeKey(Keys keys)
    {
    }

    protected virtual void HandleLeftKey(Keys keys)
    {
    }

    protected virtual void HandleRightKey(Keys keys)
    {
    }

    protected virtual void ToggleSelection(Keys keys)
    {
    }

    protected internal virtual void EnsureRenderSettings()
    {
    }

    internal virtual CalendarView GetPreviousView()
    {
      return this.CreateView(this.GetPreviousViewDate());
    }

    internal virtual CalendarView GetNextView()
    {
      return this.CreateView(this.GetNextViewDate());
    }

    internal virtual CalendarView GetPreviousView(int months)
    {
      return (CalendarView) null;
    }

    internal virtual CalendarView GetNextView(int months)
    {
      return (CalendarView) null;
    }

    protected internal virtual DateTime EffectiveVisibleDate()
    {
      if (this.Calendar.Site != null && this.Calendar.Site.DesignMode && this.Calendar.FocusedDate == new DateTime(1980, 1, 1))
        return DateTime.Today;
      return this.Calendar.FocusedDate;
    }

    protected internal virtual bool HandleKeyDown(Keys keys)
    {
      if (this.Calendar == null)
        return false;
      if ((keys & Keys.Control) == Keys.Control)
      {
        if ((keys & Keys.Right) == Keys.Right)
        {
          this.HandleRightKey(keys);
          return true;
        }
        if ((keys & Keys.Left) == Keys.Left)
        {
          this.HandleLeftKey(keys);
          return true;
        }
      }
      switch (keys)
      {
        case Keys.Return:
        case Keys.Space:
          this.ToggleSelection(keys);
          return true;
        case Keys.Prior:
          this.HandlePageUpKey(keys);
          return true;
        case Keys.Next:
          this.HandlePageDownKey(keys);
          return true;
        case Keys.End:
          this.HandleEndKey(keys);
          return true;
        case Keys.Home:
          this.HandleHomeKey(keys);
          return true;
        case Keys.Left:
          this.HandleLeftKey(keys);
          return true;
        case Keys.Up:
          this.HandleUpKey(keys);
          return true;
        case Keys.Right:
          this.HandleRightKey(keys);
          return true;
        case Keys.Down:
          this.HandleDownKey(keys);
          return true;
        default:
          return false;
      }
    }

    protected internal virtual CalendarView CreateView(DateTime date)
    {
      return (CalendarView) null;
    }

    protected internal virtual CalendarView CreateView()
    {
      return (CalendarView) null;
    }

    internal void GetViewRowsAndColumns(out int rows, out int columns)
    {
      int xShift = 0;
      int yShift = 0;
      int rows1 = 0;
      int columns1 = 0;
      this.GetContentOffset(out xShift, out yShift);
      this.GetContentRowsAndColumns(out rows1, out columns1);
      rows = rows1 + yShift;
      columns = columns1 + xShift;
    }

    internal virtual void GetContentRowsAndColumns(out int rows, out int columns)
    {
      rows = this.Rows;
      columns = this.Columns;
    }

    internal virtual void GetContentOffset(out int xShift, out int yShift)
    {
      int num1 = 0;
      int num2 = 0;
      if (this.ShowSelector)
      {
        ++num2;
        ++num1;
      }
      else
      {
        if (this.ShowRowHeaders)
          ++num1;
        if (this.ShowColumnHeaders)
          ++num2;
      }
      xShift = num1;
      yShift = num2;
    }

    internal void SetRange(DateTime beginDate, DateTime endDate)
    {
      this.currentViewBeginDate = beginDate;
      this.currentViewEndDate = endDate;
    }

    internal virtual void Initialize()
    {
    }

    internal virtual void Initialize(CalendarView view)
    {
    }

    internal virtual void ReInitialize()
    {
      if (this.Calendar == null)
        return;
      this.rows = this.Calendar.Rows;
      this.columns = this.Calendar.Columns;
    }

    internal abstract string GetTitleContent();

    internal virtual void Remove()
    {
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    private void Calendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Columns")
        this.columns = this.calendar.Columns;
      else if (e.PropertyName == "Rows")
        this.rows = this.calendar.Rows;
      this.OnNotifyPropertyChanged(e.PropertyName);
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }
  }
}
