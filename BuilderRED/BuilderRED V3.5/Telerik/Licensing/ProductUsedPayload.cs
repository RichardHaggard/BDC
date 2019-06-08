// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ProductUsedPayload
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class ProductUsedPayload : RequestPayload
  {
    private const string EventType = "ProductUsed";

    public ProductUsedPayload(Type type, string machineId, string sessionId)
      : base(type, sessionId)
    {
      this.InitializeProductData(this.ProductInfo);
      this.MachineId = machineId;
      this.Type = "ProductUsed";
      this.ProductType = this.ProductInfo.ProductType;
    }

    public string MachineId { get; set; }

    public string ProductName { get; set; }

    public string ProductVersion { get; set; }

    public string ProductCode { get; set; }

    public ProductType ProductType { get; set; }

    private void InitializeProductData(ProductInfo info)
    {
      this.ProductName = info.ProductName;
      this.ProductVersion = info.Version;
      this.ProductCode = "RCWF";
    }
  }
}
