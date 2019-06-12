// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterItemOwnerCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.DataFilterDescriptorItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  public class DataFilterItemOwnerCollection : RadItemOwnerCollection
  {
    public DataFilterItemOwnerCollection(RadElement owner)
      : base(owner)
    {
    }

    public DataFilterItemOwnerCollection()
    {
    }

    public DataFilterItemOwnerCollection(RadItemOwnerCollection value)
      : base(value)
    {
    }

    public DataFilterItemOwnerCollection(RadItem[] value)
      : base(value)
    {
    }

    public override RadItem this[string propertyName]
    {
      get
      {
        foreach (DataFilterDescriptorItem filterDescriptorItem in (RadItemCollection) this)
        {
          if (filterDescriptorItem.DescriptorName == propertyName)
            return (RadItem) filterDescriptorItem;
        }
        return (RadItem) null;
      }
    }
  }
}
