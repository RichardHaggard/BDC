// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedDropDownListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class RadCheckedDropDownListElement : RadDropDownListElement
  {
    private int minLineHeight = 14;
    private bool syncSelectionWithText = true;
    protected const int DefaultMinLineHeight = 14;
    private RadCheckedDropDownListEditableAreaElement autoCompleteEditableAreaElement;
    private DropDownCheckedItemsCollection checkedItems;
    private int updateCount;
    private bool shouldUpdate;
    private CheckAllDataItem checkAllItem;
    private bool showCheckAllItems;
    private CheckedItemTraverser checkedItemTraverser;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkedItems = new DropDownCheckedItemsCollection(this);
      RadCheckedListDataItemCollection dataItemCollection = new RadCheckedListDataItemCollection(this, this.ListElement.DataLayer, this.ListElement);
      this.checkAllItem = new CheckAllDataItem("Check All", this);
      this.checkAllItem.Owner = this.ListElement;
      this.ListElement.DataLayer.Items = (RadListDataItemCollection) dataItemCollection;
      this.ListElement.Items = (IList<RadListDataItem>) dataItemCollection;
      this.checkedItemTraverser = new CheckedItemTraverser((IList<RadListDataItem>) dataItemCollection, this.checkAllItem, this.ShowCheckAllItems);
      this.ListElement.ViewElement.DataProvider = (IEnumerable) this.checkedItemTraverser;
      this.ListElement.Scroller.Traverser = (ITraverser<RadListDataItem>) this.checkedItemTraverser;
      this.ListElement.SelectionMode = SelectionMode.One;
      this.ListElement.DataLayer.DataView.CollectionChanged += new NotifyCollectionChangedEventHandler(this.DataView_CollectionChanged);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteDataSource = (object) dataItemCollection;
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      this.ListElement.ViewElement.ElementProvider = (IVirtualizedElementProvider<RadListDataItem>) new CheckedDropDownListElementProvider(this.ListElement);
      this.WireAutoCompleteEvents();
      this.Children[2].MinSize = new Size(0, 18);
    }

    protected virtual void WireAutoCompleteEvents()
    {
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.TextChanged += new EventHandler(this.AutoCompleteTextBox_TextChanged);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.TokenValidating += new TokenValidatingEventHandler(this.AutoCompleteTextBox_TokenValidating);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.LayoutUpdated += new EventHandler(this.AutoCompleteTextBox_LayoutUpdated);
    }

    protected virtual void UnwireAutoCompleteEvents()
    {
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.TextChanged -= new EventHandler(this.AutoCompleteTextBox_TextChanged);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.TokenValidating -= new TokenValidatingEventHandler(this.AutoCompleteTextBox_TokenValidating);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.LayoutUpdated -= new EventHandler(this.AutoCompleteTextBox_LayoutUpdated);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.ViewElement.Padding = new Padding(1);
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.DrawBorder = false;
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.BorderThickness = new Padding(0);
    }

    protected override void DisposeManagedResources()
    {
      this.ListElement.DataLayer.DataView.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.DataView_CollectionChanged);
      this.UnwireAutoCompleteEvents();
      base.DisposeManagedResources();
    }

    [Bindable(true)]
    [RadDescription("SelectedValue", typeof (RadListElement))]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override object SelectedValue
    {
      get
      {
        if (this.CheckedItems.Count > 0)
          return this.CheckedItems[0].Value;
        return base.SelectedValue;
      }
      set
      {
        for (int newIndex = 0; newIndex < this.Items.Count; ++newIndex)
        {
          if (this.Items[newIndex].Value == null && value == null || this.Items[newIndex].Value != null && this.Items[newIndex].Value.Equals(value))
          {
            this.Items[newIndex].Checked = true;
            this.OnSelectedValueChanged((object) this, new Telerik.WinControls.UI.Data.ValueChangedEventArgs(newIndex, (object) true, (object) false));
          }
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("This property is not applicable for RadCheckedDropDownList")]
    public override AutoCompleteAppendHelper AutoCompleteAppend
    {
      get
      {
        return base.AutoCompleteAppend;
      }
      set
      {
        base.AutoCompleteAppend = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("This property is not applicable for RadCheckedDropDownList")]
    public override AutoCompleteSuggestHelper AutoCompleteSuggest
    {
      get
      {
        return base.AutoCompleteSuggest;
      }
      set
      {
        base.AutoCompleteSuggest = value;
      }
    }

    public bool ShowCheckAllItems
    {
      get
      {
        return this.showCheckAllItems;
      }
      set
      {
        this.showCheckAllItems = value;
        this.checkedItemTraverser.CheckAll = value;
        this.ListElement.ViewElement.Children.Clear();
        this.ListElement.Scroller.UpdateScrollRange();
        this.ListElement.UpdateLayout();
        if (!value)
          return;
        this.CheckAllItem.SetCheckState(this.Items.Count == this.CheckedItems.Count && this.Items.Count > 0, true);
      }
    }

    public CheckAllDataItem CheckAllItem
    {
      get
      {
        return this.checkAllItem;
      }
      set
      {
        this.checkAllItem = value;
      }
    }

    [DefaultValue(AutoCompleteMode.SuggestAppend)]
    [Description("Specifies the mode for the automatic completion feature used in the CheckedDropDownList and TextBox controls.")]
    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public override AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteMode;
      }
      set
      {
        this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteMode = value;
      }
    }

    [DefaultValue(14)]
    public int MinLineHeight
    {
      get
      {
        return this.minLineHeight;
      }
      set
      {
        this.minLineHeight = value;
      }
    }

    public DropDownCheckedItemsCollection CheckedItems
    {
      get
      {
        return this.checkedItems;
      }
    }

    public RadCheckedDropDownListEditableAreaElement AutoCompleteEditableAreaElement
    {
      get
      {
        return this.autoCompleteEditableAreaElement;
      }
      set
      {
        this.autoCompleteEditableAreaElement = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the string through which the SelectedValue property will be determined. This property can not be set to null. Setting it to null will cause it to contain an empty string.")]
    public string CheckedMember
    {
      get
      {
        return this.ListElement.CheckedMember;
      }
      set
      {
        this.ListElement.CheckedMember = value;
      }
    }

    public override string EditableElementText
    {
      get
      {
        return this.autoCompleteEditableAreaElement.AutoCompleteTextBox.Text;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          this.autoCompleteEditableAreaElement.AutoCompleteTextBox.Clear();
        else
          this.autoCompleteEditableAreaElement.AutoCompleteTextBox.Text = value;
      }
    }

    [Editor("Telerik.WinControls.Design.RadCheckedDropDownListCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Description("Gets a collection representing the items contained in this RadDropDownList.")]
    public RadCheckedListDataItemCollection Items
    {
      get
      {
        return (RadCheckedListDataItemCollection) this.ListElement.Items;
      }
    }

    [DefaultValue(true)]
    public override bool SyncSelectionWithText
    {
      get
      {
        return this.syncSelectionWithText;
      }
      set
      {
        this.syncSelectionWithText = value;
      }
    }

    protected override void OnItemsClear()
    {
      this.CheckedItems.Reset();
      base.OnItemsClear();
    }

    private void AutoCompleteTextBox_LayoutUpdated(object sender, EventArgs e)
    {
      if (!this.shouldUpdate)
        return;
      this.shouldUpdate = false;
      this.SyncEditorElementWithSelectedItem();
    }

    private void AutoCompleteTextBox_TokenValidating(object sender, TokenValidatingEventArgs e)
    {
      if (this.IsUpdating())
        return;
      foreach (RadListDataItem checkedItem in (ReadOnlyCollection<RadCheckedListDataItem>) this.CheckedItems)
      {
        if (checkedItem.Text == e.Text)
        {
          e.IsValidToken = false;
          this.shouldUpdate = true;
          return;
        }
      }
      foreach (RadCheckedListDataItem checkedListDataItem in (RadListDataItemCollection) this.Items)
      {
        if (checkedListDataItem.Text == e.Text && !checkedListDataItem.Checked)
          return;
      }
      this.shouldUpdate = true;
      e.IsValidToken = false;
    }

    protected override RadDropDownListEditableAreaElement CreateTextEditorElement()
    {
      this.autoCompleteEditableAreaElement = new RadCheckedDropDownListEditableAreaElement(this);
      return (RadDropDownListEditableAreaElement) this.autoCompleteEditableAreaElement;
    }

    protected internal override bool CanClosePopUp(RadPopupCloseReason reason, MouseButtons buttons)
    {
      if (!this.PopupForm.Bounds.Contains(Cursor.Position))
        return true;
      return reason != RadPopupCloseReason.Mouse;
    }

    private void DataView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.IsUpdating())
        return;
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteDataSource = (object) null;
      if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
        this.checkedItems.Reset();
      this.SyncEditorElementWithSelectedItem();
      this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteDataSource = (object) this.Items;
    }

    private List<RadCheckedListDataItem> GetItemsNonCheckedItems()
    {
      List<RadCheckedListDataItem> checkedListDataItemList = new List<RadCheckedListDataItem>(this.Items.Count);
      foreach (RadCheckedListDataItem checkedListDataItem in (RadListDataItemCollection) this.Items)
      {
        if (!this.checkedItems.Contains(checkedListDataItem))
          checkedListDataItemList.Add(checkedListDataItem);
      }
      return checkedListDataItemList;
    }

    protected internal virtual bool OnItemCheckedChanging(RadCheckedListDataItemCancelEventArgs args)
    {
      if (this.ItemCheckedChanging != null)
        this.ItemCheckedChanging((object) this, args);
      return args.Cancel;
    }

    protected internal virtual void OnItemCheckedChanged(RadCheckedListDataItemEventArgs args)
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ItemCheckedChanged", (object) null);
      if (this.ItemCheckedChanged == null)
        return;
      this.ItemCheckedChanged((object) this, args);
    }

    public event RadCheckedListDataItemCancelEventHandler ItemCheckedChanging;

    public event RadCheckedListDataItemEventHandler ItemCheckedChanged;

    public event RadCheckedListDataItemCancelEventHandler CheckAllItemCheckedChanging
    {
      add
      {
        this.checkAllItem.CheckAllItemCheckedChanging += value;
      }
      remove
      {
        this.checkAllItem.CheckAllItemCheckedChanging -= value;
      }
    }

    public event RadCheckedListDataItemEventHandler CheckAllItemCheckedChanged
    {
      add
      {
        this.checkAllItem.CheckAllItemCheckedChanged += value;
      }
      remove
      {
        this.checkAllItem.CheckAllItemCheckedChanged -= value;
      }
    }

    public override object DataSource
    {
      get
      {
        return base.DataSource;
      }
      set
      {
        this.CheckedItems.Clear();
        base.DataSource = value;
        this.SyncEditorElementWithSelectedItem();
      }
    }

    public override string Text
    {
      get
      {
        return this.EditableElementText;
      }
      set
      {
        this.EditableElementText = value;
      }
    }

    protected override void SyncVisualProperties(RadListDataItem listItem)
    {
      if (listItem == null)
      {
        int num1 = (int) this.EditableElement.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.Local);
        int num2 = (int) this.EditableElement.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
        int num3 = (int) this.EditableElement.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
        int num4 = (int) this.EditableElement.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
      }
      else
      {
        if (!listItem.Selected || this.ListElement.SuspendSelectionEvents)
          return;
        this.EditableElement.Image = listItem.Image;
        this.EditableElement.ImageAlignment = listItem.ImageAlignment;
        this.EditableElement.TextAlignment = listItem.TextAlignment;
        this.EditableElement.TextImageRelation = listItem.TextImageRelation;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadDropDownListElement);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = availableSize;
      if (this.ElementTree.RootElement != null && (this.ElementTree.RootElement.ControlDefaultSize.Height != 0 && !this.AutoCompleteEditableAreaElement.AutoCompleteTextBox.Multiline))
        availableSize1.Height = (float) this.ElementTree.RootElement.ControlDefaultSize.Height;
      return base.MeasureOverride(availableSize1);
    }

    protected internal override void EnterPressedOrLeaveControl()
    {
    }

    private void AutoCompleteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.IsUpdating() || !this.SyncSelectionWithText)
        return;
      bool flag1 = false;
      this.BeginUpdate();
      TextBoxChangedEventArgs changedEventArgs = e as TextBoxChangedEventArgs;
      if (changedEventArgs.Action == TextBoxChangeAction.TextEdit || changedEventArgs.Action == TextBoxChangeAction.TextPropertyChange)
      {
        RadTokenizedTextItemCollection items = this.autoCompleteEditableAreaElement.AutoCompleteTextBox.Items;
        foreach (RadCheckedListDataItem checkedListDataItem in (RadListDataItemCollection) this.Items)
        {
          bool flag2 = false;
          foreach (RadTokenizedTextItem tokenizedTextItem in (ReadOnlyCollection<RadTokenizedTextItem>) items)
          {
            flag2 |= tokenizedTextItem.Text == checkedListDataItem.CachedText;
            if (flag2)
              break;
          }
          flag1 |= checkedListDataItem.Checked != flag2;
          checkedListDataItem.Checked = flag2;
        }
        this.autoCompleteEditableAreaElement.AutoCompleteTextBox.AutoCompleteDataSource = (object) this.Items;
      }
      this.EndUpdate();
      if (!flag1)
        return;
      this.SyncEditorElementWithSelectedItem();
    }

    private bool FilterMethod(RadListDataItem itemToFilter)
    {
      foreach (RadListDataItem checkedItem in (ReadOnlyCollection<RadCheckedListDataItem>) this.checkedItems)
      {
        if (checkedItem.CachedText == itemToFilter.CachedText)
          return false;
      }
      return true;
    }

    protected override void OnSelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ArrowButton == null || this.ArrowButton.Shape == null)
        return;
      this.ArrowButton.Shape.IsRightToLeft = newValue;
    }

    internal override void SyncEditorElementWithSelectedItem()
    {
      if (this.IsUpdating())
        return;
      this.BeginUpdate();
      StringBuilder stringBuilder = new StringBuilder();
      char delimiter = this.autoCompleteEditableAreaElement.AutoCompleteTextBox.Delimiter;
      foreach (RadCheckedListDataItem radCheckedListDataItem in (RadListDataItemCollection) this.Items)
        this.ProcessItem(radCheckedListDataItem);
      foreach (RadCheckedListDataItem checkedItem in (ReadOnlyCollection<RadCheckedListDataItem>) this.checkedItems)
        stringBuilder.Append(checkedItem.CachedText).Append(delimiter);
      if (this.EditableElementText != stringBuilder.ToString())
        this.EditableElementText = stringBuilder.ToString();
      this.EndUpdate();
    }

    internal void ProcessItem(RadCheckedListDataItem radCheckedListDataItem)
    {
      this.checkedItems.ProcessCheckedItem(radCheckedListDataItem);
      if (this.IsUpdating())
        return;
      this.SyncEditorElementWithSelectedItem();
    }

    protected override void ScrollToItemFromText(string text)
    {
    }

    public override void BeginUpdate()
    {
      ++this.updateCount;
    }

    public override void EndUpdate()
    {
      if (this.updateCount <= 0)
        return;
      --this.updateCount;
    }

    public override bool IsUpdating()
    {
      if (this.updateCount <= 0)
        return this.ListElement.IsUpdating;
      return true;
    }

    [Description("Gets or sets a value indicating whether the drop down list is read only.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool ReadOnly
    {
      get
      {
        return this.ListElement.ReadOnly;
      }
      set
      {
        this.ListElement.ReadOnly = value;
        this.AutoCompleteEditableAreaElement.AutoCompleteTextBox.IsReadOnly = value;
        foreach (RadElement child in this.ListElement.ViewElement.Children)
        {
          RadCheckedListVisualItem checkedListVisualItem = child as RadCheckedListVisualItem;
          if (checkedListVisualItem != null)
            checkedListVisualItem.CheckBox.ReadOnly = value;
        }
      }
    }
  }
}
