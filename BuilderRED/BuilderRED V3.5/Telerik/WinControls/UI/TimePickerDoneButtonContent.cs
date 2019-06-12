// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimePickerDoneButtonContent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class TimePickerDoneButtonContent : RadControl
  {
    private TimePickerDoneButtonElement timePickerDoneButtonElement;

    public TimePickerDoneButtonContent(RadTimePickerContentElement owner)
    {
      this.ThemeClassName = typeof (RadTimePickerContent).FullName;
      this.timePickerDoneButtonElement.Owner = owner;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.timePickerDoneButtonElement = this.CreateTimePickerDoneButtonElement();
      parent.Children.Add((RadElement) this.timePickerDoneButtonElement);
    }

    protected virtual TimePickerDoneButtonElement CreateTimePickerDoneButtonElement()
    {
      return new TimePickerDoneButtonElement((RadTimePickerContentElement) null);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(50, 24));
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.ElementTree.ApplyThemeToElement((RadObject) this.timePickerDoneButtonElement.Owner, true);
      this.timePickerDoneButtonElement.Padding = new Padding(2);
      this.timePickerDoneButtonElement.BackColor = this.timePickerDoneButtonElement.Owner.HoursTable.BackColor;
      this.timePickerDoneButtonElement.DrawBorder = true;
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.timePickerDoneButtonElement.BackColor = this.timePickerDoneButtonElement.Owner.HoursTable.BackColor;
      this.timePickerDoneButtonElement.GradientStyle = GradientStyles.Solid;
      this.timePickerDoneButtonElement.DrawFill = true;
      this.timePickerDoneButtonElement.ButtonElement.Margin = new Padding(0);
      this.timePickerDoneButtonElement.ButtonElement.Padding = new Padding(0);
      if (this.timePickerDoneButtonElement.BorderBoxStyle == BorderBoxStyle.FourBorders)
      {
        this.timePickerDoneButtonElement.BorderLeftWidth = this.timePickerDoneButtonElement.BorderTopWidth;
        this.timePickerDoneButtonElement.BorderRightWidth = this.timePickerDoneButtonElement.BorderTopWidth;
        this.timePickerDoneButtonElement.BorderLeftColor = this.timePickerDoneButtonElement.BorderTopColor;
        this.timePickerDoneButtonElement.BorderRightColor = this.timePickerDoneButtonElement.BorderTopColor;
        this.timePickerDoneButtonElement.BorderBottomWidth = 0.0f;
        if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
          return;
        this.timePickerDoneButtonElement.BorderLeftWidth = 1f;
        this.timePickerDoneButtonElement.BorderLeftColor = Color.FromArgb(236, 236, 236);
      }
      else
      {
        this.timePickerDoneButtonElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
        this.timePickerDoneButtonElement.BorderBottomWidth = 0.0f;
      }
    }
  }
}
