// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuItemLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuItemLayout : LayoutPanel
  {
    private ImagePrimitive imagePrimitive;
    private ArrowPrimitive arrowPrimitive;
    private RadMenuCheckmark checkmark;
    private StackLayoutPanel textPanel;
    private TextPrimitive textPrimitive;
    private TextPrimitive descriptionTextPrimitive;
    private TextPrimitive shortcutTextPrimitive;
    private LinePrimitive textSeparator;
    private ImageAndTextLayoutPanel internalLayoutPanel;
    private float maxHeight;

    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
    }

    public ArrowPrimitive ArrowPrimitive
    {
      get
      {
        return this.arrowPrimitive;
      }
    }

    public RadMenuCheckmark Checkmark
    {
      get
      {
        return this.checkmark;
      }
    }

    public StackLayoutPanel TextPanel
    {
      get
      {
        return this.textPanel;
      }
    }

    public ImageAndTextLayoutPanel InternalLayoutPanel
    {
      get
      {
        return this.internalLayoutPanel;
      }
    }

    public TextPrimitive Text
    {
      get
      {
        return this.textPrimitive;
      }
    }

    public TextPrimitive Description
    {
      get
      {
        return this.descriptionTextPrimitive;
      }
    }

    public TextPrimitive Shortcut
    {
      get
      {
        return this.shortcutTextPrimitive;
      }
    }

    public LinePrimitive TextSeparator
    {
      get
      {
        return this.textSeparator;
      }
    }

    protected override void CreateChildElements()
    {
      this.internalLayoutPanel = (ImageAndTextLayoutPanel) new MenuImageAndTextLayout();
      this.internalLayoutPanel.Class = "RadMenuItemInternalLayoutPanel";
      this.Children.Add((RadElement) this.internalLayoutPanel);
      this.checkmark = new RadMenuCheckmark();
      int num1 = (int) this.checkmark.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      this.checkmark.Alignment = ContentAlignment.MiddleCenter;
      this.checkmark.CheckElement.Alignment = ContentAlignment.MiddleCenter;
      this.checkmark.CheckElement.Class = "RadMenuItemCheckPrimitive";
      this.internalLayoutPanel.Children.Add((RadElement) this.checkmark);
      this.imagePrimitive = new ImagePrimitive();
      int num2 = (int) this.imagePrimitive.SetValue(RadCheckmark.IsImageProperty, (object) true);
      this.imagePrimitive.Class = "RadMenuItemImagePrimitive";
      this.imagePrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.imagePrimitive.ZIndex = this.checkmark.ZIndex + 1;
      this.checkmark.Children.Add((RadElement) this.imagePrimitive);
      this.textPanel = new StackLayoutPanel();
      this.textPanel.StretchHorizontally = false;
      this.textPanel.StretchVertically = false;
      this.textPanel.Class = "RadMenuItemTextPanel";
      this.textPanel.Orientation = Orientation.Vertical;
      this.textPanel.EqualChildrenWidth = true;
      int num3 = (int) this.textPanel.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      this.internalLayoutPanel.Children.Add((RadElement) this.textPanel);
      this.textPrimitive = new TextPrimitive();
      int num4 = (int) this.textPrimitive.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      this.textPrimitive.Class = "RadMenuItemTextPrimitive";
      this.textPanel.Children.Add((RadElement) this.textPrimitive);
      this.textSeparator = new LinePrimitive();
      this.textSeparator.Class = "RadMenuItemTextSeparator";
      this.textSeparator.Visibility = ElementVisibility.Collapsed;
      this.textPanel.Children.Add((RadElement) this.textSeparator);
      this.descriptionTextPrimitive = new TextPrimitive();
      this.descriptionTextPrimitive.Class = "RadMenuItemDescriptionText";
      this.textPanel.Children.Add((RadElement) this.descriptionTextPrimitive);
      this.shortcutTextPrimitive = new TextPrimitive();
      this.shortcutTextPrimitive.Class = "RadMenuItemShortcutPrimitive";
      this.shortcutTextPrimitive.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.shortcutTextPrimitive);
      this.arrowPrimitive = new ArrowPrimitive();
      this.arrowPrimitive.Visibility = ElementVisibility.Hidden;
      this.arrowPrimitive.Direction = this.RightToLeft ? Telerik.WinControls.ArrowDirection.Left : Telerik.WinControls.ArrowDirection.Right;
      this.arrowPrimitive.Alignment = ContentAlignment.MiddleLeft;
      this.arrowPrimitive.Class = "RadMenuItemArrowPrimitive";
      this.arrowPrimitive.SmoothingMode = SmoothingMode.Default;
      this.arrowPrimitive.MinSize = Size.Empty;
      this.arrowPrimitive.MaxSize = Size.Empty;
      this.Children.Add((RadElement) this.arrowPrimitive);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      this.arrowPrimitive.Direction = this.RightToLeft ? Telerik.WinControls.ArrowDirection.Left : Telerik.WinControls.ArrowDirection.Right;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = new SizeF(float.PositiveInfinity, availableSize.Height);
      SizeF empty = SizeF.Empty;
      this.maxHeight = 0.0f;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize1);
        this.maxHeight = Math.Max(child.DesiredSize.Height, this.maxHeight);
        empty.Width += child.DesiredSize.Width;
      }
      empty.Height = this.maxHeight;
      empty.Width += (float) this.Padding.Horizontal;
      empty.Height += (float) this.Padding.Vertical;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float width = 0.0f;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout menuLayout = ((RadMenuItem) this.Parent).MenuLayout;
      if (menuLayout != null)
        width = menuLayout.RightColumnWidth;
      else if (this.arrowPrimitive != null)
        width = this.arrowPrimitive.DesiredSize.Width;
      foreach (RadElement child in this.Children)
      {
        if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
          child.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
        else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
        {
          child.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
        }
        else
        {
          RectangleF rectangleF = new RectangleF(new PointF(clientRectangle.Left, clientRectangle.Top), child.DesiredSize);
          if (this.IsRightColumnElement(child))
            rectangleF = new RectangleF(clientRectangle.Right - width, clientRectangle.Top, width, clientRectangle.Bottom);
          else if (this.IsLeftContent(child))
            rectangleF = new RectangleF(clientRectangle.Left, clientRectangle.Top, child.DesiredSize.Width, clientRectangle.Bottom);
          else if (this.IsRightContent(child))
            rectangleF = new RectangleF(clientRectangle.Right - child.DesiredSize.Width - width, clientRectangle.Top, child.DesiredSize.Width, clientRectangle.Bottom);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
      }
      return finalSize;
    }

    protected virtual bool IsLeftColumnElement(RadElement element)
    {
      if (element != this.checkmark)
        return element == this.imagePrimitive;
      return true;
    }

    protected virtual bool IsRightColumnElement(RadElement element)
    {
      return element == this.arrowPrimitive;
    }

    protected virtual bool IsLeftContent(RadElement element)
    {
      return element == this.internalLayoutPanel;
    }

    protected virtual bool IsRightContent(RadElement element)
    {
      return element == this.shortcutTextPrimitive;
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      return new RectangleF((float) (this.Padding.Left + this.BorderThickness.Left), (float) (this.Padding.Top + this.BorderThickness.Top), Math.Max(0.0f, finalSize.Width - (float) (this.Padding.Horizontal + this.BorderThickness.Horizontal)), Math.Max(0.0f, finalSize.Height - (float) (this.Padding.Vertical + this.BorderThickness.Vertical)));
    }
  }
}
