// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarThumb
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarThumb : RadItem
  {
    public static RadProperty ThumbWidthProperty = RadProperty.Register(nameof (ThumbWidth), typeof (int), typeof (TrackBarThumb), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 12, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty UseDefaultThumbShapeProperty = RadProperty.Register(nameof (UseDefaultThumbShape), typeof (bool), typeof (TrackBarThumb), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private FillPrimitive backFill;
    private BorderPrimitive borderPrimitive;
    private RadTrackBarElement parentTrackBarElement;

    public TrackBarThumb()
    {
      this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
    }

    static TrackBarThumb()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (TrackBarThumb));
    }

    [Description("Gets or sets TrackBar's thumb width.")]
    [RadPropertyDefaultValue("ThumbWidth", typeof (TrackBarThumb))]
    public virtual int ThumbWidth
    {
      get
      {
        return (int) this.GetValue(TrackBarThumb.ThumbWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarThumb.ThumbWidthProperty, (object) value);
      }
    }

    public virtual bool UseDefaultThumbShape
    {
      get
      {
        return (bool) this.GetValue(TrackBarThumb.UseDefaultThumbShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarThumb.UseDefaultThumbShapeProperty, (object) value);
      }
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
    }

    internal void PerformMouseUp(MouseEventArgs e)
    {
      this.DoMouseUp(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
    }

    public RadTrackBarElement ParentTrackBarElement
    {
      get
      {
        if (this.parentTrackBarElement == null)
        {
          for (RadElement parent = this.Parent; parent != null && this.parentTrackBarElement == null; parent = parent.Parent)
            this.parentTrackBarElement = parent as RadTrackBarElement;
        }
        return this.parentTrackBarElement;
      }
    }

    protected override void CreateChildElements()
    {
      this.backFill = new FillPrimitive();
      this.backFill.Class = "ThumbFillBack";
      this.backFill.BackColor = Color.Black;
      this.backFill.BackColor2 = Color.White;
      this.backFill.GradientStyle = GradientStyles.Linear;
      this.Children.Add((RadElement) this.backFill);
      this.backFill.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "TrackBarThumbBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (float.IsInfinity(availableSize.Width) || !float.IsInfinity(availableSize.Height))
        ;
      return sizeF;
    }
  }
}
