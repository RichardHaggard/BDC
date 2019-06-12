// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PopupEditorBaseElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public abstract class PopupEditorBaseElement : EditorBaseElement
  {
    internal static PopupManager manager = PopupManager.Default;
    internal const long PopupEditorBaseElementLastStateKey = 17592186044416;
    private RadPopupControlBase popupForm;
    private int ownerOffset;

    protected override void DisposeManagedResources()
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
      this.DisposePopupForm();
      base.DisposeManagedResources();
    }

    protected void DisposePopupForm()
    {
      this.DisposePopupFormCore(true);
    }

    protected virtual void DisposePopupFormCore(bool dispose)
    {
      if (this.popupForm == null)
        return;
      this.UnwirePopupFormEvents(this.PopupForm);
      this.popupForm.Dispose();
      this.popupForm = (RadPopupControlBase) null;
    }

    protected virtual void WirePopupFormEvents(RadPopupControlBase popup)
    {
    }

    protected virtual void UnwirePopupFormEvents(RadPopupControlBase popup)
    {
    }

    public int OwnerOffset
    {
      get
      {
        return this.ownerOffset;
      }
      set
      {
        if (this.ownerOffset == value)
          return;
        this.ownerOffset = value;
        this.OnNotifyPropertyChanged(nameof (OwnerOffset));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public virtual RadPopupControlBase PopupForm
    {
      get
      {
        return this.GetPopupForm();
      }
    }

    [Browsable(false)]
    public virtual bool IsPopupOpen
    {
      get
      {
        if (this.popupForm != null)
          return PopupManager.Default.ContainsPopup((IPopupControl) this.popupForm);
        return false;
      }
    }

    [Browsable(false)]
    public bool EditorContainsFocus
    {
      get
      {
        if (this.ElementTree == null)
          return false;
        if (this.ElementTree.Control.ContainsFocus)
          return true;
        if (this.IsPopupOpen)
          return this.PopupForm.ContainsFocus;
        return false;
      }
    }

    public event EventHandler PopupOpened;

    public event CancelEventHandler PopupOpening;

    public event RadPopupClosingEventHandler PopupClosing;

    public event RadPopupClosedEventHandler PopupClosed;

    protected virtual void TooglePopupState()
    {
      if (this.IsPopupOpen)
        this.ClosePopup();
      else
        this.ShowPopup();
    }

    public void ClosePopup()
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    public virtual void ClosePopup(RadPopupCloseReason reason)
    {
      if (reason != RadPopupCloseReason.CloseCalled)
      {
        CancelEventArgs e = new CancelEventArgs();
        this.OnQueryValue(e);
        if (e.Cancel)
          return;
      }
      if (this.popupForm == null)
        return;
      this.popupForm.ClosePopup(reason);
    }

    public virtual void ShowPopup()
    {
      if (!this.CanDisplayPopup())
        return;
      RadPopupControlBase popupForm = this.PopupForm;
      if (popupForm == null)
        return;
      if (popupForm.ElementTree.RootElement.ElementState != ElementState.Loaded)
        this.PopupForm.LoadElementTree(this.GetInitialPopupSize());
      this.UpdatePopupMinMaxSize(popupForm);
      this.ApplyThemeToPopup(this.PopupForm);
      Point popupLocation = this.GetPopupLocation(popupForm);
      popupForm.Size = this.GetPopupSize(popupForm, popupForm.RootElement.MeasureIsDirty || popupForm.RootElement.NeverMeasured);
      popupForm.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
      popupForm.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
      if (this.RightToLeft)
        this.popupForm.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToRight;
      this.ShowPopupCore(popupForm);
      popupForm.ShowPopup(new Rectangle(popupLocation, this.ControlBoundingRectangle.Size));
    }

    protected virtual Size GetInitialPopupSize()
    {
      return Size.Empty;
    }

    protected virtual void ShowPopupCore(RadPopupControlBase popup)
    {
    }

    protected virtual Point GetPopupLocation(RadPopupControlBase popup)
    {
      return this.ElementTree.Control.PointToScreen(this.ControlBoundingRectangle.Location);
    }

    protected virtual Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      return Size.Empty;
    }

    protected virtual void UpdatePopupMinMaxSize(RadPopupControlBase popup)
    {
    }

    public static void ApplyThemeToPopup(RadElementTree elementTree, RadPopupControlBase popup)
    {
      string str = "ControlDefault";
      if (elementTree != null && elementTree.ComponentTreeHandler != null && !string.IsNullOrEmpty(elementTree.ComponentTreeHandler.ThemeName))
        str = elementTree.ComponentTreeHandler.ThemeName;
      if (!(popup.ThemeName != str))
        return;
      popup.ThemeName = str;
      if (popup.RootElement.ElementState != ElementState.Loaded)
        return;
      popup.RootElement.UpdateLayout();
    }

    protected virtual void ApplyThemeToPopup(RadPopupControlBase popup)
    {
      PopupEditorBaseElement.ApplyThemeToPopup((RadElementTree) this.ElementTree, popup);
    }

    protected virtual bool CanDisplayPopup()
    {
      if (this.IsPopupOpen)
        return false;
      return this.ElementState == ElementState.Loaded;
    }

    protected virtual RadPopupControlBase CreatePopupForm()
    {
      RadEditorPopupControlBase popupControlBase = new RadEditorPopupControlBase((RadItem) this);
      popupControlBase.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.WirePopupFormEvents((RadPopupControlBase) popupControlBase);
      return (RadPopupControlBase) popupControlBase;
    }

    protected internal virtual RadPopupControlBase GetPopupForm()
    {
      if (this.popupForm == null && !this.IsDisposed)
      {
        this.popupForm = this.CreatePopupForm();
        this.popupForm.PopupOpening += new RadPopupOpeningEventHandler(this.OnPopupForm_Opening);
        this.popupForm.PopupOpened += new RadPopupOpenedEventHandler(this.OnPopupForm_Opened);
        this.popupForm.PopupClosing += new RadPopupClosingEventHandler(this.OnPopupForm_Closing);
        this.popupForm.PopupClosed += new RadPopupClosedEventHandler(this.OnPopupForm_Closed);
      }
      return this.popupForm;
    }

    private void OnPopupForm_Closed(object sender, RadPopupClosedEventArgs args)
    {
      this.OnPopupClosed(args);
    }

    private void OnPopupForm_Closing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnPopupClosing(args);
    }

    private void OnPopupForm_Opened(object sender, EventArgs args)
    {
      this.OnPopupOpened(args);
    }

    private void OnPopupForm_Opening(object sender, CancelEventArgs args)
    {
      this.OnPopupOpening(args);
    }

    protected virtual void OnPopupOpening(CancelEventArgs e)
    {
      if (this.PopupOpening == null)
        return;
      this.PopupOpening((object) this, e);
    }

    protected virtual void OnPopupOpened(EventArgs args)
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "DropDownOpened", (object) null);
      if (this.PopupOpened == null)
        return;
      this.PopupOpened((object) this, args);
    }

    protected virtual void OnPopupClosing(RadPopupClosingEventArgs e)
    {
      if (this.PopupClosing == null)
        return;
      this.PopupClosing((object) this, e);
    }

    protected virtual void OnPopupClosed(RadPopupClosedEventArgs e)
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "DropDownClosed", (object) null);
      if (this.PopupClosed == null)
        return;
      this.PopupClosed((object) this, e);
    }

    protected void OnEditorKeyDown(KeyEventArgs e)
    {
      if (e.Handled || e.KeyData != (Keys.Down | Keys.Alt))
        return;
      if (this.IsPopupOpen)
        this.ClosePopup(RadPopupCloseReason.Keyboard);
      else
        this.ShowPopup();
      e.Handled = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      int button = (int) e.Button;
      base.OnMouseDown(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadObject.BindingContextProperty)
        return;
      this.PopupForm.BindingContext = this.BindingContext;
    }

    protected internal virtual void ProcessPopupTabKey(KeyEventArgs e)
    {
      this.ClosePopup(RadPopupCloseReason.Keyboard);
      if (this.IsPopupOpen)
        return;
      this.ProcessDialogKey(e.KeyData);
    }
  }
}
