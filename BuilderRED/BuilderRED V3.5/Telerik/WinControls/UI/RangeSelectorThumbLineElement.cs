// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorThumbLineElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorThumbLineElement : RangeSelectorVisualElementWithOrientation
  {
    public static RadProperty LineWidthProperty = RadProperty.Register(nameof (LineWidth), typeof (int), typeof (RangeSelectorThumbLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsTheme));

    public int LineWidth
    {
      get
      {
        return (int) this.GetValue(RangeSelectorThumbLineElement.LineWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RangeSelectorThumbLineElement.LineWidthProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.Red;
      this.GradientStyle = GradientStyles.Solid;
      this.ClipDrawing = false;
      this.StretchVertically = true;
      this.StretchHorizontally = false;
      this.ZIndex = 2;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        return new SizeF((float) this.LineWidth, availableSize.Height);
      return new SizeF(availableSize.Width, (float) this.LineWidth);
    }
  }
}
