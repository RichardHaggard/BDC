// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PopupCloseInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class PopupCloseInfo
  {
    private object context;
    private RadPopupCloseReason closeReason;
    private bool closed;

    public PopupCloseInfo(RadPopupCloseReason closeReason, object context)
    {
      this.closeReason = closeReason;
      this.context = context;
      this.closed = true;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Closed
    {
      get
      {
        return this.closed;
      }
      internal set
      {
        this.closed = value;
      }
    }

    public RadPopupCloseReason CloseReason
    {
      get
      {
        return this.closeReason;
      }
    }

    public object Context
    {
      get
      {
        return this.context;
      }
    }
  }
}
