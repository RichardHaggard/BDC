// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MenuImageAndTextLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class MenuImageAndTextLayout : ImageAndTextLayoutPanel
  {
    private RadElement imageElement;
    private RadElement textElement;

    private RadDropDownMenuLayout MenuLayout
    {
      get
      {
        RadMenuItemLayout parent1 = this.Parent as RadMenuItemLayout;
        if (parent1 != null)
        {
          RadMenuItem parent2 = parent1.Parent as RadMenuItem;
          if (parent2 != null)
            return parent2.MenuLayout;
        }
        return (RadDropDownMenuLayout) null;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      if (this.MenuLayout != null)
      {
        this.EnsureImageAndTextElements();
        if (LayoutUtils.IsHorizontalRelation(this.TextImageRelation) && this.TextImageRelation == TextImageRelation.ImageBeforeText)
        {
          if (this.imageElement != null)
          {
            RectangleF rectangleF = new RectangleF(0.0f, 0.0f, this.MenuLayout.LeftColumnWidth, finalSize.Height);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, new RectangleF(PointF.Empty, finalSize));
            this.imageElement.Arrange(rectangleF);
          }
          if (this.textElement != null)
          {
            RectangleF rectangleF = new RectangleF(this.MenuLayout.LeftColumnWidth, 0.0f, this.textElement.DesiredSize.Width, finalSize.Height);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, new RectangleF(PointF.Empty, finalSize));
            this.textElement.Arrange(rectangleF);
          }
        }
      }
      return finalSize;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.MenuLayout != null && this.imageElement != null && (double) this.imageElement.DesiredSize.Width < (double) this.MenuLayout.LeftColumnWidth)
        sizeF.Width += this.MenuLayout.LeftColumnWidth - this.imageElement.DesiredSize.Width;
      else if (this.MenuLayout != null && this.imageElement == null)
        sizeF.Width += this.MenuLayout.LeftColumnWidth;
      return sizeF;
    }

    private void EnsureImageAndTextElements()
    {
      if (this.imageElement != null && this.textElement != null)
        return;
      foreach (RadElement child in this.Children)
      {
        if (this.IsImageElement(child))
          this.imageElement = child;
        else if (this.IsTextElement(child))
          this.textElement = child;
      }
    }

    private bool IsImageElement(RadElement element)
    {
      if (element == null)
        return false;
      return (bool) element.GetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty);
    }

    private bool IsTextElement(RadElement element)
    {
      if (element == null)
        return false;
      return (bool) element.GetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty);
    }
  }
}
