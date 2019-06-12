// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridGroupRowBehavior : GridRowBehavior
  {
    protected GridViewGroupRowInfo GroupRow
    {
      get
      {
        return this.GridViewElement.CurrentRow as GridViewGroupRowInfo;
      }
    }

    protected override bool ProcessEnterKey(KeyEventArgs keys)
    {
      this.GroupRow.IsExpanded = !this.GroupRow.IsExpanded;
      return true;
    }

    protected override bool ProcessSpaceKey(KeyEventArgs keys)
    {
      this.GroupRow.IsExpanded = !this.GroupRow.IsExpanded;
      return true;
    }

    protected override bool ProcessEscapeKey(KeyEventArgs keys)
    {
      return false;
    }

    protected override bool ProcessF2Key(KeyEventArgs keys)
    {
      return false;
    }

    protected override bool ProcessAlphaNumericKey(KeyPressEventArgs keys)
    {
      return false;
    }

    protected override bool ProcessTabKey(KeyEventArgs keys)
    {
      if (this.GridViewElement.StandardTab || this.MasterTemplate.CurrentView.ViewTemplate.Columns.Count == 0)
        return false;
      if (keys.Control)
        return this.SelectNextControl(!keys.Shift);
      if (keys.Shift && this.IsOnFirstCell())
        return this.SelectNextControl(false);
      if (!keys.Shift && this.IsOnLastCell())
        return this.SelectNextControl(true);
      if (keys.Shift)
      {
        this.GridViewElement.Navigator.SelectLastColumn();
        this.GridViewElement.Navigator.SelectPreviousRow(1);
      }
      else
      {
        this.GridViewElement.Navigator.SelectFirstColumn();
        this.GridViewElement.Navigator.SelectNextRow(1);
      }
      return true;
    }

    protected override bool ProcessLeftKey(KeyEventArgs keys)
    {
      this.GridViewElement.Navigator.SelectLastColumn();
      this.GridViewElement.Navigator.SelectPreviousRow(1);
      return true;
    }

    protected override bool ProcessRightKey(KeyEventArgs keys)
    {
      this.GridViewElement.Navigator.SelectFirstColumn();
      this.GridViewElement.Navigator.SelectNextRow(1);
      return true;
    }
  }
}
