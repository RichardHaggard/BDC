// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.Keyboard
{
  public class ChordEventArgs : CancelEventArgs
  {
    private Control associatedControl;
    private RadItem associatedItem;

    public ChordEventArgs(Control associatedControl, RadItem associatedItem)
    {
      this.associatedItem = associatedItem;
      this.associatedControl = associatedControl;
    }

    public Control AssociatedControl
    {
      get
      {
        return this.associatedControl;
      }
    }

    public RadItem AssociatedItem
    {
      get
      {
        return this.associatedItem;
      }
    }
  }
}
