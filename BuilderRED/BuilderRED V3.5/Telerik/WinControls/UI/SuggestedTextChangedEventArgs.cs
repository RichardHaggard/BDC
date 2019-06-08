// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SuggestedTextChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class SuggestedTextChangedEventArgs : EventArgs
  {
    private string text;
    private string suggestedText;
    private AutoCompleteAction action;
    private TextPosition startPosition;
    private TextPosition endPosition;

    public SuggestedTextChangedEventArgs(
      string text,
      string suggestedText,
      TextPosition startPosition,
      TextPosition endPosition,
      AutoCompleteAction action)
    {
      this.text = text;
      this.suggestedText = suggestedText;
      this.action = action;
      this.startPosition = startPosition;
      this.endPosition = endPosition;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public string SuggestedText
    {
      get
      {
        return this.suggestedText;
      }
    }

    public AutoCompleteAction Action
    {
      get
      {
        return this.action;
      }
    }

    public TextPosition StartPosition
    {
      get
      {
        return this.startPosition;
      }
      set
      {
        this.startPosition = value;
      }
    }

    public TextPosition EndPosition
    {
      get
      {
        return this.endPosition;
      }
      set
      {
        this.endPosition = value;
      }
    }
  }
}
