// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HostedTextBoxBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class HostedTextBoxBase : System.Windows.Forms.TextBox
  {
    private bool allowPrompt = true;
    private string nullText = string.Empty;
    private Color nullColor = SystemColors.GrayText;
    private bool showNullText;
    private bool useGenericBorderPaint;

    [Browsable(false)]
    [Description("Gets or sets a color of the null text")]
    [Category("Appearance")]
    public Color NullTextColor
    {
      get
      {
        return this.nullColor;
      }
      set
      {
        this.nullColor = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the null text will be shown when the control is focused and the text is empty")]
    [Browsable(false)]
    public bool ShowNullText
    {
      get
      {
        return this.showNullText;
      }
      set
      {
        this.showNullText = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets a value indicating whether to show the bottom part of characters, clipped due to font name or size particularities")]
    [DefaultValue(false)]
    public bool UseGenericBorderPaint
    {
      get
      {
        return this.useGenericBorderPaint;
      }
      set
      {
        this.useGenericBorderPaint = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the prompt text that is displayed when the TextBox  contains no text.")]
    [Category("Appearance")]
    [Localizable(true)]
    public string NullText
    {
      get
      {
        return this.nullText;
      }
      set
      {
        if (value == null)
          value = string.Empty;
        this.nullText = value.Trim();
        this.Invalidate();
      }
    }

    [Description("Gets or sets whether to use different than SystemColors.GrayText color for the prompt text.")]
    [Browsable(true)]
    [Category("Appearance")]
    public Color PromptForeColor
    {
      get
      {
        return this.nullColor;
      }
      set
      {
        this.nullColor = value;
        this.Invalidate();
      }
    }

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [Description("Controls whether the text of the edit control can span more than one line")]
    [DefaultValue(false)]
    public override bool Multiline
    {
      get
      {
        return base.Multiline;
      }
      set
      {
        base.Multiline = value;
      }
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      this.RecreateHandle();
    }

    protected override void OnTextAlignChanged(EventArgs e)
    {
      base.OnTextAlignChanged(e);
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.allowPrompt && !this.showNullText || this.Text.Length != 0)
        return;
      this.DrawNullText(e.Graphics);
    }

    protected override void WndProc(ref Message message)
    {
      switch (message.Msg)
      {
        case 7:
          this.allowPrompt = false;
          break;
        case 8:
          this.allowPrompt = true;
          break;
        case 133:
          if (this.useGenericBorderPaint && !this.Multiline)
          {
            HandleRef hWnd = new HandleRef((object) this, message.HWnd);
            HandleRef hDC = new HandleRef((object) this, Telerik.WinControls.NativeMethods.GetWindowDC(hWnd));
            HandleRef handleRef = new HandleRef((object) this, new IntPtr(129));
            try
            {
              if (!this.PaintBorders(hWnd.Handle, hDC.Handle, handleRef.Handle))
                return;
              message.Result = IntPtr.Zero;
              return;
            }
            finally
            {
              if (hDC.Handle != IntPtr.Zero)
                Telerik.WinControls.NativeMethods.ReleaseDC(hWnd, hDC);
            }
          }
          else
            break;
        case 791:
          if (this.useGenericBorderPaint && !this.Multiline)
          {
            base.WndProc(ref message);
            if (((int) message.LParam & 2) == 0)
              return;
            this.PaintBordersCore(new HandleRef((object) this, message.HWnd).Handle, new HandleRef((object) this, message.WParam).Handle, IntPtr.Zero);
            return;
          }
          break;
      }
      base.WndProc(ref message);
      if (message.Msg != 15 || !this.allowPrompt && !this.showNullText || (this.Text.Length != 0 || this.GetStyle(ControlStyles.UserPaint)))
        return;
      this.DrawTextPrompt();
    }

    private bool PaintBorders(IntPtr hWnd, IntPtr hdc, IntPtr hRgn)
    {
      if (hdc == IntPtr.Zero)
        return false;
      using (Graphics graphics = Graphics.FromHdc(hdc))
      {
        Telerik.WinControls.NativeMethods.RECT rect1 = new Telerik.WinControls.NativeMethods.RECT();
        if (!Telerik.WinControls.NativeMethods.GetWindowRect(new HandleRef((object) hWnd, this.Handle), ref rect1))
          return false;
        Rectangle windowRectangle = new Rectangle(0, 0, rect1.right - rect1.left, rect1.bottom - rect1.top);
        Rectangle rect2 = windowRectangle;
        rect2.Offset(2, 2);
        rect2.Width -= 4;
        rect2.Height -= 4;
        graphics.SetClip(rect2, CombineMode.Exclude);
        this.PaintBordersCore(graphics, windowRectangle);
      }
      return true;
    }

    protected void PaintBorders(PaintEventArgs e)
    {
      Telerik.WinControls.NativeMethods.RECT rect1 = new Telerik.WinControls.NativeMethods.RECT();
      if (!Telerik.WinControls.NativeMethods.GetWindowRect(new HandleRef((object) this.Handle, this.Handle), ref rect1))
        return;
      Rectangle windowRectangle = new Rectangle(0, 0, rect1.right - rect1.left, rect1.bottom - rect1.top);
      Rectangle rect2 = windowRectangle;
      rect2.Offset(2, 2);
      rect2.Width -= 4;
      rect2.Height -= 4;
      e.Graphics.SetClip(rect2, CombineMode.Exclude);
      this.PaintBordersCore(e.Graphics, windowRectangle);
    }

    protected void PaintBordersCore(Graphics graphics, Rectangle windowRectangle)
    {
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        graphics.FillRectangle(brush, windowRectangle);
    }

    private void PaintBordersCore(IntPtr hWnd, IntPtr hdc, IntPtr hRgn)
    {
      if (!(hdc != IntPtr.Zero))
        return;
      using (Graphics graphics = Graphics.FromHdc(hdc))
      {
        Telerik.WinControls.NativeMethods.RECT rect = new Telerik.WinControls.NativeMethods.RECT();
        if (!Telerik.WinControls.NativeMethods.GetWindowRect(new HandleRef((object) hWnd, this.Handle), ref rect))
          return;
        Rectangle rectangle = new Rectangle(0, 0, rect.right - rect.left, rect.bottom - rect.top);
        graphics.SetClip(Rectangle.Inflate(rectangle, -1, -1), CombineMode.Exclude);
        this.PaintBorders(new PaintEventArgs(graphics, rectangle));
      }
    }

    protected virtual void DrawTextPrompt()
    {
      using (Graphics graphics = this.CreateGraphics())
        this.DrawNullText(graphics);
    }

    protected virtual void DrawNullText(Graphics graphics)
    {
      TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
      Rectangle clientRectangle = this.ClientRectangle;
      TextFormatFlags flags;
      if (this.RightToLeft == RightToLeft.Yes)
      {
        flags = textFormatFlags | TextFormatFlags.RightToLeft;
        switch (this.TextAlign)
        {
          case HorizontalAlignment.Left:
            flags |= TextFormatFlags.Right;
            clientRectangle.Offset(1, 1);
            break;
          case HorizontalAlignment.Right:
            flags = flags;
            clientRectangle.Offset(0, 1);
            break;
          case HorizontalAlignment.Center:
            flags |= TextFormatFlags.HorizontalCenter;
            clientRectangle.Offset(0, 1);
            break;
        }
      }
      else
      {
        flags = textFormatFlags & ~TextFormatFlags.RightToLeft;
        switch (this.TextAlign)
        {
          case HorizontalAlignment.Left:
            flags = flags;
            clientRectangle.Offset(1, 0);
            break;
          case HorizontalAlignment.Right:
            flags |= TextFormatFlags.Right;
            clientRectangle.Offset(0, 1);
            break;
          case HorizontalAlignment.Center:
            flags |= TextFormatFlags.HorizontalCenter;
            clientRectangle.Offset(0, 1);
            break;
        }
      }
      TextRenderer.DrawText((IDeviceContext) graphics, this.nullText, this.Font, clientRectangle, this.nullColor, this.BackColor, flags);
    }

    public bool ShouldSerializeNullText()
    {
      return !string.IsNullOrEmpty(this.nullText);
    }

    public void ResetNullText()
    {
      this.nullText = "";
    }
  }
}
