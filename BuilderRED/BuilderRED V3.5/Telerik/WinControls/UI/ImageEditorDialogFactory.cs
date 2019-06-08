// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditorDialogFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.ImageEditor.Dialogs;

namespace Telerik.WinControls.UI
{
  public class ImageEditorDialogFactory
  {
    public virtual ImageEditorBaseDialog CreateDialog(
      System.Type type,
      RadImageEditorElement imageEditorElement)
    {
      ImageEditorBaseDialog editorBaseDialog = type.GetConstructor(new System.Type[1]{ typeof (RadImageEditorElement) }).Invoke(new object[1]{ (object) imageEditorElement }) as ImageEditorBaseDialog;
      editorBaseDialog.RightToLeft = imageEditorElement.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      editorBaseDialog.StartPosition = FormStartPosition.Manual;
      editorBaseDialog.Location = imageEditorElement.ElementTree.Control.PointToScreen(new Point(imageEditorElement.CommandsElementWidth, 0));
      return editorBaseDialog;
    }
  }
}
