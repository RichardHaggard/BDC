// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ColorChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ColorChangedEventArgs : EventArgs
  {
    private Color selectedColor;
    private HslColor selectedHslColor;

    public ColorChangedEventArgs(HslColor selectedHslColor)
    {
      this.selectedColor = selectedHslColor.RgbValue;
      this.selectedHslColor = selectedHslColor;
    }

    public ColorChangedEventArgs(Color selectedColor)
    {
      this.selectedColor = selectedColor;
      this.selectedHslColor = HslColor.FromColor(selectedColor);
    }

    public Color SelectedColor
    {
      get
      {
        return this.selectedColor;
      }
    }

    public HslColor SelectedHslColor
    {
      get
      {
        return this.selectedHslColor;
      }
    }
  }
}
