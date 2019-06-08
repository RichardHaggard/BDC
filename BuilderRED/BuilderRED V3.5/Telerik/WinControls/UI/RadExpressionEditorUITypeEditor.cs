// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadExpressionEditorUITypeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class RadExpressionEditorUITypeEditor : UITypeEditor
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
      IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
      if (provider != null)
        formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
      if (formsEditorService != null)
      {
        RadExpressionEditorForm expressionEditorForm = new RadExpressionEditorForm(context.Instance as GridViewDataColumn);
        expressionEditorForm.StartPosition = FormStartPosition.CenterScreen;
        expressionEditorForm.Expression = value.ToString();
        if (formsEditorService.ShowDialog((Form) expressionEditorForm) == DialogResult.OK)
          value = (object) expressionEditorForm.Expression;
      }
      return value;
    }
  }
}
