// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.TelerikLicenseProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
  public class TelerikLicenseProvider : LicenseProvider, ILicenseProvider
  {
    private ISessionManager _manager;
    private IUsageTracker _usageTracker;
    private Session _session;

    public TelerikLicenseProvider()
    {
    }

    internal TelerikLicenseProvider(ISessionManagerFactory factory)
    {
      this._manager = factory.TryCreateManager();
    }

    private event ProductUsedEventHandler ProductUsed;

    event ProductUsedEventHandler ILicenseProvider.ProductUsed
    {
      add
      {
        this.ProductUsed += value;
      }
      remove
      {
        this.ProductUsed -= value;
      }
    }

    private event ComponentUsedEventHandler ComponentUsed;

    event ComponentUsedEventHandler ILicenseProvider.ComponentUsed
    {
      add
      {
        this.ComponentUsed += value;
      }
      remove
      {
        this.ComponentUsed -= value;
      }
    }

    internal TypesCollection RegisteredTypes
    {
      get
      {
        return this.CurrentSession.Components;
      }
    }

    internal virtual IUsageTracker UsageTracker
    {
      get
      {
        if (this._usageTracker == null)
          this._usageTracker = (IUsageTracker) new Telerik.Licensing.UsageTracker((ILicenseProvider) this, (ITransportService) new TransportService(Config.GetInstance()));
        return this._usageTracker;
      }
    }

    internal virtual Session CurrentSession
    {
      get
      {
        if (this._session == null)
          this._session = this._manager.GetCurrentSession();
        return this._session;
      }
    }

    public override License GetLicense(
      LicenseContext context,
      Type type,
      object instance,
      bool allowExceptions)
    {
      ILicenseKey key = (ILicenseKey) new DefaultKey();
      try
      {
        LicenseContextManager licenseContextManager = new LicenseContextManager((ILicenseContextData) new LicenseContextData(context, type, allowExceptions));
        switch (context.UsageMode)
        {
          case LicenseUsageMode.Designtime:
            key = (ILicenseKey) new DesignTimeKey();
            licenseContextManager.SaveLicenseKey(type, key);
            this.ProcessEvents(licenseContextManager.ContextData, type, key);
            break;
          default:
            key = licenseContextManager.ExtractLicenseKey(type);
            break;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.UsageTracker.StopTracking();
      }
      return LicenseFactory.CreateLicense(key);
    }

    internal virtual void ProcessEvents(ILicenseContextData data, Type type, ILicenseKey key)
    {
      this.EnsureTracking();
      if (!this.TryEnsureSessionManager(data.Context))
        return;
      this.RegisteredTypes.TryAdd(type.FullName);
      if (!this.CurrentSession.GetProductUsageLogged())
      {
        this.RaiseProductUsed(type, this.CurrentSession.Id);
        this.CurrentSession.SetProductUsageLogged();
      }
      if (!this.CurrentSession.GetHasPendingChange())
        return;
      this.RaiseComponentUsed(type, this.CurrentSession.Id);
      this.CurrentSession.SetPendingChangeResolved();
    }

    protected virtual bool TryEnsureSessionManager(LicenseContext context)
    {
      if (this._manager == null)
        this._manager = new EnvSessionManagerFactory((IServiceProvider) context).TryCreateManager();
      return this._manager != null;
    }

    private void EnsureTracking()
    {
      if (this.UsageTracker.IsTracking())
        return;
      this.UsageTracker.StartTracking();
    }

    private void RaiseProductUsed(Type type, string sessionId)
    {
      ProductUsedEventHandler productUsed = this.ProductUsed;
      if (productUsed == null)
        return;
      productUsed((object) this, new ProductUsedEventArgs(type, sessionId));
    }

    private void RaiseComponentUsed(Type type, string sessionId)
    {
      ComponentUsedEventHandler componentUsed = this.ComponentUsed;
      if (componentUsed == null)
        return;
      componentUsed((object) this, new ComponentUsedEventArgs(type, sessionId));
    }
  }
}
