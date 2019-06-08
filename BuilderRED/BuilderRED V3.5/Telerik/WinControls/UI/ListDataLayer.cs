// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListDataLayer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  public class ListDataLayer : IDataItemSource, IDisposable
  {
    private string descriptionTextMember = "";
    private string checkedMember = "";
    private TypeConverter stringConverter = TypeDescriptor.GetConverter(typeof (string));
    private BindingContext bindingContext;
    private RadListSource<RadListDataItem> listSource;
    private string displayMember;
    private string valueMember;
    private RadListDataItemCollection items;
    private RadListElement owner;
    private TypeConverter dataBoundItemConverter;
    private bool updateDataItemConverter;

    public ListDataLayer(RadListElement owner)
    {
      this.owner = owner;
      this.listSource = (RadListSource<RadListDataItem>) this.CreateListSource();
      this.items = this.CreateItemsCollection(owner);
      this.listSource.CollectionView.GroupDescriptors.Add("GroupKey", ListSortDirection.Ascending);
      this.WireEvents();
    }

    protected virtual void WireEvents()
    {
      this.listSource.CollectionView.CurrentChanging += new CancelEventHandler(this.CollectionView_CurrentChanging);
      this.listSource.CollectionView.CurrentChanged += new EventHandler(this.CollectionView_CurrentChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.listSource.CollectionView.CurrentChanging -= new CancelEventHandler(this.CollectionView_CurrentChanging);
      this.listSource.CollectionView.CurrentChanged -= new EventHandler(this.CollectionView_CurrentChanged);
    }

    protected virtual ListControlListSource CreateListSource()
    {
      return new ListControlListSource((IDataItemSource) this);
    }

    protected virtual RadListDataItemCollection CreateItemsCollection(
      RadListElement owner)
    {
      return new RadListDataItemCollection(this, owner);
    }

    public void Dispose()
    {
      this.UnwireEvents();
      this.listSource.Dispose();
    }

    public bool ChangeCurrentOnAdd
    {
      get
      {
        return this.listSource.CollectionView.ChangeCurrentOnAdd;
      }
      set
      {
        this.listSource.CollectionView.ChangeCurrentOnAdd = value;
      }
    }

    public RadListSource<RadListDataItem> ListSource
    {
      get
      {
        return this.listSource;
      }
    }

    public string DataMember
    {
      get
      {
        return this.listSource.DataMember;
      }
      set
      {
        this.listSource.DataMember = value;
      }
    }

    public object DataSource
    {
      get
      {
        return this.listSource.DataSource;
      }
      set
      {
        this.dataBoundItemConverter = (TypeConverter) null;
        this.listSource.DataSource = value;
      }
    }

    public string DisplayMember
    {
      get
      {
        return this.displayMember;
      }
      set
      {
        this.displayMember = value;
        this.updateDataItemConverter = true;
        if (this.DataSource == null)
          return;
        this.UpdateDataItemsChachedText();
        this.listSource.CollectionView.LazyRefresh();
      }
    }

    public string DescriptionTextMember
    {
      get
      {
        return this.descriptionTextMember;
      }
      set
      {
        this.descriptionTextMember = value;
        this.updateDataItemConverter = true;
        if (this.DataSource == null)
          return;
        this.UpdateDataItemsChachedText();
        this.listSource.CollectionView.LazyRefresh();
      }
    }

    public string CheckedMember
    {
      get
      {
        return this.checkedMember;
      }
      set
      {
        this.checkedMember = value;
        this.updateDataItemConverter = true;
        if (this.DataSource == null)
          return;
        this.UpdateCheckedDataItems();
        this.listSource.CollectionView.LazyRefresh();
      }
    }

    private void UpdateCheckedDataItems()
    {
      foreach (RadListDataItem radListDataItem in this.Items)
      {
        radListDataItem.CachedText = this.GetUnformattedValue(radListDataItem);
        RadCheckedListDataItem checkedListDataItem = radListDataItem as RadCheckedListDataItem;
        if (checkedListDataItem != null)
        {
          string str = this.GetCheckedValue(checkedListDataItem).ToString();
          if (string.IsNullOrEmpty(str))
            str = "False";
          checkedListDataItem.Checked = Convert.ToBoolean(str);
        }
      }
    }

    private void UpdateDataItemsChachedText()
    {
      foreach (RadListDataItem radListDataItem in this.Items)
        radListDataItem.CachedText = this.GetUnformattedValue(radListDataItem);
    }

    public string ValueMember
    {
      get
      {
        return this.valueMember;
      }
      set
      {
        this.valueMember = value;
      }
    }

    public RadListDataItem CurrentItem
    {
      get
      {
        if (this.listSource.CollectionView.CurrentPosition < this.listSource.CollectionView.Count)
          return this.listSource.CollectionView[this.listSource.CollectionView.CurrentPosition];
        return (RadListDataItem) null;
      }
      set
      {
        this.listSource.Position = this.listSource.CollectionView.IndexOf(value);
      }
    }

    public int CurrentPosition
    {
      get
      {
        if (this.listSource.CollectionView.CurrentPosition < this.listSource.CollectionView.Count)
          return this.DataView.CurrentPosition;
        return -1;
      }
      set
      {
        if (this.DataView.CurrentPosition == value)
          return;
        this.DataView.MoveCurrentTo(this.DataView[value]);
      }
    }

    public RadCollectionView<RadListDataItem> DataView
    {
      get
      {
        return this.listSource.CollectionView;
      }
    }

    public RadListDataItemCollection Items
    {
      get
      {
        return this.items;
      }
      internal set
      {
        this.items = value;
      }
    }

    public RadListElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler CurrentPositionChanged;

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler CurrentPositionChanging;

    public event EventHandler Initialized;

    protected virtual void CollectionView_CurrentChanged(object sender, EventArgs e)
    {
      this.OnCurrentPositionChanged(this.listSource.CollectionView.CurrentPosition);
    }

    protected virtual void CollectionView_CurrentChanging(object sender, CancelEventArgs e)
    {
      e.Cancel = this.OnCurrentPositionChanging(-1);
    }

    public void Refresh()
    {
      this.ListSource.Refresh();
    }

    public object GetDisplayValue(RadListDataItem item)
    {
      if (string.IsNullOrEmpty(this.displayMember))
        return (object) this.GetFormattedValue(item.DataBoundItem);
      object component = (object) null;
      try
      {
        component = this.displayMember.Split('.').Length <= 1 ? this.listSource.GetBoundValue(item.DataBoundItem, this.displayMember) : this.GetSubPropertyValue(this.displayMember, item.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        component = item.DataBoundItem;
      }
      catch (RowNotInTableException ex)
      {
        object dataSource = this.DataSource;
        this.DataSource = (object) null;
        this.DataSource = dataSource;
      }
      if (component == null)
        component = (object) "";
      if (this.dataBoundItemConverter == null || this.updateDataItemConverter)
      {
        this.dataBoundItemConverter = TypeDescriptor.GetConverter(component);
        this.updateDataItemConverter = false;
      }
      return (object) this.GetFormattedValue(component, this.dataBoundItemConverter);
    }

    public object GetDescriptionTextValue(RadListDataItem item)
    {
      if (string.IsNullOrEmpty(this.descriptionTextMember))
        return (object) this.GetFormattedValue(item.DataBoundItem);
      object component;
      try
      {
        component = this.descriptionTextMember.Split('.').Length <= 1 ? this.listSource.GetBoundValue(item.DataBoundItem, this.descriptionTextMember) : this.GetSubPropertyValue(this.descriptionTextMember, item.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        component = item.DataBoundItem;
        this.DisplayMember = "";
        this.ValueMember = "";
        this.DescriptionTextMember = "";
      }
      if (component == null)
        component = (object) "";
      return (object) this.GetFormattedValue(component, TypeDescriptor.GetConverter(component));
    }

    internal object GetCheckedValue(RadCheckedListDataItem item)
    {
      if (string.IsNullOrEmpty(this.checkedMember))
        return (object) false;
      object component;
      try
      {
        component = this.checkedMember.Split('.').Length <= 1 ? this.listSource.GetBoundValue(item.DataBoundItem, this.checkedMember) : this.GetSubPropertyValue(this.checkedMember, item.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        component = item.DataBoundItem;
        this.DisplayMember = "";
        this.ValueMember = "";
        this.CheckedMember = "";
      }
      if (component == null)
        component = (object) "";
      return (object) this.GetFormattedValue(component, TypeDescriptor.GetConverter(component));
    }

    public string GetUnformattedValue(RadListDataItem item)
    {
      if (this.displayMember == "")
      {
        if (item.DataBoundItem == null)
          return "no data to display";
        return item.DataBoundItem.ToString();
      }
      object obj;
      try
      {
        obj = this.displayMember.Split('.').Length <= 1 ? this.listSource.GetBoundValue(item.DataBoundItem, this.displayMember) : this.GetSubPropertyValue(this.displayMember, item.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        obj = item.DataBoundItem;
        this.DisplayMember = "";
        this.ValueMember = "";
      }
      if (obj == null)
        return "";
      return obj.ToString() ?? "";
    }

    public object GetValue(RadListDataItem item)
    {
      object obj;
      try
      {
        obj = this.valueMember.Split('.').Length <= 1 ? this.listSource.GetBoundValue(item.DataBoundItem, this.valueMember) : this.GetSubPropertyValue(this.valueMember, item.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        obj = item.DataBoundItem;
        this.DisplayMember = "";
        this.ValueMember = "";
      }
      return obj;
    }

    public int GetRowIndex(RadListDataItem item)
    {
      if (!this.owner.ShowGroups)
        return this.listSource.CollectionView.IndexOf(item);
      int num = 0;
      foreach (ListGroup group in (ReadOnlyCollection<Group<RadListDataItem>>) this.owner.Groups)
      {
        if (!group.Collapsed)
        {
          foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) group.GetItems())
          {
            if (item.Equals((object) radListDataItem))
              return num;
            ++num;
          }
        }
      }
      return -1;
    }

    public RadListDataItem GetItemAtIndex(int index)
    {
      if (this.owner.ShowGroups)
      {
        int num = 0;
        foreach (ListGroup group in (ReadOnlyCollection<Group<RadListDataItem>>) this.owner.Groups)
        {
          if (!group.Collapsed)
          {
            foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) group.GetItems())
            {
              if (num == index)
                return radListDataItem;
              ++num;
            }
          }
        }
      }
      return this.Items[index];
    }

    public int GetVisibleItemsCount()
    {
      if (!this.owner.ShowGroups)
        return this.Items.Count;
      int num = 0;
      foreach (ListGroup group in (ReadOnlyCollection<Group<RadListDataItem>>) this.owner.Groups)
      {
        if (!group.Collapsed)
          num += group.GetItems().Count;
      }
      return num;
    }

    public List<RadListDataItem> GetItemRange(int startIndex, int endIndex)
    {
      List<RadListDataItem> radListDataItemList = new List<RadListDataItem>();
      if (this.owner.ShowGroups)
      {
        int num = 0;
        foreach (Group<RadListDataItem> group in (ReadOnlyCollection<Group<RadListDataItem>>) this.DataView.Groups)
        {
          foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) group.GetItems())
          {
            if (startIndex <= num && num <= endIndex)
              radListDataItemList.Add(radListDataItem);
            ++num;
          }
        }
      }
      else
      {
        for (int index = startIndex; index <= endIndex; ++index)
          radListDataItemList.Add(this.DataView[index]);
      }
      return radListDataItemList;
    }

    protected virtual void OnCurrentPositionChanged(int newPosition)
    {
      if (this.CurrentPositionChanged == null)
        return;
      this.CurrentPositionChanged((object) this, new Telerik.WinControls.UI.Data.PositionChangedEventArgs(newPosition));
    }

    protected virtual bool OnCurrentPositionChanging(int newPosition)
    {
      if (this.CurrentPositionChanging == null)
        return false;
      PositionChangingCancelEventArgs e = new PositionChangingCancelEventArgs(newPosition);
      this.CurrentPositionChanging((object) this, e);
      return e.Cancel;
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.ListSource.BoundProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    internal object GetSubPropertyValue(string propertyPath, object dataObject)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      return innerDescriptor?.GetValue(innerObject);
    }

    private string GetFormattedValue(object value, TypeConverter valueConverter)
    {
      string str = "";
      if (this.owner.FormattingEnabled)
        str = (string) Telerik.WinControls.Formatter.FormatObject(value, typeof (string), valueConverter, this.stringConverter, this.owner.FormatString, this.owner.FormatInfo, (object) null, (object) null);
      else if (valueConverter != null)
      {
        if (valueConverter.CanConvertTo(typeof (string)))
          str = valueConverter.ConvertToString(value);
        else if (value != null)
          str = value.ToString();
      }
      else if (value != null)
        str = value.ToString();
      return str;
    }

    private string GetFormattedValue(object value)
    {
      if (this.dataBoundItemConverter == null || this.updateDataItemConverter)
      {
        this.dataBoundItemConverter = TypeDescriptor.GetConverter(value);
        this.updateDataItemConverter = false;
      }
      return this.GetFormattedValue(value, this.dataBoundItemConverter);
    }

    public IDataItem NewItem()
    {
      RadListDataItem radListDataItem = this.owner.OnListItemDataBinding() ?? (!(this.items is RadCheckedListDataItemCollection) ? (!string.IsNullOrEmpty(this.descriptionTextMember) ? (RadListDataItem) new DescriptionTextListDataItem() : new RadListDataItem()) : (!string.IsNullOrEmpty(this.descriptionTextMember) ? (RadListDataItem) new DescriptionTextCheckedListDataItem() : (RadListDataItem) new RadCheckedListDataItem()));
      radListDataItem.DataLayer = this;
      radListDataItem.Owner = this.owner;
      return (IDataItem) radListDataItem;
    }

    void IDataItemSource.BindingComplete()
    {
    }

    void IDataItemSource.MetadataChanged(PropertyDescriptor pd)
    {
    }

    public void Initialize()
    {
      if (this.Initialized == null)
        return;
      this.Initialized((object) this, EventArgs.Empty);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BindingContext BindingContext
    {
      get
      {
        return this.bindingContext;
      }
      internal set
      {
        if (this.bindingContext == value)
          return;
        this.bindingContext = value;
        if (this.BindingContextChanged == null)
          return;
        this.BindingContextChanged((object) this, EventArgs.Empty);
      }
    }

    public event EventHandler BindingContextChanged;
  }
}
