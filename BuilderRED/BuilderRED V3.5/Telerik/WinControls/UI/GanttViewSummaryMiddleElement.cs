// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewSummaryMiddleElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewSummaryMiddleElement : GanttViewVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
      this.Margin = new Padding(0, 6, 0, 0);
      this.MaxSize = new Size(0, 9);
      this.DrawFill = true;
      this.BackColor = Color.LightGreen;
      this.GradientStyle = GradientStyles.Solid;
    }
  }
}
