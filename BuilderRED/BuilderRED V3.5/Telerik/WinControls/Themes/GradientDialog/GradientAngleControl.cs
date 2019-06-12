// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.GradientDialog.GradientAngleControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Themes.GradientDialog
{
  [ToolboxItem(false)]
  public class GradientAngleControl : UserControl
  {
    private double m_gradientAngle;
    private IContainer components;

    public GradientAngleControl()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    private void GradientAngleControl_Load(object sender, EventArgs e)
    {
    }

    public double GradientAngle
    {
      get
      {
        return this.m_gradientAngle;
      }
      set
      {
        this.m_gradientAngle = value;
        this.Invalidate();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(this.ClientRectangle.X + this.ClientRectangle.Width / 4 + this.ClientRectangle.Width / 8, this.ClientRectangle.Y + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 8, this.ClientRectangle.Width / 4, this.ClientRectangle.Height / 4);
      using (Pen pen = new Pen(this.ForeColor))
      {
        using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        {
          graphics.FillEllipse(brush, rect);
          graphics.DrawEllipse(pen, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1));
          double num1 = this.GradientAngle * Math.PI / 180.0;
          int num2 = this.Width * 45 / 100;
          int x1 = this.Width / 2;
          int y1 = this.Height / 2;
          int x2 = (int) ((double) x1 + (double) num2 * Math.Sin(num1));
          int y2 = (int) ((double) y1 - (double) num2 * Math.Cos(num1));
          graphics.DrawLine(pen, x1, y1, x2, y2);
        }
      }
      base.OnPaint(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ForeColor = Color.Black;
      this.Name = nameof (GradientAngleControl);
      this.Load += new EventHandler(this.GradientAngleControl_Load);
      this.ResumeLayout(false);
    }
  }
}
