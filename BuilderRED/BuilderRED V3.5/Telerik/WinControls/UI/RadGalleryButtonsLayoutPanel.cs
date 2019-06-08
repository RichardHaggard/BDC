// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryButtonsLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadGalleryButtonsLayoutPanel : LayoutPanel
  {
    public static RadProperty ChildrenWidthProperty = RadProperty.Register(nameof (ChildrenWidth), typeof (int), typeof (RadGalleryButtonsLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 16, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));

    public int ChildrenWidth
    {
      get
      {
        return (int) this.GetValue(RadGalleryButtonsLayoutPanel.ChildrenWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryButtonsLayoutPanel.ChildrenWidthProperty, (object) value);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Children.Count == 0)
        return finalSize;
      float height = finalSize.Height / (float) this.Children.Count;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        child.Arrange(new RectangleF(0.0f, (float) index * height, child.DesiredSize.Width, height));
      }
      return finalSize;
    }
  }
}
