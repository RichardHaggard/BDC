// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Interfaces;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TextBoxViewElement : TextBoxWrapPanel
  {
    private SizeF scrollOffset = (SizeF) Size.Empty;
    private bool isReadOnly;
    private TextBoxScroller vScroller;
    private TextBoxScroller hScroller;
    private TextBoxSelectionPrimitive selectionPrimitive;
    private int editingCount;
    private char passwordChar;
    private bool useSystemPasswordChar;

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.hScroller != null)
      {
        this.hScroller.Dispose();
        this.hScroller = (TextBoxScroller) null;
      }
      if (this.vScroller == null)
        return;
      this.vScroller.Dispose();
      this.vScroller = (TextBoxScroller) null;
    }

    public virtual bool UseSystemPasswordChar
    {
      get
      {
        return this.useSystemPasswordChar;
      }
      set
      {
        if (this.useSystemPasswordChar == value)
          return;
        this.useSystemPasswordChar = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (UseSystemPasswordChar));
      }
    }

    public virtual char PasswordChar
    {
      get
      {
        return this.passwordChar;
      }
      set
      {
        if ((int) this.passwordChar == (int) value)
          return;
        this.passwordChar = value;
        this.InvalidateLayout();
        this.OnNotifyPropertyChanged(nameof (PasswordChar));
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.isReadOnly;
      }
      set
      {
        if (this.isReadOnly == value)
          return;
        this.isReadOnly = value;
        this.OnNotifyPropertyChanged(nameof (IsReadOnly));
      }
    }

    public TextBoxScroller VScroller
    {
      get
      {
        return this.vScroller;
      }
      set
      {
        if (value == this.vScroller)
          return;
        if (this.vScroller != null)
          this.vScroller.ScrollerUpdated -= new EventHandler(this.VScroller_ScrollerUpdated);
        this.vScroller = value;
        if (this.vScroller == null)
          return;
        this.vScroller.ScrollerUpdated += new EventHandler(this.VScroller_ScrollerUpdated);
      }
    }

    public TextBoxScroller HScroller
    {
      get
      {
        return this.hScroller;
      }
      set
      {
        if (value == this.hScroller)
          return;
        if (this.hScroller != null)
          this.hScroller.ScrollerUpdated += new EventHandler(this.HScroller_ScrollerUpdated);
        this.hScroller = value;
        if (this.hScroller == null)
          return;
        this.hScroller.ScrollerUpdated += new EventHandler(this.HScroller_ScrollerUpdated);
      }
    }

    public TextBoxSelectionPrimitive SelectionPrimitive
    {
      get
      {
        return this.selectionPrimitive;
      }
      set
      {
        this.selectionPrimitive = value;
      }
    }

    public SizeF ScrollOffset
    {
      get
      {
        return this.scrollOffset;
      }
      set
      {
        if (!(this.scrollOffset != value))
          return;
        this.scrollOffset = value;
        this.InvalidateArrange();
        this.UpdateLayout();
        this.Invalidate();
      }
    }

    public bool IsEditing
    {
      get
      {
        return this.editingCount > 0;
      }
    }

    private void VScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.SetScrollOffset(ScrollType.Vertical);
    }

    private void HScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.SetScrollOffset(ScrollType.Horizontal);
    }

    protected override void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      base.OnPropertyChanging(e);
      if (e.Cancel || !(e.PropertyName == "Multiline") && !(e.PropertyName == "WordWrap"))
        return;
      this.selectionPrimitive.TextBoxElement.Navigator.SaveSelection();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Multiline" || e.PropertyName == "WordWrap")
        this.selectionPrimitive.TextBoxElement.Navigator.RestoreSelection();
      base.OnNotifyPropertyChanged(e);
    }

    protected override void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      if (this.selectionPrimitive == null || !this.Multiline || !this.WordWrap)
        return;
      this.selectionPrimitive.TextBoxElement.Navigator.RestoreSelection();
    }

    protected override void PaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      this.selectionPrimitive.PaintPrimitive(graphics, angle, scale);
      base.PaintChildren(graphics, clipRectange, angle, scale, useRelativeTransformation);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Multiline && this.WordWrap && !this.IsEditing)
        this.selectionPrimitive.TextBoxElement.Navigator.SaveSelection();
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ClampDesiredSize(SizeF desiredSize, SizeF availableSize)
    {
      if (float.IsPositiveInfinity(availableSize.Width) || (double) availableSize.Width == 2147483648.0)
        availableSize.Width = desiredSize.Width;
      if (float.IsPositiveInfinity(availableSize.Height) || (double) availableSize.Height == 2147483648.0)
        availableSize.Height = desiredSize.Height;
      this.UpdateScrollRange(availableSize, desiredSize);
      return base.ClampDesiredSize(desiredSize, availableSize);
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      RectangleF clientRectangle = base.GetClientRectangle(finalSize);
      clientRectangle.X += this.scrollOffset.Width;
      clientRectangle.Y += this.scrollOffset.Height;
      return clientRectangle;
    }

    private void UpdateScrollRange(SizeF availableSize, SizeF desiredSize)
    {
      if (this.vScroller != null)
      {
        this.vScroller.UpdateScrollRange(availableSize, desiredSize);
        this.SetScrollOffset(ScrollType.Vertical);
      }
      if (this.hScroller == null)
        return;
      this.hScroller.UpdateScrollRange(availableSize, desiredSize);
      this.SetScrollOffset(ScrollType.Horizontal);
    }

    public void BeginEditUpdate()
    {
      ++this.editingCount;
    }

    public void EndEditUpdate()
    {
      this.EndEditUpdate(false, (string) null, 0, TextBoxChangeAction.TextEdit);
    }

    public virtual void EndEditUpdate(
      bool notify,
      string newText,
      int caretPosition,
      TextBoxChangeAction action)
    {
      if (this.editingCount <= 0)
        return;
      --this.editingCount;
      if (!notify)
        return;
      this.NotifyTextChanged(newText, caretPosition, action);
    }

    public PointF PointToAbsolute(PointF pointF)
    {
      pointF.X -= this.ScrollOffset.Width;
      pointF.Y -= this.ScrollOffset.Height;
      return pointF;
    }

    public PointF GetLocation(TextPosition position)
    {
      if (position == (TextPosition) null)
        throw new ArgumentNullException(nameof (position));
      PointF location = position.TextBlock.GetRectangleFromCharacterIndex(position.CharPosition, false).Location;
      location.Y = position.Line.ControlBoundingRectangle.Y;
      return this.PointToAbsolute(location);
    }

    public bool Delete(TextPosition startPosition, TextPosition endPosition)
    {
      return this.Replace(startPosition, endPosition, string.Empty);
    }

    public bool Insert(TextPosition startPosition, string text)
    {
      return this.Replace(startPosition, startPosition, text);
    }

    public bool Replace(TextPosition startPosition, TextPosition endPosition, string text)
    {
      TextPosition.Swap(ref startPosition, ref endPosition);
      if (endPosition == (TextPosition) null && string.IsNullOrEmpty(text))
        return false;
      TextBoxChangeAction action = TextBoxChangeAction.TextEdit;
      int length = TextPosition.GetLength(startPosition, endPosition);
      string textRange = this.GetTextRange(startPosition, endPosition);
      if (textRange == text || !this.NotifyTextChanging((int) startPosition, length, textRange, text, action))
        return false;
      int caretPosition = (int) startPosition + (string.IsNullOrEmpty(text) ? 0 : text.Length);
      this.BeginEditUpdate();
      this.ReplaceOverride(startPosition, endPosition, text);
      this.InvalidateLayout();
      this.Invalidate();
      this.EndEditUpdate(true, text, caretPosition, action);
      return true;
    }

    protected virtual void ReplaceOverride(
      TextPosition startPosition,
      TextPosition endPosition,
      string text)
    {
      ITextBlock textBlock1 = startPosition.TextBlock;
      ITextBlock textBlock2 = endPosition.TextBlock;
      int startCharPosition = startPosition.CharPosition;
      int endCharPosition = endPosition.CharPosition;
      ITextBlock targetBlock = textBlock1;
      if (textBlock1 != textBlock2)
      {
        int position = -1;
        targetBlock = this.RemoveBlockRange(textBlock1, startCharPosition, textBlock2, endCharPosition, ref position);
        startCharPosition = position;
        endCharPosition = position;
      }
      this.ReplaceTextRange(targetBlock, startCharPosition, endCharPosition, text);
    }

    protected virtual void ReplaceTextRange(
      ITextBlock targetBlock,
      int startCharPosition,
      int endCharPosition,
      string text)
    {
      if (startCharPosition == endCharPosition && string.IsNullOrEmpty(text))
        return;
      if (targetBlock == null)
      {
        this.InsertTextBlocks(0, text, TextBoxWrapPanel.TextBlockElementType);
      }
      else
      {
        bool flag = string.IsNullOrEmpty(targetBlock.Text) || TextBoxWrapPanel.IsSpecialText(targetBlock.Text);
        int index = targetBlock.Index;
        if (flag)
          this.ReplaceSpecialTextBlock(targetBlock, startCharPosition, endCharPosition, text);
        else
          this.ReplaceTextBlock(targetBlock, startCharPosition, endCharPosition, text);
        if (!string.IsNullOrEmpty(targetBlock.Text) || !this.Children.Contains(targetBlock as RadElement))
          return;
        this.Children.Remove(targetBlock as RadElement);
      }
    }

    protected virtual void ReplaceSpecialTextBlock(
      ITextBlock targetBlock,
      int startCharPosition,
      int endCharPosition,
      string text)
    {
      int index = targetBlock.Index;
      if (startCharPosition != endCharPosition)
        targetBlock.Text = string.Empty;
      else if (startCharPosition == targetBlock.Length)
        ++index;
      int num = this.InsertTextBlocks(index, text, TextBoxWrapPanel.TextBlockElementType);
      if (startCharPosition == endCharPosition)
        return;
      targetBlock.Index = num + 1;
    }

    protected virtual void ReplaceTextBlock(
      ITextBlock targetBlock,
      int startCharPosition,
      int endCharPosition,
      string text)
    {
      if (string.IsNullOrEmpty(text) && startCharPosition == 0 && endCharPosition == targetBlock.Length)
      {
        this.Children.Remove(targetBlock as RadElement);
      }
      else
      {
        string str = targetBlock.Text.Substring(0, startCharPosition);
        string text1 = targetBlock.Text.Substring(endCharPosition);
        targetBlock.Text = str;
        if (TextBoxWrapPanel.IsSpecialText(text))
        {
          int num = this.InsertTextBlocks(string.IsNullOrEmpty(str) ? targetBlock.Index : targetBlock.Index + 1, text, TextBoxWrapPanel.TextBlockElementType);
          if (string.IsNullOrEmpty(str))
            targetBlock.Index = num + 1;
          if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(text1))
          {
            this.InsertTextBlocks(num + 1, text1, TextBoxWrapPanel.TextBlockElementType);
            return;
          }
        }
        else
          targetBlock.Text += text;
        targetBlock.Text += text1;
      }
    }

    protected virtual ITextBlock RemoveBlockRange(
      ITextBlock startBlock,
      int startCharPosition,
      ITextBlock endBlock,
      int endCharPosition,
      ref int position)
    {
      int index1 = startBlock.Index;
      if (startCharPosition > 0 || TextBoxWrapPanel.IsLineFeed(startBlock.Text))
      {
        if (startCharPosition > 0 && startCharPosition != startBlock.Length)
        {
          string str = startBlock.Text.Substring(0, startCharPosition);
          startBlock.Text = str;
        }
        ++index1;
      }
      int index2 = endBlock.Index;
      if (endCharPosition >= 0)
      {
        if (endCharPosition != endBlock.Length)
          --index2;
        if (endCharPosition < endBlock.Text.Length)
        {
          string str = endBlock.Text.Substring(endCharPosition);
          endBlock.Text = str;
        }
      }
      for (int index3 = index2 - index1 + 1; index3 > 0 && this.Children.Count > index1; --index3)
        this.Children.RemoveAt(index1);
      ITextBlock textBlock = (ITextBlock) null;
      if (index1 == 0 && this.Children.Count > 0)
      {
        textBlock = this.Children[0] as ITextBlock;
        textBlock.Index = 0;
        position = 0;
      }
      else if (index1 - 1 >= 0 && this.Children.Count > 0)
      {
        int index3 = index1 - 1;
        textBlock = this.Children[index3] as ITextBlock;
        textBlock.Index = index3;
        position = textBlock.Length;
      }
      else if (index1 + 1 < this.Children.Count)
      {
        textBlock = this.Children[index1 + 1] as ITextBlock;
        position = 0;
      }
      else
        position = -1;
      return textBlock;
    }

    private void SetScrollOffset(ScrollType scrollType)
    {
      if (scrollType == ScrollType.Vertical)
        this.ScrollOffset = new SizeF(this.ScrollOffset.Width, (float) -this.VScroller.Value);
      else
        this.ScrollOffset = new SizeF((float) -this.HScroller.Value, this.ScrollOffset.Height);
    }
  }
}
