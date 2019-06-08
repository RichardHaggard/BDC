// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardActionEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CardActionEventArgs : EventArgs
  {
    private ChatCardAction action;
    private object userData;

    public CardActionEventArgs(ChatCardAction action, object userData)
    {
      this.action = action;
      this.userData = userData;
    }

    public ChatCardAction Action
    {
      get
      {
        return this.action;
      }
      set
      {
        this.action = value;
      }
    }

    public object UserData
    {
      get
      {
        return this.userData;
      }
      set
      {
        this.userData = value;
      }
    }
  }
}
