// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStripItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStripItem : RadPageViewItem
  {
    public static RadProperty AutoCorrectOrientationProperty = RadProperty.Register(nameof (AutoCorrectOrientation), typeof (bool), typeof (RadPageViewStripItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public const float EditorOffset = 5f;

    [Description("Gets or sets a property which determines whether to consider the ItemBorderAndFillOrientation of RadPageViewElement.")]
    [Category("Appearance")]
    public bool AutoCorrectOrientation
    {
      get
      {
        return (bool) this.GetValue(RadPageViewStripItem.AutoCorrectOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripItem.AutoCorrectOrientationProperty, (object) value);
      }
    }

    public RadPageViewStripItem()
    {
    }

    public RadPageViewStripItem(string text)
    {
      this.Text = text;
    }

    public RadPageViewStripItem(string text, Image image)
    {
      this.Text = text;
      this.Image = image;
    }

    protected override object CorrectFillAndBorderOrientation(IGraphics g)
    {
      if (this.AutoCorrectOrientation)
        return this.ApplyOrientationTransform(g, this.BorderAndFillOrientation);
      return (object) null;
    }

    protected override RectangleF ModifyBorderAndFillPaintRect(
      RectangleF preferred,
      Padding padding)
    {
      if (!this.AutoCorrectOrientation)
        return preferred;
      return base.ModifyBorderAndFillPaintRect(preferred, padding);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      foreach (RadElement child in this.Children)
      {
        if (child is RadTextBoxControlElement)
          child.Arrange(new RectangleF(5f, 5f, finalSize.Width - 10f, finalSize.Height - 10f));
      }
      return sizeF;
    }
  }
}
