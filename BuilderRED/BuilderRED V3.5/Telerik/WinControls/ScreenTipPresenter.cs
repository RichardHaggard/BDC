// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ScreenTipPresenter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  internal class ScreenTipPresenter : Form
  {
    private int showDelay = 900;
    private Point pivotPoint = Point.Empty;
    private Timer timer = new Timer();
    private const int DefaultOffset = 25;
    private TipStates taskbarState;
    private RadItem activeItem;
    private Control ownerControl;
    private IContainer components;
    private RadScreenTipPlaceholder radScreenTipControl1;

    public ScreenTipPresenter()
      : this((Control) null)
    {
    }

    public ScreenTipPresenter(Control ownerControl)
    {
      this.InitializeComponent();
      this.ownerControl = ownerControl;
      this.timer.Tick += new EventHandler(this.OnTimer);
      this.timer.Enabled = true;
    }

    protected override CreateParams CreateParams
    {
      get
      {
        return new CreateParams()
        {
          Parent = IntPtr.Zero,
          Style = int.MinValue,
          ExStyle = 524424
        };
      }
    }

    public void Show(RadItem item, Point pivotPoint)
    {
      this.activeItem = item;
      this.Show((IScreenTipContent) item.ScreenTip, pivotPoint, -1);
    }

    public void Show(IScreenTipContent content, Point pivotPoint, int delay)
    {
      if (pivotPoint.IsEmpty || content == null)
        return;
      if (delay >= 0)
        this.showDelay = delay;
      this.pivotPoint = pivotPoint;
      this.UpdateScreenTipSizeAndShape(content);
      this.UpdateScreenTipState();
    }

    public new void Hide()
    {
      if (this.taskbarState == TipStates.Hidden)
        return;
      this.timer.Stop();
      this.taskbarState = TipStates.Hidden;
      base.Hide();
      this.radScreenTipControl1.SetScreenTipElement((RadScreenTipElement) null);
    }

    protected override void WndProc(ref Message message)
    {
      if (message.Msg == 132)
        message.Result = (IntPtr) -1;
      else
        base.WndProc(ref message);
    }

    protected void OnTimer(object obj, EventArgs e)
    {
      if (this.taskbarState != TipStates.Appearing)
        return;
      this.pivotPoint = this.GetCorrectedLocation(this.pivotPoint);
      NativeMethods.SetWindowPos(new HandleRef((object) this.activeItem, this.Handle), NativeMethods.HWND_TOPMOST, this.pivotPoint.X, this.pivotPoint.Y, 0, 0, 81);
      if (!this.Visible)
        this.Visible = true;
      this.timer.Stop();
      this.taskbarState = TipStates.Visible;
    }

    private void UpdateScreenTipSizeAndShape(IScreenTipContent content)
    {
      RadScreenTipElement element = (RadScreenTipElement) content;
      if (element.TipSize != Size.Empty)
      {
        element.MinSize = element.TipSize;
        element.MaxSize = new Size(element.TipSize.Width, element.MaxSize.Height);
      }
      this.radScreenTipControl1.Size = Screen.FromPoint(Control.MousePosition).WorkingArea.Size;
      this.radScreenTipControl1.SetScreenTipElement(element);
      this.radScreenTipControl1.LoadElementTree();
      this.radScreenTipControl1.RootElement.InvalidateMeasure(true);
      this.radScreenTipControl1.RootElement.UpdateLayout();
      this.radScreenTipControl1.Size = this.radScreenTipControl1.RootElement.DesiredSize.ToSize();
      this.Size = this.radScreenTipControl1.Size;
      if (this.radScreenTipControl1.RootElement.Shape == null)
        return;
      if (this.Region != null)
        this.Region.Dispose();
      using (GraphicsPath path = this.radScreenTipControl1.RootElement.Shape.CreatePath(this.DisplayRectangle))
        this.Region = new Region(path);
    }

    private void UpdateScreenTipState()
    {
      switch (this.taskbarState)
      {
        case TipStates.Hidden:
          this.taskbarState = TipStates.Appearing;
          this.timer.Interval = this.showDelay;
          this.timer.Start();
          break;
        case TipStates.Appearing:
          this.Refresh();
          break;
        case TipStates.Visible:
          this.timer.Stop();
          this.Refresh();
          break;
        case TipStates.Disappearing:
          this.timer.Stop();
          this.taskbarState = TipStates.Visible;
          this.Refresh();
          break;
      }
    }

    private Point GetCorrectedLocation(Point current)
    {
      Point point = current;
      if (this.ownerControl != null)
      {
        Point mousePosition = Control.MousePosition;
        Screen screen = Screen.FromPoint(mousePosition);
        int x = current.X;
        int y = current.Y;
        if (point.X + this.Size.Width > screen.WorkingArea.Right)
          x = screen.WorkingArea.Right - this.Size.Width - 25;
        if (point.Y + this.Size.Height > screen.WorkingArea.Bottom)
          y = screen.WorkingArea.Bottom - this.Size.Height - 25;
        if (point.X < screen.WorkingArea.Left)
          x = screen.WorkingArea.Left + 25;
        if (point.Y < screen.WorkingArea.Top)
          y = screen.WorkingArea.Top + 25;
        Rectangle rectangle = new Rectangle(new Point(x, y), this.Size);
        if (rectangle.Contains(mousePosition))
        {
          if (mousePosition.X - rectangle.X <= mousePosition.Y - rectangle.Y)
            x = mousePosition.X + this.Width + 25 >= screen.WorkingArea.Right ? mousePosition.X - this.Width - 25 : mousePosition.X + 25;
          else
            y = mousePosition.Y + this.Height + 25 >= screen.WorkingArea.Bottom ? mousePosition.Y - this.Height - 25 : mousePosition.Y + 25;
        }
        point = new Point(x, y);
      }
      return point;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
          this.components.Dispose();
        if (this.BackgroundImage != null)
        {
          this.BackgroundImage.Dispose();
          this.BackgroundImage = (Image) null;
        }
        if (this.Region != null)
        {
          this.Region.Dispose();
          this.Region = (Region) null;
        }
        if (this.radScreenTipControl1 != null)
          this.radScreenTipControl1.Dispose();
        if (this.timer != null)
          this.timer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radScreenTipControl1 = new RadScreenTipPlaceholder();
      this.radScreenTipControl1.BeginInit();
      this.SuspendLayout();
      this.Controls.Add((Control) this.radScreenTipControl1);
      this.radScreenTipControl1.Location = new Point(0, 0);
      this.StartPosition = FormStartPosition.Manual;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimizeBox = false;
      this.MaximizeBox = false;
      this.ControlBox = false;
      this.ShowInTaskbar = false;
      this.AutoSize = false;
      this.TopLevel = true;
      this.BackColor = SystemColors.AppWorkspace;
      this.Opacity = 0.99;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.ClientSize = new Size(2, 2);
      this.Name = nameof (ScreenTipPresenter);
      this.Text = "ScreenTipForm";
      this.ResumeLayout(false);
      this.radScreenTipControl1.EndInit();
    }
  }
}
