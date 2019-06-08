// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorScaleContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class RangeSelectorScaleContainerElement : RangeSelectorVisualElementWithOrientation
  {
    private bool displayScale = true;
    private ViewPosition scalePostion;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
    }

    internal bool DisplayScale
    {
      get
      {
        return this.displayScale;
      }
      set
      {
        this.displayScale = value;
      }
    }

    public ViewPosition ScalePostion
    {
      get
      {
        return this.scalePostion;
      }
      set
      {
        this.scalePostion = value;
      }
    }

    public float ArrangePositionOffset
    {
      get
      {
        if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        {
          if (this.RangeSelectorElement.ShowButtons)
            return (float) (this.RangeSelectorElement.BodyElement.LeftArrow.BoundingRectangle.Width + 1);
          return 0.0f;
        }
        if (this.RangeSelectorElement.ShowButtons)
          return this.RangeSelectorElement.BodyElement.LeftArrow.DesiredSize.Height;
        return 0.0f;
      }
    }
  }
}
