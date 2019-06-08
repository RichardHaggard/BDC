// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuCalendarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class FilterMenuCalendarElement : LightVisualElement
  {
    private RadHostItem hostItem;
    private RadCalendar calendar;

    public RadCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BorderGradientStyle = GradientStyles.Solid;
      this.BorderColor = Color.FromArgb(156, 189, 232);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.calendar = new RadCalendar();
      this.calendar.AllowMultipleSelect = true;
      this.hostItem = new RadHostItem((Control) this.calendar);
      this.Children.Add((RadElement) this.hostItem);
    }
  }
}
