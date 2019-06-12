﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxThreeStateAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [Description("The RadSortOrderAttribute is an attribute which specifies the sort order for properties inside RadPropertyGrid.")]
  [AttributeUsage(AttributeTargets.Property)]
  public class RadCheckBoxThreeStateAttribute : Attribute
  {
    private bool value;

    public RadCheckBoxThreeStateAttribute(bool value)
    {
      this.value = value;
    }

    public bool Value
    {
      get
      {
        return this.value;
      }
    }
  }
}
