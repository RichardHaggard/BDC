// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseListViewGroupVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BaseListViewGroupVisualItem : BaseListViewVisualItem
  {
    public static RadProperty ExpandedProperty = RadProperty.Register("Expanded", typeof (bool), typeof (BaseListViewGroupVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));

    [Browsable(false)]
    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(BaseListViewGroupVisualItem.ExpandedProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(BaseListViewGroupVisualItem.ExpandedProperty, (object) value);
      }
    }

    static BaseListViewGroupVisualItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ListViewGroupVisualItemStateManagerFactory(), typeof (BaseListViewGroupVisualItem));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ToggleElement.Class = "ListViewGroupToggleButton";
      this.ToggleElement.ThemeRole = "ListViewGroupToggleButton";
      this.ShowHorizontalLine = true;
      this.HorizontalLineColor = Color.FromArgb((int) byte.MaxValue, 207, 214, 225);
      this.AllowDrag = false;
      this.AllowDrop = false;
    }

    public bool HasVisibleItems()
    {
      ListViewTraverser enumerator = this.Data.Owner.ViewElement.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) this.Data;
      return enumerator.MoveNext() && !(enumerator.Current is ListViewDataItemGroup);
    }

    protected override RadToggleButtonElement CreateToggleElement()
    {
      return (RadToggleButtonElement) new ListViewGroupToggleButton();
    }

    public override bool IsCompatible(ListViewDataItem data, object context)
    {
      return data is ListViewDataItemGroup;
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      this.ToggleElement.IsThreeState = false;
      this.IsExpanded = (this.Data as ListViewDataItemGroup).Expanded;
      this.ToggleElement.ToggleState = this.IsExpanded ? ToggleState.On : ToggleState.Off;
    }

    protected override bool OnToggleButtonStateChanging(StateChangingEventArgs args)
    {
      if (this.IsInValidState(true))
      {
        (this.Data as ListViewDataItemGroup).Expanded = args.NewValue == ToggleState.On;
        args.Cancel = (this.Data as ListViewDataItemGroup).Expanded != (args.NewValue == ToggleState.On);
      }
      return args.Cancel;
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      ListViewDataItemGroup data = this.Data as ListViewDataItemGroup;
      data.Expanded = !data.Expanded;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != BaseListViewGroupVisualItem.ExpandedProperty)
        return;
      this.Data.Owner.InvalidateMeasure(true);
      this.Data.Owner.ViewElement.Scroller.UpdateScrollRange();
    }

    protected override void DrawHorizontalLine(IGraphics graphics)
    {
      if (this.Image != null)
        this.DrawHorizontalLineTextWithImage(graphics);
      else
        this.DrawHorizontalLineTextWithoutImage(graphics);
    }

    protected virtual void DrawHorizontalLineTextWithoutImage(IGraphics graphics)
    {
      SizeF desiredSize = this.Layout.RightPart.DesiredSize;
      int x1_1 = this.RightToLeft ? 2 : (int) this.ToggleElement.DesiredSize.Width;
      int y1 = this.Size.Height / 2;
      int x2_1 = 0;
      int y2 = y1;
      switch (this.GetTextAlignment())
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          x1_1 += (int) desiredSize.Width + 10;
          x2_1 = this.Size.Width - (this.RightToLeft ? (int) this.ToggleElement.DesiredSize.Width : 2);
          break;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          ++x1_1;
          x2_1 = (this.RightToLeft ? 0 : (int) this.ToggleElement.DesiredSize.Width) + (this.Size.Width - (int) this.ToggleElement.DesiredSize.Width) / 2 - (int) desiredSize.Width / 2 - 10;
          break;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          ++x1_1;
          x2_1 = this.Size.Width - (int) desiredSize.Width - 10 - (this.RightToLeft ? (int) this.ToggleElement.DesiredSize.Width : 2);
          break;
      }
      if (x1_1 >= x2_1)
        return;
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      using (Pen pen = new Pen(this.HorizontalLineColor, (float) this.HorizontalLineWidth))
      {
        underlayGraphics.DrawLine(pen, x1_1, y1, x2_1, y2);
        if (this.TextAlignment != ContentAlignment.MiddleCenter && this.TextAlignment != ContentAlignment.TopCenter && this.TextAlignment != ContentAlignment.BottomCenter)
          return;
        int x1_2 = (this.RightToLeft ? 0 : (int) this.ToggleElement.DesiredSize.Width) + (int) ((double) this.Size.Width - (double) this.ToggleElement.DesiredSize.Width) / 2 + (int) desiredSize.Width / 2 + 10;
        int x2_2 = this.Size.Width - (this.RightToLeft ? (int) this.ToggleElement.DesiredSize.Width : 2);
        underlayGraphics.DrawLine(pen, x1_2, y1, x2_2, y2);
      }
    }

    protected virtual void DrawHorizontalLineTextWithImage(IGraphics graphics)
    {
      int x1_1 = 0;
      int y1 = this.Size.Height / 2;
      int x2_1 = 0;
      int y2 = y1;
      switch (this.GetTextAlignment())
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          x1_1 = (int) this.Layout.RightPart.Bounds.Right + 10;
          x2_1 = this.Size.Width - (this.RightToLeft ? (int) this.ToggleElement.DesiredSize.Width : 2);
          break;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          x1_1 = this.RightToLeft ? 2 : (int) this.ToggleElement.DesiredSize.Width;
          x2_1 = (int) this.Layout.RightPart.Bounds.Left - 10;
          break;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          x1_1 = this.RightToLeft ? 2 : (int) this.ToggleElement.DesiredSize.Width;
          x2_1 = (int) this.Layout.RightPart.Bounds.Left - 12;
          break;
      }
      if (x1_1 >= x2_1)
        return;
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      using (Pen pen = new Pen(this.HorizontalLineColor, (float) this.HorizontalLineWidth))
      {
        underlayGraphics.DrawLine(pen, x1_1, y1, x2_1, y2);
        if (this.TextAlignment != ContentAlignment.MiddleCenter && this.TextAlignment != ContentAlignment.TopCenter && this.TextAlignment != ContentAlignment.BottomCenter)
          return;
        int x1_2 = (int) this.Layout.RightPart.Bounds.Right + 10;
        int x2_2 = this.Size.Width - (this.RightToLeft ? (int) this.ToggleElement.DesiredSize.Width : 2);
        underlayGraphics.DrawLine(pen, x1_2, y1, x2_2, y2);
      }
    }

    protected override void DrawHorizontalLineWithoutText(IGraphics graphics)
    {
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      using (Pen pen = new Pen(this.HorizontalLineColor, (float) this.HorizontalLineWidth))
      {
        Point pt1 = new Point(this.ToggleElement.Size.Width, this.Size.Height / 2);
        Point pt2 = new Point(this.Size.Width, this.Size.Height / 2);
        if (this.RightToLeft)
        {
          pt1.X = 0;
          pt2.X -= this.ToggleElement.Size.Width;
        }
        underlayGraphics.DrawLine(pen, pt1, pt2);
      }
    }

    protected override void PaintContent(IGraphics graphics)
    {
      this.PaintBackgroundImage(graphics);
      if (string.IsNullOrEmpty(this.Text) || !this.DrawText)
      {
        if (!this.ShowHorizontalLine)
          return;
        this.DrawHorizontalLineWithoutText(graphics);
      }
      else
      {
        if (this.ShowHorizontalLine)
          this.DrawHorizontalLine(graphics);
        this.PaintImage(graphics);
        this.PaintText(graphics);
      }
    }

    protected override void PaintText(IGraphics graphics)
    {
      this.PaintTextCore(graphics);
    }
  }
}
