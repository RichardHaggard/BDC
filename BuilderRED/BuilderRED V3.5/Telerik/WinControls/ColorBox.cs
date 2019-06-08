// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ColorBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public class ColorBox : System.Windows.Forms.Label
  {
    private bool mouseEnter;

    public ColorBox()
    {
      this.BackColor = Color.White;
      this.AutoSize = false;
      this.Size = new Size(41, 23);
      this.BorderStyle = BorderStyle.Fixed3D;
      this.MouseEnter += new EventHandler(this.ColorBox_MouseEnter);
      this.MouseLeave += new EventHandler(this.ColorBox_MouseLeave);
      this.Click += new EventHandler(this.ColorBox_Click);
      this.MouseMove += new MouseEventHandler(this.ColorBox_MouseMove);
    }

    private void ColorBox_MouseMove(object sender, MouseEventArgs e)
    {
      if (!(Cursor.Current != Cursors.Hand))
        return;
      Cursor.Current = Cursors.Hand;
    }

    private void ColorBox_MouseLeave(object sender, EventArgs e)
    {
      this.mouseEnter = false;
      Cursor.Current = Cursors.Default;
      this.Invalidate();
    }

    private void ColorBox_MouseEnter(object sender, EventArgs e)
    {
      this.mouseEnter = true;
      Cursor.Current = Cursors.Hand;
      this.Invalidate();
    }

    public event ColorChangedEventHandler ColorChanged;

    public event ColorDialogCreatedEventHandler ColorDialogCreated;

    private void ColorBox_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.Hand;
      IRadColorDialog dialogForm = RadColorEditor.CreateColorDialogInstance();
      if (this.ColorDialogCreated != null)
        this.ColorDialogCreated((object) this, new ColorDialogEventArgs(dialogForm));
      UserControl selectorInstance = RadColorEditor.CreateColorSelectorInstance() as UserControl;
      ((IColorSelector) dialogForm.RadColorSelector).SelectedColor = this.BackColor;
      ((IColorSelector) dialogForm.RadColorSelector).OldColor = this.BackColor;
      ((IColorSelector) selectorInstance).OkButtonClicked += (ColorChangedEventHandler) ((sender1, args) =>
      {
        ((Form) dialogForm).DialogResult = DialogResult.OK;
        ((Form) dialogForm).Close();
      });
      ((IColorSelector) selectorInstance).CancelButtonClicked += (ColorChangedEventHandler) ((sender1, args) =>
      {
        ((Form) dialogForm).DialogResult = DialogResult.Cancel;
        ((Form) dialogForm).Close();
      });
      selectorInstance.Dock = DockStyle.Fill;
      ((Control) dialogForm).Controls.Add((Control) selectorInstance);
      if (((Form) dialogForm).ShowDialog() == DialogResult.OK)
      {
        this.BackColor = ((IColorSelector) dialogForm.RadColorSelector).SelectedColor;
        if (this.ColorChanged != null)
          this.ColorChanged((object) this, new ColorChangedEventArgs(this.BackColor));
      }
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Cursor.Current = Cursors.Hand;
      if (this.mouseEnter)
      {
        using (Pen pen = new Pen(this.ForeColor))
          e.Graphics.DrawRectangle(pen, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1));
      }
      base.OnPaint(e);
    }
  }
}
