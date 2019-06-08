// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IColorSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls
{
  public interface IColorSelector
  {
    Color SelectedColor { get; set; }

    HslColor SelectedHslColor { get; set; }

    Color OldColor { get; set; }

    bool ShowBasicColors { get; set; }

    ColorPickerActiveMode ActiveMode { get; set; }

    bool ShowSystemColors { get; set; }

    bool ShowWebColors { get; set; }

    bool ShowProfessionalColors { get; set; }

    bool ShowCustomColors { get; set; }

    bool ShowHEXColorValue { get; set; }

    bool AllowEditHEXValue { get; set; }

    bool AllowColorPickFromScreen { get; set; }

    bool AllowColorSaving { get; set; }

    Color[] CustomColors { get; }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the basic tab label.")]
    string BasicTabHeading { get; set; }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the system tab label.")]
    string SystemTabHeading { get; set; }

    [Description("Gets or sets the heading of the web tab label.")]
    [Localizable(true)]
    [Category("Behavior")]
    string WebTabHeading { get; set; }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the professional tab label.")]
    string ProfessionalTabHeading { get; set; }

    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the heading of the selected color label.")]
    string SelectedColorLabelHeading { get; set; }

    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the heading of the old color label.")]
    string OldColorLabelHeading { get; set; }

    event ColorChangedEventHandler OkButtonClicked;

    event ColorChangedEventHandler CancelButtonClicked;
  }
}
