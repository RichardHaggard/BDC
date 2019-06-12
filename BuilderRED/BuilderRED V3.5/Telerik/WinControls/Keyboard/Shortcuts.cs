// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.Shortcuts
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.Keyboard
{
  [ProvideProperty("CommandBinding", typeof (IComponent))]
  [ToolboxItem(false)]
  [ToolboxItemFilter("System.Windows.Forms")]
  [Browsable(false)]
  [DefaultEvent("Activate")]
  [Designer("Telerik.WinControls.UI.Design.ShortcutsDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Editor("Telerik.WinControls.UI.Design.InputBindingEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Description("Chords Provider")]
  [ToolboxBitmap(typeof (ResFinder), "Resources.Shortcuts.bmp")]
  public class Shortcuts : Component, IExtenderProvider
  {
    private InputBindingsCollection inputBindings = new InputBindingsCollection();
    private ChordMessageFilter chordMessageFilter;
    private Control owner;
    private IContainer components;

    public Shortcuts()
    {
      this.InitializeComponent();
    }

    public Shortcuts(Control owner)
    {
      this.owner = owner;
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this.AddShortcutsSupport();
    }

    public Shortcuts(IContainer container)
    {
      container.Add((IComponent) this);
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this.AddShortcutsSupport();
    }

    public event ChordsEventHandler Activate;

    protected virtual void OnActivate(ChordEventArgs e)
    {
      ChordsEventHandler activate = this.Activate;
      if (activate == null)
        return;
      activate((object) this, e);
    }

    public Control Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    internal Form OwnerForm
    {
      get
      {
        if (this.owner != null)
          return this.owner.FindForm();
        return (Form) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputBindingsCollection InputBindings
    {
      get
      {
        return this.inputBindings;
      }
    }

    public void AddCommandBindings(InputBinding binding)
    {
      this.inputBindings.Add(binding);
    }

    public void AddCommandBindings(List<InputBinding> bindings)
    {
      this.inputBindings.AddRange(bindings.ToArray());
    }

    public void AddCommandBindings(InputBindingsCollection bindings)
    {
      this.inputBindings.AddRange(bindings);
    }

    public virtual void AddShortcutsSupport()
    {
      this.chordMessageFilter = ChordMessageFilter.CreateInstance();
      ChordMessageFilter.RegisterChordsConsumer(this);
    }

    internal virtual void RemoveShortcutsSupport()
    {
      if (this.chordMessageFilter == null)
        return;
      ChordMessageFilter.UnregisterChordsConsumer(this);
      this.chordMessageFilter = (ChordMessageFilter) null;
    }

    public bool CanExtend(object extendee)
    {
      return typeof (RadControl).IsAssignableFrom(extendee.GetType()) || typeof (RadItem).IsAssignableFrom(extendee.GetType());
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [DefaultValue(null)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public InputBinding GetCommandBinding(IComponent component)
    {
      InputBindingsCollection bindingByComponent = this.inputBindings.GetBindingByComponent(component);
      if (bindingByComponent != null && bindingByComponent != null)
      {
        int count = bindingByComponent.Count;
      }
      if (bindingByComponent != null && bindingByComponent.Count > 0)
        return bindingByComponent[0];
      return (InputBinding) null;
    }

    public void SetCommandBinding(IComponent component, InputBinding value)
    {
      if (value == null || value != null && value.IsEmpty)
        this.inputBindings.RemoveBindingByComponent(component);
      else
        this.inputBindings.Add(value);
    }

    private bool ShouldSerializeCommandBinding(InputBinding value)
    {
      return !value.IsEmpty;
    }

    public void ResetCommandBinding(InputBinding value)
    {
      value.Clear();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
      {
        this.components.Dispose();
        this.owner = (Control) null;
      }
      this.RemoveShortcutsSupport();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
    }
  }
}
