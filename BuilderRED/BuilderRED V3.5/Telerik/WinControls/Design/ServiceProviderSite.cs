// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.ServiceProviderSite
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.Design
{
  public class ServiceProviderSite : ISite, IServiceProvider, IContainer, IDisposable
  {
    private IServiceProvider serviceProvider;
    private string name;

    public ServiceProviderSite(IServiceProvider provider)
    {
      this.serviceProvider = provider;
    }

    public void Add(IComponent component, string name)
    {
      throw new NotImplementedException();
    }

    public void Add(IComponent component)
    {
      throw new NotImplementedException();
    }

    public ComponentCollection Components
    {
      get
      {
        return new ComponentCollection(new IComponent[0]);
      }
    }

    public void Remove(IComponent component)
    {
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public IComponent Component
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public IContainer Container
    {
      get
      {
        return (IContainer) this;
      }
    }

    public bool DesignMode
    {
      get
      {
        return false;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public object GetService(Type serviceType)
    {
      return this.serviceProvider.GetService(serviceType);
    }
  }
}
