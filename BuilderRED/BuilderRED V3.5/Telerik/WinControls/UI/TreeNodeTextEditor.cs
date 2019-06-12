// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeTextEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Telerik.WinControls.UI
{
  public class TreeNodeTextEditor : RadTextBoxItem, IValueEditor
  {
    public void Initialize(object owner, object value)
    {
      if (value == null)
        return;
      this.Text = value.ToString();
    }

    public void BeginEdit()
    {
    }

    public bool EndEdit()
    {
      return true;
    }

    public bool Validate()
    {
      return true;
    }

    public object Value
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public event ValueChangingEventHandler ValueChanging;

    public event EventHandler ValueChanged;

    public event ValidationErrorEventHandler ValidationError;

    protected virtual void OnValueChanged(EventArgs args)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, args);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs args)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, args);
    }

    protected virtual void OnValidationError(ValidationErrorEventArgs args)
    {
      if (this.ValidationError == null)
        return;
      this.ValidationError((object) this, args);
    }

    [SpecialName]
    void IValueEditor.add_Validating(CancelEventHandler _param1)
    {
      this.Validating += _param1;
    }

    [SpecialName]
    void IValueEditor.remove_Validating(CancelEventHandler _param1)
    {
      this.Validating -= _param1;
    }

    [SpecialName]
    void IValueEditor.add_Validated(EventHandler _param1)
    {
      this.Validated += _param1;
    }

    [SpecialName]
    void IValueEditor.remove_Validated(EventHandler _param1)
    {
      this.Validated -= _param1;
    }
  }
}
