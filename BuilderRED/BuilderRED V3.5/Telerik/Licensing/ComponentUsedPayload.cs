// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ComponentUsedPayload
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class ComponentUsedPayload : ProductUsedPayload
  {
    private const string EventType = "ComponentUsed";

    public ComponentUsedPayload(Type type, string machineId, string sessionId)
      : base(type, machineId, sessionId)
    {
      this.ComponentType = type.FullName;
      this.Type = "ComponentUsed";
    }

    public string ComponentType { get; set; }
  }
}
