// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStripButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStripButtonElement : RadPageViewButtonElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ImageAlignment = ContentAlignment.MiddleCenter;
    }

    protected internal override void SetContentOrientation(
      PageViewContentOrientation orientation,
      bool recursive)
    {
      switch ((StripViewButtons) this.Tag)
      {
        case StripViewButtons.LeftScroll:
        case StripViewButtons.RightScroll:
          if (orientation == PageViewContentOrientation.Vertical270)
          {
            orientation = PageViewContentOrientation.Vertical90;
            break;
          }
          break;
        case StripViewButtons.Close:
          orientation = PageViewContentOrientation.Horizontal;
          break;
      }
      base.SetContentOrientation(orientation, recursive);
    }
  }
}
