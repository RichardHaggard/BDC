﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.AccessTokenPayload
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class AccessTokenPayload
  {
    private readonly string _grantType = "client_credentials";

    public string Grant_Type
    {
      get
      {
        return this._grantType;
      }
    }
  }
}
