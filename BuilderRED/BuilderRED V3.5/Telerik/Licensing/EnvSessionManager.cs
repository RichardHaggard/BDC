// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.EnvSessionManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class EnvSessionManager : ISessionManager
  {
    private readonly EnvDTEInterop _dte;
    private readonly ISerializationService _service;
    private Session _session;

    public EnvSessionManager(EnvDTEInterop dte, ISerializationService service)
    {
      this._dte = dte;
      this._service = service;
    }

    public EnvDTEInterop Environment
    {
      get
      {
        return this._dte;
      }
    }

    protected ISerializationService SerializationService
    {
      get
      {
        return this._service;
      }
    }

    public Session GetSessionByName(SessionName name)
    {
      if (this._session == null)
        this._session = !this.Exists(name) ? this.Create(name) : this.Load(name);
      this.EnsureNotExpired(this._session);
      return this._session;
    }

    public Session GetCurrentSession()
    {
      return this.GetSessionByName(new SessionName(this.Environment.GetName(), false));
    }

    private void EnsureNotExpired(Session session)
    {
      if (!session.IsExpired())
        return;
      session.Reset();
    }

    public Session Create(SessionName name)
    {
      Session session = new Session();
      session.SetName(name);
      session.SessionChanged += new SessionChangedEventHandler(this.SessionChanged);
      return session;
    }

    public bool Exists(SessionName name)
    {
      if (this.Environment == null)
        return false;
      return this.Environment.GetViableExists((object) name.Name);
    }

    public void Save(Session session)
    {
      this.Environment.SetVariable((object) session.GetName().Name, (object) this.SerializationService.Serialize<Session>(session));
      this.Environment.SetVariablePerists((object) session.GetName().Name, false);
    }

    public Session Load(SessionName name)
    {
      Session session = this.SerializationService.Deserialize<Session>((string) this.Environment.GetVariable((object) name.Name));
      session.SetName(name);
      session.SessionChanged += new SessionChangedEventHandler(this.SessionChanged);
      return session;
    }

    private void SessionChanged(object sender, SessionChangedEventArgs e)
    {
      this.Save(e.Session);
    }
  }
}
