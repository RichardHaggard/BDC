// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.RequestPayload
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal abstract class RequestPayload
  {
    private const string SourceType = "Licenser";
    private readonly ProductInfo _productInfo;

    protected RequestPayload(System.Type componentType, string sessionId)
    {
      this._productInfo = ProductInfo.GetProductInfo(componentType);
      this.Source = "Licenser";
      this.TimeStamp = DateTime.UtcNow.ToString("o");
      this.SessionId = sessionId;
    }

    public string Type { get; set; }

    public string Source { get; set; }

    public string SessionId { get; set; }

    public string TimeStamp { get; set; }

    protected ProductInfo ProductInfo
    {
      get
      {
        return this._productInfo;
      }
    }
  }
}
