﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListRootElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RadDropDownListRootElement : RootRadElement
  {
    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "MinSize" || property.Name == "MaxSize" || property.Name == "AutoSizeMode")
        return new bool?(false);
      return base.ShouldSerializeProperty(property);
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RootRadElement);
      }
    }
  }
}
