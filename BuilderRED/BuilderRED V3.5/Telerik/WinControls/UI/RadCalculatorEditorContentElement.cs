// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorEditorContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCalculatorEditorContentElement : RadTextBoxElement
  {
    private RadCalculatorDropDownElement owner;

    public RadCalculatorEditorContentElement()
    {
      this.TextAlign = HorizontalAlignment.Right;
      this.Alignment = ContentAlignment.MiddleCenter;
      this.TextBoxItem.TextChanging += new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      int num = (int) this.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
    }

    public RadCalculatorEditorContentElement(RadCalculatorDropDownElement owner)
      : this()
    {
      this.owner = owner;
    }

    protected override void DisposeManagedResources()
    {
      this.TextBoxItem.TextChanging -= new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      base.DisposeManagedResources();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyValue == 115)
      {
        if (this.owner == null)
          return;
        if (this.owner.IsPopupOpen)
          this.owner.ClosePopup();
        else
          this.owner.ShowPopup();
        e.Handled = true;
      }
      else if (this.owner != null && this.owner.IsPopupOpen)
      {
        this.owner.CalculatorContentElement.Focus();
        this.owner.CalculatorContentElement.ProcessKeyDown(e);
        e.Handled = true;
      }
      else
        base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.owner != null && this.owner.IsPopupOpen)
      {
        this.owner.CalculatorContentElement.Focus();
        this.owner.CalculatorContentElement.ProcessKeyPress(e);
        e.Handled = true;
      }
      else
      {
        base.OnKeyPress(e);
        NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
        string decimalSeparator = numberFormat.NumberDecimalSeparator;
        string numberGroupSeparator = numberFormat.NumberGroupSeparator;
        string negativeSign = numberFormat.NegativeSign;
        if (e.KeyChar == '.')
          e.KeyChar = decimalSeparator[0];
        string str = e.KeyChar.ToString();
        if (char.IsDigit(e.KeyChar) || str.Equals(decimalSeparator) || (str.Equals(numberGroupSeparator) || str.Equals(negativeSign)) || (e.KeyChar == '\b' || (Control.ModifierKeys & (Keys.Control | Keys.Alt)) != Keys.None))
          return;
        e.Handled = true;
        Telerik.WinControls.NativeMethods.MessageBeep(0);
      }
    }

    private void TextBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.owner.Value = (object) this.Text;
      e.Cancel = this.owner.CancelValueChanging;
    }
  }
}
