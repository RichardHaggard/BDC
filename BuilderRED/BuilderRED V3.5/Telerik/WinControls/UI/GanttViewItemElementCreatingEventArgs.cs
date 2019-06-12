// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewItemElementCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewItemElementCreatingEventArgs : RadGanttViewEventArgs
  {
    private GanttViewBaseViewElement viewElement;
    private GanttViewBaseItemElement itemElement;

    public GanttViewItemElementCreatingEventArgs(
      GanttViewDataItem item,
      GanttViewBaseViewElement viewElement)
      : base(item)
    {
      this.viewElement = viewElement;
    }

    public GanttViewBaseViewElement ViewElement
    {
      get
      {
        return this.viewElement;
      }
    }

    public GanttViewBaseItemElement ItemElement
    {
      get
      {
        return this.itemElement;
      }
      set
      {
        this.itemElement = value;
      }
    }
  }
}
