// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.ColorDialog.CustomColorsEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Themes.ColorDialog
{
  public class CustomColorsEventArgs : EventArgs
  {
    private string configLocation;
    private string configFilename;

    public CustomColorsEventArgs(string location, string fileName)
    {
      this.configLocation = location;
      this.configFilename = fileName;
    }

    public string ConfigFilename
    {
      get
      {
        return this.configFilename;
      }
      set
      {
        this.configFilename = value;
      }
    }

    public string ConfigLocation
    {
      get
      {
        return this.configLocation;
      }
      set
      {
        this.configLocation = value;
      }
    }
  }
}
