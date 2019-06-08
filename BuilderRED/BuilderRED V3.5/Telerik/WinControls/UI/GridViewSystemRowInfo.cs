// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSystemRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewSystemRowInfo : GridViewRowInfo, IComparable<GridViewSystemRowInfo>
  {
    private SystemRowPosition rowPosition;

    public GridViewSystemRowInfo(GridViewInfo viewInfo)
      : base(viewInfo)
    {
      this.SuspendPropertyNotifications();
      this.AllowResize = false;
      this.IsAttached = false;
      this.ResumePropertyNotifications();
    }

    [Browsable(false)]
    public override bool IsSystem
    {
      get
      {
        return true;
      }
    }

    public override int Index
    {
      get
      {
        return -1;
      }
    }

    public SystemRowPosition RowPosition
    {
      get
      {
        return this.rowPosition;
      }
      set
      {
        if (this.rowPosition == value)
          return;
        PropertyChangingEventArgsEx args = new PropertyChangingEventArgsEx(nameof (RowPosition), (object) this.rowPosition, (object) value);
        this.OnPropertyChanging(args);
        if (args.Cancel)
          return;
        SystemRowPosition rowPosition = this.rowPosition;
        this.rowPosition = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (RowPosition), (object) rowPosition, (object) value));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IHierarchicalRow Parent
    {
      get
      {
        if (this.ViewInfo != null)
          return (IHierarchicalRow) this.ViewInfo.ParentRow;
        return base.Parent;
      }
    }

    int IComparable<GridViewSystemRowInfo>.CompareTo(
      GridViewSystemRowInfo row)
    {
      return this.CompareToSystemRowInfo(row);
    }

    protected virtual int CompareToSystemRowInfo(GridViewSystemRowInfo row)
    {
      return 1;
    }
  }
}
