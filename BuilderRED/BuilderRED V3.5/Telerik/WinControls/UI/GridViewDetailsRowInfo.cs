// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDetailsRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewDetailsRowInfo : GridViewRowInfo
  {
    private int actualHeight = -1;
    private GridViewHierarchyRowInfo ownerRowInfo;
    private bool isLastRow;
    internal bool resetActualHeight;

    public GridViewDetailsRowInfo(GridViewHierarchyRowInfo ownerRowInfo)
      : base(ownerRowInfo.ViewInfo)
    {
      this.ownerRowInfo = ownerRowInfo;
      this.SuspendPropertyNotifications();
      this.MaxHeight = 500;
      this.ResumePropertyNotifications();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewInfo ChildViewInfo
    {
      get
      {
        return this.ownerRowInfo.ActiveView;
      }
      internal set
      {
        this.ownerRowInfo.ActiveView = value;
      }
    }

    public IReadOnlyCollection<GridViewInfo> ChildViewInfos
    {
      get
      {
        return this.ownerRowInfo.Views;
      }
    }

    public GridViewDataRowInfo Owner
    {
      get
      {
        return (GridViewDataRowInfo) this.ownerRowInfo;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridDetailViewRowElement);
      }
    }

    public override GridViewInfo ViewInfo
    {
      get
      {
        return this.ownerRowInfo.ViewInfo;
      }
    }

    public override IHierarchicalRow Parent
    {
      get
      {
        return this.Owner.Parent;
      }
    }

    public int ActualHeight
    {
      get
      {
        return this.actualHeight;
      }
      internal set
      {
        this.actualHeight = value;
      }
    }

    public bool IsLastRow
    {
      get
      {
        return this.isLastRow;
      }
      internal set
      {
        this.isLastRow = value;
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.None;
      }
    }
  }
}
