// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class FilterMenuItem : RadMenuItem
  {
    public FilterMenuItem(string text)
      : base(text)
    {
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }

    protected override void OnClick(EventArgs e)
    {
      this.ElementTree.Control.Hide();
      base.OnClick(e);
    }
  }
}
