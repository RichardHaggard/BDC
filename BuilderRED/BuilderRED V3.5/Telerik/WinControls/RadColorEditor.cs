// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadColorEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadColorEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public static IColorSelector CreateColorSelectorInstance()
    {
      System.Type type = System.Type.GetType("Telerik.WinControls.UI.RadColorSelector");
      if ((object) type == null)
        type = System.Type.GetType(string.Format("Telerik.WinControls.UI.RadColorSelector, Telerik.WinControls.UI, Version={0}, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", (object) "2018.3.1016.20"));
      if ((object) type != null)
        return (IColorSelector) Activator.CreateInstance(type);
      throw new InvalidOperationException("Unable to locate color selector: Telerik.WinControls.UI.RadColorSelector. Please add reference to assembly Telerik.WinControls.UI.dll");
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      IRadColorDialog colorDialogInstance = RadColorEditor.CreateColorDialogInstance();
      UserControl radColorSelector = colorDialogInstance.RadColorSelector;
      bool flag = false;
      if (value != null && (object) value.GetType() == (object) typeof (Color))
      {
        ((IColorSelector) radColorSelector).SelectedColor = (Color) value;
        ((IColorSelector) radColorSelector).OldColor = (Color) value;
      }
      else if (value != null && (object) value.GetType() == (object) typeof (HslColor))
      {
        ((IColorSelector) radColorSelector).SelectedHslColor = (HslColor) value;
        ((IColorSelector) radColorSelector).OldColor = ((HslColor) value).RgbValue;
        flag = true;
      }
      if (value == null)
        return (object) null;
      if (((Form) colorDialogInstance).ShowDialog() != DialogResult.OK)
        return value;
      if (flag)
        return (object) colorDialogInstance.SelectedHslColor;
      return (object) colorDialogInstance.SelectedColor;
    }

    public override void PaintValue(PaintValueEventArgs e)
    {
      Color color = e.Value == null || (object) e.Value.GetType() != (object) typeof (HslColor) ? (e.Value == null ? Color.Empty : (Color) e.Value) : ((HslColor) e.Value).RgbValue;
      if (e.Value == null)
        return;
      using (Brush brush = (Brush) new SolidBrush(color))
        e.Graphics.FillRectangle(brush, e.Bounds);
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public static IRadColorDialog CreateColorDialogInstance()
    {
      System.Type type = System.Type.GetType("Telerik.WinControls.UI.RadColorDialogForm");
      if ((object) type == null)
        type = System.Type.GetType(string.Format("Telerik.WinControls.UI.RadColorDialogForm, Telerik.WinControls.UI, Version={0}, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", (object) "2018.3.1016.20"));
      if ((object) type != null)
        return (IRadColorDialog) Activator.CreateInstance(type);
      throw new InvalidOperationException("Unable to locate color selector: Telerik.WinControls.UI.RadColorDialogForm. Please add reference to assembly Telerik.WinControls.UI.dll");
    }
  }
}
