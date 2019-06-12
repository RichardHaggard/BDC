// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.SessionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class SessionChangedEventArgs : EventArgs
  {
    private readonly Session _session;

    public SessionChangedEventArgs(Session session)
    {
      this._session = session;
    }

    public Session Session
    {
      get
      {
        return this._session;
      }
    }
  }
}
