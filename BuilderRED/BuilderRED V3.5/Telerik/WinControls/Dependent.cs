// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Dependent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal struct Dependent
  {
    private RadProperty _DP;
    private WeakReference _wrDO;
    private WeakReference _wrEX;

    public override bool Equals(object o)
    {
      if (!(o is Dependent))
        return false;
      Dependent dependent = (Dependent) o;
      if (!this.IsValid() || !dependent.IsValid() || (this._wrEX.Target != dependent._wrEX.Target || this._DP != dependent._DP))
        return false;
      if (this._wrDO != null && dependent._wrDO != null)
      {
        if (this._wrDO.Target != dependent._wrDO.Target)
          return false;
      }
      else if (this._wrDO != null || dependent._wrDO != null)
        return false;
      return true;
    }

    public Dependent(RadObject o, RadProperty p)
    {
      this._wrEX = (WeakReference) null;
      this._DP = p;
      this._wrDO = o == null ? (WeakReference) null : new WeakReference((object) o);
    }

    public override int GetHashCode()
    {
      int num = 0;
      if (this._wrDO != null)
      {
        RadObject target = (RadObject) this._wrDO.Target;
        num += target == null ? 0 : target.GetHashCode();
      }
      return num + (this._DP == null ? 0 : this._DP.GetHashCode());
    }

    public bool IsValid()
    {
      return this._wrEX.IsAlive && (this._wrDO == null || this._wrDO.IsAlive);
    }

    public static bool operator ==(Dependent first, Dependent second)
    {
      return first.Equals((object) second);
    }

    public static bool operator !=(Dependent first, Dependent second)
    {
      return !first.Equals((object) second);
    }

    public RadObject DO
    {
      get
      {
        if (this._wrDO == null)
          return (RadObject) null;
        return (RadObject) this._wrDO.Target;
      }
    }

    public RadProperty DP
    {
      get
      {
        return this._DP;
      }
    }
  }
}
