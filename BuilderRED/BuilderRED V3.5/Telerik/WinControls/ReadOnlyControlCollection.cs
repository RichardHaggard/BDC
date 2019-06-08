// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ReadOnlyControlCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class ReadOnlyControlCollection : Control.ControlCollection
  {
    private readonly bool _isReadOnly;

    public ReadOnlyControlCollection(Control owner, bool isReadOnly)
      : base(owner)
    {
      this._isReadOnly = isReadOnly;
    }

    public override void Add(Control value)
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
      this.AddInternal(value);
    }

    public virtual void AddInternal(Control value)
    {
      base.Add(value);
    }

    public override void Clear()
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
      base.Clear();
    }

    public override void RemoveByKey(string key)
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
      base.RemoveByKey(key);
    }

    public virtual void RemoveInternal(Control value)
    {
      this.Remove(value);
    }

    public override bool IsReadOnly
    {
      get
      {
        return this._isReadOnly;
      }
    }
  }
}
