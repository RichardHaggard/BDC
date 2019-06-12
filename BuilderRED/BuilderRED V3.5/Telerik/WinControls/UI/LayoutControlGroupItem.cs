// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class LayoutControlGroupItem : LayoutControlItemBase
  {
    public static readonly RadProperty HeaderHeightProperty = RadProperty.Register(nameof (HeaderHeight), typeof (int), typeof (LayoutControlGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (LayoutControlGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.Cancelable));
    private LayoutControlContainerElement containerElement;
    private CollapsiblePanelHeaderElement headerElement;
    internal bool isAttaching;

    public event EventHandler Expanded;

    public event EventHandler Collapsed;

    public event CancelEventHandler Expanding;

    public event CancelEventHandler Collapsing;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchHorizontally = this.StretchVertically = true;
      this.Padding = new Padding(4);
      this.headerElement = new CollapsiblePanelHeaderElement();
      int num1 = (int) this.headerElement.HeaderTextElement.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.headerElement.BindProperty(CollapsiblePanelHeaderElement.IsExpandedProperty, (RadObject) this, LayoutControlGroupItem.IsExpandedProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.headerElement.HeaderButtonElement.BindProperty(CollapsiblePanelButtonElement.IsExpandedProperty, (RadObject) this, LayoutControlGroupItem.IsExpandedProperty, PropertyBindingOptions.TwoWay);
      this.containerElement = this.CreateContainerElement();
      this.containerElement.Class = "LayoutControlGroupContainer";
      this.containerElement.DrawBorder = false;
      this.Children.Add((RadElement) this.headerElement);
      this.Children.Add((RadElement) this.containerElement);
      this.headerElement.HeaderButtonElement.MouseUp += new MouseEventHandler(this.headerElement_MouseUp);
    }

    protected virtual LayoutControlContainerElement CreateContainerElement()
    {
      return new LayoutControlContainerElement();
    }

    [Browsable(false)]
    public override ContentAlignment TextAlignment
    {
      get
      {
        return base.TextAlignment;
      }
      set
      {
        base.TextAlignment = value;
      }
    }

    [Browsable(false)]
    public override bool TextWrap
    {
      get
      {
        return base.TextWrap;
      }
      set
      {
        base.TextWrap = value;
      }
    }

    [Browsable(false)]
    public override bool ShowHorizontalLine
    {
      get
      {
        return base.ShowHorizontalLine;
      }
      set
      {
        base.ShowHorizontalLine = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the line in the header element should be shown.")]
    [DefaultValue(true)]
    public bool ShowHeaderLine
    {
      get
      {
        return this.HeaderElement.ShowHeaderLine;
      }
      set
      {
        this.HeaderElement.ShowHeaderLine = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CollapsiblePanelHeaderElement HeaderElement
    {
      get
      {
        return this.headerElement;
      }
    }

    [Description("Gets or sets a value indicating whether the group is currently expanded.")]
    [RadPropertyDefaultValue("IsExpanded", typeof (LayoutControlGroupItem))]
    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(LayoutControlGroupItem.IsExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlGroupItem.IsExpandedProperty, (object) value);
      }
    }

    [Description("Gets or sets the height of the header.")]
    [RadPropertyDefaultValue("HeaderHeight", typeof (LayoutControlGroupItem))]
    [VsbBrowsable(true)]
    public int HeaderHeight
    {
      get
      {
        return (int) this.GetValue(LayoutControlGroupItem.HeaderHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlGroupItem.HeaderHeightProperty, (object) value);
      }
    }

    public LayoutControlContainerElement ContainerElement
    {
      get
      {
        return this.containerElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MinSize
    {
      get
      {
        if (this.containerElement.LayoutTree.Root == null)
          return base.MinSize;
        if (this.IsExpanded)
          return new Size((int) this.containerElement.LayoutTree.Root.MinSize.Width + this.Padding.Horizontal, (int) this.containerElement.LayoutTree.Root.MinSize.Height + this.HeaderHeight + this.Padding.Vertical);
        return new Size((int) this.containerElement.LayoutTree.Root.MinSize.Width + this.Padding.Horizontal, this.HeaderHeight + this.Padding.Vertical);
      }
      set
      {
        base.MinSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MaxSize
    {
      get
      {
        if (this.containerElement.LayoutTree.Root == null)
          return base.MaxSize;
        if (this.IsExpanded)
          return new Size((double) this.containerElement.LayoutTree.Root.MaxSize.Width != 0.0 ? (int) this.containerElement.LayoutTree.Root.MaxSize.Width + this.Padding.Horizontal : 0, (double) this.containerElement.LayoutTree.Root.MaxSize.Height != 0.0 ? (int) this.containerElement.LayoutTree.Root.MaxSize.Height + this.HeaderHeight + this.Padding.Vertical : 0);
        return new Size((double) this.containerElement.LayoutTree.Root.MaxSize.Width != 0.0 ? (int) this.containerElement.LayoutTree.Root.MaxSize.Width + this.Padding.Horizontal : 0, this.HeaderHeight + this.Padding.Vertical);
      }
      set
      {
        base.MaxSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemCollection Items
    {
      get
      {
        return (RadItemCollection) this.containerElement.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        if (this.Parent is RadPageViewContentAreaElement)
          return true;
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    public override bool DrawBorder
    {
      get
      {
        if (this.Parent is RadPageViewContentAreaElement)
          return (bool) this.GetValue(LightVisualElement.DrawBorderProperty);
        return base.DrawBorder;
      }
      set
      {
        if (this.Parent is RadPageViewContentAreaElement)
        {
          int num = (int) this.SetValue(LightVisualElement.DrawBorderProperty, (object) value);
        }
        else
          base.DrawBorder = value;
      }
    }

    private void headerElement_MouseUp(object sender, MouseEventArgs e)
    {
      LayoutControlGroupItem controlGroupItem = this;
      controlGroupItem.IsExpanded = !controlGroupItem.IsExpanded;
    }

    protected virtual void UpdateOnExpandCollapse()
    {
      bool flag = this.CheckIsHidden() || this.IsHidden;
      foreach (LayoutControlItemBase layoutControlItemBase in this.Items)
        layoutControlItemBase.IsHidden = flag || !this.IsExpanded;
      this.FindAncestor<LayoutControlContainerElement>()?.LayoutTree.UpdateItemsBounds();
      this.containerElement.Visibility = !this.IsExpanded || this.Visibility != ElementVisibility.Visible ? ElementVisibility.Collapsed : ElementVisibility.Visible;
      this.SetBounds(this.Bounds);
      if (this.ElementTree == null)
        return;
      this.ElementTree.RootElement.InvalidateMeasure(true);
      this.ElementTree.RootElement.UpdateLayout();
      (this.ElementTree.Control as RadLayoutControl)?.UpdateScrollbars();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "IsHidden"))
        return;
      if (this.ElementState == ElementState.Loaded && !this.IsHidden && (this.ContainerElement.LayoutTree.Root == null && this.ContainerElement.Items.Count != 0))
        this.containerElement.RebuildLayoutTree();
      foreach (LayoutControlItemBase layoutControlItemBase in this.Items)
        layoutControlItemBase.IsHidden = this.IsHidden;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size = this.GetClientRectangle(availableSize).Size;
      if (this.headerElement.Visibility != ElementVisibility.Collapsed)
      {
        this.headerElement.Measure(new SizeF(availableSize.Width, (float) this.HeaderHeight));
        this.containerElement.Measure(new SizeF(size.Width, size.Height - (float) this.HeaderHeight));
      }
      else
        this.containerElement.Measure(new SizeF(size.Width, size.Height));
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (!this.AutoSize)
        this.MeasureOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(new SizeF(finalSize.Width, finalSize.Height));
      if (this.headerElement.Visibility != ElementVisibility.Collapsed)
      {
        this.headerElement.Arrange(new RectangleF((PointF) Point.Empty, new SizeF(finalSize.Width, (float) this.HeaderHeight)));
        clientRectangle.Height -= (float) this.HeaderHeight;
        clientRectangle.Offset(0.0f, (float) this.HeaderHeight);
        this.containerElement.Arrange(clientRectangle);
      }
      else
        this.containerElement.Arrange(clientRectangle);
      if (this.AutoSize)
        this.containerElement.PerformControlLayout(false);
      return finalSize;
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs e)
    {
      base.OnPropertyChanging(e);
      if (e.Property != LayoutControlGroupItem.IsExpandedProperty)
        return;
      CancelEventArgs e1 = new CancelEventArgs(false);
      if (e.NewValue is bool)
      {
        if ((bool) e.NewValue && this.Expanding != null)
          this.Expanding((object) this, e1);
        if (!(bool) e.NewValue && this.Collapsing != null)
          this.Collapsing((object) this, e1);
      }
      e.Cancel = e1.Cancel;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != LayoutControlGroupItem.IsExpandedProperty)
        return;
      this.UpdateOnExpandCollapse();
      if (!(e.NewValue is bool))
        return;
      if ((bool) e.NewValue && this.Expanded != null)
        this.Expanded((object) this, EventArgs.Empty);
      if ((bool) e.NewValue || this.Collapsed == null)
        return;
      this.Collapsed((object) this, EventArgs.Empty);
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent is RadPageViewContentAreaElement)
      {
        this.headerElement.Visibility = ElementVisibility.Collapsed;
        int num = (int) this.SetDefaultValueOverride(RadElement.PaddingProperty, (object) Padding.Empty);
      }
      else
      {
        this.headerElement.Visibility = ElementVisibility.Visible;
        int num = (int) this.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(4));
      }
    }
  }
}
