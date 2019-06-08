// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorDropDownElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadCalculatorDropDownElement : PopupEditorBaseElement
  {
    public static RadProperty DefaultPopupWidthProperty = RadProperty.Register(nameof (DefaultPopupWidth), typeof (int), typeof (RadCalculatorDropDownElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 200));
    public static RadProperty DefaultPopupHeightProperty = RadProperty.Register(nameof (DefaultPopupHeight), typeof (int), typeof (RadCalculatorDropDownElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 200));
    public static RadProperty MinPopupWidthProperty = RadProperty.Register(nameof (MinPopupWidth), typeof (int), typeof (RadCalculatorDropDownElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    public static RadProperty MinPopupHeightProperty = RadProperty.Register(nameof (MinPopupHeight), typeof (int), typeof (RadCalculatorDropDownElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    public static RadProperty IsDropDownShownProperty = RadProperty.Register("IsDropDownShown", typeof (bool), typeof (RadCalculatorDropDownElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private RadCalculatorEditorContentElement editorContentElement;
    private RadCalculatorArrowButtonElement arrowButton;
    private RadCalculatorEditorPopupControlBase popup;
    private RadCalculatorContentElement calculatorContentElement;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private RadCalculatorMemoryElement memoryElement;
    private Size popupSize;
    internal bool InputtingDigits;
    internal bool CancelValueChanging;
    private object value;
    private bool valueChanging;
    private bool readOnly;

    public RadCalculatorDropDownElement()
    {
      this.Value = (object) 0;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.editorContentElement = new RadCalculatorEditorContentElement(this);
      this.Children.Add((RadElement) this.editorContentElement);
      this.arrowButton = new RadCalculatorArrowButtonElement();
      this.arrowButton.MinSize = new Size(RadArrowButtonElement.RadArrowButtonDefaultSize.Width, this.arrowButton.ArrowFullSize.Height);
      this.arrowButton.ClickMode = ClickMode.Press;
      this.arrowButton.Click += new EventHandler(this.popupOpenButton_Click);
      this.arrowButton.KeyPress += new KeyPressEventHandler(this.arrowButton_KeyPress);
      this.Children.Add((RadElement) this.arrowButton);
      this.calculatorContentElement = new RadCalculatorContentElement(this);
      RadCalculatorContentElement calculatorContentElement = new RadCalculatorContentElement(this);
      calculatorContentElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) calculatorContentElement);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "CalculatorBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.fillPrimitive = new FillPrimitive();
      int num1 = (int) this.fillPrimitive.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.TwoWay);
      this.fillPrimitive.Class = "CalculatorFill";
      int num2 = (int) this.fillPrimitive.SetDefaultValueOverride(RadElement.ZIndexProperty, (object) -1);
      this.fillPrimitive.RadPropertyChanged += new RadPropertyChangedEventHandler(this.fillPrimitive_RadPropertyChanged);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.memoryElement = new RadCalculatorMemoryElement();
      this.Children.Add((RadElement) this.memoryElement);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.popupSize = new Size(this.DefaultPopupWidth, this.DefaultPopupHeight);
    }

    protected override void DisposeManagedResources()
    {
      this.arrowButton.Click -= new EventHandler(this.popupOpenButton_Click);
      this.arrowButton.KeyPress -= new KeyPressEventHandler(this.arrowButton_KeyPress);
      this.fillPrimitive.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.fillPrimitive_RadPropertyChanged);
      base.DisposeManagedResources();
    }

    public override object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (this.value != null && value != null && this.value.ToString() != value.ToString())
        {
          ValueChangingEventArgs e = new ValueChangingEventArgs(value, this.value);
          this.OnCalculatorValueChanging(e);
          if (e.Cancel)
          {
            this.CancelValueChanging = true;
            return;
          }
          this.valueChanging = true;
        }
        this.CancelValueChanging = false;
        if (value == null || value.ToString() == string.Empty)
          this.value = (object) string.Empty;
        else if (this.InputtingDigits)
        {
          this.value = (object) value.ToString();
        }
        else
        {
          Decimal num = new Decimal(0);
          try
          {
            num = Convert.ToDecimal(value);
          }
          catch (Exception ex)
          {
          }
          this.value = (object) num.ToString("G29");
        }
        if (this.valueChanging)
        {
          this.OnCalculatorValueChanged();
          this.valueChanging = false;
        }
        this.editorContentElement.Text = this.value != null ? this.value.ToString() : "0";
      }
    }

    public RadCalculatorEditorContentElement EditorContentElement
    {
      get
      {
        return this.editorContentElement;
      }
      set
      {
        this.editorContentElement = value;
      }
    }

    public RadCalculatorArrowButtonElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
      set
      {
        this.arrowButton = value;
      }
    }

    public RadCalculatorEditorPopupControlBase Popup
    {
      get
      {
        if (this.popup == null)
          this.popup = (RadCalculatorEditorPopupControlBase) this.CreatePopupForm();
        return this.popup;
      }
      set
      {
        this.popup = value;
      }
    }

    public RadCalculatorContentElement CalculatorContentElement
    {
      get
      {
        return this.calculatorContentElement;
      }
    }

    public RadCalculatorMemoryElement MemoryElement
    {
      get
      {
        return this.memoryElement;
      }
    }

    public int DefaultPopupWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadCalculatorDropDownElement.DefaultPopupWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadCalculatorDropDownElement.DefaultPopupWidthProperty, (object) value);
      }
    }

    public int DefaultPopupHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadCalculatorDropDownElement.DefaultPopupHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadCalculatorDropDownElement.DefaultPopupHeightProperty, (object) value);
      }
    }

    public int MinPopupWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadCalculatorDropDownElement.MinPopupWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadCalculatorDropDownElement.MinPopupWidthProperty, (object) value);
        if (this.popup == null)
          return;
        this.popup.MinimumSize = new Size(value, this.popup.MinimumSize.Height);
      }
    }

    public int MinPopupHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadCalculatorDropDownElement.MinPopupHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadCalculatorDropDownElement.MinPopupHeightProperty, (object) value);
        if (this.popup == null)
          return;
        this.popup.MinimumSize = new Size(this.popup.MinimumSize.Width, value);
      }
    }

    public FillPrimitive FillPrimitive
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    public BorderPrimitive BorderPrimitive
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected override RadPopupControlBase CreatePopupForm()
    {
      this.popup = new RadCalculatorEditorPopupControlBase((RadItem) this);
      this.popup.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.popup.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.Smooth;
      this.popup.SizingMode = SizingMode.UpDownAndRightBottom;
      this.popup.SizingGrip.ShouldAspectRootElement = false;
      this.popup.SizingGrip.ShouldAspectMinSize = true;
      this.popup.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.Inherit;
      this.popup.MinimumSize = new Size(this.MinPopupWidth, this.MinPopupHeight);
      this.WirePopupFormEvents((RadPopupControlBase) this.popup);
      this.popup.SizingGripDockLayout.Children.Add((RadElement) this.calculatorContentElement);
      return (RadPopupControlBase) this.popup;
    }

    protected override void WirePopupFormEvents(RadPopupControlBase popup)
    {
      base.WirePopupFormEvents(popup);
      popup.PopupOpened += new RadPopupOpenedEventHandler(this.popup_PopupOpened);
      popup.PopupClosing += new RadPopupClosingEventHandler(this.popup_PopupClosing);
      popup.PopupClosed += new RadPopupClosedEventHandler(this.popup_PopupClosed);
      popup.PopupOpening += new RadPopupOpeningEventHandler(this.popup_PopupOpening);
    }

    protected override void UnwirePopupFormEvents(RadPopupControlBase popup)
    {
      base.UnwirePopupFormEvents(popup);
      popup.PopupOpened -= new RadPopupOpenedEventHandler(this.popup_PopupOpened);
      popup.PopupClosing -= new RadPopupClosingEventHandler(this.popup_PopupClosing);
      popup.PopupClosed -= new RadPopupClosedEventHandler(this.popup_PopupClosed);
      popup.PopupOpening -= new RadPopupOpeningEventHandler(this.popup_PopupOpening);
    }

    protected override Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      return this.popupSize;
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (!this.IsPopupOpen)
        return;
      this.calculatorContentElement.ProcessKeyPress(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!this.IsPopupOpen)
        return;
      this.calculatorContentElement.ProcessKeyDown(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ArrowButton.Shape == null)
        return;
      this.ArrowButton.Shape.IsRightToLeft = newValue;
    }

    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        this.readOnly = value;
        this.EditorContentElement.TextBoxItem.ReadOnly = value;
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) availableSize.Width < 1.0 || (double) availableSize.Height < 1.0)
        return availableSize;
      availableSize = this.GetClientRectangle(availableSize).Size;
      this.arrowButton.Measure(availableSize);
      this.memoryElement.Measure(new SizeF(availableSize.Width - this.arrowButton.DesiredSize.Width, availableSize.Height));
      this.editorContentElement.Measure(new SizeF(availableSize.Width - this.arrowButton.DesiredSize.Width - this.memoryElement.DesiredSize.Width, availableSize.Height));
      SizeF empty = SizeF.Empty;
      empty.Width += this.editorContentElement.DesiredSize.Width + this.memoryElement.DesiredSize.Width + this.arrowButton.DesiredSize.Width;
      empty.Height = Math.Max(this.editorContentElement.DesiredSize.Height, this.arrowButton.DesiredSize.Height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Width < 1.0 || (double) finalSize.Height < 1.0)
        return finalSize;
      base.ArrangeOverride(finalSize);
      this.arrowButton.Arrange(new RectangleF(this.RightToLeft ? 0.0f : finalSize.Width - this.arrowButton.DesiredSize.Width, 0.0f, this.arrowButton.DesiredSize.Width, finalSize.Height));
      this.memoryElement.Arrange(new RectangleF(this.RightToLeft ? this.arrowButton.DesiredSize.Width : 0.0f, 0.0f, this.memoryElement.DesiredSize.Width, finalSize.Height));
      this.editorContentElement.Arrange(new RectangleF(this.RightToLeft ? this.arrowButton.DesiredSize.Width + this.memoryElement.DesiredSize.Width : this.memoryElement.DesiredSize.Width, 0.0f, finalSize.Width - this.arrowButton.DesiredSize.Width - this.memoryElement.DesiredSize.Width, finalSize.Height));
      return finalSize;
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      Padding padding = this.Padding;
      RectangleF rectangleF = new RectangleF((float) padding.Left, (float) padding.Top, finalSize.Width - (float) padding.Horizontal, finalSize.Height - (float) padding.Vertical);
      if (this.borderPrimitive.Visibility == ElementVisibility.Visible)
      {
        Padding borderThickness = this.GetBorderThickness(false);
        rectangleF.X += (float) borderThickness.Left;
        rectangleF.Y += (float) borderThickness.Top;
        rectangleF.Width -= (float) borderThickness.Horizontal;
        rectangleF.Height -= (float) borderThickness.Vertical;
      }
      rectangleF.Width = Math.Max(0.0f, rectangleF.Width);
      rectangleF.Height = Math.Max(0.0f, rectangleF.Height);
      return rectangleF;
    }

    protected internal virtual Padding GetBorderThickness(bool checkDrawBorder)
    {
      Padding padding = Padding.Empty;
      if (this.borderPrimitive.BoxStyle == BorderBoxStyle.SingleBorder)
        padding = this.borderPrimitive.BorderThickness;
      else if (this.borderPrimitive.BoxStyle == BorderBoxStyle.FourBorders)
        padding = new Padding((int) this.borderPrimitive.LeftWidth, (int) this.borderPrimitive.TopWidth, (int) this.borderPrimitive.RightWidth, (int) this.borderPrimitive.BottomWidth);
      else if (this.borderPrimitive.BoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        int all = this.borderPrimitive.BorderThickness.All;
        if (all == 1)
          all = 2;
        padding = new Padding(all);
      }
      return padding;
    }

    public event ValueChangingEventHandler CalculatorValueChanging;

    protected virtual void OnCalculatorValueChanging(ValueChangingEventArgs e)
    {
      if (this.CalculatorValueChanging == null)
        return;
      this.CalculatorValueChanging((object) this, e);
    }

    public event EventHandler CalculatorValueChanged;

    protected virtual void OnCalculatorValueChanged()
    {
      if (this.CalculatorValueChanged == null)
        return;
      this.CalculatorValueChanged((object) this, new EventArgs());
    }

    private void popupOpenButton_Click(object sender, EventArgs e)
    {
      this.TooglePopupState();
    }

    private void popup_PopupOpened(object sender, EventArgs args)
    {
      int num1 = (int) this.arrowButton.SetValue(RadCalculatorDropDownElement.IsDropDownShownProperty, (object) true);
      int num2 = (int) this.SetValue(RadCalculatorDropDownElement.IsDropDownShownProperty, (object) true);
      this.calculatorContentElement.Reset();
      this.editorContentElement.Focus();
    }

    private void popup_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      if (args.CloseReason == RadPopupCloseReason.Mouse && this.ArrowButton.ControlBoundingRectangle.Contains(this.ElementTree.Control.PointToClient(Control.MousePosition)))
        args.Cancel = true;
      if (args.Cancel)
        return;
      this.calculatorContentElement.ProcessOperation(true);
      this.editorContentElement.Focus();
      this.popupSize = this.popup.Size;
    }

    private void popup_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num1 = (int) this.arrowButton.SetValue(RadCalculatorDropDownElement.IsDropDownShownProperty, (object) false);
      int num2 = (int) this.SetValue(RadCalculatorDropDownElement.IsDropDownShownProperty, (object) false);
    }

    private void fillPrimitive_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != VisualElement.BackColorProperty)
        return;
      int num = (int) this.editorContentElement.SetDefaultValueOverride(VisualElement.BackColorProperty, e.NewValue);
    }

    private void arrowButton_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.IsPopupOpen)
        return;
      this.calculatorContentElement.ProcessKeyPress(e);
    }

    private void popup_PopupOpening(object sender, CancelEventArgs args)
    {
      args.Cancel = this.readOnly;
    }
  }
}
