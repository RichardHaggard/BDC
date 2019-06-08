// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarNavigationElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class CalendarNavigationElement : CalendarVisualElement
  {
    public static RadProperty LeftButtonImageProperty = RadProperty.Register(nameof (LeftButtonImage), typeof (Image), typeof (CalendarNavigationElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty RightButtonImageProperty = RadProperty.Register(nameof (RightButtonImage), typeof (Image), typeof (CalendarNavigationElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty FastRightButtonImageProperty = RadProperty.Register(nameof (FastRightButtonImage), typeof (Image), typeof (CalendarNavigationElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty FastLeftButtonImageProperty = RadProperty.Register(nameof (FastLeftButtonImage), typeof (Image), typeof (CalendarNavigationElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private int fastNavigationItemsCount = 7;
    private int monthStep = 1;
    private HeaderNavigationMode navigationMode = HeaderNavigationMode.Popup;
    private RadButtonElement fastBackwardButton;
    private RadButtonElement fastForwardButton;
    private RadRepeatButtonElement prevButton;
    private RadRepeatButtonElement nextButton;
    private BoxLayout boxLayout;
    private Timer scrollingTimer;
    private RadDateTimePickerDropDown dropDown;
    private RadCalendarFastNavigationControl hostedControl;
    private bool scrollUp;
    private bool shouldScroll;
    private int viewIndex;
    private Point offsetPoint;
    private FastNavigationItem item1;
    private FastNavigationItem item2;
    private FastNavigationItem item3;
    private FastNavigationItem item4;
    private FastNavigationItem item5;
    private FastNavigationItem item6;
    private FastNavigationItem item7;
    private FastNavigationItem item8;
    private FastNavigationItem item9;
    private FastNavigationItem item10;
    private FastNavigationItem item11;
    private FastNavigationItem item12;
    private FastNavigationItem item13;

    internal CalendarNavigationElement(RadCalendarElement owner)
      : base((CalendarVisualElement) owner)
    {
      this.Calendar.PropertyChanged += new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
      this.dropDown.OwnerControl = (RadControl) this.Calendar;
      this.SetText();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
      this.StretchHorizontally = true;
      this.DrawFill = true;
      this.Class = nameof (CalendarNavigationElement);
      this.TextAlignment = ContentAlignment.MiddleCenter;
      this.scrollingTimer = new Timer();
      this.scrollingTimer.Tick += new EventHandler(this.scrollingTimer_Tick);
      this.scrollingTimer.Interval = 1000;
      this.dropDown = new RadDateTimePickerDropDown((RadItem) this);
      this.hostedControl = new RadCalendarFastNavigationControl();
      this.dropDown.HostedControl = (RadControl) this.hostedControl;
      this.dropDown.SizingGrip.Visibility = ElementVisibility.Collapsed;
      this.hostedControl.ThemeName = "ControlDefault";
      this.dropDown.BackColor = Color.White;
      this.dropDown.AnimationEnabled = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.boxLayout = new BoxLayout();
      this.boxLayout.Orientation = Orientation.Horizontal;
      this.boxLayout.StretchHorizontally = true;
      this.boxLayout.StretchVertically = false;
      this.boxLayout.RightToLeft = false;
      this.fastForwardButton = new RadButtonElement();
      this.fastForwardButton.DisplayStyle = DisplayStyle.Image;
      this.fastForwardButton.ThemeRole = "FastNavigateForwardButton";
      this.fastForwardButton.StretchHorizontally = false;
      this.fastForwardButton.StretchVertically = false;
      int num1 = (int) this.fastForwardButton.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.Last);
      this.fastForwardButton.MouseDown += new MouseEventHandler(this.fastForwardButton_MouseDown);
      this.fastForwardButton.Class = "fastForwardButton";
      this.fastForwardButton.MinSize = new Size(16, 16);
      this.fastForwardButton.ShowBorder = false;
      this.fastForwardButton.Children[1].Alignment = ContentAlignment.MiddleCenter;
      this.fastForwardButton.Children[0].Visibility = ElementVisibility.Hidden;
      this.boxLayout.Children.Add((RadElement) this.fastForwardButton);
      this.nextButton = new RadRepeatButtonElement();
      this.nextButton.DisplayStyle = DisplayStyle.Image;
      this.nextButton.ThemeRole = "NavigateForwardButton";
      this.nextButton.StretchHorizontally = false;
      this.nextButton.StretchVertically = false;
      int num2 = (int) this.nextButton.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.Last);
      this.nextButton.Click += new EventHandler(this.nextButton_Click);
      this.nextButton.Class = "nextButton";
      this.nextButton.MinSize = new Size(16, 16);
      this.nextButton.Children[1].Alignment = ContentAlignment.MiddleCenter;
      this.nextButton.ShowBorder = false;
      this.nextButton.Children[0].Visibility = ElementVisibility.Hidden;
      this.boxLayout.Children.Add((RadElement) this.nextButton);
      this.fastBackwardButton = new RadButtonElement();
      this.fastBackwardButton.DisplayStyle = DisplayStyle.Image;
      this.fastBackwardButton.ThemeRole = "FastNavigateBackwardButton";
      this.fastBackwardButton.StretchHorizontally = false;
      this.fastBackwardButton.StretchVertically = false;
      this.fastBackwardButton.MouseDown += new MouseEventHandler(this.fastBackwardButton_MouseDown);
      int num3 = (int) this.fastBackwardButton.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.First);
      this.fastBackwardButton.Class = "fastBackwardButton";
      this.fastBackwardButton.MinSize = new Size(16, 16);
      this.fastBackwardButton.ShowBorder = false;
      this.fastBackwardButton.Children[0].Visibility = ElementVisibility.Hidden;
      this.fastBackwardButton.Children[1].Alignment = ContentAlignment.MiddleCenter;
      this.boxLayout.Children.Add((RadElement) this.fastBackwardButton);
      this.prevButton = new RadRepeatButtonElement();
      this.prevButton.DisplayStyle = DisplayStyle.Image;
      this.prevButton.ThemeRole = "NavigateBackwardButton";
      this.prevButton.StretchHorizontally = false;
      this.prevButton.StretchVertically = false;
      this.prevButton.Click += new EventHandler(this.prevButton_Click);
      this.prevButton.Class = "prevButton";
      int num4 = (int) this.prevButton.SetValue(BoxLayout.StripPositionProperty, (object) BoxLayout.StripPosition.First);
      this.prevButton.MinSize = new Size(16, 16);
      this.prevButton.Children[1].Alignment = ContentAlignment.MiddleCenter;
      this.prevButton.ShowBorder = false;
      this.prevButton.Children[0].Visibility = ElementVisibility.Hidden;
      this.boxLayout.Children.Add((RadElement) this.prevButton);
      this.Children.Add((RadElement) this.boxLayout);
    }

    protected override void DisposeManagedResources()
    {
      this.Calendar.PropertyChanged -= new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
      this.View = (CalendarView) null;
      this.Calendar = (RadCalendar) null;
      if (this.prevButton != null)
      {
        this.prevButton.Click -= new EventHandler(this.prevButton_Click);
        this.prevButton.Dispose();
        this.prevButton = (RadRepeatButtonElement) null;
      }
      if (this.nextButton != null)
      {
        this.nextButton.Click -= new EventHandler(this.nextButton_Click);
        this.nextButton.Dispose();
        this.nextButton = (RadRepeatButtonElement) null;
      }
      if (this.fastBackwardButton != null)
      {
        this.fastBackwardButton.MouseDown -= new MouseEventHandler(this.fastBackwardButton_MouseDown);
        this.fastBackwardButton.Dispose();
        this.fastBackwardButton = (RadButtonElement) null;
      }
      if (this.fastForwardButton != null)
      {
        this.fastForwardButton.MouseDown -= new MouseEventHandler(this.fastForwardButton_MouseDown);
        this.fastForwardButton.Dispose();
        this.fastForwardButton = (RadButtonElement) null;
      }
      this.DisposeNavigationElements();
      base.DisposeManagedResources();
    }

    private void DisposeNavigationElements()
    {
      if (this.scrollingTimer != null)
      {
        this.scrollingTimer.Stop();
        this.scrollingTimer.Dispose();
      }
      if (this.dropDown != null)
      {
        this.dropDown.Dispose();
        this.dropDown = (RadDateTimePickerDropDown) null;
      }
      if (this.hostedControl == null)
        return;
      this.hostedControl.Dispose();
      this.hostedControl = (RadCalendarFastNavigationControl) null;
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [Description(" Gets or sets the count of the items in the fast navigation drop down")]
    public int FastNavigationItemsCount
    {
      get
      {
        return this.fastNavigationItemsCount;
      }
      set
      {
        if (this.fastNavigationItemsCount == value || value != 3 && value != 7 && (value != 11 && value != 13))
          return;
        this.hostedControl.Items.Clear();
        this.fastNavigationItemsCount = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement FastBackwardButton
    {
      get
      {
        return this.fastBackwardButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement FastForwardButton
    {
      get
      {
        return this.fastForwardButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement PreviousButton
    {
      get
      {
        return (RadButtonElement) this.prevButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement NextButton
    {
      get
      {
        return (RadButtonElement) this.nextButton;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Localizable(true)]
    [Category("Appearance")]
    public virtual Image RightButtonImage
    {
      get
      {
        return (Image) this.GetValue(CalendarNavigationElement.RightButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarNavigationElement.RightButtonImageProperty, (object) value);
        this.nextButton.Image = value;
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Image LeftButtonImage
    {
      get
      {
        return (Image) this.GetValue(CalendarNavigationElement.LeftButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarNavigationElement.LeftButtonImageProperty, (object) value);
        this.prevButton.Image = value;
      }
    }

    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Image FastRightButtonImage
    {
      get
      {
        return (Image) this.GetValue(CalendarNavigationElement.FastRightButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarNavigationElement.FastRightButtonImageProperty, (object) value);
        this.fastForwardButton.Image = value;
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image FastLeftButtonImage
    {
      get
      {
        return (Image) this.GetValue(CalendarNavigationElement.FastLeftButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(CalendarNavigationElement.FastLeftButtonImageProperty, (object) value);
        this.fastBackwardButton.Image = value;
      }
    }

    [DefaultValue(1)]
    public int MonthStep
    {
      get
      {
        return this.monthStep;
      }
      set
      {
        this.monthStep = value;
      }
    }

    public HeaderNavigationMode NavigationMode
    {
      get
      {
        return this.navigationMode;
      }
      set
      {
        this.navigationMode = value;
      }
    }

    [Browsable(false)]
    public int Direction
    {
      get
      {
        return this.Calendar.RightToLeft != RightToLeft.Yes ? 1 : -1;
      }
    }

    private void Calendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "ShowHeader":
          this.Visibility = this.Calendar.ShowHeader ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case "TitleFormat":
          this.Text = this.View.GetTitleContent();
          this.UpdateVisuals();
          break;
        case "ShowFastNavigationButtons":
          if (!this.Calendar.ShowFastNavigationButtons)
          {
            this.fastBackwardButton.Visibility = ElementVisibility.Hidden;
            this.fastForwardButton.Visibility = ElementVisibility.Hidden;
            break;
          }
          this.fastBackwardButton.Visibility = ElementVisibility.Visible;
          this.fastForwardButton.Visibility = ElementVisibility.Visible;
          break;
        case "ShowNavigationButtons":
          if (!this.Calendar.ShowNavigationButtons)
          {
            this.nextButton.Visibility = ElementVisibility.Hidden;
            this.prevButton.Visibility = ElementVisibility.Hidden;
            break;
          }
          this.nextButton.Visibility = ElementVisibility.Visible;
          this.prevButton.Visibility = ElementVisibility.Visible;
          break;
        case "FastNavigationNextText":
          this.fastForwardButton.Text = this.Calendar.FastNavigationNextText;
          this.UpdateVisuals();
          break;
        case "FastNavigationNextToolTip":
          this.fastForwardButton.ToolTipText = this.Calendar.FastNavigationNextToolTip;
          this.UpdateVisuals();
          break;
        case "FastNavigationPrevText":
          this.fastBackwardButton.Text = this.Calendar.FastNavigationPrevText;
          this.UpdateVisuals();
          break;
        case "FastNavigationPrevToolTip":
          this.fastBackwardButton.ToolTipText = this.Calendar.FastNavigationPrevToolTip;
          this.UpdateVisuals();
          break;
        case "NavigationNextText":
          this.nextButton.Text = this.Calendar.NavigationNextText;
          this.UpdateVisuals();
          break;
        case "NavigationNextToolTip":
          this.nextButton.ToolTipText = this.Calendar.NavigationNextToolTip;
          this.UpdateVisuals();
          break;
        case "NavigationPrevText":
          this.prevButton.Text = this.Calendar.NavigationPrevText;
          this.UpdateVisuals();
          break;
        case "NavigationPrevToolTip":
          this.prevButton.ToolTipText = this.Calendar.NavigationPrevToolTip;
          this.UpdateVisuals();
          break;
        case "ThemeName":
          if (this.hostedControl == null)
            break;
          this.hostedControl.ThemeName = this.Calendar.ThemeName;
          break;
      }
    }

    private void prevButton_Click(object sender, EventArgs e)
    {
      if (!this.Calendar.AllowNavigation || this.Calendar.ReadOnly)
        return;
      MonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement;
      DateTime now = DateTime.Now;
      CalendarView calendarView = this.View;
      DateTime startDate;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        calendarView = this.Calendar.GetNewViewFromStep(-1 * this.Direction);
        startDate = calendarView.ViewStartDate;
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Months)
      {
        startDate = this.View.ViewStartDate.AddYears(-1 * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-1);
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Years)
      {
        startDate = this.View.ViewStartDate.AddYears(-10 * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-10);
      }
      else
      {
        startDate = this.View.ViewStartDate.AddYears(-100 * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-100);
      }
      CalendarNavigatingEventArgs args = new CalendarNavigatingEventArgs(startDate, CalendarNavigationDirection.Backward, false);
      this.Calendar.OnNavigating(args);
      if (args.Cancel)
        return;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        this.UpdateView(args.StartDate != calendarView.ViewStartDate ? calendarView.CreateView(args.StartDate) : calendarView);
        this.UpdateVisuals();
      }
      else
      {
        calendarVisualElement.RefreshVisuals(true);
        this.Text = this.GetTitleText();
      }
      this.Calendar.OnNavigated(new CalendarNavigatedEventArgs(CalendarNavigationDirection.Backward, false));
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      if (!this.Calendar.AllowNavigation || this.Calendar.ReadOnly)
        return;
      MonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement;
      DateTime now = DateTime.Now;
      CalendarView calendarView = this.View;
      DateTime startDate;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        calendarView = this.Calendar.GetNewViewFromStep(this.Direction);
        startDate = calendarView.ViewStartDate;
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Months)
      {
        startDate = this.View.ViewStartDate.AddYears(this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(1);
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Years)
      {
        startDate = this.View.ViewStartDate.AddYears(10 * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(10);
      }
      else
      {
        startDate = this.View.ViewStartDate.AddYears(100 * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(100);
      }
      CalendarNavigatingEventArgs args = new CalendarNavigatingEventArgs(startDate, CalendarNavigationDirection.Forward, false);
      this.Calendar.OnNavigating(args);
      if (args.Cancel)
        return;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        this.UpdateView(args.StartDate != calendarView.ViewStartDate ? calendarView.CreateView(args.StartDate) : calendarView);
        this.UpdateVisuals();
      }
      else
      {
        calendarVisualElement.RefreshVisuals(true);
        this.Text = this.GetTitleText();
      }
      this.Calendar.OnNavigated(new CalendarNavigatedEventArgs(CalendarNavigationDirection.Forward, false));
    }

    private void fastForwardButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.Calendar.AllowFastNavigation || this.Calendar.ReadOnly)
        return;
      MonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement;
      DateTime now = DateTime.Now;
      CalendarView calendarView = this.View;
      DateTime startDate;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        calendarView = this.Calendar.GetNewViewFromStep(this.Calendar.FastNavigationStep * this.Direction);
        startDate = calendarView.ViewStartDate;
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Months)
      {
        startDate = this.View.ViewStartDate.AddYears(this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(this.Calendar.FastNavigationStep);
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Years)
      {
        startDate = this.View.ViewStartDate.AddYears(10 * this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(10 * this.Calendar.FastNavigationStep);
      }
      else
      {
        startDate = this.View.ViewStartDate.AddYears(100 * this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(100 * this.Calendar.FastNavigationStep);
      }
      CalendarNavigatingEventArgs args = new CalendarNavigatingEventArgs(startDate, CalendarNavigationDirection.Forward, true);
      this.Calendar.OnNavigating(args);
      if (args.Cancel)
        return;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        this.UpdateView(args.StartDate != calendarView.ViewStartDate ? calendarView.CreateView(args.StartDate) : calendarView);
        this.UpdateVisuals();
      }
      else
      {
        calendarVisualElement.RefreshVisuals(true);
        this.Text = this.GetTitleText();
      }
      this.Calendar.OnNavigated(new CalendarNavigatedEventArgs(CalendarNavigationDirection.Forward, true));
    }

    private void fastBackwardButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.Calendar.AllowFastNavigation || this.Calendar.ReadOnly)
        return;
      MonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement;
      DateTime now = DateTime.Now;
      CalendarView calendarView = this.View;
      DateTime startDate;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        calendarView = this.Calendar.GetNewViewFromStep(-this.Calendar.FastNavigationStep * this.Direction);
        startDate = calendarView.ViewStartDate;
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Months)
      {
        startDate = this.View.ViewStartDate.AddYears(-1 * this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-1 * this.Calendar.FastNavigationStep);
      }
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Years)
      {
        startDate = this.View.ViewStartDate.AddYears(-10 * this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-10 * this.Calendar.FastNavigationStep);
      }
      else
      {
        startDate = this.View.ViewStartDate.AddYears(-100 * this.Calendar.FastNavigationStep * this.Direction);
        this.Calendar.FocusedDate = this.Calendar.FocusedDate.AddYears(-100 * this.Calendar.FastNavigationStep);
      }
      CalendarNavigatingEventArgs args = new CalendarNavigatingEventArgs(startDate, CalendarNavigationDirection.Backward, true);
      this.Calendar.OnNavigating(args);
      if (args.Cancel)
        return;
      if (calendarVisualElement is MultiMonthViewElement || calendarVisualElement.TableElement.Level == ZoomLevel.Days)
      {
        this.UpdateView(args.StartDate != calendarView.ViewStartDate ? calendarView.CreateView(args.StartDate) : calendarView);
        this.UpdateVisuals();
      }
      else
      {
        calendarVisualElement.RefreshVisuals(true);
        this.Text = this.GetTitleText();
      }
      this.Calendar.OnNavigated(new CalendarNavigatedEventArgs(CalendarNavigationDirection.Backward, true));
    }

    private void scrollingTimer_Tick(object sender, EventArgs e)
    {
      if (!this.shouldScroll)
        return;
      int num;
      if (this.scrollUp)
      {
        if ((this.hostedControl.Items[0] as FastNavigationItem).View.ViewStartDate <= this.Calendar.RangeMinDate)
          return;
        --this.viewIndex;
        num = 1;
      }
      else
      {
        if ((this.hostedControl.Items[this.hostedControl.Items.Count - 1] as FastNavigationItem).View.ViewEndDate >= this.Calendar.RangeMaxDate)
          return;
        ++this.viewIndex;
        num = -1;
      }
      if (this.Calendar.RangeMaxDate.Month + this.Calendar.RangeMaxDate.Year * 12 - (this.Calendar.RangeMinDate.Month + this.Calendar.RangeMinDate.Year * 12) > this.hostedControl.Items.Count)
      {
        CalendarView nextView1 = (this.hostedControl.Items[this.hostedControl.Items.Count - 1] as FastNavigationItem).View.GetNextView(-num);
        int month1 = nextView1.ViewStartDate.Month;
        int year1 = nextView1.ViewStartDate.Year;
        CalendarView nextView2 = (this.hostedControl.Items[0] as FastNavigationItem).View.GetNextView(-num);
        int month2 = nextView2.ViewStartDate.Month;
        int year2 = nextView2.ViewStartDate.Year;
        this.InitializeNavItems(this.viewIndex);
        if (month1 >= this.Calendar.RangeMaxDate.Month && year1 >= this.Calendar.RangeMaxDate.Year || month2 <= this.Calendar.RangeMinDate.Month && year2 <= this.Calendar.RangeMinDate.Year)
          this.viewIndex += num;
      }
      this.scrollingTimer.Interval = 2500 / (Math.Abs(this.offsetPoint.Y) + 1);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.offsetPoint = Point.Empty;
      this.shouldScroll = false;
      if (this.Capture)
      {
        if (this.dropDown.PointToScreen(Point.Empty).Y > Cursor.Position.Y)
        {
          this.shouldScroll = true;
          this.scrollUp = true;
          this.offsetPoint = new Point(0, this.dropDown.PointToScreen(Point.Empty).Y - Cursor.Position.Y);
        }
        else if (this.dropDown.PointToScreen(Point.Empty).Y + this.dropDown.Size.Height < Cursor.Position.Y)
        {
          this.shouldScroll = true;
          this.scrollUp = false;
          this.offsetPoint = new Point(0, this.dropDown.PointToScreen(Point.Empty).Y + this.dropDown.Size.Height - Cursor.Position.Y);
        }
        Point client = this.dropDown.PointToClient(Cursor.Position);
        foreach (FastNavigationItem fastNavigationItem in (RadItemCollection) this.hostedControl.Items)
        {
          fastNavigationItem.Selected = false;
          if (fastNavigationItem.ControlBoundingRectangle.Contains(client))
            fastNavigationItem.Selected = true;
        }
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
      if (this.NavigationMode == HeaderNavigationMode.Zoom)
      {
        ++(this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement).TableElement.Level;
        this.Text = this.GetTitleText();
      }
      else if (this.NavigationMode == HeaderNavigationMode.Popup)
      {
        Point client = this.dropDown.PointToClient(Cursor.Position);
        foreach (FastNavigationItem fastNavigationItem in (RadItemCollection) this.hostedControl.Items)
        {
          if (fastNavigationItem.ControlBoundingRectangle.Contains(client))
          {
            CalendarView view = fastNavigationItem.View;
            CalendarNavigatingEventArgs args = new CalendarNavigatingEventArgs(view.ViewStartDate, CalendarNavigationDirection.None, false);
            this.Calendar.OnNavigating(args);
            if (!args.Cancel)
            {
              this.UpdateView(view);
              this.UpdateVisuals();
              this.Calendar.OnNavigated(new CalendarNavigatedEventArgs(CalendarNavigationDirection.None, false));
              break;
            }
            break;
          }
        }
        this.dropDown.Hide();
        this.scrollingTimer.Stop();
      }
      base.OnMouseUp(e);
    }

    private string GetTitleText()
    {
      MonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement;
      string str = string.Empty;
      if (calendarVisualElement.TableElement.Level == ZoomLevel.Months)
        str = (calendarVisualElement.TableElement.VisualElements[0] as CalendarCellElement).Date.Year.ToString();
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.Years)
        str = (calendarVisualElement.TableElement.VisualElements[1] as CalendarCellElement).Date.Year.ToString() + " - " + (calendarVisualElement.TableElement.VisualElements[10] as CalendarCellElement).Date.Year.ToString();
      else if (calendarVisualElement.TableElement.Level == ZoomLevel.YearsRange)
        str = (calendarVisualElement.TableElement.VisualElements[1] as CalendarCellElement).Date.Year.ToString() + " - " + ((calendarVisualElement.TableElement.VisualElements[10] as CalendarCellElement).Date.Year + 9).ToString();
      return str;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.NavigationMode == HeaderNavigationMode.Zoom || this.NavigationMode == HeaderNavigationMode.None)
        return;
      if (!this.Calendar.ReadOnly && this.Calendar.CalendarElement.AllowDropDownFastNavigation)
      {
        this.Capture = true;
        this.LoadFastNavigationItems();
        this.dropDown.Location = new Point(this.Calendar.PointToScreen(Point.Empty).X + this.Calendar.Width / 4, this.Calendar.PointToScreen(Point.Empty).Y + this.ControlBoundingRectangle.Y - this.dropDown.Size.Height / 2);
        this.scrollingTimer.Start();
        this.dropDown.Show();
        this.hostedControl.PerformLayout();
        this.dropDown.Size = new Size(this.Calendar.Size.Width / 2, 1000);
        if (this.hostedControl.Items.Count > 0)
          this.hostedControl.Items[0].InvalidateMeasure();
        float num = 0.0f;
        for (int index = 0; index < this.hostedControl.Items.Count; ++index)
          num += (float) (int) this.hostedControl.Items[0].DesiredSize.Height;
        this.dropDown.Size = new Size(this.Calendar.Size.Width / 2, (int) num + this.hostedControl.fastNavigationElement.ChildrenMargin.Vertical + this.hostedControl.fastNavigationElement.Margin.Vertical + 2);
        this.hostedControl.fastNavigationElement.UpdateLayout();
        this.dropDown.Location = new Point(this.Calendar.PointToScreen(Point.Empty).X + this.Calendar.Width / 4, this.Calendar.PointToScreen(Point.Empty).Y + this.ControlBoundingRectangle.Y - this.dropDown.Size.Height / 2 + 9);
      }
      base.OnMouseDown(e);
    }

    internal void SetText()
    {
      if (this.Calendar != null)
      {
        if (this.Calendar.CalendarElement == null || this.Calendar.CalendarElement.CalendarVisualElement == null || this.Calendar.CalendarElement.CalendarVisualElement is MultiMonthViewElement || this.Calendar.CalendarElement.CalendarVisualElement is MonthViewElement && (this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement).TableElement.Level == ZoomLevel.Days)
          this.Text = this.View.GetTitleContent();
        else
          this.Text = this.GetTitleText();
      }
      else
        this.Text = DateTime.Now.Year.ToString();
    }

    protected internal virtual void UpdateView(CalendarView view)
    {
      if (this.Calendar != null)
      {
        bool flag = this.Calendar.DefaultView != view;
        if (flag && this.Calendar.CallOnViewChanging(view).Cancel)
          return;
        this.Calendar.SetCalendarView(view);
        this.Calendar.CalendarElement.CalendarVisualElement.View = view;
        if (flag)
          this.Calendar.CallOnViewChanged();
      }
      this.Text = this.View.GetTitleContent();
      this.UpdateVisuals();
    }

    protected internal virtual void UpdateVisuals()
    {
      this.Invalidate();
    }

    public override void RefreshVisuals()
    {
      base.RefreshVisuals();
      this.UpdateView(this.View);
    }

    public override void RefreshVisuals(bool unconditional)
    {
      base.RefreshVisuals(unconditional);
      this.UpdateView(this.View);
    }

    private void InitializeNavItems(int offset)
    {
      if (this.item1 == null)
      {
        this.item1 = new FastNavigationItem(this.Calendar, this.View);
        this.item2 = new FastNavigationItem(this.Calendar, this.View);
        this.item3 = new FastNavigationItem(this.Calendar, this.View);
        this.item4 = new FastNavigationItem(this.Calendar, this.View);
        this.item5 = new FastNavigationItem(this.Calendar, this.View);
        this.item6 = new FastNavigationItem(this.Calendar, this.View);
        this.item7 = new FastNavigationItem(this.Calendar, this.View);
        this.item8 = new FastNavigationItem(this.Calendar, this.View);
        this.item9 = new FastNavigationItem(this.Calendar, this.View);
        this.item10 = new FastNavigationItem(this.Calendar, this.View);
        this.item11 = new FastNavigationItem(this.Calendar, this.View);
        this.item12 = new FastNavigationItem(this.Calendar, this.View);
        this.item13 = new FastNavigationItem(this.Calendar, this.View);
        this.item1.InvalidateMeasure();
        this.item2.InvalidateMeasure();
        this.item3.InvalidateMeasure();
        this.item4.InvalidateMeasure();
        this.item5.InvalidateMeasure();
        this.item6.InvalidateMeasure();
        this.item7.InvalidateMeasure();
        this.item8.InvalidateMeasure();
        this.item9.InvalidateMeasure();
        this.item10.InvalidateMeasure();
        this.item11.InvalidateMeasure();
        this.item12.InvalidateMeasure();
        this.item13.InvalidateMeasure();
      }
      int num1 = 3;
      switch (this.FastNavigationItemsCount)
      {
        case 3:
          num1 = 1;
          break;
        case 7:
          num1 = 3;
          break;
        case 9:
          num1 = 4;
          break;
        case 11:
          num1 = 5;
          break;
        case 13:
          num1 = 6;
          break;
      }
      int num2 = (this.Calendar.DefaultView.ViewStartDate.Year - this.Calendar.RangeMinDate.Year) * 12 + (this.Calendar.DefaultView.ViewStartDate.Month - this.Calendar.RangeMinDate.Month);
      if (num2 < this.FastNavigationItemsCount)
        num1 = num2;
      if (this.FastNavigationItemsCount > 1)
      {
        RadCalendar calendar = this.Calendar;
        int num3 = -num1 * this.MonthStep;
        int monthStep = this.MonthStep;
        int num4 = offset;
        int navigationStep = num3 + num4;
        CalendarView newViewFromStep1 = calendar.GetNewViewFromStep(navigationStep);
        CalendarView newViewFromStep2 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + this.MonthStep + offset);
        CalendarView newViewFromStep3 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 2 * this.MonthStep + offset);
        this.item1.View = newViewFromStep1;
        this.item2.View = newViewFromStep2;
        this.item3.View = newViewFromStep3;
        this.item1.Text = this.item1.View.GetTitleContent();
        this.item2.Text = this.item2.View.GetTitleContent();
        this.item3.Text = this.item3.View.GetTitleContent();
      }
      if (this.FastNavigationItemsCount > 3)
      {
        CalendarView newViewFromStep1 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 3 * this.MonthStep + offset);
        CalendarView newViewFromStep2 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 4 * this.MonthStep + offset);
        CalendarView newViewFromStep3 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 5 * this.MonthStep + offset);
        CalendarView newViewFromStep4 = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 6 * this.MonthStep + offset);
        this.item4.View = newViewFromStep1;
        this.item5.View = newViewFromStep2;
        this.item6.View = newViewFromStep3;
        this.item7.View = newViewFromStep4;
        this.item4.Text = this.item4.View.GetTitleContent();
        this.item5.Text = this.item5.View.GetTitleContent();
        this.item6.Text = this.item6.View.GetTitleContent();
        this.item7.Text = this.item7.View.GetTitleContent();
      }
      if (this.FastNavigationItemsCount > 7)
      {
        this.item8.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 7 * this.MonthStep + offset);
        this.item9.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 8 * this.MonthStep + offset);
        this.item8.Text = this.item8.View.GetTitleContent();
        this.item9.Text = this.item9.View.GetTitleContent();
      }
      if (this.FastNavigationItemsCount > 9)
      {
        this.item10.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 9 * this.MonthStep + offset);
        this.item11.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 10 * this.MonthStep + offset);
        this.item10.Text = this.item10.View.GetTitleContent();
        this.item11.Text = this.item11.View.GetTitleContent();
      }
      if (this.fastNavigationItemsCount > 11)
      {
        this.item12.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 11 * this.MonthStep + offset);
        this.item13.View = this.Calendar.GetNewViewFromStep(-num1 * this.MonthStep + 12 * this.MonthStep + offset);
        this.item12.Text = this.item12.View.GetTitleContent();
        this.item13.Text = this.item13.View.GetTitleContent();
      }
      this.SetNavigationItemsVisibility();
      this.dropDown.Invalidate();
      this.hostedControl.Refresh();
    }

    private void LoadFastNavigationItems()
    {
      this.viewIndex = 0;
      this.InitializeNavItems(0);
      this.hostedControl.Items.Clear();
      if (this.hostedControl.Items.Count == 0)
      {
        this.dropDown.SizingMode = SizingMode.None;
        int num1 = this.FastNavigationItemsCount;
        int num2 = this.Calendar.RangeMaxDate.Year * 12 + this.Calendar.RangeMaxDate.Month - (this.Calendar.RangeMinDate.Year * 12 + this.Calendar.RangeMinDate.Month) + 1;
        if (num1 > num2)
          num1 = num2;
        FastNavigationItem fastNavigationItem1 = (FastNavigationItem) null;
        for (int index = 1; index <= num1; ++index)
        {
          FastNavigationItem fastNavigationItem2 = (FastNavigationItem) null;
          switch (index)
          {
            case 1:
              fastNavigationItem2 = this.item1;
              break;
            case 2:
              fastNavigationItem2 = this.item2;
              break;
            case 3:
              fastNavigationItem2 = this.item3;
              break;
            case 4:
              fastNavigationItem2 = this.item4;
              break;
            case 5:
              fastNavigationItem2 = this.item5;
              break;
            case 6:
              fastNavigationItem2 = this.item6;
              break;
            case 7:
              fastNavigationItem2 = this.item7;
              break;
            case 8:
              fastNavigationItem2 = this.item8;
              break;
            case 9:
              fastNavigationItem2 = this.item9;
              break;
            case 10:
              fastNavigationItem2 = this.item10;
              break;
            case 11:
              fastNavigationItem2 = this.item11;
              break;
            case 12:
              fastNavigationItem2 = this.item12;
              break;
            case 13:
              fastNavigationItem2 = this.item13;
              break;
          }
          if (fastNavigationItem2 != null)
          {
            if (fastNavigationItem1 != null && fastNavigationItem2.View.ViewStartDate == fastNavigationItem1.View.ViewStartDate && fastNavigationItem2.View.ViewEndDate == fastNavigationItem1.View.ViewEndDate)
            {
              fastNavigationItem1 = fastNavigationItem2;
            }
            else
            {
              this.hostedControl.Items.Add((RadItem) fastNavigationItem2);
              fastNavigationItem1 = fastNavigationItem2;
            }
          }
        }
      }
      this.SetNavigationItemsVisibility();
    }

    private void SetNavigationItemsVisibility()
    {
      for (int index = 0; index < this.hostedControl.Items.Count; ++index)
        (this.hostedControl.Items[index] as FastNavigationItem).Visibility = ElementVisibility.Visible;
      for (int index1 = 0; index1 < this.hostedControl.Items.Count; ++index1)
      {
        FastNavigationItem fastNavigationItem1 = this.hostedControl.Items[index1] as FastNavigationItem;
        for (int index2 = index1 + 1; index2 < this.hostedControl.Items.Count; ++index2)
        {
          FastNavigationItem fastNavigationItem2 = this.hostedControl.Items[index2] as FastNavigationItem;
          if (fastNavigationItem1.View.Equals((object) fastNavigationItem2.View))
            fastNavigationItem2.Visibility = ElementVisibility.Collapsed;
        }
      }
      List<FastNavigationItem> fastNavigationItemList = new List<FastNavigationItem>();
      for (int index = 0; index < this.hostedControl.Items.Count; ++index)
      {
        if (this.hostedControl.Items[index].Visibility == ElementVisibility.Visible)
        {
          fastNavigationItemList.Add(this.hostedControl.Items[index] as FastNavigationItem);
          this.hostedControl.Items[index].Visibility = ElementVisibility.Collapsed;
        }
      }
      for (int index = 0; index < this.hostedControl.Items.Count && fastNavigationItemList.Count > index; ++index)
      {
        (this.hostedControl.Items[index] as FastNavigationItem).View = fastNavigationItemList[index].View;
        (this.hostedControl.Items[index] as FastNavigationItem).Text = fastNavigationItemList[index].View.GetTitleContent();
        (this.hostedControl.Items[index] as FastNavigationItem).Visibility = ElementVisibility.Visible;
      }
    }
  }
}
