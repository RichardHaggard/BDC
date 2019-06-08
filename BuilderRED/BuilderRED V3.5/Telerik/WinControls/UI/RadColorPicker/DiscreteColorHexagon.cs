// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.DiscreteColorHexagon
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.RadColorPicker
{
  [ToolboxItem(false)]
  public class DiscreteColorHexagon : UserControl
  {
    private ColorHexagonElement[] hexagonElements = new ColorHexagonElement[147];
    private float[] matrix1 = new float[6]{ -0.5f, -1f, -0.5f, 0.5f, 1f, 0.5f };
    private float[] matrix2 = new float[6]{ 0.824f, 0.0f, -0.824f, -0.824f, 0.0f, 0.824f };
    private int oldSelectedHexagonIndex = -1;
    private int sectorMaximum = 7;
    private int selectedHexagonIndex = -1;
    private const float coeffcient = 0.824f;
    private Container container;

    public event ColorChangedEventHandler ColorChanged;

    public DiscreteColorHexagon()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.Opaque, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      for (int index = 0; index < this.hexagonElements.Length; ++index)
        this.hexagonElements[index] = new ColorHexagonElement();
      this.container = new Container();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.container != null)
        this.container.Dispose();
      base.Dispose(disposing);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        if (this.selectedHexagonIndex >= 0)
        {
          this.hexagonElements[this.selectedHexagonIndex].IsSelected = false;
          this.Invalidate(this.hexagonElements[this.selectedHexagonIndex].BoundingRectangle);
        }
        this.selectedHexagonIndex = -1;
        if (this.oldSelectedHexagonIndex >= 0)
        {
          this.selectedHexagonIndex = this.oldSelectedHexagonIndex;
          this.hexagonElements[this.selectedHexagonIndex].IsSelected = true;
          if (this.ColorChanged != null)
            this.ColorChanged((object) this, new ColorChangedEventArgs(this.SelectedColor));
          this.Invalidate(this.hexagonElements[this.selectedHexagonIndex].BoundingRectangle);
        }
      }
      base.OnMouseDown(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.BackColor == Color.Transparent)
        this.OnPaintBackground(e);
      Graphics graphics = e.Graphics;
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      foreach (ColorHexagonElement hexagonElement in this.hexagonElements)
        hexagonElement.Paint(graphics);
      if (this.oldSelectedHexagonIndex >= 0)
        this.hexagonElements[this.oldSelectedHexagonIndex].Paint(graphics);
      if (this.selectedHexagonIndex >= 0)
        this.hexagonElements[this.selectedHexagonIndex].Paint(graphics);
      base.OnPaint(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.DrawHexagonHighlighter(-1);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.DrawHexagonHighlighter(this.GetHexagonIndexFromCoordinates(e.X, e.Y));
    }

    protected override void OnResize(EventArgs e)
    {
      this.InitializeHexagons();
      base.OnResize(e);
    }

    private int GetHexagonWidth(int availableHeight)
    {
      int num = availableHeight / (2 * this.sectorMaximum);
      if ((int) Math.Floor((double) num / 2.0) * 2 < num)
        --num;
      return num;
    }

    private void InitializeHexagons()
    {
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Offset(0, -8);
      if (clientRectangle.Height < clientRectangle.Width)
        clientRectangle.Inflate(-(clientRectangle.Width - clientRectangle.Height) / 2, 0);
      else
        clientRectangle.Inflate(0, -(clientRectangle.Height - clientRectangle.Width) / 2);
      int hexagonWidth = this.GetHexagonWidth(Math.Min(clientRectangle.Height, clientRectangle.Width));
      int centerOfMiddleHexagonX = (clientRectangle.Left + clientRectangle.Right) / 2;
      int centerOfMiddleHexagonY = (clientRectangle.Top + clientRectangle.Bottom) / 2 - hexagonWidth;
      this.hexagonElements[0].CurrentColor = Color.White;
      this.hexagonElements[0].SetHexagonPoints((float) centerOfMiddleHexagonX, (float) centerOfMiddleHexagonY, hexagonWidth);
      int index1 = 1;
      for (int index2 = 1; index2 < this.sectorMaximum; ++index2)
      {
        float yCoordinate = (float) centerOfMiddleHexagonY;
        float xCoordinate = (float) (centerOfMiddleHexagonX + hexagonWidth * index2);
        for (int index3 = 0; index3 < this.sectorMaximum - 1; ++index3)
        {
          int num1 = (int) ((double) hexagonWidth * (double) this.matrix2[index3]);
          int num2 = (int) ((double) hexagonWidth * (double) this.matrix1[index3]);
          for (int index4 = 0; index4 < index2; ++index4)
          {
            double num3 = 0.936 * (double) (this.sectorMaximum - index2) / (double) this.sectorMaximum + 0.12;
            float colorQuotient = ColorServices.GetColorQuotient(xCoordinate - (float) centerOfMiddleHexagonX, yCoordinate - (float) centerOfMiddleHexagonY);
            this.hexagonElements[index1].SetHexagonPoints(xCoordinate, yCoordinate, hexagonWidth);
            this.hexagonElements[index1].CurrentColor = ColorServices.ColorFromRGBRatios((double) colorQuotient, num3, 1.0);
            yCoordinate += (float) num1;
            xCoordinate += (float) num2;
            ++index1;
          }
        }
      }
      clientRectangle.Y -= hexagonWidth + hexagonWidth / 2;
      this.InitializeGrayscaleHexagons(ref clientRectangle, hexagonWidth, ref centerOfMiddleHexagonX, ref centerOfMiddleHexagonY, ref index1);
    }

    private void InitializeGrayscaleHexagons(
      ref Rectangle clientRectangle,
      int hexagonWidth,
      ref int centerOfMiddleHexagonX,
      ref int centerOfMiddleHexagonY,
      ref int index)
    {
      int maxValue = (int) byte.MaxValue;
      int num1 = 17;
      int num2 = 16;
      int num3 = (clientRectangle.Width - 7 * hexagonWidth) / 2 + clientRectangle.X - hexagonWidth / 3;
      centerOfMiddleHexagonX = num3;
      centerOfMiddleHexagonY = clientRectangle.Bottom;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        this.hexagonElements[index].CurrentColor = Color.FromArgb(maxValue, maxValue, maxValue);
        this.hexagonElements[index].SetHexagonPoints((float) centerOfMiddleHexagonX, (float) centerOfMiddleHexagonY, hexagonWidth);
        centerOfMiddleHexagonX += hexagonWidth;
        ++index;
        if (index1 == 7)
        {
          centerOfMiddleHexagonX = num3 + hexagonWidth / 2;
          centerOfMiddleHexagonY += (int) ((double) hexagonWidth * 0.824000000953674);
        }
        maxValue -= num1;
      }
    }

    private void DrawHexagonHighlighter(int selectedHexagonIndex)
    {
      if (selectedHexagonIndex == this.oldSelectedHexagonIndex)
        return;
      if (this.oldSelectedHexagonIndex >= 0)
      {
        this.hexagonElements[this.oldSelectedHexagonIndex].IsHovered = false;
        this.Invalidate(this.hexagonElements[this.oldSelectedHexagonIndex].BoundingRectangle);
      }
      this.oldSelectedHexagonIndex = selectedHexagonIndex;
      if (this.oldSelectedHexagonIndex < 0)
        return;
      this.hexagonElements[this.oldSelectedHexagonIndex].IsHovered = true;
      this.Invalidate(this.hexagonElements[this.oldSelectedHexagonIndex].BoundingRectangle);
    }

    private int GetHexagonIndexFromCoordinates(int xCoordinate, int yCoordinate)
    {
      for (int index = 0; index < this.hexagonElements.Length; ++index)
      {
        if (this.hexagonElements[index].BoundingRectangle.Contains(xCoordinate, yCoordinate))
          return index;
      }
      return -1;
    }

    public Color SelectedColor
    {
      get
      {
        if (this.selectedHexagonIndex < 0)
          return Color.Empty;
        return this.hexagonElements[this.selectedHexagonIndex].CurrentColor;
      }
    }
  }
}
