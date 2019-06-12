// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewCellElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewCellElementProvider : BaseVirtualizedElementProvider<GanttViewTextViewColumn>
  {
    private GanttViewTextItemElement owner;

    public GanttViewTextItemElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public GanttViewTextViewCellElementProvider(GanttViewTextItemElement owner)
    {
      this.owner = owner;
    }

    public override IVirtualizedElement<GanttViewTextViewColumn> CreateElement(
      GanttViewTextViewColumn data,
      object context)
    {
      if (data != null && data.Owner != null)
      {
        GanttViewDataCellElementCreatingEventArgs e = new GanttViewDataCellElementCreatingEventArgs(new GanttViewTextViewCellElement(this.owner, data));
        data.Owner.OnDataCellCreating(e);
        if (e.CellElement != null)
          return (IVirtualizedElement<GanttViewTextViewColumn>) e.CellElement;
      }
      return (IVirtualizedElement<GanttViewTextViewColumn>) new GanttViewTextViewCellElement(this.owner, data);
    }

    public override SizeF GetElementSize(GanttViewTextViewColumn item)
    {
      return new SizeF((float) item.Width, 50f);
    }
  }
}
