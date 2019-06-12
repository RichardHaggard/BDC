// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CaptureBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class CaptureBox : UserControl
  {
    private Color capturedColor = Color.Empty;
    private Point mouse = Point.Empty;
    private Timer timer;
    private IContainer components;

    public Color CapturedColor
    {
      get
      {
        return this.capturedColor;
      }
      set
      {
        this.capturedColor = value;
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    public CaptureBox()
    {
      this.InitializeComponent();
      this.timer = new Timer();
      this.timer.Interval = 1;
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (!(this.mouse != Control.MousePosition))
        return;
      this.mouse = Control.MousePosition;
      IntPtr dc = Telerik.WinControls.NativeMethods.CreateDC("Display", (string) null, (string) null, IntPtr.Zero);
      int pixel = Telerik.WinControls.NativeMethods.GetPixel(dc, Control.MousePosition.X, Control.MousePosition.Y);
      Telerik.WinControls.NativeMethods.DeleteDC(new HandleRef((object) null, dc));
      Color color = Color.FromArgb(pixel & (int) byte.MaxValue, (pixel & 65280) >> 8, (pixel & 16711680) >> 16);
      if (!(color != this.capturedColor))
        return;
      this.capturedColor = color;
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.capturedColor));
    }

    public void Start()
    {
      this.timer.Start();
      this.Capture = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = false;
      this.timer.Stop();
    }

    public static Color GetScreenColor()
    {
      IntPtr dc = Telerik.WinControls.NativeMethods.CreateDC("Display", (string) null, (string) null, IntPtr.Zero);
      int pixel = Telerik.WinControls.NativeMethods.GetPixel(dc, Control.MousePosition.X, Control.MousePosition.Y);
      Telerik.WinControls.NativeMethods.DeleteDC(new HandleRef((object) null, dc));
      return Color.FromArgb(pixel & (int) byte.MaxValue, (pixel & 65280) >> 8, (pixel & 16711680) >> 16);
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
      this.Name = nameof (CaptureBox);
      this.Size = new Size(166, 131);
      this.ResumeLayout(false);
    }
  }
}
