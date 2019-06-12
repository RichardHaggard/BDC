// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridSizeGripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridSizeGripElement : LightVisualElement
  {
    static PropertyGridSizeGripElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (PropertyGridSizeGripElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
      this.MinSize = new Size(0, 5);
      this.DrawFill = true;
      this.DrawBorder = true;
    }
  }
}
