// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridClipboardEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualGridClipboardEventArgs : ClipboardEventArgs
  {
    private VirtualGridViewInfo viewInfo;

    public VirtualGridClipboardEventArgs()
    {
    }

    public VirtualGridClipboardEventArgs(bool cancel)
      : base(cancel)
    {
    }

    public VirtualGridClipboardEventArgs(bool cancel, VirtualGridViewInfo viewInfo)
      : base(cancel)
    {
      this.viewInfo = viewInfo;
    }

    public VirtualGridClipboardEventArgs(
      bool cancel,
      DataObject dataObject,
      VirtualGridViewInfo viewInfo)
      : base(cancel, dataObject)
    {
      this.viewInfo = viewInfo;
    }

    public VirtualGridClipboardEventArgs(
      bool cancel,
      string format,
      DataObject dataObject,
      VirtualGridViewInfo viewInfo)
      : base(cancel, format, dataObject)
    {
      this.viewInfo = viewInfo;
    }

    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }
  }
}
