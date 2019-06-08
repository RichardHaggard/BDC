// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollViewElement : ScrollViewElement<ScrollViewElementContainer>
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.HScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
      this.VScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
    }

    private void ScrollBar_ValueChanged(object sender, EventArgs e)
    {
      if (this.ViewElement.Children.Count == 1)
        this.ViewElement.Children[0].PositionOffset = new SizeF((float) -this.GetScrollBarValue(this.HScrollBar), (float) -this.GetScrollBarValue(this.VScrollBar));
      this.Invalidate();
    }

    private int GetScrollBarValue(RadScrollBarElement scrollBar)
    {
      if (scrollBar.Visibility == ElementVisibility.Visible)
        return scrollBar.Value;
      return scrollBar.Minimum;
    }

    protected override SizeF MeasureView(SizeF availableSize)
    {
      this.GetClientRectangle(availableSize);
      this.ViewElement.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
      this.HScrollBar.Measure(availableSize);
      this.VScrollBar.Measure(availableSize);
      SizeF offset = this.GetOffset();
      SizeF sizeF = new SizeF(this.ViewElement.DesiredSize.Width + offset.Width, this.ViewElement.DesiredSize.Height + offset.Height);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }

    private SizeF GetOffset()
    {
      Padding padding = this.Padding;
      SizeF sizeF = new SizeF((float) padding.Horizontal, (float) padding.Vertical);
      if (this.DrawBorder)
      {
        Padding borderThickness = this.GetBorderThickness(false);
        sizeF.Width += (float) borderThickness.Horizontal;
        sizeF.Height += (float) borderThickness.Vertical;
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.UpdateScrollbars(finalSize);
      return finalSize;
    }

    private void UpdateScrollbars(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF rectangleF = clientRectangle;
      RadElement radElement = (RadElement) null;
      if (this.ViewElement.Children.Count == 1)
        radElement = this.ViewElement.Children[0];
      ElementVisibility visibility1 = this.HScrollBar.Visibility;
      ElementVisibility visibility2 = this.VScrollBar.Visibility;
      if (((double) this.ViewElement.DesiredSize.Width > (double) clientRectangle.Width ? 0 : 2) == 0)
        clientRectangle.Height = rectangleF.Height - this.HScrollBar.DesiredSize.Height;
      ElementVisibility elementVisibility1 = (double) this.ViewElement.DesiredSize.Height > (double) clientRectangle.Height ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      if (elementVisibility1 == ElementVisibility.Visible)
        clientRectangle.Width = rectangleF.Width - this.VScrollBar.DesiredSize.Width;
      ElementVisibility elementVisibility2 = (double) this.ViewElement.DesiredSize.Width > (double) clientRectangle.Width ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      if (elementVisibility2 == ElementVisibility.Visible)
        clientRectangle.Height = rectangleF.Height - this.HScrollBar.DesiredSize.Height;
      if (elementVisibility2 != ElementVisibility.Visible && radElement != null)
        radElement.PositionOffset = new SizeF(0.0f, radElement.PositionOffset.Height);
      if (elementVisibility1 != ElementVisibility.Visible && radElement != null)
        radElement.PositionOffset = new SizeF(radElement.PositionOffset.Width, 0.0f);
      this.HScrollBar.Maximum = (int) this.ViewElement.DesiredSize.Width;
      this.HScrollBar.LargeChange = (int) clientRectangle.Width;
      this.HScrollBar.Visibility = elementVisibility2;
      this.VScrollBar.Maximum = (int) this.ViewElement.DesiredSize.Height;
      this.VScrollBar.LargeChange = (int) clientRectangle.Height;
      this.VScrollBar.Visibility = elementVisibility1;
    }
  }
}
