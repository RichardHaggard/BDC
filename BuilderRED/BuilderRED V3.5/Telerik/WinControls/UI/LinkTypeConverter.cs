// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LinkTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class LinkTypeConverter
  {
    private RadGanttViewElement ganttViewElement;

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttViewElement;
      }
      internal set
      {
        this.ganttViewElement = value;
      }
    }

    public virtual TasksLinkType ConvertToLinkType(object value)
    {
      return (TasksLinkType) Convert.ToInt32(value);
    }

    public virtual object ConvertFromLinkType(TasksLinkType linkType)
    {
      return (object) (int) linkType;
    }
  }
}
