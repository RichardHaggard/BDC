// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CreateTextBlockEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CreateTextBlockEventArgs : EventArgs
  {
    private ITextBlock textBlock;
    private readonly string text;

    public CreateTextBlockEventArgs(string text, ITextBlock textBlock)
    {
      this.textBlock = textBlock;
      this.text = text;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public ITextBlock TextBlock
    {
      get
      {
        return this.textBlock;
      }
      set
      {
        if (this.textBlock == value)
          return;
        this.textBlock = value;
      }
    }
  }
}
