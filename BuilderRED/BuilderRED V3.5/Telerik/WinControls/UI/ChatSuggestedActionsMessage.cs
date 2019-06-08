// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatSuggestedActionsMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ChatSuggestedActionsMessage : ChatMessage
  {
    private IEnumerable<SuggestedActionDataItem> suggestedActions;

    public ChatSuggestedActionsMessage(
      IEnumerable<SuggestedActionDataItem> suggestedActions,
      Author author,
      DateTime timeStamp)
      : this(suggestedActions, author, timeStamp, (object) null)
    {
    }

    public ChatSuggestedActionsMessage(
      IEnumerable<SuggestedActionDataItem> suggestedActions,
      Author author,
      DateTime timeStamp,
      object userData)
      : base(author, timeStamp, userData)
    {
      this.suggestedActions = suggestedActions;
    }

    public IEnumerable<SuggestedActionDataItem> SuggestedActions
    {
      get
      {
        return this.suggestedActions;
      }
      set
      {
        this.suggestedActions = value;
      }
    }
  }
}
