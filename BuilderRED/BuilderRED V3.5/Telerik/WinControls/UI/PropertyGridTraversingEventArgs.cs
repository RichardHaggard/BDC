// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTraversingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class PropertyGridTraversingEventArgs : EventArgs
  {
    private PropertyGridItemBase item;
    private bool process;

    public PropertyGridTraversingEventArgs(PropertyGridItemBase item)
    {
      this.item = item;
      this.process = item == null || item.Visible;
    }

    public bool Process
    {
      get
      {
        return this.process;
      }
      set
      {
        this.process = value;
      }
    }

    public PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
    }
  }
}
