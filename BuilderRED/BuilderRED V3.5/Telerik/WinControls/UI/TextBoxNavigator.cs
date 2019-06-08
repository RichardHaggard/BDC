// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxNavigator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public class TextBoxNavigator : ITextBoxNavigator, IDisposable
  {
    private readonly RadTextBoxControlElement textBoxElement;
    private TextPosition selectionStart;
    private TextPosition selectionEnd;
    private int suspendNotificationCount;
    private bool selectionSet;
    private int? savedStartOffset;
    private int? savedEndOffset;

    public TextBoxNavigator(RadTextBoxControlElement textBoxElement)
    {
      this.textBoxElement = textBoxElement;
      this.selectionStart = (TextPosition) null;
      this.selectionEnd = (TextPosition) null;
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      viewElement.VScroller.ScrollerUpdated += new EventHandler(this.OnScrollerUpdated);
      viewElement.HScroller.ScrollerUpdated += new EventHandler(this.OnScrollerUpdated);
      viewElement.TextChanged += new EventHandler(this.OnViewElementTextChanged);
      this.textBoxElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.OnTextBoxElementRadPropertyChanged);
    }

    ~TextBoxNavigator()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      if (viewElement.VScroller != null)
        viewElement.VScroller.ScrollerUpdated -= new EventHandler(this.OnScrollerUpdated);
      if (viewElement.HScroller != null)
        viewElement.HScroller.ScrollerUpdated -= new EventHandler(this.OnScrollerUpdated);
      viewElement.TextChanged -= new EventHandler(this.OnViewElementTextChanged);
      this.textBoxElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.OnTextBoxElementRadPropertyChanged);
    }

    protected RadTextBoxControlElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
    }

    public TextPosition SelectionStart
    {
      get
      {
        return this.selectionStart;
      }
      set
      {
        this.Select(value, this.selectionEnd);
      }
    }

    public TextPosition SelectionEnd
    {
      get
      {
        return this.selectionEnd;
      }
      set
      {
        this.Select(this.selectionStart, value);
      }
    }

    public int SelectionLength
    {
      get
      {
        return TextPosition.GetLength(this.SelectionStart, this.SelectionEnd);
      }
    }

    public TextPosition CaretPosition
    {
      get
      {
        TextPosition textPosition = this.selectionEnd;
        if ((object) textPosition == null)
          textPosition = this.selectionStart;
        return textPosition;
      }
      set
      {
        this.Select(value, (TextPosition) null);
      }
    }

    public event SelectionChangingEventHandler SelectionChanging;

    protected virtual void OnSelectionChanging(SelectionChangingEventArgs e)
    {
      SelectionChangingEventHandler selectionChanging = this.SelectionChanging;
      if (selectionChanging == null || this.suspendNotificationCount != 0)
        return;
      selectionChanging((object) this, e);
    }

    public event SelectionChangedEventHandler SelectionChanged;

    protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged != null && this.suspendNotificationCount == 0)
        selectionChanged((object) this, e);
      if (this.textBoxElement == null || this.textBoxElement.ElementTree == null || this.textBoxElement.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.textBoxElement.ElementTree.Control as RadControl, "SelectionChanged");
    }

    private void OnScrollerUpdated(object sender, EventArgs e)
    {
      this.SetCaretPosition();
    }

    private void OnViewElementTextChanged(object sender, EventArgs e)
    {
      TextBoxChangedEventArgs changedEventArgs = e as TextBoxChangedEventArgs;
      if (changedEventArgs.Action == TextBoxChangeAction.TextPropertyChange)
      {
        this.selectionSet = false;
      }
      else
      {
        if (changedEventArgs.Action != TextBoxChangeAction.TextEdit)
          return;
        this.CaretPosition = this.GetPositionFromOffset(changedEventArgs.CaretPosition);
        this.SetCaretPosition();
        this.ScrollToCaret();
      }
    }

    private void OnTextBoxElementRadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.IsFocusedProperty || !(bool) e.NewValue || this.selectionSet)
        return;
      this.selectionSet = true;
      if (this.SelectionLength != 0 || Control.MouseButtons != MouseButtons.None)
        return;
      this.textBoxElement.SelectAll();
    }

    public virtual void SaveSelection()
    {
      ITextBoxNavigator navigator = this.textBoxElement.Navigator;
      if (navigator.SelectionStart != (TextPosition) null)
        this.savedStartOffset = new int?((int) navigator.SelectionStart);
      if (!(navigator.SelectionEnd != (TextPosition) null))
        return;
      this.savedEndOffset = new int?((int) navigator.SelectionEnd);
    }

    public virtual void RestoreSelection()
    {
      bool flag = false;
      TextPosition start = this.SelectionStart;
      TextPosition end = this.SelectionEnd;
      if (this.savedStartOffset.HasValue)
      {
        start = this.GetPositionFromOffset(this.savedStartOffset.Value);
        flag = true;
        this.savedStartOffset = new int?();
      }
      if (this.savedEndOffset.HasValue)
      {
        end = this.GetPositionFromOffset(this.savedEndOffset.Value);
        flag = true;
        this.savedEndOffset = new int?();
      }
      if (!flag)
        return;
      this.SuspendNotifications();
      this.Select(start, end);
      this.ResumeNotifications();
    }

    public void SuspendNotifications()
    {
      ++this.suspendNotificationCount;
    }

    public void ResumeNotifications()
    {
      if (this.suspendNotificationCount <= 0)
        return;
      --this.suspendNotificationCount;
    }

    public virtual TextPosition GetPositionFromOffset(int offset)
    {
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (offset > this.textBoxElement.TextLength)
        offset = this.textBoxElement.TextLength;
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      LineInfo line = viewElement.Lines.BinarySearchByOffset(offset);
      if (line == null)
        return (TextPosition) null;
      ITextBlock textBlock = viewElement.BinarySearchTextBlockByOffset(line, offset);
      int charPosition = offset - textBlock.Offset;
      return new TextPosition(line, textBlock, charPosition);
    }

    public virtual TextPosition GetPositionFromPoint(PointF point)
    {
      LineInfoCollection lines = this.textBoxElement.ViewElement.Lines;
      LineInfo firstLine = lines.FirstLine;
      LineInfo lastLine = lines.LastLine;
      if (firstLine == null || lastLine == null)
        return (TextPosition) null;
      LineInfo line = (double) point.Y > (double) firstLine.ControlBoundingRectangle.Top ? ((double) point.Y < (double) lastLine.ControlBoundingRectangle.Bottom ? lines.BinarySearchByYCoordinate(point.Y) : lastLine) : firstLine;
      if (line == null)
        return (TextPosition) null;
      TextPosition positionFromLine = this.GetTextPositionFromLine(line, point.X);
      ITextBlock textBlock = positionFromLine.TextBlock;
      if (TextBoxWrapPanel.IsLineFeed(textBlock.Text) || TextBoxWrapPanel.IsCarriageReturn(textBlock.Text))
        positionFromLine.CharPosition = 0;
      return positionFromLine;
    }

    protected virtual TextPosition GetTextPositionFromLine(LineInfo line, float x)
    {
      ITextBlock startBlock = line.StartBlock;
      ITextBlock endBlock = line.EndBlock;
      if ((double) x <= (double) startBlock.ControlBoundingRectangle.X)
        return new TextPosition(line, startBlock, 0);
      if ((double) x >= (double) endBlock.ControlBoundingRectangle.Right)
        return new TextPosition(line, endBlock, endBlock.Length);
      ITextBlock textBlock = this.textBoxElement.ViewElement.BinarySearchTextBlockByXCoordinate(line, x);
      if (textBlock == null)
        return (TextPosition) null;
      int characterIndexFromX = textBlock.GetCharacterIndexFromX(x);
      return new TextPosition(line, textBlock, characterIndexFromX);
    }

    public virtual bool ScrollToCaret()
    {
      TextBoxControlCaret caret = this.TextBoxElement.Caret;
      RadScrollBarElement hscrollBar = this.textBoxElement.HScrollBar;
      RadScrollBarElement vscrollBar = this.textBoxElement.VScrollBar;
      if (caret == null || this.CaretPosition == (TextPosition) null)
      {
        vscrollBar.Value = vscrollBar.Minimum;
        hscrollBar.Value = hscrollBar.Minimum;
        return false;
      }
      RectangleF boundingRectangle = (RectangleF) caret.ControlBoundingRectangle;
      RectangleF viewportBounds = (RectangleF) this.TextBoxElement.ViewElement.ViewportBounds;
      float num1 = 0.0f;
      float num2 = 0.0f;
      LineInfo line = this.CaretPosition.Line;
      ITextBlock textBlock = this.CaretPosition.TextBlock;
      int charPosition = this.CaretPosition.CharPosition;
      if (textBlock == line.StartBlock && charPosition == 0)
        num1 = (float) (hscrollBar.Minimum - hscrollBar.Value);
      else if ((double) boundingRectangle.X > (double) viewportBounds.Right || (double) boundingRectangle.X < (double) viewportBounds.Left)
        num1 = boundingRectangle.X - viewportBounds.X - viewportBounds.Width / 2f;
      this.SetScrollBarValue(hscrollBar, hscrollBar.Value + (int) num1);
      if (this.textBoxElement.Multiline)
      {
        if (line == this.textBoxElement.ViewElement.Lines.FirstLine)
          num2 = (float) (vscrollBar.Minimum - vscrollBar.Value);
        else if (line == this.textBoxElement.ViewElement.Lines.LastLine)
        {
          int num3 = this.textBoxElement.ViewElement.VScroller.MaxValue - 1;
          if (num3 != 0)
            num2 = (float) (num3 - vscrollBar.Value + 1);
        }
        else if ((double) boundingRectangle.Top < (double) viewportBounds.Top)
          num2 = boundingRectangle.Top - viewportBounds.Top;
        else if ((double) boundingRectangle.Bottom > (double) viewportBounds.Bottom)
          num2 = boundingRectangle.Bottom - viewportBounds.Bottom;
        this.SetScrollBarValue(vscrollBar, vscrollBar.Value + (int) num2);
      }
      return true;
    }

    private void SetScrollBarValue(RadScrollBarElement scrollBar, int value)
    {
      int minimum = scrollBar.Minimum;
      int num = scrollBar.Maximum - scrollBar.LargeChange + 1;
      if (value > num)
        value = num;
      if (value < minimum)
        value = minimum;
      scrollBar.Value = value;
    }

    public virtual bool Navigate(KeyEventArgs keys)
    {
      if (this.selectionStart == (TextPosition) null)
        return false;
      TextPosition end = (TextPosition) null;
      TextPosition caretPosition = this.CaretPosition;
      switch (keys.KeyCode)
      {
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Right:
          end = this.NavigateAtLine(keys, caretPosition);
          break;
        case Keys.Up:
        case Keys.Down:
          if (!this.textBoxElement.Multiline)
            return false;
          end = this.NavigateToLine(keys, caretPosition);
          break;
      }
      if (keys.Shift)
        this.Select(this.selectionStart, end);
      else
        this.CaretPosition = end;
      if (this.textBoxElement.Multiline)
        this.ScrollToCaret();
      return false;
    }

    protected virtual TextPosition NavigateAtLine(
      KeyEventArgs keys,
      TextPosition position)
    {
      Keys keyCode = keys.KeyCode;
      LineInfo line = position.Line;
      TextPosition position1 = position;
      switch (keyCode)
      {
        case Keys.End:
          ITextBlock endBlock = line.EndBlock;
          position1 = new TextPosition(line, endBlock, endBlock.Length);
          break;
        case Keys.Home:
          position1 = new TextPosition(line, line.StartBlock, 0);
          break;
        case Keys.Left:
          position1 = this.GetPreviousPosition(position1);
          break;
        case Keys.Right:
          position1 = this.GetNextPosition(position1);
          break;
      }
      return position1;
    }

    public virtual TextPosition GetPreviousPosition(TextPosition position)
    {
      return this.GetPreviousPositionCore(position);
    }

    protected virtual TextPosition GetPreviousPositionCore(TextPosition position)
    {
      TextPosition position1 = position;
      LineInfo line = position.Line;
      int charPosition1 = position.CharPosition - 1;
      ITextBlock textBlock = position.TextBlock;
      int num = textBlock.Offset - 1;
      bool flag = false;
      if (charPosition1 >= 0)
      {
        position1 = new TextPosition(line, textBlock, charPosition1);
        flag = TextBoxWrapPanel.IsCarriageReturn(textBlock.Text);
      }
      else if (num >= 0)
      {
        TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
        int index = textBlock.Index - 1;
        ITextBlock child = viewElement.Children[index] as ITextBlock;
        int charPosition2 = child.Length > 0 ? child.Length - 1 : 0;
        if (index < line.StartBlock.Index)
          line = viewElement.Lines.BinarySearchByBlockIndex(index);
        else
          flag = TextBoxWrapPanel.IsLineFeed(child.Text) || TextBoxWrapPanel.IsCarriageReturn(child.Text);
        position1 = new TextPosition(line, child, charPosition2);
      }
      if (!object.Equals((object) position1, (object) position) && flag)
        position1 = this.GetPreviousPositionCore(position1);
      return position1;
    }

    public virtual TextPosition GetNextPosition(TextPosition position)
    {
      return this.GetNextPositionCore(position);
    }

    protected virtual TextPosition GetNextPositionCore(TextPosition position)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      TextPosition position1 = position;
      LineInfo line = position.Line;
      ITextBlock textBlock = position.TextBlock;
      int charPosition1 = position.CharPosition + 1;
      int index = textBlock.Index + 1;
      int num = textBlock.Offset + textBlock.Length + 1;
      bool flag = false;
      if (charPosition1 <= textBlock.Length)
      {
        position1 = new TextPosition(line, textBlock, charPosition1);
        flag = TextBoxWrapPanel.IsCarriageReturn(textBlock.Text);
      }
      else if (num <= viewElement.TextLength)
      {
        ITextBlock child = viewElement.Children[index] as ITextBlock;
        int charPosition2;
        if (index > line.EndBlock.Index)
        {
          line = viewElement.Lines.BinarySearchByBlockIndex(index);
          charPosition2 = 0;
        }
        else
        {
          charPosition2 = child.Length > 0 ? 1 : 0;
          flag = TextBoxWrapPanel.IsCarriageReturn(child.Text) || TextBoxWrapPanel.IsLineFeed(child.Text);
        }
        position1 = new TextPosition(line, child, charPosition2);
      }
      if (!object.Equals((object) position1, (object) position) && flag)
        position1 = this.GetNextPositionCore(position1);
      return position1;
    }

    protected virtual TextPosition NavigateToLine(
      KeyEventArgs keys,
      TextPosition position)
    {
      Keys keyCode = keys.KeyCode;
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      ReadOnlyCollection<LineInfo> lines = (ReadOnlyCollection<LineInfo>) viewElement.Lines;
      LineInfo line = position.Line;
      int num = lines.IndexOf(line);
      if (keyCode == Keys.Up && num > 0)
        line = lines[num - 1];
      else if (keyCode == Keys.Down && num < lines.Count - 1)
        line = lines[num + 1];
      if (position.Line == line)
        return position;
      float x = viewElement.GetLocation(position).X + viewElement.ScrollOffset.Width;
      return this.GetTextPositionFromLine(line, x);
    }

    public virtual bool Select(TextPosition start, TextPosition end)
    {
      if (start == (TextPosition) null && end != (TextPosition) null)
        throw new ArgumentNullException(nameof (start));
      if (object.Equals((object) start, (object) end))
        end = (TextPosition) null;
      if (object.Equals((object) this.SelectionStart, (object) start) && object.Equals((object) this.SelectionEnd, (object) end))
      {
        this.SetCaretPosition();
        return false;
      }
      bool notify = this.SelectionStart != (TextPosition) null;
      if (notify)
      {
        int length = TextPosition.GetLength(start, end);
        SelectionChangingEventArgs e = new SelectionChangingEventArgs(this.textBoxElement.SelectionStart, this.SelectionLength, end == (TextPosition) null ? (int) start : Math.Min((int) start, (int) end), length);
        this.OnSelectionChanging(e);
        if (e.Cancel)
          return false;
      }
      this.selectionStart = start;
      this.selectionEnd = end;
      this.SelectOverride(notify);
      return true;
    }

    protected virtual void SelectOverride(bool notify)
    {
      this.selectionSet = true;
      this.SetCaretPosition();
      if (this.selectionEnd == (TextPosition) null || this.selectionStart == this.selectionEnd)
        this.textBoxElement.Caret.Show();
      else
        this.textBoxElement.Caret.Hide();
      this.textBoxElement.ViewElement.SelectionPrimitive.Invalidate(this.selectionStart, this.selectionEnd, this.suspendNotificationCount == 0);
      if (!this.textBoxElement.Multiline)
        this.ScrollToCaret();
      if (!notify)
        return;
      this.OnSelectionChanged(new SelectionChangedEventArgs(this.textBoxElement.SelectionStart, this.SelectionLength));
    }

    public virtual void SetCaretPosition()
    {
      if (this.selectionStart == (TextPosition) null)
        return;
      TextBoxControlCaret caret = this.textBoxElement.Caret;
      TextPosition caretPosition = this.CaretPosition;
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      PointF location = viewElement.GetLocation(caretPosition);
      SizeF scrollOffset = viewElement.ScrollOffset;
      location.X += scrollOffset.Width;
      location.Y += scrollOffset.Height;
      caret.Height = (int) caretPosition.Line.Size.Height;
      Point point = this.textBoxElement.PointFromControl(Point.Truncate(location));
      caret.Position = point;
    }
  }
}
