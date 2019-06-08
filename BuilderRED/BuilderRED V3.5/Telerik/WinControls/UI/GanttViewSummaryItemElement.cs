// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewSummaryItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewSummaryItemElement : GanttGraphicalViewBaseItemElement
  {
    public GanttViewSummaryItemElement(GanttViewGraphicalViewElement owner)
      : base(owner)
    {
    }

    protected override GanttGraphicalViewBaseTaskElement CreateTaskElement()
    {
      return (GanttGraphicalViewBaseTaskElement) new GanttViewSummaryElement();
    }

    public override bool IsCompatible(GanttViewDataItem data, object context)
    {
      if (data != null && data.Items.Count > 0)
        return data.Start != data.End;
      return false;
    }
  }
}
