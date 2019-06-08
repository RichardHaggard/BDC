// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Session
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal class Session
  {
    private SessionName _name;
    private TypesCollection _types;
    private bool _productUsageLogged;
    private bool _hasNewItem;

    public Session()
    {
      this.Id = Guid.NewGuid().ToString();
      this.Start = DateTime.Now;
      this.Timeout = TimeSpan.FromHours(24.0);
      this.Components = new TypesCollection();
    }

    public event SessionChangedEventHandler SessionChanged;

    public string Id { get; set; }

    public DateTime Start { get; set; }

    public TimeSpan Timeout { get; set; }

    public TypesCollection Components
    {
      get
      {
        return this._types;
      }
      set
      {
        if (this._types != null)
          this._types.CollectionChanged -= new CollectionChangedEventHandler(this.CollectionChanged);
        this._types = value;
        this._types.CollectionChanged += new CollectionChangedEventHandler(this.CollectionChanged);
      }
    }

    public SessionName GetName()
    {
      return this._name;
    }

    public Session SetName(SessionName name)
    {
      this._name = name;
      return this;
    }

    public bool IsExpired()
    {
      return this.Start.Add(this.Timeout) < DateTime.Now;
    }

    public bool GetHasPendingChange()
    {
      return this._hasNewItem;
    }

    public bool GetProductUsageLogged()
    {
      return this._productUsageLogged;
    }

    public void SetProductUsageLogged()
    {
      this._productUsageLogged = true;
    }

    public void SetHasPendingChange()
    {
      this._hasNewItem = true;
    }

    public void SetPendingChangeResolved()
    {
      this._hasNewItem = false;
    }

    public void Reset()
    {
      this.Start = DateTime.Now;
      this._productUsageLogged = false;
    }

    private void CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      if (!this.GetHasPendingChange())
        this.SetHasPendingChange();
      SessionChangedEventHandler sessionChanged = this.SessionChanged;
      if (sessionChanged == null)
        return;
      sessionChanged(sender, new SessionChangedEventArgs(this));
    }
  }
}
