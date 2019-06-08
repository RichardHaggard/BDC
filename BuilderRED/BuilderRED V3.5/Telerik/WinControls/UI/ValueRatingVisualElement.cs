// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ValueRatingVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class ValueRatingVisualElement : RatingBaseVisualElement
  {
    private Rectangle clipArea;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
      this.ZIndex = 1;
    }

    internal override Rectangle ClipArea
    {
      get
      {
        return this.clipArea;
      }
      set
      {
        this.clipArea = value;
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      ((RadGdiGraphics) graphics).Graphics.SetClip(this.ClipArea);
      base.PaintElement(graphics, angle, scale);
    }
  }
}
