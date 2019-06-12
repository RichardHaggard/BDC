// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewGroupColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewGroupColumn : GridViewColumn
  {
    private GridViewColumnGroup group;

    static GridViewGroupColumn()
    {
      GridViewColumn.AllowSortProperty.OverrideMetadata(typeof (GridViewGroupColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    }

    public GridViewGroupColumn(GridViewColumnGroup group)
    {
      this.group = group;
      this.HeaderText = group.Text;
      this.PinPosition = this.group.PinPosition;
      this.AllowResize = true;
      this.group.PropertyChanged += new PropertyChangedEventHandler(this.group_PropertyChanged);
    }

    private void group_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "PinPosition") || this.IsDisposed)
        return;
      this.SuspendPropertyNotifications();
      int num = (int) this.SetValue(GridViewColumn.PinPositionProperty, (object) this.group.PinPosition);
      this.ResumePropertyNotifications();
    }

    public GridViewColumnGroup Group
    {
      get
      {
        return this.group;
      }
    }

    public GridViewColumnGroup RootColumnGroup
    {
      get
      {
        if (this.group != null)
          return this.group.RootColumnGroup;
        return (GridViewColumnGroup) null;
      }
    }

    public override bool IsVisible
    {
      get
      {
        if (this.group != null)
          return this.group.IsVisible;
        return false;
      }
      set
      {
        if (this.group == null)
          return;
        this.group.IsVisible = value;
      }
    }

    public override bool VisibleInColumnChooser
    {
      get
      {
        if (this.group != null)
          return this.group.VisibleInColumnChooser;
        return false;
      }
      set
      {
        if (this.group == null)
          return;
        this.group.VisibleInColumnChooser = value;
      }
    }

    internal override bool CanGroup
    {
      get
      {
        return false;
      }
    }

    public override bool AllowReorder
    {
      get
      {
        if (this.Group != null)
          return this.Group.AllowReorder;
        return false;
      }
      set
      {
        if (this.Group == null)
          return;
        this.Group.AllowReorder = value;
      }
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewTableHeaderRowInfo)
        return typeof (GridColumnGroupCellElement);
      return (Type) null;
    }

    protected override void DisposeManagedResources()
    {
      this.group.PropertyChanged -= new PropertyChangedEventHandler(this.group_PropertyChanged);
      this.group = (GridViewColumnGroup) null;
      base.DisposeManagedResources();
    }
  }
}
