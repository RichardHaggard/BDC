// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.UsageTracker
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class UsageTracker : IUsageTracker
  {
    private bool _isTracking;

    public UsageTracker(ILicenseProvider provider, ITransportService transportService)
    {
      this.LicenseProvider = provider;
      this.TransportService = transportService;
    }

    private ITransportService TransportService { get; set; }

    private ILicenseProvider LicenseProvider { get; set; }

    public virtual void Track(RequestPayload payload)
    {
      this.TransportService.CallHome(payload);
    }

    public void StartTracking()
    {
      if (this._isTracking)
        return;
      this.LicenseProvider.ProductUsed += new ProductUsedEventHandler(this.ProductUsed);
      this.LicenseProvider.ComponentUsed += new ComponentUsedEventHandler(this.ComponentUsed);
      this._isTracking = true;
    }

    public bool IsTracking()
    {
      return this._isTracking;
    }

    public void StopTracking()
    {
      this._isTracking = false;
      this.LicenseProvider.ProductUsed -= new ProductUsedEventHandler(this.ProductUsed);
      this.LicenseProvider.ComponentUsed -= new ComponentUsedEventHandler(this.ComponentUsed);
    }

    private void ProductUsed(object sender, ProductUsedEventArgs e)
    {
      this.Track((RequestPayload) new ProductUsedPayload(e.Type, UniqueMachineId.GetIdWithDefaultHash(), e.SessionId));
    }

    private void ComponentUsed(object sender, ComponentUsedEventArgs e)
    {
      this.Track((RequestPayload) new ComponentUsedPayload(e.Type, UniqueMachineId.GetIdWithDefaultHash(), e.SessionId));
    }
  }
}
