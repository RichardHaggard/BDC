// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RootRadElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls
{
  public class RootRadElement : RadItem
  {
    public static readonly RoutedEvent OnRoutedImageListChanged = RadElement.RegisterRoutedEvent(nameof (OnRoutedImageListChanged), typeof (RootRadElement));
    public static readonly RoutedEvent AutoSizeChangedEvent = RadElement.RegisterRoutedEvent(nameof (AutoSizeChangedEvent), typeof (RootRadElement));
    public static readonly RoutedEvent StretchChangedEvent = RadElement.RegisterRoutedEvent(nameof (StretchChangedEvent), typeof (RootRadElement));
    public static RoutedEvent RootLayoutSuspendedEvent = RadElement.RegisterRoutedEvent(nameof (RootLayoutSuspendedEvent), typeof (RootRadElement));
    public static RoutedEvent RootLayoutResumedEvent = RadElement.RegisterRoutedEvent(nameof (RootLayoutResumedEvent), typeof (RootRadElement));
    public static RadProperty UsePaintCacheProperty = RadProperty.Register(nameof (UsePaintCache), typeof (bool), typeof (RootRadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ApplyShapeToControlProperty = RadProperty.Register(nameof (ApplyShapeToControl), typeof (bool), typeof (RootRadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ControlBoundsProperty = RadProperty.Register(nameof (ControlBounds), typeof (Rectangle), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Rectangle.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ControlDefaultSizeProperty = RadProperty.Register(nameof (ControlDefaultSize), typeof (Size), typeof (RootRadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Size.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private static readonly ArrayList excludedRootElementProps = new ArrayList((ICollection) new string[8]
    {
      "Visibility",
      "Font",
      "BackColor",
      "FitToAvailableSize",
      "Bounds",
      "Name",
      "ShouldHandleMouseInput",
      nameof (UsePaintCache)
    });
    internal const long DefaultStretchHorizontallyStateKey = 8796093022208;
    internal const long DefaultStretchVerticallyStateKey = 17592186044416;
    internal const long ControlInitiatedPropertyChangeStateKey = 35184372088832;
    internal const long RootElementInitiatedPropertyChangeStateKey = 70368744177664;
    internal const long DisableControlSizeSetStateKey = 140737488355328;
    internal const long ForcingLocationStateKey = 281474976710656;
    private Control parentControl;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.ShouldHandleMouseInput = false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(false)]
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

    public override bool IsElementVisible
    {
      get
      {
        if (this.ElementTree == null)
          return base.IsElementVisible;
        if (this.Visibility == ElementVisibility.Visible)
          return this.ElementTree.Control.Visible;
        return false;
      }
    }

    [DefaultValue(RadAutoSizeMode.FitToAvailableSize)]
    public override RadAutoSizeMode AutoSizeMode
    {
      get
      {
        return base.AutoSizeMode;
      }
      set
      {
        base.AutoSizeMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Foreground color - ex. of the text and borders of an element. The property is inheritable through the element tree.")]
    [TypeConverter(typeof (RadColorEditorConverter))]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool UseCompatibleTextRendering
    {
      get
      {
        return base.UseCompatibleTextRendering;
      }
      set
      {
        base.UseCompatibleTextRendering = value;
      }
    }

    [RadPropertyDefaultValue("ControlBounds", typeof (RootRadElement))]
    [Description("Represents the owning control bounding rectangle")]
    [Category("Layout")]
    public virtual Rectangle ControlBounds
    {
      get
      {
        return (Rectangle) this.GetValue(RootRadElement.ControlBoundsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RootRadElement.ControlBoundsProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("UsePaintCache", typeof (RootRadElement))]
    public bool UsePaintCache
    {
      get
      {
        return (bool) this.GetValue(RootRadElement.UsePaintCacheProperty);
      }
      set
      {
        int num = (int) this.SetValue(RootRadElement.UsePaintCacheProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ApplyShapeToControl", typeof (RootRadElement))]
    [Description("Gets or sets value indicating whether the shape set to the root elementwould be applied as a region to the RadControl that contains the element.")]
    public bool ApplyShapeToControl
    {
      get
      {
        return (bool) this.GetValue(RootRadElement.ApplyShapeToControlProperty);
      }
      set
      {
        int num = (int) this.SetValue(RootRadElement.ApplyShapeToControlProperty, (object) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DisableControlSizeSet
    {
      get
      {
        return this.BitState[140737488355328L];
      }
      set
      {
        this.SetBitState(140737488355328L, value);
      }
    }

    [RadPropertyDefaultValue("ControlDefaultSize", typeof (RootRadElement))]
    [Description("hen set, replaces the default control size.")]
    public Size ControlDefaultSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(RootRadElement.ControlDefaultSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RootRadElement.ControlDefaultSizeProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Padding Padding
    {
      get
      {
        return base.Padding;
      }
      set
      {
        base.Padding = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ContentAlignment Alignment
    {
      get
      {
        return base.Alignment;
      }
      set
      {
        base.Alignment = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override float AngleTransform
    {
      get
      {
        return base.AngleTransform;
      }
      set
      {
        base.AngleTransform = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool FlipText
    {
      get
      {
        return base.FlipText;
      }
      set
      {
        base.FlipText = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string KeyTip
    {
      get
      {
        return base.KeyTip;
      }
      set
      {
        base.KeyTip = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Padding Margin
    {
      get
      {
        return base.Margin;
      }
      set
      {
        base.Margin = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Orientation TextOrientation
    {
      get
      {
        return base.TextOrientation;
      }
      set
      {
        base.TextOrientation = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ToolTipText
    {
      get
      {
        return base.ToolTipText;
      }
      set
      {
        base.ToolTipText = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SaveCurrentStretchModeAsDefault()
    {
      this.BitState[8796093022208L] = this.StretchHorizontally;
      this.BitState[17592186044416L] = this.StretchVertically;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void NotifyControlImageListChanged()
    {
      this.RaiseTunnelEvent((RadElement) this, new RoutedEventArgs(EventArgs.Empty, RootRadElement.OnRoutedImageListChanged));
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "StretchHorizontally")
        return new bool?((bool) property.GetValue((object) this) != this.GetBitState(8796093022208L));
      if (property.Name == "StretchVertically")
        return new bool?((bool) property.GetValue((object) this) != this.GetBitState(17592186044416L));
      if (property.Name == "ToolTipText")
        return new bool?(!string.IsNullOrEmpty((string) property.GetValue((object) this)));
      if (property.Name == "Alignment")
        return new bool?(!property.GetValue((object) this).Equals(RadElement.AlignmentProperty.GetMetadata((RadObject) this).DefaultValue));
      if (property.Name == "AngleTransform")
        return new bool?(!property.GetValue((object) this).Equals(RadElement.AngleTransformProperty.GetMetadata((RadObject) this).DefaultValue));
      if (property.Name == "FlipText")
        return new bool?(!property.GetValue((object) this).Equals(RadItem.FlipTextProperty.GetMetadata((RadObject) this).DefaultValue));
      if (property.Name == "Margin")
        return new bool?(!property.GetValue((object) this).Equals(RadElement.MarginProperty.GetMetadata((RadObject) this).DefaultValue));
      if (property.Name == "TextOrientation")
        return new bool?(!property.GetValue((object) this).Equals(RadItem.TextOrientationProperty.GetMetadata((RadObject) this).DefaultValue));
      if (RootRadElement.excludedRootElementProps.Contains((object) property.Name))
        return new bool?(false);
      return new bool?();
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      if (this.GetBitState(281474976710656L) && args.Property == RadElement.BoundsProperty)
        return;
      base.OnPropertyChanging(args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (this.ElementTree == null || this.GetBitState(281474976710656L) && e.Property == RadElement.BoundsProperty)
        return;
      base.OnPropertyChanged(e);
      if (!this.GetBitState(35184372088832L))
      {
        this.BitState[70368744177664L] = true;
        if (e.Property == RootRadElement.ControlBoundsProperty)
          this.ElementTree.Control.Bounds = (Rectangle) e.NewValue;
        else if (e.Property == RadElement.StretchHorizontallyProperty)
          this.RaiseTunnelEvent((RadElement) this, new RoutedEventArgs((EventArgs) new StretchEventArgs(true, (bool) e.NewValue), RootRadElement.StretchChangedEvent));
        else if (e.Property == RadElement.StretchVerticallyProperty)
          this.RaiseTunnelEvent((RadElement) this, new RoutedEventArgs((EventArgs) new StretchEventArgs(false, (bool) e.NewValue), RootRadElement.StretchChangedEvent));
        else if (e.Property == RadElement.BoundsProperty)
        {
          if (this.Shape != null && this.ElementTree != null && this.ApplyShapeToControl && ((Rectangle) e.OldValue).Size != ((Rectangle) e.NewValue).Size)
            this.ElementTree.Control.Region = this.Shape.CreateRegion(new Rectangle(Point.Empty, new Size(this.Size.Width, this.Size.Height)));
        }
        else if (e.Property == RadElement.ShapeProperty && this.ApplyShapeToControl)
        {
          ElementShape newValue = e.NewValue as ElementShape;
          if (newValue != null && this.ElementTree != null)
            this.ElementTree.Control.Region = new Region(newValue.GetElementShape((RadElement) this));
        }
        else if (e.Property == RootRadElement.ApplyShapeToControlProperty)
        {
          if ((bool) e.NewValue && this.Shape != null)
            this.ElementTree.Control.Region = new Region(this.Shape.GetElementShape((RadElement) this));
          else
            this.ElementTree.Control.Region = (Region) null;
        }
        else if (e.Property == RootRadElement.ControlDefaultSizeProperty)
          this.OnControlDefaultSizeChanged(e);
        if (e.Property == RadItem.ShadowDepthProperty || e.Property == RadItem.EnableElementShadowProperty || (e.Property == RadItem.ShadowColorProperty || e.Property == RootRadElement.ControlBoundsProperty) || (e.Property == RadElement.VisibilityProperty || e.Property == RadElement.BoundsProperty || e.Property == VisualElement.BackColorProperty))
          this.PaintControlShadow();
        this.ElementTree.ComponentTreeHandler.OnAmbientPropertyChanged(e.Property);
        this.BitState[70368744177664L] = false;
      }
      this.BitState[35184372088832L] = false;
    }

    protected override void OnLayoutPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadElement.StretchHorizontallyProperty || e.Property == RadElement.StretchVerticallyProperty)
      {
        IComponentTreeHandler componentTreeHandler = this.ElementTree.ComponentTreeHandler;
        if (componentTreeHandler != null)
        {
          componentTreeHandler.ElementTree.PerformLayout();
          return;
        }
      }
      base.OnLayoutPropertyChanged(e);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (this.LayoutManager.IsUpdating)
        return;
      Control control = this.ElementTree.Control;
      if (control == null || !control.AutoSize)
        return;
      control.Size = this.Size;
    }

    protected override void OnLocationChanged(RadPropertyChangedEventArgs e)
    {
      base.OnLocationChanged(e);
      if (this.LayoutManager.IsUpdating)
        return;
      Control control = this.ElementTree.Control;
      if (control == null)
        return;
      control.Location = ((Rectangle) e.NewValue).Location;
    }

    protected override void OnDisplayPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (!this.IsInValidState(true))
        return;
      this.ElementTree.ComponentTreeHandler.OnDisplayPropertyChanged(e);
      base.OnDisplayPropertyChanged(e);
    }

    protected virtual void OnControlDefaultSizeChanged(RadPropertyChangedEventArgs e)
    {
      RadControl control = this.ElementTree.Control as RadControl;
      if (control == null || control.AutoSize)
        return;
      Size controlDefaultSize = control.GetControlDefaultSize();
      Size newValue = (Size) e.NewValue;
      if (controlDefaultSize == control.Size || control.Size == Size.Empty)
      {
        control.Size = newValue;
      }
      else
      {
        if (control.Size.Height >= newValue.Height)
          return;
        control.Height = newValue.Height;
      }
    }

    internal void OnControlAutoSizeChanged(bool autoSize)
    {
      this.RaiseTunnelEvent((RadElement) this, new RoutedEventArgs((EventArgs) new AutoSizeEventArgs(autoSize), RootRadElement.AutoSizeChangedEvent));
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        child.Measure(availableSize);
        SizeF desiredSize = child.DesiredSize;
        if ((double) empty.Width < (double) desiredSize.Width)
          empty.Width = desiredSize.Width;
        if ((double) empty.Height < (double) desiredSize.Height)
          empty.Height = desiredSize.Height;
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      for (int index = 0; index < this.Children.Count; ++index)
        this.Children[index].Arrange(new RectangleF(PointF.Empty, finalSize));
      return finalSize;
    }

    protected override SizeF MeasureCore(SizeF availableSize)
    {
      SizeF availableSize1 = new SizeF(Math.Max(availableSize.Width, 0.0f), Math.Max(availableSize.Height, 0.0f));
      RadElement.MinMax minMax = new RadElement.MinMax((RadElement) this);
      availableSize1.Width = Math.Max(minMax.minWidth, Math.Min(availableSize1.Width, minMax.maxWidth));
      availableSize1.Height = Math.Max(minMax.minHeight, Math.Min(availableSize1.Height, minMax.maxHeight));
      if (!this.StretchHorizontally)
        availableSize1.Width = float.PositiveInfinity;
      if (!this.StretchVertically)
        availableSize1.Height = float.PositiveInfinity;
      if ((double) minMax.maxWidth > 0.0 && (double) availableSize1.Width > (double) minMax.maxWidth)
        availableSize1.Width = minMax.maxWidth;
      if ((double) minMax.maxHeight > 0.0 && (double) availableSize1.Height > (double) minMax.maxHeight)
        availableSize1.Height = minMax.maxHeight;
      SizeF sizeF = this.MeasureOverride(availableSize1);
      sizeF = new SizeF(Math.Max(sizeF.Width, minMax.minWidth), Math.Max(sizeF.Height, minMax.minHeight));
      if ((double) sizeF.Width > (double) minMax.maxWidth && (double) minMax.maxWidth > 0.0)
        sizeF.Width = minMax.maxWidth;
      if ((double) sizeF.Height > (double) minMax.maxHeight && (double) minMax.maxHeight > 0.0)
        sizeF.Height = minMax.maxHeight;
      return new SizeF(Math.Max(0.0f, sizeF.Width), Math.Max(0.0f, sizeF.Height));
    }

    protected override void ArrangeCore(RectangleF finalRect)
    {
      Control control = this.ElementTree.Control;
      SizeF size = finalRect.Size;
      size.Width = Math.Max(0.0f, size.Width);
      size.Height = Math.Max(0.0f, size.Height);
      SizeF desiredSize = this.DesiredSize;
      if (!this.StretchHorizontally)
        size.Width = desiredSize.Width;
      if (!this.StretchVertically)
        size.Height = desiredSize.Height;
      RadElement.MinMax minMax = new RadElement.MinMax((RadElement) this);
      SizeF finalSize = new SizeF(Math.Max(size.Width, 0.0f), Math.Max(size.Height, 0.0f));
      finalSize.Width = Math.Max(minMax.minWidth, Math.Min(finalSize.Width, minMax.maxWidth));
      finalSize.Height = Math.Max(minMax.minHeight, Math.Min(finalSize.Height, minMax.maxHeight));
      SizeF sizeF1 = this.ArrangeOverride(finalSize);
      SizeF sizeF2 = sizeF1;
      if ((double) minMax.maxWidth > 0.0)
        sizeF2.Width = Math.Min(sizeF1.Width, minMax.maxWidth);
      if ((double) minMax.maxHeight > 0.0)
        sizeF2.Height = Math.Min(sizeF1.Height, minMax.maxHeight);
      Rectangle rectangle = new Rectangle(Point.Ceiling(finalRect.Location), Size.Ceiling(sizeF2));
      this.Bounds = rectangle;
      if (control == null || !control.AutoSize || this.GetBitState(140737488355328L))
        return;
      control.Size = rectangle.Size;
    }

    protected internal override RectangleF GetArrangeRect(RectangleF proposed)
    {
      if ((double) proposed.Width > 0.0 && (double) proposed.Height > 0.0)
        return proposed;
      RectangleF bounds = (RectangleF) this.ElementTree.Control.Bounds;
      if (float.IsPositiveInfinity(proposed.Width))
        bounds.Width = this.DesiredSize.Width;
      if (float.IsPositiveInfinity(proposed.Height))
        bounds.Height = this.DesiredSize.Height;
      return bounds;
    }

    public void Paint(IGraphics graphics, Rectangle clipRectangle)
    {
      this.PaintOverride(graphics, clipRectangle, 0.0f, new SizeF(1f, 1f), false);
    }

    public void Paint(IGraphics graphics, Rectangle clipRectangle, bool useRelativeTransformation)
    {
      this.PaintOverride(graphics, clipRectangle, 0.0f, new SizeF(1f, 1f), useRelativeTransformation);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.PaintControlShadow();
    }

    public void PaintControlShadow()
    {
      if (this.EnableElementShadow)
      {
        if (!this.IsPaintingRipple && this.parentControl != null)
        {
          Rectangle elementRect = Rectangle.Union(new Rectangle(this.ElementTree.Control.Bounds.Location, this.ElementTree.Control.Bounds.Size), new Rectangle(this.Bounds.Location, this.ElementTree.Control.Bounds.Size));
          elementRect = this.GetShadowRect(ref elementRect, 5);
          Region region = new Region(elementRect);
          this.parentControl.Invalidate(region, false);
          region.Dispose();
        }
        else
        {
          if (this.ElementState != ElementState.Loaded)
            return;
          this.WireParentControl();
          if (this.parentControl == null)
            return;
          this.PaintControlShadow();
        }
      }
      else
        this.ReleaseParentControl();
    }

    private void Control_ParentChanged(object sender, EventArgs e)
    {
      this.ReleaseParentControl();
      this.WireParentControl();
    }

    private void WireParentControl()
    {
      if (this.ElementTree == null || this.ElementTree.Control == null || this.ElementTree.Control.Parent == null)
        return;
      this.ElementTree.Control.ParentChanged += new EventHandler(this.Control_ParentChanged);
      this.parentControl = this.ElementTree.Control.Parent;
      this.parentControl.Paint += new PaintEventHandler(this.Parent_Paint);
    }

    protected override void DisposeManagedResources()
    {
      this.ReleaseParentControl();
      base.DisposeManagedResources();
    }

    private void ReleaseParentControl()
    {
      if (this.parentControl != null)
        this.parentControl.Invalidate(this.ElementTree.Control.Region, false);
      if (this.ElementTree != null && this.ElementTree.Control != null)
        this.ElementTree.Control.ParentChanged -= new EventHandler(this.Control_ParentChanged);
      if (this.parentControl == null)
        return;
      this.parentControl.Paint -= new PaintEventHandler(this.Parent_Paint);
      this.parentControl = (Control) null;
    }

    protected void Parent_Paint(object sender, PaintEventArgs e)
    {
      if (!this.ElementTree.Control.Visible)
        return;
      this.PaintShadowCore(e.Graphics, new Rectangle(this.ElementTree.Control.Location, this.Bounds.Size), (RadItem) this);
    }

    protected internal override object GetInheritedValue(RadProperty property)
    {
      if (this.ElementTree == null)
        return base.GetInheritedValue(property);
      return this.ElementTree.ComponentTreeHandler.GetAmbientPropertyValue(property);
    }

    protected override bool PerformLayoutTransformation(ref RadMatrix matrix)
    {
      return false;
    }

    protected override object CoerceValue(RadPropertyValue propVal, object baseValue)
    {
      if (propVal.Property == RootRadElement.ControlBoundsProperty)
        return (object) this.ElementTree.Control.Bounds;
      return base.CoerceValue(propVal, baseValue);
    }

    internal void ForceLocationTo(Point newLocation)
    {
      Rectangle bounds = this.Bounds;
      if (!(bounds.Location != newLocation))
        return;
      this.BitState[281474976710656L] = true;
      this.SetBoundsCore(new Rectangle(newLocation, bounds.Size));
      this.BitState[281474976710656L] = false;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      if (!RadControl.EnableDpiScaling)
        return;
      this.DpiScaleFactor = TelerikDpiHelper.ScaleSizeF(this.DpiScaleFactor, scaleFactor);
      this.InvalidateMeasure(true);
      base.DpiScaleChanged(this.DpiScaleFactor);
    }
  }
}
