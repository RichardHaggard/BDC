// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPickerEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadColorPickerEditorElement : RadColorBoxElement
  {
    public override void SetColorValue(Color newValue)
    {
      bool flag = false;
      if (this.ReadOnly)
      {
        this.ReadOnly = false;
        flag = true;
      }
      base.SetColorValue(newValue);
      if (!flag)
        return;
      this.ReadOnly = true;
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadColorBoxElement);
      }
    }
  }
}
