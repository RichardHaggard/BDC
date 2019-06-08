// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorBaseElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public abstract class EditorBaseElement : RadEditorElement, IInputEditor, IValueEditor, ISupportInitialize
  {
    private static readonly object ValidatingEventKey = new object();
    private static readonly object ValueChangedEventKey = new object();
    private static readonly object ValueChangingEventKey = new object();
    private static readonly object ValidationErrorEventKey = new object();
    internal const long InitializingStateKey = 8796093022208;
    internal const long CallEditorHandlerStateKey = 17592186044416;
    internal const long EditorBaseElementLastStateKey = 17592186044416;
    private object defaultValue;
    protected IEditorHandler EditorHandler;
    protected object originalValue;
    private object nullValue;
    private IEditorManager editorManager;
    private RadItem editorElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.editorElement = (RadItem) this;
    }

    public virtual RadItem EditorElement
    {
      get
      {
        return this.editorElement;
      }
      set
      {
        this.editorElement = value;
      }
    }

    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.Events.AddHandler(EditorBaseElement.ValueChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(EditorBaseElement.ValueChangingEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the editor finished the value editing.")]
    [Category("Action")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.Events.AddHandler(EditorBaseElement.ValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(EditorBaseElement.ValueChangedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler QueryValue;

    public event ValidationErrorEventHandler ValidationError
    {
      add
      {
        this.Events.AddHandler(EditorBaseElement.ValidationErrorEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(EditorBaseElement.ValidationErrorEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler Validating;

    public event EventHandler Validated;

    protected virtual void OnValidationError(ValidationErrorEventArgs args)
    {
      ValidationErrorEventHandler errorEventHandler = (ValidationErrorEventHandler) this.Events[EditorBaseElement.ValidationErrorEventKey];
      if (errorEventHandler == null)
        return;
      errorEventHandler((object) this, args);
    }

    protected virtual void OnValidationError(string message)
    {
      this.OnValidationError(new ValidationErrorEventArgs((object) null, new Exception(message)));
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.EditorHandler != null && this.GetBitState(17592186044416L))
      {
        this.EditorHandler.HandleEditorKeyDown(e);
        this.BitState[17592186044416L] = false;
      }
      else
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.GetBitState(17592186044416L) && this.EditorHandler != null)
        this.EditorHandler.HandleEditorKeyUp(e);
      this.BitState[17592186044416L] = false;
      base.OnKeyUp(e);
    }

    protected override void OnTextChanging(TextChangingEventArgs e)
    {
      base.OnTextChanging(e);
      if (e.Cancel)
        return;
      ValueChangingEventArgs args = new ValueChangingEventArgs((object) e.NewValue, (object) e.OldValue);
      this.OnValueChanging(args);
      e.Cancel = args.Cancel;
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.OnValueChanged(new EventArgs());
    }

    protected virtual void OnValidating(ValueChangingEventArgs e)
    {
      if (this.Validating == null)
        return;
      this.Validating((object) this, (CancelEventArgs) e);
    }

    protected virtual void OnValidated(EventArgs e)
    {
      if (this.Validated == null)
        return;
      this.Validated((object) this, e);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs args)
    {
      if (this.GetBitState(8796093022208L))
        return;
      if (this.EditorHandler != null)
      {
        this.EditorHandler.HandleEditorValueChanging(args);
        if (args.Cancel)
          return;
      }
      ValueChangingEventHandler changingEventHandler = (ValueChangingEventHandler) this.Events[EditorBaseElement.ValueChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, args);
    }

    protected virtual void OnValueChanged(EventArgs args)
    {
      if (this.GetBitState(8796093022208L))
        return;
      if (this.EditorHandler != null)
        this.EditorHandler.HandleEditorValueChanged(args);
      EventHandler eventHandler = (EventHandler) this.Events[EditorBaseElement.ValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, args);
    }

    protected virtual void OnQueryValue(CancelEventArgs e)
    {
      if (this.QueryValue == null)
        return;
      this.QueryValue((object) this, e);
    }

    protected internal virtual Form FindForm()
    {
      if (this.ElementTree != null)
        return this.ElementTree.Control.FindForm();
      return (Form) null;
    }

    public virtual void BeginInit()
    {
      this.BitState[8796093022208L] = true;
    }

    public virtual void EndInit()
    {
      this.BitState[8796093022208L] = false;
    }

    public void ProcessKeyPress(KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    public void ProcessKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    public void ProcessKeyUp(KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    public void ProcessMouseEnter(EventArgs e)
    {
      this.OnMouseEnter(e);
    }

    public void ProcessMouseLeave(EventArgs e)
    {
      this.OnMouseLeave(e);
    }

    public void ProcessMouseUp(MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    public void ProcessMouseDown(MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    public void ProcessMouseMove(MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    public void ProcessMouseWheel(MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    public object NullValue
    {
      get
      {
        return this.nullValue;
      }
      set
      {
        if (this.nullValue == value)
          return;
        this.nullValue = value;
        this.OnNotifyPropertyChanged(nameof (NullValue));
      }
    }

    public virtual string EditorType
    {
      get
      {
        return this.GetType().ToString();
      }
    }

    public virtual bool IsModified
    {
      get
      {
        return !object.Equals(this.Value, this.originalValue);
      }
    }

    protected virtual void OnFormat(ConvertEventArgs e)
    {
      if (this.Format == null)
        return;
      this.Format((object) this, e);
    }

    public event ConvertEventHandler Format;

    protected virtual void OnParse(ConvertEventArgs e)
    {
      if (this.Parse == null)
        return;
      this.Parse((object) this, e);
    }

    public event ConvertEventHandler Parse;

    public virtual bool IsNestedEditor
    {
      get
      {
        return false;
      }
    }

    public RadElement FocusableElement()
    {
      return (RadElement) this;
    }

    public virtual void Initialize()
    {
    }

    public virtual void Initialize(object value)
    {
      this.Initialize();
      this.originalValue = value;
      this.Value = value;
    }

    public void Initialize(object owner, object value)
    {
      this.EditorHandler = owner as IEditorHandler;
      this.Initialize(value);
    }

    public virtual object Value
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    public object DefaultValue
    {
      get
      {
        return this.defaultValue;
      }
      set
      {
        if (this.defaultValue == value)
          return;
        this.defaultValue = value;
        this.OnNotifyPropertyChanged(nameof (DefaultValue));
      }
    }

    public virtual object MinValue
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    public virtual object MaxValue
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    public virtual EditorVisualMode VisualMode
    {
      get
      {
        return EditorVisualMode.Default;
      }
    }

    public virtual EditorSupportedType SupportedType
    {
      get
      {
        return EditorSupportedType.AlphaNumeric;
      }
    }

    public virtual void BeginEdit()
    {
    }

    public virtual bool EndEdit()
    {
      return true;
    }

    public virtual bool Validate()
    {
      this.ValidateCore();
      return true;
    }

    protected virtual void ValidateCore()
    {
      ValueChangingEventArgs e = new ValueChangingEventArgs(this.Value);
      this.OnValidating(e);
      if (!e.Cancel)
      {
        this.OnValidated(new EventArgs());
      }
      else
      {
        this.OnValidationError("On Validating canceled");
        this.Value = this.originalValue;
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

    public bool CaptureMouse()
    {
      this.Capture = true;
      return this.Capture;
    }

    public void ReleaseMouseCapture()
    {
      this.Capture = false;
    }

    public virtual bool Focusable
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    public bool IsEnabled
    {
      get
      {
        return this.Enabled;
      }
    }

    public bool IsMouseCaptured
    {
      get
      {
        return this.Capture;
      }
    }
  }
}
