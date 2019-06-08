// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryPopupElement
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
  public class RadGalleryPopupElement : RadItem
  {
    private RadScrollViewer scrollViewer = new RadScrollViewer();
    private StackLayoutPanel groupHolderStackLayout = new StackLayoutPanel();
    private SizeGripElement gripElement = new SizeGripElement();
    private DockLayoutPanel mainLayout = new DockLayoutPanel();
    private RadMenuElement menuElement = new RadMenuElement();
    private RadDropDownButtonElement filtersDropDownButton = new RadDropDownButtonElement();
    private SizeF actualSize = SizeF.Empty;
    private bool filterEnabled = true;
    private SizingMode dropDownSizingMode = SizingMode.UpDownAndRightBottom;
    private RadGalleryGroupFilter selectedFilter;
    private RadItemOwnerCollection galleryItems;
    private RadItemOwnerCollection groups;
    private RadItemOwnerCollection menuItems;
    private RadItemOwnerCollection filters;
    private SizeF newSize;
    private SizeF initialSize;
    private SizeF minimumSize;

    public RadGalleryPopupElement(
      RadItemOwnerCollection items,
      RadItemOwnerCollection groups,
      RadItemOwnerCollection filters,
      RadItemOwnerCollection menuItems,
      SizeF initialSize,
      SizeF minimumSize)
      : this(items, groups, filters, menuItems, initialSize, minimumSize, SizingMode.UpDownAndRightBottom)
    {
    }

    public RadGalleryPopupElement(
      RadItemOwnerCollection items,
      RadItemOwnerCollection groups,
      RadItemOwnerCollection filters,
      RadItemOwnerCollection menuItems)
    {
      this.galleryItems = items;
      this.groups = groups;
      this.menuItems = menuItems;
      this.newSize = this.initialSize;
      this.filters = filters;
      this.BuildPopupContent();
      this.menuElement.Items.AddRange((RadItemCollection) this.menuItems);
    }

    public RadGalleryPopupElement(
      RadItemOwnerCollection items,
      RadItemOwnerCollection groups,
      RadItemOwnerCollection filters,
      RadItemOwnerCollection menuItems,
      SizeF initialSize,
      SizeF minimumSize,
      SizingMode dropDownSizingMode)
    {
      this.galleryItems = items;
      this.groups = groups;
      this.menuItems = menuItems;
      this.newSize = initialSize;
      this.actualSize = initialSize;
      this.initialSize = initialSize;
      this.minimumSize = minimumSize;
      this.filters = filters;
      this.dropDownSizingMode = dropDownSizingMode;
      this.gripElement.SizingMode = dropDownSizingMode;
      this.BuildPopupContent();
      this.menuElement.MinSize = new Size((int) initialSize.Width, 0);
      this.menuElement.Items.AddRange((RadItemCollection) this.menuItems);
    }

    public RadScrollViewer ScrollViewer
    {
      get
      {
        return this.scrollViewer;
      }
    }

    public RadMenuElement MenuElement
    {
      get
      {
        return this.menuElement;
      }
    }

    public SizeGripElement SizingGrip
    {
      get
      {
        return this.gripElement;
      }
    }

    internal SizeF ActualSize
    {
      get
      {
        return this.actualSize;
      }
      set
      {
        this.actualSize = value;
      }
    }

    internal SizeF NewSize
    {
      get
      {
        return this.newSize;
      }
      set
      {
        this.newSize = value;
      }
    }

    internal SizeF InitialSize
    {
      get
      {
        return this.initialSize;
      }
      set
      {
        this.initialSize = value;
      }
    }

    internal SizeF MinimumSize
    {
      get
      {
        return this.minimumSize;
      }
      set
      {
        this.minimumSize = value;
      }
    }

    internal SizingMode DropDownSizingMode
    {
      get
      {
        return this.dropDownSizingMode;
      }
      set
      {
        this.dropDownSizingMode = value;
      }
    }

    internal RadDropDownButtonElement FiltersButton
    {
      get
      {
        return this.filtersDropDownButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the currently selected group filter.")]
    [Browsable(false)]
    public RadGalleryGroupFilter SelectedFilter
    {
      get
      {
        return this.selectedFilter;
      }
      set
      {
        if (this.selectedFilter == value)
          return;
        if (this.selectedFilter != null)
          this.selectedFilter.Selected = false;
        this.selectedFilter = value;
        this.selectedFilter.Selected = true;
        this.filtersDropDownButton.Text = this.selectedFilter.Text;
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.groups)
        {
          if (!this.selectedFilter.Items.Contains((RadItem) group))
          {
            group.Visibility = ElementVisibility.Collapsed;
            foreach (RadElement radElement in (RadItemCollection) group.Items)
              radElement.Visibility = ElementVisibility.Collapsed;
          }
          else
          {
            group.Visibility = ElementVisibility.Visible;
            foreach (RadItem radItem in (RadItemCollection) group.Items)
            {
              radItem.Visibility = ElementVisibility.Visible;
              this.InvalidateAndUpdate((RadElement) radItem);
            }
          }
        }
        foreach (RadMenuItem radMenuItem in (RadItemCollection) this.filtersDropDownButton.Items)
          radMenuItem.ToggleState = this.selectedFilter == (RadGalleryGroupFilter) radMenuItem.Tag ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
    }

    public StackLayoutPanel GroupHolderStackLayout
    {
      get
      {
        return this.groupHolderStackLayout;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.GroupHolderStackLayout.Orientation = Orientation.Vertical;
      this.gripElement.SizingMode = SizingMode.UpDownAndRightBottom;
      this.scrollViewer.Viewport = (RadElement) this.GroupHolderStackLayout;
      this.scrollViewer.ShowFill = true;
      this.scrollViewer.ShowBorder = false;
      this.scrollViewer.HorizontalScrollState = ScrollState.AlwaysHide;
      this.scrollViewer.ScrollLayoutPanel.MeasureWithAvaibleSize = true;
      this.mainLayout.LastChildFill = true;
      this.menuElement.AllItemsEqualHeight = true;
      DockLayoutPanel.SetDock((RadElement) this.filtersDropDownButton, Dock.Top);
      DockLayoutPanel.SetDock((RadElement) this.gripElement, Dock.Bottom);
      DockLayoutPanel.SetDock((RadElement) this.menuElement, Dock.Bottom);
      this.scrollViewer.Class = "RadGalleryPopupScrollViewer";
      this.scrollViewer.FillElement.Class = "RadGalleryPopupScrollViewerFill";
      this.gripElement.SizingMode = this.dropDownSizingMode;
      this.mainLayout.Children.Add((RadElement) this.filtersDropDownButton);
      this.mainLayout.Children.Add((RadElement) this.gripElement);
      this.mainLayout.Children.Add((RadElement) this.menuElement);
      this.mainLayout.Children.Add((RadElement) this.scrollViewer);
      this.Children.Add((RadElement) this.mainLayout);
      this.WireEvents();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(this.newSize);
      this.SetMenuItemsVertically();
      return this.newSize;
    }

    private void SetMenuItemsVertically()
    {
      int num = 0;
      foreach (RadElement menuItem in (RadItemCollection) this.menuItems)
        num = Math.Max(num, (int) menuItem.DesiredSize.Width);
      this.menuElement.MaxSize = new Size(num, 0);
      if (this.ElementTree == null)
        return;
      this.ElementTree.Control.BackColor = this.filtersDropDownButton.BackColor;
    }

    internal void ClearCollections()
    {
      foreach (RadElement radElement in (RadItemCollection) this.filtersDropDownButton.Items)
        radElement.Click -= new EventHandler(this.filterItem_Click);
      this.filtersDropDownButton.Items.Clear();
      this.GroupHolderStackLayout.Children.Clear();
      this.menuElement.Items.Clear();
    }

    protected void WireEvents()
    {
      this.gripElement.GripItemNS.Sizing += new ValueChangingEventHandler(this.GripItemNS_Sizing);
      this.gripElement.GripItemNSEW.Sizing += new ValueChangingEventHandler(this.GripItemNS_Sizing);
      this.gripElement.GripItemNS.Sized += new ValueChangingEventHandler(this.GripItemNS_Sized);
      this.gripElement.GripItemNSEW.Sized += new ValueChangingEventHandler(this.GripItemNS_Sized);
    }

    protected void UnWireEvents()
    {
      this.gripElement.GripItemNS.Sizing -= new ValueChangingEventHandler(this.GripItemNS_Sizing);
      this.gripElement.GripItemNSEW.Sizing -= new ValueChangingEventHandler(this.GripItemNS_Sizing);
      this.gripElement.GripItemNS.Sized -= new ValueChangingEventHandler(this.GripItemNS_Sized);
      this.gripElement.GripItemNSEW.Sized -= new ValueChangingEventHandler(this.GripItemNS_Sized);
    }

    internal void BuildPopupContent()
    {
      this.BuildGroup();
      this.BuildFilters();
    }

    protected void BuildGroup()
    {
      this.galleryItems.Owner = (RadElement) null;
      if (this.groups.Count == 0)
      {
        RadGalleryGroupItem galleryGroupItem = new RadGalleryGroupItem(string.Empty);
        galleryGroupItem.ShowCaption = false;
        galleryGroupItem.Items.AddRange((RadItemCollection) this.galleryItems);
        galleryGroupItem.Items.Owner = (RadElement) galleryGroupItem.ItemsLayoutPanel;
        this.GroupHolderStackLayout.Children.Add((RadElement) galleryGroupItem);
        this.InvalidateAndUpdate((RadElement) galleryGroupItem.ItemsLayoutPanel);
      }
      else
      {
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.groups)
        {
          group.Items.Owner = (RadElement) group.ItemsLayoutPanel;
          this.GroupHolderStackLayout.Children.Add((RadElement) group);
          this.InvalidateAndUpdate((RadElement) group.ItemsLayoutPanel);
        }
      }
      this.InvalidateAndUpdate((RadElement) this.GroupHolderStackLayout);
    }

    private void InvalidateAndUpdate(RadElement element)
    {
      element.InvalidateMeasure();
      element.InvalidateArrange();
      element.UpdateLayout();
    }

    private void BuildFilters()
    {
      if (this.filterEnabled && this.filters.Count > 0 && this.groups.Count > 0)
      {
        foreach (RadGalleryGroupFilter filter in (RadItemCollection) this.filters)
        {
          RadMenuItem radMenuItem = new RadMenuItem(filter.Text);
          radMenuItem.Click += new EventHandler(this.filterItem_Click);
          radMenuItem.Tag = (object) filter;
          if (filter.Selected)
          {
            this.SelectedFilter = filter;
            radMenuItem.IsChecked = true;
          }
          this.filtersDropDownButton.Items.Add((RadItem) radMenuItem);
        }
        if (this.SelectedFilter == null)
          this.SelectedFilter = this.filters[0] as RadGalleryGroupFilter;
        this.filtersDropDownButton.Visibility = ElementVisibility.Visible;
      }
      else
        this.filtersDropDownButton.Visibility = ElementVisibility.Collapsed;
    }

    private void filterItem_Click(object sender, EventArgs e)
    {
      this.SelectedFilter = ((RadElement) sender).Tag as RadGalleryGroupFilter;
    }

    private void GripItemNS_Sized(object sender, ValueChangingEventArgs e)
    {
      this.newSize = this.actualSize + (SizeF) e.NewValue;
      if ((double) this.newSize.Width < (double) this.minimumSize.Width)
        this.newSize.Width = this.minimumSize.Width;
      if ((double) this.newSize.Height < (double) this.minimumSize.Height)
        this.newSize.Height = this.minimumSize.Height;
      if ((double) this.newSize.Width > (double) this.ElementTree.RootElement.MaxSize.Width && this.ElementTree.RootElement.MaxSize.Width > 0)
        this.newSize.Width = (float) this.ElementTree.RootElement.MaxSize.Width;
      if ((double) this.newSize.Height > (double) this.ElementTree.RootElement.MaxSize.Height && this.ElementTree.RootElement.MaxSize.Height > 0)
        this.newSize.Height = (float) this.ElementTree.RootElement.MaxSize.Height;
      this.InvalidateMeasure();
      this.ElementTree.RootElement.UpdateLayout();
      this.ElementTree.Control.Size = this.ElementTree.RootElement.DesiredSize.ToSize();
    }

    private void GripItemNS_Sizing(object sender, ValueChangingEventArgs e)
    {
      this.actualSize = (SizeF) this.Size;
    }
  }
}
