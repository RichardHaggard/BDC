// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ClipboardEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ClipboardEventArgs : CancelEventArgs
  {
    private DataObject dataObject;
    private string format;

    public ClipboardEventArgs()
    {
    }

    public ClipboardEventArgs(bool cancel)
      : base(cancel)
    {
    }

    public ClipboardEventArgs(bool cancel, DataObject dataObject)
      : this(cancel)
    {
      this.dataObject = dataObject;
    }

    public ClipboardEventArgs(bool cancel, string format, DataObject dataObject)
      : this(cancel, dataObject)
    {
      this.format = format;
    }

    public DataObject DataObject
    {
      get
      {
        return this.dataObject;
      }
    }

    public string Format
    {
      get
      {
        return this.format;
      }
    }
  }
}
