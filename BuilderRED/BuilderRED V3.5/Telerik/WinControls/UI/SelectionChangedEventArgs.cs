// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class SelectionChangedEventArgs : EventArgs
  {
    private int selectionStart;
    private int selectionLength;

    public SelectionChangedEventArgs(int selectionStart, int selectionLength)
    {
      this.selectionStart = selectionStart;
      this.selectionLength = selectionLength;
    }

    public int SelectionStart
    {
      get
      {
        return this.selectionStart;
      }
    }

    public int SelectionLength
    {
      get
      {
        return this.selectionLength;
      }
    }
  }
}
