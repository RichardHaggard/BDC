// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadProperty
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class RadProperty
  {
    private bool? propertyTypeIsValueType = new bool?();
    public static readonly object UnsetValue = new object();
    internal static ItemStructList<RadProperty> RegisteredPropertyList = new ItemStructList<RadProperty>(384);
    private static Hashtable PropertyFromName = new Hashtable();
    internal static object Synchronized = new object();
    private static Type NullableType = typeof (Nullable<>);
    internal InsertionSortMap _metadataMap;
    private string _name;
    private Type _propertyType;
    private Type _ownerType;
    private RadPropertyMetadata _defaultMetadata;
    private ValidateValueCallback _validateValueCallback;
    private static int GlobalIndexCount;
    private int _globalIndex;
    private int nameHashCode;
    private RadProperty.FromNameKey key;

    private RadProperty(
      string name,
      Type propertyType,
      Type ownerType,
      RadPropertyMetadata defaultMetadata,
      ValidateValueCallback validateValueCallback)
    {
      this._metadataMap = new InsertionSortMap();
      this._name = name;
      this._propertyType = propertyType;
      this._ownerType = ownerType;
      this._defaultMetadata = defaultMetadata;
      this._validateValueCallback = validateValueCallback;
      lock (RadProperty.Synchronized)
      {
        this._globalIndex = RadProperty.GetUniqueGlobalIndex(ownerType, name);
        this.nameHashCode = name.GetHashCode();
        RadProperty.RegisteredPropertyList.Add(this);
      }
    }

    public override int GetHashCode()
    {
      return this._globalIndex;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    private static void RegisterParameterValidation(string name, Type propertyType, Type ownerType)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(nameof (name));
      if ((object) ownerType == null)
        throw new ArgumentNullException(nameof (ownerType));
      if ((object) propertyType == null)
        throw new ArgumentNullException(nameof (propertyType));
    }

    public static RadProperty Register(
      string name,
      Type propertyType,
      Type ownerType,
      RadPropertyMetadata typeMetadata)
    {
      return RadProperty.Register(name, propertyType, ownerType, typeMetadata, (ValidateValueCallback) null);
    }

    public static RadProperty Register(
      string name,
      Type propertyType,
      Type ownerType,
      RadPropertyMetadata typeMetadata,
      ValidateValueCallback validateValueCallback)
    {
      RadPropertyMetadata defaultMetadata = (RadPropertyMetadata) null;
      if (typeMetadata != null && typeMetadata.DefaultValueWasSet())
        defaultMetadata = new RadPropertyMetadata(typeMetadata.DefaultValue);
      RadProperty radProperty = RadProperty.RegisterCommon(name, propertyType, ownerType, AttachedPropertyUsage.Self, defaultMetadata, validateValueCallback);
      if (typeMetadata != null)
        radProperty.OverrideMetadata(ownerType, typeMetadata);
      return radProperty;
    }

    public static RadProperty RegisterAttached(
      string name,
      Type propertyType,
      Type ownerType,
      RadPropertyMetadata typeMetadata)
    {
      return RadProperty.RegisterAttached(name, propertyType, ownerType, typeMetadata, (ValidateValueCallback) null);
    }

    public static RadProperty RegisterAttached(
      string name,
      Type propertyType,
      Type ownerType,
      RadPropertyMetadata typeMetadata,
      ValidateValueCallback validateValueCallback)
    {
      RadProperty.RegisterParameterValidation(name, propertyType, ownerType);
      return RadProperty.RegisterCommon(name, propertyType, ownerType, AttachedPropertyUsage.Anywhere, typeMetadata, validateValueCallback);
    }

    public RadProperty AddOwner(Type ownerType)
    {
      return this.AddOwner(ownerType, (RadPropertyMetadata) null);
    }

    public RadProperty AddOwner(Type ownerType, RadPropertyMetadata typeMetadata)
    {
      if ((object) ownerType == null)
        throw new ArgumentNullException(nameof (ownerType));
      if (this._defaultMetadata.ReadOnly && this._defaultMetadata.AttachedPropertyUsage != AttachedPropertyUsage.Self)
        throw new InvalidOperationException(string.Format("Cannot Add Owner For Attached ReadOnly Property: {0}", new object[1]
        {
          (object) this.Name
        }));
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(this.Name, ownerType);
      lock (RadProperty.Synchronized)
      {
        if (RadProperty.PropertyFromName.Contains((object) fromNameKey))
          throw new ArgumentException(string.Format("Property Already Registered {0}, {1}", new object[2]
          {
            (object) this.Name,
            (object) ownerType.Name
          }));
      }
      if (typeMetadata == null && this._defaultMetadata.AttachedPropertyUsage != AttachedPropertyUsage.Self)
        typeMetadata = this._defaultMetadata.Copy(this);
      if (typeMetadata != null)
      {
        typeMetadata.SetAttachedPropertyUsage(AttachedPropertyUsage.Self);
        this.OverrideMetadata(ownerType, typeMetadata);
      }
      lock (RadProperty.Synchronized)
        RadProperty.PropertyFromName[(object) fromNameKey] = (object) this;
      return this;
    }

    public void OverrideMetadata(Type forType, RadPropertyMetadata typeMetadata)
    {
      RadObjectType dType;
      RadPropertyMetadata baseMetadata;
      this.SetupOverrideMetadata(forType, typeMetadata, out dType, out baseMetadata);
      if (baseMetadata.ReadOnly)
        throw new InvalidOperationException(string.Format("ReadOnlyOverrideNotAllowed {0}", new object[1]
        {
          (object) this.Name
        }));
      this.ProcessOverrideMetadata(forType, typeMetadata, dType, baseMetadata);
    }

    private void ProcessOverrideMetadata(
      Type forType,
      RadPropertyMetadata typeMetadata,
      RadObjectType dType,
      RadPropertyMetadata baseMetadata)
    {
      lock (RadProperty.Synchronized)
      {
        if (RadProperty.UnsetValue == this._metadataMap[dType.Id])
          this._metadataMap[dType.Id] = (object) typeMetadata;
        else
          throw new ArgumentException(string.Format("TypeMetadataAlreadyRegistered {0}", new object[1]
          {
            (object) forType.Name
          }));
      }
      typeMetadata.InvokeMerge(baseMetadata, this);
      typeMetadata.Seal(this, forType);
    }

    private void SetupOverrideMetadata(
      Type forType,
      RadPropertyMetadata typeMetadata,
      out RadObjectType dType,
      out RadPropertyMetadata baseMetadata)
    {
      if ((object) forType == null)
        throw new ArgumentNullException(nameof (forType));
      if (typeMetadata == null)
        throw new ArgumentNullException(nameof (typeMetadata));
      if (typeMetadata.Sealed)
        throw new ArgumentException(string.Format("TypeMetadataAlreadyInUse"));
      if (!typeof (RadObject).IsAssignableFrom(forType))
        throw new ArgumentException(string.Format("TypeMustBeRadObjectDerived {0}", new object[1]
        {
          (object) forType.Name
        }));
      if (typeMetadata.IsDefaultValueModified)
        RadProperty.ValidateMetadataDefaultValue(typeMetadata, this.PropertyType, this.ValidateValueCallback);
      dType = RadObjectType.FromSystemType(forType);
      baseMetadata = this.GetMetadata(dType.BaseType);
      if (!baseMetadata.GetType().IsAssignableFrom(typeMetadata.GetType()))
        throw new ArgumentException(string.Format("OverridingMetadataDoesNotMatchBaseMetadataType"));
    }

    private static void ValidateMetadataDefaultValue(
      RadPropertyMetadata defaultMetadata,
      Type propertyType,
      ValidateValueCallback validateValueCallback)
    {
    }

    public Type PropertyType
    {
      get
      {
        return this._propertyType;
      }
    }

    public bool PropertyTypeIsValueType
    {
      get
      {
        if (!this.propertyTypeIsValueType.HasValue || !this.propertyTypeIsValueType.HasValue)
          this.propertyTypeIsValueType = new bool?(this.PropertyType.IsValueType);
        return this.propertyTypeIsValueType.Value;
      }
    }

    public string Name
    {
      get
      {
        return this._name;
      }
    }

    public Type OwnerType
    {
      get
      {
        return this._ownerType;
      }
    }

    public bool IsValidValue(object value, RadObject instance)
    {
      if (!RadProperty.IsValidType(value, this.PropertyType))
        return false;
      if (this.ValidateValueCallback != null)
        return this.ValidateValueCallback(value, instance);
      return true;
    }

    public bool IsValidType(object value)
    {
      return RadProperty.IsValidType(value, this.PropertyType);
    }

    internal static bool IsValidType(object value, Type propertyType)
    {
      if (value == null)
      {
        if (propertyType.IsValueType && (!propertyType.IsGenericType || (object) propertyType.GetGenericTypeDefinition() != (object) RadProperty.NullableType))
          return false;
      }
      else if (!propertyType.IsInstanceOfType(value))
        return false;
      return true;
    }

    internal int NameHash
    {
      get
      {
        return this.nameHashCode;
      }
    }

    public ValidateValueCallback ValidateValueCallback
    {
      get
      {
        return this._validateValueCallback;
      }
    }

    internal static int GetUniqueGlobalIndex(Type ownerType, string name)
    {
      if (RadProperty.GlobalIndexCount < int.MaxValue)
        return RadProperty.GlobalIndexCount++;
      if ((object) ownerType != null)
        throw new InvalidOperationException("Too many RadProperties to create a new property: " + ownerType.Name + "." + name);
      throw new InvalidOperationException("Too many RadProperties to create a constant property");
    }

    private static RadProperty RegisterCommon(
      string name,
      Type propertyType,
      Type ownerType,
      AttachedPropertyUsage usage,
      RadPropertyMetadata defaultMetadata,
      ValidateValueCallback validateValueCallback)
    {
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(name, ownerType);
      lock (RadProperty.Synchronized)
      {
        if (RadProperty.PropertyFromName.Contains((object) fromNameKey))
          throw new ArgumentException(string.Format("Property already registered", new object[2]
          {
            (object) name,
            (object) ownerType.Name
          }));
      }
      if (defaultMetadata == null)
        defaultMetadata = RadProperty.AutoGeneratePropertyMetadata(propertyType, validateValueCallback, name, ownerType);
      else if (!defaultMetadata.DefaultValueWasSet())
        defaultMetadata.DefaultValue = RadProperty.AutoGenerateDefaultValue(propertyType);
      defaultMetadata.SetAttachedPropertyUsage(usage);
      RadProperty dp = new RadProperty(name, propertyType, ownerType, defaultMetadata, validateValueCallback);
      defaultMetadata.Seal(dp, (Type) null);
      lock (RadProperty.Synchronized)
        RadProperty.PropertyFromName[(object) fromNameKey] = (object) dp;
      return dp;
    }

    private static RadPropertyMetadata AutoGeneratePropertyMetadata(
      Type propertyType,
      ValidateValueCallback validateValueCallback,
      string name,
      Type ownerType)
    {
      return new RadPropertyMetadata(RadProperty.AutoGenerateDefaultValue(propertyType));
    }

    private static object AutoGenerateDefaultValue(Type propertyType)
    {
      object obj = (object) null;
      if (propertyType.IsValueType)
        obj = Activator.CreateInstance(propertyType);
      return obj;
    }

    public RadPropertyMetadata GetMetadata(RadObject radObject)
    {
      if (radObject == null)
        throw new ArgumentNullException(nameof (radObject));
      return this.GetMetadata(radObject.RadObjectType);
    }

    public RadPropertyMetadata GetMetadata(RadObjectType radObjectType)
    {
      if (radObjectType != null)
      {
        int index = this._metadataMap.Count - 1;
        if (index < 0)
          return this._defaultMetadata;
        if (index == 0)
        {
          int key;
          object obj;
          this._metadataMap.GetKeyValuePair(index, out key, out obj);
          while (radObjectType.Id > key)
            radObjectType = radObjectType.BaseType;
          if (key == radObjectType.Id)
            return (RadPropertyMetadata) obj;
        }
        else if (radObjectType.Id != 0)
        {
          do
          {
            int key;
            object obj;
            this._metadataMap.GetKeyValuePair(index, out key, out obj);
            for (--index; radObjectType.Id < key && index >= 0; --index)
              this._metadataMap.GetKeyValuePair(index, out key, out obj);
            while (radObjectType.Id > key)
              radObjectType = radObjectType.BaseType;
            if (key == radObjectType.Id)
              return (RadPropertyMetadata) obj;
          }
          while (index >= 0);
        }
      }
      return this._defaultMetadata;
    }

    internal RadPropertyMetadata GetMetadata(RadObjectType type, out bool found)
    {
      RadPropertyMetadata metadata = this.GetMetadata(type);
      found = metadata != this._defaultMetadata;
      return metadata;
    }

    public int GlobalIndex
    {
      get
      {
        return this._globalIndex;
      }
    }

    public static RadProperty Find(string className, string propertyName)
    {
      RadProperty safe = RadProperty.FindSafe(className, propertyName);
      if (safe == null)
        throw new RadPropertyNotFoundException(propertyName, className);
      return safe;
    }

    public static RadProperty FindSafe(string className, string propertyName)
    {
      Type typeByName = RadTypeResolver.Instance.GetTypeByName(className, false);
      if ((object) typeByName == null)
        return (RadProperty) null;
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(propertyName, typeByName);
      lock (RadProperty.Synchronized)
      {
        if (RadProperty.PropertyFromName.Contains((object) fromNameKey))
          return (RadProperty) RadProperty.PropertyFromName[(object) fromNameKey];
        return (RadProperty) null;
      }
    }

    public static RadProperty FindSafe(Type objectType, string propertyName)
    {
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(propertyName, objectType);
      lock (RadProperty.Synchronized)
      {
        if (RadProperty.PropertyFromName.Contains((object) fromNameKey))
          return (RadProperty) RadProperty.PropertyFromName[(object) fromNameKey];
        return (RadProperty) null;
      }
    }

    public static RadProperty Find(Type objectType, string propertyName)
    {
      RadProperty safe = RadProperty.FindSafe(objectType, propertyName);
      if (safe == null)
        throw new RadPropertyNotFoundException(propertyName, objectType.FullName);
      return safe;
    }

    public string FullName
    {
      get
      {
        return this.OwnerType.FullName + "." + this.Name;
      }
    }

    internal RadProperty.FromNameKey PropertyKey
    {
      get
      {
        if (this.key == null)
          this.key = new RadProperty.FromNameKey(this.Name, this.OwnerType);
        return this.key;
      }
    }

    public PropertyDescriptor FindClrProperty()
    {
      return TypeDescriptor.GetProperties(this.OwnerType).Find(this.Name, false);
    }

    internal class FromNameKey
    {
      private int _hashCode;
      private string _name;
      private Type _ownerType;

      public FromNameKey(string name, Type ownerType)
      {
        this._name = name;
        this._ownerType = ownerType;
        this._hashCode = this._name.GetHashCode() ^ this._ownerType.GetHashCode();
      }

      public override bool Equals(object o)
      {
        if (o != null && o is RadProperty.FromNameKey)
          return this.Equals((RadProperty.FromNameKey) o);
        return false;
      }

      public bool Equals(RadProperty.FromNameKey key)
      {
        if (this._name.Equals(key._name))
          return (object) this._ownerType == (object) key._ownerType;
        return false;
      }

      public override int GetHashCode()
      {
        return this._hashCode;
      }

      public void UpdateNameKey(Type ownerType)
      {
        this._ownerType = ownerType;
        this._hashCode = this._name.GetHashCode() ^ this._ownerType.GetHashCode();
      }

      public override string ToString()
      {
        return this._ownerType.FullName + "." + this._name;
      }
    }
  }
}
