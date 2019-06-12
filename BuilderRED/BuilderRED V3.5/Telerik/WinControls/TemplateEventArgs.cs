// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TemplateEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal class TemplateEventArgs : EventArgs
  {
    private RadItem item;

    public TemplateEventArgs(RadItem item)
    {
      this.item = item;
    }

    public TemplateEventArgs()
    {
    }

    public RadItem TemplateInstance
    {
      get
      {
        return this.item;
      }
    }
  }
}
