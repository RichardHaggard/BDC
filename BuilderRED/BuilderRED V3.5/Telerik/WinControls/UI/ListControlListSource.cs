// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListControlListSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ListControlListSource : RadListSource<RadListDataItem>
  {
    private ListDataLayer dataLayer;
    private bool resetting;

    public ListControlListSource(IDataItemSource owner)
      : base(owner)
    {
      this.dataLayer = (ListDataLayer) owner;
    }

    public bool Resetting
    {
      get
      {
        return this.resetting;
      }
      private set
      {
        this.resetting = value;
      }
    }

    protected override void InitializeBoundRow(RadListDataItem item, object dataBoundItem)
    {
      item.SetDataBoundItem(true, dataBoundItem);
      if (!this.Resetting || !item.Selected)
        return;
      ((Collection<RadListDataItem>) this.dataLayer.Owner.SelectedItems).Add(item);
    }

    public override void Reset()
    {
      this.Resetting = true;
      base.Reset();
      this.Resetting = false;
    }
  }
}
