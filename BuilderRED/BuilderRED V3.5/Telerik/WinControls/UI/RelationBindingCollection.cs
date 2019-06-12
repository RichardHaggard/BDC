// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RelationBindingCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class RelationBindingCollection : NotifyCollection<RelationBinding>
  {
    public void Add(object dataSource, string displayMember, string parentChildMember)
    {
      this.Add(new RelationBinding(dataSource, displayMember, parentChildMember));
    }

    public void Add(
      object dataSource,
      string displayMember,
      string parentMember,
      string childMember)
    {
      this.Add(new RelationBinding(dataSource, displayMember, parentMember, childMember));
    }

    public void Add(
      object dataSource,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember)
    {
      this.Add(new RelationBinding(dataSource, displayMember, parentMember, childMember, valueMember));
    }

    public void Add(
      object dataSource,
      string dataMember,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember)
    {
      this.Add(new RelationBinding(dataSource, dataMember, displayMember, parentMember, childMember, valueMember));
    }

    public void Add(
      object dataSource,
      string dataMember,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember,
      string checkedMember)
    {
      this.Add(new RelationBinding(dataSource, dataMember, displayMember, parentMember, childMember, valueMember, checkedMember));
    }

    protected override void InsertItem(int index, RelationBinding item)
    {
      base.InsertItem(index, item);
    }
  }
}
