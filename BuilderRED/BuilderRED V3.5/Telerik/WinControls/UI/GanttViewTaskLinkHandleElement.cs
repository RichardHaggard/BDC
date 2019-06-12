// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTaskLinkHandleElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewTaskLinkHandleElement : GanttViewVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.LightSalmon;
      this.GradientStyle = GradientStyles.Solid;
      this.NotifyParentOnMouseInput = true;
      this.Visibility = ElementVisibility.Hidden;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      RadGanttViewElement ancestor = this.FindAncestor<RadGanttViewElement>();
      if (ancestor != null && !ancestor.ReadOnly)
        this.Visibility = ElementVisibility.Visible;
      this.ElementTree.Control.Cursor = Cursors.Hand;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }
  }
}
