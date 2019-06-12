// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Telerik.Data.Expressions;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Commands;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class RadTreeViewElement : VirtualizedScrollPanel<RadTreeNode, TreeNodeElement>, IDataItemSource
  {
    private int firstVisibleIndex = -1;
    private int defaultImageIndex = -1;
    private int anchorIndex = -1;
    private bool hotTracking = true;
    private bool lazyMode = true;
    private bool enableAutoExpand = true;
    private bool autoCheckChildNodes = true;
    private bool autoScrollOnClick = true;
    private string defaultImageKey = string.Empty;
    private Timer mouseUpTimer = new Timer();
    private Dictionary<System.Type, IInputEditor> cachedEditors = new Dictionary<System.Type, IInputEditor>();
    private TreeNodeEditMode treeNodeEditMode = TreeNodeEditMode.TextAndValue;
    private RadTreeViewElement.UpdateActions resumeAction = RadTreeViewElement.UpdateActions.Resume;
    private RadTreeViewElement.UpdateActions previousEndUpdateAction = RadTreeViewElement.UpdateActions.StateChanged;
    private ToggleMode toggleMode = ToggleMode.DoubleClick;
    private string lastSearchCriteria = "";
    public static RadProperty ItemDropHintProperty = RadProperty.Register(nameof (ItemDropHint), typeof (RadImageShape), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowLinesProperty = RadProperty.Register(nameof (ShowLines), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowRootLinesProperty = RadProperty.Register(nameof (ShowRootLines), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowExpandCollapseProperty = RadProperty.Register(nameof (ShowExpandCollapse), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineColorProperty = RadProperty.Register(nameof (LineColor), typeof (Color), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Gray, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ExpandImageProperty = RadProperty.Register(nameof (ExpandImage), typeof (Image), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty CollapseImageProperty = RadProperty.Register(nameof (CollapseImage), typeof (Image), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty HoveredExpandImageProperty = RadProperty.Register(nameof (HoveredExpandImage), typeof (Image), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty HoveredCollapseImageProperty = RadProperty.Register(nameof (HoveredCollapseImage), typeof (Image), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty LineStyleProperty = RadProperty.Register(nameof (LineStyle), typeof (TreeLineStyle), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TreeLineStyle.Dot, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NodeSpacingProperty = RadProperty.Register(nameof (NodeSpacing), typeof (int), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FullRowSelectProperty = RadProperty.Register(nameof (FullRowSelect), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AllowArbitraryItemHeightProperty = RadProperty.Register(nameof (AllowArbitraryItemHeight), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (int), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TreeIndentProperty = RadProperty.Register(nameof (TreeIndent), typeof (int), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AlternatingRowColorProperty = RadProperty.Register(nameof (AlternatingRowColor), typeof (Color), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(246, 251, (int) byte.MaxValue), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ExpandAnimationProperty = RadProperty.Register(nameof (ExpandAnimation), typeof (ExpandAnimation), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ExpandAnimation.Opacity));
    public static RadProperty PlusMinusAnimationStepProperty = RadProperty.Register(nameof (PlusMinusAnimationStepProperty), typeof (double), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.025));
    public static RadProperty AllowPlusMinusAnimationProperty = RadProperty.Register(nameof (AllowPlusMinusAnimation), typeof (bool), typeof (RadTreeViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    private int updateSuspendedCount;
    private int updateCurrentNodeChanged;
    private int updateSelectedNodeChanged;
    private int updateSelectionChanged;
    private bool multiSelect;
    private bool hideSelection;
    private bool checkBoxes;
    private bool allowEdit;
    private bool allowAdd;
    private bool allowRemove;
    private bool showNodeToolTips;
    private bool allowDragDrop;
    private bool triStateMode;
    private bool allowAlternatingRowColor;
    private bool enterEditMode;
    private bool performSelectionOnMouseUp;
    private bool resetSizes;
    private bool doNotStartEditingOnMouseUp;
    private RadTreeViewElement.UpdateActions pendingScrollerUpdates;
    private bool enableKineticScrolling;
    internal bool IsPerformingEndEdit;
    private bool disableEnsureNodeVisibleHorizontal;
    private bool allowDefaultContextMenu;
    private bool isBeginEdit;
    private string pathSeparator;
    private object search;
    private object cachedOldValue;
    private Timer scrollTimer;
    private Timer expandTimer;
    private IInputEditor activeEditor;
    private RadTreeNode root;
    private RadTreeNode selected;
    private RadTreeNode anchorPosition;
    private RadTreeNode lastClickedNode;
    private RadTreeNode nodeToEnsureVisible;
    private RadContextMenu contextMenu;
    private TreeViewTraverser traverser;
    private TreeNodeProvider treeNodeProvider;
    internal BindingProvider bindingProvider;
    private IComparer<RadTreeNode> comparer;
    private FilterDescriptorCollection filterDescriptors;
    private SortDescriptorCollection sortDescriptors;
    private Predicate<RadTreeNode> filterPredicate;
    private ExpressionNode expressionNode;
    private SelectedTreeNodeCollection selectedNodeCollection;
    private TreeViewDragDropService dragDropService;
    private CheckedTreeNodeCollection checkedNodeCollection;
    private ScrollServiceBehavior scrollBehavior;
    private Point mouseDownLocation;
    private Point mouseMoveLocation;
    private ScrollState horizontalScrollState;
    private ExpandMode expandMode;
    private SortOrder sortOrder;
    internal RadTreeNode draggedNode;
    private TreeViewDefaultContextMenu defaultMenu;
    private bool isByKeyboard;
    private Timer typingTimer;
    private StringBuilder searchBuffer;
    private IFindStringComparer findStringComparer;

    static RadTreeViewElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TreeViewElementStateManager(), typeof (RadTreeViewElement));
    }

    public RadTreeViewElement()
    {
      this.bindingProvider = new BindingProvider(this);
      this.filterDescriptors = (FilterDescriptorCollection) new TreeViewFilterDescriptorCollection();
      this.filterDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.filterDescriptors_CollectionChanged);
      this.filterPredicate = new Predicate<RadTreeNode>(this.EvalFilter);
      this.comparer = (IComparer<RadTreeNode>) new TreeNodeComparer(this);
      this.sortDescriptors = new SortDescriptorCollection();
      this.sortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
      this.root = (RadTreeNode) new RadTreeViewElement.RootTreeNode(this);
      this.traverser = new TreeViewTraverser(this);
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.Traverser = (ITraverser<RadTreeNode>) this.traverser;
      this.NotifyParentOnMouseInput = true;
      this.pathSeparator = "\\";
      this.selectedNodeCollection = new SelectedTreeNodeCollection(this);
      this.checkedNodeCollection = new CheckedTreeNodeCollection(this.root);
      this.dragDropService = this.CreateDragDropService();
      this.expandTimer = new Timer();
      this.expandTimer.Interval = 500;
      this.expandTimer.Tick += new EventHandler(this.ExpandTimer_Tick);
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = 20;
      this.scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
      this.mouseUpTimer = new Timer();
      this.mouseUpTimer.Interval = SystemInformation.DoubleClickTime + 10;
      this.mouseUpTimer.Tick += new EventHandler(this.mouseUpTimer_Tick);
      this.Scroller.ScrollerUpdated += new EventHandler(this.Scroller_ScrollerUpdated);
      this.scrollBehavior = new ScrollServiceBehavior();
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement, this.HScrollBar));
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement, this.VScrollBar));
    }

    private void sortDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.UpdateNodes();
      this.Update(RadTreeViewElement.UpdateActions.SortChanged);
    }

    private void UpdateNodes()
    {
      if (this.updateSuspendedCount > 0 || this.updateSelectionChanged > 0)
        return;
      if (this.filterPredicate == new Predicate<RadTreeNode>(this.EvalFilter))
        this.expressionNode = DataUtils.Parse(this.filterDescriptors.Expression, false);
      Stack<RadTreeNodeCollection> treeNodeCollectionStack = new Stack<RadTreeNodeCollection>();
      treeNodeCollectionStack.Push(this.root.Nodes);
      this.root.nodes.Update();
      while (treeNodeCollectionStack.Count > 0)
      {
        RadTreeNodeCollection treeNodeCollection = treeNodeCollectionStack.Pop();
        for (int index = 0; index < treeNodeCollection.Count; ++index)
        {
          if (treeNodeCollection[index].nodes != null)
          {
            treeNodeCollection[index].nodes.Update();
            treeNodeCollectionStack.Push(treeNodeCollection[index].nodes);
          }
        }
      }
      this.expressionNode = (ExpressionNode) null;
      this.root.InvalidateOnState(true);
    }

    internal bool PreProcess(RadTreeNode parent, RadTreeNode item, params object[] metadata)
    {
      if (!this.IsSuspended && metadata != null && metadata[0] is "Remove")
      {
        RadTreeViewCancelEventArgs e = new RadTreeViewCancelEventArgs(item);
        this.OnNodeRemoving(e);
        if (e.Cancel)
          return false;
      }
      if (this.bindingProvider.IsDataBound && !this.bindingProvider.PreProcess(parent, item, metadata))
        return false;
      if (!this.IsSuspended && metadata != null)
      {
        string str = (string) metadata[0];
        if (str == "Add" || str == "Insert")
        {
          item.TreeViewElement = this;
          RadTreeViewCancelEventArgs e = new RadTreeViewCancelEventArgs(item);
          this.OnNodeAdding(e);
          if (this.bindingProvider.IsDataBound && e.Cancel)
            this.bindingProvider.PostProcess(parent, item, (object) "Remove");
          if (e.Cancel)
          {
            item.TreeViewElement = (RadTreeViewElement) null;
            return false;
          }
        }
      }
      return true;
    }

    protected internal virtual bool PassesFilter(RadTreeNode node)
    {
      if (this.filterPredicate == null)
        return this.EvalFilter(node);
      if (this.filterPredicate(node))
        return true;
      Stack<RadTreeNode> radTreeNodeStack = new Stack<RadTreeNode>();
      if (node.HasNodes)
      {
        radTreeNodeStack.Push(node);
        while (radTreeNodeStack.Count > 0)
        {
          foreach (RadTreeNode node1 in (Collection<RadTreeNode>) radTreeNodeStack.Pop().Nodes)
          {
            if (this.filterPredicate(node1))
              return true;
            radTreeNodeStack.Push(node1);
          }
        }
      }
      return false;
    }

    internal bool HasNodeProvider
    {
      get
      {
        if (!this.bindingProvider.IsDataBound)
          return this.treeNodeProvider != null;
        return true;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BackColor = Color.White;
      this.GradientStyle = GradientStyles.Solid;
      this.DrawFill = true;
      this.AllowDrop = true;
      this.typingTimer = new Timer();
      this.typingTimer.Interval = 300;
      this.typingTimer.Tick += new EventHandler(this.typingTimer_Tick);
      this.FindStringComparer = (IFindStringComparer) new StartsWithFindStringComparer();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.FitItemsToSize = this.FullRowSelect;
    }

    protected override VirtualizedStackContainer<RadTreeNode> CreateViewElement()
    {
      return (VirtualizedStackContainer<RadTreeNode>) new RadTreeViewVirtualizedContainer();
    }

    protected override void DisposeManagedResources()
    {
      if (this.expandTimer != null)
      {
        this.expandTimer.Stop();
        this.expandTimer.Tick -= new EventHandler(this.ExpandTimer_Tick);
        this.expandTimer.Dispose();
        this.expandTimer = (Timer) null;
      }
      if (this.mouseUpTimer != null)
      {
        this.mouseUpTimer.Stop();
        this.mouseUpTimer.Tick -= new EventHandler(this.mouseUpTimer_Tick);
        this.mouseUpTimer.Dispose();
        this.mouseUpTimer = (Timer) null;
      }
      if (this.scrollTimer != null)
      {
        this.scrollTimer.Stop();
        this.scrollTimer.Tick -= new EventHandler(this.ScrollTimer_Tick);
        this.scrollTimer.Dispose();
        this.scrollTimer = (Timer) null;
      }
      if (this.typingTimer != null)
      {
        this.typingTimer.Stop();
        this.typingTimer.Tick -= new EventHandler(this.typingTimer_Tick);
        this.typingTimer.Dispose();
        this.typingTimer = (Timer) null;
      }
      if (this.defaultMenu != null)
      {
        this.defaultMenu.Dispose();
        this.defaultMenu = (TreeViewDefaultContextMenu) null;
      }
      this.Scroller.ScrollerUpdated -= new EventHandler(this.Scroller_ScrollerUpdated);
      foreach (KeyValuePair<System.Type, IInputEditor> cachedEditor in this.cachedEditors)
      {
        IInputEditor inputEditor = cachedEditor.Value;
        (inputEditor as BaseInputEditor)?.EditorElement?.Dispose();
        (inputEditor as IDisposable)?.Dispose();
      }
      this.cachedEditors.Clear();
      base.DisposeManagedResources();
    }

    public event TreeNodeDataErrorEventHandler DataError;

    protected internal virtual void OnDataError(TreeNodeDataErrorEventArgs e)
    {
      TreeNodeDataErrorEventHandler dataError = this.DataError;
      if (dataError != null)
      {
        dataError((object) this, e);
      }
      else
      {
        int num = (int) RadMessageBox.Show(e.ErrorText, "Data Error", MessageBoxButtons.OK, RadMessageIcon.Error);
      }
    }

    public event EventHandler BindingContextChanged;

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
      EventHandler bindingContextChanged = this.BindingContextChanged;
      if (bindingContextChanged == null)
        return;
      bindingContextChanged((object) this, e);
    }

    public event TreeNodeFormattingEventHandler NodeFormatting;

    protected internal virtual void OnNodeFormatting(TreeNodeFormattingEventArgs e)
    {
      TreeNodeFormattingEventHandler nodeFormatting = this.NodeFormatting;
      if (nodeFormatting == null)
        return;
      nodeFormatting((object) this, e);
    }

    public event CreateTreeNodeElementEventHandler CreateNodeElement;

    protected internal virtual void OnCreateNodeElement(CreateTreeNodeElementEventArgs e)
    {
      CreateTreeNodeElementEventHandler createNodeElement = this.CreateNodeElement;
      if (createNodeElement == null)
        return;
      createNodeElement((object) this, e);
    }

    public event CreateTreeNodeEventHandler CreateNode;

    protected virtual void OnCreateNode(CreateTreeNodeEventArgs e)
    {
      CreateTreeNodeEventHandler createNode = this.CreateNode;
      if (createNode == null)
        return;
      createNode((object) this, e);
    }

    public event RadTreeView.RadTreeViewEventHandler NodeDataBound;

    protected internal virtual void OnNodeDataBound(RadTreeViewEventArgs e)
    {
      if (this.NodeDataBound == null)
        return;
      this.NodeDataBound((object) this, e);
    }

    public event RadTreeView.TreeViewMouseEventHandler NodeMouseDown;

    protected internal virtual void OnNodeMouseDown(RadTreeViewMouseEventArgs e)
    {
      RadTreeView.TreeViewMouseEventHandler nodeMouseDown = this.NodeMouseDown;
      if (nodeMouseDown == null)
        return;
      nodeMouseDown((object) this, e);
    }

    public event RadTreeView.TreeViewMouseEventHandler NodeMouseUp;

    protected internal virtual void OnNodeMouseUp(RadTreeViewMouseEventArgs e)
    {
      RadTreeView.TreeViewMouseEventHandler nodeMouseUp = this.NodeMouseUp;
      if (nodeMouseUp == null)
        return;
      nodeMouseUp((object) this, e);
    }

    public event RadTreeView.TreeViewMouseEventHandler NodeMouseMove;

    protected internal virtual void OnNodeMouseMove(RadTreeViewMouseEventArgs e)
    {
      RadTreeView.TreeViewMouseEventHandler nodeMouseMove = this.NodeMouseMove;
      if (nodeMouseMove == null)
        return;
      nodeMouseMove((object) this, e);
    }

    public event RadTreeView.TreeViewShowExpanderEventHandler ShowExpander;

    protected internal virtual void OnShowExpander(TreeViewShowExpanderEventArgs e)
    {
      RadTreeView.TreeViewShowExpanderEventHandler showExpander = this.ShowExpander;
      if (showExpander == null)
        return;
      showExpander((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeMouseEnter;

    protected internal virtual void OnNodeMouseEnter(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler nodeMouseEnter = this.NodeMouseEnter;
      if (nodeMouseEnter == null)
        return;
      nodeMouseEnter((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeMouseLeave;

    protected internal virtual void OnNodeMouseLeave(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler nodeMouseLeave = this.NodeMouseLeave;
      if (nodeMouseLeave == null)
        return;
      nodeMouseLeave((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeMouseClick;

    protected internal virtual void OnNodeMouseClick(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler nodeMouseClick = this.NodeMouseClick;
      if (nodeMouseClick == null)
        return;
      nodeMouseClick((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeMouseDoubleClick;

    protected internal virtual void OnNodeMouseDoubleClick(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler mouseDoubleClick = this.NodeMouseDoubleClick;
      if (mouseDoubleClick == null)
        return;
      mouseDoubleClick((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeMouseHover;

    protected internal virtual void OnNodeMouseHover(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler nodeMouseHover = this.NodeMouseHover;
      if (nodeMouseHover == null)
        return;
      nodeMouseHover((object) this, e);
    }

    public virtual event RadTreeView.RadTreeViewCancelEventHandler NodeCheckedChanging;

    protected virtual void OnNodeCheckedChanging(RadTreeViewCancelEventArgs e)
    {
      RadTreeView.RadTreeViewCancelEventHandler nodeCheckedChanging = this.NodeCheckedChanging;
      if (nodeCheckedChanging == null)
        return;
      nodeCheckedChanging((object) this, e);
    }

    protected internal bool OnNodeCheckedChanging(RadTreeNode node)
    {
      RadTreeViewCancelEventArgs e = new RadTreeViewCancelEventArgs(node);
      this.OnNodeCheckedChanging(e);
      return !e.Cancel;
    }

    public virtual event TreeNodeCheckedEventHandler NodeCheckedChanged;

    protected virtual void OnNodeCheckedChanged(TreeNodeCheckedEventArgs e)
    {
      TreeNodeCheckedEventHandler nodeCheckedChanged = this.NodeCheckedChanged;
      if (nodeCheckedChanged == null)
        return;
      nodeCheckedChanged((object) this, e);
    }

    protected internal void OnNodeCheckedChanged(RadTreeNode node)
    {
      this.OnNodeCheckedChanged(new TreeNodeCheckedEventArgs(node));
    }

    protected internal void OnNodeCheckedChanged(RadTreeNode node, CheckedMode checkedMode)
    {
      this.OnNodeCheckedChanged(new TreeNodeCheckedEventArgs(node, checkedMode));
    }

    public event RadTreeView.RadTreeViewCancelEventHandler NodeExpandedChanging;

    protected internal bool OnNodeExpandedChanging(RadTreeNode node)
    {
      RadTreeViewCancelEventArgs e = new RadTreeViewCancelEventArgs(node);
      this.OnNodeExpandedChanging(e);
      return !e.Cancel;
    }

    protected virtual void OnNodeExpandedChanging(RadTreeViewCancelEventArgs e)
    {
      RadTreeView.RadTreeViewCancelEventHandler expandedChanging = this.NodeExpandedChanging;
      if (expandedChanging == null)
        return;
      expandedChanging((object) this, e);
    }

    public event RadTreeView.TreeViewEventHandler NodeExpandedChanged;

    protected internal virtual void OnNodeExpandedChanged(RadTreeViewEventArgs e)
    {
      RadTreeView.TreeViewEventHandler nodeExpandedChanged = this.NodeExpandedChanged;
      if (nodeExpandedChanged != null)
        nodeExpandedChanged((object) this, e);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "Expanded", (object) string.Format("{0}.{1}", (object) e.Node.Text, e.Node.Expanded ? (object) "Expanded" : (object) "Collapsed"));
    }

    public virtual event RadTreeView.RadTreeViewCancelEventHandler SelectedNodeChanging;

    protected internal virtual void OnSelectedNodeChanging(RadTreeViewCancelEventArgs args)
    {
      if (this.IsEditing)
        this.EndEdit();
      RadTreeView.RadTreeViewCancelEventHandler selectedNodeChanging = this.SelectedNodeChanging;
      if (selectedNodeChanging == null)
        return;
      selectedNodeChanging((object) this, args);
    }

    public virtual event RadTreeView.RadTreeViewEventHandler SelectedNodeChanged;

    protected internal virtual void OnSelectedNodeChanged(RadTreeViewEventArgs args)
    {
      RadTreeView.RadTreeViewEventHandler selectedNodeChanged = this.SelectedNodeChanged;
      if (selectedNodeChanged != null)
        selectedNodeChanged((object) this, args);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "SelectionChanged", (object) ("SelectedNodesCount." + (this.SelectedNodes != null ? this.SelectedNodes.Count.ToString() : "None")));
    }

    public virtual event EventHandler SelectedNodesCleared;

    protected internal virtual void OnSelectedNodesCleared()
    {
      if (this.SelectedNodesCleared == null)
        return;
      this.SelectedNodesCleared((object) this, EventArgs.Empty);
    }

    public virtual event EventHandler<RadTreeViewEventArgs> SelectedNodesChanged;

    protected internal virtual void OnSelectedNodesChanged(RadTreeNode node)
    {
      if (this.SelectedNodesChanged == null)
        return;
      this.SelectedNodesChanged((object) this, new RadTreeViewEventArgs(node));
    }

    public event RadTreeView.EditorRequiredHandler EditorRequired;

    protected virtual void OnEditorRequired(TreeNodeEditorRequiredEventArgs e)
    {
      this.OnEditorRequired((object) this, e);
    }

    protected virtual void OnEditorRequired(object sender, TreeNodeEditorRequiredEventArgs e)
    {
      RadTreeView.EditorRequiredHandler editorRequired = this.EditorRequired;
      if (editorRequired == null)
        return;
      editorRequired(sender, e);
    }

    public event TreeNodeEditingEventHandler Editing;

    protected virtual void OnEditing(TreeNodeEditingEventArgs e)
    {
      this.OnEditing((object) this, e);
    }

    protected virtual void OnEditing(object sender, TreeNodeEditingEventArgs e)
    {
      TreeNodeEditingEventHandler editing = this.Editing;
      if (editing == null)
        return;
      editing(sender, e);
    }

    public event TreeNodeEditorInitializedEventHandler EditorInitialized;

    protected virtual void OnEditorInitialized(TreeNodeEditorInitializedEventArgs e)
    {
      TreeNodeEditorInitializedEventHandler editorInitialized = this.EditorInitialized;
      if (editorInitialized == null)
        return;
      editorInitialized((object) this, e);
    }

    public event TreeNodeEditedEventHandler Edited;

    protected virtual void OnEdited(TreeNodeEditedEventArgs e)
    {
      TreeNodeEditedEventHandler edited = this.Edited;
      if (edited != null)
        edited((object) this, e);
      if (this.ElementTree == null || this.ElementTree.Control == null || e.Node == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "Edited", (object) e.Node.Text);
    }

    public event TreeNodeValueChangingEventHandler ValueChanging;

    protected virtual void OnValueChanging(TreeNodeValueChangingEventArgs e)
    {
      TreeNodeValueChangingEventHandler valueChanging = this.ValueChanging;
      if (valueChanging == null)
        return;
      valueChanging((object) this, e);
    }

    public event TreeNodeValueChangedEventHandler ValueChanged;

    protected virtual void OnValueChanged(TreeNodeValueChangedEventArgs e)
    {
      TreeNodeValueChangedEventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged((object) this, e);
    }

    public event TreeNodeValidatingEventHandler ValueValidating;

    protected virtual void OnValueValidating(TreeNodeValidatingEventArgs e)
    {
      TreeNodeValidatingEventHandler valueValidating = this.ValueValidating;
      if (valueValidating == null)
        return;
      valueValidating((object) this, e);
    }

    public event EventHandler ValidationError;

    protected virtual void OnValidationError(EventArgs e)
    {
      EventHandler validationError = this.ValidationError;
      if (validationError == null)
        return;
      validationError((object) this, e);
    }

    public event RadTreeView.ItemDragHandler ItemDrag;

    protected internal virtual void OnItemDrag(RadTreeViewEventArgs e)
    {
      RadTreeView.ItemDragHandler itemDrag = this.ItemDrag;
      if (itemDrag == null)
        return;
      itemDrag((object) this, e);
    }

    public event RadTreeView.DragStartingHandler DragStarting;

    protected internal virtual void OnDragStarting(RadTreeViewDragCancelEventArgs e)
    {
      RadTreeView.DragStartingHandler dragStarting = this.DragStarting;
      if (dragStarting == null)
        return;
      dragStarting((object) this, e);
    }

    public event RadTreeView.DragStartedHandler DragStarted;

    protected internal virtual void OnDragStarted(RadTreeViewDragEventArgs e)
    {
      this.doNotStartEditingOnMouseUp = true;
      this.mouseUpTimer.Stop();
      RadTreeView.DragStartedHandler dragStarted = this.DragStarted;
      if (dragStarted == null)
        return;
      dragStarted((object) this, e);
    }

    public event RadTreeView.DragEndingHandler DragEnding;

    protected internal virtual void OnDragEnding(RadTreeViewDragCancelEventArgs e)
    {
      RadTreeView.DragEndingHandler dragEnding = this.DragEnding;
      if (dragEnding == null)
        return;
      dragEnding((object) this, e);
    }

    public event RadTreeView.DragEndedHandler DragEnded;

    protected internal virtual void OnDragEnded(RadTreeViewDragEventArgs e)
    {
      RadTreeView.DragEndedHandler dragEnded = this.DragEnded;
      if (dragEnded == null)
        return;
      dragEnded((object) this, e);
    }

    public event EventHandler<RadTreeViewDragCancelEventArgs> DragOverNode;

    protected internal virtual void OnDragOverNode(RadTreeViewDragCancelEventArgs e)
    {
      EventHandler<RadTreeViewDragCancelEventArgs> dragOverNode = this.DragOverNode;
      if (dragOverNode == null)
        return;
      dragOverNode((object) this, e);
    }

    public event NodesNeededEventHandler NodesNeeded;

    protected internal virtual bool OnNodesNeeded(NodesNeededEventArgs e)
    {
      if (this.NodesNeeded == null)
        return false;
      this.NodesNeeded((object) this, e);
      return true;
    }

    public event TreeViewContextMenuOpeningEventHandler ContextMenuOpening;

    protected virtual void OnContextMenuOpening(TreeViewContextMenuOpeningEventArgs e)
    {
      TreeViewContextMenuOpeningEventHandler contextMenuOpening = this.ContextMenuOpening;
      if (contextMenuOpening == null)
        return;
      contextMenuOpening((object) this, e);
    }

    public event RadTreeView.RadTreeViewEventHandler NodeRemoved;

    protected internal virtual void OnNodeRemoved(RadTreeViewEventArgs e)
    {
      if (this.NodeRemoved != null)
        this.NodeRemoved((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "NodeRemoved", e.Node != null ? (object) e.Node.Text : (object) "");
    }

    public event RadTreeView.RadTreeViewCancelEventHandler NodeRemoving;

    protected virtual void OnNodeRemoving(RadTreeViewCancelEventArgs e)
    {
      if (this.NodeRemoving == null)
        return;
      this.NodeRemoving((object) this, e);
    }

    public event RadTreeView.RadTreeViewEventHandler NodeAdded;

    protected internal virtual void OnNodeAdded(RadTreeViewEventArgs e)
    {
      if (this.NodeAdded == null)
        return;
      this.NodeAdded((object) this, e);
    }

    public event RadTreeView.RadTreeViewCancelEventHandler NodeAdding;

    protected internal virtual void OnNodeAdding(RadTreeViewCancelEventArgs e)
    {
      if (this.NodeAdding == null)
        return;
      this.NodeAdding((object) this, e);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IComparer<RadTreeNode> Comparer
    {
      get
      {
        return this.comparer;
      }
      set
      {
        if (this.comparer == value)
          return;
        this.comparer = value;
        this.UpdateNodes();
        this.Update(RadTreeViewElement.UpdateActions.Resume);
        this.OnNotifyPropertyChanged(nameof (Comparer));
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.enableKineticScrolling;
      }
      set
      {
        if (this.enableKineticScrolling == value)
          return;
        this.enableKineticScrolling = value;
        if (value)
          return;
        this.ScrollBehavior.Stop();
      }
    }

    public ScrollServiceBehavior ScrollBehavior
    {
      get
      {
        return this.scrollBehavior;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool LazyMode
    {
      get
      {
        return this.lazyMode;
      }
      set
      {
        if (this.lazyMode == value)
          return;
        this.lazyMode = value;
        this.Update(RadTreeViewElement.UpdateActions.Reset);
        this.OnNotifyPropertyChanged(nameof (LazyMode));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the child nodes should be auto checked when RadTreeView is in tri state mode")]
    public virtual bool AutoCheckChildNodes
    {
      get
      {
        return this.autoCheckChildNodes;
      }
      set
      {
        if (value == this.autoCheckChildNodes)
          return;
        this.autoCheckChildNodes = value;
        this.OnNotifyPropertyChanged(nameof (AutoCheckChildNodes));
      }
    }

    [Browsable(true)]
    [Description("Contains data binding settings for related data.")]
    public RelationBindingCollection RelationBindings
    {
      get
      {
        return this.bindingProvider.RelationBindings;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableDeferredScrolling
    {
      get
      {
        return this.Scroller.ScrollMode == ItemScrollerScrollModes.Deferred;
      }
      set
      {
        if (!value)
          return;
        this.Scroller.ScrollMode = ItemScrollerScrollModes.Deferred;
        this.OnNotifyPropertyChanged("EnableDeferedScrolling");
      }
    }

    [Description("Gets or sets the type of expand animation.")]
    [Category("Behavior")]
    [DefaultValue(ExpandAnimation.Opacity)]
    public ExpandAnimation ExpandAnimation
    {
      get
      {
        return (ExpandAnimation) this.GetValue(RadTreeViewElement.ExpandAnimationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ExpandAnimationProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DefaultValue(0.025)]
    [Description("Gets or sets the animation step for expand/collapse animation.")]
    public double PlusMinusAnimationStep
    {
      get
      {
        return (double) this.GetValue(RadTreeViewElement.PlusMinusAnimationStepProperty);
      }
      set
      {
        if (this.PlusMinusAnimationStep == value)
          return;
        double num1 = value;
        if (value < 0.0)
          num1 = 0.0;
        else if (value > 1.0)
          num1 = 1.0;
        int num2 = (int) this.SetValue(RadTreeViewElement.PlusMinusAnimationStepProperty, (object) num1);
      }
    }

    [Description("Gets or sets a value indicating whether animation of collapse/expand images is enabled.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AllowPlusMinusAnimation
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.AllowPlusMinusAnimationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.AllowPlusMinusAnimationProperty, (object) value);
      }
    }

    [RelatedImageList("ImageList")]
    [Category("Appearance")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Description("The default image index for nodes.")]
    [DefaultValue(-1)]
    public override int ImageIndex
    {
      get
      {
        return this.defaultImageIndex;
      }
      set
      {
        if (this.defaultImageIndex == value)
          return;
        this.defaultImageKey = string.Empty;
        this.defaultImageIndex = value;
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [RelatedImageList("ImageList")]
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    [TypeConverter(typeof (ImageKeyConverter))]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("The default image key for nodes.")]
    public override string ImageKey
    {
      get
      {
        return this.defaultImageKey;
      }
      set
      {
        if (!(this.defaultImageKey != value))
          return;
        this.defaultImageKey = value;
        this.defaultImageIndex = -1;
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool TriStateMode
    {
      get
      {
        return this.triStateMode;
      }
      set
      {
        if (this.triStateMode == value)
          return;
        this.triStateMode = value;
        if (value && !this.CheckBoxes)
          this.CheckBoxes = value;
        this.OnNotifyPropertyChanged(nameof (TriStateMode));
      }
    }

    [DefaultValue(ToggleMode.DoubleClick)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToggleMode ToggleMode
    {
      get
      {
        return this.toggleMode;
      }
      set
      {
        if (this.toggleMode == value)
          return;
        this.toggleMode = value;
        this.OnNotifyPropertyChanged(nameof (ToggleMode));
      }
    }

    public TreeViewDragDropService DragDropService
    {
      get
      {
        return this.dragDropService;
      }
      set
      {
        if (this.dragDropService != null && !this.dragDropService.IsDisposed)
          this.dragDropService.Dispose();
        this.dragDropService = value;
      }
    }

    [VsbBrowsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadImageShape ItemDropHint
    {
      get
      {
        return (RadImageShape) this.GetValue(RadTreeViewElement.ItemDropHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ItemDropHintProperty, (object) value);
      }
    }

    public RadTreeNode LastNode
    {
      get
      {
        if (this.root.Nodes.Count > 0)
        {
          RadTreeNode radTreeNode = this.root.Nodes[this.root.Nodes.Count - 1];
          while (radTreeNode != null)
          {
            if (!radTreeNode.Visible && !this.IsInDesignMode)
              radTreeNode = radTreeNode.PrevVisibleNode;
            if (radTreeNode != null)
            {
              if (!radTreeNode.Expanded || radTreeNode.Nodes.Count <= 0)
                return radTreeNode;
              radTreeNode = radTreeNode.Nodes[radTreeNode.Nodes.Count - 1];
            }
          }
        }
        return (RadTreeNode) null;
      }
    }

    public virtual bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        if (this.allowDragDrop == value)
          return;
        this.allowDragDrop = value;
        Control control = this.ElementTree.Control;
        if (control != null)
          control.AllowDrop = true;
        this.OnNotifyPropertyChanged(nameof (AllowDragDrop));
      }
    }

    public virtual bool MultiSelect
    {
      get
      {
        return this.multiSelect;
      }
      set
      {
        if (this.multiSelect == value)
          return;
        RadTreeNode selectedNode = this.SelectedNode;
        if (!value && this.SelectedNodes.Count > 1)
          this.ClearSelection();
        if (selectedNode != null)
        {
          selectedNode.Selected = true;
          this.Update(RadTreeViewElement.UpdateActions.StateChanged, this.SelectedNode);
        }
        this.multiSelect = value;
        this.OnNotifyPropertyChanged(nameof (MultiSelect));
      }
    }

    public bool ShowExpandCollapse
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.ShowExpandCollapseProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ShowExpandCollapseProperty, (object) value);
      }
    }

    public virtual SelectedTreeNodeCollection SelectedNodes
    {
      get
      {
        return this.selectedNodeCollection;
      }
    }

    public virtual CheckedTreeNodeCollection CheckedNodes
    {
      get
      {
        return this.checkedNodeCollection;
      }
    }

    public virtual RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
        this.OnNotifyPropertyChanged(nameof (ContextMenu));
      }
    }

    public virtual bool CheckBoxes
    {
      get
      {
        return this.checkBoxes;
      }
      set
      {
        if (this.checkBoxes == value)
          return;
        this.checkBoxes = value;
        if (!value)
          this.DisableCheckBoxes();
        this.OnNotifyPropertyChanged(nameof (CheckBoxes));
      }
    }

    public virtual bool HideSelection
    {
      get
      {
        return this.hideSelection;
      }
      set
      {
        if (this.hideSelection == value)
          return;
        this.hideSelection = value;
        this.OnNotifyPropertyChanged(nameof (HideSelection));
      }
    }

    public virtual bool HotTracking
    {
      get
      {
        return this.hotTracking;
      }
      set
      {
        if (this.hotTracking == value)
          return;
        this.hotTracking = value;
        this.OnNotifyPropertyChanged(nameof (HotTracking));
      }
    }

    public int ItemHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadTreeViewElement.ItemHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ItemHeightProperty, (object) value);
      }
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    public bool AllowEdit
    {
      get
      {
        return this.allowEdit;
      }
      set
      {
        if (this.allowEdit == value)
          return;
        this.allowEdit = value;
        this.OnNotifyPropertyChanged(nameof (AllowEdit));
      }
    }

    [DefaultValue(TreeNodeEditMode.TextAndValue)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual TreeNodeEditMode EditMode
    {
      get
      {
        return this.treeNodeEditMode;
      }
      set
      {
        if (this.treeNodeEditMode == value)
          return;
        this.treeNodeEditMode = value;
        this.OnNotifyPropertyChanged(nameof (EditMode));
      }
    }

    public virtual bool AllowAdd
    {
      get
      {
        return this.allowAdd;
      }
      set
      {
        if (this.allowAdd == value)
          return;
        this.allowAdd = value;
        this.OnNotifyPropertyChanged(nameof (AllowAdd));
      }
    }

    public virtual bool AllowRemove
    {
      get
      {
        return this.allowRemove;
      }
      set
      {
        if (this.allowRemove == value)
          return;
        this.allowRemove = value;
        this.OnNotifyPropertyChanged(nameof (AllowRemove));
      }
    }

    public bool IsEditing
    {
      get
      {
        return this.ActiveEditor != null;
      }
    }

    public virtual RadTreeNode SelectedNode
    {
      get
      {
        return this.selected;
      }
      set
      {
        if (this.selected == value)
          return;
        if (value != null)
        {
          if (!this.isByKeyboard)
            this.ProcessSelection(value, Keys.None, false, RadTreeViewAction.Unknown);
          else
            this.ProcessSelection(value, Control.ModifierKeys, false, RadTreeViewAction.ByKeyboard);
        }
        else if (!this.isByKeyboard)
          this.ProcessCurrentNode(value, true, RadTreeViewAction.Unknown);
        else
          this.ProcessCurrentNode(value, true, RadTreeViewAction.ByKeyboard);
        this.isByKeyboard = false;
      }
    }

    public bool ShowLines
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.ShowLinesProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ShowLinesProperty, (object) value);
      }
    }

    public bool ShowRootLines
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.ShowRootLinesProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ShowRootLinesProperty, (object) value);
      }
    }

    public bool ShowNodeToolTips
    {
      get
      {
        return this.showNodeToolTips;
      }
      set
      {
        if (this.showNodeToolTips == value)
          return;
        this.showNodeToolTips = value;
        this.OnNotifyPropertyChanged(nameof (ShowNodeToolTips));
      }
    }

    public RadTreeNode TopNode
    {
      get
      {
        RadTreeNode radTreeNode = (RadTreeNode) null;
        TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
        while (treeViewTraverser.MoveNext())
        {
          if (treeViewTraverser.Current.Visible || this.IsInDesignMode)
          {
            radTreeNode = treeViewTraverser.Current;
            break;
          }
        }
        return radTreeNode;
      }
    }

    public Color LineColor
    {
      get
      {
        return (Color) this.GetValue(RadTreeViewElement.LineColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.LineColorProperty, (object) value);
      }
    }

    public virtual TreeLineStyle LineStyle
    {
      get
      {
        return (TreeLineStyle) this.GetValue(RadTreeViewElement.LineStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.LineStyleProperty, (object) value);
      }
    }

    public int VisibleCount
    {
      get
      {
        if (this.ViewElement != null)
          return this.ViewElement.Children.Count;
        return 0;
      }
    }

    [DefaultValue("\\")]
    [Browsable(false)]
    [Description("Gets or sets the value of the path separator.")]
    public virtual string PathSeparator
    {
      get
      {
        return this.pathSeparator;
      }
      set
      {
        if (!(this.pathSeparator != value))
          return;
        this.pathSeparator = value;
        this.OnNotifyPropertyChanged(nameof (PathSeparator));
      }
    }

    public virtual TreeNodeProvider TreeNodeProvider
    {
      get
      {
        if (this.treeNodeProvider == null)
          return (TreeNodeProvider) this.bindingProvider;
        return this.treeNodeProvider;
      }
      set
      {
        if (this.treeNodeProvider == value)
          return;
        this.treeNodeProvider = value;
        this.OnNotifyPropertyChanged(nameof (TreeNodeProvider));
      }
    }

    public override BindingContext BindingContext
    {
      get
      {
        return this.bindingProvider.BindingContext;
      }
      set
      {
        if (this.bindingProvider.BindingContext == value)
          return;
        this.bindingProvider.BindingContext = value;
        this.OnBindingContextChanged(EventArgs.Empty);
      }
    }

    public virtual object DataSource
    {
      get
      {
        return this.bindingProvider.DataSource;
      }
      set
      {
        this.bindingProvider.DataSource = value;
      }
    }

    public RadTreeNodeCollection Nodes
    {
      get
      {
        return this.root.Nodes;
      }
    }

    [Description("Gets or sets the indent of nodes, applied to each tree level.")]
    public virtual int TreeIndent
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadTreeViewElement.TreeIndentProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.TreeIndentProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public virtual object Filter
    {
      get
      {
        if (this.filterDescriptors.Count > 0)
          return this.filterDescriptors[0].Value;
        return (object) null;
      }
      set
      {
        if (value == null)
          value = (object) string.Empty;
        if (string.IsNullOrEmpty((string) value))
        {
          if (this.filterDescriptors.Count <= 0)
            return;
          this.filterDescriptors.Clear();
        }
        else if (this.filterDescriptors.Count == 0)
          this.filterDescriptors.Add(new FilterDescriptor("Text", FilterOperator.Contains, value));
        else
          this.filterDescriptors[0].Value = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(SortOrder.None)]
    public SortOrder SortOrder
    {
      get
      {
        return this.sortOrder;
      }
      set
      {
        if (this.sortOrder == value)
          return;
        if (value == SortOrder.None)
        {
          if (this.SortDescriptors.Count > 0)
          {
            this.SortDescriptors[0].PropertyChanged -= new PropertyChangedEventHandler(this.RadTreeViewElement_PropertyChanged);
            this.SortDescriptors.RemoveAt(0);
          }
          this.sortOrder = SortOrder.None;
        }
        else
        {
          ListSortDirection direction = ListSortDirection.Ascending;
          this.sortOrder = SortOrder.Ascending;
          if (value == SortOrder.Descending)
          {
            direction = ListSortDirection.Descending;
            this.sortOrder = SortOrder.Descending;
          }
          if (this.SortDescriptors.Count == 0)
          {
            this.SortDescriptors.Add("Text", direction);
            this.SortDescriptors[0].PropertyChanged += new PropertyChangedEventHandler(this.RadTreeViewElement_PropertyChanged);
          }
          else
            this.SortDescriptors[0].Direction = direction;
        }
      }
    }

    private void RadTreeViewElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.UpdateNodes();
      this.Update(RadTreeViewElement.UpdateActions.SortChanged);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filterDescriptors;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.sortDescriptors;
      }
    }

    public virtual string DataMember
    {
      get
      {
        return this.bindingProvider.DataMember;
      }
      set
      {
        this.bindingProvider.DataMember = value;
      }
    }

    public virtual string ValueMember
    {
      get
      {
        return this.bindingProvider.ValueMember;
      }
      set
      {
        this.bindingProvider.ValueMember = value;
      }
    }

    public virtual string CheckedMember
    {
      get
      {
        return this.bindingProvider.CheckedMember;
      }
      set
      {
        this.bindingProvider.CheckedMember = value;
      }
    }

    public virtual string ChildMember
    {
      get
      {
        return this.bindingProvider.ChildMember;
      }
      set
      {
        this.bindingProvider.ChildMember = value;
      }
    }

    public virtual string DisplayMember
    {
      get
      {
        return this.bindingProvider.DisplayMember;
      }
      set
      {
        this.bindingProvider.DisplayMember = value;
      }
    }

    public virtual string ParentMember
    {
      get
      {
        return this.bindingProvider.ParentMember;
      }
      set
      {
        this.bindingProvider.ParentMember = value;
      }
    }

    public virtual TypeConverter ToggleStateConverter
    {
      get
      {
        return this.bindingProvider.ToggleStateConverter;
      }
      set
      {
        this.bindingProvider.ToggleStateConverter = value;
      }
    }

    [Localizable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the expand image ")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public Image ExpandImage
    {
      get
      {
        return (Image) this.GetValue(RadTreeViewElement.ExpandImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.ExpandImageProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the expand image ")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Image CollapseImage
    {
      get
      {
        return (Image) this.GetValue(RadTreeViewElement.CollapseImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.CollapseImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the hovered expand image ")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Appearance")]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Image HoveredExpandImage
    {
      get
      {
        return (Image) this.GetValue(RadTreeViewElement.HoveredExpandImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.HoveredExpandImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Localizable(true)]
    [Description("Gets or sets the expand image ")]
    [Category("Appearance")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RefreshProperties(RefreshProperties.All)]
    public Image HoveredCollapseImage
    {
      get
      {
        return (Image) this.GetValue(RadTreeViewElement.HoveredCollapseImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.HoveredCollapseImageProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether nodes can have different height.")]
    [Browsable(true)]
    [DefaultValue(false)]
    public virtual bool AllowArbitraryItemHeight
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.AllowArbitraryItemHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.AllowArbitraryItemHeightProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to select the full row.")]
    public virtual bool FullRowSelect
    {
      get
      {
        return (bool) this.GetValue(RadTreeViewElement.FullRowSelectProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.FullRowSelectProperty, (object) value);
      }
    }

    [Description("Gets or sets the vertical spacing among nodes.")]
    public virtual int NodeSpacing
    {
      get
      {
        return (int) this.GetValue(RadTreeViewElement.NodeSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.NodeSpacingProperty, (object) value);
      }
    }

    [Description("Gets or sets the alternating row color.")]
    public virtual Color AlternatingRowColor
    {
      get
      {
        return (Color) this.GetValue(RadTreeViewElement.AlternatingRowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTreeViewElement.AlternatingRowColorProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether to show rows with alternating row colors.")]
    public virtual bool AllowAlternatingRowColor
    {
      get
      {
        return this.allowAlternatingRowColor;
      }
      set
      {
        if (this.allowAlternatingRowColor == value)
          return;
        this.firstVisibleIndex = -1;
        this.allowAlternatingRowColor = value;
        this.OnNotifyPropertyChanged(nameof (AllowAlternatingRowColor));
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [Description("Gets the index of the first visible node.")]
    public int FirstVisibleIndex
    {
      get
      {
        if (this.firstVisibleIndex == -1)
          this.UpdateFirstVisibleIndex();
        return this.firstVisibleIndex;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [DefaultValue(ExpandMode.Multiple)]
    [Browsable(false)]
    public ExpandMode ExpandMode
    {
      get
      {
        return this.expandMode;
      }
      set
      {
        this.expandMode = value;
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    [Description("Gets or sets a property that controls the visibility of the horizontal scrollbar.")]
    [Browsable(true)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.horizontalScrollState;
      }
      set
      {
        if (this.horizontalScrollState == value)
          return;
        this.horizontalScrollState = value;
        this.InvalidateMeasure();
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    [Browsable(true)]
    [Description("Gets or sets a property that controls the visibility of the vertical scrollbar.")]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.Scroller.ScrollState;
      }
      set
      {
        this.Scroller.ScrollState = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or  a value indicating whether the control is in design mode.")]
    public bool IsInDesignMode
    {
      get
      {
        if (this.ElementTree == null || this.ElementTree.Control == null)
          return false;
        if (this.ElementTree.Control.Site == null)
          return this.ElementTree.Control.GetType().Name == "DesignTimeTreeView";
        return true;
      }
    }

    [Description("Gets or sets a value indicating whether to scroll horizontally RadTreeView to ensure that the clicked node is visible.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AutoScrollOnClick
    {
      get
      {
        return this.autoScrollOnClick;
      }
      set
      {
        this.autoScrollOnClick = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the default context menu is enabled.")]
    public bool AllowDefaultContextMenu
    {
      get
      {
        return this.allowDefaultContextMenu;
      }
      set
      {
        this.allowDefaultContextMenu = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Predicate<RadTreeNode> FilterPredicate
    {
      get
      {
        return this.filterPredicate;
      }
      set
      {
        if (!(this.filterPredicate != value))
          return;
        this.filterPredicate = value;
        this.UpdateNodes();
        this.Update(RadTreeViewElement.UpdateActions.Resume);
        this.OnNotifyPropertyChanged(nameof (FilterPredicate));
      }
    }

    public virtual bool KeyboardSearchEnabled { get; set; }

    public virtual int KeyboardSearchResetInterval
    {
      get
      {
        return this.typingTimer.Interval;
      }
      set
      {
        this.typingTimer.Interval = value;
      }
    }

    public virtual IFindStringComparer FindStringComparer
    {
      get
      {
        return this.findStringComparer;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("The FindStringComparer can not be set to null.");
        this.findStringComparer = value;
        this.OnNotifyPropertyChanged(nameof (FindStringComparer));
      }
    }

    public int ExpandTimerInterval
    {
      get
      {
        return this.expandTimer.Interval;
      }
      set
      {
        this.expandTimer.Interval = value;
      }
    }

    public void SetError(string text, RadTreeNode radTreeNode, params object[] context)
    {
      this.OnDataError(new TreeNodeDataErrorEventArgs(text, radTreeNode, context));
    }

    public virtual RadTreeNode AddNodeByPath(string path)
    {
      return this.AddNodeByPath(path, this.PathSeparator);
    }

    public virtual RadTreeNode AddNodeByPath(string path, string pathSeparator)
    {
      if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(pathSeparator))
        return (RadTreeNode) null;
      RadTreeNodeCollection nodes = this.Nodes;
      int length = path.LastIndexOf(pathSeparator);
      if (length != -1)
      {
        RadTreeNode nodeByPath = this.GetNodeByPath(path.Substring(0, length), pathSeparator);
        if (nodeByPath != null)
          nodes = nodeByPath.Nodes;
      }
      int startIndex = length + 1;
      string text = path.Substring(startIndex, path.Length - startIndex);
      return nodes.Add(text);
    }

    public virtual RadTreeNode GetNodeByPath(string path)
    {
      return this.GetNodeByPath(path, this.PathSeparator);
    }

    public virtual RadTreeNode GetNodeByPath(string path, string pathSeparator)
    {
      if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(pathSeparator))
        return (RadTreeNode) null;
      string[] strArray = path.Split(pathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      RadTreeNodeCollection nodes = this.Nodes;
      RadTreeNode radTreeNode = (RadTreeNode) null;
      foreach (string index in strArray)
      {
        radTreeNode = nodes[index];
        if (radTreeNode != null)
          nodes = radTreeNode.Nodes;
        else
          break;
      }
      return radTreeNode;
    }

    public RadTreeNode GetNodeByName(string name)
    {
      foreach (RadTreeNode node in (Collection<RadTreeNode>) this.Nodes)
      {
        RadTreeNode nodeByName = this.GetNodeByName(name, node);
        if (nodeByName != null)
          return nodeByName;
      }
      return (RadTreeNode) null;
    }

    public RadTreeNode GetNodeByName(string name, RadTreeNode rootNode)
    {
      Queue<RadTreeNode> radTreeNodeQueue = new Queue<RadTreeNode>();
      radTreeNodeQueue.Enqueue(rootNode);
      while (radTreeNodeQueue.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeQueue.Peek();
        radTreeNodeQueue.Dequeue();
        if (radTreeNode.Name == name)
          return radTreeNode;
        foreach (RadTreeNode node in (Collection<RadTreeNode>) radTreeNode.Nodes)
          radTreeNodeQueue.Enqueue(node);
      }
      return (RadTreeNode) null;
    }

    public virtual bool BeginEdit()
    {
      if (!this.allowEdit || this.activeEditor != null || (this.SelectedNodes.Count > 1 || this.SelectedNode == null))
        return false;
      TreeNodeEditorRequiredEventArgs e1 = new TreeNodeEditorRequiredEventArgs(this.SelectedNode, typeof (TreeViewTextBoxEditor));
      this.OnEditorRequired(e1);
      IInputEditor editor = e1.Editor as IInputEditor ?? this.GetEditor(e1.EditorType);
      if (editor == null)
        return false;
      this.activeEditor = editor;
      this.isBeginEdit = true;
      TreeNodeEditingEventArgs e2 = new TreeNodeEditingEventArgs(this.SelectedNode, (IValueEditor) editor);
      this.OnEditing(e2);
      if (e2.Cancel)
      {
        this.isBeginEdit = false;
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      this.SelectedNode.EnsureVisible();
      TreeNodeElement element = this.GetElement(this.SelectedNode);
      if (element == null)
      {
        this.isBeginEdit = false;
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      RadScrollBarElement scrollbar = this.Scroller.Scrollbar;
      bool flag = scrollbar.Value == scrollbar.Maximum - scrollbar.LargeChange + 1;
      element.AddEditor(editor);
      ISupportInitialize activeEditor1 = this.activeEditor as ISupportInitialize;
      activeEditor1?.BeginInit();
      object obj = this.EditMode == TreeNodeEditMode.Text ? (object) this.SelectedNode.Text : this.SelectedNode.Value;
      this.activeEditor.Initialize((object) element, obj);
      activeEditor1?.EndInit();
      this.OnEditorInitialized(new TreeNodeEditorInitializedEventArgs(element, (IValueEditor) editor));
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        BaseInputEditor activeEditor2 = this.activeEditor as BaseInputEditor;
        if (activeEditor2 != null)
          activeEditor2.EditorElement.StretchVertically = true;
      }
      this.activeEditor.BeginEdit();
      this.cachedOldValue = this.EditMode == TreeNodeEditMode.Text ? (object) this.SelectedNode.Text : this.SelectedNode.Value;
      if (flag)
        this.SelectedNode.EnsureVisible();
      this.isBeginEdit = false;
      return true;
    }

    public bool EndEdit()
    {
      return this.EndEditCore(true);
    }

    public void CancelEdit()
    {
      this.EndEditCore(false);
    }

    public void Update(RadTreeViewElement.UpdateActions updateAction)
    {
      if (this.updateSuspendedCount > 0)
      {
        if (updateAction == RadTreeViewElement.UpdateActions.Reset)
          this.resumeAction = RadTreeViewElement.UpdateActions.Reset;
        if (this.previousEndUpdateAction != RadTreeViewElement.UpdateActions.StateChanged || updateAction != RadTreeViewElement.UpdateActions.ExpandedChanged)
          return;
        this.previousEndUpdateAction = RadTreeViewElement.UpdateActions.Resume;
      }
      else
      {
        if (updateAction == RadTreeViewElement.UpdateActions.Reset)
        {
          this.HScrollBar.Value = this.HScrollBar.Minimum;
          this.VScrollBar.Value = this.VScrollBar.Minimum;
          this.ViewElement.SuspendLayout();
          while (this.ViewElement.Children.Count > 0)
          {
            TreeNodeElement child = (TreeNodeElement) this.ViewElement.Children[0];
            this.ViewElement.Children.Remove((RadElement) child);
            child.Detach();
          }
          this.ViewElement.ResumeLayout(false);
          this.ViewElement.ElementProvider.ClearCache();
          this.root.ChildrenSize = Size.Empty;
          this.resetSizes = true;
        }
        if (updateAction == RadTreeViewElement.UpdateActions.Reset || updateAction == RadTreeViewElement.UpdateActions.Resume || updateAction == RadTreeViewElement.UpdateActions.SortChanged)
        {
          this.UpdateScrollers((RadTreeNode) null, updateAction);
          if (this.ElementTree != null && this.ElementTree.Control.AutoSize)
            this.InvalidateMeasure(true);
        }
        this.ViewElement.UpdateItems();
        if (updateAction != RadTreeViewElement.UpdateActions.StateChanged && updateAction != RadTreeViewElement.UpdateActions.SortChanged && updateAction != RadTreeViewElement.UpdateActions.Resume)
          return;
        this.SynchronizeNodeElements();
      }
    }

    public void Update(RadTreeViewElement.UpdateActions updateAction, params RadTreeNode[] nodes)
    {
      if (this.updateSuspendedCount > 0)
        return;
      if (updateAction == RadTreeViewElement.UpdateActions.ExpandedChanged || updateAction == RadTreeViewElement.UpdateActions.ExpandedChangedUsingAnimation)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadTreeViewElement.UpdateActions.Resume;
          return;
        }
        if (!this.UpdateOnExpandedChanged(updateAction, nodes[0]))
          return;
      }
      if (updateAction == RadTreeViewElement.UpdateActions.ItemAdded)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadTreeViewElement.UpdateActions.Resume;
          return;
        }
        this.UpdateScrollersOnAdd(nodes[0]);
      }
      if (updateAction == RadTreeViewElement.UpdateActions.ItemRemoved || updateAction == RadTreeViewElement.UpdateActions.ItemMoved)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadTreeViewElement.UpdateActions.Resume;
          return;
        }
        this.UpdateScrollers((RadTreeNode) null, updateAction);
      }
      TreeNodeElement treeNodeElement = (TreeNodeElement) null;
      foreach (TreeNodeElement child in this.ViewElement.Children)
      {
        if (child.Data == nodes[0])
        {
          treeNodeElement = child;
          break;
        }
      }
      if (treeNodeElement == null)
        return;
      if (updateAction == RadTreeViewElement.UpdateActions.NodeStateChanged)
        treeNodeElement.Synchronize();
      else
        this.Update(updateAction);
    }

    protected virtual bool UpdateOnExpandedChanged(
      RadTreeViewElement.UpdateActions updateAction,
      RadTreeNode node)
    {
      if (updateAction == RadTreeViewElement.UpdateActions.ExpandedChangedUsingAnimation && this.ExpandAnimation != ExpandAnimation.None)
      {
        TreeExpandAnimation expandAnimation = this.CreateExpandAnimation();
        TreeNodeElement element = this.GetElement(node);
        if (expandAnimation != null && element != null)
        {
          if (node.Expanded)
            expandAnimation.Expand(node);
          else
            expandAnimation.Collapse(node);
          return false;
        }
      }
      this.UpdateOnExpandedChangedCore(node);
      return true;
    }

    protected virtual TreeExpandAnimation CreateExpandAnimation()
    {
      if (this.ExpandAnimation == ExpandAnimation.Opacity)
        return (TreeExpandAnimation) new TreeExpandAnimationOpacity(this);
      return (TreeExpandAnimation) null;
    }

    protected virtual void UpdateOnExpandedChangedCore(RadTreeNode node)
    {
      if (node.Expanded)
      {
        this.UpdateScrollersOnExpand(node);
        if (this.ExpandMode != ExpandMode.Single)
          return;
        foreach (RadTreeNode node1 in (Collection<RadTreeNode>) (node.Parent ?? this.root).Nodes)
        {
          if (node1 != node)
            node1.Expanded = false;
        }
      }
      else
        this.UpdateScrollersOnCollapse(node);
    }

    public void BeginUpdate()
    {
      ++this.updateSuspendedCount;
      this.previousEndUpdateAction = RadTreeViewElement.UpdateActions.StateChanged;
    }

    public void EndUpdate()
    {
      this.EndUpdate(true, this.resumeAction);
      this.resumeAction = RadTreeViewElement.UpdateActions.Resume;
    }

    public void EndUpdate(bool performUpdate, RadTreeViewElement.UpdateActions action)
    {
      if (action < this.previousEndUpdateAction)
        this.previousEndUpdateAction = action;
      if (this.updateSuspendedCount == 0)
        return;
      --this.updateSuspendedCount;
      if (this.updateSuspendedCount != 0 || !performUpdate)
        return;
      if (action > this.previousEndUpdateAction)
        action = this.previousEndUpdateAction;
      if (action <= RadTreeViewElement.UpdateActions.ItemEdited)
        this.UpdateNodes();
      if (this.ElementState == ElementState.Unloaded)
      {
        this.pendingScrollerUpdates = RadTreeViewElement.UpdateActions.Reset;
      }
      else
      {
        this.Update(action);
        if (this.nodeToEnsureVisible == null)
          return;
        this.EnsureVisible(this.nodeToEnsureVisible);
        this.nodeToEnsureVisible = (RadTreeNode) null;
      }
    }

    private void RefreshSource(RadTreeViewElement.UpdateActions action)
    {
      if (action == RadTreeViewElement.UpdateActions.ExpandedChanged || action == RadTreeViewElement.UpdateActions.ExpandedChangedUsingAnimation || action == RadTreeViewElement.UpdateActions.Resume)
        ;
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new RadTreeViewElement.DeferHelper(this);
    }

    public void CollapseAll()
    {
      this.BeginUpdate();
      for (int index = 0; index < this.Nodes.Count; ++index)
        this.Nodes[index].Collapse();
      this.EndUpdate();
    }

    public void ExpandAll()
    {
      this.BeginUpdate();
      for (int index = 0; index < this.Nodes.Count; ++index)
        this.Nodes[index].ExpandAll();
      this.EndUpdate();
    }

    public IEnumerable<RadTreeNode> GetNodes()
    {
      if (this.Root.Nodes != null)
      {
        foreach (RadTreeNode node in (Collection<RadTreeNode>) this.Root.Nodes)
        {
          foreach (RadTreeNode descendantNode in this.GetDescendantNodes(node))
            yield return descendantNode;
        }
      }
    }

    public IEnumerable<RadTreeNode> GetDescendantNodes(RadTreeNode current)
    {
      if (current != null)
      {
        yield return current;
        if (!this.IsLazyLoading && current.Nodes != null || this.IsLazyLoading && current.nodes != null)
        {
          foreach (RadTreeNode current1 in this.IsLazyLoading ? (Collection<RadTreeNode>) current.nodes : (Collection<RadTreeNode>) current.Nodes)
          {
            foreach (RadTreeNode descendantNode in this.GetDescendantNodes(current1))
              yield return descendantNode;
          }
        }
      }
    }

    public RadTreeNode GetNodeAt(int x, int y)
    {
      return this.GetNodeElementAt(x, y)?.Data;
    }

    public RadTreeNode GetNodeAt(Point pt)
    {
      return this.GetNodeAt(pt.X, pt.Y);
    }

    public TreeNodeElement GetNodeElementAt(int x, int y)
    {
      if (this.ElementTree == null)
        return (TreeNodeElement) null;
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(new Point(x, y));
      if (elementAtPoint == null)
        return (TreeNodeElement) null;
      if (elementAtPoint is TreeNodeElement)
        return (TreeNodeElement) elementAtPoint;
      return elementAtPoint.FindAncestor<TreeNodeElement>() ?? (TreeNodeElement) null;
    }

    public TreeNodeElement GetNodeElementAt(Point pt)
    {
      return this.GetNodeElementAt(pt.X, pt.Y);
    }

    public int GetNodeCount(bool includeSubTrees)
    {
      int num = 0;
      if (includeSubTrees)
      {
        for (int index = 0; index < this.Nodes.Count; ++index)
          num += this.Nodes[index].GetNodeCount(true);
      }
      return num + this.Nodes.Count;
    }

    public RadTreeNode Find(Predicate<RadTreeNode> match)
    {
      return this.Root.Find(match);
    }

    public RadTreeNode Find<T>(FindAction<T> match, T arg)
    {
      return this.Root.Find<T>(match, arg);
    }

    public virtual RadTreeNode Find(string text)
    {
      this.search = (object) text;
      return this.Root.Find(new Predicate<RadTreeNode>(this.FindByText));
    }

    public RadTreeNode[] FindNodes(Predicate<RadTreeNode> match)
    {
      return this.Root.FindNodes(match);
    }

    public RadTreeNode[] FindNodes<T>(FindAction<T> match, T arg)
    {
      return this.Root.FindNodes<T>(match, arg);
    }

    public virtual RadTreeNode[] FindNodes(string text)
    {
      this.search = (object) text;
      return this.Root.FindNodes(new Predicate<RadTreeNode>(this.FindByText));
    }

    public void ForEach(Action<RadTreeNode> action)
    {
      this.Root.ForEach(action);
    }

    public object Execute(ICommand command, params object[] settings)
    {
      return this.Execute(true, command, settings);
    }

    public object Execute(bool includeSubTrees, ICommand command, params object[] settings)
    {
      return this.Root.Execute(includeSubTrees, command, settings);
    }

    public void ScrollTo(int delta)
    {
      int num = this.VScrollBar.Value - delta * this.VScrollBar.SmallChange;
      if (num > this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
        num = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
      if (num < this.VScrollBar.Minimum)
        num = 0;
      else if (num > this.VScrollBar.Maximum)
        num = this.VScrollBar.Maximum;
      this.VScrollBar.Value = num;
    }

    public virtual void EnsureVisible(RadTreeNode node)
    {
      TreeNodeElement element = this.GetElement(node);
      if (this.updateSuspendedCount > 0)
      {
        this.nodeToEnsureVisible = node;
      }
      else
      {
        TreeNodeElement nodeElement = this.EnsureNodeVisibleVertical(node, element);
        this.EnsureNodeVisibleHorizontal(node, nodeElement);
        this.UpdateLayout();
      }
    }

    public void BringIntoView(RadTreeNode node)
    {
      if (node == null)
        return;
      RadTreeViewElement.UpdateActions action = RadTreeViewElement.UpdateActions.StateChanged;
      this.BeginUpdate();
      for (RadTreeNode parent = node.Parent; parent != null; parent = parent.Parent)
      {
        if (!parent.Expanded)
        {
          parent.Expand();
          action = RadTreeViewElement.UpdateActions.Resume;
        }
      }
      this.EndUpdate(true, action);
      this.EnsureVisible(node);
    }

    public virtual void ClearSelection()
    {
      this.selectedNodeCollection.Clear();
    }

    public virtual void SelectAll()
    {
      if (!this.MultiSelect || this.root.Nodes.Count <= 0)
        return;
      this.BeginUpdate();
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
      while (treeViewTraverser.MoveNext())
        treeViewTraverser.Current.Selected = true;
      this.EndUpdate(true, RadTreeViewElement.UpdateActions.StateChanged);
    }

    public void BeginInit()
    {
      this.bindingProvider.SuspendUpdate();
    }

    public virtual void EndInit()
    {
      this.bindingProvider.ResumeUpdate();
      this.bindingProvider.Reset();
    }

    void IDataItemSource.Initialize()
    {
    }

    IDataItem IDataItemSource.NewItem()
    {
      return (IDataItem) this.CreateNewNode();
    }

    void IDataItemSource.BindingComplete()
    {
    }

    void IDataItemSource.MetadataChanged(PropertyDescriptor pd)
    {
    }

    protected internal virtual RadTreeNode CreateNewNode()
    {
      return this.CreateNewNode((string) null);
    }

    protected internal virtual RadTreeNode CreateNewNode(string defaultText)
    {
      CreateTreeNodeEventArgs e = new CreateTreeNodeEventArgs();
      this.OnCreateNode(e);
      if (e.Node != null)
        return e.Node;
      return new RadTreeNode(defaultText);
    }

    internal bool IsScrollBar(Point point)
    {
      if (this.ElementTree == null)
        return false;
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(point);
      if (elementAtPoint != null)
        return elementAtPoint.FindAncestor<RadScrollBarElement>() != null;
      return false;
    }

    internal RadTreeNode Root
    {
      get
      {
        return this.root;
      }
    }

    protected object CachedOldValue
    {
      get
      {
        return this.cachedOldValue;
      }
    }

    internal bool DisableEnsureNodeVisibleHorizontal
    {
      get
      {
        return this.disableEnsureNodeVisibleHorizontal;
      }
    }

    internal bool IsSuspended
    {
      get
      {
        return this.updateSuspendedCount > 0;
      }
    }

    internal bool FullLazyMode
    {
      get
      {
        if (this.LazyMode)
          return this.NodesNeeded != null;
        return false;
      }
    }

    internal bool IsLazyLoading
    {
      get
      {
        return this.NodesNeeded != null;
      }
    }

    private RadTreeNode GetNodeAt(Point location, bool fullRowSelect)
    {
      if (fullRowSelect)
        return this.GetNodeAt(location);
      TreeNodeElement nodeElementAt = this.GetNodeElementAt(location);
      if (nodeElementAt == null)
        return (RadTreeNode) null;
      Rectangle a = Rectangle.Empty;
      foreach (RadElement child in nodeElementAt.Children)
        a = Rectangle.Union(a, child.ControlBoundingRectangle);
      if (a.Contains(location))
        return nodeElementAt.Data;
      return (RadTreeNode) null;
    }

    private bool FindByText(RadTreeNode node)
    {
      string strA = this.search != null ? this.search.ToString() : (string) null;
      if (strA == null || node.Text == null || strA.Length != node.Text.Length)
        return false;
      return string.Compare(strA, node.Text, true, CultureInfo.InvariantCulture) == 0;
    }

    private void DisableCheckBoxes()
    {
      if (this.Nodes.Count == 0)
        return;
      Queue<RadTreeNodeCollection> treeNodeCollectionQueue = new Queue<RadTreeNodeCollection>();
      treeNodeCollectionQueue.Enqueue(this.Nodes);
      while (treeNodeCollectionQueue.Count > 0)
      {
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) treeNodeCollectionQueue.Dequeue())
        {
          if (radTreeNode.CheckType == CheckType.CheckBox)
            radTreeNode.CheckType = CheckType.None;
          if (radTreeNode.Nodes.Count > 0)
            treeNodeCollectionQueue.Enqueue(radTreeNode.Nodes);
        }
      }
    }

    private bool IsEditorHint(Point point)
    {
      BaseInputEditor activeEditor = this.activeEditor as BaseInputEditor;
      if (activeEditor != null)
        return activeEditor.EditorElement.ControlBoundingRectangle.Contains(point);
      return false;
    }

    private bool IsExpander(Point point)
    {
      if (this.ElementTree == null)
        return false;
      return this.ElementTree.GetElementAtPoint(point) is ExpanderItem;
    }

    private bool IsToggle(Point point)
    {
      if (this.ElementTree == null)
        return false;
      for (RadElement radElement = this.ElementTree.GetElementAtPoint(point); radElement != null; radElement = radElement.Parent)
      {
        if (radElement is RadToggleButtonElement)
          return true;
      }
      return false;
    }

    protected override IVirtualizedElementProvider<RadTreeNode> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<RadTreeNode>) new TreeViewElementProvider(this);
    }

    protected virtual void UpdateScrollers(
      RadTreeNode skipNode,
      RadTreeViewElement.UpdateActions updateAction)
    {
      if (this.root.Nodes.Count > 0)
      {
        this.resetSizes = true;
        this.root.ChildrenSize = Size.Empty;
        this.root.ActualSize = Size.Empty;
        this.UpdateActualSize(this.root.nodes[0], false, skipNode);
        this.UpdateHScrollbarMaximum(this.root.ChildrenSize.Width);
        this.resetSizes = false;
        if (updateAction == RadTreeViewElement.UpdateActions.Reset || updateAction == RadTreeViewElement.UpdateActions.Resume)
        {
          if (this.Scroller.Traverser.Current != null && this.Scroller.Traverser.Current.TreeViewElement != this)
          {
            this.Scroller.Traverser.Reset();
            this.Scroller.UpdateScrollRange();
            this.Scroller.UpdateScrollValue();
          }
          else
            this.Scroller.UpdateScrollRange();
        }
        else
          this.Scroller.UpdateScrollRange(this.root.ChildrenSize.Height, true);
        if (updateAction != RadTreeViewElement.UpdateActions.SortChanged)
          return;
        this.Scroller.ScrollToItem(this.Scroller.Traverser.Current);
      }
      else
      {
        this.HScrollBar.Value = this.HScrollBar.Minimum;
        this.HScrollBar.Maximum = this.HScrollBar.Minimum;
        this.VScrollBar.Value = this.VScrollBar.Minimum;
        this.VScrollBar.Maximum = this.VScrollBar.Minimum;
        this.UpdateHScrollbarVisibility();
        if (this.VerticalScrollState == ScrollState.AlwaysShow)
          this.VScrollBar.Visibility = ElementVisibility.Visible;
        else
          this.VScrollBar.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected virtual void UpdateActualSize(
      RadTreeNode node,
      bool stopOnSameLevel,
      RadTreeNode skipNode)
    {
      RadTreeNode radTreeNode = node;
      bool flag1 = false;
      bool flag2 = skipNode != null;
      TreeViewElementProvider elementProvider = (TreeViewElementProvider) this.ViewElement.ElementProvider;
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this, node);
      while (node != null)
      {
        if (flag2 && node == skipNode)
        {
          node = node.NextNode;
        }
        else
        {
          if (this.resetSizes)
          {
            node.SuspendPropertyNotifications();
            node.ActualSize = Size.Empty;
            node.ChildrenSize = Size.Empty;
            node.ResumePropertyNotifications();
          }
          if (node.ActualSize != Size.Empty || !node.Visible && !this.IsInDesignMode)
          {
            treeViewTraverser.MoveNext();
            node = treeViewTraverser.Current;
            if (node != null && node.Level == radTreeNode.Level && stopOnSameLevel)
              break;
          }
          else
          {
            if (!flag1)
            {
              flag1 = true;
              this.ViewElement.SuspendLayout();
              if (this.ElementTree != null && this.ElementTree.Control != null)
                ((RadControl) this.ElementTree.Control).SuspendUpdate();
            }
            if (!this.AllowArbitraryItemHeight && !this.AutoSizeItems || !this.IsNodeVisible(node))
            {
              if (node.Expanded && node.nodes != null && node.nodes.NeedsRefresh)
              {
                int num = node.HasNodes ? 1 : 0;
              }
              node.ActualSize = new Size(node.ActualSize.Width, this.ItemHeight);
            }
            else
            {
              TreeNodeElement element = (TreeNodeElement) elementProvider.GetElement(node, (object) null);
              this.ViewElement.Children.Add((RadElement) element);
              element.Attach(node, (object) null);
              element.Measure(new SizeF(element.ContentElement.TextWrap ? (float) this.ViewElement.Size.Width : float.PositiveInfinity, float.PositiveInfinity));
              this.ViewElement.Children.Remove((RadElement) element);
              elementProvider.CacheElement((IVirtualizedElement<RadTreeNode>) element);
              element.Detach();
            }
            if (this.resetSizes && !node.Expanded)
              this.ResetChildrenSize(node);
            treeViewTraverser.MoveNext();
            node = treeViewTraverser.Current;
            if (node != null && node.Level == radTreeNode.Level && stopOnSameLevel)
              break;
          }
        }
      }
      if (flag1)
      {
        this.ViewElement.ResumeLayout(false, true);
        if (this.ElementTree != null && this.ElementTree.Control != null)
          ((RadControl) this.ElementTree.Control).ResumeUpdate();
      }
      this.resetSizes = false;
    }

    protected virtual void UpdateScrollersOnAdd(RadTreeNode node)
    {
      if (!this.IsNodeVisible(node))
      {
        this.SynchronizeNodeElements();
      }
      else
      {
        this.UpdateActualSize(node, true, (RadTreeNode) null);
        this.Scroller.UpdateScrollRange(this.root.ChildrenSize.Height, false);
        this.UpdateHScrollbarMaximum(this.root.ChildrenSize.Width);
        this.SynchronizeNodeElements();
      }
    }

    protected internal virtual void UpdateScrollersOnNodesNeeded(RadTreeNode node)
    {
      if (!this.IsNodeVisible(node))
      {
        this.SynchronizeNodeElements();
      }
      else
      {
        this.UpdateActualSize(node, false, (RadTreeNode) null);
        this.Scroller.UpdateScrollRange(this.root.ChildrenSize.Height, false);
        this.UpdateHScrollbarMaximum(this.root.ChildrenSize.Width);
      }
    }

    protected virtual void UpdateScrollersOnExpand(RadTreeNode node)
    {
      if (node.UpdateActualSizeOnExpand)
      {
        this.UpdateActualSize(node, true, (RadTreeNode) null);
        node.UpdateActualSizeOnExpand = false;
      }
      this.Scroller.UpdateScrollRange(this.root.ChildrenSize.Height, false);
      this.UpdateHScrollbarMaximum(this.root.ChildrenSize.Width);
    }

    protected virtual void UpdateScrollersOnCollapse(RadTreeNode node)
    {
      this.Scroller.UpdateScrollRange(this.VScrollBar.Maximum - node.ChildrenSize.Height, false);
      this.UpdateHScrollbarMaximum(this.root.ChildrenSize.Width);
      int newValue = this.Scroller.Scrollbar.Value;
      this.Scroller.Scrollbar.Value = 0;
      this.SetScrollValue(this.Scroller.Scrollbar, newValue);
    }

    internal void UpdateHScrollbarMaximum(int newMaximum)
    {
      if (this.HScrollBar.Maximum == newMaximum)
        return;
      this.HScrollBar.Maximum = newMaximum;
      this.UpdateHScrollbarVisibility();
    }

    protected virtual bool EndEditCore(bool commitChanges)
    {
      if (!this.IsEditing || this.IsPerformingEndEdit)
        return false;
      TreeNodeElement element = this.GetElement(this.SelectedNode);
      if (element == null)
        return false;
      this.IsPerformingEndEdit = true;
      if (commitChanges && this.ActiveEditor.IsModified)
        this.SaveEditorValue(element, this.ActiveEditor.Value);
      this.activeEditor.EndEdit();
      element.RemoveEditor(this.activeEditor);
      this.InvalidateMeasure();
      this.UpdateLayout();
      this.OnEdited(new TreeNodeEditedEventArgs(element, (IValueEditor) this.activeEditor, !commitChanges));
      this.activeEditor = (IInputEditor) null;
      this.IsPerformingEndEdit = false;
      return false;
    }

    protected virtual IInputEditor GetEditor(System.Type editorType)
    {
      IInputEditor inputEditor = (IInputEditor) null;
      if (!this.cachedEditors.TryGetValue(editorType, out inputEditor))
        inputEditor = Activator.CreateInstance(editorType) as IInputEditor;
      if (inputEditor != null && !this.cachedEditors.ContainsValue(inputEditor))
        this.cachedEditors.Add(editorType, inputEditor);
      return inputEditor;
    }

    protected virtual void SaveEditorValue(TreeNodeElement nodeElement, object newValue)
    {
      if (object.Equals(newValue, (object) string.Empty))
        newValue = (object) null;
      TreeNodeValidatingEventArgs e1 = new TreeNodeValidatingEventArgs(nodeElement, this.cachedOldValue, newValue);
      this.OnValueValidating(e1);
      if (e1.Cancel)
      {
        this.OnValidationError(EventArgs.Empty);
      }
      else
      {
        newValue = e1.NewValue;
        TreeNodeValueChangingEventArgs e2 = new TreeNodeValueChangingEventArgs(this.SelectedNode, newValue, this.cachedOldValue);
        this.OnValueChanging(e2);
        if (e2.Cancel)
          return;
        this.SuspendProvider();
        this.SelectedNode.SuspendPropertyNotifications();
        if (this.EditMode == TreeNodeEditMode.Value)
          this.SelectedNode.Value = e2.NewValue;
        else if (e2.NewValue != null)
          this.SelectedNode.Text = e2.NewValue.ToString();
        if (this.SelectedNode.parent != null)
          this.SelectedNode.parent.nodes.Update();
        this.SelectedNode.ResumePropertyNotifications();
        this.OnValueChanged(new TreeNodeValueChangedEventArgs(this.SelectedNode));
        this.ResumeProvider();
      }
    }

    protected internal void ResumeProvider()
    {
      if (this.treeNodeProvider == null)
        return;
      this.treeNodeProvider.ResumeUpdate();
    }

    protected internal void SuspendProvider()
    {
      if (this.treeNodeProvider == null)
        return;
      this.treeNodeProvider.SuspendUpdate();
    }

    internal void SetScrollValue(RadScrollBarElement scrollbar, int newValue)
    {
      if (newValue > scrollbar.Maximum - scrollbar.LargeChange + 1)
        newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (newValue < scrollbar.Minimum)
        newValue = scrollbar.Minimum;
      scrollbar.Value = newValue;
    }

    private Rectangle CreateSelectionRectangle(Point currentMouseLocation)
    {
      int width = Math.Abs(this.mouseDownLocation.X - currentMouseLocation.X);
      int height = Math.Abs(this.mouseDownLocation.Y - currentMouseLocation.Y);
      if (width == 0)
        width = 1;
      if (height == 0)
        height = 1;
      return new Rectangle(Math.Min(this.mouseDownLocation.X, currentMouseLocation.X), Math.Min(this.mouseDownLocation.Y, currentMouseLocation.Y), width, height);
    }

    private TreeNodeElement GetLastFullVisibleElement()
    {
      for (int index = this.ViewElement.Children.Count - 1; index >= 0; --index)
      {
        TreeNodeElement child = (TreeNodeElement) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
          return child;
      }
      return (TreeNodeElement) null;
    }

    private TreeNodeElement GetLastPartialVisibleElement()
    {
      for (int index = this.ViewElement.Children.Count - 1; index >= 0; --index)
      {
        TreeNodeElement child = (TreeNodeElement) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Top < this.ViewElement.ControlBoundingRectangle.Bottom)
          return child;
      }
      return (TreeNodeElement) null;
    }

    private TreeNodeElement GetFirstFullVisibleElement()
    {
      for (int index = 0; index < this.ViewElement.Children.Count - 1; ++index)
      {
        TreeNodeElement child = (TreeNodeElement) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Top >= this.ViewElement.ControlBoundingRectangle.Top)
          return child;
      }
      return (TreeNodeElement) null;
    }

    private TreeNodeElement GetFirstPartialVisibleElement()
    {
      for (int index = 0; index < this.ViewElement.Children.Count - 1; ++index)
      {
        TreeNodeElement child = (TreeNodeElement) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Bottom > this.ViewElement.ControlBoundingRectangle.Top)
          return child;
      }
      return (TreeNodeElement) null;
    }

    private bool IsNodeVisible(RadTreeNode node)
    {
      if (!node.Visible && !this.IsInDesignMode)
        return false;
      for (node = node.Parent; node != null; node = node.Parent)
      {
        if (!node.Expanded || !node.Visible && !this.IsInDesignMode)
          return false;
      }
      return true;
    }

    private void ResetChildrenSize(RadTreeNode node)
    {
      if (node.Expanded || node.nodes == null)
        return;
      foreach (RadTreeNode node1 in (Collection<RadTreeNode>) node.nodes)
      {
        node1.SuspendPropertyNotifications();
        node1.ActualSize = Size.Empty;
        node1.ChildrenSize = Size.Empty;
        node1.ResumePropertyNotifications();
        this.ResetChildrenSize(node1);
      }
    }

    protected virtual TreeNodeElement EnsureNodeVisibleVertical(
      RadTreeNode node,
      TreeNodeElement nodeElement)
    {
      if (nodeElement == null)
      {
        this.UpdateLayout();
        if (this.ViewElement.Children.Count > 0)
        {
          if (this.GetNodeIndex(node) <= this.GetNodeIndex(((TreeNodeElement) this.ViewElement.Children[0]).Data))
            this.Scroller.ScrollToItem(node);
          else
            nodeElement = this.EnsureNodeVisibleVerticalCore(node);
        }
      }
      else if (nodeElement.ControlBoundingRectangle.Bottom > this.ViewElement.ControlBoundingRectangle.Bottom)
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + (nodeElement.ControlBoundingRectangle.Bottom - this.ViewElement.ControlBoundingRectangle.Bottom));
      else if (nodeElement.ControlBoundingRectangle.Top < this.ViewElement.ControlBoundingRectangle.Top)
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - (this.ViewElement.ControlBoundingRectangle.Top - nodeElement.ControlBoundingRectangle.Top));
      return nodeElement;
    }

    protected virtual TreeNodeElement EnsureNodeVisibleVerticalCore(RadTreeNode node)
    {
      bool flag = false;
      int num = 0;
      RadTreeNode data = ((TreeNodeElement) this.ViewElement.Children[this.ViewElement.Children.Count - 1]).Data;
      TreeViewTraverser enumerator = (TreeViewTraverser) this.Scroller.Traverser.GetEnumerator();
      TreeNodeElement treeNodeElement = (TreeNodeElement) null;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == node)
        {
          int maximum = this.VScrollBar.Maximum;
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num);
          this.UpdateLayout();
          treeNodeElement = this.GetElement(node);
          if (treeNodeElement != null && treeNodeElement.ControlBoundingRectangle.Bottom > this.ViewElement.ControlBoundingRectangle.Bottom)
          {
            this.EnsureVisible(node);
            break;
          }
          break;
        }
        if (enumerator.Current == data)
          flag = true;
        if (flag)
          num += (int) this.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Height + this.NodeSpacing;
      }
      return treeNodeElement;
    }

    protected virtual void EnsureNodeVisibleHorizontal(
      RadTreeNode node,
      TreeNodeElement nodeElement)
    {
      if (this.HScrollBar.Visibility != ElementVisibility.Visible || this.disableEnsureNodeVisibleHorizontal || nodeElement != null && nodeElement.ContentElement.ControlBoundingRectangle.Left > this.ViewElement.ControlBoundingRectangle.Left && nodeElement.ContentElement.ControlBoundingRectangle.Right < this.ViewElement.ControlBoundingRectangle.Right || nodeElement != null && nodeElement.ContentElement.ControlBoundingRectangle.Width > this.ViewElement.ControlBoundingRectangle.Width)
        return;
      if (nodeElement != null && this.HScrollBar.Value + this.HScrollBar.LargeChange + 1 < nodeElement.ContentElement.ControlBoundingRectangle.Right)
      {
        this.SetScrollValue(this.HScrollBar, this.HScrollBar.Value + (node.ActualSize.Width - (this.HScrollBar.Value + this.HScrollBar.LargeChange)));
      }
      else
      {
        int level = node.Level;
        int num = node.ActualSize.Width - this.TreeIndent * level;
        this.SetScrollValue(this.HScrollBar, node.ActualSize.Width - num);
      }
    }

    private bool EvalFilter(RadTreeNode node)
    {
      if (this.filterDescriptors.Count == 0)
        return true;
      ExpressionContext context = ExpressionContext.Context;
      context.Clear();
      PropertyDescriptorCollection descriptorCollection = node == null || node.DataBoundItem == null ? (PropertyDescriptorCollection) null : TypeDescriptor.GetProperties(node.DataBoundItem);
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.filterDescriptors)
      {
        if (descriptorCollection != null && descriptorCollection[filterDescriptor.PropertyName] != null)
          context.Add(filterDescriptor.PropertyName, descriptorCollection[filterDescriptor.PropertyName].GetValue(node.DataBoundItem));
        else
          context.Add(filterDescriptor.PropertyName, (object) node.Text);
      }
      if (this.expressionNode == null)
        this.expressionNode = DataUtils.Parse(this.filterDescriptors.Expression, false);
      object obj1 = this.expressionNode.Eval((object) null, (object) context);
      if (obj1 is bool && (bool) obj1)
        return true;
      Stack<RadTreeNode> radTreeNodeStack = new Stack<RadTreeNode>();
      if (node.HasNodes)
        radTreeNodeStack.Push(node);
      while (radTreeNodeStack.Count > 0)
      {
        foreach (RadTreeNode node1 in (Collection<RadTreeNode>) radTreeNodeStack.Pop().nodes)
        {
          context.Clear();
          foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.filterDescriptors)
            context.Add(filterDescriptor.PropertyName, (object) node1.Text);
          object obj2 = this.expressionNode.Eval((object) null, (object) context);
          if (obj2 is bool && (bool) obj2)
            return true;
          if (node1.HasNodes)
            radTreeNodeStack.Push(node1);
        }
      }
      return false;
    }

    private bool IsSameLetter(StringBuilder sb)
    {
      for (int index = 1; index < sb.Length; ++index)
      {
        if ((int) sb[index] != (int) sb[index - 1])
          return false;
      }
      return true;
    }

    protected virtual RadTreeNode GetFirstMatch(
      string searchCriteria,
      RadTreeNodeCollection nodes)
    {
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
      RadTreeNode radTreeNode = this.SelectedNode;
      bool flag = false;
      if (radTreeNode == null)
      {
        treeViewTraverser.MoveTo(0);
        radTreeNode = treeViewTraverser.Current;
      }
      else
        treeViewTraverser.MoveTo(radTreeNode);
      treeViewTraverser.MoveTo(radTreeNode);
      while (treeViewTraverser.Current == null || string.Equals(this.lastSearchCriteria, searchCriteria) || !this.FindStringComparer.Compare(treeViewTraverser.Current.Text, searchCriteria))
      {
        while (treeViewTraverser.MoveNext())
        {
          if (this.FindStringComparer.Compare(treeViewTraverser.Current.Text, searchCriteria))
          {
            this.lastSearchCriteria = searchCriteria;
            return treeViewTraverser.Current;
          }
        }
        if (!flag)
        {
          treeViewTraverser.Reset();
          flag = true;
          if (treeViewTraverser.Current != radTreeNode)
            continue;
        }
        this.lastSearchCriteria = searchCriteria;
        return (RadTreeNode) null;
      }
      this.lastSearchCriteria = searchCriteria;
      return treeViewTraverser.Current;
    }

    private void ProcessSelection(
      RadTreeNode node,
      Keys modifierKeys,
      bool isMouseSelection,
      RadTreeViewAction selectionAction)
    {
      if (node == null || this.updateSelectionChanged > 0)
        return;
      ++this.updateSelectionChanged;
      this.BeginUpdate();
      RadTreeViewElement.UpdateActions previousEndUpdateAction = this.previousEndUpdateAction;
      this.previousEndUpdateAction = RadTreeViewElement.UpdateActions.StateChanged;
      bool flag1 = (modifierKeys & Keys.Shift) == Keys.Shift;
      bool flag2 = (modifierKeys & Keys.Control) == Keys.Control;
      bool clearSelection = this.MultiSelect && (flag1 || !flag2 || !isMouseSelection && !flag1 && !flag2);
      bool flag3 = false;
      if (!node.Current)
      {
        ++this.updateSelectedNodeChanged;
        if (!this.ProcessCurrentNode(node, clearSelection, selectionAction))
        {
          this.EndUpdate(false, RadTreeViewElement.UpdateActions.StateChanged);
          --this.updateSelectionChanged;
          --this.updateSelectedNodeChanged;
          return;
        }
        --this.updateSelectedNodeChanged;
        flag3 = true;
      }
      else if (clearSelection)
      {
        this.ClearSelection();
        node.Selected = true;
        this.anchorPosition = node;
        this.anchorIndex = -1;
      }
      if (this.MultiSelect)
      {
        if (!flag1)
        {
          this.anchorPosition = node;
          this.anchorIndex = -1;
          if (isMouseSelection)
          {
            bool selected = node.Selected;
            node.Selected = !flag2 || !node.Selected;
            flag3 = selected != node.Selected;
          }
          else if (!flag2)
            node.Selected = true;
        }
        else
        {
          if (this.anchorPosition == null)
            this.anchorPosition = node;
          if (this.anchorIndex == -1)
            this.anchorIndex = this.GetNodeIndex(this.anchorPosition);
          bool flag4 = this.anchorIndex < this.GetNodeIndex(node);
          RadTreeNode radTreeNode = this.anchorPosition;
          node.Selected = true;
          for (; radTreeNode != node && radTreeNode != null; radTreeNode = flag4 ? radTreeNode.NextVisibleNode : radTreeNode.PrevVisibleNode)
          {
            if (radTreeNode.Enabled)
              radTreeNode.Selected = true;
          }
        }
      }
      else
      {
        this.anchorPosition = node;
        this.anchorIndex = -1;
      }
      this.EndUpdate(true, RadTreeViewElement.UpdateActions.StateChanged);
      --this.updateSelectionChanged;
      this.previousEndUpdateAction = previousEndUpdateAction;
      if (!flag3)
        return;
      this.OnSelectedNodeChanged(node, selectionAction);
    }

    internal bool ProcessCurrentNode(
      RadTreeNode node,
      bool clearSelection,
      RadTreeViewAction action)
    {
      if (node != null && !node.Enabled)
        return false;
      if (this.updateCurrentNodeChanged > 0)
        return true;
      ++this.updateCurrentNodeChanged;
      RadTreeViewCancelEventArgs args = new RadTreeViewCancelEventArgs(node, action);
      this.OnSelectedNodeChanging(args);
      if (args.Cancel)
      {
        --this.updateCurrentNodeChanged;
        return false;
      }
      if (clearSelection)
        this.ClearSelection();
      if (this.selected != null)
        this.selected.Current = false;
      this.selected = node;
      if (this.updateSelectionChanged == 0)
      {
        this.anchorPosition = this.selected;
        this.anchorIndex = -1;
      }
      if (this.TreeNodeProvider != null)
        this.TreeNodeProvider.SetCurrent(this.selected);
      if (this.selected != null)
      {
        this.selected.Current = true;
        this.BringIntoView(this.selected);
      }
      --this.updateCurrentNodeChanged;
      this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      if (!clearSelection && !this.MultiSelect)
        this.OnSelectedNodesCleared();
      this.OnSelectedNodeChanged(node, action);
      return true;
    }

    private void OnSelectedNodeChanged(RadTreeNode node, RadTreeViewAction action)
    {
      if (this.updateSelectedNodeChanged > 0)
        return;
      this.OnNotifyPropertyChanged("SelectedNode");
      this.OnSelectedNodeChanged(new RadTreeViewEventArgs(node, action));
    }

    private bool ExtendSelectionUp(Point location)
    {
      if (location.Y >= this.ViewElement.ControlBoundingRectangle.Top)
        return false;
      RadTreeNode nodeAt = this.GetNodeAt(this.mouseDownLocation);
      if (nodeAt == null || this.VScrollBar.Value == this.VScrollBar.Minimum)
        return false;
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
      treeViewTraverser.Position = (object) nodeAt;
      Rectangle boundingRectangle = this.ViewElement.ControlBoundingRectangle;
      int num = this.ViewElement.ControlBoundingRectangle.Top - location.Y;
      this.VScrollBar.Value = this.ClampValue(this.VScrollBar.Value - num, this.VScrollBar.Minimum, this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1);
      this.mouseDownLocation.Y = this.ClampValue(this.mouseDownLocation.Y + num, boundingRectangle.Y, boundingRectangle.Bottom - 2);
      this.UpdateLayout();
      TreeNodeElement partialVisibleElement = this.GetFirstPartialVisibleElement();
      if (partialVisibleElement != null)
      {
        while (treeViewTraverser.Current != null)
        {
          treeViewTraverser.Current.Selected = true;
          if (treeViewTraverser.Current == partialVisibleElement.Data || !treeViewTraverser.MovePrevious())
            break;
        }
      }
      return true;
    }

    private bool ExtendSelectionDown(Point location)
    {
      if (location.Y <= this.ViewElement.ControlBoundingRectangle.Bottom)
        return false;
      RadTreeNode nodeAt = this.GetNodeAt(this.mouseDownLocation);
      if (nodeAt == null || this.VScrollBar.Value >= this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
        return false;
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
      treeViewTraverser.Position = (object) nodeAt;
      Rectangle boundingRectangle = this.ViewElement.ControlBoundingRectangle;
      int num = location.Y - boundingRectangle.Bottom;
      this.VScrollBar.Value = this.ClampValue(this.VScrollBar.Value + num, this.VScrollBar.Minimum, this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1);
      this.mouseDownLocation.Y = this.ClampValue(this.mouseDownLocation.Y - num, boundingRectangle.Y + 1, boundingRectangle.Bottom);
      this.UpdateLayout();
      TreeNodeElement partialVisibleElement = this.GetLastPartialVisibleElement();
      if (partialVisibleElement != null)
      {
        do
        {
          treeViewTraverser.Current.Selected = true;
        }
        while (treeViewTraverser.Current != partialVisibleElement.Data && treeViewTraverser.MoveNext());
      }
      return true;
    }

    private int ClampValue(int value, int minimum, int maximum)
    {
      if (value < minimum)
        return minimum;
      if (maximum > 0 && value > maximum)
        return maximum;
      return value;
    }

    protected virtual TreeViewDragDropService CreateDragDropService()
    {
      return new TreeViewDragDropService(this);
    }

    protected internal virtual void AutoExpand(RadTreeNode node)
    {
      if (node.Expanded)
        return;
      this.expandTimer.Tag = (object) node;
      this.expandTimer.Start();
    }

    [Obsolete("Use the AutoScrollOnDragging method instead.")]
    protected internal virtual void AutoScrollOnDrag(TreeNodeElement hitItem)
    {
      this.AutoScrollOnDragging(hitItem);
    }

    protected internal virtual bool AutoScrollOnDragging(TreeNodeElement hitItem)
    {
      if (this.ViewElement.Children.Count <= 0)
        return false;
      if (this.ViewElement.Children[0] == hitItem)
      {
        this.VScrollBar.PerformSmallDecrement(1);
        return true;
      }
      if (this.ViewElement.Children[this.ViewElement.Children.Count - 1] != hitItem)
        return false;
      this.VScrollBar.PerformSmallIncrement(1);
      return true;
    }

    private void Scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.firstVisibleIndex = -1;
      if (!this.isBeginEdit)
        this.EndEdit();
      this.InvalidateMeasure();
    }

    private void ExpandTimer_Tick(object sender, EventArgs e)
    {
      this.expandTimer.Stop();
      if (this.expandTimer.Tag != null)
      {
        ((RadTreeNode) this.expandTimer.Tag).Expand();
        this.Invalidate(false);
      }
      this.expandTimer.Tag = (object) null;
    }

    private void ScrollTimer_Tick(object sender, EventArgs e)
    {
      if (this.ControlBoundingRectangle.Contains(this.ElementTree.Control.PointToClient(Control.MousePosition)))
      {
        this.scrollTimer.Enabled = false;
      }
      else
      {
        if (!this.MultiSelect || this.ExtendSelectionDown(this.mouseMoveLocation))
          return;
        this.ExtendSelectionUp(this.mouseMoveLocation);
      }
    }

    private void mouseUpTimer_Tick(object sender, EventArgs e)
    {
      this.mouseUpTimer.Stop();
      if (this.lastClickedNode == null || !this.lastClickedNode.Current)
        return;
      this.lastClickedNode = (RadTreeNode) null;
      this.BeginEdit();
    }

    private void filterDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.traverser.Reset();
      this.Root.Nodes.ResetVersion();
      this.UpdateNodes();
      this.Update(RadTreeViewElement.UpdateActions.SortChanged);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadTreeViewElement.ShowLinesProperty || e.Property == RadTreeViewElement.ShowRootLinesProperty || (e.Property == RadTreeViewElement.ShowExpandCollapseProperty || e.Property == RadTreeViewElement.LineColorProperty) || (e.Property == RadTreeViewElement.AlternatingRowColorProperty || e.Property == RadTreeViewElement.LineStyleProperty))
        this.SynchronizeNodeElements();
      else if (e.Property == RadTreeViewElement.AllowPlusMinusAnimationProperty)
        this.Update(RadTreeViewElement.UpdateActions.Reset);
      else if (e.Property == RadTreeViewElement.TreeIndentProperty)
        this.ViewElement.UpdateItems();
      else if (e.Property == RadTreeViewElement.NodeSpacingProperty)
      {
        this.ViewElement.ItemSpacing = (int) e.NewValue;
        this.Scroller.ItemSpacing = this.ViewElement.ItemSpacing;
      }
      else if (e.Property == RadTreeViewElement.AllowArbitraryItemHeightProperty)
      {
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        this.Scroller.UpdateScrollRange();
      }
      else if (e.Property == RadTreeViewElement.ItemHeightProperty)
      {
        this.Scroller.ItemHeight = this.ItemHeight;
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        this.Scroller.UpdateScrollRange();
      }
      else
      {
        if (e.Property != RadTreeViewElement.FullRowSelectProperty)
          return;
        this.FitItemsToSize = (bool) e.NewValue;
        this.Update(RadTreeViewElement.UpdateActions.Reset);
      }
    }

    protected virtual void SynchronizeNodeElements()
    {
      foreach (TreeNodeElement child in this.ViewElement.Children)
        child.Synchronize();
      this.Invalidate();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName == "TriStateMode")
      {
        this.SynchronizeNodeElements();
        if (this.TriStateMode)
          return;
        foreach (RadTreeNode radTreeNode in new TreeViewTraverser(this))
        {
          if (radTreeNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
            radTreeNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }
      }
      else
      {
        if (!(e.PropertyName == "CheckBoxes"))
          return;
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    protected override void OnStyleChanged(RadPropertyChangedEventArgs e)
    {
      base.OnStyleChanged(e);
      this.ViewElement.ItemSpacing = this.NodeSpacing;
      this.Scroller.ItemSpacing = this.NodeSpacing;
    }

    protected override void OnAutoSizeChanged()
    {
      if (!this.AutoSizeItems && this.root.Nodes.Count > 0)
      {
        TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
        while (treeViewTraverser.MoveNext())
          treeViewTraverser.Current.ItemHeight = -1;
      }
      base.OnAutoSizeChanged();
    }

    protected internal virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling)
        this.ScrollBehavior.MouseDown(e.Location);
      this.doNotStartEditingOnMouseUp = false;
      this.mouseDownLocation = e.Location;
      this.enterEditMode = false;
      this.enableAutoExpand = true;
      this.performSelectionOnMouseUp = false;
      this.draggedNode = (RadTreeNode) null;
      bool flag = this.IsToggle(this.mouseDownLocation);
      this.lastClickedNode = (RadTreeNode) null;
      if (this.IsScrollBar(this.mouseDownLocation) || this.IsExpander(this.mouseDownLocation) || this.IsEditorHint(this.mouseDownLocation))
        return false;
      RadTreeNode nodeAt = this.GetNodeAt(this.mouseDownLocation, this.FullRowSelect);
      if (nodeAt == null)
      {
        if (this.activeEditor != null)
          this.EndEdit();
        return false;
      }
      if (e.Button == MouseButtons.Right && this.DragDropService.State != RadServiceState.Working)
      {
        this.disableEnsureNodeVisibleHorizontal = !this.AutoScrollOnClick;
        this.SelectedNode = nodeAt;
        this.disableEnsureNodeVisibleHorizontal = false;
        return false;
      }
      if (e.Button == MouseButtons.Left)
      {
        this.draggedNode = nodeAt;
        if (nodeAt.Selected && !flag)
        {
          if (this.activeEditor != null && !this.IsEditorHint(e.Location))
          {
            this.EndEdit();
            return false;
          }
          this.enterEditMode = this.activeEditor == null && this.allowEdit && nodeAt.Current && this.SelectedNodes.Count <= 1;
          if (this.AllowDragDrop)
          {
            this.performSelectionOnMouseUp = true;
            this.DragDropService.Start((object) this.GetElement(nodeAt));
            return false;
          }
        }
        if (this.activeEditor != null)
        {
          this.EndEdit();
          this.enterEditMode = false;
        }
        this.disableEnsureNodeVisibleHorizontal = !this.AutoScrollOnClick;
        this.ProcessSelection(nodeAt, Control.ModifierKeys, true, RadTreeViewAction.ByMouse);
        this.disableEnsureNodeVisibleHorizontal = false;
        if (this.SelectedNode == nodeAt && !flag && !this.MultiSelect)
          this.DragDropService.Start((object) this.GetElement(nodeAt));
      }
      return false;
    }

    protected internal virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling && this.ScrollBehavior.IsRunning)
      {
        this.ScrollBehavior.MouseUp(e.Location);
        return false;
      }
      RadTreeNode nodeAt = this.GetNodeAt(e.Location, this.FullRowSelect);
      if (this.scrollTimer.Enabled)
        this.scrollTimer.Enabled = false;
      if (this.IsScrollBar(e.Location) || this.IsExpander(e.Location) || (this.IsEditorHint(e.Location) || e.Button != MouseButtons.Left) || (this.IsToggle(this.mouseDownLocation) || nodeAt == null))
        return false;
      if (this.performSelectionOnMouseUp)
        this.ProcessSelection(nodeAt, Control.ModifierKeys, true, RadTreeViewAction.ByMouse);
      if (this.enterEditMode && !this.doNotStartEditingOnMouseUp)
      {
        this.lastClickedNode = nodeAt;
        this.mouseUpTimer.Start();
        return false;
      }
      if (this.enableAutoExpand && nodeAt == this.SelectedNode && this.ExpandMode == ExpandMode.Single)
        nodeAt.Expanded = !nodeAt.Expanded;
      return false;
    }

    protected internal virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling)
        this.ScrollBehavior.MouseMove(e.Location);
      if (e.Button == MouseButtons.Left && this.MultiSelect && (this.draggedNode != null && this.DragDropService.State != RadServiceState.Working) && !this.ScrollBehavior.IsRunning)
      {
        this.performSelectionOnMouseUp = false;
        if (this.GetNodeAt(e.Location) != null)
        {
          this.scrollTimer.Stop();
          if (e.Location != this.mouseDownLocation)
            this.ProcessMouseSelection(e.Location);
        }
        else
        {
          this.mouseMoveLocation = e.Location;
          if (!this.scrollTimer.Enabled)
            this.scrollTimer.Enabled = true;
        }
      }
      return false;
    }

    private void ProcessMouseSelection(Point location)
    {
      Rectangle selectionRectangle = this.CreateSelectionRectangle(location);
      Dictionary<int, RadTreeNode> dictionary = new Dictionary<int, RadTreeNode>();
      this.BeginUpdate();
      this.draggedNode.Current = true;
      foreach (TreeNodeElement child in this.ViewElement.Children)
      {
        if (child.ControlBoundingRectangle.IntersectsWith(selectionRectangle) && child.Data.Enabled)
        {
          child.Data.Selected = true;
          dictionary.Add(child.Data.GetHashCode(), child.Data);
        }
      }
      if (Control.ModifierKeys != Keys.Control)
      {
        List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>();
        foreach (RadTreeNode selectedNode in (ReadOnlyCollection<RadTreeNode>) this.SelectedNodes)
        {
          if (!dictionary.ContainsKey(selectedNode.GetHashCode()))
            radTreeNodeList.Add(selectedNode);
        }
        for (int index = 0; index < radTreeNodeList.Count; ++index)
          radTreeNodeList[index].Selected = false;
      }
      this.EndUpdate(true, RadTreeViewElement.UpdateActions.StateChanged);
    }

    protected internal virtual bool ProecessMouseEnter(EventArgs e)
    {
      if (this.AllowPlusMinusAnimation)
      {
        foreach (TreeNodeElement child in this.ViewElement.Children)
        {
          double num = this.PlusMinusAnimationStep;
          if (num == 0.0)
            num = 0.1;
          new AnimatedPropertySetting(VisualElement.OpacityProperty, (int) Math.Ceiling(1.0 / num), 1, (object) this.PlusMinusAnimationStep)
          {
            StartValue = ((object) 0.0),
            EndValue = ((object) 1.0),
            ApplyEasingType = RadEasingType.Linear,
            RemoveAfterApply = false
          }.ApplyValue((RadObject) child.ExpanderElement);
        }
      }
      return false;
    }

    protected internal virtual bool ProecessMouseLeave(EventArgs e)
    {
      if (this.AllowPlusMinusAnimation)
      {
        foreach (TreeNodeElement child in this.ViewElement.Children)
          new AnimatedPropertySetting(VisualElement.OpacityProperty, 10, 1, (object) this.PlusMinusAnimationStep)
          {
            StartValue = ((object) 1.0),
            EndValue = ((object) 0.0),
            ApplyEasingType = RadEasingType.Linear,
            RemoveAfterApply = false
          }.ApplyValue((RadObject) child.ExpanderElement);
      }
      return false;
    }

    protected internal virtual bool ProcessMouseClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && !this.IsExpander(e.Location) && (!this.IsToggle(e.Location) && this.ExpandMode == ExpandMode.Multiple))
      {
        RadTreeNode nodeAt = this.GetNodeAt(e.Location);
        if (nodeAt != null && !this.AllowEdit && this.ToggleMode == ToggleMode.SingleClick)
          nodeAt.Expanded = !nodeAt.Expanded;
      }
      return this.ElementTree.GetElementAtPoint<RadScrollBarElement>(e.Location) != null;
    }

    protected internal virtual bool ProcessMouseDoubleClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && !this.IsExpander(e.Location) && (!this.IsToggle(e.Location) && this.ExpandMode == ExpandMode.Multiple))
      {
        RadTreeNode nodeAt = this.GetNodeAt(e.Location);
        if (nodeAt != null && this.ToggleMode == ToggleMode.DoubleClick)
          nodeAt.Expanded = !nodeAt.Expanded;
      }
      this.lastClickedNode = (RadTreeNode) null;
      this.mouseUpTimer.Stop();
      this.doNotStartEditingOnMouseUp = true;
      return false;
    }

    protected internal virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int delta = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      int num2 = this.Scroller.Scrollbar.Value;
      this.ScrollTo(delta);
      return num2 != this.Scroller.Scrollbar.Value;
    }

    protected internal virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      if (this.IsEditing)
        return false;
      switch (e.KeyCode)
      {
        case Keys.Return:
          this.HandleReturnKey(e);
          break;
        case Keys.Escape:
          this.HandleEscapeKey(e);
          break;
        case Keys.Space:
          this.HandleSpaceKey(e);
          break;
        case Keys.Prior:
          this.HandlePageUp(e);
          break;
        case Keys.Next:
          this.HandlePageDown(e);
          break;
        case Keys.End:
          this.HandleEndKey(e);
          break;
        case Keys.Home:
          this.HandleHomeKey(e);
          break;
        case Keys.Left:
          this.HandleLeftKey(e);
          break;
        case Keys.Up:
          this.HandleUpKey(e);
          break;
        case Keys.Right:
          this.HandleRightKey(e);
          break;
        case Keys.Down:
          this.HandleDownKey(e);
          break;
        case Keys.Delete:
          this.HandleDelKey(e);
          break;
        case Keys.Multiply:
          this.HandleMultiplyKey(e);
          break;
        case Keys.Add:
          this.HandleAddKey(e);
          break;
        case Keys.Subtract:
          this.HandleSubtractKey(e);
          break;
        case Keys.Divide:
          this.HandleDivideKey(e);
          break;
        case Keys.F2:
          this.HandleF2Key(e);
          break;
      }
      return false;
    }

    protected internal virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      this.HandleNavigation(e.KeyChar);
      return false;
    }

    protected internal virtual bool ProcessContextMenu(Point location)
    {
      if (this.IsScrollBar(location))
        return false;
      if (this.activeEditor != null)
        this.EndEdit();
      RadContextMenu contextMenu = this.ContextMenu;
      RadTreeNode nodeAt = this.GetNodeAt(location);
      if (nodeAt != null && nodeAt.ContextMenu != null)
        contextMenu = nodeAt.ContextMenu;
      if (contextMenu == null && this.allowDefaultContextMenu)
        contextMenu = this.InitializeDefaultContextMenu(nodeAt);
      if (contextMenu != null)
      {
        if (nodeAt != null)
          this.SelectedNode = nodeAt;
        RadControl control = this.ElementTree.Control as RadControl;
        if (control != null)
        {
          contextMenu.ThemeName = control.ThemeName;
          contextMenu.DropDown.RightToLeft = control.RightToLeft;
        }
        TreeViewContextMenuOpeningEventArgs e = new TreeViewContextMenuOpeningEventArgs(nodeAt, contextMenu);
        this.OnContextMenuOpening(e);
        if (!e.Cancel)
        {
          contextMenu.Show(this.ElementTree.Control, location);
          return true;
        }
      }
      return false;
    }

    protected virtual RadContextMenu InitializeDefaultContextMenu(RadTreeNode node)
    {
      if (this.defaultMenu != null)
        this.defaultMenu.Dispose();
      this.defaultMenu = new TreeViewDefaultContextMenu(this);
      this.defaultMenu.NodeUnderMouse = node;
      if (node != null)
      {
        if (node.Expanded && node.Nodes.Count > 0)
          this.defaultMenu.ExpandCollapseMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCollapse");
        else
          this.defaultMenu.ExpandCollapseMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuExpand");
      }
      this.defaultMenu.EditMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuEdit");
      this.defaultMenu.AddMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuNew");
      this.defaultMenu.DeleteMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuDelete");
      this.defaultMenu.CutMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCut");
      this.defaultMenu.CopyMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCopy");
      this.defaultMenu.PasteMenuItem.Text = LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuPaste");
      this.defaultMenu.ExpandCollapseMenuItem.Name = "ExpandCollapse";
      this.defaultMenu.AddMenuItem.Name = "New";
      this.defaultMenu.EditMenuItem.Name = "Edit";
      this.defaultMenu.DeleteMenuItem.Name = "Delete";
      this.defaultMenu.CutMenuItem.Name = "Cut";
      this.defaultMenu.CopyMenuItem.Name = "Copy";
      this.defaultMenu.PasteMenuItem.Name = "Paste";
      this.defaultMenu.CopyMenuItem.Enabled = node != null;
      this.defaultMenu.ExpandCollapseMenuItem.Enabled = node != null && node.Nodes.Count > 0;
      this.defaultMenu.EditMenuItem.Enabled = node != null && this.AllowEdit;
      this.defaultMenu.AddMenuItem.Enabled = this.AllowAdd;
      this.defaultMenu.DeleteMenuItem.Enabled = node != null && this.AllowRemove;
      this.defaultMenu.CutMenuItem.Enabled = node != null && this.AllowRemove;
      bool flag = Clipboard.GetData("RadTreeViewTreeNode") != null || !string.IsNullOrEmpty(Clipboard.GetText());
      this.defaultMenu.PasteMenuItem.Enabled = this.AllowAdd && flag;
      return (RadContextMenu) this.defaultMenu;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.ViewElement.ElementProvider.ClearCache();
      if (this.pendingScrollerUpdates == RadTreeViewElement.UpdateActions.None)
        return;
      this.Update(this.pendingScrollerUpdates);
      this.pendingScrollerUpdates = RadTreeViewElement.UpdateActions.None;
    }

    private void typingTimer_Tick(object sender, EventArgs e)
    {
      this.typingTimer.Stop();
    }

    private void HandleF2Key(KeyEventArgs e)
    {
      if (this.IsEditing)
        return;
      e.Handled = this.BeginEdit();
    }

    private void HandleDelKey(KeyEventArgs e)
    {
      if (this.IsEditing || !this.AllowRemove)
        return;
      RadTreeNode selectedNode = this.SelectedNode;
      if (selectedNode == null)
        return;
      RadTreeViewElement.RemoveAllNodes(this, selectedNode);
    }

    internal static void RemoveAllNodes(RadTreeViewElement treeView, RadTreeNode node)
    {
      RadTreeNodeCollection nodes = treeView.Nodes;
      RadTreeNode parent = node.Parent;
      if (parent != null)
        nodes = parent.Nodes;
      foreach (RadTreeNode node1 in (IEnumerable<RadTreeNode>) new List<RadTreeNode>((IEnumerable<RadTreeNode>) node.Nodes))
        RadTreeViewElement.RemoveAllNodes(treeView, node1);
      nodes.Remove(node);
    }

    private void HandleEscapeKey(KeyEventArgs e)
    {
      if (this.dragDropService.State != RadServiceState.Working)
        return;
      this.dragDropService.Stop(false);
      this.doNotStartEditingOnMouseUp = true;
    }

    private void HandleSpaceKey(KeyEventArgs e)
    {
      RadTreeNode selectedNode = this.SelectedNode;
      if (selectedNode == null)
        return;
      TreeNodeElement element = this.GetElement(selectedNode);
      if (element != null)
      {
        if (element.ToggleElement != null)
          element.ToggleElement.Focus();
        else
          element.Focus();
      }
      if (!this.MultiSelect || e.Modifiers != Keys.Control)
        return;
      selectedNode.Selected = !selectedNode.Selected;
    }

    private void HandleSubtractKey(KeyEventArgs e)
    {
      this.SelectedNode?.Collapse(true);
    }

    private void HandleAddKey(KeyEventArgs e)
    {
      this.SelectedNode?.Expand();
    }

    private void HandleDivideKey(KeyEventArgs e)
    {
      this.SelectedNode?.Collapse();
    }

    private void HandleMultiplyKey(KeyEventArgs e)
    {
      this.SelectedNode?.ExpandAll();
    }

    private void HandleReturnKey(KeyEventArgs e)
    {
      this.SelectedNode?.Toggle();
    }

    private void HandleHomeKey(KeyEventArgs e)
    {
      RadTreeNode topNode = this.TopNode;
      if (topNode == null)
        return;
      this.isByKeyboard = true;
      this.SelectedNode = topNode;
      this.SelectedNode.EnsureVisible();
    }

    private void HandleEndKey(KeyEventArgs e)
    {
      RadTreeNode lastNode = this.LastNode;
      if (lastNode == null)
        return;
      this.isByKeyboard = true;
      this.SelectedNode = lastNode;
      this.SelectedNode.EnsureVisible();
    }

    private void HandleLeftKey(KeyEventArgs e)
    {
      if (e.Modifiers == Keys.Control && this.HScrollBar != null)
      {
        this.HScrollBar.PerformSmallDecrement(1);
      }
      else
      {
        this.isByKeyboard = true;
        RadTreeNode selectedNode = this.SelectedNode;
        if (selectedNode == null)
          return;
        bool flag = this.ElementTree.Control.RightToLeft == RightToLeft.Yes;
        if (selectedNode.Nodes.Count > 0)
        {
          if (flag)
          {
            if (!selectedNode.Expanded)
            {
              selectedNode.Expand();
              return;
            }
          }
          else if (selectedNode.Expanded)
          {
            selectedNode.Collapse(true);
            return;
          }
        }
        if (e.Modifiers == Keys.Shift)
          return;
        if (flag)
        {
          if (selectedNode.Nodes.Count <= 0)
            return;
          this.SelectedNode = selectedNode.Nodes[0];
          this.EnsureVisible(this.SelectedNode);
        }
        else
        {
          if (selectedNode.Parent == null)
            return;
          this.SelectedNode = selectedNode.Parent;
          this.EnsureVisible(this.SelectedNode);
        }
      }
    }

    private void HandleRightKey(KeyEventArgs e)
    {
      if (e.Modifiers == Keys.Control && this.HScrollBar != null)
      {
        this.HScrollBar.PerformSmallIncrement(1);
      }
      else
      {
        this.isByKeyboard = true;
        RadTreeNode selectedNode = this.SelectedNode;
        if (selectedNode == null)
          return;
        bool flag = this.ElementTree.Control.RightToLeft == RightToLeft.Yes;
        if (selectedNode.Nodes.Count > 0)
        {
          if (flag)
          {
            if (selectedNode.Expanded)
            {
              selectedNode.Collapse(true);
              return;
            }
          }
          else if (!selectedNode.Expanded)
          {
            selectedNode.Expand();
            return;
          }
        }
        if (e.Modifiers == Keys.Shift)
          return;
        if (flag)
        {
          if (selectedNode.Parent == null)
            return;
          this.isByKeyboard = true;
          this.SelectedNode = selectedNode.Parent;
          this.EnsureVisible(this.SelectedNode);
        }
        else
        {
          if (selectedNode.Nodes.Count <= 0)
            return;
          this.isByKeyboard = true;
          this.SelectedNode = selectedNode.Nodes[0];
          this.EnsureVisible(this.SelectedNode);
        }
      }
    }

    private void HandleUpKey(KeyEventArgs e)
    {
      RadTreeNode selectedNode = this.SelectedNode;
      if (selectedNode == null)
        return;
      RadTreeNode selectableNode = this.GetSelectableNode(selectedNode, false);
      if (selectedNode == null || selectableNode == null)
        return;
      this.ProcessSelection(selectableNode, Control.ModifierKeys, false, RadTreeViewAction.ByKeyboard);
    }

    private void HandleDownKey(KeyEventArgs e)
    {
      RadTreeNode node = this.SelectedNode;
      if (node == null && this.Nodes.Count > 0)
        node = this.Nodes[0];
      if (node == null)
        return;
      RadTreeNode selectableNode = this.GetSelectableNode(node, true);
      if (node == null || selectableNode == null)
        return;
      this.ProcessSelection(selectableNode, Control.ModifierKeys, false, RadTreeViewAction.ByKeyboard);
    }

    private RadTreeNode GetSelectableNode(RadTreeNode node, bool next)
    {
      do
      {
        node = next ? node.NextVisibleNode : node.PrevVisibleNode;
        if (node != null && node.Enabled)
          return node;
      }
      while (node != null);
      return (RadTreeNode) null;
    }

    private void HandlePageDown(KeyEventArgs e)
    {
      TreeNodeElement fullVisibleElement1 = this.GetLastFullVisibleElement();
      if (fullVisibleElement1 == null)
        return;
      if (!fullVisibleElement1.Data.Current)
      {
        this.isByKeyboard = true;
        this.SelectedNode = fullVisibleElement1.Data;
        this.SelectedNode.Selected = true;
      }
      else
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + fullVisibleElement1.ControlBoundingRectangle.Top);
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        TreeNodeElement fullVisibleElement2 = this.GetLastFullVisibleElement();
        if (fullVisibleElement2 == null)
          return;
        this.isByKeyboard = true;
        this.SelectedNode = fullVisibleElement2.Data;
        this.SelectedNode.Selected = true;
      }
    }

    private void HandlePageUp(KeyEventArgs e)
    {
      TreeNodeElement fullVisibleElement1 = this.GetFirstFullVisibleElement();
      if (fullVisibleElement1 == null)
        return;
      if (!fullVisibleElement1.Data.Current)
      {
        this.isByKeyboard = true;
        this.SelectedNode = fullVisibleElement1.Data;
        this.SelectedNode.Selected = true;
      }
      else
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - (this.ViewElement.ControlBoundingRectangle.Height - fullVisibleElement1.ControlBoundingRectangle.Bottom));
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        TreeNodeElement fullVisibleElement2 = this.GetFirstFullVisibleElement();
        if (fullVisibleElement2 == null)
          return;
        this.isByKeyboard = true;
        this.SelectedNode = fullVisibleElement2.Data;
        this.SelectedNode.Selected = true;
      }
    }

    private int GetNodeIndex(RadTreeNode position)
    {
      if (this.root.Nodes.Count == 0)
        return -1;
      int num = 0;
      TreeViewTraverser treeViewTraverser = new TreeViewTraverser(this);
      while (treeViewTraverser.MoveNext() && treeViewTraverser.Current != position)
        ++num;
      return num;
    }

    private void HandleNavigation(char keyChar)
    {
      if (!this.KeyboardSearchEnabled)
        return;
      if (this.typingTimer.Enabled)
      {
        this.typingTimer.Stop();
        this.typingTimer.Start();
      }
      else
      {
        this.searchBuffer = new StringBuilder();
        this.typingTimer.Start();
      }
      this.searchBuffer.Append(keyChar);
      RadTreeNode firstMatch = this.GetFirstMatch(this.IsSameLetter(this.searchBuffer) ? this.searchBuffer[0].ToString() : this.searchBuffer.ToString(), this.Nodes);
      if (firstMatch == null)
        return;
      this.SelectedNode = firstMatch;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      this.EndEdit();
      TreeNodeElement element = this.GetElement(this.GetNodeAt(args.Location));
      if (args.IsBegin && element != null && this.DragDropService.State != RadServiceState.Working && element.ContentElement.ControlBoundingRectangle.Contains(args.Location))
      {
        this.DragDropService.BeginDrag(this.ElementTree.Control.PointToScreen(args.Location), (ISupportDrag) element);
        args.Handled = true;
      }
      else if (this.DragDropService.State == RadServiceState.Working)
      {
        this.DragDropService.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
        if (args.IsEnd)
          this.DragDropService.EndDrag();
        args.Handled = true;
      }
      else
      {
        if (this.VScrollBar.ControlBoundingRectangle.Contains(args.Location) || this.HScrollBar.ControlBoundingRectangle.Contains(args.Location))
          return;
        int num1 = this.VScrollBar.Value - args.Offset.Height;
        if (num1 > this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
          num1 = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
        if (num1 < this.VScrollBar.Minimum)
          num1 = this.VScrollBar.Minimum;
        this.VScrollBar.Value = num1;
        int num2 = this.HScrollBar.Value - args.Offset.Width;
        if (num2 > this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1)
          num2 = this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1;
        if (num2 < this.HScrollBar.Minimum)
          num2 = this.HScrollBar.Minimum;
        this.HScrollBar.Value = num2;
        args.Handled = true;
      }
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      if (this.AllowAlternatingRowColor)
        this.firstVisibleIndex = -1;
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      SizeF size = clientRectangle.Size;
      ElementVisibility visibility1 = this.HScrollBar.Visibility;
      RadTreeViewVirtualizedContainer viewElement = (RadTreeViewVirtualizedContainer) this.ViewElement;
      this.HScrollBar.Maximum = this.root.ChildrenSize.Width;
      int num1 = this.VScrollBar.Visibility == ElementVisibility.Visible ? (int) this.VScrollBar.DesiredSize.Width : 0;
      this.HScrollBar.LargeChange = Math.Max(this.HScrollBar.Minimum, (int) ((double) clientRectangle.Width - (double) num1 - (double) this.ViewElement.Margin.Horizontal));
      if (this.HScrollBar.Value > this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1)
        this.SetScrollValue(this.HScrollBar, this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1);
      this.HScrollBar.SmallChange = this.HScrollBar.LargeChange / 10;
      this.UpdateHScrollbarVisibility();
      if (this.HScrollBar.Visibility == ElementVisibility.Visible)
        size.Height -= this.HScrollBar.DesiredSize.Height;
      else
        this.HScrollBar.Value = this.HScrollBar.Minimum;
      bool flag = false;
      ElementVisibility visibility2 = this.VScrollBar.Visibility;
      this.Scroller.ClientSize = size;
      if (visibility2 != this.VScrollBar.Visibility)
      {
        int num2 = this.VScrollBar.Visibility == ElementVisibility.Visible ? (int) this.VScrollBar.DesiredSize.Width : 0;
        int num3 = (int) ((double) clientRectangle.Width - (double) num2 - (double) this.ViewElement.Margin.Horizontal);
        this.HScrollBar.LargeChange = num3 >= 0 ? num3 : 0;
        this.UpdateHScrollbarVisibility();
        flag = true;
      }
      if (this.VScrollBar.Visibility == ElementVisibility.Collapsed && this.VScrollBar.Value != this.VScrollBar.Minimum)
        this.VScrollBar.Value = this.VScrollBar.Minimum;
      if (!flag)
        return visibility1 != this.HScrollBar.Visibility;
      return true;
    }

    protected virtual void UpdateHScrollbarVisibility()
    {
      if (this.HorizontalScrollState == ScrollState.AlwaysShow)
        this.HScrollBar.Visibility = ElementVisibility.Visible;
      else if (this.HorizontalScrollState == ScrollState.AlwaysHide)
        this.HScrollBar.Visibility = ElementVisibility.Collapsed;
      else
        this.HScrollBar.Visibility = this.HScrollBar.LargeChange < this.HScrollBar.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    protected override void UpdateFitToSizeMode()
    {
    }

    private void UpdateFirstVisibleIndex()
    {
      if (this.ViewElement.Children.Count == 0)
        return;
      RadTreeNode prevVisibleNode = ((TreeNodeElement) this.ViewElement.Children[0]).Data.PrevVisibleNode;
      this.firstVisibleIndex = 0;
      for (; prevVisibleNode != null; prevVisibleNode = prevVisibleNode.PrevVisibleNode)
        ++this.firstVisibleIndex;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.UpdateScrollers((RadTreeNode) null, RadTreeViewElement.UpdateActions.ExpandedChanged);
    }

    public enum UpdateActions
    {
      None,
      Reset,
      Resume,
      ItemAdded,
      ItemRemoved,
      ItemMoved,
      ItemEdited,
      ExpandedChanged,
      ExpandedChangedUsingAnimation,
      StateChanged,
      SortChanged,
      NodeStateChanged,
    }

    internal class RootTreeNode : RadTreeNode
    {
      public RootTreeNode(RadTreeViewElement treeView)
      {
        this.Text = "RootNode";
        this.TreeViewElement = treeView;
        this.Expanded = true;
      }

      internal override bool CanPerformFind
      {
        get
        {
          return false;
        }
      }
    }

    private class DeferHelper : IDisposable
    {
      private RadTreeViewElement treeView;

      public DeferHelper(RadTreeViewElement treeView)
      {
        this.treeView = treeView;
      }

      public void Dispose()
      {
        if (this.treeView == null)
          return;
        this.treeView.EndUpdate();
        this.treeView = (RadTreeViewElement) null;
      }
    }
  }
}
