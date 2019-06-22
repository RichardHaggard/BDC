﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GroupsCollection : ObservableCollection<GridGroupByExpression>
  {
    private GridViewTemplate owner;

    public GroupsCollection(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public GridViewTemplate Owner
    {
      get
      {
        return this.owner;
      }
    }

    internal void OnCollectionChangedRaised(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnCollectionChanged(e);
    }
  }
}