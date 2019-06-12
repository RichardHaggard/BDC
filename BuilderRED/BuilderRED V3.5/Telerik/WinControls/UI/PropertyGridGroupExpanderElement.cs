// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupExpanderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupExpanderElement : PropertyGridExpanderElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = (SizeF) Size.Empty;
      PropertyGridGroupElement visualItem = this.VisualItem as PropertyGridGroupElement;
      PropertyGridTableElement gridTableElement = this.PropertyGridTableElement;
      if (gridTableElement != null && visualItem != null && visualItem.Data is PropertyGridGroupItem)
      {
        empty.Width = (float) gridTableElement.ItemIndent;
        empty.Height = !float.IsPositiveInfinity(availableSize.Height) ? availableSize.Height : (float) gridTableElement.ItemHeight;
      }
      return empty;
    }
  }
}
