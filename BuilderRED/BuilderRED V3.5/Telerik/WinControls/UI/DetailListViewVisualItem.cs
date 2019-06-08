// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class DetailListViewVisualItem : BaseListViewVisualItem
  {
    private DetailListViewColumnContainer cellContainer;

    public DetailListViewColumnContainer CellsContainer
    {
      get
      {
        return this.cellContainer;
      }
    }

    public override void Attach(ListViewDataItem data, object context)
    {
      if (data.Owner.ShowCheckBoxes)
        this.ToggleElement.Visibility = ElementVisibility.Visible;
      else
        this.ToggleElement.Visibility = ElementVisibility.Collapsed;
      base.Attach(data, context);
      this.cellContainer.Context = this.dataItem.Owner.ViewElement as DetailListViewElement;
      this.cellContainer.DataProvider = (IEnumerable) this.cellContainer.Context.ColumnScroller;
      this.cellContainer.Context.ColumnScroller.ScrollerUpdated += new EventHandler(this.ColumnScroller_ScrollerUpdated);
    }

    public override void Detach()
    {
      base.Detach();
      this.cellContainer.Context.ColumnScroller.ScrollerUpdated -= new EventHandler(this.ColumnScroller_ScrollerUpdated);
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      foreach (DetailListViewCellElement child in this.cellContainer.Children)
        child.Synchronize();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.cellContainer = new DetailListViewColumnContainer();
      this.cellContainer.StretchHorizontally = true;
      this.cellContainer.StretchVertically = true;
      this.cellContainer.Orientation = Orientation.Horizontal;
      this.cellContainer.ElementProvider = (IVirtualizedElementProvider<ListViewDetailColumn>) new DetailListViewDataCellElementProvider(this);
      this.Children.Add((RadElement) this.cellContainer);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF rectangleF1 = new RectangleF(PointF.Empty, finalSize);
      RectangleF rectangleF2 = new RectangleF(clientRectangle.X, clientRectangle.Y, this.ToggleElement.DesiredSize.Width, clientRectangle.Height);
      if (this.ToggleElement.Visibility != ElementVisibility.Collapsed)
      {
        if (this.RightToLeft)
          rectangleF2 = LayoutUtils.RTLTranslateNonRelative(rectangleF2, clientRectangle);
        this.ToggleElement.Arrange(rectangleF2);
      }
      RectangleF rectangleF3 = rectangleF1;
      rectangleF3.Width -= this.ToggleElement.DesiredSize.Width;
      rectangleF3.X += this.ToggleElement.DesiredSize.Width;
      if (this.RightToLeft)
        rectangleF3 = LayoutUtils.RTLTranslateNonRelative(rectangleF3, clientRectangle);
      this.cellContainer.Arrange(rectangleF3);
      if (this.IsInEditMode)
        this.GetEditorElement((IValueEditor) this.Editor).Arrange(this.GetEditorArrangeRectangle(clientRectangle));
      return finalSize;
    }

    protected override RectangleF GetEditorArrangeRectangle(RectangleF clientRect)
    {
      foreach (RadElement child in this.cellContainer.Children)
      {
        DetailListViewDataCellElement viewDataCellElement = child as DetailListViewDataCellElement;
        if (viewDataCellElement != null && viewDataCellElement.Data.Current)
        {
          RectangleF boundingRectangle = (RectangleF) viewDataCellElement.BoundingRectangle;
          if (!this.RightToLeft)
          {
            boundingRectangle.X += (float) this.cellContainer.BoundingRectangle.X;
            boundingRectangle.Width = Math.Min(boundingRectangle.Width, clientRect.Width - (float) this.cellContainer.BoundingRectangle.X);
          }
          else
          {
            boundingRectangle.X += clientRect.Right - (float) this.cellContainer.BoundingRectangle.Right;
            if ((double) boundingRectangle.X < 0.0)
            {
              boundingRectangle.Width += boundingRectangle.X;
              boundingRectangle.X = 0.0f;
            }
          }
          return boundingRectangle;
        }
      }
      return RectangleF.Empty;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      SizeF sizeF = base.MeasureOverride(LayoutUtils.InfinitySize);
      sizeF.Width = this.cellContainer.DesiredSize.Width + this.ToggleElement.DesiredSize.Width;
      if (this.Data.Size.Height > 0)
        sizeF.Height = (float) this.Data.Size.Height;
      RadListViewElement owner = this.Data.Owner;
      if (owner != null && !owner.AllowArbitraryItemHeight)
        sizeF.Height = (float) owner.ItemSize.Height;
      if (owner != null && owner.FullRowSelect || (double) sizeF.Width > (double) clientRectangle.Width)
        sizeF.Width = clientRectangle.Width;
      this.Data.ActualSize = sizeF.ToSize();
      this.cellContainer.Measure((SizeF) this.Data.ActualSize);
      SizeF size = clientRectangle.Size;
      this.ToggleElement.Measure(size);
      RadItem editorElement = this.GetEditorElement((IValueEditor) this.Editor);
      SizeF availableSize1 = new SizeF(size.Width - this.ToggleElement.DesiredSize.Width, size.Height);
      if (this.IsInEditMode && editorElement != null)
      {
        editorElement.Measure(new SizeF(Math.Min(this.Data.Owner.CurrentColumn.Width, availableSize.Width), availableSize1.Height));
        sizeF.Height = Math.Max(sizeF.Height, editorElement.DesiredSize.Height);
      }
      this.Layout.Measure(availableSize1);
      return sizeF;
    }

    public override string Text
    {
      get
      {
        return string.Empty;
      }
      set
      {
        base.Text = value;
      }
    }

    public override bool IsCompatible(ListViewDataItem data, object context)
    {
      return !(data is ListViewDataItemGroup) && data.Owner.ViewType == ListViewType.DetailsView;
    }

    private void ColumnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.cellContainer.InvalidateMeasure();
    }
  }
}
