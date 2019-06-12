// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ClientUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public static class ClientUtils
  {
    public static int GetBitCount(uint x)
    {
      int num = 0;
      while (x > 0U)
      {
        x &= x - 1U;
        ++num;
      }
      return num;
    }

    public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
    {
      if (value >= minValue)
        return value <= maxValue;
      return false;
    }

    public static bool IsEnumValid(
      Enum enumValue,
      int value,
      int minValue,
      int maxValue,
      int maxNumberOfBitsOn)
    {
      if (value >= minValue && value <= maxValue)
        return ClientUtils.GetBitCount((uint) value) <= maxNumberOfBitsOn;
      return false;
    }

    public static bool ArraysEqual(byte[] a1, byte[] a2)
    {
      if (a1 == a2)
        return true;
      if (a1 == null || a2 == null || a1.Length != a2.Length)
        return false;
      for (int index = 0; index < a1.Length; ++index)
      {
        if ((int) a1[index] != (int) a2[index])
          return false;
      }
      return true;
    }
  }
}
