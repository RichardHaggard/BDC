// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MonthViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class MonthViewElement : CalendarVisualElement
  {
    private CalendarTableElement tableElement;
    private TitleElement titleElement;
    private CalendarView view;
    protected DockLayoutPanel dockLayout;
    internal int Row;
    internal int Column;

    public MonthViewElement(RadCalendar calendar)
      : this(calendar, (CalendarView) null)
    {
    }

    public MonthViewElement(RadCalendar calendar, CalendarView view)
      : base(calendar, view)
    {
      this.view = view;
      this.view.PropertyChanged += new PropertyChangedEventHandler(this.view_PropertyChanged);
      this.InitializeChildren();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (MonthViewElement);
      this.DrawFill = false;
      this.DrawBorder = false;
      this.ClipDrawing = true;
    }

    protected override void DisposeManagedResources()
    {
      if (this.View != null)
      {
        this.view.PropertyChanged -= new PropertyChangedEventHandler(this.view_PropertyChanged);
        this.Calendar = (RadCalendar) null;
        this.view = (CalendarView) null;
        if (this.tableElement != null)
        {
          this.tableElement.Dispose();
          this.tableElement = (CalendarTableElement) null;
        }
        if (this.titleElement != null)
        {
          this.titleElement.Dispose();
          this.titleElement = (TitleElement) null;
        }
      }
      base.DisposeManagedResources();
    }

    public override CalendarView View
    {
      get
      {
        return this.view;
      }
      set
      {
        if (this.view == value || value == null)
          return;
        int num1 = this.view.Columns * this.view.Rows;
        int num2 = value.Columns * value.Rows;
        if (num1 == num2 && this.tableElement != null)
        {
          this.tableElement.View = value;
          this.titleElement.View = value;
          this.titleElement.Text = value.GetTitleContent();
        }
        this.view = value;
        if (num1 == num2)
          return;
        this.InitializeChildren();
      }
    }

    public virtual CalendarTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
      set
      {
        this.tableElement = value;
      }
    }

    public TitleElement TitleElement
    {
      get
      {
        return this.titleElement;
      }
      set
      {
        this.titleElement = value;
      }
    }

    public virtual void Initialize()
    {
    }

    private void view_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.SetProperty(e);
    }

    protected internal virtual void SetProperty(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "ShowViewHeader":
          if (this.titleElement == null)
            break;
          this.titleElement.Visibility = this.View.ShowHeader ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case "Orientation":
          if (this.tableElement == null)
            break;
          this.tableElement.Recreate();
          break;
      }
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

    protected internal virtual void RenderContent(
      DateTime firstDay,
      DateTime visibleDate,
      Orientation orientation)
    {
    }

    protected internal virtual void CreateVisuals()
    {
    }

    protected virtual void InitializeChildren()
    {
      this.DisposeChildren();
      this.dockLayout = new DockLayoutPanel();
      this.titleElement = new TitleElement();
      int num = (int) this.titleElement.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
      this.titleElement.StretchVertically = false;
      this.titleElement.Text = "Some Text Here";
      this.titleElement.Visibility = ElementVisibility.Collapsed;
      this.dockLayout.Children.Add((RadElement) this.titleElement);
      this.CreateTableElement();
      this.Children.Add((RadElement) this.dockLayout);
    }

    private void CreateTableElement()
    {
      this.tableElement = new CalendarTableElement((CalendarVisualElement) this, this.Calendar, this.View);
      this.dockLayout.Children.Add((RadElement) this.tableElement);
    }
  }
}
