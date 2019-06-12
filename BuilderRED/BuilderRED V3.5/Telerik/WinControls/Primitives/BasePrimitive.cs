// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.BasePrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class BasePrimitive : VisualElement, IPrimitive
  {
    public const string BoxCategory = "Box";
    internal const long BasePrimitiveLastStateKey = 68719476736;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldPaint = true;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      this.PaintPrimitive(graphics, angle, scale);
    }

    public virtual void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsEmpty
    {
      get
      {
        return false;
      }
    }
  }
}
