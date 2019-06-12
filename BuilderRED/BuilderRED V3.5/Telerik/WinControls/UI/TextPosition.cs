// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextPosition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class TextPosition : IComparable<TextPosition>, IEquatable<TextPosition>
  {
    private int charPosition;
    private ITextBlock textBlock;
    private LineInfo line;

    public TextPosition(LineInfo line, ITextBlock textBlock, int charPosition)
    {
      this.line = line;
      this.textBlock = textBlock;
      this.charPosition = charPosition;
    }

    public LineInfo Line
    {
      get
      {
        return this.line;
      }
    }

    public ITextBlock TextBlock
    {
      get
      {
        return this.textBlock;
      }
    }

    public int CharPosition
    {
      get
      {
        return this.charPosition;
      }
      internal set
      {
        this.charPosition = value;
      }
    }

    public virtual int CompareTo(TextPosition position)
    {
      if (position == (TextPosition) null)
        return 1;
      int x = (int) this;
      int num = (int) position;
      return Comparer<int>.Default.Compare(x, (int) position);
    }

    public virtual bool Equals(TextPosition position)
    {
      if (position == (TextPosition) null || this.charPosition != position.charPosition || this.line != position.line)
        return false;
      return this.textBlock == position.textBlock;
    }

    public override bool Equals(object obj)
    {
      TextPosition position = obj as TextPosition;
      if (position == (TextPosition) null)
        return false;
      return this.Equals(position);
    }

    public override int GetHashCode()
    {
      return (int) this.GetHashCode();
    }

    public static implicit operator int(TextPosition start)
    {
      if (start == (TextPosition) null)
        return 0;
      return start.TextBlock.Offset + start.CharPosition;
    }

    public static bool operator >(TextPosition start, TextPosition end)
    {
      return Comparer<TextPosition>.Default.Compare(start, end) == 1;
    }

    public static bool operator >=(TextPosition start, TextPosition end)
    {
      int num = Comparer<TextPosition>.Default.Compare(start, end);
      if (num != 1)
        return num == 0;
      return true;
    }

    public static bool operator <(TextPosition start, TextPosition end)
    {
      return Comparer<TextPosition>.Default.Compare(start, end) == -1;
    }

    public static bool operator <=(TextPosition start, TextPosition end)
    {
      int num = Comparer<TextPosition>.Default.Compare(start, end);
      if (num != -1)
        return num == 0;
      return true;
    }

    public static bool operator ==(TextPosition start, TextPosition end)
    {
      return Comparer<TextPosition>.Default.Compare(start, end) == 0;
    }

    public static bool operator !=(TextPosition start, TextPosition end)
    {
      return Comparer<TextPosition>.Default.Compare(start, end) != 0;
    }

    public override string ToString()
    {
      return string.Format("Offset: {0} TextBlock: {1} Character Position: {2}", (object) (int) this, (object) this.textBlock.Text, (object) this.charPosition);
    }

    public static int GetLength(TextPosition start, TextPosition end)
    {
      if (start == (TextPosition) null || end == (TextPosition) null)
        return 0;
      return Math.Abs((int) start - (int) end);
    }

    public static TextPosition GetFirstPosition(TextBoxWrapPanel layoutPanel)
    {
      LineInfo firstLine = layoutPanel.Lines.FirstLine;
      if (firstLine == null)
        return (TextPosition) null;
      return new TextPosition(firstLine, firstLine.StartBlock, 0);
    }

    public static TextPosition GetLastPosition(TextBoxWrapPanel layoutPanel)
    {
      LineInfo lastLine = layoutPanel.Lines.LastLine;
      if (lastLine == null)
        return (TextPosition) null;
      ITextBlock endBlock = lastLine.EndBlock;
      return new TextPosition(lastLine, endBlock, endBlock.Length);
    }

    public static void Swap(ref TextPosition startPosition, ref TextPosition endPosition)
    {
      if (!(startPosition > endPosition) || !(endPosition != (TextPosition) null))
        return;
      TextPosition textPosition = startPosition;
      startPosition = endPosition;
      endPosition = textPosition;
    }
  }
}
