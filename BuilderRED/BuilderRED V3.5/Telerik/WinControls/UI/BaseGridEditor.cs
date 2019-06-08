// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGridEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class BaseGridEditor : BaseInputEditor
  {
    private bool restoringValue;
    private RadGridViewElement gridRootElement;

    public virtual bool EndEditOnLostFocus
    {
      get
      {
        return true;
      }
    }

    public virtual bool ClearCellText
    {
      get
      {
        return true;
      }
    }

    public override System.Type DataType
    {
      get
      {
        GridDataCellElement ownerElement = this.OwnerElement as GridDataCellElement;
        if (ownerElement == null)
          return (System.Type) null;
        System.Type dataType = ((GridViewDataColumn) ownerElement.ColumnInfo).DataType;
        if ((object) dataType != null)
          return dataType;
        return typeof (string);
      }
    }

    public override void Initialize(object owner, object value)
    {
      this.OwnerElement = owner as RadElement;
      this.originalValue = value;
      GridCellElement gridCellElement = owner as GridCellElement;
      if (gridCellElement != null)
      {
        bool coerceNullValue = (object) this.DataType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState);
        value = RadDataConverter.Instance.Format(value, this.DataType, coerceNullValue, (IDataConversionInfoProvider) (gridCellElement.ColumnInfo as GridViewDataColumn));
      }
      this.Value = value;
    }

    public override bool Validate()
    {
      ValueChangingEventArgs changingEventArgs = new ValueChangingEventArgs(this.Value);
      this.OnValidating((CancelEventArgs) changingEventArgs);
      if (changingEventArgs.Cancel)
      {
        this.restoringValue = true;
        this.Value = this.originalValue;
        this.restoringValue = false;
        return false;
      }
      this.OnValidated();
      return true;
    }

    public override void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.restoringValue)
        return;
      base.OnValueChanging(e);
      GridCellElement ownerElement = this.OwnerElement as GridCellElement;
      if (ownerElement == null || ownerElement.ViewTemplate == null)
        return;
      ownerElement.ViewTemplate.EventDispatcher.RaiseEvent<ValueChangingEventArgs>(EventDispatcher.ValueChanging, (object) this, e);
    }

    public override void OnValueChanged()
    {
      if (this.restoringValue)
        return;
      base.OnValueChanged();
      GridCellElement ownerElement = this.OwnerElement as GridCellElement;
      if (ownerElement == null || ownerElement.ViewTemplate == null)
        return;
      ownerElement.ViewTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.ValueChanged, (object) this, EventArgs.Empty);
    }

    public virtual void OnKeyDown(KeyEventArgs keyEventArgs)
    {
      if (this.OwnerElement == null || !this.OwnerElement.IsInValidState(true))
        return;
      if (this.gridRootElement == null)
        this.gridRootElement = this.OwnerElement.ElementTree.RootElement.FindDescendant<RadGridViewElement>();
      if (this.gridRootElement == null)
        return;
      keyEventArgs.Handled = this.gridRootElement.GridBehavior.ProcessKeyDown(keyEventArgs);
    }

    public virtual void OnMouseWheel(MouseEventArgs mouseEventArgs)
    {
    }
  }
}
