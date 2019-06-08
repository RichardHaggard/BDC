// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Config
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Telerik.Licensing
{
  internal class Config
  {
    private static readonly object configLock = new object();
    private static Config config;

    public Uri TokenEndpoint { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public Uri MetricsEndpoint { get; set; }

    public static Config GetInstance()
    {
      Config.EnsureInitialized();
      return Config.config;
    }

    private static void EnsureInitialized()
    {
      if (Config.config != null)
        return;
      lock (Config.configLock)
      {
        if (Config.config == null)
          Config.config = new Config()
          {
            TokenEndpoint = new Uri("https://identity.telerik.com/v2/oauth/telerik/token"),
            ClientId = "uri:client.licenser",
            ClientSecret = "597f2d644c3ad29c2058fe08e477eeb5",
            MetricsEndpoint = new Uri("https://dle.telerik.com/metrics/v1/events/callhome")
          };
        ServicePoint servicePoint1 = ServicePointManager.FindServicePoint(Config.config.TokenEndpoint);
        ServicePoint servicePoint2 = ServicePointManager.FindServicePoint(Config.config.MetricsEndpoint);
        servicePoint1.UseNagleAlgorithm = false;
        servicePoint1.Expect100Continue = false;
        servicePoint2.UseNagleAlgorithm = false;
        servicePoint2.Expect100Continue = false;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Consts
    {
      public const string TokenEndpoint = "https://identity.telerik.com/v2/oauth/telerik/token";
      public const string ClientId = "uri:client.licenser";
      public const string ClientSecret = "597f2d644c3ad29c2058fe08e477eeb5";
      public const string MetricsEndpoint = "https://dle.telerik.com/metrics/v1/events/callhome";
    }
  }
}
