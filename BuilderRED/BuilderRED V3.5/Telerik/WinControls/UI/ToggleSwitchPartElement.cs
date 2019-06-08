// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleSwitchPartElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleSwitchPartElement : LightVisualElement
  {
    private RadToggleSwitchElement toggleSwitchElement;
    private object state;

    static ToggleSwitchPartElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (ToggleSwitchPartElement));
    }

    public ToggleSwitchPartElement(RadToggleSwitchElement toggleSwitchElement)
    {
      this.toggleSwitchElement = toggleSwitchElement;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float thumbTickness = (float) this.toggleSwitchElement.ThumbTickness;
      PointF location = new PointF(clientRectangle.X, clientRectangle.Y);
      SizeF size = SizeF.Subtract(finalSize, new SizeF(thumbTickness / 2f, 0.0f));
      if (!this.toggleSwitchElement.RightToLeft)
      {
        if (object.ReferenceEquals((object) this, (object) this.toggleSwitchElement.OffElement))
          this.Layout.Arrange(new RectangleF(new PointF(location.X + thumbTickness / 2f, location.Y), size));
        else
          this.Layout.Arrange(new RectangleF(location, size));
      }
      else if (object.ReferenceEquals((object) this, (object) this.toggleSwitchElement.OffElement))
        this.Layout.Arrange(new RectangleF(location, size));
      else
        this.Layout.Arrange(new RectangleF(new PointF(location.X + thumbTickness / 2f, location.Y), size));
      return sizeF;
    }

    protected override void PrePaintElement(IGraphics graphics)
    {
      this.state = (object) null;
      if (this.Parent.Shape != null)
      {
        GraphicsPath elementShape = this.Parent.Shape.GetElementShape(this.Parent);
        Matrix matrix = new Matrix();
        matrix.Translate((float) -this.BoundingRectangle.X, (float) -this.BoundingRectangle.Y);
        elementShape.Transform(matrix);
        ((Graphics) graphics.UnderlayGraphics).SetClip(elementShape, CombineMode.Intersect);
        this.state = graphics.SaveState();
      }
      base.PrePaintElement(graphics);
    }

    protected override void PostPaintElement(IGraphics graphics)
    {
      base.PostPaintElement(graphics);
      if (this.state == null)
        return;
      graphics.RestoreState(this.state);
    }
  }
}
