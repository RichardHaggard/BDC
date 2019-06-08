// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewVirtualizedElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewVirtualizedElementProvider : VirtualizedPanelElementProvider<GanttViewDataItem, GanttViewBaseItemElement>
  {
    private GanttViewBaseViewElement owner;

    public GanttViewVirtualizedElementProvider(GanttViewBaseViewElement owner)
    {
      this.owner = owner;
    }

    public override IVirtualizedElement<GanttViewDataItem> CreateElement(
      GanttViewDataItem data,
      object context)
    {
      GanttViewBaseItemElement viewBaseItemElement = this.OnItemElementCreating(data);
      if (viewBaseItemElement != null)
        return (IVirtualizedElement<GanttViewDataItem>) viewBaseItemElement;
      if (this.owner is GanttViewTextViewElement)
        return (IVirtualizedElement<GanttViewDataItem>) new GanttViewTextItemElement(this.owner as GanttViewTextViewElement);
      if (data.Items.Count > 0)
        return (IVirtualizedElement<GanttViewDataItem>) new GanttViewSummaryItemElement(this.owner as GanttViewGraphicalViewElement);
      if (data.Start == data.End)
        return (IVirtualizedElement<GanttViewDataItem>) new GanttViewMilestoneItemElement(this.owner as GanttViewGraphicalViewElement);
      return (IVirtualizedElement<GanttViewDataItem>) new GanttViewTaskItemElement(this.owner as GanttViewGraphicalViewElement);
    }

    public override bool ShouldUpdate(
      IVirtualizedElement<GanttViewDataItem> element,
      GanttViewDataItem data,
      object context)
    {
      if (element is GanttViewSummaryItemElement && data.Items.Count == 0 || element is GanttViewMilestoneItemElement && data.Start != data.End || element is GanttViewTaskItemElement && (data.Items.Count > 0 || data.Start == data.End))
        return true;
      return base.ShouldUpdate(element, data, context);
    }

    public override SizeF GetElementSize(GanttViewDataItem item)
    {
      return new SizeF(0.0f, (float) this.owner.GanttViewElement.ItemHeight);
    }

    protected virtual GanttViewBaseItemElement OnItemElementCreating(
      GanttViewDataItem item)
    {
      GanttViewItemElementCreatingEventArgs e = new GanttViewItemElementCreatingEventArgs(item, this.owner);
      this.owner.GanttViewElement.OnItemElementCreating(e);
      return e.ItemElement;
    }
  }
}
