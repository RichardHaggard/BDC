// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiMonthViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class MultiMonthViewElement : MonthViewElement
  {
    private CalendarMultiMonthViewTableElement tableElement;

    public MultiMonthViewElement(RadCalendar calendar)
      : this(calendar, (CalendarView) null)
    {
    }

    public MultiMonthViewElement(RadCalendar calendar, CalendarView view)
      : base(calendar, view)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (MultiMonthViewElement);
    }

    public override CalendarView View
    {
      get
      {
        return base.View;
      }
      set
      {
        if (base.View == value || value == null)
          return;
        int num1 = base.View.MultiViewColumns * base.View.MultiViewRows;
        int num2 = value.MultiViewColumns * value.MultiViewRows;
        if (num1 == num2 && this.tableElement != null)
        {
          this.tableElement.View = value;
          this.TitleElement.View = value;
          this.TitleElement.Text = value.GetTitleContent();
        }
        base.View = value;
        if (num1 == num2)
          return;
        this.InitializeChildren();
      }
    }

    public CalendarMultiMonthViewTableElement GetMultiTableElement()
    {
      return this.tableElement;
    }

    public override void RefreshVisuals()
    {
      if (this.tableElement == null)
        return;
      this.tableElement.RefreshVisuals();
    }

    public override void RefreshVisuals(bool unconditional)
    {
      if (this.tableElement == null)
        return;
      this.tableElement.RefreshVisuals(unconditional);
    }

    protected internal override void SetProperty(PropertyChangedEventArgs e)
    {
      base.SetProperty(e);
      switch (e.PropertyName)
      {
        case "MonthLayout":
        case "Orientation":
        case "ShowColumnHeaders":
        case "ShowRowHeaders":
        case "HeaderHeight":
        case "HeaderWidth":
        case "MultiViewRows":
        case "MultiViewColumns":
        case "ShowViewSelector":
        case "Columns":
        case "Culture":
        case "Rows":
          if (this.tableElement == null)
            break;
          this.tableElement.Recreate();
          break;
      }
    }

    protected override void InitializeChildren()
    {
      this.DisposeChildren();
      this.dockLayout = new DockLayoutPanel();
      this.TitleElement = new TitleElement();
      int num = (int) this.TitleElement.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
      this.TitleElement.StretchVertically = false;
      this.TitleElement.Text = "Some Text Here";
      this.TitleElement.Visibility = ElementVisibility.Collapsed;
      this.dockLayout.Children.Add((RadElement) this.TitleElement);
      this.tableElement = new CalendarMultiMonthViewTableElement((CalendarVisualElement) this, this.Calendar, this.View);
      this.dockLayout.Children.Add((RadElement) this.tableElement);
      this.Children.Add((RadElement) this.dockLayout);
    }
  }
}
