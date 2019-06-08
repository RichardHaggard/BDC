// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseInputEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public abstract class BaseInputEditor : IInputEditor, IValueEditor, ISupportInitialize
  {
    private bool isActive;
    protected bool isInitializing;
    private bool isInBeginEditMode;
    protected object originalValue;
    private RadElement editorElement;
    private IEditorManager editorManager;
    private RadElement ownerElement;

    public bool IsActive
    {
      get
      {
        return this.isActive;
      }
    }

    public bool IsInitalizing
    {
      get
      {
        return this.isInitializing;
      }
    }

    public bool IsInBeginEditMode
    {
      get
      {
        return this.isInBeginEditMode;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetIsInBeginEditMode(bool value)
    {
      this.isInBeginEditMode = value;
    }

    public RadElement OwnerElement
    {
      get
      {
        return this.ownerElement;
      }
      protected set
      {
        this.ownerElement = value;
      }
    }

    public bool RightToLeft
    {
      get
      {
        if (this.EditorElement.IsInValidState(true))
          return this.EditorElement.ElementTree.Control.RightToLeft == System.Windows.Forms.RightToLeft.Yes;
        return false;
      }
    }

    public IEditorManager EditorManager
    {
      get
      {
        return this.editorManager;
      }
      set
      {
        this.editorManager = value;
      }
    }

    public abstract System.Type DataType { get; }

    public virtual object Value
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

    public virtual bool IsModified
    {
      get
      {
        if (this.originalValue == null)
          return this.originalValue != this.Value;
        return !this.originalValue.Equals(this.Value);
      }
    }

    public virtual RadElement EditorElement
    {
      get
      {
        if (this.editorElement == null)
          this.editorElement = this.CreateEditorElement();
        return this.editorElement;
      }
    }

    public virtual void Initialize(object owner, object value)
    {
      this.ownerElement = owner as RadElement;
      this.originalValue = value;
      this.Value = value;
    }

    public virtual void BeginEdit()
    {
      this.isActive = true;
      RadItem ownerElement = this.OwnerElement as RadItem;
      if (ownerElement == null)
        return;
      ownerElement.IsFocusable = false;
    }

    public virtual bool EndEdit()
    {
      RadItem ownerElement = this.OwnerElement as RadItem;
      if (ownerElement != null)
        ownerElement.IsFocusable = true;
      this.isActive = false;
      this.originalValue = (object) null;
      return true;
    }

    public virtual bool Validate()
    {
      ValueChangingEventArgs changingEventArgs = new ValueChangingEventArgs(this.Value);
      this.OnValidating((CancelEventArgs) changingEventArgs);
      if (changingEventArgs.Cancel)
      {
        this.Value = this.originalValue;
        return false;
      }
      this.OnValidated();
      return true;
    }

    public event ValueChangingEventHandler ValueChanging;

    public event EventHandler ValueChanged;

    public event CancelEventHandler Validating;

    public event EventHandler Validated;

    public event ValidationErrorEventHandler ValidationError;

    public void BeginInit()
    {
      this.isInitializing = true;
    }

    public void EndInit()
    {
      this.isInitializing = false;
    }

    public virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    public virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    public virtual void OnValidating(CancelEventArgs e)
    {
      if (this.Validating == null)
        return;
      this.Validating((object) this, e);
    }

    public virtual void OnValidated()
    {
      if (this.Validated == null)
        return;
      this.Validated((object) this, EventArgs.Empty);
    }

    public virtual void OnValidationError(ValidationErrorEventArgs args)
    {
      if (this.ValidationError == null)
        return;
      this.ValidationError((object) this, args);
    }

    protected abstract RadElement CreateEditorElement();
  }
}
