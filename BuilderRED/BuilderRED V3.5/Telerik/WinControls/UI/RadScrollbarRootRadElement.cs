// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollbarRootRadElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadScrollbarRootRadElement : RootRadElement
  {
    protected override void OnControlDefaultSizeChanged(RadPropertyChangedEventArgs e)
    {
      RadControl control1 = this.ElementTree.Control as RadControl;
      if (control1 == null || control1.AutoSize)
        return;
      control1.GetControlDefaultSize();
      Size newValue = (Size) e.NewValue;
      RadScrollBar control2 = this.ElementTree.Control as RadScrollBar;
      if (control2 == null)
        return;
      if (control2.ScrollType == ScrollType.Vertical)
      {
        if (control1.Size.Width >= newValue.Width)
          return;
        control1.Width = newValue.Width;
      }
      else
      {
        if (control1.Size.Height >= newValue.Height)
          return;
        control1.Height = newValue.Height;
      }
    }
  }
}
