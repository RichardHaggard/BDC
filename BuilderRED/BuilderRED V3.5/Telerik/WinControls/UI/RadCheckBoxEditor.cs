// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class RadCheckBoxEditor : BaseGridEditor
  {
    private bool threeState;

    public bool ThreeState
    {
      get
      {
        return this.threeState;
      }
      set
      {
        if (value == this.threeState)
          return;
        this.threeState = value;
        this.SetValue(this.Value);
      }
    }

    public override object Value
    {
      get
      {
        return (object) (this.EditorElement as RadCheckBoxEditorElement).CheckState;
      }
      set
      {
        this.SetValue(value);
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
        if ((object) this.originalValue.GetType() == (object) this.Value.GetType())
          return base.IsModified;
        if (this.originalValue is bool)
        {
          Telerik.WinControls.Enumerations.ToggleState toggleState = (Telerik.WinControls.Enumerations.ToggleState) this.Value;
          bool originalValue = (bool) this.originalValue;
          if (toggleState == Telerik.WinControls.Enumerations.ToggleState.On && originalValue)
            return false;
          if (toggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
            return originalValue;
          return true;
        }
        if (this.originalValue is Telerik.WinControls.Enumerations.ToggleState)
          return !this.Value.Equals((object) (Telerik.WinControls.Enumerations.ToggleState) this.originalValue);
        return base.IsModified;
      }
    }

    private void SetValue(object value)
    {
      Telerik.WinControls.Enumerations.ToggleState toggleState = (Telerik.WinControls.Enumerations.ToggleState) value;
      if (!this.threeState && toggleState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
        toggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      RadCheckBoxEditorElement editorElement = this.EditorElement as RadCheckBoxEditorElement;
      if (this.IsInitalizing)
      {
        editorElement.CheckState = toggleState;
      }
      else
      {
        object newValue = (object) null;
        object oldValue = (object) null;
        switch (toggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            newValue = (object) false;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            newValue = (object) true;
            break;
        }
        switch (editorElement.CheckState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            oldValue = (object) false;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            oldValue = (object) true;
            break;
        }
        ValueChangingEventArgs e = new ValueChangingEventArgs(newValue, oldValue);
        this.OnValueChanging(e);
        if (e.Cancel)
          return;
        editorElement.CheckState = toggleState;
        this.OnValueChanged();
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (Telerik.WinControls.Enumerations.ToggleState);
      }
    }

    public override void Initialize(object owner, object value)
    {
      base.Initialize(owner, value);
      this.originalValue = value ?? (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      this.EditorElement.Focus();
    }

    public void ToggleState()
    {
      RadCheckBoxEditorElement editorElement = (RadCheckBoxEditorElement) this.EditorElement;
      if (this.ThreeState)
      {
        switch (editorElement.CheckState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            this.Value = (object) Telerik.WinControls.Enumerations.ToggleState.On;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            this.Value = (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            this.Value = (object) Telerik.WinControls.Enumerations.ToggleState.Off;
            break;
        }
      }
      else if (editorElement.CheckState == Telerik.WinControls.Enumerations.ToggleState.Off || editorElement.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
        this.Value = (object) Telerik.WinControls.Enumerations.ToggleState.On;
      else
        this.Value = (object) Telerik.WinControls.Enumerations.ToggleState.Off;
    }

    public override void OnMouseWheel(MouseEventArgs mouseEventArgs)
    {
      GridCellElement ownerElement = this.OwnerElement as GridCellElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || !ownerElement.GridViewElement.EditorManager.IsInEditMode)
        return;
      ownerElement.GridViewElement.GridBehavior.OnMouseWheel(mouseEventArgs);
      HandledMouseEventArgs handledMouseEventArgs = mouseEventArgs as HandledMouseEventArgs;
      if (handledMouseEventArgs == null)
        return;
      handledMouseEventArgs.Handled = true;
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadCheckBoxEditorElement(this);
    }

    internal void RaiseKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }
  }
}
