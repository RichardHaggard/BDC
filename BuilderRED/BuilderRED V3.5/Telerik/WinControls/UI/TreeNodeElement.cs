// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class TreeNodeElement : StackLayoutElement, IVirtualizedElement<RadTreeNode>
  {
    private RadBitVector64 states = new RadBitVector64(0L);
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentProperty = RadProperty.Register(nameof (IsCurrent), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsControlInactiveProperty = RadProperty.Register("IsControlInactive", typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsRootNodeProperty = RadProperty.Register(nameof (IsRootNode), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register(nameof (HotTracking), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FullRowSelectProperty = RadProperty.Register("FullRowSelect", typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HasChildrenProperty = RadProperty.Register(nameof (HasChildren), typeof (bool), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (int), typeof (TreeNodeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.AffectsDisplay));
    protected const long AlrternatingColorSetState = 1;
    protected const long FullRowSelectState = 2;
    protected const long UpdateScrollRangeIfNeeded = 4;
    private RadTreeNode node;
    private TreeNodeLinesContainer linesElement;
    private TreeNodeExpanderItem expanderElement;
    private RadToggleButtonElement toggleElement;
    private TreeNodeImageElement imageElement;
    private TreeNodeContentElement contentElement;
    private IInputEditor editor;

    static TreeNodeElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TreeNodeElementStateManager(), typeof (TreeNodeElement));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.linesElement = new TreeNodeLinesContainer();
      this.Children.Add((RadElement) this.linesElement);
      this.expanderElement = new TreeNodeExpanderItem();
      int num1 = (int) this.expanderElement.BindProperty(TreeNodeExpanderItem.IsSelectedProperty, (RadObject) this, TreeNodeElement.IsSelectedProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.expanderElement.BindProperty(TreeNodeExpanderItem.HotTrackingProperty, (RadObject) this, TreeNodeElement.HotTrackingProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.expanderElement);
      this.imageElement = new TreeNodeImageElement();
      this.imageElement.ClipDrawing = true;
      this.imageElement.StretchHorizontally = false;
      this.Children.Add((RadElement) this.imageElement);
      this.contentElement = this.CreateContentElement();
      int num3 = (int) this.contentElement.BindProperty(TreeNodeContentElement.IsRootNodeProperty, (RadObject) this, TreeNodeElement.IsRootNodeProperty, PropertyBindingOptions.OneWay);
      int num4 = (int) this.contentElement.BindProperty(TreeNodeContentElement.HasChildrenProperty, (RadObject) this, TreeNodeElement.HasChildrenProperty, PropertyBindingOptions.OneWay);
      int num5 = (int) this.contentElement.BindProperty(TreeNodeContentElement.IsControlInactiveProperty, (RadObject) this, TreeNodeElement.IsControlInactiveProperty, PropertyBindingOptions.OneWay);
      int num6 = (int) this.contentElement.BindProperty(TreeNodeContentElement.FullRowSelectProperty, (RadObject) this, TreeNodeElement.FullRowSelectProperty, PropertyBindingOptions.OneWay);
      int num7 = (int) this.contentElement.BindProperty(TreeNodeContentElement.IsSelectedProperty, (RadObject) this, TreeNodeElement.IsSelectedProperty, PropertyBindingOptions.OneWay);
      int num8 = (int) this.contentElement.BindProperty(TreeNodeContentElement.IsCurrentProperty, (RadObject) this, TreeNodeElement.IsCurrentProperty, PropertyBindingOptions.OneWay);
      int num9 = (int) this.contentElement.BindProperty(TreeNodeContentElement.HotTrackingProperty, (RadObject) this, TreeNodeElement.HotTrackingProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.contentElement.BindProperty(TreeNodeContentElement.IsExpandedProperty, (RadObject) this, TreeNodeElement.IsExpandedProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.contentElement);
    }

    protected virtual RadToggleButtonElement CreateToggleElement()
    {
      RadToggleButtonElement toggleButtonElement = (RadToggleButtonElement) null;
      switch (this.node.CheckType)
      {
        case CheckType.CheckBox:
          toggleButtonElement = (RadToggleButtonElement) new TreeNodeCheckBoxElement();
          break;
        case CheckType.RadioButton:
          toggleButtonElement = (RadToggleButtonElement) new RadRadioButtonElement();
          break;
      }
      return toggleButtonElement;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.RightToLeftMode = StackLayoutElement.RightToLeftModes.ReverseOffset;
      this.Orientation = Orientation.Horizontal;
      this.StretchHorizontally = true;
      this.FitInAvailableSize = true;
      this.states[4L] = false;
      this.states[1L] = false;
      this.states[2L] = false;
    }

    protected virtual TreeNodeContentElement CreateContentElement()
    {
      TreeNodeContentElement nodeContentElement = new TreeNodeContentElement();
      nodeContentElement.ShouldHandleMouseInput = false;
      return nodeContentElement;
    }

    protected override void DisposeManagedResources()
    {
      this.Detach();
      base.DisposeManagedResources();
    }

    [Category("Appearance")]
    [Description("Gets a value indicating that the node is selected")]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.IsSelectedProperty);
      }
    }

    [Description("Gets a value indicating that this is the current node.")]
    [Category("Appearance")]
    public virtual bool IsCurrent
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.IsCurrentProperty);
      }
    }

    [Category("Appearance")]
    [Description("Gets a value indicating that the node is expanded")]
    public virtual bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.IsExpandedProperty);
      }
    }

    [Description("Gets a value indicating that the node is expanded")]
    [Category("Appearance")]
    public virtual bool IsControlFocused
    {
      get
      {
        return !(bool) this.GetValue(TreeNodeElement.IsControlInactiveProperty);
      }
    }

    [Category("Appearance")]
    [Description("Gets a value indicating whether the node is currently at root level.")]
    public virtual bool IsRootNode
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.IsRootNodeProperty);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the arbitrary height for this particular node. Valid when the owning RadTreeViewElement's AllowArbitraryHeight property is set to true.")]
    public virtual int ItemHeight
    {
      get
      {
        RadTreeViewElement treeViewElement = this.TreeViewElement;
        if (this.Data != null && treeViewElement != null)
        {
          if (this.Data.ItemHeight != -1 && (treeViewElement.AllowArbitraryItemHeight || treeViewElement.AutoSizeItems))
            return this.Data.ItemHeight;
          if (this.GetValueSource(TreeNodeElement.ItemHeightProperty) == ValueSource.DefaultValue)
            return treeViewElement.ItemHeight;
        }
        return (int) this.GetValue(TreeNodeElement.ItemHeightProperty);
      }
      set
      {
        if (this.Data == null)
          return;
        this.Data.ItemHeight = value;
      }
    }

    [Description("Gets a value indicating that this is the hot tracking node.")]
    [Category("Appearance")]
    public virtual bool HotTracking
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.HotTrackingProperty);
      }
    }

    [Description("Gets a value indicating whether this node contains child nodes.")]
    [Category("Appearance")]
    public virtual bool HasChildren
    {
      get
      {
        return (bool) this.GetValue(TreeNodeElement.HasChildrenProperty);
      }
    }

    public override string ToolTipText
    {
      get
      {
        string str = base.ToolTipText;
        if (string.IsNullOrEmpty(str) && this.node != null)
          str = this.node.ToolTipText;
        if (this.TreeViewElement != null && !this.TreeViewElement.ShowNodeToolTips)
          str = string.Empty;
        return str;
      }
      set
      {
        base.ToolTipText = value;
      }
    }

    public TreeNodeLinesContainer LinesContainerElement
    {
      get
      {
        return this.linesElement;
      }
    }

    public TreeNodeExpanderItem ExpanderElement
    {
      get
      {
        return this.expanderElement;
      }
    }

    public RadToggleButtonElement ToggleElement
    {
      get
      {
        return this.toggleElement;
      }
    }

    public TreeNodeImageElement ImageElement
    {
      get
      {
        return this.imageElement;
      }
    }

    public TreeNodeContentElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    public RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.FindAncestor<RadTreeViewElement>();
      }
    }

    public RadTreeNode Data
    {
      get
      {
        return this.node;
      }
    }

    public virtual void Attach(RadTreeNode data, object context)
    {
      this.node = data;
      this.node.PropertyChanged += new PropertyChangedEventHandler(this.node_PropertyChanged);
      this.Synchronize();
      bool animationsEnabled = AnimatedPropertySetting.AnimationsEnabled;
      AnimatedPropertySetting.AnimationsEnabled = false;
      if (this.Style != null)
        this.Style.Apply((RadObject) this, false);
      AnimatedPropertySetting.AnimationsEnabled = animationsEnabled;
    }

    public virtual void Detach()
    {
      this.states[4L] = false;
      if (this.node != null && this.node.TreeViewElement != null && (this.node.TreeViewElement.SelectedNode == this.node && this.node.TreeViewElement.ActiveEditor != null))
        this.node.TreeViewElement.EndEdit();
      int num1 = (int) this.ResetValue(TreeNodeElement.HotTrackingProperty, ValueResetFlags.Local);
      LightVisualElement lightVisualElement = (LightVisualElement) this;
      if (!this.states[2L])
        lightVisualElement = (LightVisualElement) this.ContentElement;
      int num2 = (int) this.ImageElement.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.Local);
      int num3 = (int) this.ContentElement.ResetValue(RadElement.EnabledProperty, ValueResetFlags.Local);
      int num4 = (int) this.ContentElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num5 = (int) this.ContentElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
      int num6 = (int) lightVisualElement.ResetValue(VisualElement.OpacityProperty);
      int num7 = (int) lightVisualElement.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
      int num8 = (int) lightVisualElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      int num9 = (int) lightVisualElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      int num10 = (int) lightVisualElement.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
      int num11 = (int) lightVisualElement.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
      int num12 = (int) lightVisualElement.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
      int num13 = (int) lightVisualElement.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
      int num14 = (int) lightVisualElement.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
      int num15 = (int) lightVisualElement.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
      int num16 = (int) lightVisualElement.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
      int num17 = (int) lightVisualElement.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
      int num18 = (int) lightVisualElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
      int num19 = (int) lightVisualElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
      if (this.node == null)
        return;
      this.node.PropertyChanged -= new PropertyChangedEventHandler(this.node_PropertyChanged);
      this.node = (RadTreeNode) null;
    }

    protected virtual void DisposeToggleElement()
    {
      if (this.toggleElement == null)
        return;
      this.toggleElement.ToggleStateChanging -= new StateChangingEventHandler(this.ToggleElement_ToggleStateChanging);
      this.Children.Remove((RadElement) this.toggleElement);
      this.toggleElement.Dispose();
      this.toggleElement = (RadToggleButtonElement) null;
    }

    public virtual void Synchronize()
    {
      if (!this.IsInValidState(true))
        return;
      this.states[2L] = this.TreeViewElement.FullRowSelect;
      int num1 = (int) this.SetValue(TreeNodeElement.IsSelectedProperty, (object) this.node.Selected);
      int num2 = (int) this.SetValue(TreeNodeElement.IsCurrentProperty, (object) this.node.Current);
      int num3 = (int) this.SetValue(TreeNodeElement.IsExpandedProperty, (object) this.node.Expanded);
      int num4 = (int) this.SetValue(TreeNodeElement.IsRootNodeProperty, (object) this.node.IsRootNode);
      int num5 = (int) this.SetValue(TreeNodeElement.FullRowSelectProperty, (object) this.TreeViewElement.FullRowSelect);
      int num6 = (int) this.SetValue(TreeNodeElement.HasChildrenProperty, (object) (bool) (this.TreeViewElement.FullLazyMode ? 1 : (this.node.HasNodes ? 1 : 0)));
      if (this.IsInValidState(true))
      {
        bool flag = this.ElementTree.Control.Focused || this.ElementTree.Control.ContainsFocus;
        int num7 = (int) this.SetValue(TreeNodeElement.IsControlInactiveProperty, (object) !flag);
      }
      this.ContentElement.Synchronize();
      this.ExpanderElement.Synchronize();
      this.LinesContainerElement.Synchronize();
      this.SynchronizeToggleElement();
      this.ImageElement.Synchronize();
      int num8 = (int) this.SetValue(RadElement.EnabledProperty, (object) this.node.Enabled);
      this.ApplyStyle();
      this.OnFormatting();
    }

    protected virtual void SynchronizeToggleElement()
    {
      CheckType checkType = this.node.CheckType;
      if (checkType == CheckType.None)
        this.DisposeToggleElement();
      else if (checkType == CheckType.CheckBox && this.toggleElement is RadCheckBoxElement || checkType == CheckType.RadioButton && this.toggleElement is RadRadioButtonElement)
      {
        this.toggleElement.ToggleState = this.node.CheckState;
      }
      else
      {
        this.DisposeToggleElement();
        this.toggleElement = this.CreateToggleElement();
        if (this.toggleElement == null)
          return;
        this.Children.Insert(this.Children.IndexOf((RadElement) this.ExpanderElement) + 1, (RadElement) this.toggleElement);
        this.toggleElement.StretchHorizontally = false;
        this.toggleElement.ToggleState = this.node.CheckState;
        this.toggleElement.ShouldHandleMouseInput = true;
        this.toggleElement.NotifyParentOnMouseInput = false;
        this.toggleElement.ToggleStateChanging += new StateChangingEventHandler(this.ToggleElement_ToggleStateChanging);
      }
    }

    protected virtual void ApplyStyle()
    {
      if (this.node == null || !this.node.HasStyle || this.TreeViewElement == null)
        return;
      TreeNodeStyle style = this.node.Style;
      if (style.ForeColor != Color.Empty)
      {
        this.ContentElement.ForeColor = style.ForeColor;
      }
      else
      {
        int num1 = (int) this.ContentElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
      }
      if (style.TextAlignment != ContentAlignment.MiddleLeft)
      {
        this.ContentElement.TextAlignment = style.TextAlignment;
      }
      else
      {
        int num2 = (int) this.ContentElement.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
      }
      LightVisualElement element = (LightVisualElement) this;
      if (!this.TreeViewElement.FullRowSelect)
        element = (LightVisualElement) this.ContentElement;
      int num3 = 0;
      if (style.BackColor != Color.Empty)
      {
        element.DrawFill = true;
        element.BackColor = style.BackColor;
      }
      else
      {
        ++num3;
        int num4 = (int) element.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      }
      if (style.BackColor2 != Color.Empty)
      {
        this.EnsureDrawFill(element, 2);
        element.BackColor2 = style.BackColor2;
      }
      else
      {
        ++num3;
        int num4 = (int) element.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
      }
      if (style.BackColor3 != Color.Empty)
      {
        this.EnsureDrawFill(element, 3);
        element.BackColor3 = style.BackColor3;
      }
      else
      {
        ++num3;
        int num4 = (int) element.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
      }
      if (style.BackColor4 != Color.Empty)
      {
        this.EnsureDrawFill(element, 4);
        element.BackColor4 = style.BackColor4;
      }
      else
      {
        ++num3;
        int num4 = (int) element.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
      }
      if (num3 >= this.node.NumberOfColors && style.BackColor == Color.Empty)
      {
        int num5 = (int) element.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      }
      if (style.BorderColor != Color.Empty)
      {
        element.DrawBorder = true;
        element.BorderColor = style.BorderColor;
      }
      else
      {
        int num4 = (int) element.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
        int num6 = (int) element.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
      }
      if ((double) style.GradientAngle != 90.0)
      {
        element.GradientAngle = style.GradientAngle;
      }
      else
      {
        int num7 = (int) element.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
      }
      if ((double) style.GradientPercentage != 0.5)
      {
        element.GradientPercentage = style.GradientPercentage;
      }
      else
      {
        int num8 = (int) element.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
      }
      if ((double) style.GradientPercentage2 != 0.5)
      {
        element.GradientPercentage2 = style.GradientPercentage2;
      }
      else
      {
        int num9 = (int) element.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
      }
      if (style.GradientStyle != GradientStyles.Linear)
      {
        element.GradientStyle = style.GradientStyle;
      }
      else
      {
        int num10 = (int) element.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
      }
      if (style.NumberOfColors != 4 && element.NumberOfColors != style.NumberOfColors)
      {
        element.NumberOfColors = style.NumberOfColors;
      }
      else
      {
        int num11 = (int) element.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
      }
      if (style.Font != null)
      {
        this.ContentElement.Font = style.Font;
      }
      else
      {
        int num12 = (int) element.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      }
    }

    private void EnsureDrawFill(LightVisualElement element, int backColorNo)
    {
      if (this.node.Style.GradientStyle == GradientStyles.Solid || this.node.Style.NumberOfColors < backColorNo)
        return;
      element.DrawFill = true;
    }

    public virtual bool IsCompatible(RadTreeNode data, object context)
    {
      if (this.Data != null)
        return data.Level == this.Data.Level;
      return true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseDown(new RadTreeViewMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseUp(new RadTreeViewMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseMove(new RadTreeViewMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseEnter(new RadTreeViewEventArgs(this.Data));
    }

    protected override void OnMouseHover(EventArgs e)
    {
      base.OnMouseHover(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseHover(new RadTreeViewEventArgs(this.Data));
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseLeave(new RadTreeViewEventArgs(this.Data));
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseClick(new RadTreeViewEventArgs(this.Data));
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeMouseDoubleClick(new RadTreeViewEventArgs(this.Data));
    }

    private void ToggleElement_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      if (!this.IsInValidState(true))
        return;
      RadTreeNode node = this.node;
      node.CheckState = args.NewValue;
      args.Cancel = node.CheckState != args.NewValue;
    }

    private void node_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!this.IsInValidState(true))
        return;
      this.OnNodePropertyChanged(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.IsMouseOverElementProperty && this.IsInValidState(true))
      {
        if (this.TreeViewElement.HotTracking)
        {
          int num1 = (int) this.SetValue(TreeNodeElement.HotTrackingProperty, e.NewValue);
        }
        else
        {
          int num2 = (int) this.SetValue(TreeNodeElement.HotTrackingProperty, (object) false);
        }
        this.ApplyStyle();
        this.OnFormatting();
      }
      else if (e.Property == TreeNodeElement.IsCurrentProperty)
      {
        this.ApplyStyle();
      }
      else
      {
        if (e.Property != LightVisualElement.NumberOfColorsProperty || e.NewValueSource != ValueSource.Style)
          return;
        this.ApplyStyle();
      }
    }

    protected virtual void OnNodePropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Selected")
      {
        int num = (int) this.SetValue(TreeNodeElement.IsSelectedProperty, (object) this.node.Selected);
        this.OnFormatting();
      }
      else if (e.PropertyName == "Current")
      {
        int num = (int) this.SetValue(TreeNodeElement.IsCurrentProperty, (object) this.node.Current);
        this.OnFormatting();
      }
      else if (e.PropertyName == "Text")
      {
        this.ContentElement.Text = this.node.Text;
        this.OnFormatting();
      }
      else if (e.PropertyName == "Expanded")
      {
        int num = (int) this.SetValue(TreeNodeElement.IsExpandedProperty, (object) this.node.Expanded);
        this.ExpanderElement.Synchronize();
        this.OnFormatting();
      }
      else if (e.PropertyName == "CheckState" && this.toggleElement != null)
      {
        this.toggleElement.ToggleState = this.node.CheckState;
      }
      else
      {
        if (!(e.PropertyName == "Style"))
          return;
        this.ApplyStyle();
        this.OnFormatting();
      }
    }

    protected virtual void OnFormatting()
    {
      RadTreeViewElement treeViewElement = this.TreeViewElement;
      if (treeViewElement != null && treeViewElement.AllowAlternatingRowColor)
      {
        bool flag = !treeViewElement.FullRowSelect || !this.Data.Current && !this.Data.Selected && !this.HotTracking;
        int num = treeViewElement.FirstVisibleIndex + this.Parent.Children.IndexOf((RadElement) this);
        if (flag && num % 2 != 0)
        {
          this.DrawFill = true;
          this.GradientStyle = GradientStyles.Solid;
          this.BackColor = treeViewElement.AlternatingRowColor;
          this.StretchHorizontally = true;
          this.states[1L] = true;
          this.TreeViewElement.OnNodeFormatting(new TreeNodeFormattingEventArgs(this));
          return;
        }
      }
      if (this.states[1L])
      {
        int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        int num4 = (int) this.ResetValue(RadElement.StretchHorizontallyProperty, ValueResetFlags.Local);
      }
      this.TreeViewElement.OnNodeFormatting(new TreeNodeFormattingEventArgs(this));
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      if (base.CanDragCore(dragStartPoint))
        return this.Enabled;
      return false;
    }

    protected override Image GetDragHintCore()
    {
      ISupportDrag imageElement = (ISupportDrag) this.imageElement;
      Image dragHint1 = ((ISupportDrag) this.ContentElement).GetDragHint();
      if (imageElement == null)
        return dragHint1;
      Image dragHint2 = imageElement.GetDragHint();
      if (dragHint2 == null)
        return dragHint1;
      if (dragHint1 == null)
        return dragHint2;
      Bitmap bitmap = new Bitmap(dragHint2.Width + dragHint1.Width, Math.Max(dragHint2.Height, dragHint1.Height));
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.DrawImage(dragHint2, Point.Empty);
        graphics.DrawImage(dragHint1, new Point(dragHint2.Width, 0));
      }
      return (Image) bitmap;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RadTreeViewElement treeViewElement = this.TreeViewElement;
      SizeF sizeF = base.MeasureOverride(availableSize);
      int height1 = this.Data.ActualSize.Height;
      int height2 = this.ItemHeight;
      if (treeViewElement != null && (this.Editor != null && (double) height2 < (double) sizeF.Height || treeViewElement.AutoSizeItems))
      {
        height2 = (int) sizeF.Height;
        this.states[4L] = true;
      }
      else
        sizeF.Height = (float) height2;
      if (treeViewElement != null && height1 != height2 && (this.states[4L] || treeViewElement.AllowArbitraryItemHeight || treeViewElement.AutoSizeItems))
      {
        ItemScroller<RadTreeNode> scroller = treeViewElement.Scroller;
        this.Data.ActualSize = new Size(this.Data.ActualSize.Width, height2);
        scroller.UpdateScrollRange(scroller.Scrollbar.Maximum + (height2 - height1), false);
      }
      if (this.Editor == null)
        this.states[4L] = false;
      if (!float.IsInfinity(availableSize.Width))
      {
        this.Data.ActualSize = new Size(this.Data.ActualSize.Width, sizeF.ToSize().Height);
        sizeF.Width = availableSize.Width;
      }
      else if (!this.ContentElement.TextWrap && this.Editor == null)
        this.Data.ActualSize = sizeF.ToSize();
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.PositionOffset = new SizeF(((VirtualizedStackContainer<RadTreeNode>) this.Parent).ScrollOffset.Width / this.DpiScaleFactor.Width, 0.0f);
      base.ArrangeOverride(finalSize);
      return finalSize;
    }

    public bool IsInEditMode
    {
      get
      {
        return this.editor != null;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public virtual void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.editor == editor)
        return;
      this.editor = editor;
      RadItem editorElement = this.GetEditorElement((IValueEditor) editor);
      if (editorElement == null || this.ContentElement.Children.Contains((RadElement) editorElement))
        return;
      this.ContentElement.Children.Add((RadElement) editorElement);
      this.TreeViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      if (editor == null || this.editor != editor)
        return;
      RadItem editorElement = this.GetEditorElement((IValueEditor) editor);
      if (editorElement != null && this.ContentElement.Children.Contains((RadElement) editorElement))
        this.ContentElement.Children.Remove((RadElement) editorElement);
      this.editor = (IInputEditor) null;
      this.Synchronize();
    }

    private RadItem GetEditorElement(IValueEditor editor)
    {
      BaseInputEditor editor1 = this.editor as BaseInputEditor;
      if (editor1 != null)
        return editor1.EditorElement as RadItem;
      return editor as RadItem;
    }
  }
}
