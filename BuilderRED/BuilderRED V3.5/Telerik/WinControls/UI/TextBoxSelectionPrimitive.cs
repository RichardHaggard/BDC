// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxSelectionPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class TextBoxSelectionPrimitive : IPrimitive
  {
    private Color selectionColor = SystemColors.Highlight;
    private int selectionOpacity = 100;
    private bool hideSelection = true;
    private bool shouldPaint = true;
    private readonly RadTextBoxControlElement textBoxElement;
    private RectangleF[] selectionPolygons;

    public TextBoxSelectionPrimitive(RadTextBoxControlElement textBox)
    {
      this.textBoxElement = textBox;
    }

    public bool ShouldPaint
    {
      get
      {
        return this.shouldPaint;
      }
      set
      {
        if (this.shouldPaint == value)
          return;
        this.shouldPaint = value;
        this.textBoxElement.ViewElement.Invalidate();
      }
    }

    public bool HideSelection
    {
      get
      {
        return this.hideSelection;
      }
      set
      {
        if (this.hideSelection == value)
          return;
        this.hideSelection = value;
        this.textBoxElement.ViewElement.Invalidate();
      }
    }

    public Color SelectionColor
    {
      get
      {
        return this.selectionColor;
      }
      set
      {
        if (!(this.selectionColor != value))
          return;
        this.selectionColor = value;
        this.textBoxElement.ViewElement.Invalidate();
      }
    }

    public int SelectionOpacity
    {
      get
      {
        return this.selectionOpacity;
      }
      set
      {
        if (this.selectionOpacity == value)
          return;
        this.selectionOpacity = value;
        this.textBoxElement.ViewElement.Invalidate();
      }
    }

    public RadTextBoxControlElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
    }

    public virtual void Invalidate(
      TextPosition selectionStart,
      TextPosition selectionEnd,
      bool repaint)
    {
      this.selectionPolygons = (RectangleF[]) null;
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      if (object.Equals((object) selectionStart, (object) selectionEnd) || selectionEnd == (TextPosition) null)
      {
        viewElement.Invalidate();
      }
      else
      {
        TextPosition.Swap(ref selectionStart, ref selectionEnd);
        LineInfo line1 = selectionStart.Line;
        LineInfo line2 = selectionEnd.Line;
        ReadOnlyCollection<LineInfo> lines = (ReadOnlyCollection<LineInfo>) viewElement.Lines;
        int index = lines.IndexOf(line1);
        int lineSpacing = viewElement.LineSpacing;
        List<RectangleF> rectangleFList = new List<RectangleF>();
        for (; index < lines.Count; ++index)
        {
          LineInfo currentLine = lines[index];
          RectangleF rectangle = this.GetRectangle(currentLine);
          if (currentLine == line1)
          {
            PointF location = viewElement.GetLocation(selectionStart);
            rectangle.Width += rectangle.X - location.X;
            rectangle.X = location.X;
          }
          if (currentLine == line2)
          {
            PointF location = viewElement.GetLocation(selectionEnd);
            rectangle.Width = location.X - rectangle.X;
          }
          if (currentLine != line1)
          {
            rectangle.Y -= (float) lineSpacing;
            rectangle.Height += (float) lineSpacing;
          }
          rectangleFList.Add(rectangle);
          if (currentLine == line2)
            break;
        }
        this.selectionPolygons = rectangleFList.ToArray();
        if (!repaint)
          return;
        viewElement.Invalidate();
      }
    }

    protected virtual RectangleF GetRectangle(LineInfo currentLine)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      RectangleF boundingRectangle = currentLine.ControlBoundingRectangle;
      PointF absolute1 = viewElement.PointToAbsolute(boundingRectangle.Location);
      ITextBlock textBlock = currentLine.StartBlock;
      ITextBlock endBlock = currentLine.EndBlock;
      while (TextBoxWrapPanel.IsWhitespace(textBlock.Text))
      {
        ITextBlock nextBlock = this.TextBoxElement.ViewElement.GetNextBlock(textBlock.Index);
        if (nextBlock != null)
        {
          textBlock = nextBlock;
          absolute1.Y = viewElement.PointToAbsolute((PointF) textBlock.ControlBoundingRectangle.Location).Y;
        }
        else
          break;
      }
      PointF location = (PointF) textBlock.ControlBoundingRectangle.Location;
      PointF absolute2 = viewElement.PointToAbsolute(location);
      PointF pointF = new PointF((float) endBlock.ControlBoundingRectangle.Right, (float) endBlock.ControlBoundingRectangle.Y);
      pointF = viewElement.PointToAbsolute(pointF);
      absolute1.X = absolute2.X;
      boundingRectangle.Width = pointF.X - boundingRectangle.X;
      boundingRectangle.Location = absolute1;
      return boundingRectangle;
    }

    public virtual void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      if (!this.shouldPaint || this.textBoxElement != null && !this.textBoxElement.ContainsFocus && (!this.textBoxElement.IsFocused && this.hideSelection) || (this.selectionPolygons == null || this.selectionPolygons.Length == 0))
        return;
      Color color = Color.FromArgb(this.selectionOpacity, this.selectionColor);
      Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
      Matrix transform = underlayGraphics.Transform;
      underlayGraphics.ResetTransform();
      SizeF scrollOffset = this.textBoxElement.ViewElement.ScrollOffset;
      underlayGraphics.TranslateTransform(scrollOffset.Width, scrollOffset.Height);
      using (SolidBrush solidBrush = new SolidBrush(color))
        underlayGraphics.FillRectangles((Brush) solidBrush, this.selectionPolygons);
      underlayGraphics.ResetTransform();
      underlayGraphics.Transform = transform;
    }
  }
}
