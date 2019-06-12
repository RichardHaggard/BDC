// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.GaugeVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.UI.Gauges
{
  public class GaugeVisualElement : LightVisualElement
  {
    public virtual GraphicsPath Path
    {
      get
      {
        return (GraphicsPath) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(typeof (ElementVisibility), "Visible")]
    [Browsable(false)]
    public override ElementVisibility Visibility
    {
      get
      {
        return base.Visibility;
      }
      set
      {
        base.Visibility = value;
      }
    }

    private bool ShouldSeriliazeVisibility()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(false)]
    [Browsable(false)]
    public override bool DrawText
    {
      get
      {
        return base.DrawText;
      }
      set
      {
        base.DrawText = value;
      }
    }

    public override bool HitTest(Point point)
    {
      if (this.Path == null)
        return false;
      using (GraphicsPath graphicsPath = (GraphicsPath) this.Path.Clone())
      {
        bool flag = false;
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          graphicsPath.Transform(gdiMatrix);
          flag = graphicsPath.IsVisible(point);
        }
        return flag;
      }
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName != "Shape")
        this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape(this);
      this.Invalidate();
    }

    protected override bool IsInVisibleClipBounds(Rectangle clipRectangle)
    {
      return true;
    }

    public class GaugeShape : ElementShape
    {
      private readonly GaugeVisualElement owner;

      public GaugeShape(GaugeVisualElement owner)
      {
        this.owner = owner;
      }

      public override GraphicsPath CreatePath(Rectangle bounds)
      {
        GraphicsPath path = this.owner.Path;
        this.MirrorPath(path, (RectangleF) bounds);
        return path;
      }
    }
  }
}
