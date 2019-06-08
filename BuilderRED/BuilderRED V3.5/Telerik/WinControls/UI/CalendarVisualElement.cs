// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class CalendarVisualElement : LightVisualElement
  {
    private CalendarView view;
    private RadCalendar calendar;
    private CalendarVisualElement owner;

    public CalendarVisualElement(RadCalendar calendar)
      : this((CalendarVisualElement) null, calendar, (CalendarView) null)
    {
    }

    public CalendarVisualElement(RadCalendar calendar, CalendarView view)
      : this((CalendarVisualElement) null, calendar, view)
    {
    }

    public CalendarVisualElement(CalendarVisualElement owner)
      : this(owner, (RadCalendar) null, (CalendarView) null)
    {
    }

    public CalendarVisualElement(
      CalendarVisualElement owner,
      RadCalendar calendar,
      CalendarView view)
    {
      this.calendar = calendar;
      this.owner = owner;
      this.view = view;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.DrawBorder = true;
      this.Class = nameof (CalendarVisualElement);
    }

    protected override void DisposeManagedResources()
    {
      this.view = (CalendarView) null;
      this.calendar = (RadCalendar) null;
      this.owner = (CalendarVisualElement) null;
      base.DisposeManagedResources();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Data")]
    public virtual CalendarView View
    {
      get
      {
        if (this.Owner != null)
          return this.Owner.View;
        return this.view;
      }
      set
      {
        if (this.view == value)
          return;
        this.view = value;
      }
    }

    [Description("Gets the parent calendar that the current view is assigned to.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(null)]
    public virtual RadCalendar Calendar
    {
      get
      {
        if (this.Owner != null)
          return this.Owner.Calendar;
        return this.calendar;
      }
      internal set
      {
        if (this.calendar == value)
          return;
        this.calendar = value;
      }
    }

    protected internal virtual CalendarVisualElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    protected internal virtual void RenderVisuals()
    {
    }

    public virtual void RefreshVisuals()
    {
      this.RefreshVisuals(false);
    }

    public virtual void RefreshVisuals(bool unconditional)
    {
    }
  }
}
