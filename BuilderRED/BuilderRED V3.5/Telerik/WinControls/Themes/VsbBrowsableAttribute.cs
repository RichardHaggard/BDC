// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.VsbBrowsableAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Themes
{
  public class VsbBrowsableAttribute : Attribute
  {
    private bool browsable;

    public VsbBrowsableAttribute()
      : this(true)
    {
    }

    public VsbBrowsableAttribute(bool browsable)
    {
      this.browsable = browsable;
    }

    public bool Browsable
    {
      get
      {
        return this.browsable;
      }
    }
  }
}
