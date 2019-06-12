// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.XmlSerializerDisposalBin
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.XmlSerialization
{
  internal class XmlSerializerDisposalBin
  {
    private List<IDisposable> disposalBin;
    private List<IDisposable> nonDisposalBin;

    public void AddObjectsToDispose(IEnumerable toRead)
    {
      foreach (object toDispose in toRead)
        this.AddObjectToDispose(toDispose);
    }

    public void AddObjectToDispose(object toDispose)
    {
      IDisposable disposable = toDispose as IDisposable;
      if (disposable == null || this.nonDisposalBin != null && this.nonDisposalBin.Contains(disposable))
        return;
      if (this.disposalBin == null)
        this.disposalBin = new List<IDisposable>();
      if (this.disposalBin.Contains(disposable))
        return;
      this.disposalBin.Add(disposable);
    }

    public void SetObjectShouldNotBeDisposed(object toNotDispose)
    {
      IDisposable disposable = toNotDispose as IDisposable;
      if (disposable == null)
        return;
      if (this.nonDisposalBin == null)
        this.nonDisposalBin = new List<IDisposable>();
      this.nonDisposalBin.Add(disposable);
      if (this.disposalBin == null)
        return;
      this.disposalBin.Remove(disposable);
    }

    public void DisposeDisposalBin(DisposeObjectDelegate callBack)
    {
      if (this.disposalBin == null)
        return;
      foreach (IDisposable toBeDisposed in this.disposalBin)
      {
        if (callBack != null)
          callBack(toBeDisposed);
        else
          toBeDisposed.Dispose();
      }
      this.disposalBin.Clear();
    }
  }
}
