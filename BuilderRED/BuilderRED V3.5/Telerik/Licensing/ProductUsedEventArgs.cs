// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ProductUsedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class ProductUsedEventArgs : ComponentUsedEventArgs
  {
    private readonly string _key;

    public ProductUsedEventArgs(Type type, string sessionId)
      : base(type, sessionId)
    {
      this._key = string.Empty;
    }

    public string InstalationKey
    {
      get
      {
        return this._key;
      }
    }
  }
}
