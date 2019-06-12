// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CalendarCellElement : CalendarVisualElement
  {
    public static RoutedEvent CellClickedEvent = RadElement.RegisterRoutedEvent(nameof (CellClickedEvent), typeof (CalendarCellElement));
    public static RadProperty IsZoomingProperty = RadProperty.Register("IsZooming", typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AllowFishEyeProperty = RadProperty.Register(nameof (AllowFishEye), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ZoomFactorProperty = RadProperty.Register(nameof (ZoomFactor), typeof (float), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1.3f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty WeekEndProperty = RadProperty.Register(nameof (WeekEnd), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SpecialDayProperty = RadProperty.Register(nameof (SpecialDay), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FocusedProperty = RadProperty.Register(nameof (Focused), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TodayProperty = RadProperty.Register(nameof (Today), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsHeaderCellProperty = RadProperty.Register("IsHeaderCell", typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OutOfRangeProperty = RadProperty.Register(nameof (OutOfRange), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OtherMonthProperty = RadProperty.Register(nameof (OtherMonth), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (SelectedProperty), typeof (bool), typeof (CalendarCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private Size defaultSize;
    private int row;
    private int column;
    private Rectangle proposedBounds;
    private AnimatedPropertySetting forwardsAnimate;
    private AnimatedPropertySetting backwardsAnimate;
    private AnimatedPropertySetting forwardsFontAnimate;
    private AnimatedPropertySetting backwardsFontAnimate;
    private Font oldFont;
    private DateTime? date;
    internal bool isChecked;
    internal bool isAnimating;

    public CalendarCellElement(CalendarVisualElement owner)
      : this(owner, (RadCalendar) null, (CalendarView) null, string.Empty)
    {
    }

    public CalendarCellElement(CalendarVisualElement owner, string text)
      : this(owner, (RadCalendar) null, (CalendarView) null, text)
    {
    }

    public CalendarCellElement(RadCalendar calendar, CalendarView view)
      : this((CalendarVisualElement) null, calendar, view, "")
    {
    }

    public CalendarCellElement(RadCalendar calendar, CalendarView view, string text)
      : this((CalendarVisualElement) null, calendar, view, text)
    {
    }

    public CalendarCellElement(
      CalendarVisualElement owner,
      RadCalendar calendar,
      CalendarView view,
      string text)
      : base(owner, calendar, view)
    {
      this.Text = text;
      if (owner != null)
        return;
      this.Calendar = calendar;
      this.View = view;
    }

    static CalendarCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new CalendarCellStateManagerFactory(), typeof (CalendarCellElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CanFocus = false;
      this.DefaultSize = new Size(20, 20);
      this.Class = "CalendarVisualCellElement";
      this.DrawBorder = true;
      this.DrawFill = true;
      this.oldFont = this.Font;
      this.FontChanged += new EventHandler(this.CalendarCellElement_FontChanged);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == VisualElement.FontProperty && e.NewValueSource == ValueSource.Inherited)
      {
        this.oldFont = (Font) e.NewValue;
      }
      else
      {
        if (e.Property != CalendarCellElement.SpecialDayProperty)
          return;
        if ((bool) e.NewValue && this.Calendar != null)
        {
          RadCalendarDay specialDay = this.Calendar.SpecialDays[(object) this.Date];
          if (specialDay != null)
          {
            this.ToolTipText = specialDay.ToolTip;
            return;
          }
        }
        this.ToolTipText = string.Empty;
      }
    }

    private void OnAnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.isAnimating = false;
      this.AutoSize = true;
      if (this.Calendar == null)
        return;
      this.Calendar.Invalidate();
    }

    private void OnForwardAnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.isAnimating = false;
      if (this.Calendar == null)
        return;
      if (this.Bounds.Size == this.ProposedBounds.Size)
      {
        this.Bounds = new Rectangle(Point.Empty, this.Bounds.Size);
        this.Font = this.oldFont;
        this.Invalidate();
      }
      this.Calendar.Invalidate();
    }

    private void CalendarCellElement_FontChanged(object sender, EventArgs e)
    {
      if (this.forwardsFontAnimate == null || !this.isAnimating)
        return;
      this.Font = (Font) this.forwardsFontAnimate.GetCurrentValue((RadObject) this);
      this.Calendar.Invalidate();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return (SizeF) new Size(0, 0);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Selected && (this.AllowFishEye || this.Calendar.AllowFishEye))
        this.Calendar.Invalidate();
      return base.ArrangeOverride(finalSize);
    }

    [DefaultValue(typeof (Size), "20,20")]
    [Description("Gets or sets the default cell size.")]
    public override Size DefaultSize
    {
      get
      {
        return this.defaultSize;
      }
      set
      {
        this.defaultSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Row
    {
      get
      {
        return this.row;
      }
      internal set
      {
        this.row = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Column
    {
      get
      {
        return this.column;
      }
      internal set
      {
        this.column = value;
      }
    }

    [RadPropertyDefaultValue("AllowFishEye", typeof (CalendarCellElement))]
    [Category("Behavior")]
    [Description("Indicates the fish eye feature factor of a cell")]
    public virtual bool AllowFishEye
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.AllowFishEyeProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.AllowFishEyeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Indicates the zooming factor of a cell")]
    [RadPropertyDefaultValue("ZoomFactor", typeof (CalendarCellElement))]
    public virtual float ZoomFactor
    {
      get
      {
        return (float) this.GetValue(CalendarCellElement.ZoomFactorProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.ZoomFactorProperty, (object) value);
      }
    }

    [Description("Indicates that current cell is a week end.")]
    [RadPropertyDefaultValue("WeekEnd", typeof (CalendarCellElement))]
    [Category("Behavior")]
    public virtual bool WeekEnd
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.WeekEndProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.WeekEndProperty, (object) value);
      }
    }

    [DefaultValue(typeof (DateTime), "1/1/1980")]
    [Category("Behavior")]
    [Description("Gets or sets the date which that cell is representing")]
    public virtual DateTime Date
    {
      get
      {
        if (this.date.HasValue)
          return this.date.Value;
        return new DateTime(1980, 1, 1);
      }
      set
      {
        if ((!this.date.HasValue || !(this.date.Value != value)) && this.date.HasValue)
          return;
        this.date = new DateTime?(value);
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("TemplateDay", typeof (CalendarCellElement))]
    [Description("Indicates that current cell is representing a special day")]
    public virtual bool SpecialDay
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.SpecialDayProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.SpecialDayProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Focused", typeof (CalendarCellElement))]
    [Category("Behavior")]
    [Description("Indicates that current cell is representing the focused day")]
    public virtual bool Focused
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.FocusedProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.FocusedProperty, (object) value);
      }
    }

    [Description("Indicates that current cell is representing the current day")]
    [Category("Behavior")]
    [RadPropertyDefaultValue("Today", typeof (CalendarCellElement))]
    public virtual bool Today
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.TodayProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.TodayProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Indicates that current element is representing a day which is out of range.")]
    [RadPropertyDefaultValue("OutOfRange", typeof (CalendarCellElement))]
    public virtual bool OutOfRange
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.OutOfRangeProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.OutOfRangeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("OtherMonth", typeof (CalendarCellElement))]
    [Description("Indicates that current element is representing a cell from another month.")]
    [Category("Behavior")]
    public virtual bool OtherMonth
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.OtherMonthProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.OtherMonthProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Indicates that current element selected.")]
    [RadPropertyDefaultValue("SelectedProperty", typeof (CalendarCellElement))]
    public virtual bool Selected
    {
      get
      {
        return (bool) this.GetValue(CalendarCellElement.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarCellElement.SelectedProperty, (object) value);
      }
    }

    internal Rectangle ProposedBounds
    {
      get
      {
        return this.proposedBounds;
      }
      set
      {
        this.proposedBounds = value;
      }
    }

    internal CalendarTableElement OwnerTableElement
    {
      get
      {
        return this.Owner as CalendarTableElement;
      }
    }

    private void InitializeForwardAnimationSetting()
    {
      SizeF size = (SizeF) this.proposedBounds.Size;
      size.Width *= this.ZoomFactor;
      size.Height *= this.ZoomFactor;
      int num1 = (int) ((double) size.Width - (double) this.proposedBounds.Width);
      int num2 = (int) ((double) size.Height - (double) this.proposedBounds.Height);
      int x = -num1;
      int y = -num2;
      int width = (int) ((double) size.Width + (double) num1);
      int height = (int) ((double) size.Height + (double) num2);
      this.forwardsAnimate = new AnimatedPropertySetting(RadElement.BoundsProperty, (object) new Rectangle(0, 0, this.proposedBounds.Width, this.proposedBounds.Height), (object) new Rectangle(x, y, width, height), 8, 20);
      this.forwardsAnimate.RemoveAfterApply = true;
      this.forwardsFontAnimate = new AnimatedPropertySetting(VisualElement.FontProperty, (object) this.oldFont, (object) new Font(this.Font.FontFamily, this.oldFont.Size * 1.5f * this.ZoomFactor), 8, 20);
      this.forwardsFontAnimate.RemoveAfterApply = true;
    }

    private void InitializeBackwardAnimationSetting()
    {
      SizeF size = (SizeF) this.proposedBounds.Size;
      size.Width *= this.ZoomFactor;
      size.Height *= this.ZoomFactor;
      int num1 = (int) ((double) size.Width - (double) this.proposedBounds.Width);
      int num2 = (int) ((double) size.Height - (double) this.proposedBounds.Height);
      int x = -num1;
      int y = -num2;
      int width = (int) ((double) size.Width + (double) num1);
      int height = (int) ((double) size.Height + (double) num2);
      this.backwardsAnimate = new AnimatedPropertySetting(RadElement.BoundsProperty, (object) new Rectangle(x, y, width, height), (object) this.proposedBounds, 10, 20);
      this.backwardsAnimate.AnimationFinished += new AnimationFinishedEventHandler(this.OnAnimationFinished);
      this.backwardsFontAnimate = new AnimatedPropertySetting(VisualElement.FontProperty, (object) this.oldFont, (object) new Font(this.Font.FontFamily, this.oldFont.Size), 8, 20);
    }

    internal void PerformForwardAnimation()
    {
      this.isAnimating = true;
      if (this.forwardsAnimate == null)
      {
        this.InitializeForwardAnimationSetting();
        this.forwardsAnimate.AnimationFinished += new AnimationFinishedEventHandler(this.OnForwardAnimationFinished);
      }
      if (!this.AllowFishEye && !this.Calendar.AllowFishEye)
        return;
      this.AutoSize = false;
      this.forwardsAnimate.ApplyValue((RadObject) this);
      this.forwardsFontAnimate.ApplyValue((RadObject) this);
    }

    internal void PerformReverseAnimation()
    {
      if (this.backwardsAnimate == null)
        this.InitializeBackwardAnimationSetting();
      this.isAnimating = false;
      this.AutoSize = true;
      if (this.forwardsAnimate != null)
        this.forwardsAnimate.Stop((RadObject) this);
      if (this.forwardsFontAnimate != null)
      {
        this.forwardsFontAnimate.Stop((RadObject) this);
        this.forwardsFontAnimate.UnapplyValue((RadObject) this);
      }
      this.Bounds = new Rectangle(Point.Empty, this.ProposedBounds.Size);
      this.Font = this.oldFont;
      this.InitializeForwardAnimationSetting();
      if (this.Calendar == null)
        return;
      this.Calendar.Invalidate();
    }

    public override string ToString()
    {
      return this.Date.ToShortDateString();
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
    }
  }
}
