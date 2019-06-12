// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarBackstageView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadRibbonBarBackstageViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadRibbonBarBackstageView : RadControl
  {
    public static readonly object BackstageViewClosedEventKey = new object();
    public static readonly object BackstageViewClosingEventKey = new object();
    public static readonly object BackstageViewOpenedEventKey = new object();
    public static readonly object BackstageViewOpeningEventKey = new object();
    private bool adjustLocation = true;
    private Font keyTipFont = new Font("Arial", 10f, FontStyle.Regular);
    private BackstageViewElement backstageElement;
    private RadRibbonBarElement owner;
    private RadRibbonBarBackstageView.RibbonBackstageViewInputBehavior backstageBehavior;
    private bool isShown;
    private Form associatedForm;
    private bool isInHidePopup;

    public event EventHandler BackstageViewClosed
    {
      add
      {
        this.Events.AddHandler(RadRibbonBarBackstageView.BackstageViewClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRibbonBarBackstageView.BackstageViewClosedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler BackstageViewClosing
    {
      add
      {
        this.Events.AddHandler(RadRibbonBarBackstageView.BackstageViewClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRibbonBarBackstageView.BackstageViewClosingEventKey, (Delegate) value);
      }
    }

    public event EventHandler BackstageViewOpened
    {
      add
      {
        this.Events.AddHandler(RadRibbonBarBackstageView.BackstageViewOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRibbonBarBackstageView.BackstageViewOpenedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler BackstageViewOpening
    {
      add
      {
        this.Events.AddHandler(RadRibbonBarBackstageView.BackstageViewOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRibbonBarBackstageView.BackstageViewOpeningEventKey, (Delegate) value);
      }
    }

    public event EventHandler<BackstageItemEventArgs> ItemClicked
    {
      add
      {
        this.backstageElement.ItemClicked += value;
      }
      remove
      {
        this.backstageElement.ItemClicked -= value;
      }
    }

    public event EventHandler<BackstageItemChangingEventArgs> SelectedItemChanging
    {
      add
      {
        this.backstageElement.SelectedItemChanging += value;
      }
      remove
      {
        this.backstageElement.SelectedItemChanging -= value;
      }
    }

    public event EventHandler<BackstageItemChangeEventArgs> SelectedItemChanged
    {
      add
      {
        this.backstageElement.SelectedItemChanged += value;
      }
      remove
      {
        this.backstageElement.SelectedItemChanged -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the RadRibbonBarBAckstageView is painting Key tips")]
    public event CancelEventHandler KeyTipShowing;

    [Category("Behavior")]
    [Description("Occurs when the user is press Key tip")]
    public event CancelEventHandler KeyTipActivating;

    [Browsable(false)]
    [Description("Gets or sets a value that indicates whether the position of the BackstageView should be automatically adjusted to the bottom of the application button of the owner RadRibbonBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AdjustLocation
    {
      get
      {
        return this.adjustLocation;
      }
      set
      {
        this.adjustLocation = value;
      }
    }

    [Description("Gets or sets the selected tab.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public BackstageTabItem SelectedItem
    {
      get
      {
        return this.backstageElement.SelectedItem;
      }
      set
      {
        this.backstageElement.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    [Browsable(false)]
    [Description("Indicates whether the backstage view is opened.")]
    public bool IsShown
    {
      get
      {
        return this.isShown;
      }
    }

    [Browsable(false)]
    [Description("Gets the backstage element")]
    public BackstageViewElement BackstageElement
    {
      get
      {
        return this.backstageElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadRibbonBarBackstageItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadEditItemsAction]
    [Category("Data")]
    [Description("Gets a collection representing the items contained in this backstage's items panel.")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.backstageElement.Items;
      }
    }

    [Browsable(false)]
    public RadRibbonBarElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.backstageElement = new BackstageViewElement();
      this.RootElement.Children.Add((RadElement) this.backstageElement);
      base.CreateChildItems(parent);
      this.Visible = false;
    }

    protected override ComponentInputBehavior CreateBehavior()
    {
      this.backstageBehavior = new RadRibbonBarBackstageView.RibbonBackstageViewInputBehavior(this);
      return (ComponentInputBehavior) this.backstageBehavior;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (this.associatedForm == null)
        return;
      this.associatedForm.StyleChanged -= new EventHandler(this.OnAssociatedForm_StyleChanged);
    }

    public void ShowPopup(Point location, RadRibbonBarElement owner)
    {
      this.owner = owner;
      Control parent = owner.ElementTree.Control.Parent;
      if (this.OnBackstageViewOpening())
        return;
      if (owner != null)
      {
        owner.ExpandButton.Enabled = false;
        if (owner.Popup != null)
          owner.Popup.ClosePopup(RadPopupCloseReason.AppFocusChange);
      }
      if (this.Parent != parent)
      {
        if (this.Parent != null)
          this.Parent.Controls.Remove((Control) this);
        parent?.Controls.Add((Control) this);
      }
      this.Location = location;
      if (this.Parent != null)
      {
        this.parentControl_SizeChanged((object) this.Parent, EventArgs.Empty);
        this.Parent.SizeChanged += new EventHandler(this.parentControl_SizeChanged);
      }
      this.UpdateControlButtonsStyle();
      this.Visible = true;
      this.BringToFront();
      this.isShown = true;
      this.Focus();
      this.OnBackstageViewOpened();
    }

    public void ShowPopup(RadRibbonBarElement owner)
    {
      if (owner == null || !owner.IsInValidState(true))
        return;
      Point empty = Point.Empty;
      empty.X = owner.ElementTree.Control.Location.X;
      empty.Y = owner.ElementTree.Control.Location.Y;
      this.ThemeName = (owner.ElementTree.Control as RadControl).ThemeName;
      this.RightToLeft = owner.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      if (!this.BackstageElement.IsFullScreenTheme())
      {
        empty.Y += owner.ApplicationButtonElement.ActionButton.ControlBoundingRectangle.Bottom + owner.ApplicationButtonElement.Margin.Bottom;
      }
      else
      {
        Form form = owner.ElementTree.Control.FindForm();
        if (form != null)
          this.BackstageElement.TitleBarElement.Text = form.Text;
      }
      this.ShowPopup(empty, owner);
    }

    public void HidePopup()
    {
      if (this.isInHidePopup)
        return;
      this.isInHidePopup = true;
      if (this.OnBackstageViewClosing())
      {
        this.isInHidePopup = false;
      }
      else
      {
        if (this.owner != null)
          this.owner.ExpandButton.Enabled = true;
        if (this.Parent != null)
        {
          this.Parent.SizeChanged -= new EventHandler(this.parentControl_SizeChanged);
          this.Visible = false;
          this.SendToBack();
          this.isShown = false;
        }
        this.OnBackstageViewClosed();
        this.isInHidePopup = false;
      }
    }

    protected virtual void OnBackstageViewClosed()
    {
      if (this.owner != null)
      {
        int num = (int) this.owner.ApplicationButtonElement.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) false);
      }
      EventHandler eventHandler = (EventHandler) this.Events[RadRibbonBarBackstageView.BackstageViewClosedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual bool OnBackstageViewClosing()
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadRibbonBarBackstageView.BackstageViewClosingEventKey];
      if (cancelEventHandler == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      cancelEventHandler((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnBackstageViewOpened()
    {
      if (this.owner != null)
      {
        int num = (int) this.owner.ApplicationButtonElement.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) true);
      }
      if (this.SelectedItem == null || !this.Items.Contains((RadItem) this.SelectedItem))
      {
        foreach (RadItem radItem in (RadItemCollection) this.Items)
        {
          BackstageTabItem backstageTabItem = radItem as BackstageTabItem;
          if (backstageTabItem != null)
          {
            this.SelectedItem = backstageTabItem;
            break;
          }
        }
      }
      EventHandler eventHandler = (EventHandler) this.Events[RadRibbonBarBackstageView.BackstageViewOpenedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual bool OnBackstageViewOpening()
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadRibbonBarBackstageView.BackstageViewOpeningEventKey];
      if (cancelEventHandler == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      cancelEventHandler((object) this, e);
      return e.Cancel;
    }

    protected internal bool OnKeyTipItemActivating(RadItem item, CancelEventArgs eventArgs)
    {
      if (this.KeyTipActivating != null)
        this.KeyTipActivating((object) item, eventArgs);
      return eventArgs.Cancel;
    }

    protected virtual bool OnKeyTipShowing(RadItem currentKeyMapItem, CancelEventArgs eventArgs)
    {
      if (this.KeyTipShowing != null)
        this.KeyTipShowing((object) currentKeyMapItem, eventArgs);
      return eventArgs.Cancel;
    }

    protected internal virtual void parentControl_SizeChanged(object sender, EventArgs e)
    {
      Size size = (sender as Control).ClientRectangle.Size;
      size.Width -= this.Location.X;
      size.Height -= this.Location.Y;
      this.Size = size;
      if (!this.AdjustLocation)
        return;
      Point empty = Point.Empty;
      empty.X = this.owner.ElementTree.Control.Location.X;
      empty.Y = this.owner.ElementTree.Control.Location.Y;
      if (!this.BackstageElement.IsFullScreenTheme())
        empty.Y += this.owner.ApplicationButtonElement.ActionButton.ControlBoundingRectangle.Bottom + this.owner.ApplicationButtonElement.Margin.Bottom;
      this.Location = empty;
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      return false;
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.isShown || this.Visible)
        return;
      this.HidePopup();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.Behavior.ProccessKeyMap(e.KeyData);
      this.BackstageElement.ProcessKeyboardSelection(e.KeyCode);
      if (e.KeyData != Keys.Escape)
        return;
      this.HidePopup();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (this.owner != null && this.owner.ElementTree.Control != null)
      {
        RadRibbonBar control = this.owner.ElementTree.Control as RadRibbonBar;
        if (control != null && !control.EnableKeyboardNavigation)
          return base.ProcessCmdKey(ref msg, keyData);
      }
      if (!((RadRibbonBarBackstageView.RibbonBackstageViewInputBehavior) this.Behavior).ProcessArrowKeys(keyData))
        return base.ProcessCmdKey(ref msg, keyData);
      return true;
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.Behavior.IsKeyMapActive)
        return;
      this.PaintKeyMap(e.Graphics);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      if (this.associatedForm != null)
        this.associatedForm.StyleChanged -= new EventHandler(this.OnAssociatedForm_StyleChanged);
      base.OnParentChanged(e);
      this.associatedForm = this.FindForm();
      if (this.associatedForm == null)
        return;
      this.UpdateControlButtonsStyle();
      this.associatedForm.StyleChanged += new EventHandler(this.OnAssociatedForm_StyleChanged);
    }

    private void OnAssociatedForm_StyleChanged(object sender, EventArgs e)
    {
      this.UpdateControlButtonsStyle();
    }

    protected internal virtual void PaintKeyMap(Graphics graphics)
    {
      List<RadItem> currentKeyMap = this.Behavior.GetCurrentKeyMap(this.Behavior.ActiveKeyMapItem);
      int num = 1;
      for (int index1 = 0; index1 < currentKeyMap.Count; ++index1)
      {
        RadItem currentKeyMapItem = currentKeyMap[index1];
        Rectangle boundingRectangle = currentKeyMapItem.ControlBoundingRectangle;
        if (currentKeyMapItem.ElementTree != null)
        {
          Point customLocation = Point.Empty;
          int y = boundingRectangle.Y + (int) ((double) boundingRectangle.Height * 0.66);
          customLocation = new Point(boundingRectangle.X + (int) ((double) boundingRectangle.Width / 2.0), y);
          RadKeyTipShowingEventArgs args = new RadKeyTipShowingEventArgs(false, customLocation, this.keyTipFont, Color.White, Color.Black, Color.Gray);
          if (!this.OnKeyTipShowing(currentKeyMapItem, (CancelEventArgs) args) && currentKeyMapItem.Visibility == ElementVisibility.Visible)
          {
            Control control = currentKeyMapItem.ElementTree.Control;
            Graphics graphics1 = (Graphics) null;
            if (control != this)
              graphics1 = control.CreateGraphics();
            string empty = string.Empty;
            string keyTip;
            if (!string.IsNullOrEmpty(currentKeyMapItem.KeyTip))
            {
              keyTip = currentKeyMapItem.KeyTip;
            }
            else
            {
              bool flag;
              do
              {
                RadItem radItem1 = currentKeyMap[index1];
                keyTip = num >= 10 ? ((char) (65 + num - 10)).ToString() : num.ToString();
                flag = false;
                for (int index2 = 0; index2 < currentKeyMap.Count; ++index2)
                {
                  RadItem radItem2 = currentKeyMap[index2];
                  if (keyTip == radItem2.KeyTip)
                  {
                    ++num;
                    flag = true;
                    break;
                  }
                }
              }
              while (flag);
              currentKeyMapItem.KeyTip = keyTip;
              ++num;
            }
            if (graphics1 != null)
            {
              this.PaintKeyTip(graphics1, args, keyTip);
              graphics1.Dispose();
            }
            else
              this.PaintKeyTip(graphics, args, keyTip);
          }
        }
      }
    }

    protected internal virtual void PaintKeyTip(
      Graphics graphics,
      RadKeyTipShowingEventArgs args,
      string keyTip)
    {
      int num = 0;
      Size size = TextRenderer.MeasureText(keyTip, args.Font);
      Point point = new Point(args.CustomLocation.X - (size.Width / 2 + num), args.CustomLocation.Y);
      Rectangle rect = new Rectangle(point.X, point.Y, size.Width + 2 * num, size.Height + 2 * num);
      graphics.FillRectangle((Brush) new SolidBrush(args.BackColor), rect);
      graphics.DrawRectangle(new Pen(args.BorderColor), rect);
      RadRibbonBar radRibbonBar = (RadRibbonBar) null;
      if (this.owner != null && this.owner.ElementTree.Control != null)
        radRibbonBar = this.owner.ElementTree.Control as RadRibbonBar;
      if (radRibbonBar != null && radRibbonBar.CompositionEnabled)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          SmoothingMode smoothingMode = graphics.SmoothingMode;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          float emSize = args.Font.SizeInPoints / 72f * graphics.DpiX;
          path.AddString(keyTip, args.Font.FontFamily, (int) args.Font.Style, emSize, new Point(point.X + num, point.Y + num), StringFormat.GenericDefault);
          graphics.FillPath((Brush) new SolidBrush(args.ForeColor), path);
          graphics.SmoothingMode = smoothingMode;
        }
      }
      else
        TextRenderer.DrawText((IDeviceContext) graphics, keyTip, args.Font, new Point(point.X + num, point.Y + num), args.ForeColor);
    }

    protected virtual void UpdateControlButtonsStyle()
    {
      RadRibbonForm form = this.FindForm() as RadRibbonForm;
      if (form != null && form.RibbonBar != null && form.RibbonBar.RibbonBarElement == this.owner)
      {
        this.backstageElement.TitleBarElement.SystemButtons.Visibility = form.ControlBox ? ElementVisibility.Visible : ElementVisibility.Hidden;
        this.backstageElement.TitleBarElement.IconPrimitive.Visibility = !form.ShowIcon || !form.ControlBox ? ElementVisibility.Collapsed : ElementVisibility.Visible;
        if (form.ControlBox)
        {
          this.backstageElement.TitleBarElement.MaximizeButton.Enabled = form.MaximizeBox;
          this.backstageElement.TitleBarElement.MaximizeButton.Visibility = form.MinimizeBox || form.MaximizeBox ? ElementVisibility.Visible : ElementVisibility.Hidden;
          this.backstageElement.TitleBarElement.MinimizeButton.Enabled = form.MinimizeBox;
          this.backstageElement.TitleBarElement.MinimizeButton.Visibility = form.MaximizeBox || form.MinimizeBox ? ElementVisibility.Visible : ElementVisibility.Hidden;
          this.backstageElement.TitleBarElement.IconPrimitive.Visibility = form.ShowIcon ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        }
      }
      if (this.owner == null)
        return;
      if (this.owner.RibbonCaption.CloseButton.Visibility != ElementVisibility.Visible)
        this.backstageElement.TitleBarElement.CloseButton.Visibility = this.owner.RibbonCaption.CloseButton.Visibility;
      if (this.owner.RibbonCaption.MaximizeButton.Visibility != ElementVisibility.Visible)
        this.backstageElement.TitleBarElement.MaximizeButton.Visibility = this.owner.RibbonCaption.MaximizeButton.Visibility;
      if (this.owner.RibbonCaption.MinimizeButton.Visibility == ElementVisibility.Visible)
        return;
      this.backstageElement.TitleBarElement.MinimizeButton.Visibility = this.owner.RibbonCaption.MinimizeButton.Visibility;
    }

    public class RibbonBackstageViewInputBehavior : ComponentInputBehavior
    {
      private RadRibbonBarBackstageView owner;

      public RibbonBackstageViewInputBehavior(RadRibbonBarBackstageView owner)
        : base((IComponentTreeHandler) owner)
      {
        this.owner = owner;
        this.EnableKeyTips = true;
        this.ShowScreenTipsBellowControl = true;
      }

      protected override bool SetInternalKeyMapFocus()
      {
        if (this.owner.BackstageElement.SelectedItem != null)
          this.owner.BackstageElement.SelectedItem.Focus();
        else if (this.owner.BackstageElement.ItemsPanelElement.Items.Count > 0)
          this.owner.BackstageElement.ItemsPanelElement.Items[0].Focus();
        else
          this.owner.BackstageElement.ItemsPanelElement.Focus();
        if (!this.owner.IsDisposed)
          this.owner.Capture = true;
        return true;
      }

      protected override List<RadItem> GetRootItems()
      {
        List<RadItem> radItemList = new List<RadItem>();
        foreach (RadItem radItem in (RadItemCollection) this.owner.BackstageElement.Items)
        {
          if (radItem.Enabled)
            radItemList.Add(radItem);
        }
        return radItemList;
      }

      protected override bool ActivateSelectedItem(RadItem currentKeyMapItem)
      {
        if (currentKeyMapItem == null || currentKeyMapItem.Visibility != ElementVisibility.Visible || !this.EnableKeyMap)
          return false;
        this.owner.AccessibilityNotifyClients(AccessibleEvents.Focus, -1);
        CancelEventArgs eventArgs = new CancelEventArgs(false);
        if (this.owner.OnKeyTipItemActivating(currentKeyMapItem, eventArgs))
          return false;
        this.ActiveKeyMapItem.PerformClick();
        return false;
      }

      protected override List<RadItem> GetKeyFocusChildren(RadItem currentKeyMapItem)
      {
        if (currentKeyMapItem == null)
          return this.GetRootItems();
        List<RadItem> radItemList = new List<RadItem>();
        if (currentKeyMapItem is BackstageTabItem || currentKeyMapItem is BackstageButtonItem)
          return radItemList;
        foreach (RadElement child in currentKeyMapItem.Children)
        {
          if (child is RadItem && child.Enabled)
            radItemList.Add(child as RadItem);
        }
        return radItemList;
      }

      internal bool ProcessArrowKeys(Keys keyData)
      {
        if (this.owner.owner != null && this.owner.owner.ElementTree.Control != null)
        {
          RadRibbonBar control = this.owner.owner.ElementTree.Control as RadRibbonBar;
          if (this.IsKeyMapActive && control != null && control.EnableKeyboardNavigation && (keyData == Keys.Right || keyData == Keys.Left || (keyData == Keys.Up || keyData == Keys.Down)))
          {
            this.ResetKeyMapInternal();
            return true;
          }
        }
        return false;
      }
    }
  }
}
