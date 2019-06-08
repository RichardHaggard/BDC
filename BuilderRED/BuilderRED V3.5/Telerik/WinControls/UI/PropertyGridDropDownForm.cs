// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridDropDownForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class PropertyGridDropDownForm : RadForm
  {
    private Control currentControl;
    private IWindowsFormsEditorService service;

    public PropertyGridDropDownForm(IWindowsFormsEditorService service)
    {
      this.StartPosition = FormStartPosition.Manual;
      this.currentControl = (Control) null;
      this.ShowInTaskbar = false;
      this.ControlBox = false;
      this.MinimizeBox = false;
      this.MaximizeBox = false;
      this.Text = "";
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = false;
      this.service = service;
    }

    public Control Component
    {
      get
      {
        return this.currentControl;
      }
      set
      {
        if (this.currentControl != null)
        {
          this.Controls.Remove(this.currentControl);
          this.currentControl = (Control) null;
        }
        if (value != null)
        {
          this.currentControl = value;
          this.Controls.Add(this.currentControl);
          this.Padding = new Padding(1);
          this.Size = new Size(2 + this.currentControl.Width, 2 + this.currentControl.Height);
          this.currentControl.Dock = DockStyle.Fill;
          this.currentControl.Visible = true;
        }
        this.Enabled = this.currentControl != null;
      }
    }

    protected override void OnMouseDown(MouseEventArgs me)
    {
      if (me.Button == MouseButtons.Left)
        this.service.CloseDropDown();
      base.OnMouseDown(me);
    }

    protected override void OnClosed(EventArgs args)
    {
      if (this.Visible)
        this.service.CloseDropDown();
      base.OnClosed(args);
    }

    protected override void OnDeactivate(EventArgs args)
    {
      if (this.Visible)
        this.service.CloseDropDown();
      base.OnDeactivate(args);
    }
  }
}
