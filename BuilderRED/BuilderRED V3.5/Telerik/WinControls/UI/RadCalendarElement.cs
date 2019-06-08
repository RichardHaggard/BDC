// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadCalendarElement : CalendarVisualElement
  {
    public static RadProperty AllowFishEyeProperty = RadProperty.Register(nameof (AllowFishEye), typeof (bool), typeof (RadCalendarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ZoomFactorProperty = RadProperty.Register(nameof (ZoomFactor), typeof (float), typeof (RadCalendarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1.3f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HeaderWidthProperty = RadProperty.Register(nameof (HeaderWidth), typeof (int), typeof (RadCalendarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 17, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HeaderHeightProperty = RadProperty.Register(nameof (HeaderHeight), typeof (int), typeof (RadCalendarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 17, ElementPropertyOptions.AffectsDisplay));
    private string dayCellFormat = string.Empty;
    private bool allowDropDownFastNavigation = true;
    private CalendarStatusElement calendarStatusElement;
    private MonthViewElement calendarVisualElement;
    private CalendarNavigationElement calendarNavigationElement;
    private LightVisualElement selectedElement;
    private CalendarView view;
    private RadCalendar calendar;
    private DockLayoutPanel dockLayout;

    public RadCalendarElement(RadCalendar calendar)
      : this(calendar, (CalendarView) null)
    {
      this.InitializeChildren();
    }

    public RadCalendarElement(RadCalendar calendar, CalendarView view)
      : base(calendar, view)
    {
      this.view = view;
      this.calendar = calendar;
      this.InitializeChildren();
      LocalizationProvider<CalendarLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.CalendarLocalizationProvider_CurrentProviderChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CanFocus = false;
      this.Class = nameof (RadCalendarElement);
      this.Alignment = ContentAlignment.TopCenter;
    }

    protected override void DisposeManagedResources()
    {
      this.calendar = (RadCalendar) null;
      this.view = (CalendarView) null;
      if (this.calendarVisualElement != null)
      {
        this.calendarVisualElement.Dispose();
        this.calendarVisualElement = (MonthViewElement) null;
      }
      if (this.calendarStatusElement != null)
      {
        this.calendarStatusElement.Dispose();
        this.calendarStatusElement = (CalendarStatusElement) null;
      }
      if (this.calendarNavigationElement != null)
      {
        this.calendarNavigationElement.Dispose();
        this.calendarNavigationElement = (CalendarNavigationElement) null;
      }
      LocalizationProvider<CalendarLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.CalendarLocalizationProvider_CurrentProviderChanged);
      base.DisposeManagedResources();
    }

    [Description("Gets or sets a value whether drop down fast navigation is enabled.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AllowDropDownFastNavigation
    {
      get
      {
        return this.allowDropDownFastNavigation;
      }
      set
      {
        this.allowDropDownFastNavigation = value;
      }
    }

    [Description("Gets or sets whether the fish eye functionality is enabled")]
    [RadPropertyDefaultValue("AllowFishEye", typeof (bool))]
    [Category("Behavior")]
    public virtual bool AllowFishEye
    {
      get
      {
        return (bool) this.GetValue(RadCalendarElement.AllowFishEyeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCalendarElement.AllowFishEyeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the zooming factor of a cell which is handled by the fish eye functionality.")]
    [RadPropertyDefaultValue("ZoomFactor", typeof (float))]
    public virtual float ZoomFactor
    {
      get
      {
        return (float) this.GetValue(RadCalendarElement.ZoomFactorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCalendarElement.ZoomFactorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("HeaderWidth", typeof (int))]
    [Category("Appearance")]
    [Description("Gets or sets the width of header cells..")]
    public virtual int HeaderWidth
    {
      get
      {
        return (int) this.GetValue(RadCalendarElement.HeaderWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCalendarElement.HeaderWidthProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the height of header cells.")]
    [RadPropertyDefaultValue("HeaderHeight", typeof (int))]
    public virtual int HeaderHeight
    {
      get
      {
        return (int) this.GetValue(RadCalendarElement.HeaderHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCalendarElement.HeaderHeightProperty, (object) value);
      }
    }

    public override CalendarView View
    {
      get
      {
        if (this.Owner == null)
          return this.Calendar.DefaultView;
        return this.view;
      }
      set
      {
        this.view = value;
      }
    }

    protected LightVisualElement SelectedElement
    {
      get
      {
        return this.selectedElement;
      }
      set
      {
        this.selectedElement = value;
      }
    }

    public CalendarVisualElement CalendarVisualElement
    {
      get
      {
        return (CalendarVisualElement) this.calendarVisualElement;
      }
    }

    public CalendarNavigationElement CalendarNavigationElement
    {
      get
      {
        return this.calendarNavigationElement;
      }
    }

    public CalendarStatusElement CalendarStatusElement
    {
      get
      {
        return this.calendarStatusElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement FastBackwardButton
    {
      get
      {
        return this.CalendarNavigationElement.FastBackwardButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement FastForwardButton
    {
      get
      {
        return this.CalendarNavigationElement.FastForwardButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement PreviousButton
    {
      get
      {
        return this.CalendarNavigationElement.PreviousButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement NextButton
    {
      get
      {
        return this.CalendarNavigationElement.NextButton;
      }
    }

    public ZoomLevel ZoomLevel
    {
      get
      {
        if (this.calendarVisualElement == null || this.calendarVisualElement.TableElement == null)
          return ZoomLevel.Days;
        return this.calendarVisualElement.TableElement.Level;
      }
      set
      {
        this.calendarVisualElement.TableElement.Level = value;
      }
    }

    [DefaultValue(1)]
    public int MonthStep
    {
      get
      {
        return this.CalendarNavigationElement.MonthStep;
      }
      set
      {
        this.CalendarNavigationElement.MonthStep = value;
      }
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (!(args.RoutedEvent.EventName == "CellClickedEvent") || !(sender is CalendarCellElement))
        return;
      this.SelectedElement = sender as LightVisualElement;
    }

    private void CalendarLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.LocalizeStrings();
    }

    protected virtual void LocalizeStrings()
    {
      this.CalendarStatusElement.ClearButton.Text = LocalizationProvider<CalendarLocalizationProvider>.CurrentProvider.GetLocalizedString("CalendarClearButton");
      this.CalendarStatusElement.TodayButton.Text = LocalizationProvider<CalendarLocalizationProvider>.CurrentProvider.GetLocalizedString("CalendarTodayButton");
    }

    internal void ReInitializeChildren()
    {
      this.DisposeChildren();
      this.InitializeChildren();
    }

    public override void RefreshVisuals()
    {
      if (this.calendarNavigationElement != null)
        this.calendarNavigationElement.RefreshVisuals();
      if (this.calendarStatusElement != null)
        this.calendarStatusElement.RefreshVisuals();
      if (this.calendarVisualElement == null)
        return;
      this.calendarVisualElement.RefreshVisuals();
    }

    public override void RefreshVisuals(bool unconditional)
    {
      if (this.calendarVisualElement != null)
        this.calendarVisualElement.RefreshVisuals(unconditional);
      if (this.calendarNavigationElement != null)
        this.calendarNavigationElement.RefreshVisuals(unconditional);
      if (this.calendarStatusElement == null)
        return;
      this.calendarStatusElement.RefreshVisuals(unconditional);
    }

    private void InitializeChildren()
    {
      this.dockLayout = new DockLayoutPanel();
      this.calendarVisualElement = this.Calendar.MultiViewRows != 1 || this.Calendar.MultiViewColumns != 1 ? (MonthViewElement) new MultiMonthViewElement(this.calendar, this.calendar.DefaultView) : new MonthViewElement(this.calendar, this.calendar.DefaultView);
      this.calendarStatusElement = new CalendarStatusElement(this);
      int num1 = (int) this.calendarStatusElement.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Bottom);
      int num2 = (int) this.calendarStatusElement.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.Last);
      if (this.Calendar.ShowFooter)
        this.calendarStatusElement.Visibility = ElementVisibility.Visible;
      this.dockLayout.Children.Add((RadElement) this.calendarStatusElement);
      this.calendarNavigationElement = new CalendarNavigationElement(this);
      int num3 = (int) this.CalendarNavigationElement.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
      int num4 = (int) this.calendarNavigationElement.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.First);
      this.calendarNavigationElement.Visibility = this.Calendar.ShowHeader ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.dockLayout.Children.Add((RadElement) this.calendarNavigationElement);
      this.dockLayout.Children.Add((RadElement) this.calendarVisualElement);
      this.Children.Add((RadElement) this.dockLayout);
      this.LocalizeStrings();
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
    }
  }
}
