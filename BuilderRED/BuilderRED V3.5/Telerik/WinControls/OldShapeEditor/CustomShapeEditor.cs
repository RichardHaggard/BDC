// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.CustomShapeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.OldShapeEditor
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
      System.IServiceProvider provider,
      object value)
    {
      CustomShape customShape1 = value as CustomShape;
      CustomShapeEditorForm customShapeEditorForm = new CustomShapeEditorForm();
      if (customShape1 != null)
      {
        customShapeEditorForm.EditorControl.Dimension = customShape1.Dimension;
        customShapeEditorForm.EditorControl.Points.AddRange((IEnumerable<ShapePoint>) customShape1.Points);
      }
      if (customShapeEditorForm.ShowDialog() != DialogResult.OK)
        return value;
      CustomShape customShape2 = new CustomShape();
      customShape2.Points.AddRange((IEnumerable<ShapePoint>) customShapeEditorForm.EditorControl.Points);
      customShape2.Dimension = customShapeEditorForm.EditorControl.Dimension;
      return (object) customShape2;
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
