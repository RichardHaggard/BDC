// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class GridSpinEditor : BaseGridEditor
  {
    private System.Type valueType = typeof (Decimal);
    private int selectionStart;
    private int selectionLength;

    public override object Value
    {
      get
      {
        return this.GetEditorValue();
      }
      set
      {
        this.SetEditorValue(value);
      }
    }

    private object GetEditorValue()
    {
      RadSpinElement editorElement = this.EditorElement as RadSpinElement;
      editorElement.Validate();
      if (string.IsNullOrEmpty(editorElement.Text))
        return (object) null;
      return (object) editorElement.Value;
    }

    private void SetEditorValue(object value)
    {
      RadSpinElement editorElement = this.EditorElement as RadSpinElement;
      if (value == null)
      {
        editorElement.Text = string.Empty;
      }
      else
      {
        Decimal num = Convert.ToDecimal(value);
        if (num < editorElement.MinValue)
          num = editorElement.MinValue;
        else if (num > editorElement.MaxValue)
          num = editorElement.MaxValue;
        editorElement.Value = num;
      }
    }

    public override bool IsModified
    {
      get
      {
        if (this.originalValue == null || this.originalValue == DBNull.Value)
        {
          if (this.Value != null)
            return this.Value != DBNull.Value;
          return false;
        }
        if (this.Value == null || this.Value == DBNull.Value)
        {
          if (this.originalValue != null)
            return this.originalValue != DBNull.Value;
          return false;
        }
        GridCellElement ownerElement = this.OwnerElement as GridCellElement;
        if (ownerElement != null)
        {
          TypeConverter dataTypeConverter = ((GridViewDataColumn) ownerElement.ColumnInfo).DataTypeConverter;
          if (dataTypeConverter != null && dataTypeConverter.CanConvertTo(typeof (Decimal)))
            return !((Decimal) dataTypeConverter.ConvertTo(this.originalValue, typeof (Decimal))).Equals(Convert.ToDecimal(this.Value));
        }
        return !Convert.ToDecimal(this.originalValue).Equals(Convert.ToDecimal(this.Value));
      }
    }

    public Decimal MinValue
    {
      get
      {
        return ((RadSpinElement) this.EditorElement).MinValue;
      }
      set
      {
        ((RadSpinElement) this.EditorElement).MinValue = value;
      }
    }

    public Decimal MaxValue
    {
      get
      {
        return ((RadSpinElement) this.EditorElement).MaxValue;
      }
      set
      {
        ((RadSpinElement) this.EditorElement).MaxValue = value;
      }
    }

    public Decimal Step
    {
      get
      {
        return ((RadSpinElement) this.EditorElement).Step;
      }
      set
      {
        ((RadSpinElement) this.EditorElement).Step = value;
      }
    }

    public int DecimalPlaces
    {
      get
      {
        return ((RadSpinElement) this.EditorElement).DecimalPlaces;
      }
      set
      {
        ((RadSpinElement) this.EditorElement).DecimalPlaces = value;
      }
    }

    public bool ThousandsSeparator
    {
      get
      {
        return ((RadSpinElement) this.EditorElement).ThousandsSeparator;
      }
      set
      {
        ((RadSpinElement) this.EditorElement).ThousandsSeparator = value;
      }
    }

    public System.Type ValueType
    {
      get
      {
        return this.valueType;
      }
      set
      {
        this.valueType = value;
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (Decimal);
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadSpinEditorElement editorElement = this.EditorElement as RadSpinEditorElement;
      editorElement.SetTextValueChanged(true);
      editorElement.ValueChanging += new ValueChangingEventHandler(this.spinElement_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.spinElement_ValueChanged);
      editorElement.KeyDown += new KeyEventHandler(this.spinElement_KeyDown);
      editorElement.KeyUp += new KeyEventHandler(this.spinElement_KeyUp);
      if (!RadTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) editorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
      }
      editorElement.TextBoxItem.SelectAll();
      editorElement.TextBoxItem.TextBoxControl.Focus();
      editorElement.RightToLeft = this.RightToLeft;
      editorElement.EnableValueChangingOnTextChanging = this.OwnerElement is GridFilterCellElement;
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadSpinEditorElement editorElement = this.EditorElement as RadSpinEditorElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.spinElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.spinElement_ValueChanged);
      editorElement.KeyDown -= new KeyEventHandler(this.spinElement_KeyDown);
      editorElement.KeyUp -= new KeyEventHandler(this.spinElement_KeyUp);
      editorElement.EnableValueChangingOnTextChanging = false;
      editorElement.Value = editorElement.MinValue;
      return true;
    }

    public override bool Validate()
    {
      (this.EditorElement as RadSpinEditorElement).Validate();
      return base.Validate();
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadSpinEditorElement editorElement = this.EditorElement as RadSpinEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.selectionStart = editorElement.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement.TextBoxItem.SelectionLength;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          editorElement.Validate();
          base.OnKeyDown(e);
          break;
        case Keys.Delete:
          if (this.selectionLength != editorElement.TextBoxItem.TextLength)
            break;
          editorElement.Text = (string) null;
          break;
      }
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      RadSpinEditorElement editorElement = this.EditorElement as RadSpinEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if ((!this.RightToLeft || this.selectionStart != editorElement.Text.Length) && (this.RightToLeft || this.selectionStart != 0 || this.selectionLength != 0))
            break;
          editorElement.Validate();
          base.OnKeyDown(e);
          break;
        case Keys.Right:
          if ((!this.RightToLeft || this.selectionStart != 0) && (this.RightToLeft || this.selectionStart != editorElement.Text.Length))
            break;
          editorElement.Validate();
          base.OnKeyDown(e);
          break;
      }
    }

    private void spinElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void spinElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void spinElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void spinElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadSpinEditorElement();
    }
  }
}
