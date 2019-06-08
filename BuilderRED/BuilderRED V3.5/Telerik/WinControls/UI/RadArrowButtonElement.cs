// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadArrowButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadArrowButtonElement : RadButtonItem
  {
    private static Size defaultSize = new Size(16, 16);
    private ArrowPrimitive arrow;
    private OverflowPrimitive overflowArrow;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private ImagePrimitive imagePrimitive;

    static RadArrowButtonElement()
    {
      VisualElement.DefaultSizeProperty.OverrideMetadata(typeof (RadArrowButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadArrowButtonElement.defaultSize, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
    }

    protected override void CreateChildElements()
    {
      this.arrow = new ArrowPrimitive(ArrowDirection.Down);
      this.arrow.Class = "RadArrowButtonArrow";
      this.arrow.AutoSize = false;
      this.arrow.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.overflowArrow = new OverflowPrimitive(ArrowDirection.Down);
      this.overflowArrow.Class = "RadArrowButtonOverflowArrow";
      this.overflowArrow.AutoSize = false;
      this.overflowArrow.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.overflowArrow.Visibility = ElementVisibility.Collapsed;
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadArrowButtonFill";
      this.fillPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadArrowButtonBorder";
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.imagePrimitive = new ImagePrimitive();
      this.imagePrimitive.Class = "RadArrowButtonImage";
      this.imagePrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.imagePrimitive.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.arrow);
      this.Children.Add((RadElement) this.overflowArrow);
      this.Children.Add((RadElement) this.imagePrimitive);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static Size RadArrowButtonDefaultSize
    {
      get
      {
        return RadArrowButtonElement.defaultSize;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size ArrowFullSize
    {
      get
      {
        return new Size(this.arrow.Size.Width + (int) this.borderPrimitive.HorizontalWidth, this.arrow.Size.Height + (int) this.borderPrimitive.VerticalWidth);
      }
    }

    public ArrowDirection Direction
    {
      get
      {
        return this.arrow.Direction;
      }
      set
      {
        this.arrow.Direction = value;
        this.overflowArrow.Direction = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive Fill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    [DefaultValue(true)]
    public bool OverflowMode
    {
      get
      {
        return this.overflowArrow.Visibility == ElementVisibility.Collapsed;
      }
      set
      {
        if (value)
        {
          this.Arrow.Visibility = ElementVisibility.Collapsed;
          this.overflowArrow.Visibility = ElementVisibility.Visible;
        }
        else
        {
          this.Arrow.Visibility = ElementVisibility.Visible;
          this.overflowArrow.Visibility = ElementVisibility.Collapsed;
        }
        this.InvalidateMeasure();
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF finalRect = new RectangleF((float) (((double) finalSize.Width - (double) this.arrow.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0), this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height);
      this.arrow.Arrange(finalRect);
      finalRect = new RectangleF((float) (((double) finalSize.Width - (double) this.overflowArrow.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.overflowArrow.DesiredSize.Height) / 2.0), this.overflowArrow.DesiredSize.Width, this.overflowArrow.DesiredSize.Height);
      this.overflowArrow.Arrange(finalRect);
      return finalSize;
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.fillPrimitive.Visibility = ElementVisibility.Hidden;
      this.borderPrimitive.Visibility = ElementVisibility.Hidden;
      this.arrow.Visibility = ElementVisibility.Hidden;
    }

    protected override void UnitializeSystemSkinPaint()
    {
      base.UnitializeSystemSkinPaint();
      this.fillPrimitive.Visibility = ElementVisibility.Visible;
      this.borderPrimitive.Visibility = ElementVisibility.Visible;
      this.arrow.Visibility = ElementVisibility.Visible;
    }

    protected override void PaintElementSkin(IGraphics graphics)
    {
      if (object.ReferenceEquals((object) SystemSkinManager.DefaultElement, (object) SystemSkinManager.Instance.CurrentElement))
        return;
      base.PaintElementSkin(graphics);
    }

    private void PaintElementSkin(Graphics g, VisualStyleElement element, Rectangle bounds)
    {
      if (!SystemSkinManager.Instance.SetCurrentElement(element))
        return;
      SystemSkinManager.Instance.PaintCurrentElement(g, bounds);
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      if (!this.Enabled)
        return VisualStyleElement.ComboBox.DropDownButton.Disabled;
      if (!this.IsMouseOver && !this.IsMouseDown)
        return VisualStyleElement.ComboBox.DropDownButton.Normal;
      if (this.IsMouseOver && !this.IsMouseDown || this.IsMouseOver && this.IsMouseDown)
        return VisualStyleElement.ComboBox.DropDownButton.Hot;
      return SystemSkinManager.DefaultElement;
    }
  }
}
