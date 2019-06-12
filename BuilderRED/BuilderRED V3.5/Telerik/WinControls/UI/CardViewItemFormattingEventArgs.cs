// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewItemFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CardViewItemFormattingEventArgs : EventArgs
  {
    private LayoutControlItemBase item;
    private CardListViewVisualItem visualItem;

    public CardViewItemFormattingEventArgs(
      LayoutControlItemBase item,
      CardListViewVisualItem visualItem)
    {
      this.item = item;
      this.visualItem = visualItem;
    }

    public LayoutControlItemBase Item
    {
      get
      {
        return this.item;
      }
    }

    public CardListViewVisualItem VisualItem
    {
      get
      {
        return this.visualItem;
      }
    }
  }
}
