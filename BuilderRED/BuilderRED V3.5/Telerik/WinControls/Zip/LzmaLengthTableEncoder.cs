// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaLengthTableEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaLengthTableEncoder : LzmaLengthEncoder
  {
    private uint[] prices = new uint[new IntPtr(4352)];
    private uint[] counters = new uint[new IntPtr(16)];
    private uint tableSize;

    public void SetTableSize(uint size)
    {
      this.tableSize = size;
    }

    public uint GetPrice(uint symbol, uint posState)
    {
      return this.prices[(IntPtr) (posState * 272U + symbol)];
    }

    public void UpdateTable(uint posState)
    {
      this.SetPrices(posState, this.tableSize, this.prices, posState * 272U);
      this.counters[(IntPtr) posState] = this.tableSize;
    }

    public void UpdateTables(uint posStates)
    {
      for (uint posState = 0; posState < posStates; ++posState)
        this.UpdateTable(posState);
    }

    public new void Encode(LzmaRangeEncoder rangeEncoder, uint symbol, uint posState)
    {
      base.Encode(rangeEncoder, symbol, posState);
      if (--this.counters[(IntPtr) posState] != 0U)
        return;
      this.UpdateTable(posState);
    }
  }
}
