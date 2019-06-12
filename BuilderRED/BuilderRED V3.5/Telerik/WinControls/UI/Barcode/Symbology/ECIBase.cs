// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.ECIBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class ECIBase
  {
    protected readonly List<string> unicodeValues;
    protected readonly List<string> encodedValues;

    public ECIBase()
    {
      this.encodedValues = new List<string>();
      this.unicodeValues = new List<string>();
    }

    public ECIBase(string encPath, int validEncStep, string uniPath, int validUniStep)
    {
      this.encodedValues = BarcodeResources.GetEncodedValues(encPath, validEncStep);
      this.unicodeValues = BarcodeResources.GetEncodedValues(uniPath, validUniStep);
    }

    public virtual Dictionary<int, string> EncodeData(string dataToEncode)
    {
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      for (int key = 0; key < dataToEncode.Length; ++key)
      {
        int index = this.unicodeValues.IndexOf(Convert.ToString((int) dataToEncode[key], 16).ToUpper().PadLeft(4, '0'));
        if (index != -1)
        {
          int num = int.Parse(this.encodedValues[index], NumberStyles.HexNumber);
          dictionary.Add(key, Convert.ToString(num, 2).PadLeft(8, '0'));
        }
      }
      return dictionary;
    }

    public virtual string ValidateData(string valueToValidate)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < valueToValidate.Length; ++index)
      {
        if (this.unicodeValues.Contains(Convert.ToString((int) valueToValidate[index], 16).ToUpper().PadLeft(4, '0')))
          stringBuilder.Append(valueToValidate[index]);
      }
      return stringBuilder.ToString();
    }
  }
}
