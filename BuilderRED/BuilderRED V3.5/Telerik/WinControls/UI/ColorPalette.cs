// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColorPalette
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class ColorPalette : UserControl
  {
    private int columns;
    private int selectedColorIndex;
    private int colorMargin;
    private Color[] colors;
    private Color selectedColor;
    private IContainer components;

    public ColorPalette()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public int Columns
    {
      get
      {
        return this.columns;
      }
      set
      {
        this.columns = value;
      }
    }

    public int ColorMargin
    {
      get
      {
        return this.colorMargin;
      }
      set
      {
        this.colorMargin = value;
      }
    }

    public Color[] Colors
    {
      get
      {
        return this.colors;
      }
      set
      {
        this.colors = value;
      }
    }

    public Color SelectedColor
    {
      get
      {
        return this.selectedColor;
      }
      set
      {
        this.selectedColor = value;
        this.Refresh();
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.colors == null || this.colors.Length <= 0)
        return;
      int num1 = this.Colors.Length / this.columns;
      int num2 = (this.Width - 4 + this.colorMargin) / this.columns;
      int num3 = (this.Height - 4 + this.colorMargin) / num1;
      int index1 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        for (int index3 = 0; index3 < this.columns; ++index3)
        {
          Rectangle rectangle = new Rectangle(2 + num2 * index3, 2 + num3 * index2, num2 - this.colorMargin, num3 - this.colorMargin);
          using (Brush brush = (Brush) new SolidBrush(this.colors[index1]))
            e.Graphics.FillRectangle(brush, rectangle);
          if (this.colors[index1] == this.SelectedColor)
          {
            --rectangle.Width;
            --rectangle.Height;
            using (Pen pen = new Pen(SystemColors.Highlight, 2f))
              e.Graphics.DrawRectangle(pen, rectangle);
          }
          else
            ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
          ++index1;
          if (index1 >= this.colors.Length)
            return;
        }
      }
    }

    private void SelectionChange(bool left, bool right, bool up, bool down)
    {
      if (this.colors == null || this.colors.Length <= 0)
        return;
      int selectedColorIndex = this.selectedColorIndex;
      if (selectedColorIndex - 1 >= 0 && left)
      {
        this.SelectedColor = this.colors[selectedColorIndex - 1];
        --selectedColorIndex;
      }
      if (selectedColorIndex + 1 < this.colors.Length && right)
      {
        this.SelectedColor = this.colors[selectedColorIndex + 1];
        ++selectedColorIndex;
      }
      if (selectedColorIndex - 8 >= 0 && up)
      {
        this.SelectedColor = this.colors[selectedColorIndex - 8];
        selectedColorIndex -= 8;
      }
      if (selectedColorIndex + 8 <= this.colors.Length && down)
      {
        this.SelectedColor = this.colors[selectedColorIndex + 8];
        selectedColorIndex += 8;
      }
      this.selectedColorIndex = selectedColorIndex;
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
          this.SelectionChange(true, false, false, false);
          break;
        case Keys.Up:
          this.SelectionChange(false, false, true, false);
          break;
        case Keys.Right:
          this.SelectionChange(false, true, false, false);
          break;
        case Keys.Down:
          this.SelectionChange(false, false, false, true);
          break;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.colors == null || this.colors.Length <= 0)
        return;
      int num1 = this.Colors.Length / this.columns;
      int num2 = (this.Width + this.colorMargin) / this.columns;
      int num3 = (this.Height + this.colorMargin) / num1;
      int index1 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        for (int index3 = 0; index3 < this.columns; ++index3)
        {
          if (new Rectangle(num2 * index3, num3 * index2, num2 - this.colorMargin, num3 - this.colorMargin).Contains(e.X, e.Y))
          {
            this.SelectedColor = this.colors[index1];
            this.selectedColorIndex = index1;
            if (this.ColorChanged == null)
              return;
            this.ColorChanged((object) this, new ColorChangedEventArgs(this.SelectedColor));
            return;
          }
          ++index1;
          if (index1 >= this.colors.Length)
            return;
        }
      }
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
