// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SplitterElement : RadItem
  {
    private int thumbLength = 50;
    public static RadProperty DockProperty = RadProperty.Register(nameof (Dock), typeof (DockStyle), typeof (SplitterElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DockStyle.Left, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty SplitterWidthProperty = RadProperty.Register(nameof (SplitterWidth), typeof (int), typeof (SplitterElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (SplitterElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty SplitterAlignmentProperty = RadProperty.Register(nameof (SplitterAlignment), typeof (RadDirection), typeof (SplitterElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Left, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCollapsedProperty = RadProperty.Register(nameof (IsCollapsed), typeof (bool), typeof (SplitterElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty HorizontalImageProperty = RadProperty.Register(nameof (HorizontalImage), typeof (RadImageShape), typeof (SplitterElement), new RadPropertyMetadata((PropertyChangedCallback) null));
    public static RadProperty VerticalImageProperty = RadProperty.Register(nameof (VerticalImage), typeof (RadImageShape), typeof (SplitterElement), new RadPropertyMetadata((PropertyChangedCallback) null));
    private const int DefaultThumbLength = 50;
    private RadButtonElement prevSplitterButton;
    private ArrowPrimitive prevSplitterArrow;
    private FillPrimitive backgroundFill;
    private BorderPrimitive border;
    private SplitterElementLayout layout;
    private RadButtonElement nextSplitterButton;
    private ArrowPrimitive nextSplitterArrow;
    private object left;
    private object right;
    private bool fixedSplitter;
    private int lastSplitterPosition;

    static SplitterElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new SplitterElementStateManager(), typeof (SplitterElement));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.backgroundFill = new FillPrimitive();
      this.backgroundFill.Class = "SplitterFill";
      this.backgroundFill.GradientStyle = GradientStyles.Solid;
      this.backgroundFill.SmoothingMode = SmoothingMode.Default;
      this.Children.Add((RadElement) this.backgroundFill);
      this.layout = new SplitterElementLayout(this);
      this.Children.Add((RadElement) this.layout);
      this.prevSplitterButton = new RadButtonElement();
      this.prevSplitterButton.Class = "PrevDownSplitterThumb";
      this.prevSplitterButton.Name = "prevSplitterButton";
      this.layout.Children.Add((RadElement) this.prevSplitterButton);
      this.prevSplitterArrow = new ArrowPrimitive();
      this.prevSplitterArrow.MinSize = new Size(4, 4);
      this.prevSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Left;
      this.prevSplitterArrow.Alignment = ContentAlignment.MiddleCenter;
      this.prevSplitterArrow.Class = "PrevDownSplitterThumbArrow";
      this.prevSplitterButton.Children.Add((RadElement) this.prevSplitterArrow);
      this.nextSplitterButton = new RadButtonElement();
      this.nextSplitterButton.Class = "NextUpSplitterThumb";
      this.nextSplitterButton.Name = "nextSplitterButton";
      this.layout.Children.Add((RadElement) this.nextSplitterButton);
      this.nextSplitterArrow = new ArrowPrimitive();
      this.nextSplitterArrow.MinSize = new Size(4, 4);
      this.nextSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Right;
      this.nextSplitterArrow.Alignment = ContentAlignment.MiddleCenter;
      this.nextSplitterArrow.Class = "NextUpSSplitterThumbArrow";
      this.nextSplitterButton.Children.Add((RadElement) this.nextSplitterArrow);
      this.border = new BorderPrimitive();
      this.border.Class = "SplitterBorder";
      this.border.GradientStyle = GradientStyles.Solid;
      this.border.SmoothingMode = SmoothingMode.Default;
      this.Children.Add((RadElement) this.border);
    }

    [RadPropertyDefaultValue("Dock", typeof (SplitterElement))]
    [RadDescription("Dock", typeof (DockStyle))]
    [Category("Behavior")]
    public DockStyle Dock
    {
      get
      {
        return (DockStyle) this.GetValue(SplitterElement.DockProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.DockProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [RadDescription("SplitterWidth", typeof (int))]
    [RadPropertyDefaultValue("SplitterWidth", typeof (SplitterElement))]
    public int SplitterWidth
    {
      get
      {
        return (int) this.GetValue(SplitterElement.SplitterWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.SplitterWidthProperty, (object) value);
      }
    }

    public bool Fixed
    {
      get
      {
        return this.fixedSplitter;
      }
      set
      {
        this.fixedSplitter = value;
        this.UpdateButtonsLayout();
      }
    }

    public object RightNode
    {
      get
      {
        return this.right;
      }
      set
      {
        this.right = value;
      }
    }

    public object LeftNode
    {
      get
      {
        return this.left;
      }
      set
      {
        this.left = value;
      }
    }

    public RadItem NextNavigationButton
    {
      get
      {
        return (RadItem) this.nextSplitterButton;
      }
    }

    public RadItem PrevNavigationButton
    {
      get
      {
        return (RadItem) this.prevSplitterButton;
      }
    }

    public ArrowPrimitive PrevArrow
    {
      get
      {
        return this.prevSplitterArrow;
      }
    }

    public ArrowPrimitive NextArrow
    {
      get
      {
        return this.nextSplitterArrow;
      }
    }

    [Browsable(false)]
    public FillPrimitive BackgroundFill
    {
      get
      {
        return this.backgroundFill;
      }
    }

    [Browsable(false)]
    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    [Browsable(false)]
    public SplitterElementLayout Layout
    {
      get
      {
        return this.layout;
      }
    }

    public int ThumbLength
    {
      get
      {
        return this.thumbLength;
      }
      set
      {
        this.thumbLength = value;
      }
    }

    public int LastSplitterPosition
    {
      get
      {
        return this.lastSplitterPosition;
      }
      set
      {
        this.lastSplitterPosition = value;
      }
    }

    [Browsable(false)]
    [RadDescription("IsCollapsed", typeof (bool))]
    [RadPropertyDefaultValue("IsCollapsed", typeof (SplitterElement))]
    public bool IsCollapsed
    {
      get
      {
        return (bool) this.GetValue(SplitterElement.IsCollapsedProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.IsCollapsedProperty, (object) value);
      }
    }

    [Browsable(false)]
    [RadDescription("SplitterAlignment", typeof (RadDirection))]
    [RadPropertyDefaultValue("SplitterAlignment", typeof (SplitterElement))]
    public RadDirection SplitterAlignment
    {
      get
      {
        return (RadDirection) this.GetValue(SplitterElement.SplitterAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.SplitterAlignmentProperty, (object) value);
      }
    }

    public RadImageShape HorizontalImage
    {
      get
      {
        return this.GetValue(SplitterElement.HorizontalImageProperty) as RadImageShape;
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.HorizontalImageProperty, (object) value);
      }
    }

    public RadImageShape VerticalImage
    {
      get
      {
        return this.GetValue(SplitterElement.VerticalImageProperty) as RadImageShape;
      }
      set
      {
        int num = (int) this.SetValue(SplitterElement.VerticalImageProperty, (object) value);
      }
    }

    protected virtual void UpdateButtonsLayout()
    {
      if (!this.Fixed)
        this.layout.Visibility = ElementVisibility.Visible;
      else
        this.layout.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == SplitterElement.DockProperty)
      {
        if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
        {
          this.prevSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Left;
          this.nextSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Right;
        }
        else
        {
          this.prevSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Down;
          this.nextSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Up;
        }
      }
      if (e.Property == SplitterElement.IsVerticalProperty)
      {
        SplitterElementLayout descendant = this.FindDescendant<SplitterElementLayout>();
        if (descendant != null)
        {
          int num = (int) descendant.SetValue(SplitterElementLayout.IsVerticalProperty, e.NewValue);
          if ((bool) e.NewValue)
          {
            this.prevSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Left;
            this.nextSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Right;
            descendant.Orientation = Orientation.Vertical;
            if (!descendant.UseSplitterButtons)
              descendant.BackgroundShape = this.GetValue(SplitterElement.VerticalImageProperty) as RadImageShape;
          }
          else
          {
            this.prevSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Down;
            this.nextSplitterArrow.Direction = Telerik.WinControls.ArrowDirection.Up;
            descendant.Orientation = Orientation.Horizontal;
            if (!descendant.UseSplitterButtons)
              descendant.BackgroundShape = this.GetValue(SplitterElement.HorizontalImageProperty) as RadImageShape;
          }
        }
      }
      if (e.Property != RadItem.VisualStateProperty && e.Property != RadElement.StyleProperty)
        return;
      SplitterElementLayout child = this.Children[1] as SplitterElementLayout;
      if (child == null || child.UseSplitterButtons)
        return;
      if ((bool) this.GetValue(SplitterElement.IsVerticalProperty))
        child.BackgroundShape = this.GetValue(SplitterElement.VerticalImageProperty) as RadImageShape;
      else
        child.BackgroundShape = this.GetValue(SplitterElement.HorizontalImageProperty) as RadImageShape;
    }
  }
}
