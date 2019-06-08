// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Symbology2D
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class Symbology2D
  {
    public List<char> CharSet { get; set; }

    public Dictionary<char, string> Encoding { get; set; }
  }
}
