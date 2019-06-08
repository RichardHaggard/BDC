// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSortDescriptorCollectionFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSortDescriptorCollectionFactory : ISortDescriptorCollectionFactory
  {
    private GridViewTemplate owner;

    public GridViewSortDescriptorCollectionFactory(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public SortDescriptorCollection CreateCollection()
    {
      return (SortDescriptorCollection) new RadSortExpressionCollection(this.owner);
    }
  }
}
