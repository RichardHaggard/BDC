// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuButtonItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuButtonItem : RadMenuItemBase
  {
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private RadButtonElement buttonElement;

    public RadMenuButtonItem()
      : this("")
    {
    }

    public RadMenuButtonItem(string text)
    {
      this.Text = text;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuButtonItem);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Provides a reference to the ButtonElement in the menu item.")]
    public RadButtonElement ButtonElement
    {
      get
      {
        return this.buttonElement;
      }
    }

    [RelatedImageList("MenuElement.ElementTree.Control.ImageList")]
    [Category("Appearance")]
    [Description("Gets or sets the index value of the image that is displayed on the item.")]
    [Browsable(true)]
    public override int ImageIndex
    {
      get
      {
        return base.ImageIndex;
      }
      set
      {
        base.ImageIndex = value;
      }
    }

    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [Category("Appearance")]
    [RelatedImageList("MenuElement.ElementTree.Control.ImageList")]
    [Browsable(true)]
    public override string ImageKey
    {
      get
      {
        return base.ImageKey;
      }
      set
      {
        base.ImageKey = value;
      }
    }

    public override string ToolTipText
    {
      get
      {
        return this.ButtonElement.ToolTipText;
      }
      set
      {
        this.ButtonElement.ToolTipText = value;
      }
    }

    public override bool AutoToolTip
    {
      get
      {
        return this.ButtonElement.AutoToolTip;
      }
      set
      {
        if (this.ButtonElement == null)
          return;
        this.ButtonElement.AutoToolTip = value;
      }
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadMenuItemFillPrimitive";
      this.fillPrimitive.Name = "MenuButtonItemFill";
      this.fillPrimitive.BackColor = Color.Empty;
      this.fillPrimitive.GradientStyle = GradientStyles.Solid;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadMenuItemBorderPrimitive";
      this.borderPrimitive.Name = "MenuButtonItemBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.buttonElement = new RadButtonElement();
      this.buttonElement.CanFocus = false;
      this.buttonElement.Click += new EventHandler(this.buttonElement_Click);
      int num1 = (int) this.buttonElement.ImagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.buttonElement.ImagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.buttonElement.ImagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.buttonElement.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.Children.Add((RadElement) this.buttonElement);
      int num4 = (int) this.buttonElement.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.SetDefaultValueOverride(RadButtonItem.TextImageRelationProperty, (object) this.buttonElement.TextImageRelation);
      int num6 = (int) this.SetDefaultValueOverride(RadButtonItem.ImageAlignmentProperty, (object) this.buttonElement.ImageAlignment);
      int num7 = (int) this.SetDefaultValueOverride(RadButtonItem.TextAlignmentProperty, (object) this.buttonElement.TextAlignment);
      int num8 = (int) this.buttonElement.BindProperty(RadButtonItem.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.TwoWay);
      int num9 = (int) this.buttonElement.BindProperty(RadButtonItem.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.TwoWay);
      int num10 = (int) this.buttonElement.BindProperty(RadButtonItem.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.TwoWay);
    }

    protected override void DisposeManagedResources()
    {
      this.buttonElement.Click -= new EventHandler(this.buttonElement_Click);
      base.DisposeManagedResources();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = new SizeF(availableSize);
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      if (parent != null)
        availableSize1.Width = availableSize.Width - parent.LeftColumnMaxPadding - parent.LeftColumnWidth;
      this.buttonElement.Measure(availableSize1);
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (parent != null)
      {
        num1 = parent.LeftColumnMaxPadding + parent.LeftColumnWidth;
        num2 = parent.LeftColumnMaxPadding + parent.RightColumnWidth;
      }
      foreach (RadElement child in this.Children)
      {
        if (child == this.buttonElement)
        {
          RectangleF rectangleF = new RectangleF(clientRectangle.Left + num1, clientRectangle.Top, clientRectangle.Width - num1 - num2, child.DesiredSize.Height);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
        else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
          child.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
        else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
          child.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
        else
          child.Arrange(clientRectangle);
      }
      return finalSize;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "Class")
        return new bool?(this.Class != nameof (RadMenuButtonItem));
      return base.ShouldSerializeProperty(property);
    }

    protected override void OnClick(EventArgs e)
    {
      if (this.GetSite() != null && !(this.ElementTree.Control is RadMenu))
      {
        ISelectionService service = this.Site.GetService(typeof (ISelectionService)) as ISelectionService;
        if (service == null || service.GetComponentSelected((object) this))
          return;
        service.SetSelectedComponents((ICollection) new IComponent[1]
        {
          (IComponent) this
        });
      }
      else
        base.OnClick(e);
    }

    private void buttonElement_Click(object sender, EventArgs e)
    {
      this.OnClick(e);
      if (this.ElementTree == null || this.ElementTree.Control is RadMenu || !(this.ElementTree.Control is IPopupControl))
        return;
      (this.ElementTree.Control as IPopupControl).ClosePopup(RadPopupCloseReason.CloseCalled);
    }
  }
}
