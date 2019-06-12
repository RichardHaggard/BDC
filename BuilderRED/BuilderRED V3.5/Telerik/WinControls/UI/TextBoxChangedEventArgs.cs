// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TextBoxChangedEventArgs : EventArgs
  {
    private string text;
    private int caretPosition;
    private TextBoxChangeAction action;

    public TextBoxChangedEventArgs(string text, int caretPosition, TextBoxChangeAction action)
    {
      this.text = text;
      this.action = action;
      this.caretPosition = caretPosition;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public int CaretPosition
    {
      get
      {
        return this.caretPosition;
      }
    }

    public TextBoxChangeAction Action
    {
      get
      {
        return this.action;
      }
    }
  }
}
