// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  internal class SplitterLayout : LayoutPanel
  {
    private RadSplitter owner;

    public SplitterLayout(RadSplitter owner)
    {
      this.owner = owner;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Children.Count == 0)
        return base.MeasureOverride(availableSize);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        int num = this.owner.ThumbLength;
        if (this.Children[index].MinSize.Height != 0)
          num = this.Children[index].MinSize.Height;
        SizeF availableSize1 = this.owner.Dock == DockStyle.Left || this.owner.Dock == DockStyle.Right ? new SizeF(availableSize.Width, (float) num) : new SizeF((float) num, availableSize.Height);
        this.Children[index].Measure(availableSize1);
      }
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Children.Count == 0)
        return base.ArrangeOverride(finalSize);
      int num1 = 0;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility == ElementVisibility.Visible)
          num1 += this.owner.ThumbLength;
      }
      float num2 = this.owner.Dock == DockStyle.Left || this.owner.Dock == DockStyle.Right ? (float) (((double) finalSize.Height - (double) num1) / 2.0) : (float) (((double) finalSize.Width - (double) num1) / 2.0);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility == ElementVisibility.Visible)
        {
          if (this.owner.Dock == DockStyle.Left || this.owner.Dock == DockStyle.Right)
            this.Children[index].Arrange(new RectangleF(0.0f, num2, finalSize.Width, (float) this.owner.ThumbLength));
          else
            this.Children[index].Arrange(new RectangleF(num2, 0.0f, (float) this.owner.ThumbLength, finalSize.Height));
          num2 += (float) this.owner.ThumbLength;
        }
      }
      return finalSize;
    }
  }
}
