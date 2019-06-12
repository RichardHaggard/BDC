// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TokenValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TokenValidatingEventArgs : EventArgs
  {
    private readonly string text;
    private bool isValidToken;

    public TokenValidatingEventArgs(string text)
      : this(text, true)
    {
    }

    public TokenValidatingEventArgs(string text, bool isValidToken)
    {
      this.text = text;
      this.isValidToken = isValidToken;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public bool IsValidToken
    {
      get
      {
        return this.isValidToken;
      }
      set
      {
        this.isValidToken = value;
      }
    }
  }
}
