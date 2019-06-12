// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.BasicJsonSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Telerik.Licensing
{
  internal class BasicJsonSerializer
  {
    private const string Quote = "\"";

    public string Serialize(object objectToSerialize)
    {
      return BasicJsonSerializer.SerializeObject(objectToSerialize);
    }

    public object Deserialize(string jsonData)
    {
      Hashtable hashtable = new Hashtable();
      string[] strArray = jsonData.Split(new char[5]{ '"', ',', '{', '}', ':' }, StringSplitOptions.RemoveEmptyEntries);
      string str1 = (string) null;
      foreach (string str2 in strArray)
      {
        if (str1 == null)
        {
          str1 = str2;
        }
        else
        {
          hashtable[(object) str1] = (object) str2;
          str1 = (string) null;
        }
      }
      return (object) hashtable;
    }

    private static string SerializeObject(object objectToSerialize)
    {
      if (objectToSerialize == null)
        return (string) null;
      Type type = objectToSerialize.GetType();
      string result;
      if (BasicJsonSerializer.TrySerializePrimitive(type, objectToSerialize, out result))
        return result;
      if (!type.IsClass)
        return (string) null;
      string str1 = "{";
      foreach (PropertyInfo property in type.GetProperties())
      {
        if (str1.Length > 1)
          str1 += ",";
        string str2 = BasicJsonSerializer.SerializeObject(property.GetValue(objectToSerialize, (object[]) null));
        str1 = str1 + "\"" + property.Name + "\":" + str2;
      }
      return str1 + "}";
    }

    private static bool TrySerializePrimitive(
      Type objectType,
      object objectToSerialize,
      out string result)
    {
      if (objectToSerialize is DateTime)
      {
        result = "\"" + ((DateTime) objectToSerialize).ToUniversalTime().ToString("o") + "\"";
        return true;
      }
      if (objectToSerialize is TimeSpan)
      {
        result = "\"" + ((TimeSpan) objectToSerialize).ToString() + "\"";
        return true;
      }
      if (objectType.IsEnum)
      {
        result = "\"" + (object) (int) objectToSerialize + "\"";
        return true;
      }
      if (objectToSerialize is string || objectToSerialize is Guid || objectToSerialize is int)
      {
        result = "\"" + objectToSerialize + "\"";
        return true;
      }
      if (objectToSerialize is IEnumerable)
      {
        result = BasicJsonSerializer.SerializeIEnumerable((IEnumerable) objectToSerialize);
        return true;
      }
      result = (string) null;
      return false;
    }

    private static string SerializeIEnumerable(IEnumerable enumerable)
    {
      StringBuilder stringBuilder = new StringBuilder("[");
      foreach (object objectToSerialize in enumerable)
      {
        if (stringBuilder.Length > 1)
          stringBuilder.Append(",");
        stringBuilder.Append(BasicJsonSerializer.SerializeObject(objectToSerialize));
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }
  }
}
