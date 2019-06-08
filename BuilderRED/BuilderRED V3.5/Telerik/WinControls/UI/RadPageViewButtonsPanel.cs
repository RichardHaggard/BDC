// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewButtonsPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewButtonsPanel : RadPageViewElementBase
  {
    public static RadProperty ButtonsSizeProperty = RadProperty.Register(nameof (ButtonsSize), typeof (Size), typeof (RadPageViewButtonsPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(16, 16), ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ButtonsSpacingProperty = RadProperty.Register(nameof (ButtonsSpacing), typeof (int), typeof (RadPageViewButtonsPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));

    [Category("Appearance")]
    [Description("Gets or sets the size to be applied to each of the embedded buttons.")]
    public Size ButtonsSize
    {
      get
      {
        return (Size) this.GetValue(RadPageViewButtonsPanel.ButtonsSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewButtonsPanel.ButtonsSizeProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the spacing between each two buttons.")]
    public int ButtonsSpacing
    {
      get
      {
        return (int) this.GetValue(RadPageViewButtonsPanel.ButtonsSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewButtonsPanel.ButtonsSpacingProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadPageViewButtonsPanel.ButtonsSizeProperty)
        return;
      Size newValue = (Size) e.NewValue;
      foreach (RadObject child in this.Children)
      {
        int num = (int) child.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) newValue);
      }
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      if (changeOperation != ItemsChangeOperation.Inserted && changeOperation != ItemsChangeOperation.Set)
        return;
      int num = (int) child.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) this.ButtonsSize);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      int num1 = 0;
      Size buttonsSize = this.ButtonsSize;
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          SizeF availableSize1 = (SizeF) buttonsSize;
          if (child.MinSize != Size.Empty)
          {
            if (child.MinSize.Width > 0)
              availableSize1.Width = (float) Math.Max(child.MinSize.Width, buttonsSize.Width);
            if (child.MinSize.Height > 0)
              availableSize1.Height = (float) Math.Max(child.MinSize.Height, buttonsSize.Height);
          }
          child.Measure(availableSize1);
          SizeF desiredSize = child.DesiredSize;
          Padding margin = child.Margin;
          switch (this.ContentOrientation)
          {
            case PageViewContentOrientation.Horizontal:
            case PageViewContentOrientation.Horizontal180:
              empty.Width += desiredSize.Width + (float) margin.Horizontal;
              empty.Height = Math.Max(empty.Height, desiredSize.Height + (float) margin.Vertical);
              break;
            case PageViewContentOrientation.Vertical90:
            case PageViewContentOrientation.Vertical270:
              empty.Width = Math.Max(empty.Width, desiredSize.Width + (float) margin.Horizontal);
              empty.Height += desiredSize.Height + (float) margin.Vertical;
              break;
          }
          ++num1;
        }
      }
      if (num1 == 0)
        return SizeF.Empty;
      if (num1 > 1)
      {
        int num2 = this.ButtonsSpacing * (num1 - 1);
        switch (this.ContentOrientation)
        {
          case PageViewContentOrientation.Horizontal:
          case PageViewContentOrientation.Horizontal180:
            empty.Width += (float) num2;
            break;
          case PageViewContentOrientation.Vertical90:
          case PageViewContentOrientation.Vertical270:
            empty.Height += (float) num2;
            break;
        }
      }
      return this.ApplyMinMaxSize(this.ApplyClientOffset(empty));
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.ArrangeButtons(this.GetClientRectangle(finalSize));
      return finalSize;
    }

    private void ArrangeButtons(RectangleF client)
    {
      float x = client.X;
      float y = client.Y;
      PointF location = PointF.Empty;
      int buttonsSpacing = this.ButtonsSpacing;
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          SizeF desiredSize = child.DesiredSize;
          Padding margin = child.Margin;
          switch (this.ContentOrientation)
          {
            case PageViewContentOrientation.Horizontal:
            case PageViewContentOrientation.Horizontal180:
              location = new PointF(x + (float) margin.Left, client.Y + (float) (((double) client.Height - (double) desiredSize.Height) / 2.0));
              x += desiredSize.Width + (float) margin.Horizontal + (float) buttonsSpacing;
              break;
            case PageViewContentOrientation.Vertical90:
            case PageViewContentOrientation.Vertical270:
              location = new PointF(client.X + (float) (((double) client.Width - (double) desiredSize.Width) / 2.0), y + (float) margin.Top);
              y += desiredSize.Height + (float) margin.Vertical + (float) buttonsSpacing;
              break;
          }
          child.Arrange(new RectangleF(location, desiredSize));
        }
      }
    }
  }
}
