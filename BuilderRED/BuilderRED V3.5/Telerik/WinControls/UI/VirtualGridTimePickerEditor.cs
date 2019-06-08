// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridTimePickerEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualGridTimePickerEditor : BaseVirtualGridEditor
  {
    private bool selectedItemChanged;

    public override object Value
    {
      get
      {
        return (this.EditorElement as RadTimePickerElement).Value;
      }
      set
      {
        (this.EditorElement as RadTimePickerElement).Value = value;
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (DateTime);
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadTimePickerElement editorElement = this.EditorElement as RadTimePickerElement;
      editorElement.RightToLeft = this.RightToLeft;
      editorElement.MaskedEditBox.TextBoxItem.KeyDown += new KeyEventHandler(this.OnTextBoxItemKeyDown);
      editorElement.MaskedEditBox.TextBoxItem.KeyUp += new KeyEventHandler(this.OnTextBoxItemKeyUp);
      editorElement.ValueChanged += new EventHandler(this.OnEditorElementValueChanged);
      editorElement.ValueChanging += new CancelEventHandler(this.OnEditorElementValueChanging);
      editorElement.PopupClosed += new RadPopupClosedEventHandler(this.OnEditorElementPopupClosed);
      editorElement.Validating += new CancelEventHandler(this.OnEditorElementValidating);
      editorElement.Validated += new EventHandler(this.OnEditorElementValidated);
      editorElement.ValidationError += new ValidationErrorEventHandler(this.OnEditorElementValidationError);
      MaskDateTimeProvider provider = editorElement.MaskedEditBox.Provider as MaskDateTimeProvider;
      provider.SelectFirstEditableItem();
      provider.SelectedItemChanged += new EventHandler(this.OnSelectedItemChanged);
      editorElement.MaskedEditBox.TextBoxItem.HostedControl.Focus();
      this.selectedItemChanged = false;
    }

    private void OnSelectedItemChanged(object sender, EventArgs e)
    {
      this.selectedItemChanged = true;
    }

    public override bool EndEdit()
    {
      RadTimePickerElement editorElement = this.EditorElement as RadTimePickerElement;
      editorElement.MaskedEditBox.TextBoxItem.KeyDown -= new KeyEventHandler(this.OnTextBoxItemKeyDown);
      editorElement.MaskedEditBox.TextBoxItem.KeyUp -= new KeyEventHandler(this.OnTextBoxItemKeyUp);
      editorElement.ValueChanged -= new EventHandler(this.OnEditorElementValueChanged);
      editorElement.ValueChanging -= new CancelEventHandler(this.OnEditorElementValueChanging);
      editorElement.PopupClosed -= new RadPopupClosedEventHandler(this.OnEditorElementPopupClosed);
      editorElement.Validating -= new CancelEventHandler(this.OnEditorElementValidating);
      editorElement.Validated -= new EventHandler(this.OnEditorElementValidated);
      editorElement.ValidationError += new ValidationErrorEventHandler(this.OnEditorElementValidationError);
      return base.EndEdit();
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadTimePickerElement();
    }

    private void OnEditorElementValueChanging(object sender, CancelEventArgs e)
    {
      this.OnValueChanging(e as ValueChangingEventArgs);
    }

    private void OnEditorElementValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void OnTextBoxItemKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
        return;
      this.OnKeyDown(e);
    }

    private void OnEditorElementPopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      (sender as RadTimePickerElement).MaskedEditBox.TextBoxItem.HostedControl.Focus();
    }

    private void OnEditorElementValidationError(object sender, ValidationErrorEventArgs e)
    {
      this.OnValidationError(e);
    }

    private void OnEditorElementValidated(object sender, EventArgs e)
    {
      this.OnValidated();
    }

    private void OnEditorElementValidating(object sender, CancelEventArgs e)
    {
      this.OnValidating(e);
    }

    private void OnTextBoxItemKeyUp(object sender, KeyEventArgs e)
    {
      HostedTextBoxBase textBoxControl = (this.EditorElement as RadTimePickerElement).MaskedEditBox.TextBoxItem.TextBoxControl;
      bool flag1 = textBoxControl.SelectionStart == 0;
      bool flag2 = textBoxControl.Text.Length == textBoxControl.SelectionStart + textBoxControl.SelectionLength;
      if (!this.selectedItemChanged && (flag1 || flag2) && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
        this.OnKeyDown(e);
      this.selectedItemChanged = false;
    }
  }
}
