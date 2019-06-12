// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMDIControlsItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class RadMDIControlsItem : StackLayoutElement
  {
    private RadImageButtonElement closeButton;
    private RadImageButtonElement minimizeButton;
    private RadImageButtonElement maximizeButton;
    private Form hostForm;

    public RadButtonElement MinimizeButton
    {
      get
      {
        return (RadButtonElement) this.minimizeButton;
      }
    }

    public RadButtonElement MaximizeButton
    {
      get
      {
        return (RadButtonElement) this.maximizeButton;
      }
    }

    public RadButtonElement CloseButton
    {
      get
      {
        return (RadButtonElement) this.closeButton;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Visibility = ElementVisibility.Collapsed;
      this.StretchHorizontally = false;
      this.Orientation = Orientation.Horizontal;
      this.FitInAvailableSize = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.minimizeButton = new RadImageButtonElement();
      this.minimizeButton.Class = "RibbonMDIMinimizeButton";
      this.minimizeButton.Click += new EventHandler(this.OnMinimize);
      this.minimizeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.MDIminimize;
      this.minimizeButton.StretchHorizontally = false;
      this.minimizeButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.minimizeButton);
      this.maximizeButton = new RadImageButtonElement();
      this.maximizeButton.Class = "RibbonMDIMaximizeButton";
      this.maximizeButton.Click += new EventHandler(this.OnMaximizeRestore);
      this.maximizeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.MDImaximize;
      this.maximizeButton.StretchHorizontally = false;
      this.maximizeButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.maximizeButton);
      this.closeButton = new RadImageButtonElement();
      this.closeButton.Class = "RibbonMDICloseButton";
      this.closeButton.Click += new EventHandler(this.OnClose);
      this.closeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.MDIclose;
      this.closeButton.StretchHorizontally = false;
      this.closeButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.closeButton);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.hostForm == null)
        return;
      this.hostForm.Layout -= new LayoutEventHandler(this.hostForm_Layout);
      this.hostForm.MdiChildActivate -= new EventHandler(this.hostForm_MdiChildActivate);
    }

    public void LayoutPropertyChanged()
    {
      if (this.hostForm != null || this.ElementTree == null)
        return;
      this.hostForm = this.ElementTree.Control.FindForm();
      if (this.hostForm == null)
        return;
      this.hostForm.Layout += new LayoutEventHandler(this.hostForm_Layout);
      this.hostForm.MdiChildActivate += new EventHandler(this.hostForm_MdiChildActivate);
    }

    protected virtual void OnHostFormLayout()
    {
      this.UpdateVisibility();
    }

    public void UpdateVisibility()
    {
      if (this.hostForm == null || !this.hostForm.IsMdiContainer)
        return;
      Form form = (Form) null;
      foreach (Form mdiChild in this.hostForm.MdiChildren)
      {
        if (mdiChild is ShapedForm)
        {
          foreach (Control control in (ArrangedElementCollection) mdiChild.Controls)
          {
            if (control is RadTitleBar)
              control.Visible = mdiChild.WindowState != FormWindowState.Maximized;
          }
        }
        if (mdiChild.WindowState == FormWindowState.Maximized && (this.hostForm.ActiveMdiChild == null || this.hostForm.ActiveMdiChild == mdiChild))
        {
          form = mdiChild;
          break;
        }
      }
      if (form == null)
      {
        this.maximizeButton.Enabled = true;
        this.minimizeButton.Enabled = true;
        this.Visibility = ElementVisibility.Collapsed;
        this.InvalidateMeasure();
      }
      else
      {
        this.maximizeButton.Enabled = form.MaximizeBox;
        this.minimizeButton.Enabled = form.MinimizeBox;
        FormBorderStyle formBorderStyle = form is RadFormControlBase ? ((RadFormControlBase) form).FormBorderStyle : form.FormBorderStyle;
        if (form != null && form.Visible && (form.ControlBox && formBorderStyle != FormBorderStyle.None) && (formBorderStyle != FormBorderStyle.SizableToolWindow && formBorderStyle != FormBorderStyle.FixedToolWindow))
        {
          this.Visibility = ElementVisibility.Visible;
          this.InvalidateMeasure();
        }
        else
        {
          this.Visibility = ElementVisibility.Collapsed;
          this.InvalidateMeasure();
        }
      }
    }

    private void OnMaximizeRestore(object sender, EventArgs args)
    {
      this.PerfomMdiCommand(FormWindowState.Normal);
      this.hostForm_Layout(sender, (LayoutEventArgs) null);
    }

    private void OnMinimize(object sender, EventArgs args)
    {
      this.PerfomMdiCommand(FormWindowState.Minimized);
      this.hostForm_Layout(sender, (LayoutEventArgs) null);
    }

    private void OnClose(object sender, EventArgs args)
    {
      this.Visibility = ElementVisibility.Collapsed;
      if (this.hostForm == null || !this.hostForm.IsMdiContainer)
        return;
      Form activeMdiChild = this.hostForm.ActiveMdiChild;
      if (activeMdiChild == null)
        return;
      activeMdiChild.Close();
      if (this.hostForm.ActiveMdiChild == null)
        return;
      this.Visibility = ElementVisibility.Visible;
    }

    private void hostForm_MdiChildActivate(object sender, EventArgs e)
    {
      if (this.hostForm == null || this.hostForm.ActiveMdiChild == null)
        return;
      this.hostForm.ActiveMdiChild.VisibleChanged += new EventHandler(this.ActiveMdiChild_VisibleChanged);
    }

    private void ActiveMdiChild_VisibleChanged(object sender, EventArgs e)
    {
      if (!(sender is Form))
        return;
      (sender as Form).VisibleChanged -= new EventHandler(this.ActiveMdiChild_VisibleChanged);
      this.OnHostFormLayout();
    }

    private void hostForm_Layout(object sender, LayoutEventArgs e)
    {
      this.OnHostFormLayout();
    }

    private void PerfomMdiCommand(FormWindowState newState)
    {
      if (this.hostForm == null || !this.hostForm.IsMdiContainer)
        return;
      Form activeMdiChild = this.hostForm.ActiveMdiChild;
      if (activeMdiChild == null)
        return;
      activeMdiChild.WindowState = newState;
      activeMdiChild.ControlBox = newState != FormWindowState.Maximized;
    }
  }
}
