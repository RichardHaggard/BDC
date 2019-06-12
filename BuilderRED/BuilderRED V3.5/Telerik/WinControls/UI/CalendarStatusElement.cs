// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarStatusElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CalendarStatusElement : CalendarVisualElement
  {
    private string labelFormat = string.Empty;
    private RadButtonElement todayButtonElement;
    private RadButtonElement clearButtonElement;
    private Timer timer;
    private RadCalendarElement owner;
    private RadLabelElement label;
    private DockLayoutPanel dockLayout;

    internal CalendarStatusElement(RadCalendarElement owner)
      : base((CalendarVisualElement) owner)
    {
      this.owner = owner;
      this.owner.Calendar.PropertyChanged += new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Visibility = ElementVisibility.Collapsed;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.DrawBorder = false;
      this.DrawFill = false;
      this.Margin = new Padding(10, 5, 10, 5);
      this.StretchVertically = false;
      this.DrawFill = true;
      this.Class = nameof (CalendarStatusElement);
      this.timer = new Timer();
      this.timer.Interval = 1000;
      this.timer.Enabled = true;
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.dockLayout = new DockLayoutPanel();
      this.dockLayout.StretchHorizontally = true;
      this.dockLayout.StretchVertically = false;
      this.label = new RadLabelElement();
      int num1 = (int) this.label.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 2 : 0));
      this.label.Class = "FooterDate";
      this.label.TextWrap = false;
      this.todayButtonElement = new RadButtonElement();
      this.todayButtonElement.Text = "Today";
      int num2 = (int) this.todayButtonElement.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 0 : 2));
      this.todayButtonElement.Margin = new Padding(5, 0, 5, 0);
      this.todayButtonElement.Click += new EventHandler(this.todayButtonElement_Click);
      this.clearButtonElement = new RadButtonElement();
      this.clearButtonElement.Text = "Clear";
      int num3 = (int) this.clearButtonElement.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 0 : 2));
      this.clearButtonElement.Margin = new Padding(5, 0, 5, 0);
      this.clearButtonElement.Visibility = ElementVisibility.Visible;
      this.clearButtonElement.Click += new EventHandler(this.clearButtonElement_Click);
      this.dockLayout.Children.Add((RadElement) this.todayButtonElement);
      this.dockLayout.Children.Add((RadElement) this.clearButtonElement);
      this.dockLayout.Children.Add((RadElement) this.label);
      this.Children.Add((RadElement) this.dockLayout);
    }

    protected override void DisposeManagedResources()
    {
      if (this.owner != null && this.owner.Calendar != null)
        this.owner.Calendar.PropertyChanged -= new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
      if (this.timer != null)
      {
        this.timer.Tick -= new EventHandler(this.timer_Tick);
        this.timer.Dispose();
      }
      base.DisposeManagedResources();
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

    public RadButtonElement TodayButton
    {
      get
      {
        return this.todayButtonElement;
      }
    }

    public RadButtonElement ClearButton
    {
      get
      {
        return this.clearButtonElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement TodayButtonElement
    {
      get
      {
        return this.todayButtonElement;
      }
      set
      {
        this.todayButtonElement = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadLabelElement LabelElement
    {
      get
      {
        return this.label;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string LabelFormat
    {
      get
      {
        return this.labelFormat;
      }
      set
      {
        this.labelFormat = value;
      }
    }

    public void HideText()
    {
      this.StopTimer();
      this.label.Text = "";
    }

    private void todayButtonElement_Click(object sender, EventArgs e)
    {
      this.owner.Calendar.FocusedDate = DateTime.Now;
    }

    private void clearButtonElement_Click(object sender, EventArgs e)
    {
      this.Calendar.SelectedDates.Clear();
      if (this.Calendar.CalendarElement.CalendarVisualElement is MultiMonthViewElement)
      {
        foreach (MonthViewElement child in this.Calendar.CalendarElement.CalendarVisualElement.Children[0].Children[1].Children)
          CalendarStatusElement.ClearMonthSelection(child);
      }
      else
      {
        if (!(this.Calendar.CalendarElement.CalendarVisualElement is MonthViewElement))
          return;
        CalendarStatusElement.ClearMonthSelection(this.Calendar.CalendarElement.CalendarVisualElement as MonthViewElement);
      }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (this.Calendar.ShowFooter)
        this.label.Text = DateTime.Now.ToString(this.labelFormat, (IFormatProvider) this.Calendar.Culture);
      else
        this.StopTimer();
    }

    private void Calendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ShowFooter"))
        return;
      if (this.owner.Calendar.ShowFooter)
        this.Visibility = ElementVisibility.Visible;
      else
        this.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      int num1 = (int) this.label.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 2 : 0));
      int num2 = (int) this.todayButtonElement.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 0 : 2));
      int num3 = (int) this.clearButtonElement.SetValue(DockLayoutPanel.DockProperty, (object) (Dock) (this.RightToLeft ? 0 : 2));
    }

    protected internal virtual void StartTimer()
    {
      this.timer.Start();
    }

    protected internal virtual void StopTimer()
    {
      this.timer.Stop();
    }

    private static void ClearMonthSelection(MonthViewElement currentMonth)
    {
      if (currentMonth.Calendar != null)
        currentMonth.Calendar.SelectedDates.Clear();
      for (int index = 0; index < currentMonth.TableElement.Children.Count; ++index)
      {
        CalendarCellElement child = currentMonth.TableElement.Children[index] as CalendarCellElement;
        if (child != null)
        {
          child.isChecked = false;
          child.Selected = false;
        }
      }
    }
  }
}
