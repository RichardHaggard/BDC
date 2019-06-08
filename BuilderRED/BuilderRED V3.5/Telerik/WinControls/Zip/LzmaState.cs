// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal struct LzmaState
  {
    public uint Index { get; private set; }

    public static uint GetLenToPosState(uint length)
    {
      length -= 2U;
      if (length < 4U)
        return length;
      return 3;
    }

    public void UpdateChar()
    {
      if (this.Index < 4U)
        this.Index = 0U;
      else if (this.Index < 10U)
        this.Index -= 3U;
      else
        this.Index -= 6U;
    }

    public void UpdateMatch()
    {
      this.Index = this.Index < 7U ? 7U : 10U;
    }

    public void UpdateRep()
    {
      this.Index = this.Index < 7U ? 8U : 11U;
    }

    public void UpdateShortRep()
    {
      this.Index = this.Index < 7U ? 9U : 11U;
    }

    public bool IsCharState()
    {
      return this.Index < 7U;
    }
  }
}
