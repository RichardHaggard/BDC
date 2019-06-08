// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadCheckBoxElement : RadToggleButtonElement
  {
    public static RadProperty CheckAlignmentProperty = RadProperty.Register(nameof (CheckAlignment), typeof (System.Drawing.ContentAlignment), typeof (RadCheckBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) System.Drawing.ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private RadCheckmark checkMarkPrimitive;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
    }

    protected override void CreateChildElements()
    {
      this.ButtonFillElement = new FillPrimitive();
      this.ButtonFillElement.Class = "ButtonFill";
      this.ButtonFillElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.BorderElement = new BorderPrimitive();
      this.BorderElement.Class = "ButtonBorder";
      this.TextElement = new TextPrimitive();
      int num1 = (int) this.TextElement.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num2 = (int) this.TextElement.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.ImagePrimitive = new ImagePrimitive();
      int num3 = (int) this.ImagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num4 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.ImagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.ImagePrimitive.Class = "RadCheckBoxImagePrimitive";
      this.LayoutPanel = new ImageAndTextLayoutPanel();
      int num7 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadButtonItem.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num8 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num9 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.LayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      int num11 = (int) this.LayoutPanel.SetValue(CheckBoxLayoutPanel.IsBodyProperty, (object) true);
      this.LayoutPanel.Children.Add((RadElement) this.TextElement);
      this.LayoutPanel.Children.Add((RadElement) this.ImagePrimitive);
      ImagePrimitive imagePrimitive = new ImagePrimitive();
      int num12 = (int) imagePrimitive.SetValue(RadCheckmark.IsImageProperty, (object) true);
      imagePrimitive.Class = "RadCheckBoxCheckImage";
      this.checkMarkPrimitive = new RadCheckmark();
      this.checkMarkPrimitive.Class = "CheckMark";
      int num13 = (int) this.checkMarkPrimitive.SetValue(CheckBoxLayoutPanel.IsCheckmarkProperty, (object) true);
      int num14 = (int) this.checkMarkPrimitive.BindProperty(RadCheckmark.CheckStateProperty, (RadObject) this, RadToggleButtonElement.ToggleStateProperty, PropertyBindingOptions.OneWay);
      this.checkMarkPrimitive.CheckElement.Class = "RadCheckBoxCheckPrimitive";
      this.checkMarkPrimitive.Children.Add((RadElement) imagePrimitive);
      CheckBoxLayoutPanel checkBoxLayoutPanel = new CheckBoxLayoutPanel();
      int num15 = (int) this.TextElement.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num16 = (int) checkBoxLayoutPanel.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.OneWay);
      int num17 = (int) checkBoxLayoutPanel.BindProperty(CheckBoxLayoutPanel.CheckAlignmentProperty, (RadObject) this, RadCheckBoxElement.CheckAlignmentProperty, PropertyBindingOptions.OneWay);
      checkBoxLayoutPanel.Children.Add((RadElement) this.LayoutPanel);
      checkBoxLayoutPanel.Children.Add((RadElement) this.checkMarkPrimitive);
      this.Children.Add((RadElement) this.ButtonFillElement);
      this.Children.Add((RadElement) checkBoxLayoutPanel);
      this.Children.Add((RadElement) this.BorderElement);
    }

    [RadPropertyDefaultValue("CheckAlignment", typeof (RadCheckBoxElement))]
    [Description("Gets or sets a value indicating the alignment of the check box.")]
    public System.Drawing.ContentAlignment CheckAlignment
    {
      get
      {
        return (System.Drawing.ContentAlignment) this.GetValue(RadCheckBoxElement.CheckAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCheckBoxElement.CheckAlignmentProperty, (object) value);
      }
    }

    [Browsable(false)]
    public RadCheckmark CheckMarkPrimitive
    {
      get
      {
        return this.checkMarkPrimitive;
      }
    }

    public bool Checked
    {
      get
      {
        return this.ToggleState == ToggleState.On;
      }
      set
      {
        if (this.Checked == value)
          return;
        this.ToggleState = value ? ToggleState.On : ToggleState.Off;
      }
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      if (this.checkMarkPrimitive != null && this.paintSystemSkin.HasValue)
      {
        CheckPrimitive checkElement = this.checkMarkPrimitive.CheckElement;
        bool? paintSystemSkin1 = this.paintSystemSkin;
        int num1 = paintSystemSkin1.GetValueOrDefault() ? 0 : (paintSystemSkin1.HasValue ? 1 : 0);
        checkElement.ShouldPaint = num1 != 0;
        BorderPrimitive border = this.checkMarkPrimitive.Border;
        bool? paintSystemSkin2 = this.paintSystemSkin;
        int num2 = paintSystemSkin2.GetValueOrDefault() ? 0 : (paintSystemSkin2.HasValue ? 1 : 0);
        border.ShouldPaint = num2 != 0;
        FillPrimitive fill = this.checkMarkPrimitive.Fill;
        bool? paintSystemSkin3 = this.paintSystemSkin;
        int num3 = paintSystemSkin3.GetValueOrDefault() ? 0 : (paintSystemSkin3.HasValue ? 1 : 0);
        fill.ShouldPaint = num3 != 0;
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

    public override VisualStyleElement GetXPVisualStyle()
    {
      VisualStyleElement visualStyleElement = (VisualStyleElement) null;
      ToggleState toggleState = this.ToggleState;
      if (!this.Enabled)
      {
        switch (toggleState)
        {
          case ToggleState.Off:
            visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedDisabled;
            break;
          case ToggleState.On:
            visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedDisabled;
            break;
          case ToggleState.Indeterminate:
            visualStyleElement = VisualStyleElement.Button.CheckBox.MixedDisabled;
            break;
        }
      }
      else if (this.IsMouseDown)
      {
        switch (toggleState)
        {
          case ToggleState.Off:
            visualStyleElement = this.IsMouseOver ? VisualStyleElement.Button.CheckBox.UncheckedPressed : VisualStyleElement.Button.CheckBox.UncheckedHot;
            break;
          case ToggleState.On:
            visualStyleElement = this.IsMouseOver ? VisualStyleElement.Button.CheckBox.CheckedPressed : VisualStyleElement.Button.CheckBox.CheckedHot;
            break;
          case ToggleState.Indeterminate:
            visualStyleElement = this.IsMouseOver ? VisualStyleElement.Button.CheckBox.MixedPressed : VisualStyleElement.Button.CheckBox.MixedHot;
            break;
        }
      }
      else if (this.IsMouseOver)
      {
        switch (toggleState)
        {
          case ToggleState.Off:
            visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedHot;
            break;
          case ToggleState.On:
            visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedHot;
            break;
          case ToggleState.Indeterminate:
            visualStyleElement = VisualStyleElement.Button.CheckBox.MixedHot;
            break;
        }
      }
      else
      {
        switch (toggleState)
        {
          case ToggleState.Off:
            visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
            break;
          case ToggleState.On:
            visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedNormal;
            break;
          case ToggleState.Indeterminate:
            visualStyleElement = VisualStyleElement.Button.CheckBox.MixedNormal;
            break;
        }
      }
      return visualStyleElement;
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.TextElement.ForeColor = SystemSkinManager.Instance.Renderer.GetColor(ColorProperty.TextColor);
    }

    protected override Rectangle GetFocusRect()
    {
      if (this.TextElement != null)
        return this.TextElement.ControlBoundingRectangle;
      return Rectangle.Empty;
    }
  }
}
