// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualElementLayoutPart
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VisualElementLayoutPart : IVisualLayoutPart
  {
    private RectangleF bounds;
    private SizeF desiredSize;
    private LightVisualElement owner;
    private Padding padding;
    private Padding margin;

    public Padding Margin
    {
      get
      {
        return this.margin;
      }
      set
      {
        this.margin = value;
      }
    }

    public Padding Padding
    {
      get
      {
        return this.padding;
      }
      set
      {
        this.padding = value;
      }
    }

    public VisualElementLayoutPart(LightVisualElement owner)
    {
      this.owner = owner;
    }

    public LightVisualElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public RectangleF Bounds
    {
      get
      {
        return this.bounds;
      }
    }

    public SizeF DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
      set
      {
        this.desiredSize = value;
      }
    }

    public virtual SizeF Measure(SizeF availableSize)
    {
      return SizeF.Empty;
    }

    public virtual SizeF Arrange(RectangleF bounds)
    {
      this.bounds = bounds;
      return new SizeF(this.bounds.Size);
    }
  }
}
