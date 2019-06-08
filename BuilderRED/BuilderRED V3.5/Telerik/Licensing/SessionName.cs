// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.SessionName
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.Text;

namespace Telerik.Licensing
{
  internal class SessionName
  {
    private readonly string _name;

    public SessionName(string name, bool existing = false)
    {
      this._name = existing ? name : this.InitSessionName(name);
    }

    public string Name
    {
      get
      {
        return this._name;
      }
    }

    public static SessionName FromExisting(string name)
    {
      return new SessionName(name, false);
    }

    private string InitSessionName(string name)
    {
      name = Convert.ToBase64String(Encoding.UTF8.GetBytes(name));
      foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
        name = name.Replace(invalidFileNameChar.ToString(), string.Empty);
      return name;
    }
  }
}
