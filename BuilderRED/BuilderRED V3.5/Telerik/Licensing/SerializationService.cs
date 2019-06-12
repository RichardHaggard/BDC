// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.SerializationService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;

namespace Telerik.Licensing
{
  internal class SerializationService : ISerializationService
  {
    private static readonly object serviceLock = new object();
    private static ISerializationService service;
    private readonly BasicJsonSerializer _serializer;

    private SerializationService()
    {
      this._serializer = new BasicJsonSerializer();
    }

    protected BasicJsonSerializer Serializer
    {
      get
      {
        return this._serializer;
      }
    }

    public static ISerializationService GetInstance()
    {
      if (SerializationService.service == null)
      {
        lock (SerializationService.serviceLock)
        {
          if (SerializationService.service == null)
            SerializationService.service = (ISerializationService) new SerializationService();
        }
      }
      return SerializationService.service;
    }

    public string Serialize<T>(T obj)
    {
      return this.Serializer.Serialize((object) obj);
    }

    public T Deserialize<T>(string serializedObj)
    {
      object obj1 = this._serializer.Deserialize(serializedObj);
      if ((object) typeof (T) != (object) typeof (AccessTokenResponse))
        return (T) obj1;
      object obj2 = (object) new AccessTokenResponse();
      ((AccessTokenResponse) obj2).Access_Token = ((Hashtable) obj1)[(object) "access_token"].ToString();
      return (T) obj2;
    }

    public string SerializeToJson<T>(T obj)
    {
      return this.Serialize<T>(obj);
    }

    public T DeserializeFromJson<T>(string serializedObj)
    {
      return this.Deserialize<T>(serializedObj);
    }
  }
}
