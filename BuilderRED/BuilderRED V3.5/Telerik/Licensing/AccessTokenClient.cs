// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.AccessTokenClient
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Telerik.Licensing
{
  internal class AccessTokenClient : Client
  {
    private WebRequest _webRequest;

    public AccessTokenClient(Config config, ISerializationService service)
      : base(config, service)
    {
    }

    protected override HttpWebRequest WebClient
    {
      get
      {
        if (this._webRequest == null)
          this._webRequest = this.InitializeRequest(this.Config.TokenEndpoint);
        return (HttpWebRequest) this._webRequest;
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
        streamWriter.Write(this.Serialization.Serialize<object>(obj));
      using (WebResponse response = webClient.GetResponse())
      {
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
          string end = streamReader.ReadToEnd();
          e.Result = (object) this.Serialization.Deserialize<AccessTokenResponse>(end).Access_Token;
        }
      }
    }

    protected override WebRequest InitializeRequest(Uri endpoint)
    {
      WebRequest webRequest = base.InitializeRequest(endpoint);
      string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", (object) Uri.EscapeDataString(this.Config.ClientId), (object) this.Config.ClientSecret)));
      webRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + base64String;
      return webRequest;
    }
  }
}
