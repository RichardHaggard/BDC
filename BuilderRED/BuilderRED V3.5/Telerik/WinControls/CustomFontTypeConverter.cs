// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CustomFontTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls
{
  public class CustomFontTypeConverter : StringConverter
  {
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return true;
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      return new TypeConverter.StandardValuesCollection((ICollection) new List<string>()
      {
        "None",
        "TelerikWebUI",
        "Roboto",
        "Roboto Medium",
        "WebComponentsIcons",
        "Font Awesome 5 Brands",
        "Font Awesome 5 Free",
        "Font Awesome 5 Free Solid"
      });
    }
  }
}
