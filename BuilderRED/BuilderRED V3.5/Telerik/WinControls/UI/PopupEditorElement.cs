// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PopupEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  public class PopupEditorElement : PopupEditorBaseElement
  {
    private int defaultItemsCountInDropDown = 6;
    private int dropDownHeight = 106;
    private string autoCompleteValueMember = "";
    private string autoCompleteDisplayMember = "";
    internal const int DefaultDropDownHeight = 106;
    internal const int DefaultItemsCountForSuggest = 10;
    internal const int DefaultDropDownItems = 8;
    private RadListElement listElement;
    private RadEditorPopupControlBase popup;
    private Size dropDownMinSize;
    private Size dropDownMaxSize;
    private object autoCompleteDataSource;
    private int maxDropDownItems;

    [DefaultValue(8)]
    [Description("Gets or sets the maximum number of items to be shown in the drop-down portion of the ComboBox. ")]
    [Category("Behavior")]
    public int MaxDropDownItems
    {
      get
      {
        return this.maxDropDownItems;
      }
      set
      {
        this.maxDropDownItems = value;
      }
    }

    public object AutoCompleteDataSource
    {
      get
      {
        return this.autoCompleteDataSource;
      }
      set
      {
        if (this.autoCompleteDataSource == value)
          return;
        this.autoCompleteDataSource = value;
        this.OnAutoCompeleteDataSourceChanged();
      }
    }

    [DefaultValue("")]
    public string AutoCompleteValueMember
    {
      get
      {
        return this.autoCompleteValueMember;
      }
      set
      {
        this.autoCompleteValueMember = value;
        this.OnAutoCompeleteDataSourceChanged();
      }
    }

    [DefaultValue("")]
    public string AutoCompleteDisplayMember
    {
      get
      {
        return this.autoCompleteDisplayMember;
      }
      set
      {
        this.autoCompleteDisplayMember = value;
        this.OnAutoCompeleteDataSourceChanged();
      }
    }

    [Browsable(true)]
    [DefaultValue(106)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    [Description("Gets or sets the height in pixels of the drop-down portion of the ComboBox.")]
    public int DropDownHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.dropDownHeight, this.DpiScaleFactor);
      }
      set
      {
        this.dropDownHeight = value;
      }
    }

    public RadEditorPopupControlBase Popup
    {
      get
      {
        if (this.popup == null)
        {
          this.popup = (RadEditorPopupControlBase) this.CreatePopupForm();
          this.popup.Height = this.DropDownHeight;
        }
        return this.popup;
      }
      set
      {
        this.popup = value;
      }
    }

    [DefaultValue(6)]
    public int DefaultItemsCountInDropDown
    {
      get
      {
        return this.defaultItemsCountInDropDown;
      }
      set
      {
        this.defaultItemsCountInDropDown = value;
      }
    }

    public RadListElement ListElement
    {
      get
      {
        return this.listElement;
      }
      set
      {
        this.listElement = value;
      }
    }

    [Description("Gets or sets the drop down maximum size.")]
    [Browsable(true)]
    [DefaultValue(typeof (Size), "0,0")]
    [Category("Appearance")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.dropDownMaxSize;
      }
      set
      {
        if (!(this.dropDownMaxSize != value))
          return;
        this.dropDownMaxSize = value;
        if (!(this.dropDownMinSize != Size.Empty))
          return;
        if (this.dropDownMaxSize.Width < this.dropDownMinSize.Width)
          this.dropDownMinSize.Width = this.dropDownMaxSize.Width;
        if (this.dropDownMaxSize.Height >= this.dropDownMinSize.Height)
          return;
        this.dropDownMinSize.Height = this.dropDownMaxSize.Height;
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the drop down minimum size.")]
    [Browsable(true)]
    public Size DropDownMinSize
    {
      get
      {
        return this.dropDownMinSize;
      }
      set
      {
        if (!(this.dropDownMinSize != value))
          return;
        this.dropDownMinSize = value;
        if (!(this.dropDownMaxSize != Size.Empty))
          return;
        if (this.dropDownMinSize.Width > this.dropDownMaxSize.Width)
          this.dropDownMaxSize.Width = this.dropDownMinSize.Width;
        if (this.dropDownMinSize.Height <= this.dropDownMaxSize.Height)
          return;
        this.dropDownMaxSize.Height = this.dropDownMinSize.Height;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      BindingContext bindingContext = this.BindingContext;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.listElement = new RadListElement();
      this.WireEvents();
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    protected virtual void WireEvents()
    {
      this.listElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listElement_SelectedIndexChanged);
      this.listElement.SelectedIndexChanging += new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.listElement_SelectedIndexChanging);
      this.listElement.SelectedValueChanged += new EventHandler(this.listElement_SelectedValueChanged);
      this.listElement.ItemDataBinding += new ListItemDataBindingEventHandler(this.listElement_ItemDataBinding);
      this.listElement.ItemDataBound += new ListItemDataBoundEventHandler(this.listElement_ItemDataBound);
      this.listElement.CreatingVisualItem += new CreatingVisualListItemEventHandler(this.listElement_CreatingVisualItem);
      this.listElement.SortStyleChanged += new SortStyleChangedEventHandler(this.listElement_SortStyleChanged);
      this.listElement.VisualItemFormatting += new VisualListItemFormattingEventHandler(this.listElement_VisualItemFormatting);
      this.listElement.DataItemPropertyChanged += new RadPropertyChangedEventHandler(this.listElement_DataItemPropertyChanged);
      this.listElement.ItemsChanged += new NotifyCollectionChangedEventHandler(this.listElement_ItemsChanged);
      this.PopupOpened += new EventHandler(this.OnPopupOpened);
      this.PopupClosed += new RadPopupClosedEventHandler(this.OnPopupClosed);
    }

    protected virtual void UnwireEvents()
    {
      this.listElement.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listElement_SelectedIndexChanged);
      this.listElement.SelectedIndexChanging -= new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.listElement_SelectedIndexChanging);
      this.listElement.SelectedValueChanged -= new EventHandler(this.listElement_SelectedValueChanged);
      this.listElement.ItemDataBinding -= new ListItemDataBindingEventHandler(this.listElement_ItemDataBinding);
      this.listElement.ItemDataBound -= new ListItemDataBoundEventHandler(this.listElement_ItemDataBound);
      this.listElement.CreatingVisualItem -= new CreatingVisualListItemEventHandler(this.listElement_CreatingVisualItem);
      this.listElement.SortStyleChanged -= new SortStyleChangedEventHandler(this.listElement_SortStyleChanged);
      this.listElement.VisualItemFormatting -= new VisualListItemFormattingEventHandler(this.listElement_VisualItemFormatting);
      this.listElement.DataItemPropertyChanged -= new RadPropertyChangedEventHandler(this.listElement_DataItemPropertyChanged);
      this.listElement.ItemsChanged -= new NotifyCollectionChangedEventHandler(this.listElement_ItemsChanged);
      this.PopupOpened -= new EventHandler(this.OnPopupOpened);
      this.PopupClosed -= new RadPopupClosedEventHandler(this.OnPopupClosed);
    }

    protected virtual void OnAutoCompeleteDataSourceChanged()
    {
    }

    private void listElement_ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        if (this.listElement.Items.Count != 0)
          return;
        this.NotifyOwner(new PopupEditorNotificationData()
        {
          notificationContext = PopupEditorNotificationData.Context.ItemsClear
        });
      }
      else
        this.NotifyOwner(new PopupEditorNotificationData()
        {
          notificationContext = PopupEditorNotificationData.Context.ItemsChanged
        });
    }

    protected virtual void listElement_DataItemPropertyChanged(
      object sender,
      RadPropertyChangedEventArgs e)
    {
    }

    protected virtual void OnPopupClosed(object sender, RadPopupClosedEventArgs args)
    {
    }

    protected virtual void OnPopupOpened(object sender, EventArgs e)
    {
    }

    private void listElement_SelectedValueChanged(object sender, EventArgs e)
    {
      this.NotifyOwner(new PopupEditorNotificationData((Telerik.WinControls.UI.Data.ValueChangedEventArgs) e));
    }

    private void listElement_SelectedIndexChanging(object sender, PositionChangingCancelEventArgs e)
    {
      this.NotifyOwner(new PopupEditorNotificationData(e));
    }

    private void listElement_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.NotifyOwner(new PopupEditorNotificationData(e));
    }

    private void listElement_ItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      this.NotifyOwner(new PopupEditorNotificationData(args));
    }

    private void listElement_ItemDataBound(object sender, ListItemDataBoundEventArgs args)
    {
      this.NotifyOwner(new PopupEditorNotificationData(args));
    }

    private void listElement_CreatingVisualItem(object sender, CreatingVisualListItemEventArgs args)
    {
      this.NotifyOwner(new PopupEditorNotificationData(args));
    }

    private void listElement_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
    {
      this.NotifyOwner(new PopupEditorNotificationData(args));
    }

    private void listElement_SortStyleChanged(object sender, SortStyleChangedEventArgs args)
    {
      this.NotifyOwner(new PopupEditorNotificationData(args));
    }

    public virtual void NotifyOwner(PopupEditorNotificationData notificationData)
    {
    }

    protected virtual void SetDropDownBindingContext()
    {
      this.popup.BindingContext = this.BindingContext;
    }

    protected override Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      RadSizablePopupControl sizablePopupControl = popup as RadSizablePopupControl;
      int num1 = this.listElement.Items.Count;
      CheckedItemTraverser traverser = this.listElement.Scroller.Traverser as CheckedItemTraverser;
      if (traverser != null && traverser.CheckAll)
        ++num1;
      if (num1 > this.defaultItemsCountInDropDown || num1 == 0)
        num1 = this.defaultItemsCountInDropDown;
      if (num1 > this.maxDropDownItems && this.maxDropDownItems > 0)
        num1 = this.maxDropDownItems;
      int num2 = 0;
      if (sizablePopupControl.SizingGrip.Visibility == ElementVisibility.Visible)
      {
        RadElement sizingGrip = (RadElement) sizablePopupControl.SizingGrip;
        num2 = new Rectangle(sizingGrip.BoundingRectangle.Location, Size.Add(sizingGrip.BoundingRectangle.Size, sizingGrip.Margin.Size)).Height;
      }
      int num3 = 0;
      if (!this.listElement.AutoSizeItems)
      {
        int num4 = this.listElement.ItemHeight;
        for (int index = 0; index < num1 && index < this.listElement.Items.Count; ++index)
        {
          if (this.listElement.Items[index] is DescriptionTextListDataItem || this.listElement.IsDescriptionText)
          {
            num4 *= 2;
            break;
          }
        }
        if (this.ListElement.DpiScaleFactor != this.Popup.OwnerElement.DpiScaleFactor)
          num4 = TelerikDpiHelper.ScaleInt(num4, this.Popup.OwnerElement.DpiScaleFactor);
        int height = num1 * (num4 + this.listElement.Scroller.ItemSpacing) + num2 + (int) ((double) this.listElement.BorderWidth * 2.0);
        if (this.DropDownHeight != 106)
          height = this.DropDownHeight;
        return new Size(this.Size.Width, height);
      }
      int height1;
      if (this.listElement.Items.Count > 0)
      {
        for (int index = 0; index < num1 && index < this.listElement.Items.Count; ++index)
          num3 += (int) this.listElement.Items[index].MeasuredSize.Height + this.listElement.Scroller.ItemSpacing;
        height1 = num3 + this.listElement.Scroller.ItemSpacing + num2 + this.listElement.HScrollBar.Size.Height + (int) ((double) this.listElement.BorderWidth * 2.0);
      }
      else
        height1 = num1 * (this.listElement.ItemHeight + this.listElement.Scroller.ItemSpacing) + num2 + (int) ((double) this.listElement.BorderWidth * 2.0);
      if (this.DropDownHeight != 106)
        height1 = this.DropDownHeight;
      return new Size(this.Size.Width, height1);
    }

    public Size GetDesiredPopupSize()
    {
      return this.GetPopupSize((RadPopupControlBase) this.popup, true);
    }

    protected override void UpdatePopupMinMaxSize(RadPopupControlBase popup)
    {
      if (this.DropDownMinSize != Size.Empty)
        popup.MinimumSize = this.DropDownMinSize;
      if (this.DropDownMaxSize != Size.Empty)
        popup.MaximumSize = this.DropDownMaxSize;
      if (!(popup.MinimumSize == Size.Empty))
        return;
      RadSizablePopupControl sizablePopupControl = popup as RadSizablePopupControl;
      RadElement sizingGrip = (RadElement) sizablePopupControl.SizingGrip;
      Rectangle rectangle = new Rectangle(sizingGrip.BoundingRectangle.Location, Size.Add(sizingGrip.BoundingRectangle.Size, sizingGrip.Margin.Size));
      float num = (float) ((double) this.listElement.BorderWidth * 2.0 + (this.listElement.VScrollBar.Visibility == ElementVisibility.Visible ? (double) (this.listElement.VScrollBar.FirstButton.Size.Height + this.listElement.VScrollBar.SecondButton.Size.Height) : 0.0) + (sizablePopupControl.SizingGrip.Visibility == ElementVisibility.Collapsed ? 0.0 : (double) rectangle.Height));
      if (this.maxDropDownItems != 0)
        num = 0.0f;
      popup.Size = new Size(0, (int) Math.Ceiling(this.listElement.Items.Count == 1 ? 0.0 : (double) num));
    }
  }
}
