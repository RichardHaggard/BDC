// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarScaleContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class TrackBarScaleContainerElement : TrackBarStackElement
  {
    private TrackBarScaleElement topLeftTicksLabels;
    private TrackBarScaleElement bottomRightTicksLabels;
    private TrackBarLineElement lineElement;
    private TickStyles tickStyle;
    private TrackBarLabelStyle labelStyles;

    protected override void CreateChildElements()
    {
      this.topLeftTicksLabels = new TrackBarScaleElement(true);
      this.bottomRightTicksLabels = new TrackBarScaleElement(false);
      this.lineElement = new TrackBarLineElement();
      this.Children.Add((RadElement) this.topLeftTicksLabels);
      this.Children.Add((RadElement) this.lineElement);
      this.Children.Add((RadElement) this.bottomRightTicksLabels);
      this.SetLabelStyles();
      this.SetTickStyle();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FitInAvailableSize = true;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Orientation = Orientation.Vertical;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    public TrackBarLineElement TrackBarLineElement
    {
      get
      {
        return this.lineElement;
      }
    }

    public TrackBarScaleElement TopScaleElement
    {
      get
      {
        return this.topLeftTicksLabels;
      }
    }

    public TrackBarScaleElement BottomScaleElement
    {
      get
      {
        return this.bottomRightTicksLabels;
      }
    }

    [Description("The number of positions between large tick marks")]
    public int LargeTickFrequency
    {
      get
      {
        return this.topLeftTicksLabels.TickContainerElement.LargeTickFrequency;
      }
      set
      {
        if (this.topLeftTicksLabels.TickContainerElement.LargeTickFrequency == value)
          return;
        if (value < 0)
          value = 0;
        this.topLeftTicksLabels.TickContainerElement.LargeTickFrequency = value;
        this.bottomRightTicksLabels.TickContainerElement.LargeTickFrequency = value;
        this.topLeftTicksLabels.LabelContainerElement.LargeTickFrequency = value;
        this.bottomRightTicksLabels.LabelContainerElement.LargeTickFrequency = value;
      }
    }

    [Description("The number of positions between small tick marks")]
    public int SmallTickFrequency
    {
      get
      {
        return this.topLeftTicksLabels.TickContainerElement.SmallTickFrequency;
      }
      set
      {
        if (this.topLeftTicksLabels.TickContainerElement.SmallTickFrequency == value)
          return;
        if (value < 0)
          value = 0;
        this.topLeftTicksLabels.TickContainerElement.SmallTickFrequency = value;
        this.bottomRightTicksLabels.TickContainerElement.SmallTickFrequency = value;
        this.topLeftTicksLabels.LabelContainerElement.SmallTickFrequency = value;
        this.bottomRightTicksLabels.LabelContainerElement.SmallTickFrequency = value;
      }
    }

    [Description("Gets or Sets whether the TrackBar's ticks should be drawn")]
    public TickStyles TickStyle
    {
      get
      {
        return this.tickStyle;
      }
      set
      {
        if (this.tickStyle == value)
          return;
        this.tickStyle = value;
        this.SetTickStyle();
        this.TrackBarElement.BodyElement.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged("ThickStyle");
      }
    }

    [Description("Gets or Sets whether the TrackBar's labels should be drawn")]
    public TrackBarLabelStyle LabelStyles
    {
      get
      {
        return this.labelStyles;
      }
      set
      {
        this.labelStyles = value;
        this.SetLabelStyles();
        this.TrackBarElement.BodyElement.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (LabelStyles));
      }
    }

    private void SetTickStyle()
    {
      switch (this.tickStyle)
      {
        case TickStyles.None:
          this.TopScaleElement.TickContainerElement.Visibility = ElementVisibility.Hidden;
          this.BottomScaleElement.TickContainerElement.Visibility = ElementVisibility.Hidden;
          break;
        case TickStyles.TopLeft:
          this.TopScaleElement.TickContainerElement.Visibility = ElementVisibility.Visible;
          this.BottomScaleElement.TickContainerElement.Visibility = ElementVisibility.Hidden;
          break;
        case TickStyles.BottomRight:
          this.TopScaleElement.TickContainerElement.Visibility = ElementVisibility.Hidden;
          this.BottomScaleElement.TickContainerElement.Visibility = ElementVisibility.Visible;
          break;
        case TickStyles.Both:
          this.TopScaleElement.TickContainerElement.Visibility = ElementVisibility.Visible;
          this.BottomScaleElement.TickContainerElement.Visibility = ElementVisibility.Visible;
          break;
      }
    }

    private void SetLabelStyles()
    {
      switch (this.labelStyles)
      {
        case TrackBarLabelStyle.None:
          this.TopScaleElement.LabelContainerElement.Visibility = ElementVisibility.Collapsed;
          this.BottomScaleElement.LabelContainerElement.Visibility = ElementVisibility.Collapsed;
          break;
        case TrackBarLabelStyle.TopLeft:
          this.TopScaleElement.LabelContainerElement.Visibility = ElementVisibility.Visible;
          this.BottomScaleElement.LabelContainerElement.Visibility = ElementVisibility.Collapsed;
          break;
        case TrackBarLabelStyle.BottomRight:
          this.TopScaleElement.LabelContainerElement.Visibility = ElementVisibility.Collapsed;
          this.BottomScaleElement.LabelContainerElement.Visibility = ElementVisibility.Visible;
          break;
        case TrackBarLabelStyle.Both:
          this.TopScaleElement.LabelContainerElement.Visibility = ElementVisibility.Visible;
          this.BottomScaleElement.LabelContainerElement.Visibility = ElementVisibility.Visible;
          break;
      }
    }
  }
}
