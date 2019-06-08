// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeView
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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Commands;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Docking(DockingBehavior.Ask)]
  [DefaultEvent("SelectedNodeChanged")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadTreeViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Displays a hierarchical collection of labelled items to the user that can optionally contain an image")]
  [DefaultProperty("Nodes")]
  [TelerikToolboxCategory("Data Controls")]
  [ComplexBindingProperties("DataSource", "DataMember")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedNode")]
  public class RadTreeView : RadControl
  {
    private RadTreeViewElement treeElement;
    private bool contextMenuKeyPressed;
    private RadTreeNode oldSelected;

    public RadTreeView()
    {
      this.WireEvents();
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      if (this.RadContextMenu is TreeViewDefaultContextMenu)
        this.RadContextMenu.Dispose();
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.treeElement = this.CreateTreeViewElement();
      parent.Children.Add((RadElement) this.treeElement);
    }

    protected virtual RadTreeViewElement CreateTreeViewElement()
    {
      return new RadTreeViewElement();
    }

    protected virtual void WireEvents()
    {
      this.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.RadTreeView_SelectedNodeChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.SelectedNodeChanged -= new RadTreeView.RadTreeViewEventHandler(this.RadTreeView_SelectedNodeChanged);
    }

    private void RadTreeView_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
    {
      this.AccessibilityNotifyClients(AccessibleEvents.Focus, 0);
    }

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool AutoScroll
    {
      get
      {
        return base.AutoScroll;
      }
      set
      {
        base.AutoScroll = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    [Category("Behavior")]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.TreeViewElement.EnableKineticScrolling;
      }
      set
      {
        this.TreeViewElement.EnableKineticScrolling = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool LazyMode
    {
      get
      {
        return this.treeElement.LazyMode;
      }
      set
      {
        if (this.treeElement.LazyMode == value)
          return;
        this.treeElement.LazyMode = value;
        this.OnNotifyPropertyChanged(nameof (LazyMode));
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the color of the drop hint.")]
    [DefaultValue(typeof (Color), "")]
    public Color DropHintColor
    {
      get
      {
        return this.treeElement.DragDropService.DropHintColor;
      }
      set
      {
        this.treeElement.DragDropService.DropHintColor = value;
      }
    }

    [Category("Behavior")]
    [Description(" Gets or sets a value indicating whether [show drop hint]")]
    [DefaultValue(true)]
    public bool ShowDropHint
    {
      get
      {
        return this.treeElement.DragDropService.ShowDropHint;
      }
      set
      {
        this.treeElement.DragDropService.ShowDropHint = value;
      }
    }

    [Description(" Gets or sets a value indicating whether [show drop feedback]")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowDragHint
    {
      get
      {
        return this.treeElement.DragDropService.ShowDragHint;
      }
      set
      {
        this.treeElement.DragDropService.ShowDragHint = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Browsable(true)]
    [Description("Contains data binding settings for related data.")]
    public RelationBindingCollection RelationBindings
    {
      get
      {
        return this.treeElement.RelationBindings;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool EnableDeferredScrolling
    {
      get
      {
        return this.treeElement.EnableDeferredScrolling;
      }
      set
      {
        this.treeElement.EnableDeferredScrolling = value;
      }
    }

    [DefaultValue(ExpandAnimation.Opacity)]
    [Category("Behavior")]
    [Description("Gets or sets the type of expand animation.")]
    public ExpandAnimation ExpandAnimation
    {
      get
      {
        return this.treeElement.ExpandAnimation;
      }
      set
      {
        this.treeElement.ExpandAnimation = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the animation step for expand/collapse animation.")]
    [DefaultValue(0.025)]
    public double PlusMinusAnimationStep
    {
      get
      {
        return this.treeElement.PlusMinusAnimationStep;
      }
      set
      {
        this.treeElement.PlusMinusAnimationStep = value;
      }
    }

    [Description("Gets or sets a value indicating whether animation of collapse/expand images is enabled.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AllowPlusMinusAnimation
    {
      get
      {
        return this.treeElement.AllowPlusMinusAnimation;
      }
      set
      {
        this.treeElement.AllowPlusMinusAnimation = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(-1)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Description("The default image index for nodes.")]
    public virtual int ImageIndex
    {
      get
      {
        return this.treeElement.ImageIndex;
      }
      set
      {
        this.treeElement.ImageIndex = value;
      }
    }

    [TypeConverter(typeof (ImageKeyConverter))]
    [Category("Appearance")]
    [RelatedImageList("ImageList")]
    [Localizable(true)]
    [DefaultValue("")]
    [Description("The default image key for nodes.")]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual string ImageKey
    {
      get
      {
        return this.treeElement.ImageKey;
      }
      set
      {
        this.treeElement.ImageKey = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether drag and drop is enabled.")]
    public virtual bool AllowDragDrop
    {
      get
      {
        return this.treeElement.AllowDragDrop;
      }
      set
      {
        this.treeElement.AllowDragDrop = value;
        if (!value)
          return;
        this.AllowDrop = value;
      }
    }

    [Description("Gets or sets a value indicating whether the user is allowed to select more than one tree node at time")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool MultiSelect
    {
      get
      {
        return this.treeElement.MultiSelect;
      }
      set
      {
        this.treeElement.MultiSelect = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the shortcut menu associated to the RadTreeView.")]
    public virtual RadContextMenu RadContextMenu
    {
      get
      {
        return this.treeElement.ContextMenu;
      }
      set
      {
        if (this.treeElement.ContextMenu == value)
          return;
        if (this.treeElement.ContextMenu is TreeViewDefaultContextMenu)
          this.treeElement.ContextMenu.Dispose();
        this.treeElement.ContextMenu = value;
        if (this.treeElement.ContextMenu == null)
          return;
        this.treeElement.ContextMenu.ThemeName = this.ThemeName;
      }
    }

    [Browsable(false)]
    public override ContextMenuStrip ContextMenuStrip
    {
      get
      {
        return base.ContextMenuStrip;
      }
      set
      {
        base.ContextMenuStrip = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public virtual object Filter
    {
      get
      {
        return this.TreeViewElement.Filter;
      }
      set
      {
        this.TreeViewElement.Filter = value;
      }
    }

    [DefaultValue(SortOrder.None)]
    [Browsable(true)]
    public SortOrder SortOrder
    {
      get
      {
        return this.TreeViewElement.SortOrder;
      }
      set
      {
        this.TreeViewElement.SortOrder = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.TreeViewElement.FilterDescriptors;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.TreeViewElement.SortDescriptors;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether checkboxes are displayed beside the nodes.")]
    public virtual bool CheckBoxes
    {
      get
      {
        return this.TreeViewElement.CheckBoxes;
      }
      set
      {
        this.TreeViewElement.CheckBoxes = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the child nodes should be auto checked when RadTreeView is in tri state mode")]
    [Category("Behavior")]
    public virtual bool AutoCheckChildNodes
    {
      get
      {
        return this.treeElement.AutoCheckChildNodes;
      }
      set
      {
        this.treeElement.AutoCheckChildNodes = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the highlight spans the width of the tree view.")]
    public virtual bool FullRowSelect
    {
      get
      {
        return this.TreeViewElement.FullRowSelect;
      }
      set
      {
        this.TreeViewElement.FullRowSelect = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("TreeViewHideSelectionDescr")]
    [DefaultValue(false)]
    public virtual bool HideSelection
    {
      get
      {
        return this.TreeViewElement.HideSelection;
      }
      set
      {
        this.TreeViewElement.HideSelection = value;
      }
    }

    [Description("TreeViewHotTrackingDescr")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public virtual bool HotTracking
    {
      get
      {
        return this.TreeViewElement.HotTracking;
      }
      set
      {
        this.TreeViewElement.HotTracking = value;
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [Description("TreeViewIndentDescr")]
    [DefaultValue(20)]
    public int TreeIndent
    {
      get
      {
        return this.TreeViewElement.TreeIndent;
      }
      set
      {
        this.TreeViewElement.TreeIndent = value;
      }
    }

    [Description("TreeViewItemHeightDescr")]
    [Browsable(false)]
    [Category("CatAppearance")]
    [DefaultValue(20)]
    public virtual int ItemHeight
    {
      get
      {
        return this.TreeViewElement.ItemHeight;
      }
      set
      {
        this.TreeViewElement.ItemHeight = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether nodes can have different height.")]
    [DefaultValue(false)]
    public virtual bool AllowArbitraryItemHeight
    {
      get
      {
        return this.treeElement.AllowArbitraryItemHeight;
      }
      set
      {
        if (this.treeElement.AllowArbitraryItemHeight == value)
          return;
        this.treeElement.AllowArbitraryItemHeight = value;
        this.OnNotifyPropertyChanged(nameof (AllowArbitraryItemHeight));
      }
    }

    [Description("Gets or sets the spacing between nodes")]
    [Category("Appearance")]
    [DefaultValue(0)]
    public int SpacingBetweenNodes
    {
      get
      {
        return this.treeElement.ViewElement.ItemSpacing;
      }
      set
      {
        if (this.treeElement.NodeSpacing == value)
          return;
        this.treeElement.NodeSpacing = value;
        this.OnNotifyPropertyChanged(nameof (SpacingBetweenNodes));
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether editing is allowed.")]
    [Category("Behavior")]
    public virtual bool AllowEdit
    {
      get
      {
        return this.TreeViewElement.AllowEdit;
      }
      set
      {
        if (this.TreeViewElement.AllowEdit == value)
          return;
        this.TreeViewElement.AllowEdit = value;
        this.OnNotifyPropertyChanged(nameof (AllowEdit));
      }
    }

    [Description("Gets or sets a value indicating whether adding new nodes is allowed.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool AllowAdd
    {
      get
      {
        return this.TreeViewElement.AllowAdd;
      }
      set
      {
        if (this.TreeViewElement.AllowAdd == value)
          return;
        this.TreeViewElement.AllowAdd = value;
        this.OnNotifyPropertyChanged(nameof (AllowAdd));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether removing nodes is allowed.")]
    public virtual bool AllowRemove
    {
      get
      {
        return this.TreeViewElement.AllowRemove;
      }
      set
      {
        if (this.TreeViewElement.AllowRemove == value)
          return;
        this.TreeViewElement.AllowRemove = value;
        this.OnNotifyPropertyChanged(nameof (AllowRemove));
      }
    }

    [Description("Gets a value indicating whether there is an open editor in the tree view.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsEditing
    {
      get
      {
        return this.treeElement.IsEditing;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.treeElement.ActiveEditor;
      }
    }

    [Description("TreeViewLineColorDescr")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Gray")]
    public virtual Color LineColor
    {
      get
      {
        return this.TreeViewElement.LineColor;
      }
      set
      {
        this.TreeViewElement.LineColor = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue("\\")]
    public virtual string PathSeparator
    {
      get
      {
        return this.TreeViewElement.PathSeparator;
      }
      set
      {
        this.TreeViewElement.PathSeparator = value;
      }
    }

    [Browsable(false)]
    [Description("TreeViewSelectedNodeDescr")]
    [Category("CatAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadTreeNode SelectedNode
    {
      get
      {
        return this.TreeViewElement.SelectedNode;
      }
      set
      {
        this.TreeViewElement.SelectedNode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("TreeViewSelectedNodeDescr")]
    [Category("CatAppearance")]
    [Browsable(false)]
    public virtual SelectedTreeNodeCollection SelectedNodes
    {
      get
      {
        return this.TreeViewElement.SelectedNodes;
      }
    }

    [Browsable(false)]
    public virtual CheckedTreeNodeCollection CheckedNodes
    {
      get
      {
        return this.TreeViewElement.CheckedNodes;
      }
    }

    [Category("Behavior")]
    [Description("TreeViewShowLinesDescr")]
    [DefaultValue(false)]
    public virtual bool ShowLines
    {
      get
      {
        return this.TreeViewElement.ShowLines;
      }
      set
      {
        this.TreeViewElement.ShowLines = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("TreeViewShowShowNodeToolTipsDescr")]
    public bool ShowNodeToolTips
    {
      get
      {
        return this.TreeViewElement.ShowNodeToolTips;
      }
      set
      {
        this.TreeViewElement.ShowNodeToolTips = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether expand/collapse buttons are shown next to nodes with children.")]
    public bool ShowExpandCollapse
    {
      get
      {
        return this.TreeViewElement.ShowExpandCollapse;
      }
      set
      {
        this.TreeViewElement.ShowExpandCollapse = value;
      }
    }

    [Description("TreeViewShowRootLinesDescr")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowRootLines
    {
      get
      {
        return this.TreeViewElement.ShowRootLines;
      }
      set
      {
        this.TreeViewElement.ShowRootLines = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("CatAppearance")]
    [Description("TreeViewTopNodeDescr")]
    public RadTreeNode TopNode
    {
      get
      {
        return this.TreeViewElement.TopNode;
      }
    }

    [Category("CatAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("TreeViewVisibleCountDescr")]
    [Browsable(false)]
    public int VisibleCount
    {
      get
      {
        return this.TreeViewElement.VisibleCount;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the name of the list or table in the data source for which the RadTreeView is displaying data. ")]
    [Category("Data")]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    public virtual string DataMember
    {
      get
      {
        return this.TreeViewElement.DataMember;
      }
      set
      {
        if (!(this.TreeViewElement.DataMember != value))
          return;
        this.TreeViewElement.DataMember = value;
        this.OnNotifyPropertyChanged(nameof (DataMember));
      }
    }

    [Description("Gets or sets the data source that the RadTreeView is displaying data for.")]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    public virtual object DataSource
    {
      get
      {
        return this.TreeViewElement.DataSource;
      }
      set
      {
        if (this.TreeViewElement.DataSource == value)
          return;
        this.TreeViewElement.DataSource = value;
        this.OnNotifyPropertyChanged(nameof (DataSource));
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the display member.")]
    [Category("Data")]
    [DefaultValue("")]
    public virtual string DisplayMember
    {
      get
      {
        return this.TreeViewElement.DisplayMember;
      }
      set
      {
        this.TreeViewElement.DisplayMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the value member.")]
    [Category("Data")]
    [DefaultValue("")]
    public virtual string ValueMember
    {
      get
      {
        return this.TreeViewElement.ValueMember;
      }
      set
      {
        this.TreeViewElement.ValueMember = value;
      }
    }

    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the checked member.")]
    [Category("Data")]
    public virtual string CheckedMember
    {
      get
      {
        return this.TreeViewElement.CheckedMember;
      }
      set
      {
        this.TreeViewElement.CheckedMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the value member.")]
    [Category("Data")]
    [DefaultValue("")]
    public virtual string ChildMember
    {
      get
      {
        return this.TreeViewElement.ChildMember;
      }
      set
      {
        this.TreeViewElement.ChildMember = value;
      }
    }

    [Description("Gets or sets the parent member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DefaultValue("")]
    public virtual string ParentMember
    {
      get
      {
        return this.TreeViewElement.ParentMember;
      }
      set
      {
        this.TreeViewElement.ParentMember = value;
      }
    }

    [Category("Data")]
    [Description("Gets or sets a TypeConverter that will be used to convert the toggle state of the checkboxes to the underlying data type it is bound to.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual TypeConverter ToggleStateConverter
    {
      get
      {
        return this.TreeViewElement.ToggleStateConverter;
      }
      set
      {
        this.TreeViewElement.ToggleStateConverter = value;
      }
    }

    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.RadTreeViewEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual RadTreeNodeCollection Nodes
    {
      get
      {
        return this.TreeViewElement.Nodes;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.treeElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadScrollBarElement HScrollBar
    {
      get
      {
        return this.treeElement.HScrollBar;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadScrollBarElement VScrollBar
    {
      get
      {
        return this.treeElement.VScrollBar;
      }
    }

    [Category("Appearance")]
    [DefaultValue(TreeLineStyle.Dot)]
    [Description("Gets or sets the line style.")]
    public virtual TreeLineStyle LineStyle
    {
      get
      {
        return this.TreeViewElement.LineStyle;
      }
      set
      {
        if (this.TreeViewElement.LineStyle == value)
          return;
        this.TreeViewElement.LineStyle = value;
        this.OnNotifyPropertyChanged(nameof (LineStyle));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether tri state mode is enabled.")]
    public virtual bool TriStateMode
    {
      get
      {
        return this.TreeViewElement.TriStateMode;
      }
      set
      {
        if (this.TreeViewElement.TriStateMode == value)
          return;
        this.TreeViewElement.TriStateMode = value;
        this.OnNotifyPropertyChanged(nameof (TriStateMode));
      }
    }

    [Category("Behavior")]
    [DefaultValue(ToggleMode.DoubleClick)]
    public virtual ToggleMode ToggleMode
    {
      get
      {
        return this.TreeViewElement.ToggleMode;
      }
      set
      {
        if (this.TreeViewElement.ToggleMode == value)
          return;
        this.TreeViewElement.ToggleMode = value;
        this.OnNotifyPropertyChanged(nameof (ToggleMode));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue("")]
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string TreeViewXml
    {
      get
      {
        if (this.Nodes.Count <= 0)
          return string.Empty;
        StringBuilder sb = new StringBuilder();
        using (StringWriter stringWriter = new StringWriter(sb))
        {
          this.SaveXMLWithWriter((TextWriter) stringWriter);
          return sb.ToString();
        }
      }
      set
      {
        using (StringReader stringReader = new StringReader(value))
          this.LoadXMLWithReader((TextReader) stringReader);
        this.OnNotifyPropertyChanged(nameof (TreeViewXml));
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value that determines whether the user can navigate to an item by typing when RadTreeView is focused.")]
    public virtual bool KeyboardSearchEnabled
    {
      get
      {
        return this.TreeViewElement.KeyboardSearchEnabled;
      }
      set
      {
        this.TreeViewElement.KeyboardSearchEnabled = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value that specifies how long the user must wait before searching with the keyboard is reset.")]
    [DefaultValue(300)]
    [Category("Behavior")]
    public virtual int KeyboardSearchResetInterval
    {
      get
      {
        return this.TreeViewElement.KeyboardSearchResetInterval;
      }
      set
      {
        this.TreeViewElement.KeyboardSearchResetInterval = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the string comparer used by the keyboard navigation functionality.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IFindStringComparer FindStringComparer
    {
      get
      {
        return this.TreeViewElement.FindStringComparer;
      }
      set
      {
        this.TreeViewElement.FindStringComparer = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [Category("Behavior")]
    [Description("Occurs when the RadTreeView NodesNeeded event is handled and LazyMode property is true.")]
    public event RadTreeView.TreeViewShowExpanderEventHandler ShowExpander
    {
      add
      {
        this.treeElement.ShowExpander += value;
      }
      remove
      {
        this.treeElement.ShowExpander -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the RadTreeView report the data error.")]
    public event TreeNodeDataErrorEventHandler DataError
    {
      add
      {
        this.treeElement.DataError += value;
      }
      remove
      {
        this.treeElement.DataError -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the user begins dragging an item.")]
    public event RadTreeView.ItemDragHandler ItemDrag
    {
      add
      {
        this.treeElement.ItemDrag += value;
      }
      remove
      {
        this.treeElement.ItemDrag -= value;
      }
    }

    [Category("Action")]
    [Browsable(true)]
    [Description("Occurs when TreeView required editor.")]
    public event RadTreeView.EditorRequiredHandler EditorRequired
    {
      add
      {
        this.treeElement.EditorRequired += value;
      }
      remove
      {
        this.treeElement.EditorRequired -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs before the tree node label text is edited.")]
    public event TreeNodeEditingEventHandler Editing
    {
      add
      {
        this.treeElement.Editing += value;
      }
      remove
      {
        this.treeElement.Editing -= value;
      }
    }

    [Description("Occurs when initializing the active editor.")]
    [Category("Behavior")]
    public event TreeNodeEditorInitializedEventHandler EditorInitialized
    {
      add
      {
        this.treeElement.EditorInitialized += value;
      }
      remove
      {
        this.treeElement.EditorInitialized -= value;
      }
    }

    [Description("Occurs after the tree node label text is edited.")]
    [Category("Behavior")]
    public event TreeNodeEditedEventHandler Edited
    {
      add
      {
        this.treeElement.Edited += value;
      }
      remove
      {
        this.treeElement.Edited -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when the editor is changing the value during the editing process.")]
    public event TreeNodeValueChangingEventHandler ValueChanging
    {
      add
      {
        this.treeElement.ValueChanging += value;
      }
      remove
      {
        this.treeElement.ValueChanging -= value;
      }
    }

    [Description("Occurs when the editor finished the value editing.")]
    [Browsable(true)]
    [Category("Action")]
    public event TreeNodeValueChangedEventHandler ValueChanged
    {
      add
      {
        this.treeElement.ValueChanged += value;
      }
      remove
      {
        this.treeElement.ValueChanged -= value;
      }
    }

    [Description("Occurs when the editor changed the value editing.")]
    [Category("Action")]
    [Browsable(true)]
    public event TreeNodeValidatingEventHandler ValueValidating
    {
      add
      {
        this.treeElement.ValueValidating += value;
      }
      remove
      {
        this.treeElement.ValueValidating -= value;
      }
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs when editor validating fails.")]
    public event EventHandler ValidationError
    {
      add
      {
        this.treeElement.ValidationError += value;
      }
      remove
      {
        this.treeElement.ValidationError -= value;
      }
    }

    [Description("Occurs when a drag is ending ")]
    [Category("Behavior")]
    public event RadTreeView.DragEndingHandler DragEnding
    {
      add
      {
        this.treeElement.DragEnding += value;
      }
      remove
      {
        this.treeElement.DragEnding -= value;
      }
    }

    [Description("Occurs when a drag has ended ")]
    [Category("Behavior")]
    public event RadTreeView.DragEndedHandler DragEnded
    {
      add
      {
        this.treeElement.DragEnded += value;
      }
      remove
      {
        this.treeElement.DragEnded -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when a drag is starting")]
    public event RadTreeView.DragStartingHandler DragStarting
    {
      add
      {
        this.treeElement.DragStarting += value;
      }
      remove
      {
        this.treeElement.DragStarting -= value;
      }
    }

    [Description("Occurs when a drag has started")]
    [Category("Behavior")]
    public event RadTreeView.DragStartedHandler DragStarted
    {
      add
      {
        this.treeElement.DragStarted += value;
      }
      remove
      {
        this.treeElement.DragStarted -= value;
      }
    }

    [Description("Occurs when drag feedback is needed for a node.")]
    [Category("Behavior")]
    public event EventHandler<RadTreeViewDragCancelEventArgs> DragOverNode
    {
      add
      {
        this.treeElement.DragOverNode += value;
      }
      remove
      {
        this.treeElement.DragOverNode -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs before a node is selected.")]
    public virtual event RadTreeView.RadTreeViewCancelEventHandler SelectedNodeChanging
    {
      add
      {
        this.treeElement.SelectedNodeChanging += value;
      }
      remove
      {
        this.treeElement.SelectedNodeChanging -= value;
      }
    }

    [Description("Occurs after a node is selected.")]
    [Category("Behavior")]
    public virtual event RadTreeView.RadTreeViewEventHandler SelectedNodeChanged
    {
      add
      {
        this.treeElement.SelectedNodeChanged += value;
      }
      remove
      {
        this.treeElement.SelectedNodeChanged -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs after a Selected Nodes are cleared.")]
    public virtual event EventHandler SelectedNodesCleared
    {
      add
      {
        this.treeElement.SelectedNodesCleared += value;
      }
      remove
      {
        this.treeElement.SelectedNodesCleared -= value;
      }
    }

    [Description("Occurs when SelectedNodes collection has been changed.")]
    [Category("Behavior")]
    public virtual event EventHandler<RadTreeViewEventArgs> SelectedNodesChanged
    {
      add
      {
        this.treeElement.SelectedNodesChanged += value;
      }
      remove
      {
        this.treeElement.SelectedNodesChanged -= value;
      }
    }

    [Description("Occurs when the user presses a mouse button over a RadTreeNode.")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewMouseEventHandler NodeMouseDown
    {
      add
      {
        this.treeElement.NodeMouseDown += value;
      }
      remove
      {
        this.treeElement.NodeMouseDown -= value;
      }
    }

    [Description("Occurs when the user releases a mouse button over a RadTreeNode.")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewMouseEventHandler NodeMouseUp
    {
      add
      {
        this.treeElement.NodeMouseUp += value;
      }
      remove
      {
        this.treeElement.NodeMouseUp -= value;
      }
    }

    [Description("Occurs when the user moves the mouse in the area of a RadTreeNode.")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewMouseEventHandler NodeMouseMove
    {
      add
      {
        this.treeElement.NodeMouseMove += value;
      }
      remove
      {
        this.treeElement.NodeMouseMove -= value;
      }
    }

    [Description("Occurs when the mouse enters the area of a RadTreeNode.")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewEventHandler NodeMouseEnter
    {
      add
      {
        this.treeElement.NodeMouseEnter += value;
      }
      remove
      {
        this.treeElement.NodeMouseEnter -= value;
      }
    }

    [Description("Occurs when the mouse leaves the area of a RadTreeNode.")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewEventHandler NodeMouseLeave
    {
      add
      {
        this.treeElement.NodeMouseLeave += value;
      }
      remove
      {
        this.treeElement.NodeMouseLeave -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the mouse hovers over a RadTreeNode.")]
    public event RadTreeView.TreeViewEventHandler NodeMouseHover
    {
      add
      {
        this.treeElement.NodeMouseHover += value;
      }
      remove
      {
        this.treeElement.NodeMouseHover -= value;
      }
    }

    [Description("Occurs when a mouse button is clicked inside a TreeNodeElement")]
    [Category("Behavior")]
    public event RadTreeView.TreeViewEventHandler NodeMouseClick
    {
      add
      {
        this.treeElement.NodeMouseClick += value;
      }
      remove
      {
        this.treeElement.NodeMouseClick -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when a mouse button is double clicked inside a TreeNodeElement")]
    public event RadTreeView.TreeViewEventHandler NodeMouseDoubleClick
    {
      add
      {
        this.treeElement.NodeMouseDoubleClick += value;
      }
      remove
      {
        this.treeElement.NodeMouseDoubleClick -= value;
      }
    }

    [Description("Occurs when the value of the Checked property of a RadTreeNode is changing.")]
    [Category("Behavior")]
    public virtual event RadTreeView.RadTreeViewCancelEventHandler NodeCheckedChanging
    {
      add
      {
        this.treeElement.NodeCheckedChanging += value;
      }
      remove
      {
        this.treeElement.NodeCheckedChanging -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the value of the Checked property of a RadTreeNode is changed.")]
    public virtual event TreeNodeCheckedEventHandler NodeCheckedChanged
    {
      add
      {
        this.treeElement.NodeCheckedChanged += value;
      }
      remove
      {
        this.treeElement.NodeCheckedChanged -= value;
      }
    }

    [Browsable(true)]
    [Description("Occurs before the value of the Expanded property of a tree node is changed.")]
    [Category("Action")]
    public event RadTreeView.RadTreeViewCancelEventHandler NodeExpandedChanging
    {
      add
      {
        this.treeElement.NodeExpandedChanging += value;
      }
      remove
      {
        this.treeElement.NodeExpandedChanging -= value;
      }
    }

    [Description("Occurs after the value of the Expanded property of a tree node is changed.")]
    [Category("Action")]
    [Browsable(true)]
    public event RadTreeView.TreeViewEventHandler NodeExpandedChanged
    {
      add
      {
        this.treeElement.NodeExpandedChanged += value;
      }
      remove
      {
        this.treeElement.NodeExpandedChanged -= value;
      }
    }

    [Category("Action")]
    [Description(" Occurs when the Nodes collection requires to be populated in Load-On-Demand mode using LazyTreeNodeProvider.")]
    [Browsable(true)]
    public event NodesNeededEventHandler NodesNeeded
    {
      add
      {
        this.treeElement.NodesNeeded += value;
      }
      remove
      {
        this.treeElement.NodesNeeded -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when the node changes its state and needs to be formatted.")]
    public event TreeNodeFormattingEventHandler NodeFormatting
    {
      add
      {
        this.treeElement.NodeFormatting += value;
      }
      remove
      {
        this.treeElement.NodeFormatting -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when a new node is going to be created.")]
    public event CreateTreeNodeEventHandler CreateNode
    {
      add
      {
        this.treeElement.CreateNode += value;
      }
      remove
      {
        this.treeElement.CreateNode -= value;
      }
    }

    [Description("Occurs when a new node element is going to be created.")]
    [Category("Action")]
    public event CreateTreeNodeElementEventHandler CreateNodeElement
    {
      add
      {
        this.treeElement.CreateNodeElement += value;
      }
      remove
      {
        this.treeElement.CreateNodeElement -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when opening the context menu.")]
    public event TreeViewContextMenuOpeningEventHandler ContextMenuOpening
    {
      add
      {
        this.treeElement.ContextMenuOpening += value;
      }
      remove
      {
        this.treeElement.ContextMenuOpening -= value;
      }
    }

    [Description("Occurs after a node is removed.")]
    [Browsable(true)]
    [Category("Action")]
    public event RadTreeView.RadTreeViewEventHandler NodeRemoved
    {
      add
      {
        this.treeElement.NodeRemoved += value;
      }
      remove
      {
        this.treeElement.NodeRemoved -= value;
      }
    }

    [Description("Occurs before a node is removed.")]
    [Category("Action")]
    [Browsable(true)]
    public event RadTreeView.RadTreeViewCancelEventHandler NodeRemoving
    {
      add
      {
        this.treeElement.NodeRemoving += value;
      }
      remove
      {
        this.treeElement.NodeRemoving -= value;
      }
    }

    [Description("Occurs after a node is being added..")]
    [Browsable(true)]
    [Category("Action")]
    public event RadTreeView.RadTreeViewEventHandler NodeAdded
    {
      add
      {
        this.treeElement.NodeAdded += value;
      }
      remove
      {
        this.treeElement.NodeAdded -= value;
      }
    }

    [Browsable(true)]
    [Description("Occurs after a node is bound to a data item.")]
    [Category("Action")]
    public event RadTreeView.RadTreeViewEventHandler NodeDataBound
    {
      add
      {
        this.treeElement.NodeDataBound += value;
      }
      remove
      {
        this.treeElement.NodeDataBound -= value;
      }
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs before a node is being added.")]
    public event RadTreeView.RadTreeViewCancelEventHandler NodeAdding
    {
      add
      {
        this.treeElement.NodeAdding += value;
      }
      remove
      {
        this.treeElement.NodeAdding -= value;
      }
    }

    public void SetError(string text, RadTreeNode radTreeNode)
    {
      this.treeElement.SetError(text, radTreeNode);
    }

    public virtual RadTreeNode AddNodeByPath(string path)
    {
      return this.treeElement.AddNodeByPath(path);
    }

    public virtual RadTreeNode AddNodeByPath(string path, string pathSeparator)
    {
      return this.treeElement.AddNodeByPath(path, pathSeparator);
    }

    public virtual RadTreeNode GetNodeByPath(string path)
    {
      return this.treeElement.GetNodeByPath(path);
    }

    public virtual RadTreeNode GetNodeByPath(string path, string pathSeparator)
    {
      return this.treeElement.GetNodeByPath(path, pathSeparator);
    }

    public RadTreeNode GetNodeByName(string name)
    {
      return this.treeElement.GetNodeByName(name);
    }

    public RadTreeNode GetNodeByName(string name, RadTreeNode rootNode)
    {
      return this.treeElement.GetNodeByName(name, rootNode);
    }

    public void BringIntoView(RadTreeNode node)
    {
      this.treeElement.BringIntoView(node);
    }

    public RadTreeNode Find(Predicate<RadTreeNode> match)
    {
      return this.treeElement.Find(match);
    }

    public RadTreeNode Find<T>(FindAction<T> match, T arg)
    {
      return this.treeElement.Find<T>(match, arg);
    }

    public virtual RadTreeNode Find(string text)
    {
      return this.treeElement.Find(text);
    }

    public void ForEach(Action<RadTreeNode> action)
    {
      this.treeElement.ForEach(action);
    }

    public RadTreeNode[] FindNodes(Predicate<RadTreeNode> match)
    {
      return this.treeElement.FindNodes(match);
    }

    public RadTreeNode[] FindNodes<T>(FindAction<T> match, T arg)
    {
      return this.treeElement.FindNodes<T>(match, arg);
    }

    public virtual RadTreeNode[] FindNodes(string text)
    {
      return this.treeElement.FindNodes(text);
    }

    public object Execute(ICommand command, params object[] settings)
    {
      return this.treeElement.Execute(true, command, settings);
    }

    public object Execute(bool includeSubTrees, ICommand command, params object[] settings)
    {
      return this.treeElement.Execute(command, (object) includeSubTrees, (object) settings);
    }

    public bool BeginEdit()
    {
      return this.treeElement.BeginEdit();
    }

    public bool EndEdit()
    {
      return this.treeElement.EndEdit();
    }

    public void CancelEdit()
    {
      this.treeElement.CancelEdit();
    }

    public void LoadXML(string fileName, params System.Type[] extraTypes)
    {
      using (StreamReader streamReader = new StreamReader(fileName))
        this.LoadXMLWithReader((TextReader) streamReader, extraTypes);
    }

    public void LoadXML(Stream stream, params System.Type[] extraTypes)
    {
      this.LoadXMLWithReader((TextReader) new StreamReader(stream), extraTypes);
    }

    public void SaveXML(string fileName, params System.Type[] extraTypes)
    {
      using (StreamWriter streamWriter = new StreamWriter(fileName))
        this.SaveXMLWithWriter((TextWriter) streamWriter, extraTypes);
    }

    public void SaveXML(Stream stream, params System.Type[] extraTypes)
    {
      this.SaveXMLWithWriter((TextWriter) new StreamWriter(stream, Encoding.UTF8), extraTypes);
    }

    public void BeginUpdate()
    {
      this.TreeViewElement.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.TreeViewElement.EndUpdate();
    }

    public virtual IDisposable DeferRefresh()
    {
      return this.TreeViewElement.DeferRefresh();
    }

    public void CollapseAll()
    {
      this.TreeViewElement.CollapseAll();
    }

    public void CollapseAll(RadTreeNodeCollection nodes)
    {
      foreach (RadTreeNode node in (Collection<RadTreeNode>) nodes)
        node.Collapse();
    }

    public void ExpandAll()
    {
      this.TreeViewElement.ExpandAll();
    }

    public virtual void ExpandAll(RadTreeNodeCollection nodes)
    {
      foreach (RadTreeNode node in (Collection<RadTreeNode>) nodes)
        node.Expand();
    }

    public RadTreeNode GetNodeAt(Point pt)
    {
      return this.GetNodeAt(pt.X, pt.Y);
    }

    public RadTreeNode GetNodeAt(int x, int y)
    {
      return this.TreeViewElement.GetNodeAt(x, y);
    }

    public int GetNodeCount(bool includeSubTrees)
    {
      return this.TreeViewElement.GetNodeCount(includeSubTrees);
    }

    public override string ToString()
    {
      string str = base.ToString();
      if (this.Nodes != null)
      {
        str = str + ", Nodes.Count: " + this.Nodes.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        if (this.Nodes.Count > 0)
          str = str + ", Nodes[0]: " + this.Nodes[0].ToString();
      }
      return str;
    }

    public virtual void SelectAll()
    {
      this.treeElement.SelectAll();
    }

    public virtual void ClearSelection()
    {
      this.treeElement.ClearSelection();
    }

    public override void BeginInit()
    {
      base.BeginInit();
      this.treeElement.BeginInit();
    }

    public override void EndInit()
    {
      this.treeElement.EndInit();
      base.EndInit();
    }

    protected override void OnClick(EventArgs e)
    {
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs != null && this.RootElement.ElementTree.GetElementAtPoint<RadScrollBarElement>(mouseEventArgs.Location) != null)
        return;
      base.OnClick(e);
    }

    protected virtual void LoadXMLWithReader(TextReader reader, params System.Type[] extraTypes)
    {
      ((extraTypes == null || extraTypes.Length == 0 ? (XmlSerializer) new XmlTreeSerializer(typeof (XmlTreeView)) : (XmlSerializer) new XmlTreeSerializer(typeof (XmlTreeView), extraTypes)).Deserialize(reader) as XmlTreeView).Deserialize(this);
    }

    protected virtual void SaveXMLWithWriter(TextWriter writer, params System.Type[] extraTypes)
    {
      XmlTreeView xmlTreeView = new XmlTreeView(this);
      (extraTypes == null || extraTypes.Length == 0 ? (XmlSerializer) new XmlTreeSerializer(typeof (XmlTreeView)) : (XmlSerializer) new XmlTreeSerializer(typeof (XmlTreeView), extraTypes)).Serialize(writer, (object) xmlTreeView);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(150, 250));
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 123)
        this.WmContextMenu(ref m);
      else
        base.WndProc(ref m);
    }

    private void WmContextMenu(ref Message m)
    {
      if (this.contextMenuKeyPressed)
      {
        this.contextMenuKeyPressed = false;
        if (this.treeElement.SelectedNode == null)
          return;
        TreeNodeElement element = this.treeElement.GetElement(this.treeElement.SelectedNode);
        if (element != null)
        {
          Point location = element.ControlBoundingRectangle.Location;
          location.Offset(3, 3);
          if (this.treeElement.ProcessContextMenu(location))
            return;
        }
      }
      int x = Telerik.WinControls.NativeMethods.Util.SignedLOWORD(m.LParam);
      int y = Telerik.WinControls.NativeMethods.Util.SignedHIWORD(m.LParam);
      Point point = (int) (long) m.LParam != -1 ? this.PointToClient(new Point(x, y)) : new Point(this.Width / 2, this.Height / 2);
      if (this.treeElement.ProcessContextMenu(point))
        return;
      ContextMenu contextMenu = this.ContextMenu;
      ContextMenuStrip contextMenuStrip = contextMenu != null ? (ContextMenuStrip) null : this.ContextMenuStrip;
      if ((contextMenu != null || contextMenuStrip != null) && this.ClientRectangle.Contains(point))
      {
        if (contextMenu != null)
          contextMenu.Show((Control) this, point);
        else
          contextMenuStrip?.Show((Control) this, point);
      }
      else
        this.DefWndProc(ref m);
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      if (this.BindingContext != null)
        this.TreeViewElement.BindingContext = this.BindingContext;
      base.OnBindingContextChanged(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.treeElement.Update(RadTreeViewElement.UpdateActions.StateChanged);
      if (!this.HideSelection || this.oldSelected == null)
        return;
      this.SelectedNode = this.oldSelected;
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.ContainsFocus && this.IsEditing)
        return;
      if (!this.ContainsFocus)
        this.TreeViewElement.EndEdit();
      this.treeElement.Update(RadTreeViewElement.UpdateActions.StateChanged);
      if (!this.HideSelection || this.SelectedNode == null)
        return;
      this.oldSelected = this.SelectedNode;
      this.SelectedNode = (RadTreeNode) null;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseClick(e))
        return;
      base.OnMouseClick(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseDoubleClick(e))
        return;
      base.OnMouseDoubleClick(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Apps)
        this.contextMenuKeyPressed = true;
      if (this.TreeViewElement.ProcessKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.TreeViewElement.ProcessKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.TreeViewElement.ProecessMouseEnter(e))
        return;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.TreeViewElement.ProecessMouseLeave(e))
        return;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.TreeViewElement.ProcessMouseWheel(e))
      {
        HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
        if (handledMouseEventArgs != null)
          handledMouseEventArgs.Handled = true;
      }
      base.OnMouseWheel(e);
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
        case Keys.Left | Keys.Shift:
        case Keys.Up | Keys.Shift:
        case Keys.Right | Keys.Shift:
        case Keys.Down | Keys.Shift:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected override void OnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      base.OnThemeNameChanged(e);
      this.treeElement.Update(RadTreeViewElement.UpdateActions.Reset);
    }

    [Category("Behavior")]
    [DefaultValue(typeof (ExpandMode), "Multiple")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ExpandMode ExpandMode
    {
      get
      {
        return this.treeElement.ExpandMode;
      }
      set
      {
        this.treeElement.ExpandMode = value;
      }
    }

    [Description("Gets or sets a value indicating whether the default context menu is enabled.")]
    [DefaultValue(false)]
    public bool AllowDefaultContextMenu
    {
      get
      {
        return this.treeElement.AllowDefaultContextMenu;
      }
      set
      {
        if (this.treeElement.AllowDefaultContextMenu == value)
          return;
        this.treeElement.AllowDefaultContextMenu = value;
        this.OnNotifyPropertyChanged(nameof (AllowDefaultContextMenu));
      }
    }

    public static List<object> ExecuteBatchCommand(
      RadTreeNodeCollection nodes,
      int level,
      ICommand command,
      params object[] settings)
    {
      if (nodes == null || nodes.Count == 0)
        return new List<object>();
      List<object> objectList = new List<object>();
      for (int index = 0; index < nodes.Count; ++index)
        objectList.AddRange((IEnumerable<object>) RadTreeView.ExecuteBatchCommand(nodes[index], level, command, settings));
      return objectList;
    }

    public static object ExecuteScalarCommand(
      RadTreeNode node,
      int level,
      ICommand command,
      params object[] settings)
    {
      if (node == null)
        return (object) null;
      object obj = (object) null;
      Queue<RadTreeNode> radTreeNodeQueue = new Queue<RadTreeNode>();
      radTreeNodeQueue.Enqueue(node);
      while (radTreeNodeQueue.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeQueue.Dequeue();
        if (level == -1 || radTreeNode.Level == level)
        {
          obj = command.Execute((object) radTreeNode, (object) settings);
          if (obj == null)
          {
            if (radTreeNode.Level == level)
              continue;
          }
          else
            break;
        }
        for (int index = 0; index < radTreeNode.Nodes.Count; ++index)
        {
          RadTreeNode node1 = radTreeNode.Nodes[index];
          radTreeNodeQueue.Enqueue(node1);
        }
      }
      return obj;
    }

    public static List<object> ExecuteBatchCommand(
      RadTreeNode node,
      int level,
      ICommand command,
      params object[] settings)
    {
      if (node == null)
        return new List<object>();
      List<object> objectList = new List<object>();
      Queue<RadTreeNode> radTreeNodeQueue = new Queue<RadTreeNode>();
      radTreeNodeQueue.Enqueue(node);
      while (radTreeNodeQueue.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeQueue.Dequeue();
        if (level == -1 || radTreeNode.Level == level)
        {
          object obj = command.Execute((object) radTreeNode, (object) settings);
          if (obj != null)
            objectList.Add(obj);
          if (radTreeNode.Level == level)
            continue;
        }
        for (int index = 0; index < radTreeNode.Nodes.Count; ++index)
        {
          RadTreeNode node1 = radTreeNode.Nodes[index];
          radTreeNodeQueue.Enqueue(node1);
        }
      }
      return objectList;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadTreeViewAccessibleObject(this);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Data != null)
        this.GetChildPropertyValueRecursivly(request, this.AccessibilityObject);
      else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "ItemsCount")
        request.Data = (object) this.Nodes.Count;
      else
        base.ProcessCodedUIMessage(ref request);
    }

    private bool GetChildPropertyValueRecursivly(IPCMessage request, AccessibleObject root)
    {
      int childCount = root.GetChildCount();
      for (int index = 0; index < childCount; ++index)
      {
        AccessibleObject child = root.GetChild(index);
        if (child.Name == (string) request.Data)
        {
          RadItemAccessibleObject accessibleObject = child as RadItemAccessibleObject;
          if (accessibleObject != null && accessibleObject.Name == (string) request.Data)
          {
            RadTreeNode owner = accessibleObject.Owner as RadTreeNode;
            if (request.Message == "Expanded")
            {
              request.Data = (object) owner.Expanded;
              return true;
            }
            if (request.Message == "Selected")
            {
              request.Data = (object) owner.Selected;
              return true;
            }
            if (request.Message == "Checked")
            {
              request.Data = (object) owner.Checked;
              return true;
            }
            if (request.Message == "CheckState")
            {
              request.Data = (object) owner.CheckState.ToString();
              return true;
            }
            if (request.Message == "Text")
            {
              request.Data = owner == null ? (object) "" : (object) owner.Text;
              return true;
            }
            if (request.Message == "HasChildNodes")
            {
              request.Data = (object) (owner.Nodes.Count > 0);
              return true;
            }
            if (!(request.Message == "ItemsCount"))
              return false;
            request.Data = (object) owner.Nodes.Count;
            return true;
          }
        }
      }
      return false;
    }

    public delegate void RadTreeViewEventHandler(object sender, RadTreeViewEventArgs e);

    public delegate void TreeViewEventHandler(object sender, RadTreeViewEventArgs e);

    public delegate void TreeViewShowExpanderEventHandler(
      object sender,
      TreeViewShowExpanderEventArgs e);

    public delegate void RadTreeViewCancelEventHandler(object sender, RadTreeViewCancelEventArgs e);

    public delegate void TreeViewMouseEventHandler(object sender, RadTreeViewMouseEventArgs e);

    public delegate void ItemDragHandler(object sender, RadTreeViewEventArgs e);

    public delegate void EditorRequiredHandler(object sender, TreeNodeEditorRequiredEventArgs e);

    public delegate void DragStartedHandler(object sender, RadTreeViewDragEventArgs e);

    public delegate void DragStartingHandler(object sender, RadTreeViewDragCancelEventArgs e);

    public delegate void DragEndingHandler(object sender, RadTreeViewDragCancelEventArgs e);

    public delegate void DragEndedHandler(object sender, RadTreeViewDragEventArgs e);
  }
}
