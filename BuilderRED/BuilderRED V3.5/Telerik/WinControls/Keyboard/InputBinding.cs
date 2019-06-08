// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.InputBinding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.Keyboard
{
  [Editor("Telerik.WinControls.UI.Design.InputBindingEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  public class InputBinding
  {
    internal static object instanceLock = new object();
    private ICommand command;
    private object commandContext;
    private Chord chord;

    public InputBinding()
    {
    }

    public InputBinding(ICommand command, Chord chord, object commandContext)
    {
      this.command = command;
      this.chord = chord;
      this.commandContext = commandContext;
    }

    [DefaultValue(null)]
    [TypeConverter(typeof (CommandInstanceConverter))]
    public ICommand Command
    {
      get
      {
        return this.command;
      }
      set
      {
        this.command = value;
      }
    }

    [DefaultValue(null)]
    [TypeConverter(typeof (CommandContextConverter))]
    public object CommandContext
    {
      get
      {
        return this.commandContext;
      }
      set
      {
        this.commandContext = value;
      }
    }

    public virtual Chord Chord
    {
      get
      {
        return this.chord;
      }
      set
      {
        this.chord = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsEmpty
    {
      get
      {
        if (this.Chord != null && this.CommandContext != null)
          return this.Command == null;
        return true;
      }
    }

    public void Clear()
    {
      this.Chord = (Chord) null;
      this.CommandContext = (object) null;
      this.Command = (ICommand) null;
    }
  }
}
