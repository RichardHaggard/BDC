// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaOptimizationData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class LzmaOptimizationData
  {
    public LzmaState State { get; set; }

    public bool Prev1IsChar { get; set; }

    public bool Prev2 { get; set; }

    public uint PosPrev2 { get; set; }

    public uint BackPrev2 { get; set; }

    public uint Price { get; set; }

    public uint PosPrev { get; set; }

    public uint BackPrev { get; set; }

    public uint Backs0 { get; set; }

    public uint Backs1 { get; set; }

    public uint Backs2 { get; set; }

    public uint Backs3 { get; set; }

    public void MakeAsChar()
    {
      this.BackPrev = uint.MaxValue;
      this.Prev1IsChar = false;
    }

    public void MakeAsShortRep()
    {
      this.BackPrev = 0U;
      this.Prev1IsChar = false;
    }

    public bool IsShortRep()
    {
      return this.BackPrev == 0U;
    }
  }
}
