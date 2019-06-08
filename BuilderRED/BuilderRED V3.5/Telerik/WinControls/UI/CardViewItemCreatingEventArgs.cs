// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewItemCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CardViewItemCreatingEventArgs : EventArgs
  {
    private LayoutControlItemBase sourceItem;
    private LayoutControlItemBase newItem;
    private CardListViewVisualItem visualItem;

    public CardViewItemCreatingEventArgs(
      LayoutControlItemBase sourceItem,
      LayoutControlItemBase newItem,
      CardListViewVisualItem visualItem)
    {
      this.sourceItem = sourceItem;
      this.newItem = newItem;
      this.visualItem = visualItem;
    }

    public LayoutControlItemBase SourceItem
    {
      get
      {
        return this.sourceItem;
      }
    }

    public LayoutControlItemBase NewItem
    {
      get
      {
        return this.newItem;
      }
      set
      {
        this.newItem = value;
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
