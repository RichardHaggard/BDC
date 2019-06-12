// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiColumnComboGridView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class MultiColumnComboGridView : RadGridView
  {
    public MultiColumnComboGridView()
    {
      this.GridBehavior = (IGridBehavior) new MultiColumnComboGridBehavior();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      MultiColumnComboPopupForm parent = this.Parent as MultiColumnComboPopupForm;
      if (parent != null && parent.closed)
        return;
      base.OnMouseDown(e);
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    public override string ThemeClassName
    {
      get
      {
        return "Telerik.WinControls.UI.RadGridView";
      }
      set
      {
        base.ThemeClassName = value;
      }
    }
  }
}
