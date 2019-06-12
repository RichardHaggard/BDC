// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HorizontalScrollableStackElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class HorizontalScrollableStackElement : LightVisualElement
  {
    private Rectangle clickRect = Rectangle.Empty;
    private RadScrollBarElement scrollBar;
    private StackLayoutPanel itemsLayout;
    private ScrollService scrollService;
    private DateTime lastScroll;
    private int bufferedSteps;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.itemsLayout = new StackLayoutPanel();
      this.Children.Add((RadElement) this.itemsLayout);
      this.scrollBar = new RadScrollBarElement();
      this.scrollBar.ScrollType = ScrollType.Horizontal;
      this.scrollBar.ValueChanged += new EventHandler(this.scrollBar_ValueChanged);
      this.Children.Add((RadElement) this.scrollBar);
      this.scrollService = new ScrollService((RadElement) this, this.scrollBar);
      this.scrollService.EnableInertia = true;
    }

    public StackLayoutPanel ItemsLayout
    {
      get
      {
        return this.itemsLayout;
      }
    }

    public RadScrollBarElement ScrollBar
    {
      get
      {
        return this.scrollBar;
      }
    }

    public ScrollService ScrollService
    {
      get
      {
        return this.scrollService;
      }
    }

    protected virtual void UpdateScrollBar(SizeF availableSize)
    {
      this.scrollBar.Maximum = (int) this.GetPanelSize().Width;
      this.scrollBar.Minimum = 0;
      this.scrollBar.LargeChange = (int) availableSize.Width;
      this.scrollBar.SmallChange = 1;
      if (this.scrollBar.LargeChange >= this.scrollBar.Maximum)
        this.scrollBar.Value = 0;
      else if (this.scrollBar.Value > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
        this.scrollBar.Value = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      this.scrollBar.Visibility = ElementVisibility.Hidden;
    }

    private SizeF GetPanelSize()
    {
      return this.itemsLayout.DesiredSize;
    }

    private LayoutPanel GetCurrentLayout()
    {
      return (LayoutPanel) this.itemsLayout;
    }

    public void ScrollView(int offset)
    {
      this.ScrollView(offset, false);
    }

    public void ScrollView(int offset, bool buffered)
    {
      DateTime now = DateTime.Now;
      this.bufferedSteps += offset;
      if (buffered && now - this.lastScroll < TimeSpan.FromMilliseconds(50.0))
        return;
      this.lastScroll = now;
      int num = this.scrollBar.Value - this.bufferedSteps;
      this.bufferedSteps = 0;
      if (num > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
        num = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      if (num < this.scrollBar.Minimum)
        num = this.scrollBar.Minimum;
      this.scrollBar.Value = num;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      this.scrollBar.Measure(new SizeF(this.GetClientRectangle(availableSize).Size.Width, 0.0f));
      this.GetCurrentLayout().Measure(LayoutUtils.InfinitySize);
      SizeF desiredSize = this.GetCurrentLayout().DesiredSize;
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = desiredSize.Width;
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = desiredSize.Height;
      this.UpdateScrollBar(availableSize);
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.scrollBar.Arrange(new RectangleF(new PointF(clientRectangle.Left, clientRectangle.Bottom), new SizeF(clientRectangle.Width, 0.0f)));
      this.GetCurrentLayout().Arrange(new RectangleF(clientRectangle.Location, this.GetPanelSize()));
      return finalSize;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (args.Handled)
        return;
      this.ScrollView(args.Offset.Width, true);
      args.Handled = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = true;
      this.clickRect = new Rectangle(new Point(e.Location.X - SystemInformation.DragSize.Width / 2, e.Location.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
      this.scrollService.MouseDown(e.Location);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.scrollService.MouseUp(e.Location);
      this.Capture = false;
      if (this.clickRect.Contains(e.Location))
        this.ElementTree.GetElementAtPoint(e.Location)?.CallDoClick((EventArgs) e);
      this.clickRect = Rectangle.Empty;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture)
        return;
      this.scrollService.MouseMove(e.Location);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.scrollBar.Value = Math.Max(0, Math.Min(this.scrollBar.Maximum - this.scrollBar.LargeChange + 1, this.scrollBar.Value + Math.Sign(e.Delta) * -30));
    }

    public void UpdateViewOnScroll()
    {
      this.GetCurrentLayout().PositionOffset = new SizeF((float) -this.scrollBar.Value, 0.0f);
    }

    private void scrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.UpdateViewOnScroll();
    }
  }
}
