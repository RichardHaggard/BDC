// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.MetricsClient
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Telerik.Licensing
{
  internal class MetricsClient : Client
  {
    private readonly string _accessToken;
    private WebRequest _webRequest;

    public MetricsClient(Config config, ISerializationService service, string accessToken)
      : base(config, service)
    {
      this._accessToken = accessToken;
    }

    protected override HttpWebRequest WebClient
    {
      get
      {
        if (this._webRequest == null)
          this._webRequest = this.InitializeRequest(this.Config.MetricsEndpoint);
        return (HttpWebRequest) this._webRequest;
      }
    }

    protected string AccessToken
    {
      get
      {
        return this._accessToken;
      }
    }

    public override void Post(object payload)
    {
      this.Worker.RunWorkerAsync(payload);
    }

    protected override void DoWork(object sender, DoWorkEventArgs e)
    {
      object obj = e.Argument;
      HttpWebRequest webClient = this.WebClient;
      using (StreamWriter streamWriter = new StreamWriter(webClient.GetRequestStream()))
        streamWriter.Write(this.Serialization.SerializeToJson<object>(obj));
      using (HttpWebResponse response = (HttpWebResponse) webClient.GetResponse())
        e.Result = (object) response.StatusCode.ToString();
    }

    protected override WebRequest InitializeRequest(Uri endpoint)
    {
      WebRequest webRequest = base.InitializeRequest(endpoint);
      webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + this.AccessToken;
      return webRequest;
    }
  }
}
