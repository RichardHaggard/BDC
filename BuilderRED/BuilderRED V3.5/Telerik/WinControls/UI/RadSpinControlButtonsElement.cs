// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinControlButtonsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadSpinControlButtonsElement : RadItem
  {
    private BoxLayout layout;
    private RadRepeatArrowElement arrowButtonUp;
    private RadRepeatArrowElement arrowButtonDown;

    public RadRepeatArrowElement ButtonUp
    {
      get
      {
        return this.arrowButtonUp;
      }
    }

    public RadRepeatArrowElement ButtonDown
    {
      get
      {
        return this.arrowButtonDown;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.layout = new BoxLayout();
      this.layout.Orientation = Orientation.Vertical;
      this.Children.Add((RadElement) this.layout);
      this.arrowButtonUp = new RadRepeatArrowElement();
      this.arrowButtonUp.StretchVertically = false;
      this.arrowButtonUp.Direction = Telerik.WinControls.ArrowDirection.Up;
      this.arrowButtonUp.Border.Visibility = ElementVisibility.Hidden;
      this.arrowButtonUp.Size = new Size(10, 6);
      this.layout.Children.Add((RadElement) this.arrowButtonUp);
      this.arrowButtonUp.BorderThickness = new Padding(0);
      this.arrowButtonDown = new RadRepeatArrowElement();
      this.arrowButtonDown.StretchVertically = false;
      this.arrowButtonDown.Direction = Telerik.WinControls.ArrowDirection.Down;
      this.arrowButtonDown.Border.Visibility = ElementVisibility.Hidden;
      this.arrowButtonDown.Size = new Size(10, 6);
      this.layout.Children.Add((RadElement) this.arrowButtonDown);
      this.arrowButtonDown.BorderThickness = new Padding(0);
    }
  }
}
