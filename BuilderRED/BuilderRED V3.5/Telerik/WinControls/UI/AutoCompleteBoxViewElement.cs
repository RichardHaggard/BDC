// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AutoCompleteBoxViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.WinControls.UI
{
  public class AutoCompleteBoxViewElement : TextBoxViewElement
  {
    protected static readonly Type TokenizedTextBlockElementType = typeof (TokenizedTextBlockElement);
    protected static readonly SizeF InfinitySize = new SizeF(float.PositiveInfinity, float.PositiveInfinity);
    private char delimiter = ';';
    private bool showRemoveButton = true;
    private float minLineHeight = 18f;
    private StringBuilder editedTokenText = new StringBuilder();
    protected const float DefaultMinLineHeight = 18f;
    private const int DefaultBordersWidth = 4;
    private ITextBlock editedTokenEnd;
    private ITextBlock editedBlock;
    private bool tokenFound;
    private int suspendOnChildrenChangedCount;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.LineSpacing = 2;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool UseSystemPasswordChar
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override char PasswordChar
    {
      get
      {
        return char.MinValue;
      }
      set
      {
      }
    }

    public char Delimiter
    {
      get
      {
        return this.delimiter;
      }
      set
      {
        if ((int) value == (int) this.delimiter || !this.OnPropertyChanging(nameof (Delimiter), (object) this.delimiter, (object) value))
          return;
        string text = this.Text;
        string str = text.Replace(this.delimiter, value);
        if (!this.NotifyTextChanging(0, this.TextLength, text, str, TextBoxChangeAction.TextPropertyChange))
          return;
        this.delimiter = value;
        this.OnNotifyPropertyChanged(nameof (Delimiter));
        this.NotifyTextChanged(str, str.Length, TextBoxChangeAction.TextPropertyChange);
      }
    }

    public bool ShowRemoveButton
    {
      get
      {
        return this.showRemoveButton;
      }
      set
      {
        if (this.showRemoveButton == value || !this.OnPropertyChanging(nameof (ShowRemoveButton), (object) this.showRemoveButton, (object) value))
          return;
        this.showRemoveButton = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (ShowRemoveButton));
      }
    }

    protected internal float MinLineHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat(this.minLineHeight, this.DpiScaleFactor);
      }
      set
      {
        this.minLineHeight = value;
      }
    }

    protected override bool AreSplittedBlock(ITextBlock firstBlock, ITextBlock secondBlock)
    {
      if (firstBlock is TokenizedTextBlockElement || secondBlock is TokenizedTextBlockElement || firstBlock is TextBlockElement && secondBlock is TextBlockElement && firstBlock.Text.EndsWith(this.delimiter.ToString()))
        return false;
      return base.AreSplittedBlock(firstBlock, secondBlock);
    }

    protected bool IsDelimiter(ITextBlock textBlock)
    {
      string text = textBlock.Text;
      return !string.IsNullOrEmpty(text) && text.Length == 1 && (int) text[0] == (int) this.delimiter;
    }

    protected override void CreateTextBlocksOverride(string text)
    {
      this.InsertTextBlocks(0, text, AutoCompleteBoxViewElement.TokenizedTextBlockElementType);
    }

    protected override int InsertTextBlocks(int index, string text, Type blockType)
    {
      if ((object) blockType == (object) AutoCompleteBoxViewElement.TokenizedTextBlockElementType)
        return this.InsertTokenizedTextBlocks(index, text, true);
      return base.InsertTextBlocks(index, text, blockType);
    }

    protected virtual int InsertTokenizedTextBlocks(
      int index,
      string text,
      bool performInvalidation)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < text.Length; ++index1)
      {
        char ch = text[index1];
        if ((int) ch == (int) this.delimiter)
        {
          if (stringBuilder.Length > 0)
          {
            string str = stringBuilder.ToString();
            if (!performInvalidation || this.OnTokenValidating(str))
            {
              ITextBlock block = this.CreateBlock(str, AutoCompleteBoxViewElement.TokenizedTextBlockElementType);
              block.Index = index;
              this.Children.Insert(index, block as RadElement);
            }
            else
            {
              stringBuilder.Append(ch);
              string text1 = stringBuilder.ToString();
              index = base.InsertTextBlocks(index, text1, TextBoxWrapPanel.TextBlockElementType);
            }
            ++index;
            stringBuilder = new StringBuilder();
          }
        }
        else if ((ch == '\n' || ch == '\r') && stringBuilder.Length >= 0)
        {
          stringBuilder.Append(ch);
          index = base.InsertTextBlocks(index, stringBuilder.ToString(), TextBoxWrapPanel.TextBlockElementType) + 1;
          stringBuilder = new StringBuilder();
        }
        else
          stringBuilder.Append(ch);
      }
      if (stringBuilder.Length > 0)
        index = base.InsertTextBlocks(index, stringBuilder.ToString(), TextBoxWrapPanel.TextBlockElementType) + 1;
      return index - 1;
    }

    protected override string GetBlockText(ITextBlock block, int start, int length)
    {
      string empty = string.Empty;
      if (block is TokenizedTextBlockElement && length + start == block.Length)
      {
        if (length > 0)
          --length;
        else if (start > 0)
          --start;
        empty += (string) (object) this.delimiter;
      }
      if (start + length <= block.Length)
        return base.GetBlockText(block, start, length) + empty;
      return string.Empty;
    }

    protected override void ReplaceOverride(
      TextPosition startPosition,
      TextPosition endPosition,
      string text)
    {
      if (text.Contains(this.delimiter.ToString()) && object.Equals((object) startPosition, (object) endPosition))
      {
        string text1 = (string) null;
        this.InsertTextBlocks(this.RemoveEditableBlockRange(startPosition.TextBlock, startPosition.CharPosition, out text1), text1 + text, AutoCompleteBoxViewElement.TokenizedTextBlockElementType);
      }
      else
        base.ReplaceOverride(startPosition, endPosition, text);
    }

    protected virtual int RemoveEditableBlockRange(
      ITextBlock tailBlock,
      int startCharPosition,
      out string text)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int index = tailBlock.Index;
      string str = this.delimiter.ToString();
      while (index >= 0)
      {
        ITextBlock child = this.Children[index] as ITextBlock;
        if (child == tailBlock && startCharPosition == 0)
        {
          --index;
        }
        else
        {
          string text1 = child.Text;
          if (child is TokenizedTextBlockElement || TextBoxWrapPanel.IsLineFeed(text1))
          {
            ++index;
            break;
          }
          if (!TextBoxWrapPanel.IsCarriageReturn(text1))
          {
            bool flag = true;
            if (child == tailBlock)
            {
              child.Text = text1.Substring(startCharPosition);
              text1 = text1.Substring(0, startCharPosition);
              flag = string.IsNullOrEmpty(child.Text);
            }
            else if (text1.EndsWith(str))
            {
              ++index;
              break;
            }
            if (flag)
              this.Children.RemoveAt(index);
            stringBuilder.Insert(0, text1);
            --index;
          }
          else
            break;
        }
      }
      if (index < 0)
        index = 0;
      text = stringBuilder.ToString();
      return index;
    }

    protected override void ReplaceTextRange(
      ITextBlock targetBlock,
      int startCharPosition,
      int endCharPosition,
      string text)
    {
      bool flag = text.Contains(this.delimiter.ToString());
      if (targetBlock is TokenizedTextBlockElement && !string.IsNullOrEmpty(text))
      {
        int index1 = targetBlock.Index;
        if (startCharPosition == 0 && endCharPosition == targetBlock.Length)
          this.Children.Remove(targetBlock as RadElement);
        else if (startCharPosition == 0 && targetBlock.Index > 0)
        {
          index1 = targetBlock.Index - 1;
          targetBlock = this.Children[index1] as ITextBlock;
          startCharPosition = endCharPosition = targetBlock.Length;
        }
        else if (endCharPosition == targetBlock.Length && targetBlock.Index < this.Children.Count - 1)
        {
          index1 = targetBlock.Index + 1;
          targetBlock = this.Children[index1] as ITextBlock;
          startCharPosition = endCharPosition = 0;
        }
        targetBlock.Index = index1;
        if (targetBlock is TokenizedTextBlockElement)
        {
          int index2 = startCharPosition == 0 ? index1 : index1 + 1;
          Type blockType = flag ? AutoCompleteBoxViewElement.TokenizedTextBlockElementType : TextBoxWrapPanel.TextBlockElementType;
          this.InsertTextBlocks(index2, text, blockType);
          return;
        }
      }
      else if (flag)
      {
        if (targetBlock != null)
        {
          string str = targetBlock.Text.Substring(0, startCharPosition);
          string text1 = targetBlock.Text.Substring(endCharPosition);
          targetBlock.Text = str;
          int num = this.InsertTextBlocks(string.IsNullOrEmpty(str) ? targetBlock.Index : targetBlock.Index + 1, text, AutoCompleteBoxViewElement.TokenizedTextBlockElementType);
          if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(text1))
          {
            this.InsertTextBlocks(num + 1, text1, TextBoxWrapPanel.TextBlockElementType);
            return;
          }
          targetBlock.Text += text1;
          if (!string.IsNullOrEmpty(targetBlock.Text))
            return;
          this.Children.Remove(targetBlock as RadElement);
          return;
        }
        text = string.Empty;
      }
      base.ReplaceTextRange(targetBlock, startCharPosition, endCharPosition, text);
      this.editedBlock = targetBlock is TextBlockElement ? targetBlock : (ITextBlock) null;
    }

    protected override bool SplitBlock(
      LineInfo currentLine,
      SizeF availableSize,
      ref ITextBlock textBlock,
      ref int offset)
    {
      if (textBlock is TokenizedTextBlockElement)
        return false;
      return base.SplitBlock(currentLine, availableSize, ref textBlock, ref offset);
    }

    protected override ITextBlock CreateBlock(string text, Type type)
    {
      ITextBlock block = (ITextBlock) null;
      if ((object) type == (object) AutoCompleteBoxViewElement.TokenizedTextBlockElementType)
        block = (ITextBlock) new TokenizedTextBlockElement();
      else if ((object) type == (object) TextBoxWrapPanel.TextBlockElementType)
        block = (ITextBlock) new TextBlockElement();
      ITextBlock textBlock = this.NotifyCreateTextBlock(block, text);
      if (textBlock == null)
        throw new InvalidOperationException("ITextBlock cannot be null");
      textBlock.Text = text;
      return textBlock;
    }

    public event TokenValidatingEventHandler TokenValidating;

    protected virtual void OnTokenValidating(TokenValidatingEventArgs e)
    {
      TokenValidatingEventHandler tokenValidating = this.TokenValidating;
      if (tokenValidating == null)
        return;
      tokenValidating((object) this, e);
    }

    protected bool OnTokenValidating(string tokenText)
    {
      TokenValidatingEventArgs e = new TokenValidatingEventArgs(tokenText);
      this.OnTokenValidating(e);
      return e.IsValidToken;
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "IsReadOnly"))
        return;
      this.InvalidateMeasure();
      this.InvalidateArrange();
      this.UpdateLayout();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.ElementTree.RootElement.ControlDefaultSize.Height > 0)
        this.MinLineHeight = (float) (this.ElementTree.RootElement.ControlDefaultSize.Height - 4);
      if ((double) this.MinLineHeight == 0.0)
      {
        ITextBlock block = this.CreateBlock("X", AutoCompleteBoxViewElement.TokenizedTextBlockElementType);
        ++this.suspendOnChildrenChangedCount;
        this.Children.Add(block as RadElement);
        this.MeasureTextBlock(block, AutoCompleteBoxViewElement.InfinitySize);
        this.MinLineHeight = block.DesiredSize.Height;
        this.Children.Remove(block as RadElement);
        --this.suspendOnChildrenChangedCount;
      }
      availableSize = base.MeasureOverride(availableSize);
      return availableSize;
    }

    protected override void AddBlockDesiredSize(SizeF blockDesiredSize, LineInfo line)
    {
      base.AddBlockDesiredSize(blockDesiredSize, line);
      SizeF size = line.Size;
      float num = Math.Max(size.Height, this.MinLineHeight);
      this.MinLineHeight = TelerikDpiHelper.ScaleFloat(num, new SizeF(1f / this.DpiScaleFactor.Width, 1f / this.DpiScaleFactor.Height));
      size.Height = num;
      line.Size = size;
    }

    protected override void MergeAndMeasureBlock(ITextBlock textBlock, SizeF availableSize)
    {
      base.MergeAndMeasureBlock(textBlock, availableSize);
      if (this.tokenFound)
        return;
      this.FindToken(textBlock);
    }

    private void FindToken(ITextBlock textBlock)
    {
      if (this.editedBlock == null)
        return;
      if ((this.editedBlock as RadElement).Parent == null)
        this.editedBlock = textBlock;
      string text = textBlock.Text;
      bool flag1 = textBlock is TokenizedTextBlockElement || TextBoxWrapPanel.IsCarriageReturn(text) || TextBoxWrapPanel.IsLineFeed(text);
      bool flag2 = text.EndsWith(this.delimiter.ToString());
      if (flag1 || flag2)
      {
        if (flag2 && this.editedBlock.Index <= textBlock.Index)
        {
          this.tokenFound = true;
        }
        else
        {
          this.editedTokenEnd = (ITextBlock) null;
          this.editedTokenText = new StringBuilder();
          return;
        }
      }
      this.editedTokenEnd = textBlock;
      this.editedTokenText.Append(textBlock.Text);
    }

    protected override void MeasureTextBlockOverride(ITextBlock textBlock, SizeF availableSize)
    {
      if (textBlock is TokenizedTextBlockElement)
      {
        textBlock.Measure(new SizeF(AutoCompleteBoxViewElement.InfinitySize.Width, this.MinLineHeight));
      }
      else
      {
        RadElement radElement = textBlock as RadElement;
        radElement.MaxSize = radElement.MinSize = new Size(0, (int) Math.Round((double) this.MinLineHeight, MidpointRounding.AwayFromZero));
        base.MeasureTextBlockOverride(textBlock, availableSize);
      }
    }

    protected override void OnTextChanged(EventArgs e)
    {
      if (this.tokenFound)
      {
        string tokenText = this.editedTokenText.ToString().Replace(this.delimiter.ToString(), string.Empty);
        if (this.OnTokenValidating(tokenText))
        {
          this.BeginEditUpdate();
          string text = (string) null;
          int index = this.InsertTokenizedTextBlocks(this.RemoveEditableBlockRange(this.editedTokenEnd, this.editedTokenEnd.Length, out text), tokenText + (object) this.delimiter, false);
          this.InvalidateLayout();
          this.EndEditUpdate();
          ITextBlock child = this.Children[index] as ITextBlock;
          TextBoxChangedEventArgs changedEventArgs = e as TextBoxChangedEventArgs;
          e = (EventArgs) new TextBoxChangedEventArgs(changedEventArgs.Text, child.Offset + child.Length, changedEventArgs.Action);
        }
        this.tokenFound = false;
      }
      this.editedBlock = (ITextBlock) null;
      this.editedTokenEnd = (ITextBlock) null;
      this.editedTokenText = new StringBuilder();
      base.OnTextChanged(e);
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      if (this.suspendOnChildrenChangedCount != 0)
        return;
      base.OnChildrenChanged(child, changeOperation);
    }

    protected override void OnTextBlockFormatting(TextBlockFormattingEventArgs e)
    {
      TokenizedTextBlockElement textBlock = e.TextBlock as TokenizedTextBlockElement;
      if (textBlock != null)
        textBlock.AllowRemove = !this.IsReadOnly && this.ShowRemoveButton;
      base.OnTextBlockFormatting(e);
    }

    protected override bool MeasureWrap(
      SizeF availableSize,
      int blockIndex,
      ref SizeF desiredSize,
      ref int currentLineIndex,
      ref int offset)
    {
      int num = currentLineIndex;
      bool flag = base.MeasureWrap(availableSize, blockIndex, ref desiredSize, ref currentLineIndex, ref offset);
      if (num != currentLineIndex && this.GetLineInfo(currentLineIndex).StartBlock is TokenizedTextBlockElement)
        ++offset;
      return flag;
    }
  }
}
