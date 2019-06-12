// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewBaseViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewBaseViewElement : VirtualizedScrollPanel<GanttViewDataItem, GanttViewBaseItemElement>
  {
    private RadGanttViewElement ganttView;

    public GanttViewBaseViewElement(RadGanttViewElement ganttView)
    {
      this.ganttView = ganttView;
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttView;
      }
    }

    protected override IVirtualizedElementProvider<GanttViewDataItem> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<GanttViewDataItem>) new GanttViewVirtualizedElementProvider(this);
    }
  }
}
