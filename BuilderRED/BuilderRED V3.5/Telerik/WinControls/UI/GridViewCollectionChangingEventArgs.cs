// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCollectionChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewCollectionChangingEventArgs : NotifyCollectionChangingEventArgs
  {
    private GridViewTemplate gridViewTemplate;

    public GridViewCollectionChangingEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      object changedItem,
      int index,
      int oldIndex)
      : base(action, changedItem, index, oldIndex)
    {
      this.gridViewTemplate = template;
    }

    public GridViewCollectionChangingEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index,
      PropertyChangingEventArgsEx propertyArgs)
      : base(action, newItem, oldItem, index, propertyArgs)
    {
      this.gridViewTemplate = template;
      this.PropertyArgs = propertyArgs;
    }

    public GridViewCollectionChangingEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems,
      int newStartingIndex,
      int oldStartingIndex,
      PropertyChangingEventArgsEx propertyArgs)
      : base(action)
    {
      this.Action = action;
      this.NewItems = newItems;
      this.OldItems = oldItems;
      this.NewStartingIndex = newStartingIndex;
      this.OldStartingIndex = oldStartingIndex;
      this.PropertyArgs = propertyArgs;
      this.gridViewTemplate = template;
    }

    internal GridViewCollectionChangingEventArgs(
      GridViewTemplate template,
      NotifyCollectionChangingEventArgs args)
      : this(template, args.Action, args.NewItems, args.OldItems, args.NewStartingIndex, args.OldStartingIndex, args.PropertyArgs)
    {
    }

    public GridViewTemplate GridViewTemplate
    {
      get
      {
        return this.gridViewTemplate;
      }
    }

    public object NewValue
    {
      get
      {
        if (this.PropertyArgs == null)
          return (object) null;
        return this.PropertyArgs.NewValue;
      }
    }

    public object OldValue
    {
      get
      {
        if (this.PropertyArgs == null)
          return (object) null;
        return this.PropertyArgs.OldValue;
      }
    }

    public string PropertyName
    {
      get
      {
        if (this.PropertyArgs == null)
          return (string) null;
        return this.PropertyArgs.PropertyName;
      }
    }
  }
}
