// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.DataItemGroup`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public class DataItemGroup<TDataItem> : Group<TDataItem> where TDataItem : IDataItem
  {
    private int version = -1;
    private const int ITEMS_CAPACITY = 4;
    private List<TDataItem> items;
    private Telerik.WinControls.Data.GroupBuilder<TDataItem> groupBuilder;
    private GroupCollection<TDataItem> groups;

    public DataItemGroup(object key)
      : this(key, (Group<TDataItem>) null)
    {
    }

    public DataItemGroup(object key, Group<TDataItem> parent)
      : base(key, parent)
    {
      this.groups = (GroupCollection<TDataItem>) null;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupCollection<TDataItem> Groups
    {
      get
      {
        if (this.groupBuilder == null)
          return GroupCollection<TDataItem>.Empty;
        if (this.groupBuilder.Version != this.version)
        {
          this.groups = this.groupBuilder.Perform((IReadOnlyCollection<TDataItem>) this, this.Level + 1, (Group<TDataItem>) this);
          this.version = this.groupBuilder.Version;
        }
        return this.groups;
      }
    }

    protected internal GroupCollection<TDataItem> Cache
    {
      get
      {
        return this.groups;
      }
    }

    protected internal override IList<TDataItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<TDataItem>(4);
        return (IList<TDataItem>) this.items;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public Telerik.WinControls.Data.GroupBuilder<TDataItem> GroupBuilder
    {
      get
      {
        return this.groupBuilder;
      }
      set
      {
        this.groupBuilder = value;
      }
    }
  }
}
