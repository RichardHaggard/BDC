// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SimpleListViewVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class SimpleListViewVisualItem : BaseListViewVisualItem
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Alignment = ContentAlignment.MiddleLeft;
    }

    public override void Attach(ListViewDataItem data, object context)
    {
      if (data.Owner.ShowCheckBoxes)
        this.ToggleElement.Visibility = ElementVisibility.Visible;
      else
        this.ToggleElement.Visibility = ElementVisibility.Collapsed;
      base.Attach(data, context);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      float num = 0.0f;
      if (this.Data.Owner.ShowGroups && (this.Data.Owner.EnableCustomGrouping || this.Data.Owner.EnableGrouping) && this.Data.Owner.Groups.Count > 0)
        num = (float) this.Data.Owner.GroupIndent;
      SizeF finalSize = base.MeasureOverride(LayoutUtils.InfinitySize);
      finalSize.Width += this.ToggleElement.DesiredSize.Width;
      if (this.Data.Size.Height > 0)
        finalSize.Height = (float) this.Data.Size.Height;
      if (this.Data.Size.Width > 0)
        finalSize.Width = (float) this.Data.Size.Width;
      RadListViewElement owner = this.Data.Owner;
      if (owner != null && !owner.AllowArbitraryItemWidth)
        finalSize.Width = (float) owner.ItemSize.Width;
      if (owner != null && !owner.AllowArbitraryItemHeight)
        finalSize.Height = (float) owner.ItemSize.Height;
      if (owner != null && owner.FullRowSelect)
        finalSize.Width = Math.Max(float.IsInfinity(availableSize.Width) ? 0.0f : this.GetClientRectangle(availableSize).Width, finalSize.Width + num);
      if (owner != null && owner.AllowArbitraryItemHeight && !owner.AllowArbitraryItemWidth)
      {
        SizeF sizeF = base.MeasureOverride(new SizeF(finalSize.Width, float.PositiveInfinity));
        if (this.dataItem.Owner.ShowCheckBoxes && (this.dataItem.Owner.CheckBoxesPosition == CheckBoxesPosition.Top || this.dataItem.Owner.CheckBoxesPosition == CheckBoxesPosition.Bottom))
          sizeF.Height += this.ToggleElement.DesiredSize.Height;
        finalSize.Height = sizeF.Height;
      }
      if (owner != null && owner.AllowArbitraryItemWidth && !owner.AllowArbitraryItemHeight)
      {
        SizeF sizeF = base.MeasureOverride(new SizeF(float.PositiveInfinity, finalSize.Height));
        if (this.dataItem.Owner.ShowCheckBoxes && (this.dataItem.Owner.CheckBoxesPosition == CheckBoxesPosition.Left || this.dataItem.Owner.CheckBoxesPosition == CheckBoxesPosition.Right))
          sizeF.Width += this.ToggleElement.DesiredSize.Width;
        finalSize.Width = sizeF.Width;
      }
      SizeF size = this.GetClientRectangle(finalSize).Size;
      RadItem editorElement = this.GetEditorElement((IValueEditor) this.Editor);
      SizeF availableSize1 = new SizeF(size.Width - this.ToggleElement.DesiredSize.Width, size.Height);
      if (this.IsInEditMode && editorElement != null)
      {
        float width = Math.Min(size.Width - this.ToggleElement.DesiredSize.Width - num, availableSize.Width - num);
        editorElement.Measure(new SizeF(width, float.PositiveInfinity));
        finalSize.Height = Math.Max(finalSize.Height, editorElement.DesiredSize.Height);
        availableSize1.Height = finalSize.Height;
      }
      this.Layout.Measure(availableSize1);
      this.Data.ActualSize = finalSize.ToSize();
      return finalSize;
    }

    protected override void ArrangeContentCore(RectangleF clientRect)
    {
      if (this.Data.Owner.ShowGroups && (this.Data.Owner.EnableCustomGrouping || this.Data.Owner.EnableGrouping) && (this.Data.Owner.Groups.Count > 0 && this.Data.Owner.FullRowSelect))
      {
        if (!this.RightToLeft)
          clientRect.X += (float) this.Data.Owner.GroupIndent;
        clientRect.Width -= (float) this.Data.Owner.GroupIndent;
      }
      base.ArrangeContentCore(clientRect);
    }

    protected override RectangleF GetEditorArrangeRectangle(RectangleF clientRect)
    {
      RectangleF bounds = new RectangleF(clientRect.X + this.ToggleElement.DesiredSize.Width, clientRect.Y, clientRect.Width - this.ToggleElement.DesiredSize.Width, clientRect.Height);
      if ((double) bounds.Width > (double) this.Data.Owner.ViewElement.ViewElement.DesiredSize.Width)
        bounds.Width = this.Data.Owner.ViewElement.ViewElement.DesiredSize.Width - clientRect.X;
      if (this.Data.Owner.ShowGroups && (this.Data.Owner.EnableCustomGrouping || this.Data.Owner.EnableGrouping) && (this.Data.Owner.Groups.Count > 0 && !this.Data.Owner.FullRowSelect))
        bounds.Width -= (float) this.Data.Owner.GroupIndent;
      if (this.RightToLeft)
        bounds = LayoutUtils.RTLTranslateNonRelative(bounds, clientRect);
      return bounds;
    }
  }
}
