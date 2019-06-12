// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRadioButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadRadioButtonElement : RadToggleButtonElement
  {
    public static RadProperty RadioCheckAlignmentProperty = RadProperty.Register(nameof (RadioCheckAlignment), typeof (System.Drawing.ContentAlignment), typeof (RadRadioButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) System.Drawing.ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private RadRadiomark checkMarkPrimitive;

    public RadRadiomark CheckMarkPrimitive
    {
      get
      {
        return this.checkMarkPrimitive;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
    }

    protected override void CreateChildElements()
    {
      this.ButtonFillElement = new FillPrimitive();
      this.ButtonFillElement.Class = "radioButtonFill";
      this.ButtonFillElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.BorderElement = new BorderPrimitive();
      this.BorderElement.Class = "radioButtonBorder";
      this.TextElement = new TextPrimitive();
      int num1 = (int) this.TextElement.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num2 = (int) this.TextElement.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.ImagePrimitive = new ImagePrimitive();
      int num3 = (int) this.ImagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num4 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.ImagePrimitive.Class = "radioButtonLayoutImagePrimitive";
      this.LayoutPanel = new ImageAndTextLayoutPanel();
      int num7 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadButtonItem.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num8 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num9 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      int num11 = (int) this.LayoutPanel.SetValue(CheckBoxLayoutPanel.IsBodyProperty, (object) true);
      this.LayoutPanel.Children.Add((RadElement) this.TextElement);
      this.LayoutPanel.Children.Add((RadElement) this.ImagePrimitive);
      RadioPrimitive radioPrimitive = new RadioPrimitive();
      int num12 = (int) radioPrimitive.SetValue(RadRadiomark.IsCheckmarkProperty, (object) true);
      radioPrimitive.Class = "radioButtonPrimitive";
      ImagePrimitive imagePrimitive = new ImagePrimitive();
      int num13 = (int) imagePrimitive.SetValue(RadRadiomark.IsImageProperty, (object) true);
      imagePrimitive.Class = "radioButtonImagePrimitive";
      this.checkMarkPrimitive = new RadRadiomark();
      int num14 = (int) this.checkMarkPrimitive.SetValue(CheckBoxLayoutPanel.IsCheckmarkProperty, (object) true);
      int num15 = (int) this.checkMarkPrimitive.BindProperty(RadRadiomark.CheckStateProperty, (RadObject) this, RadToggleButtonElement.ToggleStateProperty, PropertyBindingOptions.OneWay);
      this.checkMarkPrimitive.Children.Add((RadElement) radioPrimitive);
      this.checkMarkPrimitive.Children.Add((RadElement) imagePrimitive);
      this.checkMarkPrimitive.NotifyParentOnMouseInput = true;
      this.checkMarkPrimitive.ShouldHandleMouseInput = false;
      CheckBoxLayoutPanel checkBoxLayoutPanel = new CheckBoxLayoutPanel();
      int num16 = (int) this.TextElement.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num17 = (int) checkBoxLayoutPanel.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.OneWay);
      int num18 = (int) checkBoxLayoutPanel.BindProperty(CheckBoxLayoutPanel.CheckAlignmentProperty, (RadObject) this, RadRadioButtonElement.RadioCheckAlignmentProperty, PropertyBindingOptions.OneWay);
      checkBoxLayoutPanel.Children.Add((RadElement) this.LayoutPanel);
      checkBoxLayoutPanel.Children.Add((RadElement) this.checkMarkPrimitive);
      this.Children.Add((RadElement) this.ButtonFillElement);
      this.Children.Add((RadElement) checkBoxLayoutPanel);
      this.Children.Add((RadElement) this.BorderElement);
    }

    [Description("Gets or sets a value indicating the alignment of the radio-mark according to the text of the button.")]
    public System.Drawing.ContentAlignment RadioCheckAlignment
    {
      get
      {
        return (System.Drawing.ContentAlignment) this.GetValue(RadRadioButtonElement.RadioCheckAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRadioButtonElement.RadioCheckAlignmentProperty, (object) value);
      }
    }

    protected override void OnToggleStateChanged(StateChangedEventArgs e)
    {
      if (this.ElementTree != null && this.ElementTree.Control.Parent != null && this.ElementTree.Control is RadRadioButton)
      {
        for (int index = 0; index < this.ElementTree.Control.Parent.Controls.Count; ++index)
        {
          RadControl control = this.ElementTree.Control.Parent.Controls[index] as RadControl;
          if (control != null && control is RadRadioButton)
          {
            RadRadioButtonElement child = control.RootElement.Children[0] as RadRadioButtonElement;
            if (child != null && child != this && this.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
              child.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          }
        }
        base.OnToggleStateChanged(e);
      }
      else
      {
        if (this.Parent != null)
        {
          foreach (RadElement child in this.Parent.Children)
          {
            if (child is RadRadioButtonElement && child != this && this.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
              (child as RadRadioButtonElement).ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          }
        }
        base.OnToggleStateChanged(e);
      }
    }

    protected override void OnClick(EventArgs e)
    {
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Left || this.ToggleState != Telerik.WinControls.Enumerations.ToggleState.Off)
        return;
      base.OnClick(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.ClickMode == ClickMode.Hover && e.Button == MouseButtons.Left)
      {
        if (this.GetBitState(17592186044416L))
          this.Capture = true;
        if (this.ClickMode == ClickMode.Press)
        {
          int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
          base.OnClick((EventArgs) e);
        }
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.ClickMode == ClickMode.Release)
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        if (this.GetBitState(17592186044416L))
          this.Capture = false;
      }
      this.SetBitState(35184372088832L, false);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.ClickMode != ClickMode.Hover && !this.GetBitState(35184372088832L))
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
      this.SetBitState(35184372088832L, false);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.IsPressed && this.ClickMode == ClickMode.Hover)
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        this.SetBitState(35184372088832L, true);
        this.DoClick(e);
      }
      base.OnMouseLeave(e);
    }

    protected override Rectangle GetFocusRect()
    {
      if (this.TextElement != null)
        return this.TextElement.ControlBoundingRectangle;
      return Rectangle.Empty;
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      if (this.checkMarkPrimitive != null && this.paintSystemSkin.HasValue)
      {
        RadRadiomark checkMarkPrimitive1 = this.checkMarkPrimitive;
        bool? paintSystemSkin1 = this.paintSystemSkin;
        int num1 = paintSystemSkin1.GetValueOrDefault() ? 0 : (paintSystemSkin1.HasValue ? 1 : 0);
        checkMarkPrimitive1.ShouldPaintChildren = num1 != 0;
        RadRadiomark checkMarkPrimitive2 = this.checkMarkPrimitive;
        bool? paintSystemSkin2 = this.paintSystemSkin;
        int num2 = paintSystemSkin2.GetValueOrDefault() ? 0 : (paintSystemSkin2.HasValue ? 1 : 0);
        checkMarkPrimitive2.ShouldPaint = num2 != 0;
      }
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) == 0)
        return base.ShouldPaintChild(element);
      if (element != this.ButtonFillElement)
        return element != this.BorderElement;
      return false;
    }

    protected override Rectangle GetSystemSkinPaintBounds()
    {
      if (this.checkMarkPrimitive != null)
        return this.checkMarkPrimitive.ControlBoundingRectangle;
      return base.GetSystemSkinPaintBounds();
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      Telerik.WinControls.Enumerations.ToggleState toggleState = this.ToggleState;
      return this.Enabled ? (!this.IsMouseDown ? (!this.IsMouseOver ? (toggleState == Telerik.WinControls.Enumerations.ToggleState.On ? VisualStyleElement.Button.RadioButton.CheckedNormal : VisualStyleElement.Button.RadioButton.UncheckedNormal) : (toggleState == Telerik.WinControls.Enumerations.ToggleState.On ? VisualStyleElement.Button.RadioButton.CheckedHot : VisualStyleElement.Button.RadioButton.UncheckedHot)) : (!this.IsMouseOver ? (toggleState == Telerik.WinControls.Enumerations.ToggleState.On ? VisualStyleElement.Button.RadioButton.CheckedHot : VisualStyleElement.Button.RadioButton.UncheckedHot) : (toggleState == Telerik.WinControls.Enumerations.ToggleState.On ? VisualStyleElement.Button.RadioButton.CheckedPressed : VisualStyleElement.Button.RadioButton.UncheckedPressed))) : (toggleState == Telerik.WinControls.Enumerations.ToggleState.On ? VisualStyleElement.Button.RadioButton.CheckedDisabled : VisualStyleElement.Button.RadioButton.UncheckedDisabled);
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.TextElement.ForeColor = SystemSkinManager.Instance.Renderer.GetColor(ColorProperty.TextColor);
    }
  }
}
