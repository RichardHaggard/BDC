// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataLayoutControlItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DataLayoutControlItem : LayoutControlItem
  {
    private LightVisualElement validationLabel;
    private int validationTextFixedSize;

    [Browsable(true)]
    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the fixed size of the validation label. If set to 0, the text will be autosized.")]
    public int ValidationTextFixedSize
    {
      get
      {
        return this.validationTextFixedSize;
      }
      set
      {
        if (this.validationTextFixedSize == value)
          return;
        this.validationTextFixedSize = value;
        this.OnNotifyPropertyChanged(nameof (ValidationTextFixedSize));
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawText = true;
      this.TextProportionalSize = 0.25f;
      this.validationLabel = new LightVisualElement();
      this.validationLabel.Visibility = ElementVisibility.Collapsed;
      this.validationLabel.RadPropertyChanged += new RadPropertyChangedEventHandler(this.validationLabel_RadPropertyChanged);
      this.Children.Add((RadElement) this.validationLabel);
    }

    private void validationLabel_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.VisibilityProperty && e.Property != RadItem.TextProperty)
        return;
      this.InvalidateMeasure();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public LightVisualElement ValidationLabel
    {
      get
      {
        return this.validationLabel;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.validationLabel.Visibility != ElementVisibility.Collapsed)
      {
        if (this.validationTextFixedSize != 0)
          this.validationLabel.Measure((SizeF) this.GetValidationTextRectangle(this.GetClientRectangle(availableSize)).Size);
        else
          this.validationLabel.Measure(this.GetClientRectangle(availableSize).Size);
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.validationLabel.Visibility != ElementVisibility.Collapsed)
        this.validationLabel.Arrange((RectangleF) this.GetValidationTextRectangle(clientRectangle));
      return sizeF;
    }

    protected override Rectangle GetControlRectangle(RectangleF clientRect)
    {
      Rectangle controlRectangle = base.GetControlRectangle(clientRect);
      Rectangle validationTextRectangle = this.GetValidationTextRectangle(clientRect);
      if (this.TextPosition == LayoutItemTextPosition.Top || this.TextPosition == LayoutItemTextPosition.Bottom)
      {
        controlRectangle.Height -= validationTextRectangle.Height;
        if (this.TextPosition == LayoutItemTextPosition.Bottom)
          controlRectangle.Y += validationTextRectangle.Height;
      }
      if (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Right)
      {
        controlRectangle.Width -= validationTextRectangle.Width;
        if (this.TextPosition == LayoutItemTextPosition.Right)
          controlRectangle.X += validationTextRectangle.Width;
      }
      return controlRectangle;
    }

    protected virtual Rectangle GetValidationTextRectangle(RectangleF clientRect)
    {
      if (this.validationLabel.Visibility == ElementVisibility.Collapsed)
        return Rectangle.Empty;
      int num = this.validationTextFixedSize;
      RectangleF rectangleF = RectangleF.Empty;
      if (num == 0 && (this.TextPosition == LayoutItemTextPosition.Top || this.TextPosition == LayoutItemTextPosition.Top))
        num = (int) this.validationLabel.DesiredSize.Height;
      else if (num == 0 && (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Right))
        num = (int) this.validationLabel.DesiredSize.Width;
      if (this.TextPosition == LayoutItemTextPosition.Top)
        rectangleF = new RectangleF(clientRect.Left, clientRect.Bottom - (float) num, clientRect.Width, (float) num);
      else if (this.TextPosition == LayoutItemTextPosition.Bottom)
        rectangleF = new RectangleF(clientRect.Left, clientRect.Top, clientRect.Width, (float) num);
      else if (this.TextPosition == LayoutItemTextPosition.Left)
        rectangleF = new RectangleF(clientRect.Right - (float) num, clientRect.Top, (float) num, clientRect.Height);
      else if (this.TextPosition == LayoutItemTextPosition.Right)
        rectangleF = new RectangleF(clientRect.Left, clientRect.Top, (float) num, clientRect.Height);
      return new Rectangle((int) rectangleF.X, (int) rectangleF.Y, (int) rectangleF.Width, (int) rectangleF.Height);
    }

    public override LayoutItemTextPosition TextPosition
    {
      get
      {
        return base.TextPosition;
      }
      set
      {
        if (((this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Right) && (value == LayoutItemTextPosition.Top || value == LayoutItemTextPosition.Bottom) || (this.TextPosition == LayoutItemTextPosition.Top || this.TextPosition == LayoutItemTextPosition.Bottom) && (value == LayoutItemTextPosition.Left || value == LayoutItemTextPosition.Right)) && (this.ElementTree != null && this.ElementTree.Control != null))
        {
          RadDataLayout parent = this.ElementTree.Control.Parent as RadDataLayout;
          if (parent != null && parent.Site != null && (parent.AutoSizeLabels && this.TextSizeMode == LayoutItemTextSizeMode.Fixed))
          {
            SizeF sizeF = MeasurementGraphics.CreateMeasurementGraphics().Graphics.MeasureString(this.Text, this.Font);
            if (value == LayoutItemTextPosition.Left || value == LayoutItemTextPosition.Right)
              this.TextFixedSize = (int) sizeF.Width + this.Padding.Horizontal + 2;
            else
              this.TextFixedSize = (int) sizeF.Height + this.Padding.Vertical + 2;
          }
        }
        base.TextPosition = value;
      }
    }
  }
}
