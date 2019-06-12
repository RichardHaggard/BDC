// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.EnvSessionManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Telerik.Licensing
{
  internal class EnvSessionManagerFactory : ISessionManagerFactory
  {
    private static readonly object managerLock = new object();
    private static EnvSessionManager manager;
    private static IServiceProvider provider;

    public EnvSessionManagerFactory()
      : this((IServiceProvider) null)
    {
    }

    public EnvSessionManagerFactory(IServiceProvider serviceProvider)
    {
      EnvSessionManagerFactory.provider = serviceProvider;
    }

    public ISessionManager TryCreateManager()
    {
      return (ISessionManager) this.GetInstance();
    }

    private EnvSessionManager GetInstance()
    {
      if (EnvSessionManagerFactory.manager == null)
      {
        lock (EnvSessionManagerFactory.managerLock)
        {
          if (EnvSessionManagerFactory.manager == null)
          {
            object envObject = this.GetEnvObject(EnvSessionManagerFactory.provider);
            EnvSessionManagerFactory.manager = envObject != null ? new EnvSessionManager(new EnvDTEInterop(envObject), SerializationService.GetInstance()) : (EnvSessionManager) null;
          }
        }
      }
      return EnvSessionManagerFactory.manager;
    }

    private object GetEnvObject(IServiceProvider provider)
    {
      object envdte;
      if (this.TryGetEnvDteFromService(provider, out envdte))
        return envdte;
      string format = "VisualStudio.DTE.{0}.0";
      int num1 = 10;
      int num2 = 20;
      for (int index = num1; index < num2; ++index)
      {
        if (this.TryMarshalEnvDte(string.Format(format, (object) index), out envdte))
          return envdte;
      }
      return (object) null;
    }

    private bool TryGetEnvDteFromService(IServiceProvider provider, out object envdte)
    {
      envdte = (object) null;
      try
      {
        Type type = Assembly.Load("envdte, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").GetType("EnvDTE.DTE");
        envdte = provider.GetType().InvokeMember("GetService", BindingFlags.InvokeMethod, (Binder) null, (object) provider, new object[1]
        {
          (object) type
        });
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private bool TryMarshalEnvDte(string name, out object envdte)
    {
      envdte = (object) null;
      try
      {
        envdte = Marshal.GetActiveObject(name);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
