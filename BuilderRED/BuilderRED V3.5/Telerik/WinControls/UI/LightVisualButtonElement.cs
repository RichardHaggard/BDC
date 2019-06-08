// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LightVisualButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("Click")]
  public class LightVisualButtonElement : LightVisualElement
  {
    static LightVisualButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (LightVisualButtonElement));
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
    }
  }
}
