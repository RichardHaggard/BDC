// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RenderElementEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public sealed class RenderElementEventArgs : EventArgs
  {
    private LightVisualElement element;
    private RadCalendarDay day;
    private CalendarView view;

    public RenderElementEventArgs(
      LightVisualElement cell,
      RadCalendarDay day,
      CalendarView currentView)
    {
      this.day = day;
      this.element = cell;
      this.view = currentView;
    }

    public LightVisualElement Element
    {
      get
      {
        return this.element;
      }
    }

    public RadCalendarDay Day
    {
      get
      {
        return this.day;
      }
    }

    public CalendarView View
    {
      get
      {
        return this.view;
      }
    }
  }
}
