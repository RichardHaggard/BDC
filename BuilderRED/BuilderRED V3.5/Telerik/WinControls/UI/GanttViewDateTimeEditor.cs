// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDateTimeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewDateTimeEditor : BaseDateTimeEditor
  {
    protected override RadElement CreateEditorElement()
    {
      BaseDateTimeEditorElement editorElement = (BaseDateTimeEditorElement) base.CreateEditorElement();
      editorElement.Format = DateTimePickerFormat.Custom;
      editorElement.CustomFormat = "f";
      return (RadElement) editorElement;
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      (this.OwnerElement as GanttViewTextItemElement)?.TextView.GanttViewElement.EndEdit();
    }
  }
}
