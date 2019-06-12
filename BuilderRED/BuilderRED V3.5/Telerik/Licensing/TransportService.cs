// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.TransportService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Net;

namespace Telerik.Licensing
{
  internal class TransportService : ITransportService
  {
    private static readonly object accessTokenLock = new object();
    private static string accessToken;
    private readonly Config _config;

    public TransportService(Config config)
    {
      this._config = config;
    }

    protected Config Config
    {
      get
      {
        return this._config;
      }
    }

    protected string AccessToken
    {
      get
      {
        return TransportService.accessToken;
      }
    }

    public virtual void CallHome(RequestPayload data)
    {
      if (string.IsNullOrEmpty(this.AccessToken))
        this.EnsureAccessToken(new TransportService.Action<RequestPayload>(this.RequestMetrics), data);
      else
        this.RequestMetrics(data);
    }

    protected virtual Client GetAccessTokenClient()
    {
      return (Client) new AccessTokenClient(this.Config, SerializationService.GetInstance());
    }

    protected virtual Client GetMetricsClient(string token)
    {
      return (Client) new MetricsClient(this.Config, SerializationService.GetInstance(), token);
    }

    private void RequestMetrics(RequestPayload data)
    {
      Client metricsClient = this.GetMetricsClient(this.AccessToken);
      metricsClient.RequestCompleted += (RunWorkerCompletedEventHandler) ((metricsSender, metricsArgs) => {});
      metricsClient.Post((object) data);
    }

    private void EnsureAccessToken(
      TransportService.Action<RequestPayload> doneCallback,
      RequestPayload data)
    {
      Client accessTokenClient = this.GetAccessTokenClient();
      accessTokenClient.RequestCompleted += (RunWorkerCompletedEventHandler) ((tokenSender, tokenArgs) =>
      {
        if (tokenArgs.Error != null)
          return;
        if (TransportService.accessToken == null)
        {
          lock (TransportService.accessTokenLock)
          {
            if (TransportService.accessToken == null)
              TransportService.accessToken = (string) tokenArgs.Result;
          }
        }
        doneCallback(data);
      });
      accessTokenClient.Post((object) new AccessTokenPayload());
    }

    private bool CheckIsAccessUnauthorized(Exception error)
    {
      if (error is WebException)
      {
        HttpWebResponse response = ((WebException) error).Response as HttpWebResponse;
        if (response != null && response.StatusCode == HttpStatusCode.Unauthorized)
          return true;
      }
      return false;
    }

    public delegate void Action<T>(T t);
  }
}
