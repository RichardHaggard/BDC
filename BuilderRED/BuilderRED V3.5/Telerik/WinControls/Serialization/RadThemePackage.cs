// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Serialization.RadThemePackage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.Serialization
{
  [Serializable]
  public class RadThemePackage : RadArchivePackage
  {
    public const string FileExtension = "tssp";
    private string themeName;

    protected override PackageFormat DefaultFormat
    {
      get
      {
        return PackageFormat.XML;
      }
    }

    public string ThemeName
    {
      get
      {
        return this.themeName;
      }
      set
      {
        this.themeName = value;
      }
    }

    public XmlTheme[] DecompressThemes()
    {
      List<XmlTheme> xmlThemeList = new List<XmlTheme>();
      foreach (RadArchiveStream stream in this.Streams)
      {
        XmlTheme xmlTheme = stream.Unzip() as XmlTheme;
        if (xmlTheme != null)
          xmlThemeList.Add(xmlTheme);
      }
      return xmlThemeList.ToArray();
    }
  }
}
