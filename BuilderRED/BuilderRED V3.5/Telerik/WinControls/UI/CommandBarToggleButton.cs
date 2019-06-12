// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarToggleButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class CommandBarToggleButton : RadCommandBarBaseItem
  {
    public static RadProperty ToggleStateProperty = RadProperty.Register(nameof (ToggleState), typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (CommandBarToggleButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off, ElementPropertyOptions.AffectsDisplay));
    private bool isTreeState;

    public CommandBarToggleButton()
    {
      this.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DefaultButton;
    }

    static CommandBarToggleButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new CommandBarToggleButton.CommandBarToggleButtonStateManagerFactory(), typeof (CommandBarToggleButton));
    }

    public event EventHandler IsCheckedChanged;

    public event StateChangingEventHandler ToggleStateChanging;

    public event StateChangedEventHandler ToggleStateChanged;

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs when the elements's check state changes.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler CheckStateChanged;

    [Bindable(true)]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(CheckState.Unchecked)]
    [Description("Gets or sets the Check state. Check state enumeration defines the following values: Uncheck, Indeterminate, and Check.")]
    public CheckState CheckState
    {
      get
      {
        switch (this.ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            return CheckState.Unchecked;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            return CheckState.Checked;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            return CheckState.Indeterminate;
          default:
            return CheckState.Indeterminate;
        }
      }
      set
      {
        if (this.CheckState == value)
          return;
        switch (value)
        {
          case CheckState.Unchecked:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            break;
          case CheckState.Checked:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            break;
          case CheckState.Indeterminate:
            this.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            break;
        }
        this.OnNotifyPropertyChanged(nameof (CheckState));
        this.OnCheckStateChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the toggle state. Toggle state enumeration defines the following values: Off, Indeterminate, and On.")]
    [Browsable(true)]
    [Bindable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("ToggleState", typeof (RadToggleButtonElement))]
    public Telerik.WinControls.Enumerations.ToggleState ToggleState
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(CommandBarToggleButton.ToggleStateProperty);
      }
      set
      {
        Telerik.WinControls.Enumerations.ToggleState toggleState = this.ToggleState;
        if (toggleState == value)
          return;
        StateChangingEventArgs e = new StateChangingEventArgs(toggleState, value, false);
        this.OnToggleStateChanging(e);
        if (e.Cancel)
          return;
        int num = (int) this.SetValue(CommandBarToggleButton.ToggleStateProperty, (object) value);
        this.OnToggleStateChanged(new StateChangedEventArgs(value));
        this.OnCheckStateChanged(EventArgs.Empty);
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

    protected override void OnOrientationChanged(EventArgs e)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        int num1 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) TextImageRelation.ImageAboveText);
      }
      else
      {
        int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) TextImageRelation.ImageBeforeText);
      }
      int num3 = (int) this.SetDefaultValueOverride(RadItem.TextOrientationProperty, (object) this.Orientation);
      this.InvalidateMeasure(true);
      base.OnOrientationChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanging(StateChangingEventArgs e)
    {
      if (e.NewValue == Telerik.WinControls.Enumerations.ToggleState.Indeterminate && !this.isTreeState)
        e.Cancel = true;
      if (this.ToggleStateChanging == null)
        return;
      this.ToggleStateChanging((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanged(StateChangedEventArgs e)
    {
      if (this.ToggleStateChanged == null)
        return;
      this.ToggleStateChanged((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCheckStateChanged(EventArgs e)
    {
      if (this.CheckStateChanged == null)
        return;
      this.CheckStateChanged((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnIsCheckedChanged(EventArgs e)
    {
      if (this.IsCheckedChanged == null)
        return;
      this.IsCheckedChanged((object) this, e);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
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
    }

    private class CommandBarToggleButtonStateManagerFactory : ItemStateManagerFactory
    {
      protected override StateNodeBase CreateSpecificStates()
      {
        StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Toggled", (Condition) new SimpleCondition(CommandBarToggleButton.ToggleStateProperty, (object) Telerik.WinControls.Enumerations.ToggleState.On));
        CompositeStateNode compositeStateNode = new CompositeStateNode("command bar toggle button states");
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
        return (StateNodeBase) compositeStateNode;
      }

      protected override void AddDefaultVisibleStates(ItemStateManager sm)
      {
        base.AddDefaultVisibleStates(sm);
        sm.AddDefaultVisibleState("Toggled");
      }

      protected override ItemStateManager CreateStateManagerCore()
      {
        return (ItemStateManager) new CommandBarToggleButton.CommandBarToggleButtonStateManager(this.RootState);
      }
    }

    private class CommandBarToggleButtonStateManager : ItemStateManager
    {
      public CommandBarToggleButtonStateManager(StateNodeBase rootState)
        : base(rootState)
      {
      }

      public override void ItemStateChanged(
        RadObject senderItem,
        RadPropertyChangedEventArgs changeArgs)
      {
        CommandBarToggleButton commandBarToggleButton = senderItem as CommandBarToggleButton;
        if (changeArgs != (RadPropertyChangedEventArgs) null && !commandBarToggleButton.Enabled && commandBarToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
          this.SetItemState(senderItem, "Disabled" + (object) '.' + "Toggled");
        else
          base.ItemStateChanged(senderItem, changeArgs);
      }
    }
  }
}
