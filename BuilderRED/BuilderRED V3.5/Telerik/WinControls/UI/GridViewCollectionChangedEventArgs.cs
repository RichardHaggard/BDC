// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCollectionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewCollectionChangedEventArgs : NotifyCollectionChangedEventArgs
  {
    private GridViewTemplate gridViewTemplate;

    public GridViewCollectionChangedEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      object changedItem,
      int index,
      int oldIndex)
      : base(action, changedItem, index, oldIndex)
    {
      this.gridViewTemplate = template;
    }

    public GridViewCollectionChangedEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index,
      string propertyName)
      : base(action, newItem, oldItem, index, propertyName)
    {
      this.gridViewTemplate = template;
    }

    internal GridViewCollectionChangedEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedEventArgs args)
      : base(args.Action)
    {
      this.NewItems = args.NewItems;
      this.NewStartingIndex = args.NewStartingIndex;
      this.OldItems = args.OldItems;
      this.OldStartingIndex = args.OldStartingIndex;
      this.PropertyName = args.PropertyName;
      this.gridViewTemplate = template;
    }

    public GridViewCollectionChangedEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems,
      int newStartingIndex,
      int oldStartingIndex,
      string propertyName)
      : base(action)
    {
      this.Action = action;
      this.NewItems = newItems;
      this.OldItems = oldItems;
      this.NewStartingIndex = newStartingIndex;
      this.OldStartingIndex = oldStartingIndex;
      this.PropertyName = propertyName;
      this.gridViewTemplate = template;
    }

    public GridViewTemplate GridViewTemplate
    {
      get
      {
        return this.gridViewTemplate;
      }
    }
  }
}
