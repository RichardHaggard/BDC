// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [RadToolboxItem(false)]
  [DefaultProperty("Items")]
  public class RadDropDownMenu : RadItemsPopupControl
  {
    private RadElement ownerElement;
    private RadElement popupElement;
    private RadItem clickedItem;
    private Timer childDropDownTimeout;
    private Stack<RadMenuItemBase> itemsToClose;
    private object syncObj;
    private bool showing;
    private Stopwatch dblClickStopWatch;
    private RadMenuItemBase elementUnderMouse;
    private Keys lastPressedKey;

    public RadDropDownMenu()
      : this((RadElement) null)
    {
    }

    public RadDropDownMenu(RadElement ownerElement)
      : base(ownerElement)
    {
      this.AutoSize = true;
      this.Items.ItemsChanged += new ItemChangedDelegate(this.OnItemsChanged);
      this.Items.ItemTypes = new System.Type[6]
      {
        typeof (RadMenuItemBase),
        typeof (RadMenuItem),
        typeof (RadMenuSeparatorItem),
        typeof (RadMenuComboItem),
        typeof (RadMenuButtonItem),
        typeof (RadMenuHeaderItem)
      };
      this.ownerElement = ownerElement;
      this.CausesValidation = false;
      this.Visible = false;
      this.InitializeChildren();
      this.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.FadeAnimationType = FadeAnimationType.FadeIn;
      this.DropShadow = true;
      this.itemsToClose = new Stack<RadMenuItemBase>();
      this.syncObj = new object();
      this.childDropDownTimeout = new Timer();
      this.childDropDownTimeout.Interval = SystemInformation.MenuShowDelay == 0 ? 40 : SystemInformation.MenuShowDelay;
      this.childDropDownTimeout.Tick += new EventHandler(this.OnChildrenDropDown_TimeOut);
    }

    private void OnChildrenDropDown_TimeOut(object sender, EventArgs e)
    {
      this.childDropDownTimeout.Stop();
      this.childDropDownTimeout.Tag = (object) null;
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem == null)
        return;
      lock (this.syncObj)
      {
        while (this.itemsToClose.Count > 0)
        {
          this.itemsToClose.Peek().HideChildItems();
          this.itemsToClose.Pop();
        }
      }
      selectedItem.ShowChildItems();
    }

    protected virtual void InitializeChildren()
    {
      RadElement rootElement = (RadElement) this.RootElement;
      rootElement.StretchVertically = false;
      rootElement.StretchHorizontally = false;
      if (this.popupElement != null)
        return;
      this.popupElement = this.CreatePopupElement();
      rootElement.Children.Add(this.popupElement);
    }

    protected override bool ProcessDialogChar(char charCode)
    {
      return true;
    }

    protected override void OnLoad(Size desiredSize)
    {
      if (this.ownerElement != null && this.ownerElement.IsInValidState(true))
      {
        this.ImageList = this.ownerElement.ElementTree.ComponentTreeHandler.ImageList;
        this.BindingContext = this.ownerElement.ElementTree.Control.BindingContext;
      }
      base.OnLoad(desiredSize);
    }

    public RadItem ClickedItem
    {
      get
      {
        return this.clickedItem;
      }
    }

    public IComponentTreeHandler RootTreeHandler
    {
      get
      {
        bool flag = false;
        IPopupControl ownerPopup = this.OwnerPopup;
        RadElement ownerElement = this.OwnerElement;
        while (!flag)
        {
          if (ownerPopup != null)
          {
            ownerElement = ownerPopup.OwnerElement;
            ownerPopup = ownerPopup.OwnerPopup;
          }
          else
          {
            flag = true;
            if (ownerElement != null && ownerElement.IsInValidState(true))
              return ownerElement.ElementTree.ComponentTreeHandler;
          }
        }
        return (IComponentTreeHandler) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement PopupElement
    {
      get
      {
        return this.popupElement;
      }
      set
      {
        if (this.popupElement == value || value == null)
          return;
        if (this.popupElement != null && !this.popupElement.IsDisposing && !this.popupElement.IsDisposed)
        {
          this.RootElement.Children.Remove(this.popupElement);
          this.popupElement.Disposed -= new EventHandler(this.OnPopupElement_Disposed);
        }
        this.popupElement = value;
        if (!this.popupElement.IsDisposing && !this.popupElement.IsDisposed)
          this.popupElement.Disposed += new EventHandler(this.OnPopupElement_Disposed);
        this.Items.Owner = this.popupElement;
        this.RootElement.Children.Add(this.popupElement);
      }
    }

    private void OnPopupElement_Disposed(object sender, EventArgs e)
    {
      this.popupElement = (RadElement) null;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsTwoColumnMenu
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    public new bool CausesValidation
    {
      get
      {
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string HeaderText
    {
      get
      {
        return (this.popupElement as RadDropDownMenuElement)?.HeaderText;
      }
      set
      {
        RadDropDownMenuElement popupElement = this.popupElement as RadDropDownMenuElement;
        if (popupElement == null)
          return;
        popupElement.HeaderText = value;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image HeaderImage
    {
      get
      {
        return (this.popupElement as RadDropDownMenuElement)?.HeaderImage;
      }
      set
      {
        RadDropDownMenuElement popupElement = this.popupElement as RadDropDownMenuElement;
        if (popupElement == null)
          return;
        popupElement.HeaderImage = value;
      }
    }

    public new void Show()
    {
      if (this.showing)
        base.Show();
      else
        this.ShowCore(this.Location, 0, RadDirection.Down);
    }

    public void Show(int x, int y)
    {
      this.ShowCore(new Point(x, y), 0, RadDirection.Right);
    }

    public new void Show(Point point)
    {
      this.ShowCore(point, 0, RadDirection.Right);
    }

    public void Show(Point point, RadDirection popupDirection)
    {
      this.ShowCore(point, 0, popupDirection);
    }

    public void Show(Control control, int x, int y)
    {
      this.Show(control, new Point(x, y));
    }

    public void Show(Control control, Point point)
    {
      this.Show(control, point, RadDirection.Right);
    }

    public void Show(Control control, Point point, RadDirection popupDirection)
    {
      RadControl radControl = control as RadControl;
      Point screen = control.PointToScreen(point);
      if (radControl == null)
        radControl = TelerikHelper.FindRadControlParent(control);
      if (radControl != null)
      {
        if (radControl.RootElement != null && this.dpiScaleFactor != radControl.RootElement.DpiScaleFactor)
          this.dpiScaleFactor = radControl.RootElement.DpiScaleFactor;
      }
      else
      {
        RadFormControlBase form = control.FindForm() as RadFormControlBase;
        SizeF sizeF;
        if (form != null)
        {
          sizeF = form.RootElement.DpiScaleFactor;
        }
        else
        {
          Point systemDpi = Telerik.WinControls.NativeMethods.GetSystemDpi();
          sizeF = new SizeF((float) systemDpi.X / 96f, (float) systemDpi.Y / 96f);
        }
        if (this.dpiScaleFactor != sizeF)
          this.dpiScaleFactor = sizeF;
      }
      this.ShowCore(screen, 0, popupDirection);
    }

    public void Show(RadItem item, int x, int y)
    {
      this.dpiScaleFactor = item.DpiScaleFactor;
      this.ShowCore(item.PointToScreen(new Point(x, y)), 0, RadDirection.Right);
    }

    public void Show(RadItem item, Point point)
    {
      this.dpiScaleFactor = item.DpiScaleFactor;
      this.ShowCore(item.PointToScreen(point), 0, RadDirection.Right);
    }

    public void Show(RadItem item, Point point, RadDirection popupDirection)
    {
      this.dpiScaleFactor = item.DpiScaleFactor;
      this.ShowCore(item.PointToScreen(point), 0, popupDirection);
    }

    public void Show(RadItem item, int ownerOffset, RadDirection popupDirection)
    {
      this.dpiScaleFactor = item.DpiScaleFactor;
      this.ShowCore(Point.Empty, ownerOffset, popupDirection);
    }

    public override string ThemeClassName
    {
      get
      {
        if (this.ownerElement != null && this.ownerElement.IsInValidState(true))
        {
          IComponentTreeHandler componentTreeHandler = this.OwnerElement.ElementTree.ComponentTreeHandler;
          if (componentTreeHandler != null)
          {
            if (this.OwnerElement is IDropDownMenuOwner)
            {
              if ((this.OwnerElement as IDropDownMenuOwner).DropDownInheritsThemeClassName)
                return componentTreeHandler.ThemeClassName;
            }
            else if (componentTreeHandler is RadDropDownMenu)
              return componentTreeHandler.ThemeClassName;
          }
        }
        return base.ThemeClassName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    protected virtual void ShowCore(Point point, int ownerOffset, RadDirection popupDirection)
    {
      if (this.Visible || this.IsDisposed)
        return;
      if (this.LastShowDpiScaleFactor.IsEmpty)
        this.LastShowDpiScaleFactor = new SizeF(1f, 1f);
      if (this.LastShowDpiScaleFactor != this.dpiScaleFactor)
      {
        this.Scale(new SizeF(this.dpiScaleFactor.Width / this.LastShowDpiScaleFactor.Width, this.dpiScaleFactor.Height / this.LastShowDpiScaleFactor.Height));
        this.LastShowDpiScaleFactor = this.dpiScaleFactor;
      }
      this.showing = true;
      bool isLoaded = this.IsLoaded;
      this.SetTheme();
      if (!isLoaded)
      {
        this.LoadElementTree();
        isLoaded = this.IsLoaded;
      }
      this.SetTheme();
      Point empty = Point.Empty;
      Point location;
      if (this.ownerElement != null && this.ownerElement.IsDesignMode)
      {
        IntPtr activeWindow = Telerik.WinControls.NativeMethods.GetActiveWindow();
        Telerik.WinControls.NativeMethods.POINT pt = new Telerik.WinControls.NativeMethods.POINT(0, 0);
        Telerik.WinControls.NativeMethods.ClientToScreen(new HandleRef((object) this, activeWindow), pt);
        point.X -= pt.x;
        point.Y -= pt.y;
        location = !this.ownerElement.IsInValidState(true) ? this.OnDropDownLocationNeeded(point) : this.ownerElement.ElementTree.Control.PointToScreen(this.ownerElement.ControlBoundingRectangle.Location);
      }
      else
        location = this.ownerElement == null ? this.OnDropDownLocationNeeded(point) : (!this.ownerElement.IsInValidState(true) ? this.OnDropDownLocationNeeded(point) : this.ownerElement.ElementTree.Control.PointToScreen(this.ownerElement.ControlBoundingRectangle.Location));
      location.Offset(ownerOffset, ownerOffset);
      Size size = this.ownerElement != null ? this.ownerElement.ControlBoundingRectangle.Size : Size.Empty;
      Rectangle alignmentRectangle = new Rectangle(location, size);
      this.showing = false;
      if (isLoaded)
      {
        this.RootElement.InvalidateMeasure(true);
        this.RootElement.UpdateLayout();
        this.Size = this.RootElement.DesiredSize.ToSize();
      }
      this.ShowPopup(alignmentRectangle);
      if (!isLoaded)
        return;
      this.RootElement.InvalidateMeasure(true);
      this.RootElement.UpdateLayout();
      this.Size = this.RootElement.DesiredSize.ToSize();
    }

    protected virtual Point OnDropDownLocationNeeded(Point point)
    {
      return point;
    }

    protected virtual bool CanProcessItem(RadMenuItemBase menuItem)
    {
      if (menuItem != null)
        return menuItem.Enabled;
      return false;
    }

    protected virtual void OnItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase != null)
      {
        if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Set)
        {
          radMenuItemBase.HierarchyParent = this.ownerElement as IHierarchicalItem;
          radMenuItemBase.Owner = (object) this.ownerElement;
        }
        else if (operation == ItemsChangeOperation.Removed)
          radMenuItemBase.HierarchyParent = (IHierarchicalItem) null;
      }
      if (operation != ItemsChangeOperation.Inserted || !(radMenuItemBase is RadMenuItem) || radMenuItemBase.Site != null)
        return;
      (radMenuItemBase as RadMenuItem).ShowKeyboardCue = false;
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      RadDropDownMenuElement popupElement = this.popupElement as RadDropDownMenuElement;
      if (popupElement == null || popupElement.ScrollPanel.VerticalScrollBar.Visibility != ElementVisibility.Visible)
        return false;
      if (delta > 0)
        popupElement.ScrollPanel.LineUp();
      else
        popupElement.ScrollPanel.LineDown();
      return true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      RadMenuItemBase elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase;
      if (!this.CanProcessItem(elementAtPoint) || !elementAtPoint.ShouldHandleMouseInput || (elementAtPoint.DropDown == null || elementAtPoint.DropDown.IsVisible))
        return;
      this.childDropDownTimeout.Stop();
      elementAtPoint.ShowChildItems();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      RadMenuItemBase elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase;
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (!this.CanProcessItem(elementAtPoint))
      {
        base.OnMouseMove(e);
      }
      else
      {
        if (selectedItem != null && !object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
        {
          selectedItem.Deselect();
          lock (this.syncObj)
            this.itemsToClose.Push(selectedItem);
        }
        if (object.ReferenceEquals((object) elementAtPoint, this.childDropDownTimeout.Tag))
        {
          this.childDropDownTimeout.Stop();
          this.childDropDownTimeout.Tag = (object) null;
        }
        else if (!object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
        {
          if (this.childDropDownTimeout.Tag == null)
            this.childDropDownTimeout.Tag = (object) selectedItem;
          this.childDropDownTimeout.Start();
        }
        base.OnMouseMove(e);
      }
    }

    protected override void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      for (RadElement ownerElement = this.ownerElement; ownerElement != null && ownerElement.ElementTree != null && ownerElement.ElementTree.Control != null; ownerElement = ((RadPopupControlBase) ownerElement.ElementTree.Control).OwnerElement)
      {
        if (!(ownerElement.ElementTree.Control is RadDropDownMenu))
        {
          if (ownerElement.ElementTree.Control is RadControl)
          {
            ((RadControl) ownerElement.ElementTree.Control).CallOnToolTipTextNeeded(sender, e);
            break;
          }
          break;
        }
      }
      base.OnToolTipTextNeeded(sender, e);
    }

    protected override void OnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      for (RadElement ownerElement = this.ownerElement; ownerElement != null && ownerElement.ElementTree != null && ownerElement.ElementTree.Control != null; ownerElement = ((RadPopupControlBase) ownerElement.ElementTree.Control).OwnerElement)
      {
        if (!(ownerElement.ElementTree.Control is RadDropDownMenu))
        {
          if (ownerElement.ElementTree.Control is RadControl)
          {
            ((RadControl) ownerElement.ElementTree.Control).CallOnScreenTipNeeded(sender, e);
            break;
          }
          break;
        }
      }
      base.OnScreenTipNeeded(sender, e);
    }

    private RadElement GetRootOwnerElement()
    {
      IPopupControl ownerPopup = this.OwnerPopup;
      if (ownerPopup == null)
        return this.OwnerElement;
      while (ownerPopup != null)
      {
        ownerPopup = ownerPopup.OwnerPopup;
        if (ownerPopup == null)
          return this.OwnerElement;
      }
      return (RadElement) null;
    }

    private void DoDesignTimeDoubleClick(MouseEventArgs e)
    {
      RadMenuItemBase elementUnderMouse = this.elementUnderMouse;
      this.elementUnderMouse = this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase;
      if (this.dblClickStopWatch == null)
        this.dblClickStopWatch = Stopwatch.StartNew();
      else if (this.elementUnderMouse != elementUnderMouse)
      {
        if (this.dblClickStopWatch == null)
          return;
        this.dblClickStopWatch.Reset();
        this.dblClickStopWatch.Start();
      }
      else if (this.dblClickStopWatch.ElapsedMilliseconds < (long) SystemInformation.DoubleClickTime)
      {
        if (this.elementUnderMouse == null)
          return;
        ISite site = this.elementUnderMouse.GetSite();
        if (site == null || (IDesignerHost) site.GetService(typeof (IDesignerHost)) == null)
          return;
        this.ClosePopup(RadPopupCloseReason.Mouse);
        if (this.OwnerPopup != null)
          PopupManager.Default.CloseAllToRoot(RadPopupCloseReason.Mouse, this.OwnerPopup);
        this.dblClickStopWatch.Reset();
        this.elementUnderMouse = (RadMenuItemBase) null;
      }
      else
      {
        this.dblClickStopWatch.Reset();
        this.elementUnderMouse = (RadMenuItemBase) null;
      }
    }

    protected virtual void DoOnItemClicked(RadMenuItemBase menuItem, MouseEventArgs e)
    {
      if (!menuItem.HasChildren && menuItem.GetSite() == null)
      {
        menuItem.Select();
        PopupManager.Default.CloseAllToRoot(RadPopupCloseReason.Mouse, (IPopupControl) this);
        if (menuItem.RootItem == null || !(menuItem.RootItem.Owner is RadMenuElement) || (!(menuItem.Owner is RadMenuElement) || !((menuItem.Owner as RadMenuElement).ElementTree.Control is RadMenu)))
          return;
        ((menuItem.Owner as RadMenuElement).ElementTree.Control as RadMenu).SetMenuState(RadMenu.RadMenuState.NotActive);
      }
      else
      {
        if (menuItem.GetSite() != null && e.Button == MouseButtons.Right)
          return;
        menuItem.ShowChildItems();
      }
    }

    protected RadMenuItemBase GetMenuItemAtPoint(Point location)
    {
      for (RadElement radElement = this.ElementTree.GetElementAtPoint(location); radElement != null; radElement = radElement.Parent)
      {
        RadMenuItemBase radMenuItemBase = radElement as RadMenuItemBase;
        if (radMenuItemBase != null)
          return radMenuItemBase;
      }
      return (RadMenuItemBase) null;
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      RadMenuItemBase menuItemAtPoint = this.GetMenuItemAtPoint(e.Location);
      base.OnMouseClick(e);
      RadElement rootOwnerElement = this.GetRootOwnerElement();
      if (rootOwnerElement != null && e.Button == MouseButtons.Left && rootOwnerElement.IsDesignMode)
      {
        this.DoDesignTimeDoubleClick(e);
      }
      else
      {
        if (menuItemAtPoint == null || !menuItemAtPoint.Enabled)
          return;
        this.clickedItem = (RadItem) menuItemAtPoint;
        if (menuItemAtPoint is RadMenuButtonItem || menuItemAtPoint is RadMenuComboItem)
          return;
        this.DoOnItemClicked(menuItemAtPoint, e);
        this.PerformMenuItemClick(menuItemAtPoint);
        this.clickedItem = (RadItem) null;
      }
    }

    protected virtual void PerformMenuItemClick(RadMenuItemBase menuItem)
    {
      if (menuItem.GetSite() != null || menuItem.IsMouseOver)
        return;
      menuItem.PerformClick();
    }

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      if (value)
      {
        if (this.ownerElement != null && this.ownerElement.ElementTree != null)
        {
          Control control = this.ownerElement.ElementTree.Control;
          if (control != null)
            control.Capture = false;
        }
        if (!(this.popupElement is RadDropDownMenuElement))
          return;
        int num = (int) this.popupElement.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.Popup);
      }
      else
        (this.GetSelectedItem() as RadMenuItemBase)?.Deselect();
    }

    protected override void OnItemSelected(ItemSelectedEventArgs args)
    {
      base.OnItemSelected(args);
      if (!(args.Item is RadMenuItemBase))
        return;
      RadMenuItemBase radMenuItemBase = args.Item as RadMenuItemBase;
      radMenuItemBase.Selected = true;
      if (radMenuItemBase.Items.Count != 0 && radMenuItemBase.IsPopupShown)
        return;
      this.AccessibilityNotifyClients(AccessibleEvents.Focus, this.Items.IndexOf((RadItem) radMenuItemBase));
    }

    protected override void OnItemDeselected(ItemSelectedEventArgs args)
    {
      base.OnItemDeselected(args);
      if (!(args.Item is RadMenuItemBase))
        return;
      (args.Item as RadMenuItemBase).Selected = false;
    }

    protected override void OnDropDownClosed(RadPopupClosedEventArgs args)
    {
      base.OnDropDownClosed(args);
      this.childDropDownTimeout.Tag = (object) null;
      this.childDropDownTimeout.Stop();
      (this.GetSelectedItem() as RadMenuItemBase)?.Deselect();
      if (!(this.OwnerElement is RadMenuItemBase))
        return;
      RadMenuItemBase ownerElement = this.OwnerElement as RadMenuItemBase;
      if (this.lastPressedKey == Keys.Escape || ownerElement.IsOnDropDown || (!(ownerElement.ElementTree.Control is IItemsControl) || !object.ReferenceEquals((object) ((ownerElement.ElementTree.Control as IItemsControl).GetSelectedItem() as RadMenuItemBase), (object) ownerElement)))
        return;
      ownerElement.Selected = false;
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (reason == RadPopupCloseReason.Mouse)
      {
        if (this.OwnerPopup == null && this.OwnerElement != null && this.RootTreeHandler != null)
        {
          RadMenuItemBase ownerElement = this.OwnerElement as RadMenuItemBase;
          Control rootTreeHandler = this.RootTreeHandler as Control;
          RadElement elementAtPoint = this.RootTreeHandler.ElementTree.GetElementAtPoint(rootTreeHandler.PointToClient(Control.MousePosition));
          if (ownerElement != null && elementAtPoint != null && (object.ReferenceEquals((object) elementAtPoint, (object) ownerElement) && rootTreeHandler.Focused) || ownerElement != null && ownerElement.IsMouseDown)
            return false;
        }
        if (this.OwnerElement != null)
        {
          this.OwnerElement.UpdateContainsMouse();
          if (this.OwnerElement.ContainsMouse || this.OwnerElement.IsMouseDown)
            return false;
        }
      }
      return base.CanClosePopup(reason);
    }

    protected void PerformItemClick(RadMenuItemBase menuItem)
    {
      if (menuItem == null || !menuItem.Enabled)
        return;
      this.clickedItem = (RadItem) menuItem;
      if (menuItem.HasChildren)
      {
        menuItem.ShowChildItems();
        menuItem.DropDown.SelectFirstVisibleItem();
      }
      else
      {
        PopupManager.Default.CloseAllToRoot(RadPopupCloseReason.Keyboard, (IPopupControl) this);
        if (menuItem.Owner is RadMenuElement && (menuItem.Owner as RadMenuElement).ElementTree.Control is RadMenu)
          ((menuItem.Owner as RadMenuElement).ElementTree.Control as RadMenu).SetMenuState(RadMenu.RadMenuState.NotActive);
      }
      menuItem.PerformClick();
      this.clickedItem = (RadItem) null;
    }

    public override bool CanNavigate(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return this.CheckCanNavigate(keyData);
        default:
          return false;
      }
    }

    public override bool CanProcessMnemonic(char keyData)
    {
      return this.CheckCanProcessMnemonic((IItemsControl) this, keyData);
    }

    protected virtual bool CheckCanProcessMnemonic(IItemsControl itemsControl, char keyData)
    {
      bool flag = false;
      foreach (RadItem radItem in (RadItemCollection) itemsControl.Items)
      {
        RadMenuItemBase radMenuItemBase = radItem as RadMenuItemBase;
        if (radMenuItemBase != null && radMenuItemBase.IsPopupShown)
          return this.CheckCanProcessMnemonic((IItemsControl) radMenuItemBase.DropDown, keyData);
        if (Control.IsMnemonic(keyData, radItem.Text))
          flag = radMenuItemBase == null || !radMenuItemBase.HasChildren;
      }
      return flag;
    }

    protected virtual bool CheckCanNavigate(Keys keyData)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem != null)
      {
        switch (keyData)
        {
          case Keys.Left:
            if (selectedItem.HierarchyParent != null && !selectedItem.HierarchyParent.IsRootItem)
              return true;
            break;
          case Keys.Right:
            return selectedItem.Enabled && selectedItem.HasChildren;
          default:
            return true;
        }
      }
      return false;
    }

    public override bool OnKeyDown(Keys keyData)
    {
      this.lastPressedKey = keyData;
      switch (keyData)
      {
        case Keys.Back:
          return false;
        case Keys.Return:
          RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
          if (selectedItem == null)
            return false;
          this.BeginInvoke((Delegate) new RadDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) selectedItem);
          return true;
        case Keys.Left:
        case Keys.Right:
          return this.ProcessLeftRightNavigationKey(keyData == Keys.Left ^ this.RightToLeft == RightToLeft.Yes);
        case Keys.Up:
        case Keys.Down:
          return this.ProcessUpDownNavigationKey(keyData == Keys.Up);
        default:
          if (this.ProcessMnemonic(keyData))
            return true;
          return base.OnKeyDown(keyData);
      }
    }

    protected virtual bool ProcessMnemonic(Keys keyData)
    {
      string unicode = TelerikHelper.KeyCodeToUnicode(keyData);
      if (string.IsNullOrEmpty(unicode))
        return false;
      char charCode = unicode[0];
      List<RadItem> radItemList = new List<RadItem>();
      int num = -1;
      RadItem selectedItem = this.GetSelectedItem();
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (Control.IsMnemonic(charCode, radItem.Text) && radItem.Enabled && radItem.Visibility == ElementVisibility.Visible)
        {
          radItemList.Add(radItem);
          if (selectedItem == radItem)
            num = radItemList.Count - 1;
        }
      }
      if (radItemList.Count == 1)
      {
        radItemList[0].Select();
        this.BeginInvoke((Delegate) new RadDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) radItemList[0]);
        return true;
      }
      if (radItemList.Count <= 0)
        return false;
      int index = (num + 1) % radItemList.Count;
      radItemList[index].Focus();
      this.SelectItem(radItemList[index]);
      return true;
    }

    protected virtual bool ProcessLeftRightNavigationKey(bool isLeft)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem == null)
        return false;
      if (!isLeft)
      {
        if (!selectedItem.HasChildren)
          return false;
        selectedItem.ShowChildItems();
        selectedItem.DropDown.SelectFirstVisibleItem();
        return true;
      }
      if (selectedItem.HierarchyParent == null || selectedItem.HierarchyParent.IsRootItem && this.OwnerPopup == null)
        return false;
      this.ClosePopup(RadPopupCloseReason.Keyboard);
      return true;
    }

    protected virtual void EnsureItemEnabled(RadItem item, bool isUp)
    {
      do
      {
        item = this.GetNextItem(item, !isUp);
      }
      while (!item.Enabled);
      this.SelectItem(item);
    }

    protected virtual bool ProcessUpDownNavigationKey(bool isUp)
    {
      RadMenuItemBase selectedItem1 = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem1 != null)
        this.EnsureItemEnabled((RadItem) selectedItem1, isUp);
      else if (!isUp)
      {
        this.SelectFirstVisibleItem();
        RadMenuItemBase selectedItem2 = this.GetSelectedItem() as RadMenuItemBase;
        if (!selectedItem2.Enabled)
          this.EnsureItemEnabled((RadItem) selectedItem2, isUp);
      }
      else
      {
        this.SelectLastVisibleItem();
        RadMenuItemBase selectedItem2 = this.GetSelectedItem() as RadMenuItemBase;
        if (!selectedItem2.Enabled)
          this.EnsureItemEnabled((RadItem) selectedItem2, isUp);
      }
      return true;
    }

    protected virtual RadElement CreatePopupElement()
    {
      RadDropDownMenuElement dropDownMenuElement = new RadDropDownMenuElement();
      this.Items.Owner = dropDownMenuElement.LayoutPanel;
      return (RadElement) dropDownMenuElement;
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (this.OwnerElement != null && this.ownerElement.ElementTree != null)
      {
        RadControl control = this.OwnerElement.ElementTree.Control as RadControl;
        if (control != null && this.OwnerElement is IDropDownMenuOwner)
        {
          if (((IDropDownMenuOwner) this.OwnerElement).DropDownInheritsThemeClassName)
            return control.ControlDefinesThemeForElement(element);
          return ((IDropDownMenuOwner) this.OwnerElement).ControlDefinesThemeForElement(element);
        }
      }
      System.Type type = element.GetType();
      if (type.Equals(typeof (RadTextBoxElement)))
      {
        if (element.FindAncestorByThemeEffectiveType(typeof (RadDropDownListElement)) != null)
          return true;
      }
      else if (type.Equals(typeof (RadMaskedEditBoxElement)) && element.FindAncestor<RadDateTimePickerElement>() != null)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    internal void SetTheme()
    {
      bool flag = false;
      if (this.ownerElement != null && this.ownerElement.ElementTree != null)
      {
        Control control = this.ownerElement.ElementTree.Control;
        IComponentTreeHandler componentTreeHandler = this.ownerElement.ElementTree.ComponentTreeHandler;
        if (control != null && !string.IsNullOrEmpty(componentTreeHandler.ThemeName))
        {
          if (this.ThemeName != componentTreeHandler.ThemeName)
            this.ThemeName = componentTreeHandler.ThemeName;
          flag = true;
        }
      }
      if (flag || !string.IsNullOrEmpty(this.ThemeName))
        return;
      this.ThemeName = "ControlDefault";
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.childDropDownTimeout.Dispose();
        this.ownerElement = (RadElement) null;
        this.Items.ItemsChanged -= new ItemChangedDelegate(this.OnItemsChanged);
        this.ImageList = (ImageList) null;
        this.BindingContext = (BindingContext) null;
      }
      base.Dispose(disposing);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadDropDownMenuAccessibleObject(this);
    }

    internal delegate void PerformClickInvoker(RadMenuItemBase item);
  }
}
