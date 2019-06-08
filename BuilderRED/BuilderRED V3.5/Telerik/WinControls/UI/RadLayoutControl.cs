// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLayoutControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  [Docking(DockingBehavior.Ask)]
  [Description("Keeps its child controls arranged in a consistent way and scales their layout as the control size changes.")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Containers")]
  [Designer("Telerik.WinControls.UI.Design.RadLayoutControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadLayoutControl : RadNCEnabledControl, IMessageFilter
  {
    private bool allowCustomize = true;
    private bool allowHiddenItems = true;
    private bool drawBorder = true;
    private LayoutControlContainerElement containerElement;
    private LayoutControlDraggableOverlay dragOverlay;
    private RadVScrollBar verticalScrollbar;
    private RadHScrollBar horizontalScrollbar;
    private RadDropDownMenu dropDownMenu;
    private RadDropDownMenu initialDropDownMenu;
    private RadItemCollection hiddenItems;
    private bool allowResize;
    private RadLayoutControlCustomizeDialog customizeDialog;
    private ComponentXmlSerializationInfo xmlSerializationInfo;
    private LayoutControlResizingBehavior capturedBehavior;
    private RadMenuItem hideMenuItem;
    internal bool ShowItemBorders;

    public event EventHandler StructureChanged;

    protected internal virtual void OnStructureChanged(object sender)
    {
      this.UpdateScrollbars();
      if (this.StructureChanged == null)
        return;
      this.StructureChanged(sender, EventArgs.Empty);
    }

    internal event EventHandler HandleDropCompleted;

    protected internal virtual void OnHandleDropCompleted(object sender)
    {
      if (this.HandleDropCompleted == null)
        return;
      this.HandleDropCompleted(sender, EventArgs.Empty);
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 33554432;
        return createParams;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.containerElement = new LayoutControlContainerElement();
      this.containerElement.Class = "RootLayoutControlContainer";
      parent.Children.Add((RadElement) this.containerElement);
      this.verticalScrollbar = new RadVScrollBar();
      this.verticalScrollbar.Focusable = false;
      this.verticalScrollbar.Dock = DockStyle.Right;
      this.verticalScrollbar.ValueChanged += new EventHandler(this.verticalScrollbar_ValueChanged);
      this.verticalScrollbar.Visible = false;
      this.horizontalScrollbar = new RadHScrollBar();
      this.horizontalScrollbar.Focusable = false;
      this.horizontalScrollbar.Dock = DockStyle.Bottom;
      this.horizontalScrollbar.ValueChanged += new EventHandler(this.horizontalScrollbar_ValueChanged);
      this.horizontalScrollbar.Visible = false;
      this.dropDownMenu = new RadDropDownMenu();
      this.initialDropDownMenu = this.dropDownMenu;
      this.InitializeDropDownMenu();
      ((RadLayoutControlControlCollection) this.Controls).AddInternal((Control) this.verticalScrollbar);
      ((RadLayoutControlControlCollection) this.Controls).AddInternal((Control) this.horizontalScrollbar);
      this.hiddenItems = new RadItemCollection();
      this.hiddenItems.ItemTypes = new System.Type[1]
      {
        typeof (LayoutControlItemBase)
      };
      this.hiddenItems.ItemsChanged += new ItemChangedDelegate(this.hiddenItems_ItemsChanged);
    }

    private void hiddenItems_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation != ItemsChangeOperation.Inserted)
        return;
      ((LayoutControlItemBase) target).IsHidden = true;
    }

    protected virtual void InitializeDropDownMenu()
    {
      if (this.hideMenuItem == null)
        this.hideMenuItem = new RadMenuItem(LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText"));
      this.hideMenuItem.Image = LayoutControlIcons.Customize;
      this.hideMenuItem.Click += new EventHandler(this.customizeItem_Click);
      this.dropDownMenu.Items.Add((RadItem) this.hideMenuItem);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.ContainerElement.RebuildLayoutTree(false);
      this.UpdateControlsLayout();
      this.UpdateScrollbars();
    }

    protected override void Dispose(bool disposing)
    {
      foreach (DisposableObject hiddenItem in this.HiddenItems)
        hiddenItem.Dispose();
      if (this.customizeDialog != null)
        this.customizeDialog.Dispose();
      this.hideMenuItem.Dispose();
      this.dropDownMenu.Dispose();
      base.Dispose(disposing);
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the control should draw its border.")]
    [Browsable(true)]
    public bool DrawBorder
    {
      get
      {
        return this.drawBorder;
      }
      set
      {
        if (this.drawBorder == value)
          return;
        this.drawBorder = value;
        this.InvalidateNCArea();
        this.OnNotifyPropertyChanged(nameof (DrawBorder));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadLayoutControlCustomizeDialog CustomizeDialog
    {
      get
      {
        if (this.customizeDialog == null)
          this.customizeDialog = new RadLayoutControlCustomizeDialog(this);
        return this.customizeDialog;
      }
      set
      {
        this.customizeDialog = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether resizing is enabled when the Customize Dialog is not shown.")]
    public bool AllowResize
    {
      get
      {
        return this.allowResize;
      }
      set
      {
        if (this.allowResize == value)
          return;
        this.allowResize = value;
        this.OnNotifyPropertyChanged(nameof (AllowResize));
      }
    }

    [Description("Gets or sets a value indicating whether the end-user is allowed to hide and show existing items.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowHiddenItems
    {
      get
      {
        return this.allowHiddenItems;
      }
      set
      {
        if (this.allowHiddenItems == value)
          return;
        this.allowHiddenItems = value;
        this.OnNotifyPropertyChanged(nameof (AllowHiddenItems));
      }
    }

    [Description("Gets or sets a value indicating whether the end-user is allowed show the Customize Dialog and modify the existing layout.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowCustomize
    {
      get
      {
        return this.allowCustomize;
      }
      set
      {
        if (this.allowCustomize == value)
          return;
        this.allowCustomize = value;
        this.OnNotifyPropertyChanged(nameof (AllowCustomize));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDropDownMenu DropDownMenu
    {
      get
      {
        return this.dropDownMenu;
      }
      set
      {
        if (this.dropDownMenu == value)
          return;
        if (this.initialDropDownMenu != null)
        {
          this.initialDropDownMenu.Dispose();
          this.initialDropDownMenu = (RadDropDownMenu) null;
        }
        this.dropDownMenu = value;
        this.OnNotifyPropertyChanged(nameof (DropDownMenu));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadVScrollBar VerticalScrollbar
    {
      get
      {
        return this.verticalScrollbar;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadHScrollBar HorizontalScrollbar
    {
      get
      {
        return this.horizontalScrollbar;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public LayoutControlContainerElement ContainerElement
    {
      get
      {
        return this.containerElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.containerElement.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public RadItemCollection HiddenItems
    {
      get
      {
        return this.hiddenItems;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public LayoutControlDraggableOverlay DragOverlay
    {
      get
      {
        return this.dragOverlay;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DragOverlayVisible
    {
      get
      {
        if (this.dragOverlay != null)
          return this.dragOverlay.Visible;
        return false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Cursor Cursor
    {
      get
      {
        return base.Cursor;
      }
      set
      {
        base.Cursor = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(200, 100);
      }
    }

    public bool IsResizing
    {
      get
      {
        if (this.capturedBehavior != null)
          return this.capturedBehavior.IsResizing;
        return false;
      }
    }

    public LayoutControlResizingBehavior CurrentResizingBehavior
    {
      get
      {
        return this.capturedBehavior;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    public void BeginUpdate()
    {
      this.BeginInit();
    }

    public void EndUpdate()
    {
      this.EndInit();
    }

    public override void BeginInit()
    {
      base.BeginInit();
      this.RootElement.SuspendLayout(true);
    }

    public override void EndInit()
    {
      base.EndInit();
      this.RootElement.ResumeLayout(true, false);
      if (!this.IsLoaded)
        return;
      this.containerElement.RebuildLayoutTree();
      this.UpdateScrollbars();
    }

    public void AddItem(Control control, Control anchor, LayoutControlDropPosition position)
    {
      LayoutControlItem itemByControl = this.FindItemByControl(anchor);
      this.AddItem(control, (LayoutControlItemBase) itemByControl, position);
    }

    public void AddItem(
      Control control,
      LayoutControlItemBase anchor,
      LayoutControlDropPosition position)
    {
      if (!this.Controls.Contains(control))
        ((RadLayoutControlControlCollection) this.Controls).AddInternal(control);
      LayoutControlItem layoutControlItem = this.FindItemByControl(control);
      if (layoutControlItem == null)
        layoutControlItem = new LayoutControlItem()
        {
          AssociatedControl = control
        };
      this.AddItem((LayoutControlItemBase) layoutControlItem, anchor, position);
    }

    public void AddItem(Control control, LayoutControlContainerElement container)
    {
      if (!this.Controls.Contains(control))
        ((RadLayoutControlControlCollection) this.Controls).AddInternal(control);
      LayoutControlItem layoutControlItem = this.FindItemByControl(control);
      if (layoutControlItem == null)
        layoutControlItem = new LayoutControlItem()
        {
          AssociatedControl = control
        };
      this.AddItem((LayoutControlItemBase) layoutControlItem, container);
    }

    public void AddItem(
      LayoutControlItemBase item,
      Control anchor,
      LayoutControlDropPosition position)
    {
      LayoutControlItem itemByControl = this.FindItemByControl(anchor);
      this.AddItem(item, (LayoutControlItemBase) itemByControl, position);
    }

    public void AddItem(
      LayoutControlItemBase item,
      LayoutControlItemBase anchor,
      LayoutControlDropPosition position)
    {
      if (anchor == null)
      {
        this.AddItem(item, this.ContainerElement);
      }
      else
      {
        Point mousePosition = new Point((anchor.Bounds.Left + anchor.Bounds.Right) / 2, (anchor.Bounds.Top + anchor.Bounds.Bottom) / 2);
        switch (position)
        {
          case LayoutControlDropPosition.Left:
            mousePosition.X -= (int) Math.Ceiling((double) anchor.Bounds.Width / 10.0);
            break;
          case LayoutControlDropPosition.Right:
            mousePosition.X += (int) Math.Ceiling((double) anchor.Bounds.Width / 10.0);
            break;
          case LayoutControlDropPosition.Top:
            mousePosition.Y -= (int) Math.Ceiling((double) anchor.Bounds.Height / 4.0);
            break;
          case LayoutControlDropPosition.Bottom:
            mousePosition.Y += (int) Math.Ceiling((double) anchor.Bounds.Height / 4.0);
            break;
        }
        mousePosition.Offset(anchor.Parent.ControlBoundingRectangle.Location);
        anchor.FindAncestor<LayoutControlContainerElement>()?.LayoutTree.HandleDrop(new DraggableLayoutControlItem(anchor), item, mousePosition);
        this.containerElement.InvalidateMeasure(true);
        this.containerElement.UpdateLayout();
        if (!(item is LayoutControlTabbedGroup))
          return;
        this.ContainerElement.RebuildLayoutTree();
      }
    }

    public void AddItem(LayoutControlItemBase item)
    {
      this.AddItem(item, this.ContainerElement);
    }

    public void AddItem(LayoutControlItemBase item, LayoutControlContainerElement container)
    {
      if (item == null)
        return;
      container.Items.Add((RadItem) item);
      container.RebuildLayoutTree();
      this.containerElement.InvalidateMeasure(true);
      this.containerElement.UpdateLayout();
    }

    public void RemoveItem(Control control)
    {
      this.Controls.Remove(control);
    }

    public void RemoveItem(LayoutControlItemBase item)
    {
      ILayoutControlItemsHost parentItemsContainer = item.GetParentItemsContainer();
      if (parentItemsContainer == null)
        return;
      parentItemsContainer.Items.Remove((RadItem) item);
      foreach (LayoutControlItem descendant in item.GetDescendants((Predicate<RadElement>) (element => element is LayoutControlItem), TreeTraversalMode.BreadthFirst))
        ((RadLayoutControlControlCollection) this.Controls).RemoveInternal(descendant.AssociatedControl);
    }

    public void ResizeItem(LayoutControlItemBase item, int diff)
    {
      if (item == null)
        return;
      LayoutControlContainerElement ancestor = item.FindAncestor<LayoutControlContainerElement>();
      if (ancestor == null)
        return;
      LayoutTreeNode nodeByItem = ancestor.LayoutTree.FindNodeByItem(item);
      if (nodeByItem.Parent != null)
      {
        if (nodeByItem.Parent.Right.Item == item)
          diff *= -1;
        ancestor.LayoutTree.MoveSplitter((float) diff, nodeByItem.Parent);
        ancestor.LayoutTree.ResetOriginalBounds(nodeByItem.Parent);
      }
      this.UpdateControlsLayout();
    }

    public void ResizeItem(Control control, int diff)
    {
      this.ResizeItem((LayoutControlItemBase) this.FindItemByControl(control, false), diff);
    }

    public void HideItem(LayoutControlItemBase item)
    {
      if (item == null || item.ElementTree == null || item.ElementTree.Control != this)
        return;
      LayoutControlContainerElement containerElement = (LayoutControlContainerElement) null;
      LayoutControlTabbedGroup controlTabbedGroup = (LayoutControlTabbedGroup) null;
      for (RadElement parent = item.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is LayoutControlContainerElement)
        {
          containerElement = (LayoutControlContainerElement) parent;
          break;
        }
        if (parent is LayoutControlTabbedGroup)
        {
          controlTabbedGroup = (LayoutControlTabbedGroup) parent;
          break;
        }
      }
      if (containerElement == null && controlTabbedGroup == null)
        return;
      if (containerElement != null)
        containerElement.Items.Remove((RadItem) item);
      else
        controlTabbedGroup?.ItemGroups.Remove((RadItem) item);
      this.HiddenItems.Add((RadItem) item);
      LayoutControlItem layoutControlItem = item as LayoutControlItem;
      if (layoutControlItem != null && layoutControlItem.AssociatedControl != null)
        layoutControlItem.AssociatedControl.Visible = false;
      ControlTraceMonitor.TrackAtomicFeature(this.Parent is RadDataLayout ? (RadControl) this.Parent : (RadControl) this, "LayoutModified", (object) "ItemHidden");
      this.OnHandleDropCompleted((object) item);
    }

    public void HideItem(Control control)
    {
      this.HideItem((LayoutControlItemBase) this.FindItemByControl(control, false));
    }

    public void ShowDragOverlay()
    {
      this.dragOverlay = this.dragOverlay ?? new LayoutControlDraggableOverlay(this);
      this.dragOverlay.ThemeName = this.ThemeName;
      this.dragOverlay.UpdatePreview();
      this.dragOverlay.Dock = DockStyle.Fill;
      ((RadLayoutControlControlCollection) this.Controls).AddInternal((Control) this.dragOverlay);
      this.dragOverlay.Visible = true;
      this.dragOverlay.BringToFront();
      this.PerformLayout();
      this.Refresh();
    }

    public void HideDragOverlay()
    {
      if (this.dragOverlay == null)
        return;
      this.dragOverlay.Visible = false;
      this.dragOverlay.SetPreviewRectangle(Rectangle.Empty);
      this.Controls.Remove((Control) this.dragOverlay);
    }

    public void ShowCustomizeDialog()
    {
      if (this.CustomizeDialog.Visible)
        return;
      this.ShowDragOverlay();
      this.CustomizeDialog.Owner = this.FindForm();
      ThemeResolutionService.ApplyThemeToControlTree((Control) this.CustomizeDialog, this.ThemeName);
      if (this.CustomizeDialog.Location == Point.Empty)
        this.CustomizeDialog.Location = this.GetInitialCustomizeDialogLocation();
      this.CustomizeDialog.Show();
      this.CustomizeDialog.RightToLeft = this.RightToLeft;
      ControlTraceMonitor.TrackAtomicFeature(this.Parent is RadDataLayout ? (RadControl) this.Parent : (RadControl) this, "CustomizeDialogOpened");
    }

    protected virtual Point GetInitialCustomizeDialogLocation()
    {
      Point screen1 = this.PointToScreen(new Point(this.Width + 80, (this.Height - this.CustomizeDialog.Height) / 2));
      Rectangle rectangle = new Rectangle(screen1, this.CustomizeDialog.Size);
      Screen screen2 = Screen.FromControl(this.ElementTree.Control);
      if (screen2.Bounds.Right < rectangle.Right)
        screen1.X -= rectangle.Right - screen2.Bounds.Right;
      if (screen2.Bounds.Bottom < rectangle.Bottom)
        screen1.Y -= rectangle.Bottom - screen2.Bounds.Bottom;
      if (screen2.Bounds.Top > rectangle.Top)
        screen1.Y -= rectangle.Top - screen2.Bounds.Top;
      return screen1;
    }

    public void CloseCustomizeDialog()
    {
      if (this.CustomizeDialog == null)
        return;
      this.CustomizeDialog.Close();
    }

    public LayoutControlItem FindItemByControl(Control control)
    {
      return this.FindItemByControl(control, true);
    }

    public LayoutControlItem FindItemByControl(Control control, bool includeHidden)
    {
      LayoutControlItem layoutItem = ((RadLayoutControlControlCollection) this.Controls).FindLayoutItem(control);
      if (layoutItem != null)
        return layoutItem;
      if (!includeHidden)
        return (LayoutControlItem) null;
      foreach (LayoutControlItemBase hiddenItem in this.HiddenItems)
      {
        if (hiddenItem is LayoutControlItem && ((LayoutControlItem) hiddenItem).AssociatedControl == control)
          return (LayoutControlItem) hiddenItem;
      }
      return (LayoutControlItem) null;
    }

    public IEnumerable<LayoutControlItemBase> GetAllItems()
    {
      return this.GetAllItems(true);
    }

    public IEnumerable<LayoutControlItemBase> GetAllItems(
      bool includeHidden)
    {
      foreach (LayoutControlItemBase descendant in this.ContainerElement.GetDescendants((Predicate<RadElement>) (x => x is LayoutControlItemBase), TreeTraversalMode.BreadthFirst))
        yield return descendant;
      if (includeHidden)
      {
        foreach (LayoutControlItemBase hiddenItem in this.HiddenItems)
          yield return hiddenItem;
      }
    }

    public virtual void UpdateScrollbars()
    {
      this.verticalScrollbar.LargeChange = Math.Max(0, this.Height - (this.horizontalScrollbar.Visible ? this.horizontalScrollbar.Height : 0));
      this.verticalScrollbar.Maximum = this.containerElement.Bounds.Height;
      this.verticalScrollbar.SmallChange = 20;
      this.horizontalScrollbar.LargeChange = Math.Max(0, this.Width - (this.verticalScrollbar.Visible ? this.verticalScrollbar.Width : 0));
      this.horizontalScrollbar.Maximum = this.containerElement.Bounds.Width;
      this.horizontalScrollbar.SmallChange = 20;
      this.verticalScrollbar.Visible = this.containerElement.LayoutTree.Root != null && (double) this.containerElement.LayoutTree.Root.MinSize.Height > (double) this.verticalScrollbar.LargeChange;
      this.horizontalScrollbar.Visible = this.containerElement.LayoutTree.Root != null && (double) this.containerElement.LayoutTree.Root.MinSize.Width > (double) this.horizontalScrollbar.LargeChange;
      this.verticalScrollbar.Value = this.verticalScrollbar.Visible ? Math.Min(this.verticalScrollbar.Value, this.verticalScrollbar.Maximum - this.verticalScrollbar.LargeChange + 1) : 0;
      this.horizontalScrollbar.Value = this.horizontalScrollbar.Visible ? Math.Min(this.horizontalScrollbar.Value, this.horizontalScrollbar.Maximum - this.horizontalScrollbar.LargeChange + 1) : 0;
      if (!this.verticalScrollbar.Visible && !this.horizontalScrollbar.Visible)
        return;
      this.verticalScrollbar.BringToFront();
      this.horizontalScrollbar.BringToFront();
    }

    public void UpdateControlsLayout()
    {
      this.RootElement.InvalidateMeasure(true);
      this.RootElement.UpdateLayout();
      this.Refresh();
    }

    public Cursor GetCursorAtPoint(Point point)
    {
      foreach (LayoutControlContainerElement containerElement in this.GetContainerElements((RadElement) this.containerElement))
      {
        if (containerElement.ControlBoundingRectangle.Contains(point))
        {
          Cursor cursorAtPoint = containerElement.ResizingBehavior.GetCursorAtPoint(point);
          if (cursorAtPoint != Cursors.Default)
            return cursorAtPoint;
        }
      }
      return Cursors.Default;
    }

    public LayoutControlResizingBehavior GetBehaviorAtPoint(Point point)
    {
      foreach (LayoutControlContainerElement containerElement in this.GetContainerElements((RadElement) this.containerElement))
      {
        if (containerElement.ControlBoundingRectangle.Contains(point) && containerElement.ResizingBehavior.GetCursorAtPoint(point) != Cursors.Default)
          return containerElement.ResizingBehavior;
      }
      return (LayoutControlResizingBehavior) null;
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new RadLayoutControlControlCollection(this);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      this.UpdateScrollbars();
      if (this.dragOverlay != null && this.dragOverlay.Visible)
        this.dragOverlay.UpdatePreview(false);
      this.InvalidateNCArea();
      base.OnSizeChanged(e);
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      if (!this.IsLoaded || this.IsInitializing)
        return;
      this.containerElement.PerformControlLayout();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Escape || this.customizeDialog == null || !this.customizeDialog.Visible)
        return base.ProcessCmdKey(ref msg, keyData);
      this.CloseCustomizeDialog();
      return true;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      Cursor cursor = Cursors.Default;
      if (this.capturedBehavior == null && (this.AllowResize || this.Site != null))
        cursor = this.GetCursorAtPoint(e.Location);
      else if (this.capturedBehavior != null && this.capturedBehavior.IsResizing)
      {
        this.capturedBehavior.Resize(e.Location);
        cursor = (Cursor) null;
      }
      else if (this.capturedBehavior == null)
      {
        LayoutControlSplitterItem elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as LayoutControlSplitterItem;
        if (elementAtPoint != null)
          cursor = elementAtPoint.Orientation == Orientation.Vertical ? Cursors.SizeWE : Cursors.SizeNS;
      }
      if (!(cursor != (Cursor) null) || !(cursor != this.Cursor))
        return;
      this.Cursor = cursor;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        if (this.AllowCustomize && this.Site == null)
        {
          this.dropDownMenu.ThemeName = this.ThemeName;
          this.hideMenuItem.Text = LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText");
          this.dropDownMenu.Show((Control) this, e.Location);
        }
        else
          base.OnMouseDown(e);
      }
      else
      {
        base.OnMouseDown(e);
        if (this.AllowResize || this.ElementTree.Control.Site != null)
        {
          Cursor cursorAtPoint = this.GetCursorAtPoint(e.Location);
          LayoutControlResizingBehavior behaviorAtPoint = this.GetBehaviorAtPoint(e.Location);
          if (cursorAtPoint != Cursors.Default && e.Button == MouseButtons.Left && behaviorAtPoint != null)
          {
            Orientation resizeType = cursorAtPoint == Cursors.SizeNS ? Orientation.Vertical : Orientation.Horizontal;
            if (behaviorAtPoint.BeginResize(e.Location, resizeType))
            {
              this.capturedBehavior = behaviorAtPoint;
              this.Capture = true;
            }
          }
        }
        if (this.capturedBehavior != null)
          return;
        LayoutControlSplitterItem elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as LayoutControlSplitterItem;
        if (elementAtPoint == null)
          return;
        LayoutControlContainerElement ancestor = elementAtPoint.FindAncestor<LayoutControlContainerElement>();
        if (!ancestor.ResizingBehavior.BeginResize(elementAtPoint))
          return;
        this.capturedBehavior = ancestor.ResizingBehavior;
        this.Capture = true;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right && this.AllowCustomize && this.Site == null)
        return;
      base.OnMouseUp(e);
      if (this.capturedBehavior == null || !this.capturedBehavior.IsResizing)
        return;
      this.Capture = false;
      this.capturedBehavior.EndResize();
      this.capturedBehavior = (LayoutControlResizingBehavior) null;
      this.ElementTree.Control.Cursor = this.GetCursorAtPoint(e.Location);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
      if (handledMouseEventArgs == null || handledMouseEventArgs.Handled || !this.verticalScrollbar.Visible)
        return;
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = this.verticalScrollbar.Value - Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines * this.verticalScrollbar.SmallChange;
      if (num2 > this.verticalScrollbar.Maximum - this.verticalScrollbar.LargeChange + 1)
        num2 = this.verticalScrollbar.Maximum - this.verticalScrollbar.LargeChange + 1;
      if (num2 < this.verticalScrollbar.Minimum)
        num2 = this.verticalScrollbar.Minimum;
      else if (num2 > this.verticalScrollbar.Maximum - this.verticalScrollbar.LargeChange + 1)
        num2 = this.verticalScrollbar.Maximum - this.verticalScrollbar.LargeChange + 1;
      if (this.verticalScrollbar.Value == num2)
        return;
      this.verticalScrollbar.Value = num2;
      handledMouseEventArgs.Handled = true;
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.verticalScrollbar.ThemeName = this.ThemeName;
      this.horizontalScrollbar.ThemeName = this.ThemeName;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ContainerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ContainerElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ContainerElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
      int num = (int) this.ContainerElement.UpdateValue(VisualElement.ForeColorProperty);
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ContainerElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.ContainerElement.ElementTree.ApplyThemeToElementTree();
    }

    private IEnumerable<LayoutControlContainerElement> GetContainerElements()
    {
      return this.GetContainerElements((RadElement) this.containerElement);
    }

    private IEnumerable<LayoutControlContainerElement> GetContainerElements(
      RadElement root)
    {
      if (root is LayoutControlContainerElement)
        yield return (LayoutControlContainerElement) root;
      foreach (RadElement child in root.Children)
      {
        foreach (LayoutControlContainerElement containerElement in this.GetContainerElements(child))
          yield return containerElement;
      }
    }

    private IEnumerable<LayoutControlItemBase> GetItems()
    {
      foreach (LayoutControlContainerElement containerElement in this.GetContainerElements())
      {
        foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) containerElement.Items)
          yield return layoutControlItemBase;
      }
    }

    private bool IsItemVisible(LayoutControlItemBase item)
    {
      RadElement radElement = (RadElement) item;
      while (radElement.Visibility == ElementVisibility.Visible)
      {
        radElement = radElement.Parent;
        if (radElement == null)
          return true;
      }
      return false;
    }

    private void customizeItem_Click(object sender, EventArgs e)
    {
      if (!this.AllowCustomize)
        return;
      this.ShowCustomizeDialog();
    }

    private void horizontalScrollbar_ValueChanged(object sender, EventArgs e)
    {
      this.containerElement.PositionOffset = new SizeF((float) -this.horizontalScrollbar.Value, this.containerElement.PositionOffset.Height);
      this.containerElement.PerformControlLayout();
      if (this.dragOverlay != null && this.dragOverlay.Visible)
        this.dragOverlay.UpdatePreview();
      this.UpdateParentForm();
      this.Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
      base.OnPaint(e);
    }

    private void verticalScrollbar_ValueChanged(object sender, EventArgs e)
    {
      this.ElementTree.Control.SuspendLayout();
      this.containerElement.PositionOffset = new SizeF(this.containerElement.PositionOffset.Width, (float) -this.verticalScrollbar.Value);
      this.containerElement.PerformControlLayout();
      if (this.dragOverlay != null && this.dragOverlay.Visible)
        this.dragOverlay.UpdatePreview();
      this.UpdateParentForm();
      this.ElementTree.Control.ResumeLayout();
    }

    private void UpdateParentForm()
    {
      if (this.ElementTree.Control.TopLevelControl == null)
        return;
      RadFormControlBase topLevelControl = this.ElementTree.Control.TopLevelControl as RadFormControlBase;
      if (topLevelControl == null)
        return;
      if (topLevelControl.FormBehavior != null && topLevelControl.FormBehavior.FormElement != null)
        topLevelControl.FormBehavior.FormElement.Invalidate();
      this.ElementTree.Control.TopLevelControl.Invalidate();
    }

    private IList<LayoutControlItemBase> GetAllChildItems(
      RadItemCollection items)
    {
      List<LayoutControlItemBase> layoutControlItemBaseList = new List<LayoutControlItemBase>();
      Queue<LayoutControlItemBase> layoutControlItemBaseQueue = new Queue<LayoutControlItemBase>();
      foreach (LayoutControlItemBase layoutControlItemBase in items)
        layoutControlItemBaseQueue.Enqueue(layoutControlItemBase);
      while (layoutControlItemBaseQueue.Count > 0)
      {
        LayoutControlItemBase layoutControlItemBase1 = layoutControlItemBaseQueue.Dequeue();
        layoutControlItemBaseList.Add(layoutControlItemBase1);
        LayoutControlGroupItem controlGroupItem = layoutControlItemBase1 as LayoutControlGroupItem;
        LayoutControlTabbedGroup controlTabbedGroup = layoutControlItemBase1 as LayoutControlTabbedGroup;
        if (controlGroupItem != null)
        {
          foreach (LayoutControlItemBase layoutControlItemBase2 in controlGroupItem.Items)
            layoutControlItemBaseQueue.Enqueue(layoutControlItemBase2);
        }
        else if (controlTabbedGroup != null)
        {
          foreach (LayoutControlItemBase itemGroup in (RadItemCollection) controlTabbedGroup.ItemGroups)
            layoutControlItemBaseQueue.Enqueue(itemGroup);
        }
      }
      return (IList<LayoutControlItemBase>) layoutControlItemBaseList;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      Application.AddMessageFilter((IMessageFilter) this);
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
      base.OnHandleDestroyed(e);
      Application.RemoveMessageFilter((IMessageFilter) this);
    }

    public bool PreFilterMessage(ref Message m)
    {
      if (m.Msg == 512 || m.Msg == 534)
      {
        Point client = this.PointToClient(Control.MousePosition);
        this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, client.X, client.Y, 0));
      }
      return false;
    }

    public virtual void SaveLayout(XmlWriter xmlWriter)
    {
      LayoutControlXmlSerializer controlXmlSerializer = new LayoutControlXmlSerializer(this, this.XmlSerializationInfo);
      xmlWriter.WriteStartElement(nameof (RadLayoutControl));
      controlXmlSerializer.WriteObjectElement(xmlWriter, (object) this);
      xmlWriter.WriteEndElement();
    }

    public virtual void SaveLayout(Stream stream)
    {
      LayoutControlXmlSerializer controlXmlSerializer = new LayoutControlXmlSerializer(this, this.XmlSerializationInfo);
      StreamWriter streamWriter = new StreamWriter(stream);
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) streamWriter);
      xmlTextWriter.WriteStartElement(nameof (RadLayoutControl));
      controlXmlSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      xmlTextWriter.WriteEndElement();
      streamWriter.Flush();
    }

    public virtual void SaveLayout(string fileName)
    {
      LayoutControlXmlSerializer controlXmlSerializer = new LayoutControlXmlSerializer(this, this.XmlSerializationInfo);
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement(nameof (RadLayoutControl));
        controlXmlSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      }
    }

    public virtual void LoadLayout(string fileName)
    {
      if (!File.Exists(fileName))
      {
        int num = (int) RadMessageBox.Show(LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ErrorFileNotFoundText"), LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ErrorBoxText"), MessageBoxButtons.OK, RadMessageIcon.Error);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(fileName))
        {
          using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) streamReader))
            this.LoadLayout((XmlReader) xmlTextReader);
        }
      }
    }

    public virtual void LoadLayout(Stream stream)
    {
      if (stream == null || stream.Length <= 0L)
        return;
      if (stream.Position == stream.Length)
        stream.Position = 0L;
      this.LoadLayout((XmlReader) new XmlTextReader((TextReader) new StreamReader(stream)));
    }

    public virtual void LoadLayout(XmlReader xmlReader)
    {
      try
      {
        this.Items.Clear();
        this.HiddenItems.Clear();
        this.SuspendLayout();
        this.RootElement.SuspendLayout(true);
        LayoutControlXmlSerializer controlXmlSerializer = new LayoutControlXmlSerializer(this, this.XmlSerializationInfo);
        xmlReader.Read();
        controlXmlSerializer.ReadObjectElement(xmlReader, (object) this);
        this.EnsureItemsControlsVisibility();
        this.ContainerElement.RebuildLayoutTree();
        this.RootElement.ResumeLayout(true, false);
        this.ResumeLayout();
        this.RootElement.InvalidateMeasure(true);
        this.RootElement.UpdateLayout();
        this.ContainerElement.PerformControlLayout();
        this.UpdateScrollbars();
        if (!this.DragOverlayVisible)
          return;
        this.DragOverlay.UpdatePreview();
      }
      catch (Exception ex)
      {
        int num = (int) RadMessageBox.Show(LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ErrorLoadingLayoutText"), LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ErrorBoxText"), MessageBoxButtons.OK, RadMessageIcon.Error);
      }
    }

    protected void EnsureItemsControlsVisibility()
    {
      foreach (LayoutControlItemBase layoutControlItemBase in this.GetItems())
      {
        if (layoutControlItemBase is LayoutControlItem && ((LayoutControlItem) layoutControlItemBase).AssociatedControl != null)
          ((LayoutControlItem) layoutControlItemBase).AssociatedControl.Visible = this.IsItemVisible(layoutControlItemBase);
      }
      foreach (LayoutControlItemBase allChildItem in (IEnumerable<LayoutControlItemBase>) this.GetAllChildItems(this.HiddenItems))
      {
        if (allChildItem is LayoutControlItem && ((LayoutControlItem) allChildItem).AssociatedControl != null)
          ((LayoutControlItem) allChildItem).AssociatedControl.Visible = false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ComponentXmlSerializationInfo XmlSerializationInfo
    {
      get
      {
        if (this.xmlSerializationInfo == null)
          this.xmlSerializationInfo = this.GetDefaultXmlSerializationInfo();
        return this.xmlSerializationInfo;
      }
      set
      {
        this.xmlSerializationInfo = value;
      }
    }

    protected virtual ComponentXmlSerializationInfo GetDefaultXmlSerializationInfo()
    {
      return new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection() { { typeof (RadLayoutControl), "Name", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLayoutControl), "Visible", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLayoutControl), "AllowResize", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "ThemeName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Controls", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "DataBindings", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadComponentElement), "DataBindings", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Style", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (LayoutControlItem), "AssociatedControl", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (LayoutControlItem), "AssociatedControlName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (LayoutControlItem), "Text", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (LayoutControlItem), "DrawText", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (Control), "Tag", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "RootElement", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Size", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Location", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Dock", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Anchor", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } } });
    }

    private void InvalidateNCArea()
    {
      if (!this.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 547);
    }

    protected override void OnNCPaint(Graphics g)
    {
      if (!this.DrawBorder)
        return;
      Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) this.ContainerElement, false);
      this.ContainerElement.BorderPrimitiveImpl.PaintBorder((IGraphics) new RadGdiGraphics(g), 0.0f, new SizeF(1f, 1f), new RectangleF(PointF.Empty, new SizeF((float) (this.Width - borderThickness.Right), (float) (this.Height - borderThickness.Bottom))));
    }

    protected override Padding GetNCMetrics()
    {
      if (this.DrawBorder)
        return LightVisualElement.GetBorderThickness((LightVisualElement) this.ContainerElement, false);
      return Padding.Empty;
    }

    public Padding ClientMargin
    {
      get
      {
        return this.GetNCMetrics();
      }
    }

    protected override bool EnableNCPainting
    {
      get
      {
        return true;
      }
    }

    protected override bool EnableNCModification
    {
      get
      {
        return true;
      }
    }
  }
}
