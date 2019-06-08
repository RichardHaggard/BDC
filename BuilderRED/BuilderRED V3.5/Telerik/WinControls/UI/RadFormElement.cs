// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadFormElement : RadItem
  {
    public static RadProperty IsFormActiveProperty = RadProperty.Register(nameof (IsFormActive), typeof (bool), typeof (RadFormElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FormWindowStateProperty = RadProperty.Register("FormWindowState", typeof (FormWindowState), typeof (RadFormElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) FormWindowState.Normal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private DockLayoutPanel dockLayoutPanel;
    private DockLayoutPanel scrollBarsDockLayout;
    private FormBorderPrimitive borderPrimitive;
    private RadFormTitleBarElement radTitleBarElement;
    private RadFormMdiControlStripItem mdiControlStripItem;
    private FormImageBorderPrimitive imageBorder;
    private RadScrollBarElement horizontalScrollbar;
    private RadScrollBarElement verticalScrollbar;
    private DockLayoutPanel horizontalScrollBarAndSquareHolder;
    private DockLayoutPanel horizontalScrollbarDockPanel;
    private RadItem formSizingGrip;
    private FillPrimitive formSizingGripFill;
    private BorderPrimitive formSizingGripBorder;
    private StackLayoutPanel imageBorderAndScrollbarsDockLayout;

    static RadFormElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadFormElementStateManager(), typeof (RadFormElement));
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsFormActive
    {
      get
      {
        return (bool) this.GetValue(RadFormElement.IsFormActiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadFormElement.IsFormActiveProperty, (object) value);
      }
    }

    public RadItem ScrollBarsFormSizingGrip
    {
      get
      {
        return this.formSizingGrip;
      }
    }

    public Padding BorderWidth
    {
      get
      {
        return Padding.Add(this.imageBorder.BorderWidth, new Padding((int) Math.Round((double) TelerikDpiHelper.ScaleFloat(this.borderPrimitive.Width, this.DpiScaleFactor), MidpointRounding.AwayFromZero)));
      }
    }

    public RadFormMdiControlStripItem MdiControlStrip
    {
      get
      {
        return this.mdiControlStripItem;
      }
    }

    public FormBorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public FormImageBorderPrimitive ImageBorder
    {
      get
      {
        return this.imageBorder;
      }
    }

    public RadFormTitleBarElement TitleBar
    {
      get
      {
        return this.radTitleBarElement;
      }
    }

    public RadScrollBarElement HorizontalScrollbar
    {
      get
      {
        return this.horizontalScrollbar;
      }
    }

    public RadScrollBarElement VerticalScrollbar
    {
      get
      {
        return this.verticalScrollbar;
      }
    }

    internal void SetIsFormActiveInternal(bool isActive)
    {
      int num = (int) this.SetDefaultValueOverride(RadFormElement.IsFormActiveProperty, (object) isActive);
    }

    internal void SetFormElementIconInternal(Image icon)
    {
      int num = (int) this.TitleBar.IconPrimitive.SetDefaultValueOverride(ImagePrimitive.ImageProperty, (object) icon);
      this.TitleBar.UpdateLayout();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.imageBorderAndScrollbarsDockLayout = new StackLayoutPanel();
      this.dockLayoutPanel = new DockLayoutPanel();
      this.scrollBarsDockLayout = new DockLayoutPanel();
      this.imageBorder = new FormImageBorderPrimitive();
      this.imageBorder.Class = "RadFormImageBorder";
      this.borderPrimitive = new FormBorderPrimitive();
      this.borderPrimitive.Class = "RadFormBorder";
      this.borderPrimitive.StretchVertically = true;
      this.borderPrimitive.StretchHorizontally = true;
      this.radTitleBarElement = this.CreateTitleBarElement();
      this.radTitleBarElement.Class = "TitleBar";
      this.radTitleBarElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.mdiControlStripItem = new RadFormMdiControlStripItem();
      int num = (int) this.mdiControlStripItem.Fill.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      this.mdiControlStripItem.Visibility = ElementVisibility.Collapsed;
      this.mdiControlStripItem.MinSize = new Size(0, 18);
      this.mdiControlStripItem.MaxSize = new Size(0, 18);
      this.mdiControlStripItem.Class = "MdiControlStrip";
      this.dockLayoutPanel.Children.Add((RadElement) this.radTitleBarElement);
      this.dockLayoutPanel.Children.Add((RadElement) this.mdiControlStripItem);
      this.dockLayoutPanel.Children.Add((RadElement) this.imageBorderAndScrollbarsDockLayout);
      this.Children.Add((RadElement) this.imageBorder);
      this.Children.Add((RadElement) this.scrollBarsDockLayout);
      this.horizontalScrollbar = new RadScrollBarElement();
      this.horizontalScrollbar.ScrollType = ScrollType.Horizontal;
      this.horizontalScrollbar.Class = "RadFormHScroll";
      this.horizontalScrollbar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.verticalScrollbar = new RadScrollBarElement();
      this.verticalScrollbar.ScrollType = ScrollType.Vertical;
      this.verticalScrollbar.Class = "RadFormVScroll";
      this.verticalScrollbar.MinSize = new Size(RadScrollBarElement.VerticalScrollBarWidth, 0);
      this.horizontalScrollBarAndSquareHolder = new DockLayoutPanel();
      this.formSizingGrip = new RadItem();
      this.formSizingGripFill = new FillPrimitive();
      this.formSizingGripFill.Class = "RadFormScrollSquareFill";
      this.formSizingGripBorder = new BorderPrimitive();
      this.formSizingGripBorder.Class = "RadFormScrollSquareBorder";
      this.formSizingGrip.Class = "RadFormElementScrollbarSquare";
      this.formSizingGrip.Children.Add((RadElement) this.formSizingGripFill);
      this.formSizingGrip.Children.Add((RadElement) this.formSizingGripBorder);
      this.formSizingGrip.MinSize = new Size(RadScrollBarElement.VerticalScrollBarWidth, RadScrollBarElement.HorizontalScrollBarHeight);
      this.horizontalScrollbarDockPanel = new DockLayoutPanel();
      this.horizontalScrollbarDockPanel.Children.Add((RadElement) this.horizontalScrollbar);
      DockLayoutPanel.SetDock((RadElement) this.horizontalScrollbar, Dock.Bottom);
      this.horizontalScrollBarAndSquareHolder.Children.Add((RadElement) this.formSizingGrip);
      this.horizontalScrollBarAndSquareHolder.Children.Add((RadElement) this.horizontalScrollbarDockPanel);
      this.horizontalScrollBarAndSquareHolder.LastChildFill = true;
      DockLayoutPanel.SetDock((RadElement) this.horizontalScrollbarDockPanel, Dock.Right);
      DockLayoutPanel.SetDock((RadElement) this.formSizingGrip, Dock.Right);
      DockLayoutPanel.SetDock((RadElement) this.radTitleBarElement, Dock.Top);
      DockLayoutPanel.SetDock((RadElement) this.mdiControlStripItem, Dock.Top);
      this.scrollBarsDockLayout.Children.Add((RadElement) this.horizontalScrollBarAndSquareHolder);
      this.scrollBarsDockLayout.Children.Add((RadElement) this.verticalScrollbar);
      this.scrollBarsDockLayout.LastChildFill = false;
      DockLayoutPanel.SetDock((RadElement) this.horizontalScrollBarAndSquareHolder, Dock.Bottom);
      DockLayoutPanel.SetDock((RadElement) this.verticalScrollbar, Dock.Right);
      this.Children.Add((RadElement) this.dockLayoutPanel);
      this.Children.Add((RadElement) this.borderPrimitive);
    }

    protected virtual RadFormTitleBarElement CreateTitleBarElement()
    {
      return new RadFormTitleBarElement();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize.Width -= (float) this.BorderWidth.Horizontal;
      availableSize.Width -= (float) this.BorderThickness.Horizontal;
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      float left = (float) this.borderPrimitive.BorderThickness.Left;
      float y = (float) (this.radTitleBarElement.ControlBoundingRectangle.Height + this.borderPrimitive.BorderThickness.Top);
      float width = finalSize.Width - (float) this.borderPrimitive.BorderThickness.Horizontal;
      float height = finalSize.Height - (this.radTitleBarElement.DesiredSize.Height + (float) this.borderPrimitive.BorderThickness.Vertical);
      this.imageBorder.Arrange(new RectangleF(left, y, width, height));
      RectangleF finalRect = new RectangleF((float) (this.imageBorder.BorderThickness.Left + this.borderPrimitive.BorderThickness.Left), y, finalSize.Width - (float) (this.imageBorder.BorderThickness.Horizontal + this.borderPrimitive.BorderThickness.Horizontal), finalSize.Height - ((float) (this.borderPrimitive.BorderThickness.Vertical + this.imageBorder.BorderThickness.Bottom) + this.radTitleBarElement.DesiredSize.Height));
      this.scrollBarsDockLayout.Arrange(finalRect);
      if (this.borderPrimitive.Visibility == ElementVisibility.Collapsed && this.imageBorder.Visibility == ElementVisibility.Collapsed)
      {
        this.borderPrimitive.Arrange((RectangleF) Rectangle.Empty);
        this.imageBorder.Arrange((RectangleF) Rectangle.Empty);
        finalRect = new RectangleF(0.0f, this.radTitleBarElement.DesiredSize.Height, finalSize.Width, finalSize.Height - this.radTitleBarElement.DesiredSize.Height);
        this.scrollBarsDockLayout.Arrange(finalRect);
      }
      return sizeF;
    }
  }
}
