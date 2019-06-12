// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxWrapPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Interfaces;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TextBoxWrapPanel : LightVisualElement, INotifyPropertyChangingEx
  {
    protected static readonly System.Type TextBlockElementType = typeof (TextBlockElement);
    private string cachedText = string.Empty;
    private bool wordWrap = true;
    public const char CarriageReturnSymbol = '\r';
    public const char LineFeedSymbol = '\n';
    public const char TabSymbol = '\t';
    private readonly LineInfoCollection lines;
    private HorizontalAlignment textAlign;
    private bool multiline;
    private int lineSpacing;

    public TextBoxWrapPanel()
    {
      this.lines = new LineInfoCollection();
      int num = (int) this.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(2));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = true;
      this.CanFocus = false;
      this.ShouldHandleMouseInput = false;
      this.NotifyParentOnMouseInput = true;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    public Rectangle ViewportBounds
    {
      get
      {
        Rectangle boundingRectangle = this.ControlBoundingRectangle;
        Padding clientOffset = this.GetClientOffset(true);
        boundingRectangle.Height -= clientOffset.Vertical;
        boundingRectangle.Width -= clientOffset.Horizontal;
        boundingRectangle.X += clientOffset.Left;
        boundingRectangle.Y += clientOffset.Top;
        return boundingRectangle;
      }
    }

    public int LineSpacing
    {
      get
      {
        return this.lineSpacing;
      }
      set
      {
        this.lineSpacing = value;
      }
    }

    public LineInfoCollection Lines
    {
      get
      {
        this.LoadElementTree();
        return this.lines;
      }
    }

    public int TextLength
    {
      get
      {
        LineInfo lastLine = this.Lines.LastLine;
        if (lastLine == null)
          return 0;
        ITextBlock endBlock = lastLine.EndBlock;
        return endBlock.Offset + endBlock.Length;
      }
    }

    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.textAlign;
      }
      set
      {
        if (!this.OnPropertyChanging(nameof (TextAlign), (object) this.textAlign, (object) value))
          return;
        this.textAlign = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (TextAlign));
      }
    }

    public bool WordWrap
    {
      get
      {
        return this.wordWrap;
      }
      set
      {
        if (this.wordWrap == value || !this.OnPropertyChanging(nameof (WordWrap), (object) this.wordWrap, (object) value))
          return;
        this.wordWrap = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (WordWrap));
      }
    }

    public bool Multiline
    {
      get
      {
        return this.multiline;
      }
      set
      {
        if (this.multiline == value || !this.OnPropertyChanging(nameof (Multiline), (object) this.multiline, (object) value))
          return;
        this.multiline = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (Multiline));
      }
    }

    public override string Text
    {
      get
      {
        if (string.IsNullOrEmpty(this.cachedText) && this.Children.Count > 0)
        {
          TextPosition firstPosition = TextPosition.GetFirstPosition(this);
          TextPosition lastPosition = TextPosition.GetLastPosition(this);
          if (firstPosition == (TextPosition) null && lastPosition == (TextPosition) null)
            return string.Empty;
          this.cachedText = this.GetTextRange(firstPosition, lastPosition);
        }
        return this.cachedText;
      }
      set
      {
        TextBoxChangeAction action = TextBoxChangeAction.TextPropertyChange;
        string text = this.Text;
        if (!(text != value) || !this.NotifyTextChanging(0, this.TextLength, text, value, action))
          return;
        this.CreateTextBlocks(value);
        this.NotifyTextChanged(value, string.IsNullOrEmpty(value) ? 0 : value.Length, action);
      }
    }

    public event TextBlockFormattingEventHandler TextBlockFormatting;

    protected void OnTextBlockFormatting(ITextBlock textBlock)
    {
      this.OnTextBlockFormatting(new TextBlockFormattingEventArgs(textBlock));
    }

    protected virtual void OnTextBlockFormatting(TextBlockFormattingEventArgs e)
    {
      TextBlockFormattingEventHandler textBlockFormatting = this.TextBlockFormatting;
      if (textBlockFormatting == null)
        return;
      textBlockFormatting((object) this, e);
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected bool OnPropertyChanging(string propertyName, object oldValue, object newValue)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, oldValue, newValue);
      this.OnPropertyChanging(e);
      return !e.Cancel;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      PropertyChangingEventHandlerEx propertyChanging = this.PropertyChanging;
      if (propertyChanging == null)
        return;
      propertyChanging((object) this, e);
    }

    public event CreateTextBlockEventHandler CreateTextBlock;

    protected ITextBlock NotifyCreateTextBlock(ITextBlock block, string text)
    {
      CreateTextBlockEventArgs e = new CreateTextBlockEventArgs(text, block);
      this.OnCreateTextBlock(e);
      return e.TextBlock;
    }

    protected virtual void OnCreateTextBlock(CreateTextBlockEventArgs e)
    {
      CreateTextBlockEventHandler createTextBlock = this.CreateTextBlock;
      if (createTextBlock == null)
        return;
      createTextBlock((object) this, e);
    }

    protected virtual bool NotifyTextChanging(
      int startPosition,
      int length,
      string oldText,
      string newText,
      TextBoxChangeAction action)
    {
      TextBoxChangingEventArgs changingEventArgs = new TextBoxChangingEventArgs(startPosition, length, oldText, newText, action);
      this.OnTextChanging((TextChangingEventArgs) changingEventArgs);
      return !changingEventArgs.Cancel;
    }

    protected virtual void NotifyTextChanged(
      string text,
      int caretPosition,
      TextBoxChangeAction action)
    {
      this.OnTextChanged((EventArgs) new TextBoxChangedEventArgs(text, caretPosition, action));
    }

    protected override void OnTextChanged(EventArgs e)
    {
      this.cachedText = string.Empty;
      base.OnTextChanged(e);
    }

    protected void InvalidateLayout()
    {
      this.InvalidateMeasure();
      this.InvalidateArrange();
      this.UpdateLayout();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      Padding clientOffset = this.GetClientOffset(true);
      SizeF availableSize1 = new SizeF(availableSize.Width - (float) clientOffset.Horizontal, availableSize.Height - (float) clientOffset.Vertical);
      if (this.Children.Count == 0)
        this.Children.Add(this.CreateBlock(string.Empty, TextBoxWrapPanel.TextBlockElementType) as RadElement);
      SizeF desiredSize = SizeF.Empty;
      double width = (double) availableSize1.Width;
      int num = -1;
      int offset = 0;
      for (int blockIndex = 0; blockIndex < this.Children.Count; ++blockIndex)
      {
        ITextBlock child = this.Children[blockIndex] as ITextBlock;
        child.Index = blockIndex;
        this.MergeAndMeasureBlock(child, availableSize1);
        bool flag = TextBoxWrapPanel.IsLineFeed(child.Text);
        if (flag && num >= 0)
          ++offset;
        LineInfo line = num == -1 || flag && this.multiline ? this.CreateNewLine(child, ref desiredSize, ref num) : this.GetLineInfo(num);
        if (this.multiline && this.wordWrap)
        {
          if (!this.MeasureWrap(availableSize1, blockIndex, ref desiredSize, ref num, ref offset))
            line = this.GetLineInfo(num);
          else
            continue;
        }
        this.AddBlockDesiredSize(child.DesiredSize, line);
        line.EndBlock = child;
        child.Offset = offset;
        offset += child.Length;
      }
      this.AddLineDesiredSize(this.GetLineInfo(num), ref desiredSize);
      this.RemoveUnnecessaryLines(num);
      desiredSize.Width += (float) clientOffset.Horizontal;
      desiredSize.Height += (float) clientOffset.Vertical;
      desiredSize = this.ClampDesiredSize(desiredSize, availableSize);
      return desiredSize;
    }

    protected virtual SizeF ClampDesiredSize(SizeF desiredSize, SizeF availableSize)
    {
      if ((double) desiredSize.Width > (double) availableSize.Width)
        desiredSize.Width = availableSize.Width;
      if ((double) desiredSize.Height > (double) availableSize.Height)
        desiredSize.Height = availableSize.Height;
      return desiredSize;
    }

    protected virtual void MergeAndMeasureBlock(ITextBlock textBlock, SizeF availableSize)
    {
      int index1 = textBlock.Index;
      int index2 = index1 + 1;
      ITextBlock firstBlock = textBlock;
      ITextBlock nextBlock = this.GetNextBlock(index1);
      StringBuilder stringBuilder = new StringBuilder(firstBlock.Text);
      while (this.AreSplittedBlock(firstBlock, nextBlock))
      {
        stringBuilder.Append(nextBlock.Text);
        firstBlock = nextBlock;
        nextBlock = this.GetNextBlock(index2);
        this.Children.RemoveAt(index2);
      }
      textBlock.Text = stringBuilder.ToString();
      this.MeasureTextBlock(textBlock, availableSize);
    }

    protected virtual bool MeasureWrap(
      SizeF availableSize,
      int blockIndex,
      ref SizeF desiredSize,
      ref int currentLineIndex,
      ref int offset)
    {
      LineInfo currentLine = this.GetLineInfo(currentLineIndex);
      ITextBlock child = this.Children[blockIndex] as ITextBlock;
      float width1 = child.DesiredSize.Width;
      float num = currentLine.Size.Width + width1;
      float width2 = availableSize.Width;
      if ((double) num > (double) availableSize.Width)
      {
        ITextBlock previousBlock = this.GetPreviousBlock(blockIndex);
        if ((double) width1 > (double) availableSize.Width)
        {
          if (child != currentLine.StartBlock)
            currentLine = this.CreateNewLine(child, ref desiredSize, ref currentLineIndex);
        }
        else if (!this.AreSplittedBlock(child, previousBlock))
          availableSize.Width -= currentLine.Size.Width;
        if ((double) width1 > (double) availableSize.Width && ((double) num <= (double) availableSize.Width || (double) width1 >= (double) width2) && this.SplitBlock(currentLine, availableSize, ref child, ref offset))
          return true;
        if (child != currentLine.StartBlock)
          this.CreateNewLine(child, ref desiredSize, ref currentLineIndex);
      }
      return false;
    }

    protected virtual bool SplitBlock(
      LineInfo currentLine,
      SizeF availableSize,
      ref ITextBlock textBlock,
      ref int offset)
    {
      string text1 = textBlock.Text;
      int num = TextBoxControlMeasurer.BinarySearchWrapIndex(text1, this.Font, availableSize.Width);
      switch (num)
      {
        case -1:
        case 0:
          return false;
        default:
          if (num != text1.Length)
          {
            string str = text1.Substring(0, num);
            string text2 = text1.Substring(num);
            textBlock.Text = str;
            textBlock.Offset = offset;
            offset += textBlock.Length;
            int index1 = textBlock.Index;
            this.MeasureTextBlock(textBlock, availableSize);
            currentLine.EndBlock = textBlock;
            this.AddBlockDesiredSize(textBlock.DesiredSize, currentLine);
            int index2 = index1 + 1;
            textBlock = this.CreateBlock(text2, TextBoxWrapPanel.TextBlockElementType);
            this.Children.Insert(index2, textBlock as RadElement);
            return true;
          }
          goto case -1;
      }
    }

    protected void MeasureTextBlock(ITextBlock textBlock, SizeF availableSize)
    {
      this.OnTextBlockFormatting(textBlock);
      this.MeasureTextBlockOverride(textBlock, availableSize);
    }

    protected virtual void MeasureTextBlockOverride(ITextBlock textBlock, SizeF availableSize)
    {
      textBlock.Measure(availableSize);
    }

    public void CreateTextBlocks(string text)
    {
      this.Children.Clear();
      if (!string.IsNullOrEmpty(text))
        this.CreateTextBlocksOverride(text);
      this.UpdateLayout();
    }

    protected virtual void CreateTextBlocksOverride(string text)
    {
      this.InsertTextBlocks(0, text, TextBoxWrapPanel.TextBlockElementType);
    }

    protected virtual int InsertTextBlocks(int index, string text, System.Type blockType)
    {
      if (string.IsNullOrEmpty(text))
        return index;
      int length = text.Length;
      bool flag1 = this.Children.Count == 0 && length > 1024;
      StringBuilder stringBuilder = new StringBuilder();
      List<RadElement> radElementList = (List<RadElement>) null;
      if (flag1)
        radElementList = new List<RadElement>((IEnumerable<RadElement>) this.Children);
      for (int index1 = 0; index1 < length; ++index1)
      {
        char c = text[index1];
        if (char.IsWhiteSpace(c) || c == '\t' || (c == '\n' || c == '\r'))
        {
          if (stringBuilder.Length > 0)
          {
            ITextBlock block = this.CreateBlock(stringBuilder.ToString(), blockType);
            bool flag2 = index >= this.Children.Count;
            if (flag1)
              flag2 = index >= radElementList.Count;
            if (flag2)
            {
              if (!flag1)
              {
                this.Children.Add(block as RadElement);
                index = this.Children.Count - 1;
              }
              else
              {
                radElementList.Add(block as RadElement);
                index = radElementList.Count - 1;
              }
              block.Index = index;
            }
            else
            {
              block.Index = index;
              if (!flag1)
                this.Children.Insert(index, block as RadElement);
              else
                radElementList.Insert(index, block as RadElement);
            }
            stringBuilder = new StringBuilder();
            ++index;
          }
          ITextBlock block1 = this.CreateBlock(c.ToString(), blockType);
          block1.Index = index;
          if (!flag1)
            this.Children.Insert(index, block1 as RadElement);
          else
            radElementList.Insert(index, block1 as RadElement);
          ++index;
        }
        else
          stringBuilder.Append(c);
      }
      if (flag1)
      {
        this.SuspendLayout();
        this.Children.Clear();
        this.Children.AddRange((IList<RadElement>) radElementList);
      }
      if (stringBuilder.Length > 0)
      {
        ITextBlock block = this.CreateBlock(stringBuilder.ToString(), blockType);
        block.Index = index;
        this.Children.Insert(index, block as RadElement);
        ++index;
      }
      if (flag1)
        this.ResumeLayout(true);
      return index - 1;
    }

    private void RemoveUnnecessaryLines(int currentLineIndex)
    {
      ++currentLineIndex;
      int count = this.lines.Count - currentLineIndex;
      if (count <= 0)
        return;
      this.lines.RemoveRange(currentLineIndex, count);
    }

    protected virtual LineInfo CreateNewLine(
      ITextBlock block,
      ref SizeF desiredSize,
      ref int lineIndex)
    {
      if (lineIndex >= 0)
        this.AddLineDesiredSize(this.GetLineInfo(lineIndex), ref desiredSize);
      ++lineIndex;
      LineInfo lineInfo = this.GetLineInfo(lineIndex);
      lineInfo.Size = SizeF.Empty;
      lineInfo.StartBlock = block;
      lineInfo.EndBlock = (ITextBlock) null;
      return lineInfo;
    }

    protected virtual void AddLineDesiredSize(LineInfo line, ref SizeF desiredSize)
    {
      SizeF size = line.Size;
      if (line != this.lines.FirstLine)
        size.Height += (float) this.lineSpacing;
      desiredSize.Width = Math.Max(desiredSize.Width, size.Width);
      desiredSize.Height += size.Height;
    }

    protected virtual void AddBlockDesiredSize(SizeF blockDesiredSize, LineInfo line)
    {
      SizeF size = line.Size;
      size.Width += blockDesiredSize.Width;
      size.Height = Math.Max(size.Height, blockDesiredSize.Height);
      line.Size = size;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      switch (this.TextAlign)
      {
        case HorizontalAlignment.Right:
          return this.ArrangeWithRightAlignment(finalSize);
        case HorizontalAlignment.Center:
          return this.ArrangeWithCenterAlignment(finalSize);
        default:
          return this.ArrangeWithLeftAlignment(finalSize);
      }
    }

    protected virtual SizeF ArrangeWithLeftAlignment(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x = clientRectangle.X;
      float y = clientRectangle.Y;
      foreach (LineInfo line in (ReadOnlyCollection<LineInfo>) this.lines)
      {
        ITextBlock startBlock = line.StartBlock;
        ITextBlock endBlock = line.EndBlock;
        int index = startBlock.Index;
        if (TextBoxWrapPanel.IsWhitespace(startBlock.Text) && !TextBoxWrapPanel.IsTab(startBlock.Text) && this.lines.IndexOf(line) > 0)
          ++index;
        for (; index <= endBlock.Index; ++index)
        {
          RadElement child = this.Children[index];
          ITextBlock textBlock = child as ITextBlock;
          child.InvalidateArrange();
          PointF location = new PointF(x, y);
          SizeF desiredSize = textBlock.DesiredSize;
          float baselineOffset = this.GetBaselineOffset(line, textBlock);
          location.Y += (float) Math.Ceiling((double) baselineOffset);
          textBlock.Arrange(new RectangleF(location, desiredSize));
          if (textBlock == endBlock)
          {
            float width = (float) (endBlock.ControlBoundingRectangle.Right - startBlock.ControlBoundingRectangle.X);
            line.Size = new SizeF(width, line.Size.Height);
          }
          x += desiredSize.Width;
        }
        x = clientRectangle.X;
        y += line.Size.Height + (float) this.lineSpacing;
      }
      return finalSize;
    }

    protected virtual SizeF ArrangeWithCenterAlignment(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x = clientRectangle.X;
      float y = clientRectangle.Y;
      foreach (LineInfo line in (ReadOnlyCollection<LineInfo>) this.lines)
      {
        ITextBlock startBlock = line.StartBlock;
        ITextBlock endBlock = line.EndBlock;
        int index1 = startBlock.Index;
        List<RectangleF> rectangleFList = new List<RectangleF>();
        float width = 0.0f;
        for (; index1 <= endBlock.Index; ++index1)
        {
          RadElement child = this.Children[index1];
          ITextBlock textBlock = child as ITextBlock;
          child.InvalidateArrange();
          PointF location = new PointF(x, y);
          SizeF desiredSize = textBlock.DesiredSize;
          float num = this.GetBaselineOffset(line, textBlock);
          if (textBlock is TokenizedTextBlockElement)
            num = 0.0f;
          location.Y += num;
          rectangleFList.Add(new RectangleF(location, desiredSize));
          width += desiredSize.Width;
          if (textBlock == endBlock)
            line.Size = new SizeF(width, line.Size.Height);
          x += desiredSize.Width;
        }
        if ((double) width < (double) finalSize.Width)
        {
          float num = (float) (((double) finalSize.Width - (double) width) / 2.0);
          for (int index2 = 0; index2 < rectangleFList.Count; ++index2)
            rectangleFList[index2] = new RectangleF(rectangleFList[index2].X + num, rectangleFList[index2].Y, rectangleFList[index2].Width, rectangleFList[index2].Height);
        }
        for (int index2 = startBlock.Index; index2 <= endBlock.Index; ++index2)
          (this.Children[index2] as ITextBlock).Arrange(rectangleFList[index2 - startBlock.Index]);
        x = clientRectangle.X;
        y += line.Size.Height + (float) this.lineSpacing;
      }
      return finalSize;
    }

    protected virtual SizeF ArrangeWithRightAlignment(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float right = clientRectangle.Right;
      float y = clientRectangle.Y;
      foreach (LineInfo line in (ReadOnlyCollection<LineInfo>) this.lines)
      {
        ITextBlock startBlock = line.StartBlock;
        ITextBlock endBlock = line.EndBlock;
        for (int index = endBlock.Index; index >= startBlock.Index; --index)
        {
          RadElement child = this.Children[index];
          ITextBlock textBlock = child as ITextBlock;
          child.InvalidateArrange();
          PointF location = new PointF(right, y);
          SizeF desiredSize = textBlock.DesiredSize;
          float num = this.GetBaselineOffset(line, textBlock);
          if (textBlock is TokenizedTextBlockElement)
            num = 0.0f;
          location.Y += num;
          location.X -= desiredSize.Width;
          textBlock.Arrange(new RectangleF(location, desiredSize));
          if (textBlock == endBlock)
          {
            float width = (float) (endBlock.ControlBoundingRectangle.Right - startBlock.ControlBoundingRectangle.X);
            line.Size = new SizeF(width, line.Size.Height);
          }
          right -= desiredSize.Width;
        }
        right = clientRectangle.Right;
        y += line.Size.Height + (float) this.lineSpacing;
      }
      return finalSize;
    }

    protected virtual float GetBaselineOffset(LineInfo line, ITextBlock textBlock)
    {
      return (float) ((double) line.Size.Height / 2.0 - (double) textBlock.DesiredSize.Height / 2.0);
    }

    protected override void ToggleTextPrimitive(RadProperty property)
    {
    }

    protected virtual bool AreSplittedBlock(ITextBlock firstBlock, ITextBlock secondBlock)
    {
      if (firstBlock == secondBlock || firstBlock == null || secondBlock == null)
        return false;
      string text1 = firstBlock.Text;
      string text2 = secondBlock.Text;
      return !TextBoxWrapPanel.IsLineFeed(text1) && !TextBoxWrapPanel.IsLineFeed(text2) && (!TextBoxWrapPanel.IsCarriageReturn(text1) && !TextBoxWrapPanel.IsCarriageReturn(text2)) && (!TextBoxWrapPanel.IsTabOrWhitespace(text1) && !TextBoxWrapPanel.IsTabOrWhitespace(text2));
    }

    protected internal ITextBlock GetNextBlock(int index)
    {
      if (index < this.Children.Count - 1)
        return this.Children[index + 1] as ITextBlock;
      return (ITextBlock) null;
    }

    protected internal ITextBlock GetPreviousBlock(int index)
    {
      if (index > 0)
        return this.Children[index - 1] as ITextBlock;
      return (ITextBlock) null;
    }

    protected override void PaintText(IGraphics graphics)
    {
    }

    public bool HasText()
    {
      int count = this.Children.Count;
      if (count == 1)
        return !string.IsNullOrEmpty((this.Children[0] as ITextBlock).Text);
      return count > 0;
    }

    private void LoadElementTree()
    {
      if (this.ElementTree == null)
        return;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control == null || control.IsLoaded || this.Children.Count <= 0)
        return;
      control.LoadElementTree();
    }

    protected LineInfo GetLineInfo(int lineIndex)
    {
      LineInfo line;
      if (lineIndex < this.lines.Count)
      {
        line = this.lines[lineIndex];
      }
      else
      {
        line = new LineInfo();
        this.lines.Add(line);
      }
      return line;
    }

    protected virtual ITextBlock CreateBlock(string text, System.Type type)
    {
      ITextBlock block = (ITextBlock) null;
      if ((object) type == (object) TextBoxWrapPanel.TextBlockElementType)
        block = (ITextBlock) new TextBlockElement();
      ITextBlock textBlock = this.NotifyCreateTextBlock(block, text);
      if (textBlock == null)
        throw new InvalidOperationException("ITextBlock cannot be null.");
      textBlock.Text = text;
      return textBlock;
    }

    public void Clear()
    {
      this.Text = string.Empty;
    }

    public string GetTextRange(TextPosition startPosition, TextPosition endPosition)
    {
      if (startPosition == (TextPosition) null)
        throw new ArgumentNullException(nameof (startPosition));
      if (endPosition == (TextPosition) null)
        throw new ArgumentNullException(nameof (endPosition));
      TextPosition.Swap(ref startPosition, ref endPosition);
      StringBuilder stringBuilder = new StringBuilder();
      ITextBlock textBlock1 = startPosition.TextBlock;
      ITextBlock textBlock2 = endPosition.TextBlock;
      if (object.Equals((object) textBlock1, (object) textBlock2))
        return this.GetBlockText(textBlock1, startPosition.CharPosition, endPosition.CharPosition - startPosition.CharPosition);
      stringBuilder.Append(this.GetBlockText(textBlock1, startPosition.CharPosition, textBlock1.Length - startPosition.CharPosition));
      for (int index = textBlock1.Index + 1; index < textBlock2.Index && index < this.Children.Count; ++index)
      {
        ITextBlock child = this.Children[index] as ITextBlock;
        if (child != null)
          stringBuilder.Append(this.GetBlockText(child, 0, child.Length));
      }
      stringBuilder.Append(this.GetBlockText(textBlock2, 0, endPosition.CharPosition));
      return stringBuilder.ToString();
    }

    protected virtual string GetBlockText(ITextBlock block, int start, int length)
    {
      if (length < 0)
        return block.Text.Substring(start);
      if (TextBoxWrapPanel.IsLineFeed(block.Text))
        return block.Text;
      return block.Text.Substring(start, length);
    }

    public ITextBlock BinarySearchTextBlockByXCoordinate(LineInfo line, float x)
    {
      IComparer comparer = (IComparer) new TextBlockXComparer(x);
      return this.BinarySearch(line, comparer);
    }

    public ITextBlock BinarySearchTextBlockByOffset(LineInfo line, int offset)
    {
      IComparer comparer = (IComparer) new TextBlockOffsetComparer(offset);
      return this.BinarySearch(line, comparer);
    }

    protected virtual ITextBlock BinarySearch(LineInfo line, IComparer comparer)
    {
      int count = line.EndBlock.Index - line.StartBlock.Index + 1;
      int index = this.Children.BinarySearch(line.StartBlock.Index, count, (RadElement) null, comparer);
      if (index >= 0)
        return this.Children[index] as ITextBlock;
      return (ITextBlock) null;
    }

    public static bool IsSpecialText(string text)
    {
      if (!TextBoxWrapPanel.IsWhitespace(text) && !TextBoxWrapPanel.IsTab(text) && (!TextBoxWrapPanel.IsLineFeed(text) && !TextBoxWrapPanel.IsCarriageReturn(text)) && !(text == Environment.NewLine))
        return TextBoxWrapPanel.ContainsNewLine(text);
      return true;
    }

    public static bool ContainsNewLine(string text)
    {
      if (string.IsNullOrEmpty(text))
        return false;
      string str1 = text;
      char[] separator = new char[3]{ ' ', ',', '.' };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        if (!string.IsNullOrEmpty(str2) && str2.Contains(Environment.NewLine))
          return true;
      }
      return false;
    }

    public static bool IsWhitespace(string text)
    {
      if (string.IsNullOrEmpty(text))
        return false;
      for (int index = 0; index < text.Length; ++index)
      {
        if (text[index] != ' ')
          return false;
      }
      return true;
    }

    public static bool IsTab(string text)
    {
      if (string.IsNullOrEmpty(text))
        return false;
      return text == '\t'.ToString();
    }

    public static bool IsTabOrWhitespace(string text)
    {
      if (!TextBoxWrapPanel.IsTab(text))
        return TextBoxWrapPanel.IsWhitespace(text);
      return true;
    }

    public static bool IsLineFeed(string text)
    {
      return text.Length == 1 && text[0] == '\n';
    }

    public static bool IsCarriageReturn(string text)
    {
      return text.Length == 1 && text[0] == '\r';
    }
  }
}
