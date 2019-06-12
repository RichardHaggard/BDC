// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewItemExpanderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewItemExpanderItem : ExpanderItem
  {
    private GanttViewTextItemElement itemElement;

    public GanttViewTextViewItemExpanderItem(GanttViewTextItemElement itemElement)
    {
      this.itemElement = itemElement;
    }

    public override bool Expanded
    {
      get
      {
        return base.Expanded;
      }
      set
      {
        base.Expanded = value;
        this.itemElement.Data.Expanded = value;
      }
    }

    public GanttViewTextItemElement ItemElement
    {
      get
      {
        return this.itemElement;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.itemElement.TextView != null)
        sizeF.Width = (float) this.itemElement.TextView.Indent;
      return sizeF;
    }
  }
}
