// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IconListViewVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class IconListViewVisualItem : BaseListViewVisualItem
  {
    public override bool IsCompatible(ListViewDataItem data, object context)
    {
      return !(data is ListViewDataItemGroup) && data.Owner.ViewType == ListViewType.IconsView;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.TextWrap = true;
    }

    public override void Attach(ListViewDataItem data, object context)
    {
      if (data.Owner.ShowCheckBoxes)
        this.ToggleElement.Visibility = ElementVisibility.Visible;
      else
        this.ToggleElement.Visibility = ElementVisibility.Collapsed;
      base.Attach(data, context);
    }

    protected override RectangleF GetEditorArrangeRectangle(RectangleF clientRect)
    {
      RectangleF bounds = this.Layout.RightPart.Bounds;
      bounds.Width = Math.Min(bounds.Width, this.PreviousConstraint.Width);
      return bounds;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      SizeF desiredSize = base.MeasureOverride(availableSize);
      return this.MeasureContentCore(availableSize, desiredSize);
    }

    protected virtual SizeF MeasureContentCore(SizeF availableSize, SizeF desiredSize)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.dataItem.Owner.ShowCheckBoxes && (this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Left || this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Right))
        num1 = this.ToggleElement.DesiredSize.Width;
      if (this.dataItem.Owner.ShowCheckBoxes && (this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Bottom || this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Top))
        num2 = this.ToggleElement.DesiredSize.Height;
      desiredSize.Width += num1;
      desiredSize.Height += num2;
      if (this.Data.Size.Height > 0)
        desiredSize.Height = (float) this.Data.Size.Height;
      if (this.Data.Size.Width > 0)
        desiredSize.Width = (float) this.Data.Size.Width;
      RadListViewElement owner = this.Data.Owner;
      if (owner != null)
      {
        if (!owner.AllowArbitraryItemHeight && !owner.AllowArbitraryItemWidth)
          desiredSize = (SizeF) owner.ItemSize;
        else if (owner.AllowArbitraryItemHeight && !owner.AllowArbitraryItemWidth)
        {
          desiredSize = base.MeasureOverride(new SizeF((float) owner.ItemSize.Width - num1, availableSize.Height));
          desiredSize.Width = (float) owner.ItemSize.Width;
          desiredSize.Height += num2;
          if (this.Data.Size.Height > 0)
            desiredSize.Height = (float) this.Data.Size.Height;
        }
        else if (!owner.AllowArbitraryItemHeight && owner.AllowArbitraryItemWidth)
        {
          desiredSize = base.MeasureOverride(new SizeF(availableSize.Width, (float) owner.ItemSize.Height - num2));
          desiredSize.Height = (float) owner.ItemSize.Height;
          desiredSize.Width += num1;
          if (this.Data.Size.Width > 0)
            desiredSize.Width = (float) this.Data.Size.Width;
        }
      }
      this.Data.ActualSize = desiredSize.ToSize();
      SizeF size = this.GetClientRectangle(desiredSize).Size;
      RadItem editorElement = this.GetEditorElement((IValueEditor) this.Editor);
      this.Layout.Measure(new SizeF(size.Width - this.ToggleElement.DesiredSize.Width, size.Height));
      if (this.IsInEditMode && editorElement != null)
        editorElement.Measure(this.Layout.RightPart.DesiredSize);
      return desiredSize;
    }
  }
}
