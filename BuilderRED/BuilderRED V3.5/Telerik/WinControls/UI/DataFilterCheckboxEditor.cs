// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCheckboxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterCheckboxEditor : BaseInputEditor
  {
    public override object Value
    {
      get
      {
        return (object) ((this.EditorElement as DataFilterCheckboxEditorElement).CheckState == Telerik.WinControls.Enumerations.ToggleState.On);
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
          bool flag = (bool) this.Value;
          bool originalValue = (bool) this.originalValue;
          if (flag && originalValue)
            return false;
          if (!flag)
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
      bool flag = value != null && !TelerikHelper.StringIsNullOrWhiteSpace(value.ToString()) && Convert.ToBoolean(value);
      DataFilterCheckboxEditorElement editorElement = this.EditorElement as DataFilterCheckboxEditorElement;
      if (this.IsInitalizing)
      {
        editorElement.CheckState = flag ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      else
      {
        object newValue = (object) flag;
        object oldValue = (object) null;
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
        editorElement.CheckState = flag ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.OnValueChanged();
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (bool);
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      this.EditorElement.Focus();
    }

    public void ToggleState()
    {
      DataFilterCheckboxEditorElement editorElement = (DataFilterCheckboxEditorElement) this.EditorElement;
      if (editorElement.CheckState == Telerik.WinControls.Enumerations.ToggleState.Off || editorElement.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
        this.Value = (object) true;
      else
        this.Value = (object) false;
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new DataFilterCheckboxEditorElement(this);
    }

    internal void RaiseKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    public virtual void OnKeyDown(KeyEventArgs e)
    {
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return)
      {
        ownerElement.TreeViewElement.EndEdit();
      }
      else
      {
        if (e.KeyCode != Keys.Escape)
          return;
        ownerElement.TreeViewElement.CancelEdit();
      }
    }
  }
}
