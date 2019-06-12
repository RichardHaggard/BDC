// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStackItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStackItem : RadPageViewItem
  {
    public RadPageViewStackItem()
    {
    }

    public RadPageViewStackItem(string text)
      : this()
    {
      this.Text = text;
    }

    public RadPageViewStackItem(string text, Image image)
      : this(text)
    {
      this.Image = image;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      foreach (RadElement child in this.Children)
      {
        if (child is RadTextBoxControlElement)
        {
          float x = 5f;
          float y = (float) (((double) finalSize.Height - (double) child.DesiredSize.Height) / 2.0);
          child.Arrange(new RectangleF(x, y, child.DesiredSize.Width, child.DesiredSize.Height));
        }
      }
      return sizeF;
    }
  }
}
