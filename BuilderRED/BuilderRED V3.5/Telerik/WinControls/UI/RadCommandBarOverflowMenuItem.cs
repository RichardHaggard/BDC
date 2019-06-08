// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarOverflowMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarOverflowMenuItem : RadMenuItemBase
  {
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private LightVisualElement menuElement;
    private RadMenuCheckmark menuCheckMark;
    private RadCommandBarBaseItem representedItem;
    private RadDropDownMenu ownerMenu;

    public RadCommandBarOverflowMenuItem(
      RadCommandBarBaseItem representedItem,
      RadDropDownMenu ownerMenu)
    {
      this.representedItem = representedItem;
      this.ownerMenu = ownerMenu;
      if (!string.IsNullOrEmpty(representedItem.DisplayName))
        this.menuElement.Text = this.CheckForLongText(representedItem.DisplayName);
      else
        this.menuElement.Text = this.CheckForLongText(representedItem.Name);
      PropertyInfo property = this.representedItem.GetType().GetProperty(nameof (Image));
      if ((object) property != null)
      {
        this.menuCheckMark.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
        this.Image = (Image) property.GetValue((object) this.representedItem, (object[]) null);
      }
      if (this.menuElement.Image == null)
        this.menuElement.Image = (Image) new Bitmap(16, 16);
      else
        this.Image = this.Image.GetThumbnailImage(16, 16, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
      if (this.representedItem.VisibleInStrip)
        return;
      this.menuCheckMark.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public new Image Image
    {
      get
      {
        return this.menuElement.Image;
      }
      set
      {
        this.menuElement.Image = value;
      }
    }

    public new string Text
    {
      get
      {
        return this.menuElement.Text;
      }
      set
      {
        this.menuElement.Text = value;
      }
    }

    public bool Checked
    {
      get
      {
        return this.menuCheckMark.CheckState == Telerik.WinControls.Enumerations.ToggleState.On;
      }
      set
      {
        if (this.representedItem.VisibleInStrip == value)
          return;
        this.representedItem.VisibleInStrip = value;
        if (this.representedItem.VisibleInStrip)
          this.menuCheckMark.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
        else
          this.menuCheckMark.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.representedItem.Parent.InvalidateMeasure(true);
        this.representedItem.Parent.UpdateLayout();
        this.ownerMenu.PopupElement.InvalidateMeasure(true);
        this.ownerMenu.PopupElement.UpdateLayout();
      }
    }

    private string CheckForLongText(string text)
    {
      return text;
    }

    protected void menuElement_MouseEnter(object sender, EventArgs e)
    {
      this.Select();
    }

    protected void menuElement_MouseLeave(object sender, EventArgs e)
    {
      this.Deselect();
    }

    protected void menuElement_Click(object sender, EventArgs e)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "RadMenuComboItem";
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
      this.borderPrimitive.Visibility = ElementVisibility.Visible;
      this.borderPrimitive.Class = "RadMenuItemBorderPrimitive";
      this.borderPrimitive.Name = "MenuComboItemBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.menuElement = new LightVisualElement();
      this.menuElement.MinSize = new Size(100, 20);
      this.menuElement.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.menuElement.TextAlignment = ContentAlignment.MiddleLeft;
      this.menuElement.ImageAlignment = ContentAlignment.MiddleLeft;
      this.menuElement.ImageLayout = ImageLayout.Stretch;
      this.menuElement.MaxSize = new Size(0, 16);
      this.menuElement.MinSize = new Size(0, 16);
      this.menuElement.MouseEnter += new EventHandler(this.menuElement_MouseEnter);
      this.menuElement.MouseLeave += new EventHandler(this.menuElement_MouseLeave);
      this.menuElement.Click += new EventHandler(this.menuElement_Click);
      this.Children.Add((RadElement) this.menuElement);
      if (this.DesignMode)
        this.menuElement.RoutedEventBehaviors.Add((RoutedEventBehavior) new RadCommandBarOverflowMenuItem.CancelMouseBehavior());
      this.menuCheckMark = new RadMenuCheckmark();
      this.menuCheckMark.MinSize = new Size(13, 13);
      this.menuCheckMark.Alignment = ContentAlignment.MiddleCenter;
      this.menuCheckMark.CheckElement.Alignment = ContentAlignment.MiddleCenter;
      this.menuCheckMark.Class = "RadMenuItemCheckPrimitive";
      this.menuCheckMark.NotifyParentOnMouseInput = false;
      this.Children.Add((RadElement) this.menuCheckMark);
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
        if (object.ReferenceEquals((object) child, (object) this.menuElement))
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
      float width = 0.0f;
      float num = 0.0f;
      if (parent != null)
      {
        width = parent.LeftColumnMaxPadding + parent.LeftColumnWidth;
        num = parent.LeftColumnMaxPadding + parent.RightColumnWidth;
      }
      foreach (RadElement child in this.Children)
      {
        if (child == this.menuElement)
        {
          if (this.RightToLeft)
            child.Arrange(new RectangleF((float) ((double) clientRectangle.Right - (double) width - (double) clientRectangle.Width + ((double) width + (double) num)), clientRectangle.Top, clientRectangle.Width - (width + num), child.DesiredSize.Height));
          else
            child.Arrange(new RectangleF(clientRectangle.Left + width, clientRectangle.Top, clientRectangle.Width - (width + num), child.DesiredSize.Height));
        }
        else if (child == this.menuCheckMark)
        {
          RectangleF finalRect = new RectangleF(clientRectangle.X, clientRectangle.Y, width, clientRectangle.Height);
          if (this.RightToLeft)
            finalRect.X = clientRectangle.Width - finalRect.Width;
          child.Arrange(finalRect);
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
        return new bool?(this.Class != "RadMenuComboItem");
      return base.ShouldSerializeProperty(property);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.Checked = !this.Checked;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.Select();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.Deselect();
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
