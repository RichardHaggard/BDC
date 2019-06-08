// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Client
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Net;

namespace Telerik.Licensing
{
  internal abstract class Client : IDisposable
  {
    protected const string JsonContentType = "application/json";
    private readonly ISerializationService _serializationService;
    private readonly Config _config;
    private BackgroundWorker _worker;

    protected Client(Config config, ISerializationService serializationService)
    {
      this._serializationService = serializationService;
      this._config = config;
      this._worker = new BackgroundWorker();
      this._worker.DoWork += new DoWorkEventHandler(this.DoWork);
      this._worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
    }

    public event RunWorkerCompletedEventHandler RequestCompleted;

    protected BackgroundWorker Worker
    {
      get
      {
        return this._worker;
      }
    }

    protected Config Config
    {
      get
      {
        return this._config;
      }
    }

    protected ISerializationService Serialization
    {
      get
      {
        return this._serializationService;
      }
    }

    protected abstract HttpWebRequest WebClient { get; }

    public abstract void Post(object payload);

    public void Dispose()
    {
      this.Dispose(true);
    }

    protected abstract void DoWork(object sender, DoWorkEventArgs e);

    protected virtual WebRequest InitializeRequest(Uri endpoint)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(endpoint);
      httpWebRequest.Method = "POST";
      httpWebRequest.Accept = "application/json";
      httpWebRequest.ContentType = "application/json";
      return (WebRequest) httpWebRequest;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.Worker == null)
        return;
      this._worker.Dispose();
      this._worker = (BackgroundWorker) null;
    }

    protected void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (this.RequestCompleted == null)
        return;
      this.RequestCompleted(sender, e);
    }
  }
}
