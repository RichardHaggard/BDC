// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeSeparatorEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TimeSeparatorEventArgs : EventArgs
  {
    private bool shouldAddSeparator;
    private BaseChatDataItem previousItem;
    private BaseChatDataItem item;

    public TimeSeparatorEventArgs(
      bool shouldAddSeparator,
      BaseChatDataItem previousItem,
      BaseChatDataItem item)
    {
      this.shouldAddSeparator = shouldAddSeparator;
      this.previousItem = previousItem;
      this.item = item;
    }

    public bool ShouldAddSeparator
    {
      get
      {
        return this.shouldAddSeparator;
      }
      set
      {
        this.shouldAddSeparator = value;
      }
    }

    public BaseChatDataItem PreviousItem
    {
      get
      {
        return this.previousItem;
      }
    }

    public BaseChatDataItem Item
    {
      get
      {
        return this.item;
      }
    }
  }
}
