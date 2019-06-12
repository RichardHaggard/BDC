// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.GripPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  [Editor(typeof (RadFillEditor), typeof (UITypeEditor))]
  public class GripPrimitive : BasePrimitive
  {
    public static RadProperty NumberOfDotsProperty = RadProperty.Register(nameof (NumberOfDots), typeof (int), typeof (GripPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor2Property = RadProperty.Register(nameof (BackColor2), typeof (Color), typeof (GripPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.Control), ElementPropertyOptions.AffectsDisplay));

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
    }

    [DefaultValue(false)]
    public override bool StretchHorizontally
    {
      get
      {
        return base.StretchHorizontally;
      }
      set
      {
        base.StretchHorizontally = value;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BackColor2", typeof (GripPrimitive))]
    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Description("Second color component when gradient style is other than solid")]
    public virtual Color BackColor2
    {
      get
      {
        return (Color) this.GetValue(GripPrimitive.BackColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(GripPrimitive.BackColor2Property, (object) value);
      }
    }

    public virtual int NumberOfDots
    {
      get
      {
        return (int) this.GetValue(GripPrimitive.NumberOfDotsProperty);
      }
      set
      {
        int num = (int) this.SetValue(GripPrimitive.NumberOfDotsProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      int num1 = this.Size.Height / this.NumberOfDots;
      RectangleF BorderRectangle1 = new RectangleF(0.0f, 4f, 2f, 2f);
      int num2 = 0;
      while (true)
      {
        RectangleF BorderRectangle2 = new RectangleF(BorderRectangle1.X + 0.1f, BorderRectangle1.Y + 0.1f, 2f, 2f);
        g.FillRectangle(BorderRectangle2, this.BackColor2);
        g.FillRectangle(BorderRectangle1, this.BackColor);
        BorderRectangle1.Y += 4f;
        if ((double) BorderRectangle1.Y + 6.0 <= (double) this.Size.Height)
          ++num2;
        else
          break;
      }
    }
  }
}
