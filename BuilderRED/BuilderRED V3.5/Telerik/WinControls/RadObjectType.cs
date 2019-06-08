// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadObjectType
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Telerik.WinControls
{
  public class RadObjectType
  {
    private static Hashtable DTypeFromCLRType = new Hashtable();
    private static int DTypeCount = 0;
    private static object _lock = new object();
    private RadObjectType _baseDType;
    private int _id;
    private Type _systemType;

    private RadObjectType()
    {
    }

    public static RadObjectType FromSystemType(Type systemType)
    {
      if ((object) systemType == null)
        throw new ArgumentNullException(nameof (systemType));
      if (!typeof (RadObject).IsAssignableFrom(systemType))
        throw new ArgumentException("DTypeNotSupportForSystemType");
      Queue encounteredTypes = (Queue) null;
      RadObjectType radObjectType;
      lock (RadObjectType._lock)
        radObjectType = RadObjectType.FromSystemTypeRecursive(systemType, ref encounteredTypes);
      while (encounteredTypes != null && encounteredTypes.Count > 0)
        RuntimeHelpers.RunClassConstructor((RuntimeTypeHandle) encounteredTypes.Dequeue());
      return radObjectType;
    }

    private static RadObjectType FromSystemTypeRecursive(
      Type systemType,
      ref Queue encounteredTypes)
    {
      RadObjectType radObjectType = (RadObjectType) RadObjectType.DTypeFromCLRType[(object) systemType];
      if (radObjectType == null)
      {
        if (encounteredTypes == null)
          encounteredTypes = new Queue(10);
        radObjectType = new RadObjectType();
        radObjectType._systemType = systemType;
        RadObjectType.DTypeFromCLRType[(object) systemType] = (object) radObjectType;
        if ((object) systemType != (object) typeof (RadObject))
          radObjectType._baseDType = RadObjectType.FromSystemTypeRecursive(systemType.BaseType, ref encounteredTypes);
        radObjectType._id = RadObjectType.DTypeCount++;
        encounteredTypes.Enqueue((object) systemType.TypeHandle);
      }
      return radObjectType;
    }

    public override int GetHashCode()
    {
      return this._id;
    }

    public bool IsInstanceOfType(RadObject radObject)
    {
      if (radObject != null)
      {
        RadObjectType radObjectType = radObject.RadObjectType;
        while (radObjectType.Id != this.Id)
        {
          radObjectType = radObjectType._baseDType;
          if (radObjectType == null)
            goto label_5;
        }
        return true;
      }
label_5:
      return false;
    }

    public bool IsSubclassOf(RadObjectType radObjectType)
    {
      if (radObjectType != null)
      {
        for (RadObjectType baseDtype = this._baseDType; baseDtype != null; baseDtype = baseDtype._baseDType)
        {
          if (baseDtype.Id == radObjectType.Id)
            return true;
        }
      }
      return false;
    }

    public RadObjectType BaseType
    {
      get
      {
        return this._baseDType;
      }
    }

    public int Id
    {
      get
      {
        return this._id;
      }
    }

    public string Name
    {
      get
      {
        return this.SystemType.Name;
      }
    }

    public Type SystemType
    {
      get
      {
        return this._systemType;
      }
    }

    internal RadProperty[] GetRadProperties()
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < RadProperty.RegisteredPropertyList.List.Length; ++index)
      {
        RadProperty radProperty = RadProperty.RegisteredPropertyList.List[index];
        if (radProperty != null && (object) radProperty.OwnerType == (object) this.SystemType)
          arrayList.Add((object) radProperty);
      }
      return (RadProperty[]) arrayList.ToArray(typeof (RadProperty));
    }
  }
}
