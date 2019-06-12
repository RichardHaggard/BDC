// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewContextMenuOpeningEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewContextMenuOpeningEventArgs : RadGanttViewCancelEventArgs
  {
    private RadContextMenu menu;

    public GanttViewContextMenuOpeningEventArgs(GanttViewDataItem item, RadContextMenu menu)
      : base(item)
    {
      this.menu = menu;
    }

    public RadContextMenu Menu
    {
      get
      {
        return this.menu;
      }
      set
      {
        this.menu = value;
      }
    }
  }
}
