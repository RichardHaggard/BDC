// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseAutoComplete
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class BaseAutoComplete : IDisposable
  {
    private RadDropDownListElement owner;

    public BaseAutoComplete(RadDropDownListElement owner)
    {
      this.owner = owner;
    }

    public RadDropDownListElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public abstract void AutoComplete(KeyPressEventArgs e);

    public virtual void Dispose()
    {
    }
  }
}
