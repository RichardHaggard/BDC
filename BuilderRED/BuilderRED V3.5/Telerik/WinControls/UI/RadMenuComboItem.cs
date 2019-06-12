// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuComboItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuComboItem : RadMenuItemBase
  {
    private ImagePrimitive imagePrimitive;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private RadDropDownListElement comboBoxElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuComboItem);
    }

    [Category("Behavior")]
    [Description("Provides a reference to the ComboBox element in the menu item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public RadDropDownListElement ComboBoxElement
    {
      get
      {
        return this.comboBoxElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets a collection representing the items contained in this ComboBox.")]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [Browsable(false)]
    public RadListDataItemCollection Items
    {
      get
      {
        return this.comboBoxElement.Items;
      }
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadMenuItemFillPrimitive";
      this.fillPrimitive.Name = "MenuComboItemFill";
      this.fillPrimitive.BackColor = Color.Empty;
      this.fillPrimitive.GradientStyle = GradientStyles.Solid;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Visibility = ElementVisibility.Collapsed;
      this.borderPrimitive.Class = "RadMenuComboItemBorderPrimitive";
      this.borderPrimitive.Name = "MenuComboItemBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.imagePrimitive = new ImagePrimitive();
      int num1 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num2 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.imagePrimitive.Class = "RadMenuComboItemImagePrimitive";
      this.Children.Add((RadElement) this.imagePrimitive);
      this.comboBoxElement = new RadDropDownListElement();
      this.comboBoxElement.MinSize = new Size(100, 20);
      this.comboBoxElement.BindingContext = new BindingContext();
      this.Children.Add((RadElement) this.comboBoxElement);
      if (!this.DesignMode)
        return;
      this.comboBoxElement.ArrowButton.RoutedEventBehaviors.Add((RoutedEventBehavior) new RadMenuComboItem.CancelMouseBehavior());
    }

    protected override void OnSelect()
    {
      base.OnSelect();
      this.comboBoxElement.EditableElement.Focus();
      PopupManager.Default.LastActivatedPopup = (IPopupControl) this.comboBoxElement.PopupForm;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      float num1 = 0.0f;
      float num2 = 0.0f;
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      if (parent != null)
      {
        num1 = parent.LeftColumnMaxPadding + parent.LeftColumnWidth;
        num2 = parent.LeftColumnMaxPadding + parent.RightColumnWidth;
      }
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        child.Measure(availableSize);
        empty.Height = Math.Max(child.DesiredSize.Height, empty.Height);
        if (object.ReferenceEquals((object) child, (object) this.comboBoxElement))
          empty.Width += child.DesiredSize.Width;
      }
      empty.Width += num1 + num2;
      empty.Width += (float) (this.Padding.Horizontal + this.BorderThickness.Horizontal);
      empty.Height += (float) (this.Padding.Vertical + this.BorderThickness.Vertical);
      return empty;
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
        if (child == this.imagePrimitive)
        {
          float width = parent != null ? parent.LeftColumnWidth : 0.0f;
          RectangleF rectangleF = new RectangleF(clientRectangle.Left, clientRectangle.Top, width, clientRectangle.Height);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
        else if (child == this.comboBoxElement)
        {
          RectangleF rectangleF = new RectangleF(clientRectangle.Left + num1, clientRectangle.Top, clientRectangle.Width - (num1 + num2), child.DesiredSize.Height);
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
        return new bool?(this.Class != nameof (RadMenuComboItem));
      return base.ShouldSerializeProperty(property);
    }

    private class CancelMouseBehavior : RoutedEventBehavior
    {
      public CancelMouseBehavior()
        : base(new RaisedRoutedEvent(RadElement.MouseDownEvent, string.Empty, EventBehaviorSenderType.AnySender, RoutingDirection.Tunnel))
      {
      }

      public override void OnEventOccured(
        RadElement sender,
        RadElement element,
        RoutedEventArgs args)
      {
        args.Canceled = true;
        base.OnEventOccured(sender, element, args);
      }
    }
  }
}
