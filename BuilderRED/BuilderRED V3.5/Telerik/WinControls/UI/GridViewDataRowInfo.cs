// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDataRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewDataRowInfo : GridViewRowInfo
  {
    public GridViewDataRowInfo(GridViewInfo viewInfo)
      : base(viewInfo)
    {
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.PropertyName == "PinPosition"))
        return;
      GridViewGroupRowInfo parent = this.Parent as GridViewGroupRowInfo;
      if (parent == null)
        return;
      bool flag = true;
      foreach (GridViewRowInfo childRow in parent.ChildRows)
      {
        if (childRow.PinPosition == PinnedRowPosition.None)
        {
          flag = false;
          break;
        }
      }
      this.IsVisible = !flag;
    }

    protected override bool OnBeginEdit()
    {
      IEditableObject dataBoundItem = this.DataBoundItem as IEditableObject;
      if (dataBoundItem != null)
      {
        try
        {
          this.ViewTemplate.ListSource.BeginUpdate();
          dataBoundItem.BeginEdit();
        }
        catch (Exception ex)
        {
          this.ViewTemplate.SetError(new GridViewCellCancelEventArgs((GridViewRowInfo) this, (GridViewColumn) null, (IInputEditor) null), ex);
        }
        finally
        {
          this.ViewTemplate.ListSource.EndUpdate(false);
        }
      }
      return base.OnBeginEdit();
    }
  }
}
