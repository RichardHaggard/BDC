// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarArrowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
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
  public class RadCommandBarArrowButton : LightVisualElement
  {
    private static Size defaultSize = new Size(16, 16);
    private ArrowPrimitive arrow;

    public ArrowDirection Direction
    {
      get
      {
        return this.arrow.Direction;
      }
      set
      {
        this.arrow.Direction = value;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num1 = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
      int num2 = (int) this.SetValue(RadElement.IsMouseOverProperty, (object) false);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadCommandBarArrowButton);
      }
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
      this.Children.Add((RadElement) this.arrow);
      this.NotifyParentOnMouseInput = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return (SizeF) RadCommandBarArrowButton.defaultSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.arrow.Arrange(new RectangleF((float) (((double) finalSize.Width - (double) this.arrow.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0), this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height));
      return finalSize;
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.arrow.Visibility = ElementVisibility.Hidden;
    }

    protected override void UnitializeSystemSkinPaint()
    {
      base.UnitializeSystemSkinPaint();
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
