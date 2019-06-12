// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseColorEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseColorEditor : BaseInputEditor
  {
    protected System.Type valueType = typeof (Color);

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadColorBoxElement();
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (Color);
      }
    }

    public override object Value
    {
      get
      {
        return (object) this.GetEditorValue();
      }
      set
      {
        this.SetEditorValue(value);
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
      editorElement.ValueChanging += new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.editorElement_ValueChanged);
      editorElement.KeyDown += new KeyEventHandler(this.editorElement_KeyDown);
      editorElement.KeyUp += new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.TextBoxItem.SelectAll();
      editorElement.TextBoxItem.TextBoxControl.Focus();
      editorElement.RightToLeft = this.RightToLeft;
      editorElement.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.editorElement_ValueChanged);
      editorElement.KeyDown -= new KeyEventHandler(this.editorElement_KeyDown);
      editorElement.KeyUp -= new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      return true;
    }

    public override bool Validate()
    {
      if (((RadColorBoxElement) this.EditorElement).Validate())
        return base.Validate();
      return false;
    }

    protected virtual void OnLostFocus()
    {
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus();
    }

    private void editorElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void editorElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void editorElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void editorElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    private Color GetEditorValue()
    {
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
      editorElement.Validate();
      return editorElement.Value;
    }

    private void SetEditorValue(object value)
    {
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
      if (value is Color)
      {
        editorElement.Value = (Color) value;
      }
      else
      {
        object obj = converter.ConvertFromString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, value.ToString());
        if (obj == null)
          editorElement.Value = Color.Empty;
        else
          editorElement.Value = (Color) obj;
      }
    }
  }
}
