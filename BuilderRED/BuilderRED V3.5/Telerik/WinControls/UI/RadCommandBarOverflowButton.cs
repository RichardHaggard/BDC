// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarOverflowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class RadCommandBarOverflowButton : RadCommandBarVisualElement
  {
    public static RadProperty HasOverflowedItemsProperty = RadProperty.Register(nameof (HasOverflowedItems), typeof (bool), typeof (RadCommandBarOverflowButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    protected RadDropDownMenu dropDownMenuElement;
    private RadMenuItem addRemoveButtonsMenuItem;
    private RadMenuItem customizeButtonMenuItem;
    protected RadCommandBarOverflowPanelElement panel;
    protected CommandBarStripElement owner;
    private ArrowPrimitive arrowPrimitive;
    private LayoutPanel layout;
    private CommandBarCustomizeDialogProvider dialogProvider;
    private bool cachedHasOverflowedItems;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawBorder = true;
      this.DrawFill = true;
      this.MinSize = new Size(11, 25);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.OnOverflowMenuOpening(new CancelEventArgs()))
        return;
      base.OnMouseDown(e);
      this.ShowOverflowMenu();
      this.OnOverflowMenuOpened(new EventArgs());
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return new SizeF(11f, 25f);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
      this.Invalidate();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CommandBarCustomizeDialogProvider DialogProvider
    {
      get
      {
        return this.dialogProvider;
      }
      set
      {
        this.dialogProvider = value;
      }
    }

    public RadMenuItem AddRemoveButtonsMenuItem
    {
      get
      {
        return this.addRemoveButtonsMenuItem;
      }
    }

    public RadMenuItem CustomizeButtonMenuItem
    {
      get
      {
        return this.customizeButtonMenuItem;
      }
    }

    public RadCommandBarOverflowPanelElement OverflowPanel
    {
      get
      {
        return this.panel;
      }
    }

    public RadDropDownMenu DropDownMenu
    {
      get
      {
        return this.dropDownMenuElement;
      }
    }

    public bool HasOverflowedItems
    {
      get
      {
        return this.cachedHasOverflowedItems;
      }
    }

    public override Orientation Orientation
    {
      get
      {
        return this.cachedOrientation;
      }
      set
      {
        this.cachedOrientation = value;
        this.AngleTransform = value == Orientation.Horizontal ? 0.0f : 90f;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string HostControlThemeName
    {
      get
      {
        return this.dropDownMenuElement.ThemeName;
      }
      set
      {
        this.dropDownMenuElement.ThemeName = value;
      }
    }

    public LayoutPanel ItemsLayout
    {
      get
      {
        return this.layout;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ArrowPrimitive ArrowPrimitive
    {
      get
      {
        return this.arrowPrimitive;
      }
      set
      {
        this.arrowPrimitive = value;
      }
    }

    public event CancelEventHandler OverflowMenuOpening;

    public event EventHandler OverflowMenuOpened;

    public event CancelEventHandler OverflowMenuClosing;

    public event EventHandler OverflowMenuClosed;

    protected void OnOverflowMenuOpened(EventArgs e)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) true);
      if (this.OverflowMenuOpened == null)
        return;
      this.OverflowMenuOpened((object) this.owner, e);
    }

    protected bool OnOverflowMenuOpening(CancelEventArgs e)
    {
      if (this.OverflowMenuOpening == null)
        return false;
      this.OverflowMenuOpening((object) this.owner, e);
      return e.Cancel;
    }

    protected void OnOverflowMenuClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) false);
      if (this.OverflowMenuClosed == null)
        return;
      this.OverflowMenuClosed(sender, (EventArgs) args);
    }

    protected virtual void OnOverflowMenuClosing(object sender, RadPopupClosingEventArgs args)
    {
      if (args.Cancel || this.OverflowMenuClosing == null)
        return;
      CancelEventArgs e = new CancelEventArgs();
      this.OverflowMenuClosing((object) this, e);
      args.Cancel = e.Cancel;
    }

    static RadCommandBarOverflowButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadCommandBarOverflowButton.RadCommandBarOverflowButtonStateManagerFactory(), typeof (RadCommandBarOverflowButton));
    }

    public RadCommandBarOverflowButton(CommandBarStripElement owner)
    {
      this.dialogProvider = new CommandBarCustomizeDialogProvider();
      this.owner = owner;
      this.dropDownMenuElement = new RadDropDownMenu((RadElement) this);
      this.dropDownMenuElement.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToEdges;
      this.dropDownMenuElement.MinimumSize = this.owner.OverflowMenuMinSize;
      this.dropDownMenuElement.MaximumSize = this.owner.OverflowMenuMaxSize;
      this.dropDownMenuElement.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.panel = new RadCommandBarOverflowPanelElement();
      RadMenuSeparatorItem menuSeparatorItem = new RadMenuSeparatorItem();
      this.panel.Visibility = ElementVisibility.Collapsed;
      menuSeparatorItem.Visibility = ElementVisibility.Collapsed;
      this.dropDownMenuElement.Items.Add((RadItem) this.panel);
      this.dropDownMenuElement.Items.Add((RadItem) menuSeparatorItem);
      this.addRemoveButtonsMenuItem = new RadMenuItem(LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("OverflowMenuAddOrRemoveButtonsText"));
      this.dropDownMenuElement.Items.Add((RadItem) this.addRemoveButtonsMenuItem);
      this.dropDownMenuElement.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.customizeButtonMenuItem = new RadMenuItem(LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("OverflowMenuCustomizeText"));
      this.dropDownMenuElement.Items.Add((RadItem) this.customizeButtonMenuItem);
      this.layout = this.panel.Layout;
      this.layout.MaxSize = this.owner.OverflowMenuMaxSize;
      this.WireEvents();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.ElementTree.Control == null)
        return;
      this.dropDownMenuElement.BindingContext = this.ElementTree.Control.BindingContext;
      this.ElementTree.Control.BindingContextChanged += new EventHandler(this.Control_BindingContextChanged);
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      if (oldTree.Control == null)
        return;
      oldTree.Control.BindingContextChanged -= new EventHandler(this.Control_BindingContextChanged);
    }

    private void Control_BindingContextChanged(object sender, EventArgs e)
    {
      if (this.ElementTree.Control == null)
        return;
      this.dropDownMenuElement.BindingContext = this.ElementTree.Control.BindingContext;
    }

    protected void customizeButton_Click(object sender, EventArgs e)
    {
      RadControl radControl = (RadControl) null;
      if (this.ElementTree != null)
        radControl = this.ElementTree.Control as RadControl;
      if (this.owner.FloatingForm != null && !this.owner.FloatingForm.IsDisposed)
      {
        RadControl itemsHostControl = (RadControl) this.owner.FloatingForm.ItemsHostControl;
        CommandBarCustomizeDialogProvider.CurrentProvider.ShowCustomizeDialog((object) this.owner, this.owner.FloatingForm.StripInfoHolder);
      }
      else
      {
        RadCommandBar radCommandBar = (RadCommandBar) null;
        if (radControl != null)
          radCommandBar = radControl as RadCommandBar;
        if (radCommandBar == null)
          return;
        CommandBarCustomizeDialogProvider.CurrentProvider.ShowCustomizeDialog((object) this.owner, radCommandBar.CommandBarElement.StripInfoHolder);
      }
    }

    private void wrapLayout_ChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      if (e.ChangeOperation != ItemsChangeOperation.Inserted && e.ChangeOperation != ItemsChangeOperation.Removed && e.ChangeOperation != ItemsChangeOperation.Cleared)
        return;
      this.cachedHasOverflowedItems = this.layout.Children.Count > 0;
      int num = (int) this.SetValue(RadCommandBarOverflowButton.HasOverflowedItemsProperty, (object) (this.layout.Children.Count > 0));
    }

    private void WireEvents()
    {
      this.dropDownMenuElement.PopupClosed += new RadPopupClosedEventHandler(this.OnOverflowMenuClosed);
      this.dropDownMenuElement.PopupClosing += new RadPopupClosingEventHandler(this.OnOverflowMenuClosing);
      this.customizeButtonMenuItem.Click += new EventHandler(this.customizeButton_Click);
      this.layout.ChildrenChanged += new ChildrenChangedEventHandler(this.wrapLayout_ChildrenChanged);
      this.addRemoveButtonsMenuItem.DropDownClosing += new RadPopupClosingEventHandler(this.dropDownMenuElement_DropDownClosing);
      LocalizationProvider<CommandBarLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.CommandBarLocalizationProvider_CurrentProviderChanged);
    }

    private void UnwireEvents()
    {
      this.dropDownMenuElement.PopupClosed -= new RadPopupClosedEventHandler(this.OnOverflowMenuClosed);
      this.dropDownMenuElement.PopupClosing -= new RadPopupClosingEventHandler(this.OnOverflowMenuClosing);
      this.customizeButtonMenuItem.Click -= new EventHandler(this.customizeButton_Click);
      this.layout.ChildrenChanged -= new ChildrenChangedEventHandler(this.wrapLayout_ChildrenChanged);
      this.addRemoveButtonsMenuItem.DropDownClosing -= new RadPopupClosingEventHandler(this.dropDownMenuElement_DropDownClosing);
      if (this.ElementTree != null && this.ElementTree.Control != null)
        this.ElementTree.Control.BindingContextChanged -= new EventHandler(this.Control_BindingContextChanged);
      LocalizationProvider<CommandBarLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.CommandBarLocalizationProvider_CurrentProviderChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
      this.dropDownMenuElement.Dispose();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadCommandBarOverflowButton.HasOverflowedItemsProperty)
      {
        if (this.HasOverflowedItems)
        {
          this.dropDownMenuElement.Items[0].Visibility = ElementVisibility.Visible;
          this.dropDownMenuElement.Items[1].Visibility = ElementVisibility.Visible;
        }
        else
        {
          this.dropDownMenuElement.Items[0].Visibility = ElementVisibility.Collapsed;
          this.dropDownMenuElement.Items[1].Visibility = ElementVisibility.Collapsed;
        }
      }
      else if (e.Property == RadElement.VisibilityProperty)
      {
        if (this.LayoutManager == null || this.owner == null)
          return;
        this.LayoutManager.MeasureQueue.Add((RadElement) this.owner);
      }
      else
      {
        if (e.Property != RadElement.RightToLeftProperty)
          return;
        this.dropDownMenuElement.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      }
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control == null)
        return;
      this.dropDownMenuElement.ImageList = control.ImageList;
    }

    public void PopulateDropDownMenu()
    {
      this.addRemoveButtonsMenuItem.Items.Clear();
      int count1 = this.owner.Items.Count;
      for (int index = 0; index < count1; ++index)
      {
        RadCommandBarBaseItem representedItem = this.owner.Items[index];
        if (representedItem.VisibleInOverflowMenu)
          this.addRemoveButtonsMenuItem.Items.Add((RadItem) new RadCommandBarOverflowMenuItem(representedItem, this.dropDownMenuElement));
      }
      int count2 = this.layout.Children.Count;
      for (int index = 0; index < count2; ++index)
      {
        RadCommandBarBaseItem child = this.layout.Children[index] as RadCommandBarBaseItem;
        if (child != null && child.VisibleInOverflowMenu)
          this.addRemoveButtonsMenuItem.Items.Add((RadItem) new RadCommandBarOverflowMenuItem(child, this.dropDownMenuElement));
      }
      this.addRemoveButtonsMenuItem.MinSize = this.owner.OverflowMenuMinSize;
      this.addRemoveButtonsMenuItem.MaxSize = this.owner.OverflowMenuMaxSize;
      this.SetVisualStyles((LightVisualElement) this.dropDownMenuElement.Items[0]);
      this.dropDownMenuElement.Items[0].InvalidateMeasure(true);
      this.dropDownMenuElement.LoadElementTree();
    }

    private void SetVisualStyles(LightVisualElement item)
    {
      item.BackColor = this.owner.BackColor;
      item.BackColor2 = this.owner.BackColor2;
      item.BackColor3 = this.owner.BackColor3;
      item.BackColor4 = this.owner.BackColor4;
      item.NumberOfColors = this.owner.NumberOfColors;
      item.BorderColor = this.owner.BorderColor;
      item.BorderColor2 = this.owner.BorderColor2;
      item.BorderColor3 = this.owner.BorderColor3;
      item.BorderColor4 = this.owner.BorderColor4;
      item.DrawFill = this.owner.DrawFill;
      item.DrawBorder = this.owner.DrawBorder;
    }

    private void ShowOverflowMenu()
    {
      this.PopulateDropDownMenu();
      Point point = new Point(0, this.Size.Height);
      if (this.Orientation == Orientation.Vertical)
        point = new Point(this.Size.Width, this.Size.Height);
      else if (this.RightToLeft)
        point = new Point(this.Size.Width - this.dropDownMenuElement.PreferredSize.Width, this.Size.Height);
      this.dropDownMenuElement.Show((RadItem) this, point);
    }

    private void CommandBarLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.addRemoveButtonsMenuItem.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("OverflowMenuAddOrRemoveButtonsText");
      this.customizeButtonMenuItem.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("OverflowMenuCustomizeText");
    }

    private void dropDownMenuElement_DropDownClosing(object sender, RadPopupClosingEventArgs args)
    {
      RadItemCollection items = (RadItemCollection) this.addRemoveButtonsMenuItem.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].ContainsMouse)
          args.Cancel = true;
      }
    }

    private class RadCommandBarOverflowButtonStateManagerFactory : ItemStateManagerFactory
    {
      protected override StateNodeBase CreateSpecificStates()
      {
        StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("HasOverflowedItems", (Condition) new SimpleCondition(RadCommandBarOverflowButton.HasOverflowedItemsProperty, (object) true));
        StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsDropDownShown", (Condition) new SimpleCondition(RadDropDownButtonElement.IsDropDownShownProperty, (object) true));
        CompositeStateNode compositeStateNode = new CompositeStateNode("command bar overflow button states");
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
        return (StateNodeBase) compositeStateNode;
      }

      protected override ItemStateManagerBase CreateStateManager()
      {
        ItemStateManagerBase stateManager = base.CreateStateManager();
        stateManager.AddDefaultVisibleState("HasOverflowedItems");
        stateManager.AddDefaultVisibleState("IsDropDownShown");
        return stateManager;
      }
    }
  }
}
