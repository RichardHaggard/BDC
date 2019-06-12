// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSortOrderAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [AttributeUsage(AttributeTargets.Property)]
  [Description("The RadSortOrderAttribute is an attribute which specifies the sort order for properties inside RadPropertyGrid.")]
  public class RadSortOrderAttribute : Attribute
  {
    private int value;

    public RadSortOrderAttribute(int value)
    {
      this.value = value;
    }

    public int Value
    {
      get
      {
        return this.value;
      }
    }
  }
}
