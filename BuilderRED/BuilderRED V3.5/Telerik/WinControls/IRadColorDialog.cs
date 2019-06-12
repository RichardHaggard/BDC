// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IRadColorDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public interface IRadColorDialog
  {
    UserControl RadColorSelector { get; }

    Color SelectedColor { get; set; }

    HslColor SelectedHslColor { get; set; }

    Color OldColor { get; set; }

    ColorPickerActiveMode ActiveMode { get; set; }

    bool ShowBasicColors { get; set; }

    bool ShowSystemColors { get; set; }

    bool ShowWebColors { get; set; }

    bool ShowProfessionalColors { get; set; }

    bool ShowCustomColors { get; set; }

    bool ShowHEXColorValue { get; set; }

    bool AllowEditHEXValue { get; set; }

    bool AllowColorPickFromScreen { get; set; }

    bool AllowColorSaving { get; set; }

    Color[] CustomColors { get; }

    string BasicTabHeading { get; set; }

    string SystemTabHeading { get; set; }

    string WebTabHeading { get; set; }

    string ProfessionalTabHeading { get; set; }

    string SelectedColorLabelHeading { get; set; }

    string OldColorLabelHeading { get; set; }

    event ColorChangedEventHandler ColorChanged;
  }
}
