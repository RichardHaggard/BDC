// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.IGroupFactory`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public interface IGroupFactory<T> where T : IDataItem
  {
    Group<T> CreateGroup(object key, Group<T> parent, params object[] metaData);

    GroupCollection<T> CreateCollection(IList<Group<T>> list);
  }
}
