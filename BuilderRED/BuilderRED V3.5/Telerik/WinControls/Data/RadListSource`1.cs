// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.RadListSource`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls.Data
{
  public class RadListSource<TDataItem> : IList<TDataItem>, ICollection<TDataItem>, IEnumerable<TDataItem>, IList, ICollection, IEnumerable, ITypedList, ICancelAddNew, INotifyCollectionChanged, INotifyPropertyChanged, ICurrencyManagerProvider, IDisposable
    where TDataItem : IDataItem
  {
    private bool processListChanged = true;
    private bool createATransactionForEveryValueSetting = true;
    private string dataMember = string.Empty;
    private List<TDataItem> items = new List<TDataItem>(128);
    private PropertyDescriptorCollection boundProperties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
    private int version;
    private int update;
    private bool skipListChangedForItem;
    private bool propertyDescriptorChanged;
    private object dataSource;
    private IDataItemSource source;
    private CurrencyManager currencyManager;
    private RadCollectionView<TDataItem> collectionView;
    private BindingContext bindingContext;
    private Dictionary<string, RadListSource<TDataItem>> relatedBindingSources;
    private ConstructorInfo itemConstructor;
    private System.Type itemType;
    private IDataItem skippedItem;
    private bool bindingContextChange;
    private bool useCaseSensitiveFieldNames;

    public event EventHandler PositionChanged;

    public RadListSource()
      : this((IDataItemSource) null)
    {
    }

    public RadListSource(IDataItemSource source)
      : this(source, (RadCollectionView<TDataItem>) null)
    {
      this.collectionView = this.CreateDefaultCollectionView();
      if (this.collectionView == null)
        return;
      this.collectionView.CurrentChanged += new EventHandler(this.collectionView_CurrentChanged);
    }

    public RadListSource(IDataItemSource source, RadCollectionView<TDataItem> collectionView)
    {
      this.currencyManager = (CurrencyManager) null;
      this.boundProperties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
      this.collectionView = collectionView;
      if (this.collectionView == null)
        this.collectionView = (RadCollectionView<TDataItem>) new EmptyCollectionView<TDataItem>((IEnumerable<TDataItem>) this);
      this.collectionView.CurrentChanged += new EventHandler(this.collectionView_CurrentChanged);
      if (source == null)
      {
        this.bindingContext = new BindingContext();
      }
      else
      {
        this.source = source;
        this.bindingContext = this.source.BindingContext;
        this.source.BindingContextChanged += new EventHandler(this.source_BindingContextChanged);
      }
    }

    public int Position
    {
      get
      {
        if (this.IsDataBound)
          return this.currencyManager.Position;
        return this.IndexOf(this.CollectionView.CurrentItem);
      }
      set
      {
        if (value < 0)
          throw new IndexOutOfRangeException("Invalid index.");
        if (value >= this.Count)
          return;
        this.CollectionView.MoveCurrentTo(this[value]);
      }
    }

    public TDataItem Current
    {
      get
      {
        return this.CollectionView.CurrentItem;
      }
    }

    protected List<TDataItem> Items
    {
      get
      {
        return this.items;
      }
      set
      {
        this.items = value;
      }
    }

    public bool UseCaseSensitiveFieldNames
    {
      get
      {
        return this.useCaseSensitiveFieldNames;
      }
      set
      {
        this.useCaseSensitiveFieldNames = value;
      }
    }

    public bool CreateATransactionForEveryValueSetting
    {
      get
      {
        return this.createATransactionForEveryValueSetting;
      }
      set
      {
        this.createATransactionForEveryValueSetting = value;
      }
    }

    protected virtual RadCollectionView<TDataItem> CreateDefaultCollectionView()
    {
      return (RadCollectionView<TDataItem>) new RadDataView<TDataItem>((IEnumerable<TDataItem>) this);
    }

    public virtual void Refresh()
    {
      if (this.update != 0)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public virtual void Reset()
    {
      this.Initialize();
    }

    public void BeginUpdate()
    {
      ++this.update;
      this.collectionView.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public void EndUpdate(bool notifyUpdates)
    {
      --this.update;
      this.collectionView.EndUpdate(false);
      if (this.update < 0)
      {
        this.update = 0;
      }
      else
      {
        if (this.update != 0 || !notifyUpdates)
          return;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }
    }

    public virtual TDataItem AddNew()
    {
      if (this.IsDataBound)
      {
        if (this.currencyManager.List is IBindingList)
        {
          if (this.currencyManager.List is ICancelAddNew)
            this.skipListChangedForItem = true;
          this.currencyManager.AddNew();
          if (this.currencyManager.List.Count > this.Count)
            this.InsertItem(this.currencyManager.List.Count - 1, default (TDataItem));
          this.skipListChangedForItem = false;
          return this[this.Count - 1];
        }
        if ((object) this.itemConstructor == null)
          throw new InvalidOperationException(string.Format("RadListSource needs a parameterless Constructor for {0}", new object[1]{ (object) this.itemType == null ? (object) "(null)" : (object) this.itemType.FullName }));
        this.currencyManager.List.Add(this.itemConstructor.Invoke((object[]) null));
      }
      IDataItem dataItem = this.source.NewItem();
      this.InsertItem(this.Count, (TDataItem) dataItem);
      return (TDataItem) dataItem;
    }

    public virtual TDataItem AddNew(TDataItem item)
    {
      if (this.IsDataBound)
      {
        if (this.currencyManager.List is IBindingList)
        {
          this.skipListChangedForItem = true;
          this.currencyManager.AddNew();
          if (this.currencyManager.List.Count > this.Count)
            this.InsertItem(this.currencyManager.List.Count - 1, item);
          this.skipListChangedForItem = false;
          return this[this.Count - 1];
        }
        if ((object) this.itemConstructor == null)
          throw new InvalidOperationException(string.Format("RadListSource need a parameterless Constructor for {0}", new object[1]{ (object) this.itemType == null ? (object) "(null)" : (object) this.itemType.FullName }));
        this.currencyManager.List.Add(this.itemConstructor.Invoke((object[]) null));
      }
      this.InsertItem(this.Count, item);
      return item;
    }

    public void Move(int oldIndex, int newIndex)
    {
      if (this.IsDataBound)
        throw new InvalidOperationException("Items cannot be moved to the RadListSource when is in data-bound mode");
      this.MoveItem(oldIndex, newIndex);
    }

    public RadCollectionView<TDataItem> CollectionView
    {
      get
      {
        return this.collectionView;
      }
    }

    protected virtual void InsertItem(int index, TDataItem item)
    {
      if ((object) item == null)
        item = (TDataItem) this.source.NewItem();
      if (this.IsDataBound && index < this.currencyManager.List.Count)
        this.InitializeBoundRow(item, this.currencyManager.List[index]);
      if (this.IsDataBound && index > this.currencyManager.List.Count)
        index = this.currencyManager.List.Count;
      this.items.Insert(index, item);
      ++this.version;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item, index));
    }

    protected virtual void SetItem(int index, TDataItem item)
    {
      IDataItem dataItem = (IDataItem) this[index];
      if (this.IsDataBound)
        this.InitializeBoundRow(item, this.currencyManager.List[index]);
      this.items[index] = item;
      ++this.version;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (object) item, (object) dataItem, index));
    }

    protected virtual void ClearItems()
    {
      this.ClearItemsCore();
      ++this.version;
      this.OnNotifyPropertyChanged("Count");
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    protected virtual void ClearItemsCore()
    {
      this.items.Clear();
    }

    protected virtual void RemoveItem(int index)
    {
      TDataItem dataItem = this[index];
      this.items.RemoveAt(index);
      ++this.version;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) dataItem, -1, index));
    }

    protected virtual void MoveItem(int oldIndex, int newIndex)
    {
      TDataItem dataItem = this[oldIndex];
      this.items.RemoveAt(oldIndex);
      this.items.Insert(newIndex, dataItem);
      ++this.version;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, (object) dataItem, newIndex, oldIndex));
    }

    protected virtual void ChangeItem(int index, TDataItem item, string propertyName)
    {
      if ((object) item == null)
      {
        if (this.IsDataBound)
        {
          object dataBoundItem = this.currencyManager.List[index];
          if (string.IsNullOrEmpty(propertyName))
          {
            if (index < this.Count)
            {
              item = this[index];
              this.InitializeBoundRow(item, dataBoundItem);
            }
          }
          else
          {
            item = this.collectionView.Find(index, dataBoundItem);
            if ((object) item == null && index < this.Count)
            {
              item = this[index];
              this.InitializeBoundRow(item, dataBoundItem);
            }
          }
        }
        else
          item = this.items[index];
      }
      if ((object) item == null)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) item, (object) item, index, propertyName));
    }

    public void NotifyItemChanging(TDataItem item)
    {
      this.skipListChangedForItem = true;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanging, (object) item));
    }

    public void NotifyItemChanged(TDataItem item)
    {
      this.skipListChangedForItem = false;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) item));
    }

    public void NotifyItemChanging(TDataItem item, string propertyName)
    {
      this.skipListChangedForItem = true;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanging, (object) item)
      {
        PropertyName = propertyName
      });
    }

    public void NotifyItemChanged(TDataItem item, string propertyName)
    {
      this.skipListChangedForItem = false;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) item)
      {
        PropertyName = propertyName
      });
    }

    private void collectionView_CurrentChanged(object sender, EventArgs e)
    {
      if (!this.IsDataBound || (object) this.CollectionView.CurrentItem == null || this.currencyManager.Position >= 0 && this.currencyManager.Position < this.Count && this[this.currencyManager.Position].Equals((object) this.CollectionView.CurrentItem))
        return;
      int num = this.IndexOf(this.CollectionView.CurrentItem);
      if (num < 0)
        return;
      DataRowView dataBoundItem = this.CollectionView.CurrentItem.DataBoundItem as DataRowView;
      if (dataBoundItem != null && dataBoundItem.IsNew)
        return;
      this.currencyManager.Position = num;
    }

    private void source_BindingContextChanged(object sender, EventArgs e)
    {
      this.bindingContextChange = true;
      this.bindingContext = this.source.BindingContext;
      try
      {
        if (this.dataSource != null)
          this.Bind(this.DataSource, this.DataMember);
      }
      catch (ArgumentException ex)
      {
        this.DataMember = string.Empty;
      }
      this.bindingContextChange = false;
    }

    public object GetBoundValue(object dataBoundItem, string propertyName)
    {
      if (!this.IsDataBound)
        return (object) null;
      PropertyDescriptor propertyDescriptor = this.boundProperties.Find(propertyName, !this.UseCaseSensitiveFieldNames);
      if (propertyDescriptor == null)
        throw new ArgumentException("There is no property descriptor corresponding to property name: " + propertyName);
      object obj = propertyDescriptor.GetValue(dataBoundItem);
      if (propertyDescriptor.PropertyType.IsEnum)
        obj = Enum.ToObject(propertyDescriptor.PropertyType, obj);
      return obj;
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.boundProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    private void SetSubPropertyValue(string propertyPath, object dataObject, object value)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      if (innerDescriptor == null)
        return;
      IEditableObject editableObject = innerObject as IEditableObject;
      editableObject?.BeginEdit();
      innerDescriptor.SetValue(innerObject, value);
      editableObject?.EndEdit();
    }

    public bool SetBoundValue(IDataItem dataItem, string propertyName, object value)
    {
      return this.SetBoundValue(dataItem, propertyName, value, (string) null);
    }

    public bool SetBoundValue(IDataItem dataItem, string propertyName, object value, string path)
    {
      return this.SetBoundValue(dataItem, propertyName, propertyName, value, path);
    }

    public bool SetBoundValue(
      IDataItem dataItem,
      string propertyName,
      string columnName,
      object value,
      string path)
    {
      if (!this.IsDataBound)
        return false;
      object dataBoundItem = dataItem.DataBoundItem;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanging, (object) dataItem, (object) dataItem, -1, columnName));
      this.skipListChangedForItem = true;
      this.skippedItem = dataItem;
      if (string.IsNullOrEmpty(path))
      {
        IEditableObject editableObject = dataBoundItem as IEditableObject;
        if (editableObject != null && this.CreateATransactionForEveryValueSetting)
          editableObject.BeginEdit();
        PropertyDescriptor propertyDescriptor = this.boundProperties.Find(propertyName, !this.UseCaseSensitiveFieldNames);
        if (value == null)
        {
          object obj = propertyDescriptor.GetValue(dataBoundItem);
          if (obj != DBNull.Value)
          {
            if (obj != null)
            {
              try
              {
                propertyDescriptor.SetValue(dataBoundItem, value);
              }
              catch
              {
                propertyDescriptor.SetValue(dataBoundItem, (object) DBNull.Value);
              }
            }
          }
        }
        else
        {
          System.Type underlyingType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType);
          if (propertyDescriptor.Converter != null && (object) underlyingType != null && underlyingType.IsGenericType)
            value = propertyDescriptor.Converter.ConvertFromInvariantString(value.ToString());
          propertyDescriptor.SetValue(dataBoundItem, value);
        }
        if (editableObject != null && this.CreateATransactionForEveryValueSetting)
          editableObject.EndEdit();
      }
      else
        this.SetSubPropertyValue(path, dataBoundItem, value);
      int index = this.IndexOf((TDataItem) dataItem);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) dataItem, (object) dataItem, index, columnName));
      this.skipListChangedForItem = false;
      this.skippedItem = (IDataItem) null;
      return true;
    }

    [Description("Gets or sets the name of the list or table in the data source for which the GridViewTemplate is displaying data. ")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Browsable(true)]
    [DefaultValue("")]
    public string DataMember
    {
      get
      {
        return this.dataMember;
      }
      set
      {
        if (!(value != this.dataMember))
          return;
        this.Bind(this.dataSource, value);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (DataMember)));
      }
    }

    [Category("Data")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the data source that the GridViewTemplate is displaying data for.")]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    public object DataSource
    {
      get
      {
        return this.dataSource;
      }
      set
      {
        if (value == this.dataSource)
          return;
        if (this.ShouldChangeDataMember(value))
          this.dataMember = string.Empty;
        this.Bind(value, this.dataMember);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (DataSource)));
      }
    }

    private void Bind(object dataSource, string dataMember)
    {
      this.dataSource = dataSource;
      this.dataMember = dataMember;
      this.UnWireEvents();
      CurrencyManager currencyManager = this.currencyManager;
      if (this.bindingContext != null && this.dataSource != null && this.dataSource != Convert.DBNull)
      {
        ISupportInitializeNotification dataSource1 = this.dataSource as ISupportInitializeNotification;
        if (dataSource1 != null && !dataSource1.IsInitialized)
        {
          dataSource1.Initialized += new EventHandler(this.notification_Initialized);
          this.currencyManager = (CurrencyManager) null;
        }
        else
        {
          this.currencyManager = this.bindingContext[this.dataSource, this.dataMember] as CurrencyManager;
          this.BindToEnumerable();
          if (this.currencyManager != null)
          {
            this.itemType = ListBindingHelper.GetListItemType((object) this.currencyManager.List);
            this.itemConstructor = this.itemType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
          }
        }
      }
      else
      {
        this.currencyManager = (CurrencyManager) null;
        if (currencyManager != null && currencyManager.List != null && currencyManager.Position >= currencyManager.List.Count)
          currencyManager.Position = -1;
      }
      this.WireEvents();
      if (!this.Rebuild(currencyManager) || currencyManager == this.currencyManager)
        return;
      this.Initialize();
    }

    private bool Rebuild(CurrencyManager cm)
    {
      return !this.bindingContextChange || cm == null || (this.currencyManager == null || cm.List != this.currencyManager.List) || (cm.Count <= 0 || cm.Count != this.currencyManager.List.Count);
    }

    private void BindToEnumerable()
    {
      if (this.currencyManager != null && !this.IsObjectArray(this.dataSource) || !(this.dataSource is IEnumerable))
        return;
      List<object> objectList = new List<object>((int) byte.MaxValue);
      foreach (object obj in this.dataSource as IEnumerable)
        objectList.Add(obj);
      this.currencyManager = this.bindingContext[(object) new ReadOnlyCollection<object>((IList<object>) objectList), (string) null] as CurrencyManager;
    }

    private bool IsObjectArray(object parameter)
    {
      if (parameter == null)
        return false;
      System.Type type = parameter.GetType();
      if (type.IsArray)
        return (object) type.GetElementType() == (object) typeof (object);
      return false;
    }

    private void notification_Initialized(object sender, EventArgs e)
    {
      ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
      if (dataSource != null)
        dataSource.Initialized -= new EventHandler(this.notification_Initialized);
      this.Bind(this.dataSource, this.dataMember);
    }

    private void InitializeBoundRows()
    {
      if (this.currencyManager == null)
        return;
      this.BeginUpdate();
      this.collectionView.MoveCurrentToPosition(-1);
      this.ClearItemsCore();
      this.items.Capacity = this.currencyManager.List.Count;
      for (int index = 0; index < this.currencyManager.List.Count; ++index)
      {
        TDataItem dataItem = (TDataItem) this.source.NewItem();
        this.InitializeBoundRow(dataItem, this.currencyManager.List[index]);
        this.items.Add(dataItem);
      }
      this.EndUpdate();
    }

    protected virtual void InitializeBoundRow(TDataItem item, object dataBoundItem)
    {
      item.DataBoundItem = dataBoundItem;
    }

    private void Initialize()
    {
      bool flag = false;
      this.BeginUpdate();
      this.collectionView.MoveCurrentToPosition(-1);
      if (this.currencyManager != null)
      {
        this.boundProperties = this.BindToInterface(this.currencyManager);
        if (this.boundProperties == null && (object) this.itemType != null && ((object) this.itemType != (object) typeof (object) && (object) this.itemType.GetInterface("ICustomTypeDescriptor") == null) && Environment.OSVersion.Version.Major <= 5)
          this.boundProperties = TypeDescriptor.GetProperties(this.itemType);
        if (this.boundProperties == null)
          this.boundProperties = this.currencyManager.GetItemProperties();
        flag = this.currencyManager.List.Count == 0;
        this.InitializeBoundRows();
      }
      else
      {
        this.ClearItems();
        this.boundProperties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
      }
      if (flag)
        this.EndUpdate(false);
      if (this.source != null)
        this.source.Initialize();
      if (flag)
        this.BeginUpdate();
      this.EndUpdate();
      this.InitializeCurrentItem();
      if (this.source == null)
        return;
      this.source.BindingComplete();
    }

    private PropertyDescriptorCollection BindToInterface(
      CurrencyManager currencyManager)
    {
      System.Type listItemType = ListBindingHelper.GetListItemType((object) this.currencyManager.List);
      if ((object) listItemType != null && listItemType.IsInterface && (!(listItemType is ITypedList) && this.currencyManager.List.Count > 0))
      {
        if (this.currencyManager.List.Count == 1)
          return TypeDescriptor.GetProperties(this.currencyManager.List[0]);
        bool flag = true;
        for (int index = 1; index < this.currencyManager.List.Count; ++index)
        {
          if ((object) this.currencyManager.List[0].GetType() != (object) this.currencyManager.List[index].GetType())
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return TypeDescriptor.GetProperties(this.currencyManager.List[0]);
      }
      return (PropertyDescriptorCollection) null;
    }

    private void InitializeCurrentItem()
    {
      if (this.currencyManager == null || this.currencyManager.Position == -1 || this.currencyManager.Position >= this.Count)
        this.collectionView.MoveCurrentToPosition(-1);
      else
        this.collectionView.MoveCurrentTo(this[this.currencyManager.Position]);
    }

    protected virtual void WireEvents()
    {
      if (this.currencyManager == null)
        return;
      this.currencyManager.PositionChanged += new EventHandler(this.currencyManager_PositionChanged);
      this.currencyManager.ListChanged += new ListChangedEventHandler(this.currencyManager_ListChanged);
    }

    protected virtual void UnWireEvents()
    {
      if (this.currencyManager == null)
        return;
      this.currencyManager.PositionChanged -= new EventHandler(this.currencyManager_PositionChanged);
      this.currencyManager.ListChanged -= new ListChangedEventHandler(this.currencyManager_ListChanged);
    }

    private void currencyManager_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (!this.processListChanged)
        return;
      switch (e.ListChangedType)
      {
        case ListChangedType.Reset:
          this.InitializeBoundRows();
          this.InitializeCurrentItem();
          if (!this.propertyDescriptorChanged)
            break;
          this.propertyDescriptorChanged = false;
          if (this.source == null)
            break;
          this.source.BindingComplete();
          break;
        case ListChangedType.ItemAdded:
          if (this.skipListChangedForItem)
            break;
          if (e.NewIndex >= 0 && e.NewIndex < this.currencyManager.Count && this.currencyManager.List.Count == this.Count)
          {
            if (this.items[e.NewIndex].DataBoundItem == this.currencyManager.List[e.NewIndex])
            {
              this.ChangeItem(e.NewIndex, this.items[e.NewIndex], (string) null);
              break;
            }
            this.items[e.NewIndex].DataBoundItem = this.currencyManager.List[e.NewIndex];
            break;
          }
          this.InsertItem(e.NewIndex, default (TDataItem));
          break;
        case ListChangedType.ItemDeleted:
          this.RemoveItem(e.NewIndex);
          break;
        case ListChangedType.ItemMoved:
          this.MoveItem(e.OldIndex, e.NewIndex);
          break;
        case ListChangedType.ItemChanged:
          if (this.skipListChangedForItem)
          {
            object obj = this.currencyManager.List[e.NewIndex];
            if (this.skippedItem == null || this.skippedItem.DataBoundItem == obj)
              break;
          }
          string propertyName = e.PropertyDescriptor != null ? e.PropertyDescriptor.Name : string.Empty;
          this.ChangeItem(e.NewIndex, default (TDataItem), propertyName);
          break;
        case ListChangedType.PropertyDescriptorAdded:
        case ListChangedType.PropertyDescriptorDeleted:
        case ListChangedType.PropertyDescriptorChanged:
          if (e.PropertyDescriptor == null)
          {
            this.Initialize();
            this.propertyDescriptorChanged = true;
            break;
          }
          if (this.source == null)
            break;
          this.boundProperties = this.currencyManager.GetItemProperties();
          this.source.MetadataChanged(e.PropertyDescriptor);
          break;
      }
    }

    public bool IsDataBound
    {
      get
      {
        if (this.dataSource != null)
          return this.currencyManager != null;
        return false;
      }
    }

    private void currencyManager_PositionChanged(object sender, EventArgs e)
    {
      if (this.PositionChanged != null)
        this.PositionChanged((object) this, e);
      if (this.currencyManager.Position < 0 || this.currencyManager.Position >= this.Count)
        return;
      TDataItem dataItem = this[this.currencyManager.Position];
      if (dataItem.Equals((object) this.Current))
        return;
      this.CollectionView.MoveCurrentTo(dataItem);
    }

    private bool ShouldChangeDataMember(object newDataSource)
    {
      if (this.bindingContext == null)
        return false;
      if (newDataSource != null)
      {
        CurrencyManager currencyManager = this.bindingContext[newDataSource] as CurrencyManager;
        if (currencyManager == null)
          return false;
        PropertyDescriptorCollection itemProperties = currencyManager.GetItemProperties();
        if (this.dataMember != null && this.dataMember.Length != 0 && itemProperties[this.dataMember] != null)
          return false;
      }
      return true;
    }

    public PropertyDescriptorCollection BoundProperties
    {
      get
      {
        return this.boundProperties;
      }
    }

    public PropertyDescriptorCollection GetItemProperties(
      PropertyDescriptor[] listAccessors)
    {
      object list = ListBindingHelper.GetList(this.dataSource);
      if (list is ITypedList && !string.IsNullOrEmpty(this.dataMember))
        return ListBindingHelper.GetListItemProperties(list, this.dataMember, listAccessors);
      return ListBindingHelper.GetListItemProperties((object) this.items, listAccessors);
    }

    public string GetListName(PropertyDescriptor[] listAccessors)
    {
      return ListBindingHelper.GetListName((object) this, listAccessors);
    }

    void ICancelAddNew.CancelNew(int position)
    {
      if (this.currencyManager != null)
      {
        ICancelAddNew list = this.currencyManager.List as ICancelAddNew;
        if (list != null)
        {
          list.CancelNew(position);
          return;
        }
      }
      this.RemoveAt(position);
    }

    void ICancelAddNew.EndNew(int position)
    {
      if (this.currencyManager != null)
        (this.currencyManager.List as ICancelAddNew)?.EndNew(position);
      if (position < 0 || position >= this.Count)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) this[position], position));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.update != 0 || this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, e);
    }

    CurrencyManager ICurrencyManagerProvider.CurrencyManager
    {
      get
      {
        return this.currencyManager;
      }
    }

    CurrencyManager ICurrencyManagerProvider.GetRelatedCurrencyManager(
      string dataMember)
    {
      if (string.IsNullOrEmpty(dataMember))
        return this.currencyManager;
      if (dataMember.IndexOf(".") != -1)
        return (CurrencyManager) null;
      return ((ICurrencyManagerProvider) this.GetRelatedBindingSource(dataMember)).CurrencyManager;
    }

    private RadListSource<TDataItem> GetRelatedBindingSource(string dataMember)
    {
      if (this.relatedBindingSources == null)
        this.relatedBindingSources = new Dictionary<string, RadListSource<TDataItem>>();
      foreach (string key in this.relatedBindingSources.Keys)
      {
        if (string.Equals(key, dataMember, StringComparison.OrdinalIgnoreCase))
          return this.relatedBindingSources[key];
      }
      RadListSource<TDataItem> radListSource = new RadListSource<TDataItem>(this.source);
      radListSource.bindingContext = this.bindingContext;
      radListSource.Bind(this.DataSource, dataMember);
      this.relatedBindingSources[dataMember] = radListSource;
      return radListSource;
    }

    public int IndexOf(TDataItem item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, TDataItem item)
    {
      if (this.IsDataBound)
        throw new InvalidOperationException("Items cannot be inserted to the RadListSource when is in data-bound mode");
      this.InsertItem(index, item);
    }

    public void RemoveAt(int index)
    {
      if (this.IsDataBound)
      {
        this.currencyManager.List.Remove(this[index].DataBoundItem);
        if (this.currencyManager.List is IBindingList)
          return;
      }
      this.RemoveItem(index);
    }

    public TDataItem this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.SetItem(index, value);
      }
    }

    public void Add(TDataItem item)
    {
      if (this.IsDataBound)
        throw new InvalidOperationException("Items cannot be added to the RadListSource when is in data-bound mode");
      this.InsertItem(this.Count, item);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AddUnboundInternal(TDataItem item)
    {
      if ((object) item == null)
        return;
      this.items.Insert(this.Count, item);
      ++this.version;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item, this.Count));
    }

    public void Clear()
    {
      if (this.IsDataBound)
      {
        this.currencyManager.List.Clear();
        if (this.currencyManager.List is IBindingList)
          return;
      }
      this.ClearItems();
    }

    public bool Contains(TDataItem item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(TDataItem[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        if (this.currencyManager != null)
          return this.currencyManager.List.IsReadOnly;
        return false;
      }
    }

    public bool IsUpdating
    {
      get
      {
        return this.update > 0;
      }
    }

    public bool Remove(TDataItem item)
    {
      if (this.IndexOf(item) < 0)
        return false;
      if (this.IsDataBound)
      {
        int count = this.currencyManager.List.Count;
        (item.DataBoundItem as IEditableObject)?.EndEdit();
        this.currencyManager.List.Remove(item.DataBoundItem);
        if (this.currencyManager.List is IBindingList)
          return count != this.currencyManager.List.Count;
      }
      int index = this.IndexOf(item);
      if (index >= 0)
        this.RemoveItem(index);
      return true;
    }

    int IList.Add(object value)
    {
      if (!(value is TDataItem))
        throw new ArgumentException(string.Format("Invalid value type"));
      this.Add((TDataItem) value);
      return this.items.IndexOf((TDataItem) value);
    }

    bool IList.Contains(object value)
    {
      if (value is TDataItem)
        return this.items.Contains((TDataItem) value);
      return false;
    }

    int IList.IndexOf(object value)
    {
      if (value is TDataItem)
        return this.items.IndexOf((TDataItem) value);
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      if (!(value is TDataItem))
        throw new ArgumentException(string.Format("Invalid value type"));
      this.Insert(index, (TDataItem) value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        if (this.currencyManager != null)
          return this.currencyManager.List.IsFixedSize;
        return false;
      }
    }

    void IList.Remove(object value)
    {
      if (!(value is TDataItem))
        throw new ArgumentException(string.Format("Invalid value type"));
      this.Remove((TDataItem) value);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this.items[index];
      }
      set
      {
        if (!(value is TDataItem))
          throw new ArgumentException(string.Format("Invalid value type"));
        this.items[index] = (TDataItem) value;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection) this.items)?.CopyTo(array, index);
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return ((ICollection) this.items).IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this.items).SyncRoot;
      }
    }

    public IEnumerator<TDataItem> GetEnumerator()
    {
      return (IEnumerator<TDataItem>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    public void Dispose()
    {
      this.UnWireEvents();
      this.currencyManager = (CurrencyManager) null;
    }
  }
}
