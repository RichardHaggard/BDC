// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxControlCaret
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TextBoxControlCaret : RadElement
  {
    private Color color = Color.Black;
    private Timer timer;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldPaint = true;
      this.AutoSize = false;
      this.NotifyParentOnMouseInput = false;
      this.ShouldHandleMouseInput = false;
      this.Visibility = ElementVisibility.Hidden;
      int num = SystemInformation.CaretBlinkTime;
      if (num <= 0)
        num = 700;
      this.Size = new Size(SystemInformation.CaretWidth, 12);
      this.timer = new Timer();
      this.timer.Enabled = false;
      this.timer.Interval = num;
      this.timer.Tick += new EventHandler(this.OnTimerTick);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.timer == null)
        return;
      this.Hide();
      this.timer.Dispose();
      this.timer.Tick -= new EventHandler(this.OnTimerTick);
      this.timer = (Timer) null;
    }

    public Point Position
    {
      get
      {
        return this.ControlBoundingRectangle.Location;
      }
      set
      {
        if (!(this.Position != value))
          return;
        this.Arrange(new RectangleF((PointF) value, (SizeF) this.Size));
      }
    }

    public int Height
    {
      get
      {
        return this.Size.Height;
      }
      set
      {
        this.Size = new Size(this.Size.Width, value);
      }
    }

    public int Width
    {
      get
      {
        return this.Size.Width;
      }
      set
      {
        this.Size = new Size(value, this.Size.Height);
      }
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      int caretBlinkTime = SystemInformation.CaretBlinkTime;
      if (caretBlinkTime > 0)
      {
        this.timer.Interval = caretBlinkTime;
        this.ShouldPaint = !this.ShouldPaint;
        if (this.Visibility != ElementVisibility.Visible || this.ElementTree == null)
        {
          this.timer.Stop();
          this.ShouldPaint = true;
        }
        this.Invalidate();
      }
      else
      {
        if (this.ShouldPaint)
          return;
        this.ShouldPaint = true;
        this.Invalidate();
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
      if (underlayGraphics == null)
        return;
      Telerik.WinControls.NativeMethods.InvertRect(underlayGraphics, this.ControlBoundingRectangle);
    }

    public void Show()
    {
      if (this.Visibility == ElementVisibility.Visible)
        return;
      this.Visibility = ElementVisibility.Visible;
      if (this.Visibility != ElementVisibility.Visible)
        return;
      this.ShouldPaint = true;
      this.timer.Start();
    }

    public void Hide()
    {
      this.timer.Stop();
      this.Visibility = ElementVisibility.Hidden;
    }

    public void SuspendBlinking()
    {
      this.timer.Stop();
    }

    public void ResumeBlinking()
    {
      if (this.Visibility != ElementVisibility.Visible)
        return;
      this.ShouldPaint = true;
      this.timer.Start();
    }
  }
}
