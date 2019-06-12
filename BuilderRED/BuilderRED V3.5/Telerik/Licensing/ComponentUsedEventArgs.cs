// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ComponentUsedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class ComponentUsedEventArgs : EventArgs
  {
    private readonly Type _type;
    private readonly string _sessionId;

    public ComponentUsedEventArgs(Type type, string sessionId)
    {
      this._type = type;
      this._sessionId = sessionId;
    }

    public Type Type
    {
      get
      {
        return this._type;
      }
    }

    public string SessionId
    {
      get
      {
        return this._sessionId;
      }
    }
  }
}
