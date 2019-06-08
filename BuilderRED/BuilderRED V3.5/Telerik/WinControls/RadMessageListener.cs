// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadMessageListener
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadMessageListener : IMessageListener
  {
    public virtual InstalledHook DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage;
      }
    }

    public virtual MessagePreviewResult PreviewMessage(ref Message msg)
    {
      return MessagePreviewResult.NotProcessed;
    }

    public virtual void PreviewWndProc(Message msg)
    {
    }

    public virtual void PreviewSystemMessage(SystemMessage message, Message msg)
    {
    }
  }
}
