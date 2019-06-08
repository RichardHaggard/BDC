// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewListSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewListSource : RadListSource<GridViewRowInfo>
  {
    private GridViewTemplate template;

    public GridViewListSource(GridViewTemplate template)
      : base((IDataItemSource) template)
    {
      this.Initialize(template);
    }

    public GridViewListSource(
      GridViewTemplate template,
      RadCollectionView<GridViewRowInfo> collectionView)
      : base((IDataItemSource) template, collectionView)
    {
      this.Initialize(template);
    }

    protected virtual void Initialize(GridViewTemplate template)
    {
      this.template = template;
      RadCollectionView<GridViewRowInfo> collectionView = this.CollectionView;
      collectionView.GroupFactory = (IGroupFactory<GridViewRowInfo>) new DataGroupFactory(template);
      collectionView.SortDescriptorCollectionFactory = (ISortDescriptorCollectionFactory) new GridViewSortDescriptorCollectionFactory(template);
      collectionView.Comparer = (IComparer<GridViewRowInfo>) new GridViewRowInfoComparer(collectionView.SortDescriptors);
      collectionView.GroupDescriptorCollectionFactory = (IGroupDescriptorCollectionFactory) new GridViewGroupDescriptorCollectionFactory(template);
      collectionView.CanFilter = false;
      collectionView.CanPage = false;
    }

    protected override RadCollectionView<GridViewRowInfo> CreateDefaultCollectionView()
    {
      return (RadCollectionView<GridViewRowInfo>) new GridDataView(this);
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }
  }
}
