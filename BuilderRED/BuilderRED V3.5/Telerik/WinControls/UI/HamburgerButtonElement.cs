// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HamburgerButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class HamburgerButtonElement : RadButtonElement
  {
    private readonly RadPageViewNavigationViewElement navigationViewElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Alignment = ContentAlignment.TopLeft;
      this.Padding = new Padding(10);
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.Text = "☰";
    }

    public HamburgerButtonElement(
      RadPageViewNavigationViewElement navigationViewElement)
    {
      this.navigationViewElement = navigationViewElement;
    }

    public RadPageViewNavigationViewElement NavigationViewElement
    {
      get
      {
        return this.navigationViewElement;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Height = (float) this.NavigationViewElement.HeaderHeight;
      return sizeF;
    }
  }
}
