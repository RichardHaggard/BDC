// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollablePanelContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.NoItemsDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(false)]
  public class RadScrollablePanelContainer : Panel
  {
    private bool allowAutomaticScrollToControl = true;
    private RadScrollablePanel parentPanel;

    public RadScrollablePanelContainer()
      : this((RadScrollablePanel) null)
    {
    }

    public RadScrollablePanelContainer(RadScrollablePanel parentPanel)
    {
      this.DoubleBuffered = true;
      this.SetStyle(ControlStyles.Selectable, true);
      this.parentPanel = parentPanel;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowAutomaticScrollToControl
    {
      get
      {
        return this.allowAutomaticScrollToControl;
      }
      set
      {
        this.allowAutomaticScrollToControl = value;
      }
    }

    [DefaultValue(true)]
    public override bool AutoScroll
    {
      get
      {
        return base.AutoScroll;
      }
      set
      {
        base.AutoScroll = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [DefaultValue(DockStyle.Fill)]
    [Browsable(false)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
      }
    }

    [DefaultValue(typeof (Point), "1,1")]
    public new Point Location
    {
      get
      {
        return base.Location;
      }
      set
      {
        base.Location = value;
      }
    }

    [DefaultValue("PanelContainer")]
    public new string Name
    {
      get
      {
        return base.Name;
      }
      set
      {
        base.Name = value;
      }
    }

    [DefaultValue(0)]
    public new int TabIndex
    {
      get
      {
        return base.TabIndex;
      }
      set
      {
        base.TabIndex = value;
      }
    }

    public event ScrollbarSynchronizationNeededEventHandler ScrollBarSynchronizationNeeded;

    protected virtual object GetFieldValue(object instance, string fieldName)
    {
      return instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
    }

    protected override Point ScrollToControl(Control activeControl)
    {
      Point point = Point.Empty;
      if (this.AllowAutomaticScrollToControl)
      {
        point = base.ScrollToControl(activeControl);
      }
      else
      {
        point = new Point(-this.parentPanel.HorizontalScrollbar.Value, -this.parentPanel.VerticalScrollbar.Value);
        if (!TelerikHelper.ControlIsReallyVisible(activeControl))
          point = base.ScrollToControl(activeControl);
      }
      if (this.ScrollBarSynchronizationNeeded != null)
        this.ScrollBarSynchronizationNeeded((object) this, new ScrollbarSynchronizationNeededEventArgs()
        {
          ActiveControl = activeControl,
          ScrolledLocation = point
        });
      return point;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Graphics graphics = e.Graphics;
      Padding ncMargin = this.parentPanel.NCMargin;
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
      radGdiGraphics.TranslateTransform(-ncMargin.Left, -ncMargin.Top);
      this.parentPanel.RootElement.Paint((IGraphics) radGdiGraphics, e.ClipRectangle, true);
      radGdiGraphics.TranslateTransform(ncMargin.Left, ncMargin.Top);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 131)
        Telerik.WinControls.NativeMethods.ShowScrollBar(this.Handle, 3, false);
      base.WndProc(ref m);
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      if (this.parentPanel == null || !this.parentPanel.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged += new EventHandler(this.Control_SizeChanged);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      if (this.parentPanel == null || !this.parentPanel.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
    }

    protected internal virtual void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.CalcSize();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged += new EventHandler(this.Control_SizeChanged);
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
    }

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      this.CalcSize();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
      base.Dispose(disposing);
    }

    private void CalcSize()
    {
      if (this.parentPanel == null)
        return;
      this.parentPanel.FitToChildControls();
    }
  }
}
