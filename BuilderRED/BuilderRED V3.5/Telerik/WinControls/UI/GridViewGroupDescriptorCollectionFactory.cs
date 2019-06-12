// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewGroupDescriptorCollectionFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewGroupDescriptorCollectionFactory : IGroupDescriptorCollectionFactory
  {
    private GridViewTemplate owner;

    public GridViewGroupDescriptorCollectionFactory(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public GroupDescriptorCollection CreateCollection()
    {
      return (GroupDescriptorCollection) new GridGroupByExpressionCollection(this.owner);
    }
  }
}
