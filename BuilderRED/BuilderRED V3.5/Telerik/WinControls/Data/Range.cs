// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.Range
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Data
{
  public struct Range
  {
    private int min;
    private int max;
    private bool isNotNull;

    public Range(int min, int max)
    {
      this.min = min;
      this.max = max;
      this.isNotNull = true;
    }

    public int Min
    {
      get
      {
        return this.min;
      }
    }

    public int Max
    {
      get
      {
        return this.max;
      }
    }

    public int Count
    {
      get
      {
        if (this.IsNull)
          return 0;
        return this.max - this.min + 1;
      }
    }

    public bool IsNull
    {
      get
      {
        return !this.isNotNull;
      }
    }
  }
}
