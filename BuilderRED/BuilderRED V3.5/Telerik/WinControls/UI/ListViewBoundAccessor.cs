// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewBoundAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ListViewBoundAccessor : ListViewAccessor
  {
    public ListViewBoundAccessor(ListViewDetailColumn column)
      : base(column)
    {
    }

    public override object this[ListViewDataItem item]
    {
      get
      {
        if (item.DataBoundItem == null)
          return (object) null;
        if (string.IsNullOrEmpty(this.Column.FieldName))
          return (object) null;
        return this.Owner.ListSource.GetBoundValue(item.DataBoundItem, this.Column.FieldName);
      }
      set
      {
        if (item.DataBoundItem == null)
          return;
        this.Owner.ListSource.SetBoundValue((IDataItem) item, this.Column.FieldName, this.Column.Name, value, (string) null);
      }
    }
  }
}
