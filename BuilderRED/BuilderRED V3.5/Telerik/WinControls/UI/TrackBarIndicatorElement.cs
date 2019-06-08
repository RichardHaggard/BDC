// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarIndicatorElement : TrackBarStackElement
  {
    private TrackBarRange rangeInfo;
    private TrackBarThumbElement startThumb;
    private TrackBarRangeElement range;
    private TrackBarThumbElement endThumb;
    private bool isSelected;

    public TrackBarIndicatorElement(TrackBarRange rangeInfo)
    {
      this.RangeInfo = rangeInfo;
    }

    protected override void CreateChildElements()
    {
      this.startThumb = new TrackBarThumbElement(this, true);
      this.range = new TrackBarRangeElement();
      this.endThumb = new TrackBarThumbElement(this, false);
      this.Children.Add((RadElement) this.startThumb);
      this.Children.Add((RadElement) this.range);
      this.Children.Add((RadElement) this.endThumb);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.Alignment = ContentAlignment.MiddleRight;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    public TrackBarRangeElement RangeElement
    {
      get
      {
        return this.range;
      }
    }

    public TrackBarThumbElement StartThumbElement
    {
      get
      {
        return this.startThumb;
      }
    }

    public TrackBarThumbElement EndThumbElement
    {
      get
      {
        return this.endThumb;
      }
    }

    public TrackBarRange RangeInfo
    {
      get
      {
        return this.rangeInfo;
      }
      set
      {
        this.rangeInfo = value;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.isSelected;
      }
      set
      {
        this.isSelected = value;
      }
    }

    public void UpdateIndicatorElement()
    {
      this.Children.Clear();
      if (this.Orientation == Orientation.Horizontal)
      {
        this.StartThumbElement.StretchHorizontally = false;
        this.StartThumbElement.StretchVertically = true;
        this.StartThumbElement.IsVertical = false;
        int num = (int) this.RangeElement.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(0, 2));
        this.RangeElement.Alignment = ContentAlignment.MiddleRight;
        this.RangeElement.IsVertical = false;
        this.EndThumbElement.StretchHorizontally = false;
        this.EndThumbElement.StretchVertically = true;
        this.EndThumbElement.IsVertical = false;
        this.Children.Add((RadElement) this.startThumb);
        this.Children.Add((RadElement) this.range);
        this.Children.Add((RadElement) this.endThumb);
      }
      else
      {
        this.StartThumbElement.StretchHorizontally = true;
        this.StartThumbElement.StretchVertically = false;
        this.StartThumbElement.IsVertical = true;
        int num = (int) this.RangeElement.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(2, 0));
        this.RangeElement.Alignment = ContentAlignment.TopCenter;
        this.RangeElement.IsVertical = true;
        this.EndThumbElement.StretchHorizontally = true;
        this.EndThumbElement.StretchVertically = false;
        this.EndThumbElement.IsVertical = true;
        this.Children.Add((RadElement) this.endThumb);
        this.Children.Add((RadElement) this.range);
        this.Children.Add((RadElement) this.startThumb);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.UpdateThumbVisibility();
      return this.MeasureSnapeModeNone(availableSize);
    }

    private SizeF MeasureSnapeModeNone(SizeF availableSize)
    {
      if (this.TrackBarElement.SnapMode != TrackBarSnapModes.None || this.TrackBarElement.TrackBarMode != TrackBarRangeMode.Range || (double) this.rangeInfo.Start == (double) this.rangeInfo.End)
        return base.MeasureOverride(availableSize);
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
        return this.MeasureSnapeModeNoneHorizontal(availableSize);
      return this.MeasureSnapeModeNoneVeritcal(availableSize);
    }

    private SizeF MeasureSnapeModeNoneVeritcal(SizeF availableSize)
    {
      float height = (this.rangeInfo.End - this.rangeInfo.Start) * this.TrackBarElement.TickOffSet;
      if ((double) height > (double) this.TrackBarElement.ThumbSize.Width)
      {
        this.range.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Width, height));
        this.startThumb.Measure(availableSize);
        this.endThumb.Measure(availableSize);
        return new SizeF((float) this.TrackBarElement.ThumbSize.Height, height + (float) this.TrackBarElement.ThumbSize.Width);
      }
      this.range.Measure(SizeF.Empty);
      if (this.StartThumbElement.IsSelected)
      {
        this.startThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Height, (float) this.TrackBarElement.ThumbSize.Width));
        this.StartThumbElement.ZIndex = 10;
        this.EndThumbElement.ZIndex = 0;
      }
      else
        this.startThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Height, height));
      if (this.EndThumbElement.IsSelected)
      {
        this.endThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Height, (float) this.TrackBarElement.ThumbSize.Width));
        this.StartThumbElement.ZIndex = 0;
        this.EndThumbElement.ZIndex = 10;
      }
      else
        this.endThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Height, height));
      return new SizeF((float) this.TrackBarElement.ThumbSize.Height, height + (float) this.TrackBarElement.ThumbSize.Width);
    }

    private SizeF MeasureSnapeModeNoneHorizontal(SizeF availableSize)
    {
      float width = (this.rangeInfo.End - this.rangeInfo.Start) * this.TrackBarElement.TickOffSet;
      if ((double) width > (double) this.TrackBarElement.ThumbSize.Width)
      {
        this.range.Measure(new SizeF(width, (float) this.TrackBarElement.ThumbSize.Height));
        this.startThumb.Measure(availableSize);
        this.endThumb.Measure(availableSize);
        return new SizeF(width + (float) this.TrackBarElement.ThumbSize.Width, (float) this.TrackBarElement.ThumbSize.Height);
      }
      this.range.Measure(SizeF.Empty);
      if (this.StartThumbElement.IsSelected)
      {
        this.startThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Width, (float) this.TrackBarElement.ThumbSize.Height));
        this.StartThumbElement.ZIndex = 10;
        this.EndThumbElement.ZIndex = 0;
      }
      else
        this.startThumb.Measure(new SizeF(width, (float) this.TrackBarElement.ThumbSize.Height));
      if (this.EndThumbElement.IsSelected)
      {
        this.endThumb.Measure(new SizeF((float) this.TrackBarElement.ThumbSize.Width, (float) this.TrackBarElement.ThumbSize.Height));
        this.StartThumbElement.ZIndex = 0;
        this.EndThumbElement.ZIndex = 10;
      }
      else
        this.endThumb.Measure(new SizeF(width, (float) this.TrackBarElement.ThumbSize.Height));
      return new SizeF(width + (float) this.TrackBarElement.ThumbSize.Width, (float) this.TrackBarElement.ThumbSize.Height);
    }

    private void UpdateThumbVisibility()
    {
      if (this.TrackBarElement.TrackBarMode != TrackBarRangeMode.Range)
        return;
      if ((double) this.rangeInfo.End - (double) this.rangeInfo.Start <= 0.0)
      {
        if (this.StartThumbElement.IsSelected)
        {
          this.EndThumbElement.Visibility = ElementVisibility.Collapsed;
          this.StartThumbElement.Visibility = ElementVisibility.Visible;
        }
        else if (this.EndThumbElement.IsSelected)
        {
          this.StartThumbElement.Visibility = ElementVisibility.Collapsed;
          this.EndThumbElement.Visibility = ElementVisibility.Visible;
        }
        else if ((double) this.rangeInfo.Start < ((double) this.TrackBarElement.Maximum - (double) this.TrackBarElement.Minimum) / 2.0)
        {
          this.StartThumbElement.Visibility = ElementVisibility.Collapsed;
          this.EndThumbElement.Visibility = ElementVisibility.Visible;
        }
        else
        {
          this.EndThumbElement.Visibility = ElementVisibility.Collapsed;
          this.StartThumbElement.Visibility = ElementVisibility.Visible;
        }
      }
      else
      {
        this.StartThumbElement.Visibility = ElementVisibility.Visible;
        this.EndThumbElement.Visibility = ElementVisibility.Visible;
      }
      if (this.rangeInfo.IsSelected)
        this.ZIndex = 5;
      else
        this.ZIndex = 0;
    }
  }
}
