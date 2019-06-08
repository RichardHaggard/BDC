// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MaskPart
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class MaskPart
  {
    public int max = 2500;
    public string maskPart;
    public int start;
    public int len;
    public bool month;
    public bool year;
    public bool day;
    public bool readOnly;
    public PartTypes type;
    public int offset;
    public int min;
    public int value;
    public string charValue;
    public bool hasZero;
    public bool trimsZeros;

    public void Validate()
    {
      if (this.value > this.max)
        this.value = this.max;
      if (this.value >= this.min)
        return;
      this.value = this.min;
    }
  }
}
