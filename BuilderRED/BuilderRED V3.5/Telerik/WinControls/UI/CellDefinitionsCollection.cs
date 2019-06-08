// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CellDefinitionsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class CellDefinitionsCollection : ObservableCollection<CellDefinition>
  {
    private RowDefinition owner;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RowDefinition Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      base.OnCollectionChanged(e);
      if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.ItemChanged)
      {
        foreach (CellDefinition newItem in (IEnumerable) e.NewItems)
          newItem.Row = this.owner;
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove)
          return;
        foreach (CellDefinition newItem in (IEnumerable) e.NewItems)
          newItem.Row = (RowDefinition) null;
      }
    }
  }
}
