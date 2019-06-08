// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImagePart
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ImagePart : VisualElementLayoutPart
  {
    public ImagePart(LightVisualElement owner)
      : base(owner)
    {
    }

    public override SizeF Measure(SizeF availableSize)
    {
      if (this.Owner.Image != null)
      {
        lock (this.Owner.Image)
        {
          if (this.Owner.ImageLayout == ImageLayout.Zoom)
          {
            float f = Math.Min(availableSize.Width / (float) this.Owner.Image.Width, availableSize.Height / (float) this.Owner.Image.Height);
            int num1 = (int) Math.Round((double) this.Owner.Image.Width * (double) f);
            int num2 = (int) Math.Round((double) this.Owner.Image.Height * (double) f);
            if (float.IsInfinity(f))
              num1 = num2 = 0;
            this.DesiredSize = new SizeF((float) num1, (float) num2);
          }
          else
            this.DesiredSize = TelerikDpiHelper.ScaleSizeF((SizeF) this.Owner.Image.Size, this.Owner.DpiScaleFactor);
        }
      }
      else
        this.DesiredSize = SizeF.Empty;
      this.DesiredSize = new SizeF(Math.Min(availableSize.Width, this.DesiredSize.Width), Math.Min(availableSize.Height, this.DesiredSize.Height));
      return this.DesiredSize;
    }
  }
}
