// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ISerializationService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal interface ISerializationService
  {
    string Serialize<T>(T obj);

    T Deserialize<T>(string serializedObj);

    string SerializeToJson<T>(T obj);

    T DeserializeFromJson<T>(string serializedObj);
  }
}
