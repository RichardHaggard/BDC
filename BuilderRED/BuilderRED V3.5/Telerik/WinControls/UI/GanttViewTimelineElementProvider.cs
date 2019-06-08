// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineElementProvider : BaseVirtualizedElementProvider<GanttViewTimelineDataItem>
  {
    private GanttViewGraphicalViewElement owner;

    public GanttViewGraphicalViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public GanttViewTimelineElementProvider(GanttViewGraphicalViewElement owner)
    {
      this.owner = owner;
    }

    public override IVirtualizedElement<GanttViewTimelineDataItem> CreateElement(
      GanttViewTimelineDataItem data,
      object context)
    {
      return (IVirtualizedElement<GanttViewTimelineDataItem>) this.OnItemElementCreating(data) ?? (IVirtualizedElement<GanttViewTimelineDataItem>) new GanttViewTimelineItemElement(data, this.owner);
    }

    public override SizeF GetElementSize(GanttViewTimelineDataItem item)
    {
      return new SizeF(item.Width, 0.0f);
    }

    protected virtual GanttViewTimelineItemElement OnItemElementCreating(
      GanttViewTimelineDataItem item)
    {
      GanttViewTimelineItemElementCreatingEventArgs e = new GanttViewTimelineItemElementCreatingEventArgs(item);
      this.owner.GanttViewElement.OnTimelineItemElementCreating(e);
      return e.ItemElement;
    }
  }
}
