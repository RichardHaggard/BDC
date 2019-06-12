// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CardViewContainerElement : LayoutControlContainerElement
  {
    protected override void CreateChildElements()
    {
      this.ShouldHandleMouseInput = false;
      base.CreateChildElements();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RadListView control = this.ElementTree.Control as RadListView;
      CardListViewVisualItem parent = this.Parent as CardListViewVisualItem;
      if (parent != null && parent.ToggleElement.Visibility == ElementVisibility.Visible)
      {
        CheckBoxesPosition checkBoxesPosition = control.CheckBoxesPosition;
        availableSize.Width -= checkBoxesPosition == CheckBoxesPosition.Left || checkBoxesPosition == CheckBoxesPosition.Right ? parent.ToggleElement.DesiredSize.Width : 0.0f;
        availableSize.Height -= checkBoxesPosition == CheckBoxesPosition.Top || checkBoxesPosition == CheckBoxesPosition.Bottom ? parent.ToggleElement.DesiredSize.Height : 0.0f;
      }
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF SubtractControlScrollbarsSize(SizeF availableSize)
    {
      return availableSize;
    }

    protected override void EnsureItemControlAdded(LayoutControlItem controlItem)
    {
      if (controlItem != null)
        throw new ArgumentException("CardView cannot host LayoutControlItems. Use CardViewItem instead.");
    }
  }
}
