// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.ElementWithCaptionLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.Layouts
{
  public class ElementWithCaptionLayoutPanel : LayoutPanel
  {
    public static RadProperty CaptionElementProperty = RadProperty.Register("CaptionElement", typeof (bool), typeof (ElementWithCaptionLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty CaptionOnTopProperty = RadProperty.Register(nameof (CaptionOnTop), typeof (bool), typeof (ElementWithCaptionLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ShowCaptionProperty = RadProperty.Register(nameof (ShowCaption), typeof (bool), typeof (ElementWithCaptionLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private RadElement captionElement;
    private RadElement bodyElement;

    public bool CaptionOnTop
    {
      get
      {
        return (bool) this.GetValue(ElementWithCaptionLayoutPanel.CaptionOnTopProperty);
      }
      set
      {
        int num = (int) this.SetValue(ElementWithCaptionLayoutPanel.CaptionOnTopProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ShowCaption", typeof (ElementWithCaptionLayoutPanel))]
    public bool ShowCaption
    {
      get
      {
        return (bool) this.GetValue(ElementWithCaptionLayoutPanel.ShowCaptionProperty);
      }
      set
      {
        this.InitializeElements();
        int num = (int) this.SetValue(ElementWithCaptionLayoutPanel.ShowCaptionProperty, (object) value);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF bounds = (RectangleF) this.Parent.Bounds;
      SizeF sizeF1 = finalSize;
      this.InitializeElements();
      SizeF sizeF2 = SizeF.Empty;
      SizeF sizeF3 = SizeF.Empty;
      int num1 = 8;
      if (this.captionElement != null)
      {
        sizeF2 = this.captionElement.DesiredSize;
        if (this.captionElement.Visibility != ElementVisibility.Collapsed)
          sizeF2.Height = Math.Max((float) num1, sizeF2.Height);
      }
      if (this.bodyElement != null)
      {
        sizeF3 = this.bodyElement.DesiredSize;
        if (this.bodyElement.Visibility == ElementVisibility.Collapsed)
          sizeF3.Height = 0.0f;
      }
      Math.Max(sizeF2.Width, sizeF3.Width);
      float height = sizeF3.Height;
      float width = sizeF1.Width;
      float num2 = sizeF1.Height - sizeF2.Height;
      if (this.captionElement != null)
      {
        SizeF size = new SizeF(width, sizeF2.Height);
        this.captionElement.Arrange(new RectangleF(this.CaptionOnTop ? PointF.Empty : new PointF(0.0f, num2), size));
      }
      if (this.bodyElement != null)
      {
        SizeF size = new SizeF(width, num2);
        this.bodyElement.Arrange(new RectangleF(this.CaptionOnTop ? new PointF(0.0f, sizeF2.Height) : PointF.Empty, size));
      }
      return finalSize;
    }

    private void InitializeElements()
    {
      foreach (RadElement child in this.Children)
      {
        if ((bool) child.GetValue(ElementWithCaptionLayoutPanel.CaptionElementProperty))
          this.captionElement = child;
        else
          this.bodyElement = child;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.InitializeElements();
      base.MeasureOverride(availableSize);
      return new SizeF(Math.Max(this.captionElement.DesiredSize.Width, this.bodyElement.DesiredSize.Width), this.captionElement.DesiredSize.Height + this.bodyElement.DesiredSize.Height);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == ElementWithCaptionLayoutPanel.ShowCaptionProperty)
        this.captionElement.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      base.OnPropertyChanged(e);
    }
  }
}
