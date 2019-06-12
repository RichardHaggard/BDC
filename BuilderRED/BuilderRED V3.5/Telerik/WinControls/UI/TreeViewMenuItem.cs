// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TreeViewMenuItem : RadMenuItem
  {
    private string command;

    public TreeViewMenuItem(string command, string text)
      : base(text)
    {
      this.command = command;
    }

    public string Command
    {
      get
      {
        return this.command;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }
  }
}
