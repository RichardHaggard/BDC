// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseSpinEditor
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
  public class BaseSpinEditor : BaseInputEditor
  {
    private System.Type valueType = typeof (Decimal);
    protected int selectionStart;
    protected int selectionLength;

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
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      editorElement.ValueChanging += new ValueChangingEventHandler(this.spinElement_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.spinElement_ValueChanged);
      editorElement.KeyDown += new KeyEventHandler(this.spinElement_KeyDown);
      editorElement.KeyUp += new KeyEventHandler(this.spinElement_KeyUp);
      editorElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.spinElement_RadPropertyChanged);
      if (!BaseTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) editorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
      }
      editorElement.TextBoxItem.SelectAll();
      editorElement.TextBoxItem.TextBoxControl.Focus();
      editorElement.RightToLeft = this.RightToLeft;
      editorElement.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
    }

    private void spinElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.ContainsFocusProperty || (bool) e.NewValue)
        return;
      this.OnLostFocus();
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.spinElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.spinElement_ValueChanged);
      editorElement.KeyDown -= new KeyEventHandler(this.spinElement_KeyDown);
      editorElement.KeyUp -= new KeyEventHandler(this.spinElement_KeyUp);
      editorElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.spinElement_RadPropertyChanged);
      editorElement.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      editorElement.EnableValueChangingOnTextChanging = false;
      editorElement.Value = editorElement.MinValue;
      return true;
    }

    public override bool Validate()
    {
      (this.EditorElement as BaseSpinEditorElement).Validate();
      return base.Validate();
    }

    public virtual void OnLostFocus()
    {
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
    }

    private void spinElement_KeyDown(object sender, KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.selectionStart = editorElement.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement.TextBoxItem.SelectionLength;
      this.OnKeyDown(e);
    }

    private void spinElement_KeyUp(object sender, KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
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

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus();
    }

    public override void Initialize(object owner, object value)
    {
      RadSpinElement editorElement = this.EditorElement as RadSpinElement;
      editorElement.MaxValue = new Decimal(int.MaxValue);
      editorElement.MinValue = new Decimal(int.MinValue);
      base.Initialize(owner, value);
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new BaseSpinEditorElement();
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
        try
        {
          Decimal num = Convert.ToDecimal(value);
          if (num < editorElement.MinValue)
            num = editorElement.MinValue;
          else if (num > editorElement.MaxValue)
            num = editorElement.MaxValue;
          editorElement.Value = num;
        }
        catch
        {
          try
          {
            editorElement.Value = Decimal.Parse(value.ToString());
          }
          catch
          {
            editorElement.Value = editorElement.MinValue;
          }
        }
      }
    }
  }
}
