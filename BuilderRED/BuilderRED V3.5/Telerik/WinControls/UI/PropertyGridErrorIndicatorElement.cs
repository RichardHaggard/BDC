// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridErrorIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridErrorIndicatorElement : LightVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = false;
      this.DrawBorder = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.ElementTree.Control.Cursor = Cursors.Default;
      base.OnMouseMove(e);
    }
  }
}
