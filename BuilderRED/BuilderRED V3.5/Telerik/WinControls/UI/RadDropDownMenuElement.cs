// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownMenuElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDropDownMenuElement : RadElement
  {
    public static readonly RadProperty DropDownPositionProperty = RadProperty.Register("DropDownPosition", typeof (DropDownPosition), typeof (RadDropDownMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DropDownPosition.Unknown, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    internal const long UseScrollingStateKey = 137438953472;
    private FillPrimitive fill;
    private FillPrimitive leftColumnFill;
    private BorderPrimitive leftColumnBorder;
    private BorderPrimitive border;
    private RadDropDownMenuLayout layoutPanel;
    private RadScrollViewer scrollViewer;
    private RadElement leftColumnElement;
    private ImageAndTextLayoutPanel headerColumnElement;
    private FillPrimitive headerColumnFill;
    private BorderPrimitive headerColumnBorder;
    private TextPrimitive headerColumnText;
    private ImagePrimitive headerColumnImage;
    private RadElement headerElement;

    protected override void InitializeFields()
    {
      this.SetBitState(137438953472L, true);
      base.InitializeFields();
      this.Class = "DropDownMenuElement";
    }

    public RadElement LayoutPanel
    {
      get
      {
        return (RadElement) this.layoutPanel;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    public RadDropDownMenuLayout Layout
    {
      get
      {
        return this.layoutPanel;
      }
    }

    public RadElement LeftColumnElement
    {
      get
      {
        return this.leftColumnElement;
      }
    }

    public FillPrimitive LeftColumnFill
    {
      get
      {
        return this.leftColumnFill;
      }
    }

    public BorderPrimitive LeftColumnBorder
    {
      get
      {
        return this.leftColumnBorder;
      }
    }

    public ImageAndTextLayoutPanel HeaderColumn
    {
      get
      {
        return this.headerColumnElement;
      }
    }

    public FillPrimitive HeaderColumnFill
    {
      get
      {
        return this.headerColumnFill;
      }
    }

    public BorderPrimitive HeaderColumnBorder
    {
      get
      {
        return this.headerColumnBorder;
      }
    }

    public TextPrimitive HeaderColumnText
    {
      get
      {
        return this.headerColumnText;
      }
    }

    public ImagePrimitive HeaderColumnImage
    {
      get
      {
        return this.headerColumnImage;
      }
    }

    public string HeaderText
    {
      get
      {
        return this.headerColumnText.Text;
      }
      set
      {
        this.headerColumnText.Text = value;
        this.SetHeaderVisibility();
      }
    }

    public Image HeaderImage
    {
      get
      {
        return this.headerColumnImage.Image;
      }
      set
      {
        this.headerColumnImage.Image = value;
        this.SetHeaderVisibility();
      }
    }

    public RadScrollViewer ScrollPanel
    {
      get
      {
        return this.scrollViewer;
      }
    }

    public override bool CanHaveOwnStyle
    {
      get
      {
        return true;
      }
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "RadSubMenuPanelBackFillPrimitive";
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.border.Class = "RadSubMenuPanelBorderPrimitive";
      this.Children.Add((RadElement) this.border);
      this.leftColumnElement = new RadElement();
      this.leftColumnElement.Class = "RadSubMenuPanelLeftElement";
      this.Children.Add(this.leftColumnElement);
      this.leftColumnFill = new FillPrimitive();
      this.leftColumnFill.Class = "RadSubMenuPanelFillPrimitive";
      this.leftColumnElement.Children.Add((RadElement) this.leftColumnFill);
      this.leftColumnBorder = new BorderPrimitive();
      this.leftColumnBorder.Class = "RadSubMenuPanelLeftBorderPrimitive";
      this.leftColumnElement.Children.Add((RadElement) this.leftColumnBorder);
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Horizontal;
      stackLayoutPanel.ZIndex = 1;
      this.Children.Add((RadElement) stackLayoutPanel);
      stackLayoutPanel.Children.Add(this.CreateHeaderColumnElement());
      this.layoutPanel = this.CreateMenuLayout();
      if (this.GetBitState(137438953472L))
      {
        this.scrollViewer = new RadScrollViewer();
        this.scrollViewer.ShowBorder = false;
        this.scrollViewer.ShowFill = false;
        this.scrollViewer.Viewport = (RadElement) this.layoutPanel;
        stackLayoutPanel.Children.Add((RadElement) this.scrollViewer);
      }
      else
        stackLayoutPanel.Children.Add((RadElement) this.layoutPanel);
    }

    protected virtual RadDropDownMenuLayout CreateMenuLayout()
    {
      return new RadDropDownMenuLayout();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      this.layoutPanel.Measure(availableSize);
      SizeF desiredSize = this.layoutPanel.DesiredSize;
      desiredSize.Width += this.headerElement.DesiredSize.Width;
      desiredSize.Width += (float) this.Padding.Horizontal;
      desiredSize.Height += (float) this.Padding.Vertical;
      desiredSize.Height += (float) this.BorderThickness.Vertical;
      desiredSize.Width += (float) this.BorderThickness.Horizontal;
      return this.ApplySizeConstraints(desiredSize);
    }

    protected virtual SizeF ApplySizeConstraints(SizeF desiredSize)
    {
      RadPopupControlBase control1 = this.ElementTree.Control as RadPopupControlBase;
      if (control1 == null)
        return desiredSize;
      RadDropDownMenu control2 = this.ElementTree.Control as RadDropDownMenu;
      if (control2 == null)
        return (SizeF) control1.ApplySizingConstraints(desiredSize.ToSize(), control1.GetCurrentScreen());
      RadMenuItem ownerElement = control2.OwnerElement as RadMenuItem;
      if (ownerElement == null)
        return (SizeF) control1.ApplySizingConstraints(desiredSize.ToSize(), control1.GetCurrentScreen());
      Control ownerControl = ownerElement.OwnerControl;
      return (SizeF) control1.ApplySizingConstraints(desiredSize.ToSize(), Screen.FromControl(ownerControl ?? (Control) control2));
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF finalRect1 = new RectangleF(this.headerElement.DesiredSize.Width + (float) this.BorderThickness.Left + (float) this.Padding.Left + (float) this.layoutPanel.BoundingRectangle.Left, (float) (this.BorderThickness.Top + this.Padding.Top), this.layoutPanel.LeftColumnWidth + this.layoutPanel.LeftColumnMaxPadding, finalSize.Height - (float) (this.BorderThickness.Vertical + this.Padding.Vertical));
      if (this.RightToLeft)
        finalRect1.X = finalSize.Width - (this.headerElement.DesiredSize.Width + (float) this.BorderThickness.Right + (float) this.Padding.Right + finalRect1.Width);
      this.leftColumnElement.Arrange(finalRect1);
      SizeF size = new SizeF(finalSize);
      size.Width -= (float) (this.BorderThickness.Horizontal + this.Padding.Horizontal) + this.headerElement.DesiredSize.Width;
      size.Height -= (float) (this.BorderThickness.Vertical + this.Padding.Vertical);
      RectangleF finalRect2 = new RectangleF(new PointF(this.headerElement.DesiredSize.Width, 0.0f), size);
      if (this.RightToLeft)
        finalRect2.X = finalSize.Width - (float) this.Padding.Horizontal - (float) this.BorderThickness.Vertical - finalRect2.X - finalRect2.Width;
      if (this.GetBitState(137438953472L) && this.scrollViewer != null)
        this.scrollViewer.Arrange(finalRect2);
      else
        this.layoutPanel.Arrange(finalRect2);
      return finalSize;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadDropDownMenuElement.DropDownPositionProperty)
        return;
      foreach (RadObject radObject in this.ChildrenHierarchy)
      {
        int num = (int) radObject.SetValue(RadDropDownMenuElement.DropDownPositionProperty, e.NewValue);
      }
    }

    private RadElement CreateHeaderColumnElement()
    {
      this.headerElement = new RadElement();
      this.headerElement.Visibility = ElementVisibility.Collapsed;
      this.headerColumnFill = new FillPrimitive();
      this.headerColumnFill.Class = "RadSubMenuPanelHeaderFill";
      this.headerElement.Children.Add((RadElement) this.headerColumnFill);
      this.headerColumnBorder = new BorderPrimitive();
      this.headerColumnBorder.Class = "RadSubMenuPanelHeaderBorder";
      this.headerElement.Children.Add((RadElement) this.headerColumnBorder);
      this.headerColumnElement = new ImageAndTextLayoutPanel();
      this.headerColumnElement.Class = "RadSubMenuPanelHeaderColumn";
      this.headerColumnElement.ZIndex = 1;
      this.headerColumnElement.AngleTransform = 270f;
      this.headerElement.Children.Add((RadElement) this.headerColumnElement);
      this.headerColumnText = new TextPrimitive();
      int num1 = (int) this.headerColumnText.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      this.headerColumnText.Class = "RadMenuItemTextPrimitive";
      this.headerColumnElement.Children.Add((RadElement) this.headerColumnText);
      this.headerColumnImage = new ImagePrimitive();
      int num2 = (int) this.headerColumnImage.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      this.headerColumnImage.Class = "RadMenuItemImagePrimitive";
      this.headerColumnElement.Children.Add((RadElement) this.headerColumnImage);
      return this.headerElement;
    }

    private void SetHeaderVisibility()
    {
      if (this.headerElement == null)
        return;
      if (!string.IsNullOrEmpty(this.headerColumnText.Text) || this.headerColumnImage.Image != null)
        this.headerElement.Visibility = ElementVisibility.Visible;
      else
        this.headerElement.Visibility = ElementVisibility.Hidden;
    }
  }
}
