// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadFillEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  public class RadFillEditor : UITypeEditor
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
      if (value == null)
        return (object) null;
      RadGradientDialog radGradientDialog = new RadGradientDialog();
      FillPrimitive fillPrimitive = (FillPrimitive) value;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) fillPrimitive);
      radGradientDialog.Fill.BackColor = (Color) properties.Find("BackColor", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.BackColor2 = (Color) properties.Find("BackColor2", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.BackColor3 = (Color) properties.Find("BackColor3", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.BackColor4 = (Color) properties.Find("BackColor4", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.GradientAngle = (float) properties.Find("GradientAngle", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.GradientPercentage = (float) properties.Find("GradientPercentage", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.GradientPercentage2 = (float) properties.Find("GradientPercentage2", true).GetValue((object) fillPrimitive);
      radGradientDialog.Fill.GradientStyle = (GradientStyles) properties.Find("GradientStyle", true).GetValue((object) fillPrimitive);
      if (radGradientDialog.ShowDialog() == DialogResult.OK)
      {
        properties.Find("BackColor", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.BackColor);
        properties.Find("BackColor2", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.BackColor2);
        properties.Find("BackColor3", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.BackColor3);
        properties.Find("BackColor4", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.BackColor4);
        properties.Find("GradientAngle", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.GradientAngle);
        properties.Find("GradientPercentage", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.GradientPercentage);
        properties.Find("GradientPercentage2", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.GradientPercentage2);
        properties.Find("GradientStyle", false).SetValue((object) fillPrimitive, (object) radGradientDialog.Fill.GradientStyle);
      }
      return value;
    }

    public override void PaintValue(PaintValueEventArgs e)
    {
      if (e.Value == null || !(e.Value is FillPrimitive))
        return;
      using (Brush backgroundBrush = (Brush) new SolidBrush(Color.Transparent))
      {
        Bitmap asBitmap = (e.Value as FillPrimitive).GetAsBitmap(backgroundBrush, 0.0f, new SizeF(1f, 1f));
        if (asBitmap == null)
          return;
        e.Graphics.DrawImage((Image) asBitmap, e.Bounds);
        asBitmap.Dispose();
      }
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
