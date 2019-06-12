// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewTableHeaderRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewTableHeaderRowInfo : GridViewSystemRowInfo
  {
    public GridViewTableHeaderRowInfo(GridViewInfo viewInfo)
      : base(viewInfo)
    {
      this.SuspendPropertyNotifications();
      this.PinPosition = PinnedRowPosition.Top;
      this.ViewInfo.PinnedRows.UpdateRow((GridViewRowInfo) this);
      this.ResumePropertyNotifications();
    }

    internal override object this[GridViewColumn column]
    {
      get
      {
        return (object) string.Empty;
      }
      set
      {
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.None;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridTableHeaderRowElement);
      }
    }

    protected override int CompareToSystemRowInfo(GridViewSystemRowInfo row)
    {
      return row is GridViewTableHeaderRowInfo ? 0 : -1;
    }
  }
}
