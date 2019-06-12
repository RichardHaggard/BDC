// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewClipboardEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridViewClipboardEventArgs : ClipboardEventArgs
  {
    private GridViewTemplate template;

    public GridViewClipboardEventArgs()
    {
    }

    public GridViewClipboardEventArgs(bool cancel)
      : base(cancel)
    {
    }

    public GridViewClipboardEventArgs(bool cancel, GridViewTemplate template)
      : base(cancel)
    {
      this.template = template;
    }

    public GridViewClipboardEventArgs(
      bool cancel,
      DataObject dataObject,
      GridViewTemplate template)
      : base(cancel, dataObject)
    {
      this.template = template;
    }

    public GridViewClipboardEventArgs(
      bool cancel,
      string format,
      DataObject dataObject,
      GridViewTemplate template)
      : base(cancel, format, dataObject)
    {
      this.template = template;
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }
  }
}
