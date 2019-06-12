// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectionChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class SelectionChangingEventArgs : CancelEventArgs
  {
    private int oldSelectionStart;
    private int oldSelectionLength;
    private int newSelectionStart;
    private int newSelectionLength;

    public SelectionChangingEventArgs(
      int oldSelectionStart,
      int oldSelectionLength,
      int newSelectionStart,
      int newSelectionLength)
    {
      this.oldSelectionStart = oldSelectionStart;
      this.oldSelectionLength = oldSelectionLength;
      this.newSelectionStart = newSelectionStart;
      this.newSelectionLength = newSelectionLength;
    }

    public int OldSelectionStart
    {
      get
      {
        return this.oldSelectionStart;
      }
    }

    public int OldSelectionLength
    {
      get
      {
        return this.oldSelectionLength;
      }
    }

    public int NewSelectionStart
    {
      get
      {
        return this.newSelectionStart;
      }
    }

    public int NewSelectionLength
    {
      get
      {
        return this.newSelectionLength;
      }
    }
  }
}
