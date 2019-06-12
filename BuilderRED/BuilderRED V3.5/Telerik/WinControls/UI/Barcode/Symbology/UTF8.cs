// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.UTF8
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class UTF8 : ECIBase
  {
    private Encoder encoder;

    public UTF8()
    {
      this.encoder = Encoding.UTF8.GetEncoder();
    }

    public override Dictionary<int, string> EncodeData(string dataToEncode)
    {
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      for (int key = 0; key < dataToEncode.Length; ++key)
      {
        char[] chars = new char[1]{ dataToEncode[key] };
        byte[] bytes = new byte[this.encoder.GetByteCount(chars, 0, 1, true)];
        if (this.encoder.GetBytes(chars, 0, 1, bytes, 0, true) == 1)
        {
          dictionary.Add(key, Convert.ToString(bytes[0], 2).PadLeft(8, '0'));
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (byte num in bytes)
            stringBuilder.Append(Convert.ToString(num, 2).PadLeft(8, '0'));
          dictionary.Add(key, stringBuilder.ToString());
        }
      }
      return dictionary;
    }

    public override string ValidateData(string valueToValidate)
    {
      return valueToValidate;
    }
  }
}
