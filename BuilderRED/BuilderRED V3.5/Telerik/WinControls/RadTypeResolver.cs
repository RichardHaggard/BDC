// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadTypeResolver
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  public sealed class RadTypeResolver
  {
    private static object syncRoot = new object();
    private string typeResolverAssemblyName = "Telerik";
    private const string TelerikName = "Telerik";
    private Dictionary<int, Type> loadedTypes;
    private Dictionary<int, Type> knownTypes;
    private List<RadTypeResolver.LoadedAssembly> loadedAssemblies;
    private int lastParsedAssemblyCount;
    private static RadTypeResolver instance;
    private Version typeResolverAssemblyVersion;
    private bool resolveTypesInCurrentAssembly;

    private RadTypeResolver()
    {
      this.loadedAssemblies = new List<RadTypeResolver.LoadedAssembly>(20);
      this.knownTypes = new Dictionary<int, Type>(30);
      this.loadedTypes = new Dictionary<int, Type>(20);
      this.InitializeKnownTypes();
    }

    public void RegisterKnownType(string className, Type type)
    {
      lock (RadTypeResolver.syncRoot)
      {
        int hashCode = className.GetHashCode();
        if (this.knownTypes.ContainsKey(hashCode))
          return;
        this.knownTypes.Add(hashCode, type);
      }
    }

    public Type GetTypeByName(string className)
    {
      return this.GetTypeByName(className, true);
    }

    public Type GetTypeByName(string className, bool throwOnError)
    {
      return this.GetTypeByName(className, throwOnError, true);
    }

    public Type GetTypeByName(
      string className,
      bool throwOnError,
      bool onlyInTelerikAssemblies)
    {
      Type type = (Type) null;
      lock (RadTypeResolver.syncRoot)
        type = this.FindTypeByName(className, onlyInTelerikAssemblies);
      if ((object) type == null && throwOnError)
        throw new ArgumentException("Type not found: " + className + ". Please make sure you have reference to an assembly which contains type: " + className);
      return type;
    }

    public RadProperty GetRegisteredRadProperty(Type radOjectType, string propertyName)
    {
      RadProperty radProperty = (RadProperty) null;
      Type objectType = radOjectType;
      for (Type type = typeof (object); (object) objectType != (object) type; objectType = objectType.BaseType)
      {
        radProperty = RadProperty.FindSafe(objectType, propertyName);
        if (radProperty != null)
          break;
      }
      return radProperty;
    }

    public RadProperty GetRegisteredRadPropertyFromFullName(string propertyFullName)
    {
      RadProperty radProperty = (RadProperty) null;
      if (propertyFullName != string.Empty)
      {
        string[] strArray = propertyFullName.Split('.');
        if (strArray.Length <= 1)
          throw new Exception("Invalid property parts");
        string propertyName = strArray[strArray.Length - 1];
        radProperty = this.GetRegisteredRadProperty(this.GetTypeByName(string.Join(".", strArray, 0, strArray.Length - 1)), propertyName);
      }
      return radProperty;
    }

    public bool IsTelerikAssembly(Assembly asm)
    {
      if ((object) asm == null)
        return false;
      if (this.resolveTypesInCurrentAssembly)
        return (object) asm == (object) Assembly.GetCallingAssembly();
      Version version = this.typeResolverAssemblyVersion;
      if (version == (Version) null)
        version = Assembly.GetExecutingAssembly().GetName().Version;
      try
      {
        if (asm.FullName.Contains(this.typeResolverAssemblyName))
        {
          if (asm.GetName().Version == version)
            return true;
        }
      }
      catch
      {
      }
      AssemblyName[] referencedAssemblies = asm.GetReferencedAssemblies();
      for (int index = 0; index < referencedAssemblies.Length; ++index)
      {
        try
        {
          if (referencedAssemblies[index].FullName.Contains(this.typeResolverAssemblyName))
          {
            if (referencedAssemblies[index].Version == version)
              return true;
          }
        }
        catch
        {
        }
      }
      return false;
    }

    private void InitializeKnownTypes()
    {
      lock (RadTypeResolver.syncRoot)
      {
        this.knownTypes.Add("Telerik.WinControls.Primitives.FillPrimitive".GetHashCode(), typeof (FillPrimitive));
        this.knownTypes.Add("Telerik.WinControls.VisualElement".GetHashCode(), typeof (VisualElement));
        this.knownTypes.Add("Telerik.WinControls.RadElement".GetHashCode(), typeof (RadElement));
        this.knownTypes.Add("Telerik.WinControls.Primitives.BorderPrimitive".GetHashCode(), typeof (BorderPrimitive));
        this.knownTypes.Add("Telerik.WinControls.RadItem".GetHashCode(), typeof (RadItem));
        this.knownTypes.Add("Telerik.WinControls.Primitives.TextPrimitive".GetHashCode(), typeof (TextPrimitive));
        this.knownTypes.Add("Telerik.WinControls.RoundRectShape".GetHashCode(), typeof (RoundRectShape));
      }
    }

    private Type GetKnownType(string className)
    {
      Type type;
      this.knownTypes.TryGetValue(className.GetHashCode(), out type);
      return type;
    }

    private void BuildLoadedAssemblies(Assembly[] asmArray)
    {
      this.loadedAssemblies.Clear();
      int length = asmArray.Length;
      for (int index = 0; index < length; ++index)
      {
        Assembly asm = asmArray[index];
        this.loadedAssemblies.Add(new RadTypeResolver.LoadedAssembly(asm, this.IsTelerikAssembly(asm)));
      }
      this.lastParsedAssemblyCount = length;
    }

    private Type FindTypeByNameInAllAssemblies(string className)
    {
      return this.FindTypeByName(className, false);
    }

    private Type FindTypeByName(string className, bool onlyInTelerikAssemblies)
    {
      Type knownType = this.GetKnownType(className);
      if ((object) knownType != null)
      {
        RuntimeHelpers.RunClassConstructor(knownType.TypeHandle);
        return knownType;
      }
      Type type;
      this.loadedTypes.TryGetValue(className.GetHashCode(), out type);
      if ((object) type != null)
        return type;
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      if (assemblies.Length != this.lastParsedAssemblyCount)
        this.BuildLoadedAssemblies(assemblies);
      for (int index = 0; index < this.loadedAssemblies.Count; ++index)
      {
        RadTypeResolver.LoadedAssembly loadedAssembly = this.loadedAssemblies[index];
        if (!onlyInTelerikAssemblies || loadedAssembly.isTelerik)
        {
          type = loadedAssembly.assembly.GetType(className, false, false);
          if ((object) type != null)
            break;
        }
      }
      if ((object) type != null)
      {
        this.loadedTypes.Add(className.GetHashCode(), type);
        RuntimeHelpers.RunClassConstructor(type.TypeHandle);
      }
      return type;
    }

    internal List<RadTypeResolver.LoadedAssembly> LoadedAssemblies
    {
      get
      {
        if (this.loadedAssemblies.Count == 0)
          this.FindTypeByNameInAllAssemblies("RadControl");
        return this.loadedAssemblies;
      }
    }

    public bool ResolveTypesInCurrentAssembly
    {
      get
      {
        return this.resolveTypesInCurrentAssembly;
      }
      set
      {
        this.resolveTypesInCurrentAssembly = value;
      }
    }

    public string TypeResolverAssemblyName
    {
      get
      {
        return this.typeResolverAssemblyName;
      }
      set
      {
        this.typeResolverAssemblyName = value;
      }
    }

    public Version TypeResolverAssemblyVersion
    {
      get
      {
        return this.typeResolverAssemblyVersion;
      }
      set
      {
        this.typeResolverAssemblyVersion = value;
      }
    }

    public static RadTypeResolver Instance
    {
      get
      {
        if (RadTypeResolver.instance == null)
        {
          lock (RadTypeResolver.syncRoot)
          {
            if (RadTypeResolver.instance == null)
              RadTypeResolver.instance = new RadTypeResolver();
          }
        }
        return RadTypeResolver.instance;
      }
    }

    internal struct LoadedAssembly
    {
      public Assembly assembly;
      public bool isTelerik;

      public LoadedAssembly(Assembly assembly, bool isTelerik)
      {
        this.assembly = assembly;
        this.isTelerik = isTelerik;
      }

      public override int GetHashCode()
      {
        return this.assembly.FullName.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        RadTypeResolver.LoadedAssembly loadedAssembly = (RadTypeResolver.LoadedAssembly) obj;
        if ((object) loadedAssembly.assembly == (object) this.assembly)
          return loadedAssembly.isTelerik == this.isTelerik;
        return false;
      }
    }
  }
}
