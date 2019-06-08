// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewViewsSplitterElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewViewsSplitterElement : GanttViewVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.ElementTree.Control.Cursor = Cursors.VSplit;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }
  }
}
