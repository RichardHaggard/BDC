// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.EAN128A
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class EAN128A : Code128A
  {
    private static readonly string Prefix = "÷";

    protected override int[] GetIndices(string value)
    {
      if (!value.StartsWith(EAN128A.Prefix))
        value = EAN128A.Prefix + value;
      return base.GetIndices(value);
    }
  }
}
