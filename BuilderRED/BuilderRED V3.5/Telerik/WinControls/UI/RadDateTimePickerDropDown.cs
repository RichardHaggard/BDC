// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePickerDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadDateTimePickerDropDown : RadEditorPopupControlBase, INotifyPropertyChanged
  {
    private Rectangle backupBounds = Rectangle.Empty;
    private Size minimum = Size.Empty;
    private Size maximum = Size.Empty;
    private RadControl hostedControl;
    internal bool calendarLostFocus;
    [Description("The owner control of the popup")]
    public RadControl OwnerControl;
    private int backupHeight;
    private int backupWidth;
    private Size lastSize;

    [Description("Occurs when the drop down is opened")]
    [Category("Action")]
    public event EventHandler Opened;

    [Description("Occurs when the drop down is opening")]
    [Category("Action")]
    public event CancelEventHandler Opening;

    [Category("Action")]
    [Description("Occurs when the drop down is closing")]
    public event RadPopupClosingEventHandler Closing;

    [Category("Action")]
    [Description("Occurs when the drop down is closed")]
    public event RadPopupClosedEventHandler Closed;

    public RadDateTimePickerDropDown(RadItem ownerElement)
      : base(ownerElement)
    {
      this.PopupClosed += new RadPopupClosedEventHandler(this.RadDateTimePickerDropDown_PopupClosed);
      this.PopupClosing += new RadPopupClosingEventHandler(this.RadDateTimePickerDropDown_PopupClosing);
      this.PopupOpened += new RadPopupOpenedEventHandler(this.RadDateTimePickerDropDown_PopupOpened);
      this.PopupOpening += new RadPopupOpeningEventHandler(this.RadDateTimePickerDropDown_PopupOpening);
    }

    [Description("Gets or sets the hosted control in the popup.")]
    [Browsable(false)]
    [Category("Behavior")]
    public RadControl HostedControl
    {
      get
      {
        return this.hostedControl;
      }
      set
      {
        if (this.hostedControl == value)
          return;
        this.hostedControl = value;
        if (this.hostedControl == null)
          this.Controls.Clear();
        else
          this.Controls.Add((Control) value);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (HostedControl)));
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.SizingGripDockLayout.LastChildFill = false;
    }

    public Point ShowControl(RadDirection popupDirection, int ownerOffset)
    {
      bool flag = false;
      this.DropDownAnimationDirection = popupDirection;
      if (this.OwnerElement is RadDateTimePickerElement)
      {
        this.SizingGrip.MinSize = new Size(10, 10);
        this.SizingGrip.Visibility = ElementVisibility.Visible;
        this.SizingGrip.GripItemNSEW.Alignment = ContentAlignment.BottomRight;
      }
      if (this.SizingGrip != null)
      {
        if (flag)
          DockLayoutPanel.SetDock((RadElement) this.SizingGrip, Dock.Top);
        else
          DockLayoutPanel.SetDock((RadElement) this.SizingGrip, Dock.Bottom);
      }
      if (!(this.OwnerElement as RadDateTimePickerElement).RightToLeft)
        this.SizingGrip.RightToLeft = false;
      else
        this.SizingGrip.RightToLeft = true;
      this.AutoUpdateBounds();
      this.ShowPopup(this.OwnerControl.RectangleToScreen(this.OwnerElement.ControlBoundingRectangle));
      return this.Location;
    }

    public void HideControl()
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override Size GetBackupSize()
    {
      SizeF scaleFactor = new SizeF(1f, 1f);
      if (this.LastShowDpiScaleFactor.IsEmpty)
        scaleFactor = new SizeF(1f / this.OwnerElement.DpiScaleFactor.Width, 1f / this.OwnerElement.DpiScaleFactor.Height);
      return TelerikDpiHelper.ScaleSize(this.Size, scaleFactor);
    }

    internal Size Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        this.minimum = value;
      }
    }

    internal Size Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        this.maximum = value;
      }
    }

    protected void AutoUpdateBounds()
    {
      this.minimum = new Size(this.Width, 0);
      this.maximum = this.Size;
    }

    protected virtual void BackupBounds()
    {
      this.backupBounds = this.Bounds;
      if (this.backupHeight == 0)
        this.backupHeight = this.Height;
      if (this.backupWidth != 0)
        return;
      this.backupWidth = this.Width;
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      if (this.SizingGrip == null)
        return;
      this.SizingGrip.RightToLeft = this.RightToLeft == RightToLeft.Yes;
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (this.HostedControl == null)
        return;
      this.HostedControl.Size = new Size(this.Size.Width, this.Size.Height - (this.SizingGrip.Visibility == ElementVisibility.Visible ? (int) this.SizingGrip.DesiredSize.Height : 0));
      this.lastSize = this.Size;
    }

    protected override void Dispose(bool disposing)
    {
      this.PopupClosed -= new RadPopupClosedEventHandler(this.RadDateTimePickerDropDown_PopupClosed);
      this.PopupClosing -= new RadPopupClosingEventHandler(this.RadDateTimePickerDropDown_PopupClosing);
      this.PopupOpened -= new RadPopupOpenedEventHandler(this.RadDateTimePickerDropDown_PopupOpened);
      this.PopupOpening -= new RadPopupOpeningEventHandler(this.RadDateTimePickerDropDown_PopupOpening);
      base.Dispose(disposing);
    }

    private void RadDateTimePickerDropDown_PopupOpening(object sender, CancelEventArgs args)
    {
      if (this.Opening == null)
        return;
      this.Opening(sender, args);
    }

    private void RadDateTimePickerDropDown_PopupOpened(object sender, EventArgs args)
    {
      if (this.Opened == null)
        return;
      this.Opened(sender, args);
    }

    private void RadDateTimePickerDropDown_PopupClosing(
      object sender,
      RadPopupClosingEventArgs args)
    {
      if (this.Closing == null)
        return;
      this.Closing(sender, args);
    }

    private void RadDateTimePickerDropDown_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      if (this.Closed == null)
        return;
      this.Closed(sender, args);
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (reason != RadPopupCloseReason.Mouse)
        return base.CanClosePopup(reason);
      if (!this.OwnerElement.IsInValidState(true) || !(this.OwnerElement is RadDateTimePickerElement))
        return base.CanClosePopup(reason);
      RadDateTimePickerElement ownerElement = this.OwnerElement as RadDateTimePickerElement;
      if (ownerElement.ArrowButton == null)
        return base.CanClosePopup(reason);
      return !ownerElement.ArrowButton.IsMouseOver;
    }
  }
}
