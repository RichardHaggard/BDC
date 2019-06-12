// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.OpenFileDialogOpeningEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class OpenFileDialogOpeningEventArgs : CancelEventArgs
  {
    private OpenFileDialog openFileDialog;

    public OpenFileDialogOpeningEventArgs(OpenFileDialog openFileDialog)
    {
      this.openFileDialog = openFileDialog;
    }

    public OpenFileDialog OpenFileDialog
    {
      get
      {
        return this.openFileDialog;
      }
    }
  }
}
