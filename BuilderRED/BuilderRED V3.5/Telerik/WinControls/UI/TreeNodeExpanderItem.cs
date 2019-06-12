// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeExpanderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class TreeNodeExpanderItem : ExpanderItem
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register("IsSelected", typeof (bool), typeof (TreeNodeExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register("HotTracking", typeof (bool), typeof (TreeNodeExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private bool signImageSet;

    static TreeNodeExpanderItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TreeNodeExpanderItemStateManager(), typeof (TreeNodeExpanderItem));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.LinkOrientation = ExpanderItem.LinkLineOrientation.Bottom;
      this.LinkLineStyle = DashStyle.Dot;
      this.DrawSignBorder = true;
      this.DrawSignFill = false;
      this.SignSize = (SizeF) new Size(9, 9);
      this.SignPadding = new Padding(1, 1, 1, 1);
      this.SignBorderColor = Color.LightGray;
      this.SignBorderWidth = 1f;
      this.DrawSignBorder = true;
      this.ForeColor = Color.Black;
      this.SignStyle = SignStyles.PlusMinus;
      this.SignBorderColor = Color.Gray;
      this.LinkLineColor = Color.Gray;
    }

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.FindAncestor<TreeNodeElement>();
      }
    }

    public RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.FindAncestor<RadTreeViewElement>();
      }
    }

    public virtual void Synchronize()
    {
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement == null)
        return;
      this.Expanded = nodeElement.Data.Expanded;
      RadTreeViewElement treeViewElement = nodeElement.TreeViewElement;
      if (treeViewElement.FullLazyMode)
      {
        TreeViewShowExpanderEventArgs e = new TreeViewShowExpanderEventArgs(nodeElement.Data, true);
        this.TreeViewElement.OnShowExpander(e);
        this.Visibility = e.ShowExpander ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      else if (!treeViewElement.ShowExpandCollapse || !nodeElement.Data.HasNodes || nodeElement.Data.Parent == null && !treeViewElement.ShowRootLines)
        this.Visibility = ElementVisibility.Collapsed;
      else
        this.Visibility = ElementVisibility.Visible;
      this.LinkLineColor = treeViewElement.LineColor;
      this.LinkOrientation = ExpanderItem.LinkLineOrientation.None;
      this.LinkLineStyle = (DashStyle) treeViewElement.LineStyle;
      if (treeViewElement.ShowLines)
      {
        if (nodeElement.Data.PrevNode != null || nodeElement.Data.Parent != null)
          this.LinkOrientation |= ExpanderItem.LinkLineOrientation.Top | ExpanderItem.LinkLineOrientation.Horizontal;
        if (nodeElement.Data.NextNode != null)
          this.LinkOrientation |= ExpanderItem.LinkLineOrientation.Bottom | ExpanderItem.LinkLineOrientation.Horizontal;
      }
      if (this.TreeViewElement.AllowPlusMinusAnimation)
        this.Opacity = this.TreeViewElement.ContainsMouse ? 1.0 : 0.0;
      else
        this.Opacity = 1.0;
      this.UpdateSignImage();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement != null)
        sizeF.Width = (float) nodeElement.TreeViewElement.TreeIndent;
      return sizeF;
    }

    protected override void ToggleExpanded()
    {
      RadTreeNode data = this.NodeElement.Data;
      bool flag = !data.Expanded;
      data.Expanded = flag;
      if (data.Expanded != flag)
        return;
      this.Expanded = flag;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != ExpanderItem.ExpandedProperty && e.Property != TreeNodeElement.HotTrackingProperty)
        return;
      this.UpdateSignImage();
    }

    protected virtual void UpdateSignImage()
    {
      RadTreeViewElement treeViewElement = this.TreeViewElement;
      if (treeViewElement == null)
        return;
      bool flag = (bool) this.GetValue(TreeNodeElement.HotTrackingProperty);
      if (this.Expanded && (flag && this.SetSignImage((RadElement) treeViewElement, RadTreeViewElement.HoveredExpandImageProperty) || this.SetSignImage((RadElement) treeViewElement, RadTreeViewElement.ExpandImageProperty)) || (flag && this.SetSignImage((RadElement) treeViewElement, RadTreeViewElement.HoveredCollapseImageProperty) || (this.SetSignImage((RadElement) treeViewElement, RadTreeViewElement.CollapseImageProperty) || !this.signImageSet)))
        return;
      this.signImageSet = false;
      int num1 = (int) this.ResetValue(ExpanderItem.SignImageProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(ExpanderItem.DrawSignBorderProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(ExpanderItem.DrawSignFillProperty, ValueResetFlags.Local);
    }

    protected virtual bool SetSignImage(RadElement element, RadProperty property)
    {
      if (element.GetValueSource(property) == ValueSource.DefaultValue)
        return false;
      this.SignImage = (Image) element.GetValue(property);
      this.SignStyle = SignStyles.Image;
      this.DrawSignBorder = false;
      this.DrawSignFill = false;
      this.signImageSet = true;
      return true;
    }

    protected override void PaintSign(IGraphics g, RectangleF signRect)
    {
      if (this.SignStyle == SignStyles.Image)
      {
        if (this.cachedSignImage == null)
          return;
        float num1 = (float) (this.cachedSignImage.Width % 2);
        float num2 = (float) (this.cachedSignImage.Height % 2);
        PointF pos = new PointF((float) Math.Max(0, (this.Size.Width - this.cachedSignImage.Width) / 2), (float) Math.Max(0, (this.Size.Height - this.cachedSignImage.Height) / 2));
        pos.X = Math.Min(pos.X + num1, (float) this.Size.Width);
        pos.Y = Math.Min(pos.Y + num2, (float) this.Size.Height);
        SizeF sz = new SizeF((float) Math.Min(this.Size.Width, this.cachedSignImage.Size.Width), (float) Math.Min(this.Size.Height, this.cachedSignImage.Size.Height));
        this.PaintSignImage(g, pos, sz);
      }
      else
      {
        if ((double) signRect.Width <= (double) this.SignWidth || (double) signRect.Height <= (double) this.SignWidth)
          return;
        int val1 = (int) Math.Round((double) this.SignSize.Width, MidpointRounding.AwayFromZero);
        int val2 = (int) Math.Round((double) this.SignSize.Height, MidpointRounding.AwayFromZero);
        if (this.SquareSignSize)
        {
          val1 = Math.Min(val1, val2);
          val2 = val1;
        }
        int num1 = val1 % 2;
        int num2 = val2 % 2;
        signRect.X += (float) num1;
        signRect.Y += (float) num2;
        this.PaintSignShape(g, signRect);
      }
    }

    protected override void PaintBorder(IGraphics g, RectangleF signBorder)
    {
      float num1 = signBorder.Width % 2f;
      float num2 = signBorder.Height % 2f;
      if (this.DrawSignBorder && (double) this.SignBorderWidth == 1.0)
      {
        num1 = (num1 + 1f) % 2f;
        num2 = (num2 + 1f) % 2f;
      }
      signBorder.X += num1;
      signBorder.Y += num2;
      g.DrawRectangle(signBorder, this.SignBorderColor, PenAlignment.Inset, this.SignBorderWidth);
    }

    protected override void PaintSignLines(IGraphics g, RectangleF signRect, RectangleF signBorder)
    {
      if (this.LinkOrientation == ExpanderItem.LinkLineOrientation.None)
        return;
      Size size = this.Size;
      int num1 = (int) Math.Round((double) size.Width / 2.0, MidpointRounding.AwayFromZero);
      float num2 = signBorder.Width % 2f;
      float num3 = signBorder.Height % 2f;
      if (this.DrawSignBorder && (double) this.SignBorderWidth == 1.0)
      {
        num2 = (num2 + 1f) % 2f;
        num3 = (num3 + 1f) % 2f;
      }
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Top) != ExpanderItem.LinkLineOrientation.None)
      {
        int top = this.Padding.Top;
        int y2 = (int) Math.Round((double) signBorder.Top - 1.0 + (double) num3, MidpointRounding.AwayFromZero);
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, num1, top, num1, y2);
      }
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Bottom) != ExpanderItem.LinkLineOrientation.None)
      {
        int y1 = (int) Math.Round((double) signBorder.Bottom + (double) num3, MidpointRounding.AwayFromZero);
        int y2 = size.Height - this.Padding.Vertical;
        if (this.DrawSignBorder && (double) this.SignBorderWidth == 1.0)
          ++y1;
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, num1, y1, num1, y2);
      }
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Horizontal) == ExpanderItem.LinkLineOrientation.None)
        return;
      int num4 = size.Height / 2 + size.Height % 2;
      if (this.RightToLeft)
      {
        int x2 = (int) Math.Round((double) signBorder.Left, MidpointRounding.AwayFromZero);
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, 0, num4, x2, num4);
      }
      else
      {
        int x1 = (int) Math.Round((double) signBorder.Right + (double) num2, MidpointRounding.AwayFromZero);
        if (this.DrawSignBorder && (double) this.SignBorderWidth == 1.0)
          ++x1;
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, x1, num4, size.Width, num4);
      }
    }
  }
}
