// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterElementLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SplitterElementLayout : StackLayoutElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (SplitterElementLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty UseSplitterButtonsProperty = RadProperty.Register(nameof (UseSplitterButtons), typeof (bool), typeof (SplitterElementLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private SplitterElement owner;

    static SplitterElementLayout()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new SplitterLayoutStateManager(), typeof (SplitterElementLayout));
    }

    public SplitterElementLayout(SplitterElement owner)
    {
      this.owner = owner;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    [Description("")]
    [RadPropertyDefaultValue("UseSplitterButtons", typeof (SplitterElementLayout))]
    public bool UseSplitterButtons
    {
      get
      {
        return (bool) this.GetValue(SplitterElementLayout.UseSplitterButtonsProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitterElementLayout.UseSplitterButtonsProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.Children.Count == 0)
        return base.MeasureOverride(availableSize);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        int num3 = this.owner.ThumbLength;
        if (this.Children[index].MinSize.Height != 0)
          num3 = this.Children[index].MinSize.Height;
        SizeF availableSize1 = this.Orientation == Orientation.Horizontal ? new SizeF((float) num3, availableSize.Height) : new SizeF(availableSize.Width, (float) num3);
        this.Children[index].Measure(availableSize1);
        if (this.Orientation == Orientation.Horizontal)
        {
          num1 += this.Children[index].DesiredSize.Width;
          num2 = Math.Max(num2, this.Children[index].DesiredSize.Height);
        }
        else
        {
          num1 = Math.Max(num1, this.Children[index].DesiredSize.Width);
          num2 += this.Children[index].DesiredSize.Height;
        }
      }
      return new SizeF(num1, num2);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Children.Count == 0)
        return base.ArrangeOverride(finalSize);
      int num1 = 0;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility == ElementVisibility.Visible)
          num1 += this.owner.ThumbLength;
      }
      float num2 = this.Orientation == Orientation.Horizontal ? (float) (((double) finalSize.Width - (double) num1) / 2.0) : (float) (((double) finalSize.Height - (double) num1) / 2.0);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility == ElementVisibility.Visible)
        {
          if (this.Orientation == Orientation.Horizontal)
            this.Children[index].Arrange(new RectangleF(num2, 0.0f, (float) this.owner.ThumbLength, finalSize.Height));
          else
            this.Children[index].Arrange(new RectangleF(0.0f, num2, finalSize.Width, (float) this.owner.ThumbLength));
          num2 += (float) this.owner.ThumbLength;
        }
      }
      return finalSize;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != SplitterElementLayout.UseSplitterButtonsProperty)
        return;
      if ((bool) e.NewValue)
        this.BackgroundShape = (RadImageShape) null;
      else if ((bool) this.GetValue(SplitterElementLayout.IsVerticalProperty))
        this.BackgroundShape = this.owner.VerticalImage;
      else
        this.BackgroundShape = this.owner.HorizontalImage;
    }
  }
}
