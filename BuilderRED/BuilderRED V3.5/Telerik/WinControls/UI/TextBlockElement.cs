// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBlockElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TextBlockElement : VisualElement, ITextBlock
  {
    private const char VisualStyleSystemPasswordChar = '●';
    private const char SystemPasswordChar = '*';
    private string text;
    private int offset;
    private int index;
    private bool performMeasure;
    private bool drawFill;
    private Dictionary<SizeF, Font> dpiScaledFontcache;

    public TextBlockElement()
      : this(string.Empty)
    {
    }

    public TextBlockElement(string text)
    {
      this.text = text;
      this.dpiScaledFontcache = new Dictionary<SizeF, Font>();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldPaint = true;
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.BypassLayoutPolicies = true;
      this.drawFill = false;
      this.performMeasure = true;
      this.index = 0;
      this.offset = 0;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        if (!(this.text != value))
          return;
        this.text = value;
        this.performMeasure = true;
        this.InvalidateMeasure();
      }
    }

    public virtual int Length
    {
      get
      {
        if (TextBoxWrapPanel.IsLineFeed(this.text) || string.IsNullOrEmpty(this.text))
          return 0;
        return this.text.Length;
      }
    }

    public int Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
      }
    }

    public int Index
    {
      get
      {
        return this.index;
      }
      set
      {
        this.index = value;
      }
    }

    public bool DrawFill
    {
      get
      {
        return this.drawFill;
      }
      set
      {
        if (this.drawFill == value)
          return;
        this.drawFill = value;
        this.Invalidate();
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != VisualElement.FontProperty)
        return;
      this.performMeasure = true;
    }

    protected override void ResetLayoutCore()
    {
      base.ResetLayoutCore();
      this.performMeasure = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (!this.performMeasure)
        return this.DesiredSize;
      this.performMeasure = false;
      string text = this.GetText();
      bool flag = string.IsNullOrEmpty(text);
      if (flag)
        text = "X";
      SizeF sizeF = (SizeF) TextBoxControlMeasurer.MeasureText(text, this.GetScaledFont(this.DpiScaleFactor.Height));
      if (flag)
        sizeF.Width = 0.0f;
      return sizeF;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.performMeasure = true;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.drawFill)
        base.PaintElement(graphics, angle, scale);
      if (this.text == null || this.text.Trim() == string.Empty)
        return;
      Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
      TextRenderingHint textRenderingHint = underlayGraphics.TextRenderingHint;
      underlayGraphics.TextRenderingHint = TextRenderingHint.SystemDefault;
      Point location = this.ControlBoundingRectangle.Location;
      --location.Y;
      List<string> textParts = this.GetTextParts();
      int count = textParts.Count;
      Color foreColor = this.ForeColor;
      Color backColor = this.Enabled ? Color.Transparent : this.BackColor;
      using (RadHdcWrapper radHdcWrapper = new RadHdcWrapper(underlayGraphics, false))
      {
        for (int index = 0; index < count; ++index)
        {
          string text = textParts[index];
          TextRenderer.DrawText((IDeviceContext) radHdcWrapper, text, this.GetScaledFont(this.DpiScaleFactor.Height), location, foreColor, backColor, TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.NoPadding);
          if (index < count - 1)
          {
            Size size = TextBoxControlMeasurer.MeasureText(text, this.GetScaledFont(this.DpiScaleFactor.Height));
            location.X += size.Width;
          }
        }
      }
      underlayGraphics.TextRenderingHint = textRenderingHint;
    }

    private List<string> GetTextParts()
    {
      string str = this.GetText();
      List<string> stringList = new List<string>();
      while (1600 < str.Length)
      {
        stringList.Add(str.Substring(0, 1600));
        str = str.Substring(1600, str.Length - 1600);
        if (str.Length <= 1600)
        {
          stringList.Add(str);
          break;
        }
      }
      if (stringList.Count == 0)
        stringList.Add(str);
      return stringList;
    }

    private string GetText()
    {
      int num = this.text != null ? this.text.Length : 0;
      if (num > 0)
      {
        TextBoxViewElement parent = this.Parent as TextBoxViewElement;
        if (parent != null && !parent.Multiline)
        {
          char ch1 = Application.RenderWithVisualStyles ? '●' : '*';
          char ch2 = parent.UseSystemPasswordChar ? ch1 : parent.PasswordChar;
          if (ch2 != char.MinValue)
          {
            StringBuilder stringBuilder = new StringBuilder();
            for (; num > 0; --num)
              stringBuilder.Append(ch2);
            return stringBuilder.ToString();
          }
        }
      }
      return this.text;
    }

    public override string ToString()
    {
      return string.Format("Text = {0} Offset= {1} Length = {2} Index = {3}", (object) this.text, (object) this.offset, (object) this.Length, (object) this.index);
    }

    public virtual RectangleF GetRectangleFromCharacterIndex(int index, bool trailEdge)
    {
      string text = this.GetText();
      if (index < 0 || index > text.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      Size size = Size.Empty;
      Font scaledFont = this.GetScaledFont(this.DpiScaleFactor.Height);
      if (index == 0 && !trailEdge)
        size.Height = (int) this.DesiredSize.Height;
      else if (index == this.text.Length && trailEdge)
      {
        size = this.DesiredSize.ToSize();
      }
      else
      {
        if (trailEdge)
          ++index;
        size = TextBoxControlMeasurer.MeasureText(text.Substring(0, index), scaledFont);
      }
      RectangleF rectangleF = new RectangleF((PointF) this.ControlBoundingRectangle.Location, new SizeF(0.0f, (float) size.Height));
      rectangleF.X += (float) size.Width;
      return rectangleF;
    }

    public virtual int GetCharacterIndexFromX(float x)
    {
      int num = 0;
      int x1 = this.ControlBoundingRectangle.X;
      if ((double) x >= (double) this.ControlBoundingRectangle.Right)
        num = this.Length;
      else if ((double) x > (double) x1)
        num = TextBoxControlMeasurer.BinarySearchCaretIndex(this.GetText(), this.GetScaledFont(this.DpiScaleFactor.Height), (double) (x - (float) x1));
      return num;
    }

    [SpecialName]
    SizeF ITextBlock.get_DesiredSize()
    {
      return this.DesiredSize;
    }

    void ITextBlock.Measure(SizeF _param1)
    {
      this.Measure(_param1);
    }

    void ITextBlock.Arrange(RectangleF _param1)
    {
      this.Arrange(_param1);
    }
  }
}
