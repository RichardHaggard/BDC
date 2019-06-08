// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHostItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class GridViewHostItem : RadHostItem
  {
    public GridViewHostItem()
      : base((Control) new MultiColumnComboGridView())
    {
    }

    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        if (value)
          this.HostedGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        else
          this.HostedGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
      }
    }

    public MultiColumnComboGridView HostedGridView
    {
      get
      {
        return this.HostedControl as MultiColumnComboGridView;
      }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.HostedGridView.CallOnMouseWheel(e);
    }
  }
}
