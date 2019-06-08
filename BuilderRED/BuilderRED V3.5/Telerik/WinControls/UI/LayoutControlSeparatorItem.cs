// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlSeparatorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class LayoutControlSeparatorItem : LayoutControlItemBase
  {
    public static RadProperty ThicknessProperty = RadProperty.Register(nameof (Thickness), typeof (int), typeof (LayoutControlSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private SeparatorElement separatorElement;

    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("Thickness", typeof (LayoutControlSeparatorItem))]
    [Browsable(true)]
    public int Thickness
    {
      get
      {
        return (int) this.GetValue(LayoutControlSeparatorItem.ThicknessProperty);
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlSeparatorItem.ThicknessProperty, (object) value);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Size = new Size(4, 4);
      this.Padding = new Padding(0);
      this.CreateVisualItem();
    }

    protected virtual void CreateVisualItem()
    {
      this.separatorElement = new SeparatorElement();
      this.Children.Add((RadElement) this.separatorElement);
    }

    public Orientation Orientation
    {
      get
      {
        LayoutControlContainerElement ancestor = this.FindAncestor<LayoutControlContainerElement>();
        if (ancestor != null)
        {
          LayoutTreeNode nodeByItem = ancestor.LayoutTree.FindNodeByItem((LayoutControlItemBase) this);
          if (nodeByItem != null && nodeByItem.Parent != null && nodeByItem.Parent.SplitType == Orientation.Vertical)
            return Orientation.Horizontal;
        }
        return Orientation.Vertical;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.UpdateVisualItemOrientation();
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (!this.AutoSize)
        this.MeasureOverride(finalSize);
      return base.ArrangeOverride(finalSize);
    }

    protected virtual void UpdateVisualItemOrientation()
    {
      this.separatorElement.Orientation = this.Orientation;
    }

    public override Size MinSize
    {
      get
      {
        return this.MaxSize;
      }
      set
      {
        base.MinSize = value;
      }
    }

    public override Size MaxSize
    {
      get
      {
        if (this.Orientation != Orientation.Vertical)
          return new Size(0, this.Thickness);
        return new Size(this.Thickness, 0);
      }
      set
      {
        base.MaxSize = value;
      }
    }
  }
}
