// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPropertyGridEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadPropertyGridEventArgs : EventArgs
  {
    private PropertyGridItemBase item;

    public RadPropertyGridEventArgs(PropertyGridItemBase item)
    {
      this.item = item;
    }

    public virtual PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
    }
  }
}
