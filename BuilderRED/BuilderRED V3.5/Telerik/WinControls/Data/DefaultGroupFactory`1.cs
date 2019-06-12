// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.DefaultGroupFactory`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public class DefaultGroupFactory<TDataItem> : IGroupFactory<TDataItem> where TDataItem : IDataItem
  {
    public Group<TDataItem> CreateGroup(
      object key,
      Group<TDataItem> parent,
      params object[] metaData)
    {
      return (Group<TDataItem>) new DataItemGroup<TDataItem>(key, parent);
    }

    public GroupCollection<TDataItem> CreateCollection(IList<Group<TDataItem>> list)
    {
      return new GroupCollection<TDataItem>(list);
    }
  }
}
