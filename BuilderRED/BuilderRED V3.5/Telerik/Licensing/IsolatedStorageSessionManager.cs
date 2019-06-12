// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.IsolatedStorageSessionManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Telerik.Licensing
{
  internal class IsolatedStorageSessionManager : ISessionManager
  {
    private static readonly object isoStoreLock = new object();
    private readonly BasicJsonSerializer _serializer = new BasicJsonSerializer();
    private static IsolatedStorageFile isoStore;
    private Session _session;

    public IsolatedStorageSessionManager()
    {
      this.CleanupOldSessions();
    }

    public SessionName SessionName { get; private set; }

    public IsolatedStorageFile IsoStore
    {
      get
      {
        if (IsolatedStorageSessionManager.isoStore == null)
        {
          lock (IsolatedStorageSessionManager.isoStoreLock)
          {
            if (IsolatedStorageSessionManager.isoStore == null)
              IsolatedStorageSessionManager.isoStore = IsolatedStorageFile.GetUserStoreForDomain();
          }
        }
        return IsolatedStorageSessionManager.isoStore;
      }
    }

    public virtual Session Create(SessionName name)
    {
      Session session = new Session();
      session.SetName(name);
      session.SessionChanged += new SessionChangedEventHandler(this.SessionChanged);
      return session;
    }

    public virtual bool Exists(SessionName name)
    {
      foreach (string fileName in this.IsoStore.GetFileNames("*"))
      {
        if (string.Equals(fileName, name.Name, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      return false;
    }

    public virtual void Save(Session session)
    {
      using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(this.SessionName.Name, FileMode.OpenOrCreate))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) storageFileStream))
          streamWriter.WriteLine(this._serializer.Serialize((object) session));
      }
      session.SetPendingChangeResolved();
    }

    public virtual Session Load(SessionName name)
    {
      using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(this.SessionName.Name, FileMode.Open))
      {
        using (StreamReader streamReader = new StreamReader((Stream) storageFileStream))
        {
          Session session = (Session) this._serializer.Deserialize(streamReader.ReadToEnd());
          session.SetProductUsageLogged();
          return session;
        }
      }
    }

    public Session GetSessionByName(SessionName name)
    {
      if (this._session == null)
        this._session = !this.Exists(name) ? this.Create(name) : this.Load(name);
      return this._session;
    }

    public Session GetCurrentSession()
    {
      throw new NotImplementedException();
    }

    protected void CleanupOldSessions()
    {
      foreach (string fileName in this.IsoStore.GetFileNames("Telerik*.json"))
      {
        try
        {
          if (this.Load(SessionName.FromExisting(fileName)).IsExpired())
            this.IsoStore.DeleteFile(fileName);
        }
        catch
        {
        }
      }
    }

    private void SessionChanged(object sender, SessionChangedEventArgs e)
    {
      this.Save(e.Session);
    }
  }
}
