// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IMECompositionResultEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class IMECompositionResultEventArgs : EventArgs
  {
    private string result;

    public IMECompositionResultEventArgs(string result)
    {
      this.result = result;
    }

    public string Result
    {
      get
      {
        return this.result;
      }
    }
  }
}
