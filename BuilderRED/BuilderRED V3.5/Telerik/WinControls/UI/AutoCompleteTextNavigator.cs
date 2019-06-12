// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AutoCompleteTextNavigator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class AutoCompleteTextNavigator : TextBoxNavigator
  {
    public AutoCompleteTextNavigator(RadAutoCompleteBoxElement owner)
      : base((RadTextBoxControlElement) owner)
    {
    }

    public override TextPosition GetNextPosition(TextPosition position)
    {
      TextPosition nextPosition = base.GetNextPosition(position);
      ITextBlock textBlock = nextPosition.TextBlock;
      if (textBlock is TokenizedTextBlockElement && !this.IsValidCharPosition(nextPosition.CharPosition, textBlock.Length))
        nextPosition.CharPosition = nextPosition.Line == position.Line ? textBlock.Length : 0;
      return nextPosition;
    }

    public override TextPosition GetPreviousPosition(TextPosition position)
    {
      TextPosition previousPosition = base.GetPreviousPosition(position);
      ITextBlock textBlock = previousPosition.TextBlock;
      if (textBlock is TokenizedTextBlockElement && !this.IsValidCharPosition(previousPosition.CharPosition, textBlock.Length))
      {
        int charPosition = previousPosition.CharPosition;
        int num = previousPosition.Line != position.Line ? textBlock.Length : (position.CharPosition != 1 || !TextBoxWrapPanel.IsCarriageReturn(position.TextBlock.Text) ? 0 : textBlock.Length);
        previousPosition.CharPosition = num;
      }
      return previousPosition;
    }

    public override TextPosition GetPositionFromOffset(int offset)
    {
      TextPosition positionFromOffset = base.GetPositionFromOffset(offset);
      this.ClampPosition(positionFromOffset);
      return positionFromOffset;
    }

    public override TextPosition GetPositionFromPoint(PointF point)
    {
      TextPosition positionFromPoint = base.GetPositionFromPoint(point);
      this.ClampPosition(positionFromPoint);
      return positionFromPoint;
    }

    public TextPosition GetPreviousEditablePosition(TextPosition position)
    {
      return this.GetEditablePosition(position, false);
    }

    public TextPosition GetNextEditablePosition(TextPosition position)
    {
      return this.GetEditablePosition(position, true);
    }

    protected virtual TextPosition GetEditablePosition(TextPosition position, bool next)
    {
      if (position == (TextPosition) null)
        return (TextPosition) null;
      TextBoxViewElement viewElement = this.TextBoxElement.ViewElement;
      ITextBlock textBlock1 = position.TextBlock;
      if (textBlock1 is TokenizedTextBlockElement)
      {
        if (position.CharPosition == 0 && textBlock1.Index > 0)
          textBlock1 = viewElement.Children[textBlock1.Index - 1] as ITextBlock;
        else if (position.CharPosition == textBlock1.Length && textBlock1.Index < viewElement.Children.Count - 1)
          textBlock1 = viewElement.Children[textBlock1.Index + 1] as ITextBlock;
      }
      ITextBlock textBlock2 = textBlock1;
      ITextBlock child;
      for (; textBlock1 is TextBlockElement; textBlock1 = child)
      {
        textBlock2 = textBlock1;
        int index = next ? textBlock1.Index + 1 : textBlock1.Index - 1;
        if (index >= 0 && index < viewElement.Children.Count)
        {
          child = viewElement.Children[index] as ITextBlock;
          if (TextBoxWrapPanel.IsLineFeed(child.Text) || TextBoxWrapPanel.IsCarriageReturn(child.Text))
            break;
        }
        else
          break;
      }
      if (textBlock2 is TextBlockElement)
        return new TextPosition(viewElement.Lines.BinarySearchByBlockIndex(textBlock2.Index), textBlock2, next ? textBlock2.Length : 0);
      return position;
    }

    private bool IsValidCharPosition(int charPosition, int length)
    {
      return charPosition == 0 || charPosition == length;
    }

    private void ClampPosition(TextPosition textPosition)
    {
      if (textPosition == (TextPosition) null)
        return;
      ITextBlock textBlock = textPosition.TextBlock;
      if (!(textBlock is TokenizedTextBlockElement))
        return;
      if (textPosition.CharPosition <= textBlock.Length / 2)
        textPosition.CharPosition = 0;
      else
        textPosition.CharPosition = textBlock.Length;
    }
  }
}
