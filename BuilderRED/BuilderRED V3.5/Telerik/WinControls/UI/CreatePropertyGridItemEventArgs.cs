// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CreatePropertyGridItemEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CreatePropertyGridItemEventArgs : EventArgs
  {
    private PropertyGridItem parent;
    private PropertyGridItemBase item;
    private Type itemType;

    public CreatePropertyGridItemEventArgs(Type itemType)
    {
      this.itemType = itemType;
    }

    public CreatePropertyGridItemEventArgs(Type itemType, PropertyGridItem parent)
    {
      this.itemType = itemType;
      this.parent = parent;
    }

    public PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
      set
      {
        this.item = value;
      }
    }

    public PropertyGridItem Parent
    {
      get
      {
        return this.parent;
      }
    }

    public Type ItemType
    {
      get
      {
        return this.itemType;
      }
    }
  }
}
