// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ResolveStyleBuilderEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class ResolveStyleBuilderEventArgs : EventArgs
  {
    private string themeName;
    private StyleGroup builder;

    public ResolveStyleBuilderEventArgs(string themeName, StyleGroup builder)
    {
      this.builder = builder;
      this.themeName = themeName;
    }

    public StyleGroup Builder
    {
      get
      {
        return this.builder;
      }
      set
      {
        this.builder = value;
      }
    }

    public string ThemeName
    {
      get
      {
        return this.themeName;
      }
    }
  }
}
