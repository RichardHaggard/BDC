// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.RadCollectionView`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public abstract class RadCollectionView<TDataItem> : ICollectionView<TDataItem>, IPagedCollectionView, IReadOnlyCollection<TDataItem>, IEnumerable<TDataItem>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    where TDataItem : IDataItem
  {
    private static bool? isDesignMode = new bool?();
    private int position = -1;
    private int currentItemHashCode = -1;
    private int pageSize = 20;
    private readonly object syncobjs = new object();
    private Dictionary<int, ExpressionNode> threadedFilterNodes = new Dictionary<int, ExpressionNode>();
    private bool changeCurrentOnAdd = true;
    private int update;
    private bool caseSensitive;
    private bool positionChangedInUpdate;
    private bool pagingBeforeGrouping;
    private string filterExpression;
    private string prevExpression;
    private int currentPageIndex;
    private bool isPageChanging;
    private StringCollection filterContext;
    private ExpressionNode filterNode;
    internal int mainThreadId;
    private SortDescriptorCollection sortDescriptors;
    private GroupDescriptorCollection groupDescriptors;
    private Predicate<TDataItem> filter;
    private bool bypassSort;
    private bool bypassFilter;
    private IEnumerable<TDataItem> sourceCollection;
    private CollectionResetReason resetReason;
    private int version;
    private IGroupFactory<TDataItem> groupFactory;
    private ISortDescriptorCollectionFactory sortDescriptorCollectionFactory;
    private IGroupDescriptorCollectionFactory groupDescriptorCollectionFactory;

    public RadCollectionView(IEnumerable<TDataItem> collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this.InitializeFields();
      this.InitializeSource(collection);
    }

    internal RadCollectionView()
    {
      this.InitializeFields();
    }

    [Browsable(false)]
    public virtual IComparer<TDataItem> Comparer
    {
      get
      {
        return this as IComparer<TDataItem>;
      }
      set
      {
      }
    }

    [Browsable(false)]
    public virtual IComparer<Group<TDataItem>> GroupComparer
    {
      get
      {
        return (IComparer<Group<TDataItem>>) null;
      }
      set
      {
      }
    }

    public bool ChangeCurrentOnAdd
    {
      get
      {
        return this.changeCurrentOnAdd;
      }
      set
      {
        this.changeCurrentOnAdd = value;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.Items.Count == 0;
      }
    }

    public bool IsDynamic
    {
      get
      {
        return this.sourceCollection is INotifyCollectionChanged;
      }
    }

    public int Count
    {
      get
      {
        return this.Items.Count;
      }
    }

    public TDataItem this[int index]
    {
      get
      {
        return this.Items[index];
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool CaseSensitive
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        if (this.caseSensitive == value)
          return;
        this.caseSensitive = value;
        if (!string.IsNullOrEmpty(this.filterExpression))
        {
          this.filterNode = ExpressionParser.Parse(this.filterExpression, this.CaseSensitive);
          this.filterContext.Clear();
          this.ClearThreadedFilterNodes();
          foreach (NameNode node in ExpressionNode.GetNodes<NameNode>(this.filterNode))
          {
            if (!this.filterContext.Contains(node.Name))
              this.filterContext.Add(node.Name);
          }
          this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("FilterExpression"));
        }
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (CaseSensitive)));
      }
    }

    public virtual string FilterExpression
    {
      get
      {
        return this.filterExpression;
      }
      set
      {
        if (!(this.filterExpression != value))
          return;
        this.filterExpression = value;
        this.filterNode = ExpressionParser.Parse(this.filterExpression, this.CaseSensitive);
        this.filterContext.Clear();
        this.ClearThreadedFilterNodes();
        if (string.IsNullOrEmpty(this.filterExpression))
        {
          if (this.filter == new Predicate<TDataItem>(this.PerformExpressionFilter))
            this.filter = (Predicate<TDataItem>) null;
          this.OnNotifyPropertyChanged(nameof (FilterExpression));
        }
        else
        {
          foreach (NameNode node in ExpressionNode.GetNodes<NameNode>(this.filterNode))
          {
            if (!this.filterContext.Contains(node.Name))
              this.filterContext.Add(node.Name);
          }
          if (this.filter == null)
            this.filter = new Predicate<TDataItem>(this.PerformExpressionFilter);
          this.OnNotifyPropertyChanged(nameof (FilterExpression));
        }
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [Category("Behavior")]
    public bool BypassFilter
    {
      get
      {
        return this.bypassFilter;
      }
      set
      {
        if (this.bypassFilter == value)
          return;
        this.bypassFilter = value;
        this.OnNotifyPropertyChanged(nameof (BypassFilter));
      }
    }

    [Browsable(false)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool BypassSort
    {
      get
      {
        return this.bypassSort;
      }
      set
      {
        if (this.bypassSort == value)
          return;
        this.bypassSort = value;
        this.OnNotifyPropertyChanged(nameof (BypassSort));
      }
    }

    public virtual bool PassesFilter(TDataItem item)
    {
      Predicate<TDataItem> filter = this.Filter;
      if (filter != null && !this.BypassFilter)
        return filter(item);
      return true;
    }

    public void BeginUpdate()
    {
      ++this.update;
    }

    public void EndUpdate(bool notifyUpdates)
    {
      if (this.update == 0)
        return;
      --this.update;
      if (this.update > 0)
        return;
      if (notifyUpdates)
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      if (!notifyUpdates || !this.positionChangedInUpdate)
        return;
      this.OnCurrentChanged((EventArgs) new CurrentChangedEventArgs(CurrentChangeReason.EndUpdate));
      this.positionChangedInUpdate = false;
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new RadCollectionView<TDataItem>.DeferHelper(this);
    }

    public void CopyTo(TDataItem[] array, int arrayIndex)
    {
      this.Items.CopyTo(array, arrayIndex);
    }

    public virtual void LoadData(IEnumerable<TDataItem> collection)
    {
      if (this.sourceCollection == collection)
        return;
      this.InitializeSource(collection);
      this.RefreshOverride();
    }

    public virtual TDataItem Find(int itemIndex, object dataBoundItem)
    {
      if (itemIndex >= this.Count)
        return default (TDataItem);
      TDataItem dataItem1 = this[itemIndex];
      if (object.Equals(dataItem1.DataBoundItem, dataBoundItem))
        return dataItem1;
      foreach (TDataItem dataItem2 in this)
      {
        if (object.Equals(dataItem2.DataBoundItem, dataBoundItem))
          return dataItem2;
      }
      return default (TDataItem);
    }

    public Group<TDataItem> FindGroup(Group<TDataItem> group)
    {
      if (this.Groups == null || this.Groups.Count == 0)
        return (Group<TDataItem>) null;
      return this.FindGroup(group, this.Groups);
    }

    public bool ContainsGroup(Group<TDataItem> group)
    {
      return this.FindGroup(group, this.Groups) != null;
    }

    public virtual int IndexOf(TDataItem item)
    {
      return this.Items.IndexOf(item);
    }

    public bool Contains(TDataItem item)
    {
      return this.IndexOf(item) >= 0;
    }

    public bool FilterEvaluate(FilterDescriptor filterDescriptor, TDataItem item)
    {
      if (filterDescriptor == null || string.IsNullOrEmpty(filterDescriptor.Expression))
        return true;
      ExpressionContext context = new ExpressionContext();
      context.CaseSensitive = this.CaseSensitive;
      this.FillFilterContext(filterDescriptor, item, context);
      object obj = ExpressionParser.ParseNoCache(filterDescriptor.Expression, this.CaseSensitive).Eval((object) null, (object) context);
      if (obj is bool)
        return (bool) obj;
      return true;
    }

    private void FillFilterContext(
      FilterDescriptor filter,
      TDataItem item,
      ExpressionContext context)
    {
      CompositeFilterDescriptor filterDescriptor1 = filter as CompositeFilterDescriptor;
      if (filterDescriptor1 != null)
      {
        foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
          this.FillFilterContext(filterDescriptor2, item, context);
      }
      else
      {
        object obj = this.GetFieldValue(item, filter.PropertyName);
        if (obj is Enum)
          obj = (object) Convert.ToInt32(obj);
        if (context.ContainsKey(filter.PropertyName))
          context[filter.PropertyName] = obj;
        else
          context.Add(filter.PropertyName, obj);
      }
    }

    public object Evaluate(string expression, TDataItem item)
    {
      int startIndex = this.IndexOf(item);
      if (startIndex >= 0)
        return this.Evaluate(expression, startIndex, 1);
      return (object) null;
    }

    public object Evaluate(string expression, int startIndex, int count)
    {
      if (startIndex >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      return this.Evaluate(expression, this.GetItems(startIndex, count));
    }

    public object Evaluate(string expression, IEnumerable<TDataItem> items)
    {
      ExpressionNode node = ExpressionParser.Parse(expression, this.CaseSensitive);
      List<NameNode> nodes = ExpressionNode.GetNodes<NameNode>(node);
      StringCollection stringCollection = new StringCollection();
      foreach (NameNode nameNode in nodes)
      {
        if (!stringCollection.Contains(nameNode.Name))
          stringCollection.Add(nameNode.Name);
      }
      IEnumerator<TDataItem> enumerator = items.GetEnumerator();
      enumerator.MoveNext();
      ExpressionContext context = ExpressionContext.Context;
      context.Clear();
      for (int index = 0; index < stringCollection.Count; ++index)
      {
        if (!RadCollectionView<TDataItem>.isDesignMode.HasValue)
        {
          Process currentProcess = Process.GetCurrentProcess();
          RadCollectionView<TDataItem>.isDesignMode = new bool?(currentProcess.ProcessName == "devenv");
          currentProcess.Dispose();
        }
        if (RadCollectionView<TDataItem>.isDesignMode.HasValue)
        {
          if (RadCollectionView<TDataItem>.isDesignMode.Value)
          {
            try
            {
              if (context.ContainsKey(stringCollection[index]))
              {
                context[stringCollection[index]] = enumerator.Current[stringCollection[index]];
                continue;
              }
              context.Add(stringCollection[index], enumerator.Current[stringCollection[index]]);
              continue;
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show(string.Format("Error evaluating expression: {0}", (object) ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return (object) null;
            }
          }
        }
        if (context.ContainsKey(stringCollection[index]))
          context[stringCollection[index]] = enumerator.Current[stringCollection[index]];
        else
          context.Add(stringCollection[index], enumerator.Current[stringCollection[index]]);
      }
      if (ExpressionNode.GetNodes<AggregateNode>(node).Count > 0)
        return node.Eval((object) new AggregateItems<TDataItem>(items), (object) context);
      return node.Eval((object) null, (object) context);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool TryEvaluate(string expression, IEnumerable<TDataItem> items, out object result)
    {
      return this.TryEvaluate(expression, items, 0, out result);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool TryEvaluate(
      string expression,
      IEnumerable<TDataItem> items,
      int index,
      out object result)
    {
      result = (object) null;
      ExpressionNode expressionNode = (ExpressionNode) null;
      if (!ExpressionParser.TryParse(expression, this.CaseSensitive, out expressionNode))
        return false;
      List<NameNode> nodes = ExpressionNode.GetNodes<NameNode>(expressionNode);
      StringCollection stringCollection = new StringCollection();
      foreach (NameNode nameNode in nodes)
      {
        if (!stringCollection.Contains(nameNode.Name))
          stringCollection.Add(nameNode.Name);
      }
      IEnumerator<TDataItem> enumerator = items.GetEnumerator();
      for (; index >= 0; --index)
      {
        if (!enumerator.MoveNext())
          throw new IndexOutOfRangeException();
      }
      if ((object) enumerator.Current == null)
        return false;
      ExpressionContext context = ExpressionContext.Context;
      context.Clear();
      for (int index1 = 0; index1 < stringCollection.Count; ++index1)
      {
        if (enumerator.Current.IndexOf(stringCollection[index1]) < 0)
          return false;
        if (context.ContainsKey(stringCollection[index1]))
          context[stringCollection[index1]] = enumerator.Current[stringCollection[index1]];
        else
          context.Add(stringCollection[index1], enumerator.Current[stringCollection[index1]]);
      }
      try
      {
        result = ExpressionNode.GetNodes<AggregateNode>(expressionNode).Count <= 0 ? expressionNode.Eval((object) null, (object) context) : expressionNode.Eval((object) new AggregateItems<TDataItem>(items), (object) context);
      }
      catch
      {
        return false;
      }
      return true;
    }

    internal static void RebuildDescriptorIndex(
      IEnumerable<TDataItem> source,
      SortDescriptorCollection sortDescriptors,
      GroupDescriptorCollection groupDescriptors)
    {
      IEnumerator<TDataItem> enumerator = source.GetEnumerator();
      enumerator.MoveNext();
      if ((object) enumerator.Current == null)
        return;
      for (int index = 0; index < sortDescriptors.Count; ++index)
      {
        if (!string.IsNullOrEmpty(sortDescriptors[index].PropertyName))
          sortDescriptors[index].PropertyIndex = enumerator.Current.IndexOf(sortDescriptors[index].PropertyName);
      }
      for (int index1 = 0; index1 < groupDescriptors.Count; ++index1)
      {
        for (int index2 = 0; index2 < groupDescriptors[index1].GroupNames.Count; ++index2)
        {
          if (!string.IsNullOrEmpty(groupDescriptors[index1].GroupNames[index2].PropertyName))
            groupDescriptors[index1].GroupNames[index2].PropertyIndex = enumerator.Current.IndexOf(groupDescriptors[index1].GroupNames[index2].PropertyName);
        }
      }
    }

    public virtual void EnsureDescriptors()
    {
    }

    public void ClearThreadedFilterNodes()
    {
      this.threadedFilterNodes.Clear();
      ExpressionContext.ClearThreadedContexts();
    }

    private ExpressionNode GetFilterNode(int threadId)
    {
      if (!this.threadedFilterNodes.ContainsKey(threadId))
      {
        ExpressionNode noCache = ExpressionParser.ParseNoCache(this.filterExpression, this.CaseSensitive);
        lock (this.syncobjs)
          this.threadedFilterNodes.Add(threadId, noCache);
      }
      return this.threadedFilterNodes[threadId];
    }

    private bool PerformExpressionFilter(TDataItem item)
    {
      if (this.filterContext.Count == 0)
        return true;
      try
      {
        lock (this.syncobjs)
        {
          int managedThreadId = Thread.CurrentThread.ManagedThreadId;
          ExpressionContext expressionContext = managedThreadId != this.mainThreadId ? ExpressionContext.GetContext(managedThreadId) : ExpressionContext.Context;
          expressionContext.Clear();
          expressionContext.CaseSensitive = this.CaseSensitive;
          for (int index1 = 0; index1 < this.filterContext.Count; ++index1)
          {
            string index2 = this.filterContext[index1];
            object obj = this.GetFieldValue(item, index2);
            if (obj is Enum)
              obj = (object) Convert.ToInt32(obj);
            if (expressionContext.ContainsKey(index2))
              expressionContext[index2] = obj;
            else
              expressionContext.Add(index2, obj);
          }
          object obj1 = (managedThreadId != this.mainThreadId ? this.GetFilterNode(managedThreadId) : this.filterNode).Eval((object) null, (object) expressionContext);
          if (obj1 is bool)
            return (bool) obj1;
        }
      }
      catch (Exception ex)
      {
        throw new FilterExpressionException("Invalid filter expression.", ex);
      }
      return false;
    }

    protected virtual object GetFieldValue(TDataItem item, string fieldName)
    {
      return item[fieldName];
    }

    protected bool HasFilter
    {
      get
      {
        if (!this.CanFilter)
          return false;
        if (this.Filter == null)
          return !string.IsNullOrEmpty(this.FilterExpression);
        return true;
      }
    }

    protected bool IsInUpdate
    {
      get
      {
        return this.update > 0;
      }
    }

    private void EndDefer()
    {
      this.EndUpdate();
    }

    protected virtual IList<TDataItem> Items
    {
      get
      {
        return (IList<TDataItem>) null;
      }
    }

    public virtual int GetItemPage(TDataItem item)
    {
      return -1;
    }

    protected bool HasDataOperation
    {
      get
      {
        if (!this.HasFilter && !this.HasSort)
          return this.HasGroup;
        return true;
      }
    }

    public virtual IGroupFactory<TDataItem> GroupFactory
    {
      get
      {
        return this.groupFactory;
      }
      set
      {
        this.groupFactory = value;
      }
    }

    private void groupDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.resetReason = CollectionResetReason.GroupingChanged;
      this.OnNotifyPropertyChanged("GroupDescriptors");
    }

    protected bool HasGroup
    {
      get
      {
        if (this.CanGroup)
          return this.GroupDescriptors.Count > 0;
        return false;
      }
    }

    private void InitializeFields()
    {
      this.filter = (Predicate<TDataItem>) null;
      this.filterExpression = string.Empty;
      this.filterContext = new StringCollection();
      this.sortDescriptorCollectionFactory = (ISortDescriptorCollectionFactory) new DefaultSortDescriptorCollectionFactory();
      this.sortDescriptors = this.SortDescriptorCollectionFactory.CreateCollection();
      this.sortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
      this.groupFactory = (IGroupFactory<TDataItem>) new DefaultGroupFactory<TDataItem>();
      this.groupDescriptorCollectionFactory = (IGroupDescriptorCollectionFactory) new DefaultGroupDescriptorCollectionFactory();
      this.groupDescriptors = this.GroupDescriptorCollectionFactory.CreateCollection();
      this.groupDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groupDescriptors_CollectionChanged);
    }

    internal void Unload()
    {
      INotifyCollectionChanged sourceCollection = this.sourceCollection as INotifyCollectionChanged;
      if (sourceCollection != null)
        sourceCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.source_CollectionChanged);
      this.sortDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
      this.groupDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.groupDescriptors_CollectionChanged);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public int Version
    {
      get
      {
        return this.version;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void LazyRefresh()
    {
      ++this.version;
    }

    protected virtual void InitializeSource(IEnumerable<TDataItem> collection)
    {
      if (this.sourceCollection != null)
      {
        INotifyCollectionChanged sourceCollection = this.sourceCollection as INotifyCollectionChanged;
        if (sourceCollection != null)
          sourceCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.source_CollectionChanged);
      }
      this.sourceCollection = collection;
      INotifyCollectionChanged collectionChanged = collection as INotifyCollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(this.source_CollectionChanged);
    }

    private void source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Batch || e.Action == NotifyCollectionChangedAction.Reset)
        this.resetReason = CollectionResetReason.Refresh;
      this.ProcessCollectionChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ISortDescriptorCollectionFactory SortDescriptorCollectionFactory
    {
      get
      {
        return this.sortDescriptorCollectionFactory;
      }
      set
      {
        if (this.sortDescriptorCollectionFactory == value)
          return;
        this.sortDescriptorCollectionFactory = value;
        if (this.sortDescriptors != null)
          this.sortDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
        this.sortDescriptors = this.sortDescriptorCollectionFactory.CreateCollection();
        this.sortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IGroupDescriptorCollectionFactory GroupDescriptorCollectionFactory
    {
      get
      {
        return this.groupDescriptorCollectionFactory;
      }
      set
      {
        if (this.groupDescriptorCollectionFactory == value)
          return;
        this.groupDescriptorCollectionFactory = value;
        if (this.groupDescriptors != null)
          this.groupDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.groupDescriptors_CollectionChanged);
        this.groupDescriptors = this.groupDescriptorCollectionFactory.CreateCollection();
        this.groupDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groupDescriptors_CollectionChanged);
      }
    }

    private void sortDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.resetReason = CollectionResetReason.SortingChanged;
      this.OnNotifyPropertyChanged("SortDescriptors");
    }

    protected bool HasSort
    {
      get
      {
        if (this.CanSort)
          return this.SortDescriptors.Count > 0;
        return false;
      }
    }

    private Group<TDataItem> FindGroup(
      Group<TDataItem> group,
      GroupCollection<TDataItem> groups)
    {
      if (groups.Count == 0)
        return (Group<TDataItem>) null;
      int index = groups.IndexOf(group);
      if (index >= 0)
        return groups[index];
      foreach (Group<TDataItem> group1 in (ReadOnlyCollection<Group<TDataItem>>) groups)
      {
        Group<TDataItem> group2 = this.FindGroup(group, group1.Groups);
        if (group2 != null)
          return group2;
      }
      return (Group<TDataItem>) null;
    }

    public event EventHandler CurrentChanged;

    public event CancelEventHandler CurrentChanging;

    protected virtual void OnCurrentChanged(EventArgs args)
    {
      if (this.update != 0 || this.CurrentChanged == null)
        return;
      this.CurrentChanged((object) this, args);
    }

    protected virtual void OnCurrentChanging(CancelEventArgs args)
    {
      if (this.update != 0 || this.CurrentChanging == null)
        return;
      this.CurrentChanging((object) this, args);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TDataItem CurrentItem
    {
      get
      {
        if (this.CurrentPosition >= 0 && this.CurrentPosition < this.Count)
          return this[this.CurrentPosition];
        return default (TDataItem);
      }
      internal set
      {
        this.CurrentPosition = this.IndexOf(value);
      }
    }

    public int CurrentPosition
    {
      get
      {
        return this.position;
      }
      protected set
      {
        this.position = value;
      }
    }

    public bool MoveCurrentTo(TDataItem item)
    {
      return this.MoveCurrentToPosition(this.IndexOf(item));
    }

    public bool MoveCurrentToFirst()
    {
      return this.MoveCurrentToPosition(0);
    }

    public bool MoveCurrentToLast()
    {
      return this.MoveCurrentToPosition(this.Count - 1);
    }

    public bool MoveCurrentToNext()
    {
      return this.MoveCurrentToPosition(this.CurrentPosition + 1);
    }

    public bool MoveCurrentToPosition(int newPosition)
    {
      return this.SetCurrentPositionCore(newPosition, false, CurrentChangeReason.Move);
    }

    protected virtual bool SetCurrentPositionCore(int newPosition, bool forceNotify)
    {
      return this.SetCurrentPositionCore(newPosition, forceNotify, CurrentChangeReason.Default);
    }

    protected bool SetCurrentPositionCore(
      int newPosition,
      bool forceNotify,
      CurrentChangeReason reason)
    {
      if (newPosition >= this.Count)
        newPosition = this.Count - 1;
      TDataItem dataItem = default (TDataItem);
      if (newPosition >= 0 && newPosition < this.Count)
        dataItem = this.Items[newPosition];
      int num = (object) dataItem != null ? dataItem.GetHashCode() : -1;
      if (num != this.currentItemHashCode)
      {
        forceNotify = true;
        this.currentItemHashCode = num;
      }
      if (this.CurrentPosition == newPosition && !forceNotify)
        return false;
      if (this.update == 0)
      {
        CurrentChangingEventArgs changingEventArgs = new CurrentChangingEventArgs(reason);
        this.OnCurrentChanging((CancelEventArgs) changingEventArgs);
        if (changingEventArgs.Cancel)
          return false;
      }
      this.CurrentPosition = newPosition;
      this.positionChangedInUpdate = this.update > 0;
      if (this.update == 0)
        this.OnCurrentChanged((EventArgs) new CurrentChangedEventArgs(reason));
      return true;
    }

    public bool MoveCurrentToPrevious()
    {
      return this.MoveCurrentToPosition(this.CurrentPosition - 1);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool CanFilter
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool CanGroup
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool CanSort
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    public virtual IEnumerable<TDataItem> SourceCollection
    {
      get
      {
        return this.sourceCollection;
      }
    }

    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.sortDescriptors;
      }
    }

    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.groupDescriptors;
      }
    }

    public virtual Predicate<TDataItem> Filter
    {
      get
      {
        return this.filter;
      }
      set
      {
        if (!(this.filter != value))
          return;
        this.filter = value;
        this.OnNotifyPropertyChanged(nameof (Filter));
      }
    }

    public bool IsIncrementalFiltering
    {
      get
      {
        if (string.IsNullOrEmpty(this.prevExpression))
          return false;
        if (this.prevExpression == this.filterExpression)
          return true;
        BinaryOpNode binaryOpNode1 = ExpressionParser.Parse(this.prevExpression, this.caseSensitive) as BinaryOpNode;
        BinaryOpNode binaryOpNode2 = ExpressionParser.Parse(this.filterExpression, this.caseSensitive) as BinaryOpNode;
        this.prevExpression = this.filterExpression;
        if (binaryOpNode1 == null || binaryOpNode2 == null || (binaryOpNode1.Op != Operator.Like || binaryOpNode2.Op != Operator.Like))
          return false;
        NameNode left1 = binaryOpNode1.Left as NameNode;
        NameNode left2 = binaryOpNode2.Left as NameNode;
        if (left1 == null || left2 == null || left1.Name != left2.Name)
          return false;
        ConstNode right1 = binaryOpNode1.Right as ConstNode;
        ConstNode right2 = binaryOpNode2.Right as ConstNode;
        if (right1 == null || right2 == null)
          return false;
        string str1 = right1.Value.ToString();
        string str2 = right2.Value.ToString();
        if (str1.Contains("%"))
        {
          string str3 = str1.Replace("%", string.Empty);
          string str4 = str2.Replace("%", string.Empty);
          if (str4.Length > 1 && str4.Substring(0, str4.Length - 1) == str3)
            return true;
        }
        return false;
      }
    }

    public virtual Predicate<TDataItem> DefaultFilter
    {
      get
      {
        return new Predicate<TDataItem>(this.PerformExpressionFilter);
      }
    }

    public virtual GroupCollection<TDataItem> Groups
    {
      get
      {
        return GroupCollection<TDataItem>.Empty;
      }
    }

    public virtual Telerik.WinControls.Data.GroupPredicate<TDataItem> GroupPredicate
    {
      get
      {
        return (Telerik.WinControls.Data.GroupPredicate<TDataItem>) null;
      }
      set
      {
      }
    }

    public virtual Telerik.WinControls.Data.GroupPredicate<TDataItem> DefaultGroupPredicate
    {
      get
      {
        return (Telerik.WinControls.Data.GroupPredicate<TDataItem>) null;
      }
    }

    public virtual void Refresh()
    {
      this.RefreshOverride();
    }

    protected virtual void RefreshOverride()
    {
      if (this.IsInUpdate)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    protected virtual void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      this.OnCollectionChanged(args);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (this.update != 0)
        return;
      if (this.VersionUpdateNeeded(args))
        ++this.version;
      if (this.CollectionChanged == null)
        return;
      args.ResetReason = this.resetReason;
      this.resetReason = CollectionResetReason.Refresh;
      this.CollectionChanged((object) this, args);
    }

    protected virtual bool VersionUpdateNeeded(NotifyCollectionChangedEventArgs args)
    {
      NotifyCollectionChangedAction action = args.Action;
      string propertyName = args.PropertyName;
      return action != NotifyCollectionChangedAction.ItemChanging && action != NotifyCollectionChangedAction.ItemChanged || (string.IsNullOrEmpty(propertyName) || this.filterContext.Contains(propertyName)) || (this.GroupDescriptors.Contains(propertyName) || this.SortDescriptors.Contains(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      if (!(propertyName == "PageSize") && !(propertyName == "PagingBeforeGrouping"))
        return;
      this.EnsurePageIndex();
    }

    protected virtual void EnsurePageIndex()
    {
      if (!this.CanPage || this.PageIndex < this.TotalPages || this.MoveToLastPage())
        return;
      this.MoveToFirstPage();
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "FilterExpression" || e.PropertyName == "Filter")
        this.resetReason = CollectionResetReason.FilteringChanged;
      if (this.PropertyChanged == null || this.update != 0)
        return;
      this.PropertyChanged((object) this, e);
    }

    void IReadOnlyCollection<TDataItem>.CopyTo(TDataItem[] array, int index)
    {
      this.Items.CopyTo(array, index);
    }

    public IEnumerator<TDataItem> GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    private IEnumerable<TDataItem> GetItems(int startIndex, int count)
    {
      for (int i = 0; i < count; ++i)
        yield return this[startIndex + i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.Items.GetEnumerator();
    }

    [DefaultValue(false)]
    public virtual bool PagingBeforeGrouping
    {
      get
      {
        return this.pagingBeforeGrouping;
      }
      set
      {
        if (this.pagingBeforeGrouping == value)
          return;
        this.pagingBeforeGrouping = value;
        this.OnNotifyPropertyChanged(nameof (PagingBeforeGrouping));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool CanPage
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    public event EventHandler<EventArgs> PageChanged;

    public event EventHandler<PageChangingEventArgs> PageChanging;

    public virtual bool MoveToFirstPage()
    {
      return this.MoveToPage(0);
    }

    public virtual bool MoveToLastPage()
    {
      if (this.ItemCount > 0)
        return this.MoveToPage(this.ItemCount / this.PageSize + (this.ItemCount % this.PageSize > 0 ? 0 : -1));
      bool flag = false;
      while (flag)
        flag = this.MoveToNextPage();
      return flag;
    }

    public virtual bool MoveToNextPage()
    {
      return this.MoveToPage(this.PageIndex + 1);
    }

    public virtual bool MoveToPage(int pageIndex)
    {
      if (!this.CanChangePage || pageIndex < 0 || (pageIndex == this.PageIndex || pageIndex >= this.TotalPages) || (pageIndex * this.PageSize >= this.ItemCount || this.OnPageChanging(pageIndex)))
        return false;
      this.isPageChanging = true;
      this.currentPageIndex = pageIndex;
      this.resetReason = CollectionResetReason.PagingChanged;
      this.RefreshOverride();
      this.isPageChanging = false;
      this.OnPageChanged();
      return true;
    }

    public virtual bool MoveToPreviousPage()
    {
      return this.MoveToPage(this.PageIndex - 1);
    }

    public virtual bool CanChangePage
    {
      get
      {
        return !this.IsPageChanging;
      }
    }

    public virtual bool IsPageChanging
    {
      get
      {
        return this.isPageChanging;
      }
    }

    public virtual int PageIndex
    {
      get
      {
        return this.currentPageIndex;
      }
    }

    public virtual int PageSize
    {
      get
      {
        return this.pageSize;
      }
      set
      {
        if (this.pageSize == value)
          return;
        this.pageSize = value;
        this.OnNotifyPropertyChanged(nameof (PageSize));
      }
    }

    public virtual int ItemCount
    {
      get
      {
        return -1;
      }
    }

    public virtual int TotalPages
    {
      get
      {
        return this.ItemCount / this.PageSize + (this.ItemCount % this.PageSize > 0 ? 1 : 0);
      }
    }

    protected virtual bool OnPageChanging(int newIndex)
    {
      PageChangingEventArgs e = new PageChangingEventArgs(newIndex);
      if (this.PageChanging != null)
        this.PageChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnPageChanged()
    {
      if (this.PageChanged == null)
        return;
      this.PageChanged((object) this, EventArgs.Empty);
    }

    private class DeferHelper : IDisposable
    {
      private RadCollectionView<TDataItem> collectionView;

      public DeferHelper(RadCollectionView<TDataItem> collectionView)
      {
        this.collectionView = collectionView;
      }

      public void Dispose()
      {
        if (this.collectionView == null)
          return;
        this.collectionView.EndDefer();
        this.collectionView = (RadCollectionView<TDataItem>) null;
      }
    }
  }
}
