// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ProductInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls;

namespace Telerik.Licensing
{
  internal class ProductInfo
  {
    public ProductInfo(Type type)
    {
      this.Type = type;
      this.ProductType = this.ReadStatus();
      this.ProductName = this.SanitizeProductName(this.ReadProductName());
      this.Version = this.ReadVersion();
    }

    public ProductType ProductType { get; private set; }

    public string ProductName { get; private set; }

    public string Version { get; private set; }

    protected Type Type { get; set; }

    public static ProductInfo GetProductInfo(Type type)
    {
      return new ProductInfo(type);
    }

    private ProductType ReadStatus()
    {
      return RadControl.IsTrial ? ProductType.Trial : ProductType.Dev;
    }

    private string SanitizeProductName(string name)
    {
      if (name.IndexOf("design time", StringComparison.InvariantCultureIgnoreCase) >= 0)
        name = name.Replace("design time", string.Empty);
      return name.Trim();
    }

    private string ReadProductName()
    {
      return "Progress® Telerik® UI for WinForms";
    }

    private string ReadVersion()
    {
      return "2018.3.1016.20";
    }
  }
}
