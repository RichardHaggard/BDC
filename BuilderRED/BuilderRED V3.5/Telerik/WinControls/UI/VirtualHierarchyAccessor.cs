// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualHierarchyAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class VirtualHierarchyAccessor : Accessor
  {
    public VirtualHierarchyAccessor(GridViewColumn column)
      : base(column)
    {
    }

    public override object this[GridViewRowInfo row]
    {
      get
      {
        return row.Cache[this.Column];
      }
      set
      {
        row.Cache[this.Column] = value;
        row.ViewTemplate.RefreshAggregates(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) row, row.Index));
      }
    }
  }
}
