// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarScaleElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarScaleElement : TrackBarStackElement
  {
    public static RadProperty IsTopLeftProperty = RadProperty.Register(nameof (IsTopLeft), typeof (bool), typeof (TrackBarScaleElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout));
    private TrackBarTickContainerElement ticksElement;
    private TrackBarLabelContainerElement labelElement;

    public TrackBarScaleElement(bool isTopLeft)
    {
      this.IsTopLeft = isTopLeft;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.AddChilds(this.IsTopLeft);
    }

    protected override void CreateChildElements()
    {
      this.ticksElement = new TrackBarTickContainerElement();
      this.labelElement = new TrackBarLabelContainerElement();
      this.Children.Add((RadElement) this.ticksElement);
      this.Children.Add((RadElement) this.labelElement);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.FitInAvailableSize = true;
      this.Alignment = ContentAlignment.MiddleCenter;
      this.Orientation = Orientation.Vertical;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    public TrackBarTickContainerElement TickContainerElement
    {
      get
      {
        return this.ticksElement;
      }
    }

    public TrackBarLabelContainerElement LabelContainerElement
    {
      get
      {
        return this.labelElement;
      }
    }

    public bool IsTopLeft
    {
      get
      {
        return (bool) this.GetValue(TrackBarScaleElement.IsTopLeftProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarScaleElement.IsTopLeftProperty, (object) value);
      }
    }

    private void AddChilds(bool isTopLeft)
    {
      this.Children.Clear();
      if (isTopLeft)
      {
        this.Children.Add((RadElement) this.labelElement);
        this.Children.Add((RadElement) this.ticksElement);
      }
      else
      {
        this.Children.Add((RadElement) this.ticksElement);
        this.Children.Add((RadElement) this.labelElement);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.ticksElement.Visibility == ElementVisibility.Hidden && this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        sizeF.Height -= this.ticksElement.DesiredSize.Height;
        if ((double) sizeF.Height <= 0.0)
          sizeF.Height = 1f;
      }
      return sizeF;
    }
  }
}
