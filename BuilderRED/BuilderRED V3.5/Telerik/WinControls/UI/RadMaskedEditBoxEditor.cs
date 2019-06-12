// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMaskedEditBoxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class RadMaskedEditBoxEditor : BaseGridEditor
  {
    private int selectionStart;
    private int selectionLength;
    private string oldValue;
    private bool suspendValueChanged;

    public override object Value
    {
      get
      {
        if (this.MaskTextBox.MaskType == MaskType.FreeFormDateTime)
        {
          RadMaskedEditBoxEditorElement editorElement = this.EditorElement as RadMaskedEditBoxEditorElement;
          if (editorElement != null)
          {
            editorElement.Validate(editorElement.Text);
            return editorElement.Provider.Value;
          }
        }
        if (this.MaskTextBox.MaskType == MaskType.DateTime)
          return this.MaskTextBox.Value;
        if (this.MaskTextBox.MaskType == MaskType.Regex || this.MaskTextBox.MaskType == MaskType.EMail)
        {
          RadMaskedEditBoxEditorElement editorElement = this.EditorElement as RadMaskedEditBoxEditorElement;
          this.ValidateRowIndicator(editorElement);
          return editorElement.Provider.Value;
        }
        if (this.MaskTextBox.Value == null)
          return (object) null;
        return (object) this.MaskTextBox.Value.ToString().Replace(this.MaskTextBox.Culture.NumberFormat.CurrencyGroupSeparator, "").Replace(this.MaskTextBox.Culture.NumberFormat.CurrencySymbol, "");
      }
      set
      {
        int maskType = (int) ((RadMaskedEditBoxElement) this.EditorElement).MaskType;
        if (value == null)
        {
          if (this.MaskTextBox.EnableNullValueInput || this.MaskTextBox.MaskType != MaskType.Numeric)
            this.MaskTextBox.Value = (object) this.NullValue;
          else if (this.MaskTextBox.MaskType == MaskType.DateTime)
            this.MaskTextBox.Value = (object) DateTime.Now;
          else
            this.MaskTextBox.Value = (object) 0;
        }
        else
        {
          object obj = this.MaskTextBox.Value;
          string str = obj != null ? obj.ToString() : "";
          if (value.Equals(obj) || !(this.oldValue != str))
            return;
          this.ClearEditorValueIfNeeded();
          this.MaskTextBox.Value = value;
        }
      }
    }

    private void ValidateRowIndicator(RadMaskedEditBoxEditorElement editor)
    {
      RadGridViewElement ancestor = this.MaskTextBox.FindAncestor<RadGridViewElement>();
      if (ancestor == null)
        return;
      if (!((RegexMaskTextBoxProvider) editor.Provider).IsValid)
        ancestor.CurrentRow.ErrorText = "Value is not valid";
      else
        ancestor.CurrentRow.ErrorText = string.Empty;
    }

    protected virtual void ClearEditorValueIfNeeded()
    {
      if (this.MaskTextBox.MaskType != MaskType.Standard)
        return;
      int length = this.MaskTextBox.Mask.Length;
      char promptChar = this.MaskTextBox.PromptChar;
      StringBuilder stringBuilder = new StringBuilder(length);
      for (int index = 0; index < length; ++index)
        stringBuilder.Append(promptChar);
      this.suspendValueChanged = true;
      this.MaskTextBox.Value = (object) stringBuilder.ToString();
      this.suspendValueChanged = false;
    }

    public string NullValue
    {
      get
      {
        string str = this.MaskTextBox.TextBoxItem.NullText;
        if (str == string.Empty)
          str = (string) null;
        return str;
      }
      set
      {
        this.MaskTextBox.TextBoxItem.NullText = value;
      }
    }

    public RadMaskedEditBoxEditorElement MaskTextBox
    {
      get
      {
        return (RadMaskedEditBoxEditorElement) this.EditorElement;
      }
    }

    public override System.Type DataType
    {
      get
      {
        switch (this.MaskTextBox.MaskType)
        {
          case MaskType.DateTime:
          case MaskType.FreeFormDateTime:
            return typeof (DateTime);
          case MaskType.Numeric:
            return typeof (Decimal);
          default:
            return typeof (string);
        }
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadMaskedEditBoxEditorElement editorElement = this.EditorElement as RadMaskedEditBoxEditorElement;
      string str = this.MaskTextBox.Value != null ? this.MaskTextBox.Value.ToString() : this.MaskTextBox.Text;
      if (editorElement.MaskType != MaskType.Regex)
        str = str.Replace(" ", "");
      this.oldValue = str;
      if (!RadTextBoxEditor.IsDarkTheme(this.OwnerElement))
        editorElement.BackColor = Color.White;
      editorElement.TextBoxItem.SelectAll();
      editorElement.TextChanging += new TextChangingEventHandler(this.Editor_TextChanging);
      editorElement.ValueChanged += new EventHandler(this.Editor_TextChanged);
      this.MaskTextBox.KeyDown += new KeyEventHandler(this.MaskTextBox_KeyDown);
      this.MaskTextBox.KeyUp += new KeyEventHandler(this.MaskTextBox_KeyUp);
      this.MaskTextBox.MouseWheel += new MouseEventHandler(this.Editor_MouseWheel);
      this.MaskTextBox.TextBoxItem.HostedControl.Focus();
      this.originalValue = (object) str;
      this.selectionStart = -1;
      this.selectionLength = -1;
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadMaskedEditBoxEditorElement editorElement = this.EditorElement as RadMaskedEditBoxEditorElement;
      editorElement.TextChanging -= new TextChangingEventHandler(this.Editor_TextChanging);
      editorElement.ValueChanged -= new EventHandler(this.Editor_TextChanged);
      this.MaskTextBox.KeyDown -= new KeyEventHandler(this.MaskTextBox_KeyDown);
      this.MaskTextBox.KeyUp -= new KeyEventHandler(this.MaskTextBox_KeyUp);
      this.MaskTextBox.MouseWheel -= new MouseEventHandler(this.Editor_MouseWheel);
      editorElement.Text = string.Empty;
      this.oldValue = (string) null;
      return true;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadMaskedEditBoxEditorElement editorElement = this.EditorElement as RadMaskedEditBoxEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Escape:
        case Keys.Up:
        case Keys.Down:
          base.OnKeyDown(e);
          break;
      }
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      RadMaskedEditBoxEditorElement editorElement1 = this.EditorElement as RadMaskedEditBoxEditorElement;
      if (editorElement1 == null || !editorElement1.IsInValidState(true))
        return;
      RadMaskedEditBoxEditorElement editorElement2 = this.EditorElement as RadMaskedEditBoxEditorElement;
      if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
        return;
      if (this.selectionStart == editorElement2.TextBoxItem.SelectionStart && this.selectionLength == editorElement2.TextBoxItem.SelectionLength)
        base.OnKeyDown(e);
      this.selectionStart = editorElement2.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement2.TextBoxItem.SelectionLength;
    }

    private void MaskTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void MaskTextBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void Editor_MouseWheel(object sender, MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    private void Editor_TextChanged(object sender, EventArgs e)
    {
      if (this.suspendValueChanged || !(this.oldValue != this.MaskTextBox.TextBoxItem.Text))
        return;
      this.OnValueChanged();
      this.oldValue = this.MaskTextBox.TextBoxItem.Text;
    }

    private void Editor_TextChanging(object sender, TextChangingEventArgs e)
    {
      if (!(e.NewValue != this.MaskTextBox.TextBoxItem.Text))
        return;
      if (!this.suspendValueChanged)
      {
        ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) this.MaskTextBox.Text, (object) this.oldValue);
        this.OnValueChanging(e1);
        e.Cancel = e1.Cancel;
      }
      this.oldValue = this.MaskTextBox.TextBoxItem.Text;
    }

    protected override RadElement CreateEditorElement()
    {
      RadMaskedEditBoxEditorElement boxEditorElement = new RadMaskedEditBoxEditorElement();
      boxEditorElement.TextBoxItem.RouteMessages = false;
      return (RadElement) boxEditorElement;
    }
  }
}
