// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuItemBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public abstract class RadMenuItemBase : RadButtonItem, IHierarchicalItem, IItemsOwner, ISiteProvider
  {
    public static readonly RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (RadMenuItemBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue));
    public static readonly RadProperty IsPopupShownProperty = RadProperty.Register(nameof (IsPopupShown), typeof (bool), typeof (RadMenuItemBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static readonly RadProperty PopupDirectionProperty = RadProperty.Register(nameof (PopupDirection), typeof (RadDirection), typeof (RadMenuItemBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Right, ElementPropertyOptions.None));
    private int positionToBeRestoredAfterMerge = -1;
    internal const long IsMdiListItemStateKey = 140737488355328;
    internal const long IsParticipatingInMergeStateKey = 281474976710656;
    internal const long MdiListStateKey = 562949953421312;
    internal const long SelectedByMouseStateKey = 1125899906842624;
    internal const long IsMainMenuItemStateKey = 2251799813685248;
    internal const long HandleClickStateKey = 4503599627370496;
    internal const long PreparedForDesignTimeStateKey = 9007199254740992;
    internal const long HandlesKeyboardStateKey = 18014398509481984;
    private MenuMerge mergeType;
    private int mergeOrder;
    private object owner;
    private IHierarchicalItem hierarchyParent;
    private RadDropDownMenu dropDown;
    private Control ownerControl;
    private RadMenuItemAccessibleObject accessibleObject;
    protected MouseButtons pressedButton;

    internal virtual int OffsetWidth
    {
      get
      {
        return 0;
      }
    }

    internal virtual RadElement ElementToOffset
    {
      get
      {
        return (RadElement) null;
      }
    }

    internal virtual RadElement ElementToAlignRight
    {
      get
      {
        return (RadElement) null;
      }
    }

    internal virtual int AlignRightOffsetWidth
    {
      get
      {
        return 0;
      }
    }

    internal virtual void SetElementOffsets(Padding padding)
    {
    }

    static RadMenuItemBase()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadMenuItemBaseStateManagerFactory(), typeof (RadMenuItemBase));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[17592186044416L] = false;
      this.BitState[4503599627370496L] = true;
      this.Class = "RadMenuItem";
      this.ClickMode = ClickMode.Release;
    }

    public override bool ProcessMnemonic(char charCode)
    {
      if (Control.IsMnemonic(charCode, this.Text))
        this.Select();
      return base.ProcessMnemonic(charCode);
    }

    protected override void DisposeManagedResources()
    {
      if (this.dropDown != null)
      {
        this.UnWireDropDownEvents();
        this.dropDown.Dispose();
      }
      if (this.accessibleObject != null)
      {
        this.accessibleObject.Dispose();
        this.accessibleObject = (RadMenuItemAccessibleObject) null;
      }
      base.DisposeManagedResources();
    }

    public virtual void ShowChildItems()
    {
      if (!this.IsInValidState(true))
        return;
      this.EnsureDropDownCreated();
      if (this.Visibility != ElementVisibility.Visible || !this.HasChildren && this.GetSite() == null || this.IsPopupShown)
        return;
      this.AdjustDropDownAnimations();
      if (this.GetValueSource(RadMenuItemBase.PopupDirectionProperty) == ValueSource.DefaultValue || this.GetValueSource(RadMenuItemBase.PopupDirectionProperty) == ValueSource.DefaultValueOverride)
        this.AdjustDropDownAlignmentForOrientation();
      else
        this.AdjustDropDownAlignmentForPopupDirection();
      if (this.dropDown.LastShowDpiScaleFactor != this.DpiScaleFactor)
      {
        SizeF sizeF = this.dropDown.LastShowDpiScaleFactor.IsEmpty ? new SizeF(1f, 1f) : this.dropDown.LastShowDpiScaleFactor;
        this.dropDown.Scale(new SizeF(this.DpiScaleFactor.Width / sizeF.Width, this.DpiScaleFactor.Height / sizeF.Height));
        this.dropDown.LastShowDpiScaleFactor = this.DpiScaleFactor;
      }
      this.dropDown.Show((RadItem) this, 0, this.PopupDirection);
    }

    protected virtual void AdjustDropDownAlignmentForPopupDirection()
    {
      if (this.GetSite() != null)
        return;
      switch (this.PopupDirection)
      {
        case RadDirection.Left:
          this.dropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToLeft;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
          break;
        case RadDirection.Right:
          this.dropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToRight;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
          break;
        case RadDirection.Up:
          this.dropDown.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.BottomToTop;
          break;
        case RadDirection.Down:
          this.dropDown.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
          break;
      }
    }

    protected virtual void AdjustDropDownAlignmentForOrientation()
    {
      if (this.HierarchyParent != null || !this.IsMainMenuItem)
      {
        if (!this.RightToLeft)
          this.dropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToRight;
        else
          this.dropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToLeft;
        this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
        this.dropDown.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      }
      else
      {
        this.dropDown.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.Smooth;
        this.dropDown.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
        if (!this.IsInValidState(true) || !(this.ElementTree.Control is RadMenu))
          return;
        if ((this.ElementTree.Control as RadMenu).Orientation == Orientation.Vertical)
        {
          this.dropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToRight;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
        }
        else
        {
          this.dropDown.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
          this.dropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
        }
      }
    }

    protected virtual void AdjustDropDownAnimations()
    {
      if (!this.IsInValidState(true))
        return;
      if (this.ElementTree.Control is RadMenu)
      {
        this.dropDown.AnimationEnabled = ((RadMenu) this.ElementTree.Control).DropDownAnimationEnabled;
        this.dropDown.EasingType = ((RadMenu) this.ElementTree.Control).DropDownAnimationEasing;
        this.dropDown.AnimationFrames = ((RadMenu) this.ElementTree.Control).DropDownAnimationFrames;
        this.dropDown.DropDownAnimationDirection = this.PopupDirection;
      }
      else
      {
        if (!(this.ElementTree.Control is RadDropDownMenu))
          return;
        this.dropDown.AnimationEnabled = ((RadPopupControlBase) this.ElementTree.Control).AnimationEnabled;
        this.dropDown.EasingType = ((RadPopupControlBase) this.ElementTree.Control).EasingType;
        this.dropDown.AnimationFrames = ((RadPopupControlBase) this.ElementTree.Control).AnimationFrames;
      }
    }

    public virtual void HideChildItems()
    {
      if (this.dropDown == null)
        return;
      this.dropDown.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    [Category("Behavior")]
    [Description("Occurs after the menu item dropdown opens.")]
    public event EventHandler DropDownOpened;

    [Description("Occurs before the menu item dropdown opens.")]
    [Category("Behavior")]
    public event CancelEventHandler DropDownOpening;

    [Category("Behavior")]
    [Description("Occurs after the menu item dropdown closes.")]
    public event RadPopupClosedEventHandler DropDownClosed;

    [Category("Behavior")]
    [Description("Occurs before the popup is creating.")]
    public event EventHandler DropDownCreating;

    [Description("Occurs before the popup is closed.")]
    [Category("Behavior")]
    public event RadPopupClosingEventHandler DropDownClosing;

    internal int PositionToBeRestoredAfterMerge
    {
      get
      {
        return this.positionToBeRestoredAfterMerge;
      }
      set
      {
        this.positionToBeRestoredAfterMerge = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool DesignTimeAllowDrop
    {
      get
      {
        return false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public RadMenuItemAccessibleObject AccessibleObject
    {
      get
      {
        if (this.accessibleObject == null)
          this.accessibleObject = new RadMenuItemAccessibleObject(this);
        return this.accessibleObject;
      }
    }

    [DefaultValue(false)]
    public bool MdiList
    {
      get
      {
        return this.GetBitState(562949953421312L);
      }
      set
      {
        this.SetBitState(562949953421312L, value);
      }
    }

    [DefaultValue(MenuMerge.Add)]
    public MenuMerge MergeType
    {
      get
      {
        return this.mergeType;
      }
      set
      {
        this.mergeType = value;
      }
    }

    [DefaultValue(0)]
    public int MergeOrder
    {
      get
      {
        return this.mergeOrder;
      }
      set
      {
        this.mergeOrder = value;
      }
    }

    [RadPropertyDefaultValue("Selected", typeof (RadMenuItemBase))]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(RadMenuItemBase.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuItemBase.SelectedProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("IsPopupShown", typeof (RadMenuItemBase))]
    [Description("Indicates whether the popup containing this menu item's children is shown.")]
    public bool IsPopupShown
    {
      get
      {
        return (bool) this.GetValue(RadMenuItemBase.IsPopupShownProperty);
      }
    }

    [Description("Gets or sets the direction of the popup which is opened by this menu item.")]
    [Browsable(true)]
    [Category("Behavior")]
    public RadDirection PopupDirection
    {
      get
      {
        return (RadDirection) this.GetValue(RadMenuItemBase.PopupDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuItemBase.PopupDirectionProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        this.EnsureDropDownCreated();
        return this.dropDown.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string DropDownHeaderText
    {
      get
      {
        if (this.dropDown != null)
          return this.dropDown.HeaderText;
        return "";
      }
      set
      {
        this.EnsureDropDownCreated();
        this.dropDown.HeaderText = value;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image DropDownHeaderImage
    {
      get
      {
        if (this.dropDown != null)
          return this.dropDown.HeaderImage;
        return (Image) null;
      }
      set
      {
        this.EnsureDropDownCreated();
        this.dropDown.HeaderImage = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsMainMenuItem
    {
      get
      {
        return this.GetBitState(2251799813685248L);
      }
      set
      {
        this.SetBitState(2251799813685248L, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Control OwnerControl
    {
      get
      {
        if (this.ownerControl != null)
          return this.ownerControl;
        if (this.Owner is RadMenuItemBase && (this.Owner as RadMenuItemBase).ElementTree != null)
          return (this.Owner as RadMenuItemBase).ElementTree.Control;
        if (this.ElementTree != null)
          return this.ElementTree.Control;
        return (Control) null;
      }
      internal set
      {
        this.ownerControl = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsOnDropDown
    {
      get
      {
        return this.FindAncestor<RadDropDownMenuLayout>() != null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool HasChildItemsToShow
    {
      get
      {
        if (this.dropDown != null)
          return this.LayoutableChildrenCount > 0;
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadDropDownMenu DropDown
    {
      get
      {
        return this.dropDown;
      }
    }

    protected override IComponentTreeHandler ShortcutsHandler
    {
      get
      {
        if (this.Owner is RadMenuItemBase)
          return (this.Owner as RadMenuItemBase).ElementTree.ComponentTreeHandler;
        return (IComponentTreeHandler) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public virtual bool HandlesKeyboard
    {
      get
      {
        return this.GetBitState(18014398509481984L);
      }
      set
      {
        this.SetBitState(18014398509481984L, value);
      }
    }

    private IHierarchicalItem FindRootHierarchyItem()
    {
      for (IHierarchicalItem hierarchyParent = this.hierarchyParent; hierarchyParent != null; hierarchyParent = hierarchyParent.HierarchyParent)
      {
        if (hierarchyParent.HierarchyParent == null)
          return hierarchyParent;
      }
      return (IHierarchicalItem) null;
    }

    public IHierarchicalItem RootItem
    {
      get
      {
        return this.FindRootHierarchyItem();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object Owner
    {
      get
      {
        if (this.HierarchyParent != null)
          return this.HierarchyParent.Owner;
        return this.owner;
      }
      set
      {
        if (this.HierarchyParent != null)
          return;
        this.owner = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool HasChildren
    {
      get
      {
        if (this.dropDown != null)
          return this.dropDown.Items.Count > 0;
        return false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsRootItem
    {
      get
      {
        return this.HierarchyParent == null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IHierarchicalItem HierarchyParent
    {
      get
      {
        return this.hierarchyParent;
      }
      set
      {
        this.hierarchyParent = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadItem Next
    {
      get
      {
        if (this.HierarchyParent != null)
        {
          int count = this.HierarchyParent.Items.Count;
          int index = this.HierarchyParent.Items.IndexOf((RadItem) this) + 1;
          if (count > index)
            return this.HierarchyParent.Items[index];
        }
        return (RadItem) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadItem Previous
    {
      get
      {
        if (this.HierarchyParent != null)
        {
          int index = this.HierarchyParent.Items.IndexOf((RadItem) this) - 1;
          if (index >= 0)
            return this.HierarchyParent.Items[index];
        }
        return (RadItem) null;
      }
    }

    public ISite GetSite()
    {
      ISite site = this.Site;
      if (site == null && this.dropDown != null && this.dropDown.Items.Count > 0)
        site = this.dropDown.Items[0].Site;
      return site;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
        return;
      if (this.GetSite() != null && this.ElementTree.Control is IItemsControl)
      {
        foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) (this.ElementTree.Control as IItemsControl).Items)
        {
          if (!object.ReferenceEquals((object) this, (object) radMenuItemBase) && radMenuItemBase.IsPopupShown)
            radMenuItemBase.HideChildItems();
        }
      }
      if (!this.IsPopupShown)
      {
        this.ShowChildItems();
      }
      else
      {
        if (this.IsOnDropDown)
          return;
        this.HideChildItems();
      }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.Select();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.IsMouseDown = false;
      if (this.IsPopupShown)
        return;
      IItemsControl control = this.ElementTree.Control as IItemsControl;
      if (control == null || !object.ReferenceEquals((object) control.GetSelectedItem(), (object) this))
        return;
      this.Deselect();
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.GetSite() == null || this.ElementTree.Control is RadMenu)
        return;
      PopupManager.Default.CloseAll(RadPopupCloseReason.Mouse);
      IDesignerHost service = (IDesignerHost) this.Site.GetService(typeof (IDesignerHost));
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (args.RoutedEvent != RadElement.MouseClickedEvent)
        return;
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Click", (object) this.Text);
      if (this.GetSite() == null || this.ElementTree.Control is RadMenu)
        return;
      ISelectionService service = this.Site.GetService(typeof (ISelectionService)) as ISelectionService;
      if (service == null || service.GetComponentSelected((object) this))
        return;
      service.SetSelectedComponents((ICollection) new IComponent[1]
      {
        (IComponent) this
      });
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty || this.dropDown == null)
        return;
      this.dropDown.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
    }

    protected virtual void OnDropDownClosed(RadPopupClosedEventArgs args)
    {
      if (this.DropDownClosed != null)
        this.DropDownClosed((object) this, args);
      int num = (int) this.SetValue(RadMenuItemBase.IsPopupShownProperty, (object) false);
    }

    protected virtual void OnDropDownOpening(CancelEventArgs args)
    {
      if (this.DropDownOpening == null)
        return;
      this.DropDownOpening((object) this, args);
    }

    protected virtual void OnDropDownClosing(RadPopupClosingEventArgs args)
    {
      if (this.DropDownClosing == null)
        return;
      this.DropDownClosing((object) this, args);
    }

    protected virtual void OnDropDownOpened(EventArgs args)
    {
      if (this.DropDownOpened != null)
        this.DropDownOpened((object) this, args);
      int num = (int) this.SetValue(RadMenuItemBase.IsPopupShownProperty, (object) true);
    }

    protected virtual RadDropDownMenu CreateDropDownMenu()
    {
      RadDropDownMenu radDropDownMenu = (RadDropDownMenu) null;
      if (this.DropDownCreating != null)
      {
        this.DropDownCreating((object) radDropDownMenu, EventArgs.Empty);
        if (radDropDownMenu != null)
          return radDropDownMenu;
      }
      return new RadDropDownMenu((RadElement) this);
    }

    protected virtual void EnsureDropDownCreated()
    {
      if (this.dropDown != null)
        return;
      this.dropDown = this.CreateDropDownMenu();
      this.dropDown.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.WireDropDownEvents();
      this.OnDropDownCreated();
    }

    private void WireDropDownEvents()
    {
      this.dropDown.DropDownOpened += new EventHandler(this.dropDown_DropDownOpened);
      this.dropDown.DropDownOpening += new CancelEventHandler(this.dropDown_DropDownOpening);
      this.dropDown.DropDownClosed += new RadPopupClosedEventHandler(this.dropDown_DropDownClosed);
      this.dropDown.PopupClosing += new RadPopupClosingEventHandler(this.dropDown_PopupClosing);
    }

    private void UnWireDropDownEvents()
    {
      this.dropDown.DropDownOpened -= new EventHandler(this.dropDown_DropDownOpened);
      this.dropDown.DropDownOpening -= new CancelEventHandler(this.dropDown_DropDownOpening);
      this.dropDown.DropDownClosed -= new RadPopupClosedEventHandler(this.dropDown_DropDownClosed);
      this.dropDown.PopupClosing -= new RadPopupClosingEventHandler(this.dropDown_PopupClosing);
    }

    private void dropDown_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnDropDownClosing(args);
    }

    private void dropDown_DropDownClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num = (int) this.SetValue(RadMenuItemBase.IsPopupShownProperty, (object) false);
      this.OnDropDownClosed(args);
    }

    private void dropDown_DropDownOpening(object sender, CancelEventArgs e)
    {
      this.OnDropDownOpening(e);
    }

    private void dropDown_DropDownOpened(object sender, EventArgs e)
    {
      this.dropDown.Site = this.ElementTree.Control.Site;
      int num = (int) this.SetValue(RadMenuItemBase.IsPopupShownProperty, (object) true);
      this.OnDropDownOpened(EventArgs.Empty);
    }

    protected virtual void OnDropDownCreated()
    {
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      return new RectangleF((float) (this.Padding.Left + this.BorderThickness.Left), (float) (this.Padding.Top + this.BorderThickness.Top), Math.Max(0.0f, finalSize.Width - (float) (this.Padding.Horizontal + this.BorderThickness.Horizontal)), Math.Max(0.0f, finalSize.Height - (float) (this.Padding.Vertical + this.BorderThickness.Vertical)));
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "Class")
        return new bool?(this.Class != "RadMenuItem");
      return base.ShouldSerializeProperty(property);
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsMdiListItem
    {
      get
      {
        return this.BitState[140737488355328L];
      }
      set
      {
        this.SetBitState(140737488355328L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool IsParticipatingInMerge
    {
      get
      {
        return this.BitState[281474976710656L];
      }
      set
      {
        this.SetBitState(281474976710656L, value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPreparedForDesignTime
    {
      get
      {
        return this.BitState[9007199254740992L];
      }
      set
      {
        this.SetBitState(9007199254740992L, value);
      }
    }
  }
}
