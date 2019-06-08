// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineCellElement : GanttViewVisualElement
  {
    private GanttViewGraphicalViewElement element;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
      this.StretchHorizontally = true;
    }

    public GanttViewTimelineCellElement(GanttViewGraphicalViewElement element)
    {
      this.element = element;
    }
  }
}
