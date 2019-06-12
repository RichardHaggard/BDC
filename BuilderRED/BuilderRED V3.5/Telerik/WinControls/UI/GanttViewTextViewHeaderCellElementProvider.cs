// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewHeaderCellElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewHeaderCellElementProvider : BaseVirtualizedElementProvider<GanttViewTextViewColumn>
  {
    public override IVirtualizedElement<GanttViewTextViewColumn> CreateElement(
      GanttViewTextViewColumn data,
      object context)
    {
      if (data != null && data.Owner != null)
      {
        GanttViewHeaderCellElementCreatingEventArgs e = new GanttViewHeaderCellElementCreatingEventArgs(new GanttViewTextViewHeaderCellElement((GanttViewTextItemElement) null, data));
        data.Owner.OnHeaderCellCreating(e);
        if (e.CellElement != null)
          return (IVirtualizedElement<GanttViewTextViewColumn>) e.CellElement;
      }
      return (IVirtualizedElement<GanttViewTextViewColumn>) new GanttViewTextViewHeaderCellElement((GanttViewTextItemElement) null, data);
    }

    public override SizeF GetElementSize(GanttViewTextViewColumn item)
    {
      return new SizeF((float) item.Width, 50f);
    }
  }
}
