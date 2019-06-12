// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEventDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridViewEventDataProvider : GridViewHierarchyDataProvider
  {
    public GridViewEventDataProvider(GridViewTemplate template)
      : base(template)
    {
      if (template.ListSource.IsDataBound)
        throw new InvalidOperationException("GridViewEventDataProvider can not be applied to bound GridViewTemplate");
    }

    public override IList<GridViewRowInfo> GetChildRows(
      GridViewRowInfo parentRow,
      GridViewInfo view)
    {
      IList<GridViewRowInfo> gridViewRowInfoList;
      IList<GridViewRowInfo> sourceCollection = gridViewRowInfoList = (IList<GridViewRowInfo>) new List<GridViewRowInfo>(64);
      GridViewRowSourceNeededEventArgs args = new GridViewRowSourceNeededEventArgs(parentRow, this.Template, sourceCollection);
      this.Template.EventDispatcher.RaiseEvent<GridViewRowSourceNeededEventArgs>(EventDispatcher.RowSourceNeeded, (object) this, args);
      return sourceCollection;
    }

    public override bool IsVirtual
    {
      get
      {
        return true;
      }
    }

    public override void Refresh()
    {
      this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.DataChanged));
    }

    public override GridViewHierarchyRowInfo GetParent(
      GridViewRowInfo gridViewRowInfo)
    {
      return (GridViewHierarchyRowInfo) null;
    }

    private void DispatchDataViewChangedEvent(DataViewChangedEventArgs args)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.Template, new GridViewEvent((object) this.Template, (object) this.Template, new object[1]
      {
        (object) args
      }, eventInfo), false);
    }
  }
}
