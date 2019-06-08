// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStripGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStripGroupItem : RadPageViewStripItem
  {
    private LightVisualElement underline;

    public RadPageViewStripGroupItem()
    {
    }

    public RadPageViewStripGroupItem(string text)
      : base(text)
    {
    }

    public RadPageViewStripGroupItem(string text, Image image)
      : base(text, image)
    {
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = true;
      this.BackColor = Color.Green;
      this.underline = new LightVisualElement();
      this.underline.StretchHorizontally = true;
      this.underline.Class = "PageViewGroupItemUnderline";
      this.underline.ThemeRole = "PageViewGroupItemUnderline";
      this.Children.Add((RadElement) this.underline);
    }

    protected override void ArrangeChildren(SizeF available)
    {
      base.ArrangeChildren(available);
      RectangleF clientRectangle = this.GetClientRectangle(available);
      this.underline.Arrange(new RectangleF(clientRectangle.Left, clientRectangle.Bottom - this.underline.DesiredSize.Height, clientRectangle.Width, this.underline.DesiredSize.Height));
    }
  }
}
