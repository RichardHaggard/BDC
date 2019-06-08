// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarThumbElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarThumbElement : TrackBarElementWithOrientation
  {
    private long prev = DateTime.Now.Ticks;
    public static RadProperty ThumbSizeProperty = RadProperty.Register(nameof (ThumbSize), typeof (Size), typeof (TrackBarThumbElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(10, 14), ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (TrackBarThumbElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private TrackBarRange rangeInfo;
    private TrackBarIndicatorElement owner;
    private bool isFirst;

    static TrackBarThumbElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TrackBarThumbElementStateManager(), typeof (TrackBarThumbElement));
    }

    public TrackBarThumbElement(TrackBarIndicatorElement owner, bool isFirst)
    {
      this.owner = owner;
      this.isFirst = isFirst;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.RangeInfo = (this.Parent as TrackBarIndicatorElement).RangeInfo;
      this.ToolTipText = this.RangeInfo.ToolTipText;
      this.RangeInfo.PropertyChanged += new PropertyChangedEventHandler(this.rangeInfo_PropertyChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.Alignment = ContentAlignment.MiddleRight;
      int num = (int) this.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      this.NotifyParentOnMouseInput = false;
    }

    protected override void DisposeManagedResources()
    {
      if (this.rangeInfo != null)
        this.rangeInfo.PropertyChanged -= new PropertyChangedEventHandler(this.rangeInfo_PropertyChanged);
      base.DisposeManagedResources();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.SelectThumb();
      long ticks = DateTime.Now.Ticks;
      if (ticks - this.prev > 1000L)
        this.MoveThumb(e);
      this.prev = ticks;
    }

    private void rangeInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ToolTipText"))
        return;
      this.ToolTipText = this.RangeInfo.ToolTipText;
    }

    public void SelectThumb()
    {
      if (this.IsSelected)
        return;
      this.TrackBarElement.CurrentThumb = this;
      this.TrackBarElement.BodyElement.IndicatorContainerElement.RefreshRanges();
      this.RangeInfo.IsSelected = true;
      if (this.TrackBarElement.TrackBarMode == TrackBarRangeMode.SingleThumb)
        return;
      this.IsSelected = true;
    }

    private void MoveThumb(MouseEventArgs e)
    {
      float tickOffSet = this.TrackBarElement.TickOffSet;
      float num1 = this.TrackBarElement.Minimum;
      int num2 = 1;
      int oldValue = (int) this.TrackBarElement.Value;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        if (this.RightToLeft)
        {
          float num3 = (float) (this.Parent.Parent.BoundingRectangle.Width - e.Location.X + this.Parent.Parent.LocationToControl().X - this.TrackBarElement.ThumbSize.Width / 2);
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
            num1 = num3 / tickOffSet;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
            num1 = (double) num3 % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) num3 / (double) tickOffSet) + num2) : (float) (int) ((double) num3 / (double) tickOffSet);
        }
        else
        {
          int num3 = this.Parent.Parent.LocationToControl().X + this.TrackBarElement.ThumbSize.Width / 2;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
            num1 = (float) (e.Location.X - num3) / tickOffSet;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
            num1 = (double) (e.Location.X - num3) % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) (e.Location.X - num3) / (double) tickOffSet) + num2) : (float) (int) ((double) (e.Location.X - num3) / (double) tickOffSet);
        }
      }
      else
      {
        int num3 = this.TrackBarElement.ThumbSize.Width / 2 - this.Parent.Parent.LocationToControl().Y;
        int height = this.Parent.Parent.BoundingRectangle.Height;
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
          num1 = (float) (height - e.Location.Y - num3) / tickOffSet;
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
          num1 = (double) (height - e.Location.Y - num3) % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) (height - e.Location.Y - num3) / (double) tickOffSet) + num2) : (float) (int) ((double) (height - e.Location.Y - num3) / (double) tickOffSet);
      }
      if ((double) num1 < 0.0)
        num1 = 0.0f;
      if ((double) num1 > (double) this.TrackBarElement.MaxTickNumber)
        num1 = (float) this.TrackBarElement.MaxTickNumber;
      if (!this.isFirst || this.TrackBarElement.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarElement.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning)
      {
        this.RangeInfo.End = this.TrackBarElement.Minimum + num1 * (float) num2;
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", (object) this.RangeInfo.End);
      }
      else
      {
        this.RangeInfo.Start = this.TrackBarElement.Minimum + num1 * (float) num2;
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", (object) this.RangeInfo.Start);
      }
      this.TrackBarElement.FireScrollEvent(oldValue);
    }

    public bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(TrackBarThumbElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarThumbElement.IsSelectedProperty, (object) value);
      }
    }

    public Size ThumbSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(TrackBarThumbElement.ThumbSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarThumbElement.ThumbSizeProperty, (object) value);
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

    public bool IsFirst
    {
      get
      {
        return this.isFirst;
      }
      set
      {
        this.isFirst = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None && !float.IsInfinity(availableSize.Width) && !float.IsInfinity(availableSize.Height))
        return availableSize;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
        return new SizeF((float) this.ThumbSize.Width, (float) this.ThumbSize.Height);
      return new SizeF((float) this.ThumbSize.Height, (float) this.ThumbSize.Width);
    }
  }
}
