// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ResFinder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Properties;

namespace Telerik.WinControls
{
  [Description("Repository for Telerik-related resources. Not for general use.")]
  public class ResFinder
  {
    public static Icon ProgressIcon
    {
      get
      {
        return Resources.ProgressIcon;
      }
    }

    public static Image WinFormsLogoWithText
    {
      get
      {
        return (Image) Resources.WinFormsLogo;
      }
    }

    public static Image MenuIcon
    {
      get
      {
        return (Image) Resources.MenuIcon;
      }
    }

    public static Icon WinFormsIcon
    {
      get
      {
        return Resources.WinFormsIcon;
      }
    }
  }
}
