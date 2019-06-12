// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadToggleButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadToggleButtonElement : RadButtonElement
  {
    public static RadProperty ToggleStateProperty = RadProperty.Register(nameof (ToggleState), typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (RadToggleButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off, ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.Cancelable));
    public static RoutedEvent ToggleStateChangingRoutedEvent = RadElement.RegisterRoutedEvent(nameof (ToggleStateChangingRoutedEvent), typeof (RadToggleButtonElement));
    public static RoutedEvent CheckedRoutedEvent = RadElement.RegisterRoutedEvent(nameof (CheckedRoutedEvent), typeof (RadToggleButtonElement));
    public static RoutedEvent Indeterminate = RadElement.RegisterRoutedEvent(nameof (Indeterminate), typeof (RadToggleButtonElement));
    public static RoutedEvent Unchecked = RadElement.RegisterRoutedEvent(nameof (Unchecked), typeof (RadToggleButtonElement));
    private static readonly object StateChangingEventKey;
    private static readonly object StateChangedEventKey;
    private static readonly object IsCheckedChangedKey;
    private static readonly object CheckStateChangingEventKey;
    private static readonly object CheckStateChangedEventKey;
    private bool isTreeState;
    private bool readOnly;

    static RadToggleButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ToggleButtonStateManagerFactory(), typeof (RadToggleButtonElement));
      RadToggleButtonElement.StateChangedEventKey = new object();
      RadToggleButtonElement.StateChangingEventKey = new object();
      RadToggleButtonElement.IsCheckedChangedKey = new object();
      RadToggleButtonElement.CheckStateChangedEventKey = new object();
      RadToggleButtonElement.CheckStateChangingEventKey = new object();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ButtonFillElement.Class = "ToggleButtonFill";
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Action")]
    [Description("Occurs before the elements's state changes.")]
    public event StateChangingEventHandler ToggleStateChanging
    {
      add
      {
        this.Events.AddHandler(RadToggleButtonElement.StateChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButtonElement.StateChangingEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the elements's state changes.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Action")]
    public event StateChangedEventHandler ToggleStateChanged
    {
      add
      {
        this.Events.AddHandler(RadToggleButtonElement.StateChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButtonElement.StateChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs before the elements's check state changes.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Action")]
    [Browsable(true)]
    public event CheckStateChangingEventHandler CheckStateChanging
    {
      add
      {
        this.Events.AddHandler(RadToggleButtonElement.CheckStateChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButtonElement.CheckStateChangingEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the elements's check state changes.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Action")]
    public event EventHandler CheckStateChanged
    {
      add
      {
        this.Events.AddHandler(RadToggleButtonElement.CheckStateChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButtonElement.CheckStateChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanging(StateChangingEventArgs e)
    {
      if (e.NewValue == Telerik.WinControls.Enumerations.ToggleState.Indeterminate && !this.isTreeState)
        e.Cancel = true;
      StateChangingEventHandler changingEventHandler = (StateChangingEventHandler) this.Events[RadToggleButtonElement.StateChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCheckStateChanging(CheckStateChangingEventArgs e)
    {
      CheckStateChangingEventHandler changingEventHandler = (CheckStateChangingEventHandler) this.Events[RadToggleButtonElement.CheckStateChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanged(StateChangedEventArgs e)
    {
      StateChangedEventHandler changedEventHandler = (StateChangedEventHandler) this.Events[RadToggleButtonElement.StateChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCheckStateChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadToggleButtonElement.CheckStateChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnIsCheckedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadToggleButtonElement.IsCheckedChangedKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Left || this.readOnly)
        return;
      this.OnToggle();
    }

    protected internal virtual void OnToggle()
    {
      if (this.isTreeState)
      {
        switch (this.ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            break;
        }
      }
      else
      {
        switch (this.ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            break;
        }
      }
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "Click", (object) string.Format("{0}.{1}", (object) this.Text, (object) this.ToggleState.ToString()));
    }

    [Description("Gets or sets the Check state. Check state enumeration defines the following values: Uncheck, Indeterminate, and Check.")]
    [Browsable(true)]
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(CheckState.Unchecked)]
    public CheckState CheckState
    {
      get
      {
        return (CheckState) this.GetValue(RadToggleButtonElement.ToggleStateProperty);
      }
      set
      {
        if (this.CheckState == value)
          return;
        this.SetToggleState((Telerik.WinControls.Enumerations.ToggleState) value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the toggle state. Toggle state enumeration defines the following values: Off, Indeterminate, and On.")]
    [Bindable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("ToggleState", typeof (RadToggleButtonElement))]
    public Telerik.WinControls.Enumerations.ToggleState ToggleState
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(RadToggleButtonElement.ToggleStateProperty);
      }
      set
      {
        if (this.ToggleState == value)
          return;
        this.SetToggleState(value);
      }
    }

    protected virtual void SetToggleState(Telerik.WinControls.Enumerations.ToggleState value)
    {
      this.SetToggleStateCore(value);
    }

    private void SetToggleStateCore(Telerik.WinControls.Enumerations.ToggleState value)
    {
      StateChangingEventArgs e1 = new StateChangingEventArgs(this.ToggleState, value, false);
      this.OnToggleStateChanging(e1);
      CheckStateChangingEventArgs e2 = new CheckStateChangingEventArgs((CheckState) this.ToggleState, (CheckState) value, e1.Cancel);
      this.OnCheckStateChanging(e2);
      if (e2.Cancel)
        return;
      int num1 = (int) this.SetValue(RadToggleButtonElement.ToggleStateProperty, (object) value);
      RoutedEvent routedEvent = RadToggleButtonElement.Indeterminate;
      switch (this.ToggleState)
      {
        case Telerik.WinControls.Enumerations.ToggleState.Off:
          routedEvent = RadToggleButtonElement.Unchecked;
          break;
        case Telerik.WinControls.Enumerations.ToggleState.On:
          routedEvent = RadToggleButtonElement.CheckedRoutedEvent;
          break;
      }
      this.RaiseRoutedEvent((RadElement) this, new RoutedEventArgs(EventArgs.Empty, routedEvent));
      this.OnToggleStateChanged(new StateChangedEventArgs(this.ToggleState));
      this.OnCheckStateChanged(EventArgs.Empty);
      this.OnNotifyPropertyChanged("ToggleState");
      this.OnNotifyPropertyChanged("CheckState");
      this.OnNotifyPropertyChanged("IsChecked");
      foreach (RadObject radObject in this.ChildrenHierarchy)
      {
        int num2 = (int) radObject.SetValue(RadToggleButtonElement.ToggleStateProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the button is checked.")]
    [DefaultValue(false)]
    public bool IsChecked
    {
      get
      {
        return this.ToggleState != Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      set
      {
        if (value != this.IsChecked)
          this.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.OnIsCheckedChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Bindable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the toggle button has three or two states.")]
    public bool IsThreeState
    {
      get
      {
        return this.isTreeState;
      }
      set
      {
        this.isTreeState = value;
      }
    }

    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        this.readOnly = value;
      }
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      if (!this.Enabled)
        return VisualStyleElement.Button.PushButton.Disabled;
      if (this.IsMouseDown)
      {
        if (!this.IsMouseOver)
          return VisualStyleElement.Button.PushButton.Hot;
        return VisualStyleElement.Button.PushButton.Pressed;
      }
      if (this.IsMouseOver)
        return VisualStyleElement.Button.PushButton.Hot;
      Telerik.WinControls.Enumerations.ToggleState toggleState = this.ToggleState;
      if (toggleState != Telerik.WinControls.Enumerations.ToggleState.On && toggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return VisualStyleElement.Button.PushButton.Normal;
      return VisualStyleElement.Button.PushButton.Pressed;
    }
  }
}
