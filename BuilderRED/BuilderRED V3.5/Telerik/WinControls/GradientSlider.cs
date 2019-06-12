// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GradientSlider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class GradientSlider : UserControl
  {
    private List<GradientColorValue> values = new List<GradientColorValue>();
    private GradientColorValue marker;
    private Point mousePosition;
    private int mouseOffsetFromTheCenter;
    private IContainer components;

    public GradientSlider()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<GradientColorValue> Values
    {
      get
      {
        return this.values;
      }
      set
      {
        this.values = value;
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    private double ResizeCoefficient
    {
      get
      {
        return 1.0;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Height -= 16;
      clientRectangle.Width -= 18;
      clientRectangle.X += 8;
      if (this.values.Count > 0)
      {
        ColorBlend colorBlend = new ColorBlend();
        colorBlend.Colors = new Color[this.values.Count];
        colorBlend.Positions = new float[this.values.Count];
        for (int index = 0; index < this.values.Count; ++index)
        {
          colorBlend.Colors[index] = this.values[index].ColorValue;
          colorBlend.Positions[index] = this.values[index].ColorPosition;
        }
        if (colorBlend.Colors.Length < 2)
        {
          using (SolidBrush solidBrush = new SolidBrush(colorBlend.Colors[0]))
            e.Graphics.FillRectangle((Brush) solidBrush, clientRectangle);
        }
        else
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(clientRectangle, colorBlend.Colors[0], colorBlend.Colors[this.values.Count - 1], 0.0f))
          {
            linearGradientBrush.InterpolationColors = colorBlend;
            e.Graphics.FillRectangle((Brush) linearGradientBrush, clientRectangle);
          }
          for (int index = 0; index < this.values.Count; ++index)
          {
            GradientColorValue gradientColorValue = this.values[index];
            Rectangle rect = new Rectangle((int) ((double) clientRectangle.Width * (double) gradientColorValue.ColorPosition + 4.0), clientRectangle.Height + 2, 6, 12);
            using (Brush brush = (Brush) new SolidBrush(gradientColorValue.ColorValue))
              e.Graphics.FillRectangle(brush, rect);
            if (gradientColorValue == this.marker)
              e.Graphics.DrawRectangle(new Pen(Brushes.OrangeRed, 1f), rect);
            else
              e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1f), rect);
          }
        }
      }
      else
        e.Graphics.FillRectangle(Brushes.Blue, clientRectangle);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Height -= 16;
      clientRectangle.Width -= 18;
      clientRectangle.X += 8;
      if (e.Y <= clientRectangle.Height + 1)
        this.OnMouseDoubleClick(e);
      this.marker = (GradientColorValue) null;
      for (int index = 1; index < this.values.Count - 1; ++index)
      {
        GradientColorValue gradientColorValue = this.values[index];
        if (new Rectangle((int) ((double) clientRectangle.Width * (double) gradientColorValue.ColorPosition + 4.0), clientRectangle.Height + 2, 8, 12).Contains(e.X, e.Y))
        {
          this.marker = gradientColorValue;
          this.Capture = true;
          this.mouseOffsetFromTheCenter = e.X - (int) ((double) clientRectangle.Width * (double) gradientColorValue.ColorPosition + 8.0);
          this.mousePosition = new Point(e.X, e.Y);
          this.Refresh();
          break;
        }
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button != MouseButtons.Left || this.marker == null)
        return;
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Height -= 16;
      clientRectangle.Width -= 18;
      clientRectangle.X += 8;
      int num1 = this.values.IndexOf(this.marker);
      float num2 = ((float) e.X - (float) this.mouseOffsetFromTheCenter - (float) this.Parent.Location.X - (float) this.Location.X) / (float) clientRectangle.Width;
      if ((double) num2 <= (double) this.values[num1 - 1].ColorPosition + 0.00999 / this.ResizeCoefficient || (double) num2 >= (double) this.values[num1 + 1].ColorPosition - 0.00999 / this.ResizeCoefficient || ((double) num2 <= 0.0 || (double) num2 >= 1.0))
        return;
      this.marker.ColorPosition = num2;
      this.Refresh();
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.marker.ColorValue));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (!new Rectangle(this.ClientRectangle.X - 10, this.ClientRectangle.Y - 8, this.ClientRectangle.Width + 500, this.ClientRectangle.Height + 10).Contains(e.X, e.Y) && this.marker != null && (this.marker != this.values[0] && this.marker != this.values[this.values.Count - 1]) && Math.Abs(e.X - this.mousePosition.X) < 70)
      {
        this.values.Remove(this.marker);
        if (this.ColorChanged != null)
          this.ColorChanged((object) this, new ColorChangedEventArgs(this.marker.ColorValue));
      }
      this.marker = (GradientColorValue) null;
      this.Refresh();
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Height -= 16;
      clientRectangle.Width -= 18;
      clientRectangle.X += 8;
      GradientColorValue gradientColorValue1 = (GradientColorValue) null;
      for (int index = 0; index < this.values.Count; ++index)
      {
        GradientColorValue gradientColorValue2 = this.values[index];
        Rectangle rectangle = new Rectangle((int) ((double) clientRectangle.Width * (double) gradientColorValue2.ColorPosition + 4.0), clientRectangle.Height, 8, 14);
        if (rectangle.Contains(e.X, e.Y))
        {
          gradientColorValue1 = gradientColorValue2;
          break;
        }
        if (rectangle.X > e.X)
        {
          if (this.values.Count >= 4)
          {
            int num = (int) MessageBox.Show("Only four color gradient styles are supported");
            return;
          }
          this.Values.Insert(index, new GradientColorValue(Color.White, ((float) e.X - (float) this.Parent.Location.X - (float) this.Location.X) / (float) (clientRectangle.Width + 4)));
          gradientColorValue1 = this.Values[index];
          break;
        }
      }
      if (gradientColorValue1 == null)
        return;
      IRadColorDialog colorDialogInstance = RadColorEditor.CreateColorDialogInstance();
      UserControl selectorInstance = RadColorEditor.CreateColorSelectorInstance() as UserControl;
      ((IColorSelector) colorDialogInstance.RadColorSelector).SelectedColor = gradientColorValue1.ColorValue;
      ((IColorSelector) colorDialogInstance.RadColorSelector).OldColor = gradientColorValue1.ColorValue;
      selectorInstance.Dock = DockStyle.Fill;
      if (((Form) colorDialogInstance).ShowDialog() == DialogResult.OK)
        gradientColorValue1.ColorValue = ((IColorSelector) colorDialogInstance.RadColorSelector).SelectedColor;
      this.Refresh();
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(gradientColorValue1.ColorValue));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }
  }
}
