// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RoutedEvent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RoutedEvent
  {
    private string eventName;
    private Type ownerType;

    internal RoutedEvent(string eventName, Type ownerType)
    {
      this.eventName = eventName;
      this.ownerType = ownerType;
    }

    public string EventName
    {
      get
      {
        return this.eventName;
      }
      set
      {
        this.eventName = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Type OwnerType
    {
      get
      {
        return this.ownerType;
      }
      internal set
      {
        this.ownerType = value;
      }
    }
  }
}
