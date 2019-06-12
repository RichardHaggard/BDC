// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorHoverElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorHoverElement : RangeSelectorVisualElementWithOrientation
  {
    private bool isFirst;

    public RangeSelectorHoverElement(bool isFirst)
    {
      this.isFirst = isFirst;
      if (isFirst)
        this.Class = "SelectorLeftHoverElement";
      else
        this.Class = "SelectorRightHoverElement";
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.Gray;
      this.GradientStyle = GradientStyles.Solid;
      this.StretchVertically = true;
      this.StretchHorizontally = false;
      this.BorderWidth = 0.0f;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.RangeSelectorElement.BodyElement.ViewContainer.SelectionRectangle.PerformSelctionClick(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RangeSelectorTrackingElement trackingElement = this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement;
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        if (this.isFirst)
          return new SizeF((float) ((double) trackingElement.StartRange * (double) availableSize.Width / 100.0), availableSize.Height);
        return new SizeF((float) ((100.0 - (double) trackingElement.EndRange) * (double) availableSize.Width / 100.0), availableSize.Height);
      }
      if (this.isFirst)
      {
        float height = (float) ((double) trackingElement.StartRange * (double) availableSize.Height / 100.0);
        return new SizeF(availableSize.Width, height);
      }
      float height1 = (float) ((100.0 - (double) trackingElement.EndRange) * (double) availableSize.Height / 100.0);
      return new SizeF(availableSize.Width, height1);
    }
  }
}
