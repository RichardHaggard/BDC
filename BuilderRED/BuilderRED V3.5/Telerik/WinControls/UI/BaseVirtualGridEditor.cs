// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseVirtualGridEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class BaseVirtualGridEditor : BaseInputEditor
  {
    private bool restoringValue;
    private RadVirtualGridElement virtualGridElement;

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
        VirtualGridCellElement ownerElement = this.OwnerElement as VirtualGridCellElement;
        if (ownerElement != null && ownerElement.Value != null)
          return ownerElement.Value.GetType();
        return (System.Type) null;
      }
    }

    public override void Initialize(object owner, object value)
    {
      this.OwnerElement = owner as RadElement;
      this.originalValue = value;
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
      (this.OwnerElement as VirtualGridCellElement)?.RowElement.TableElement.GridElement.OnValueChanging((object) this, e);
    }

    public override void OnValueChanged()
    {
      if (this.restoringValue)
        return;
      base.OnValueChanged();
      (this.OwnerElement as VirtualGridCellElement)?.RowElement.TableElement.GridElement.OnValueChanged((object) this, EventArgs.Empty);
    }

    public virtual void OnKeyDown(KeyEventArgs keyEventArgs)
    {
      if (this.OwnerElement == null || !this.OwnerElement.IsInValidState(true))
        return;
      if (this.virtualGridElement == null)
        this.virtualGridElement = this.OwnerElement.ElementTree.RootElement.FindDescendant<RadVirtualGridElement>();
      if (this.virtualGridElement == null)
        return;
      keyEventArgs.Handled = this.virtualGridElement.InputBehavior.HandleKeyDown(keyEventArgs);
    }

    public virtual void OnMouseWheel(MouseEventArgs mouseEventArgs)
    {
    }
  }
}
