// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CustomShapeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls
{
  public class CustomShapeEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      CustomShape customShape = value as CustomShape;
      return (object) new CustomShape();
    }

    public override void PaintValue(PaintValueEventArgs e)
    {
      CustomShape customShape = e.Value as CustomShape;
      if (customShape == null)
        return;
      using (GraphicsPath path = customShape.CreatePath(e.Bounds))
        e.Graphics.DrawPath(Pens.Black, path);
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
