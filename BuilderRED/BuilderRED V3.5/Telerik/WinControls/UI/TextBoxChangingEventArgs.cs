// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TextBoxChangingEventArgs : TextChangingEventArgs
  {
    private int startPosition;
    private int length;
    private TextBoxChangeAction action;

    public TextBoxChangingEventArgs(
      int startPosition,
      int length,
      string oldText,
      string newText,
      TextBoxChangeAction action)
      : base(oldText, newText)
    {
      this.startPosition = startPosition;
      this.length = length;
      this.action = action;
    }

    public TextBoxChangeAction Action
    {
      get
      {
        return this.action;
      }
    }

    public int StartPosition
    {
      get
      {
        return this.startPosition;
      }
    }

    public int Length
    {
      get
      {
        return this.length;
      }
    }
  }
}
