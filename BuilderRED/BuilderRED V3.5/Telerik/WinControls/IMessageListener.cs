// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IMessageListener
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public interface IMessageListener
  {
    InstalledHook DesiredHook { get; }

    MessagePreviewResult PreviewMessage(ref Message msg);

    void PreviewWndProc(Message msg);

    void PreviewSystemMessage(SystemMessage message, Message msg);
  }
}
