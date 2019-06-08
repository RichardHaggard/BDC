// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WaitingBarContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class WaitingBarContentElement : LightVisualElement
  {
    public static RadProperty WaitingStyleProperty = RadProperty.Register(nameof (WaitingStyle), typeof (WaitingBarStyles), typeof (WaitingBarContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) WaitingBarStyles.Indeterminate, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    protected bool waitingFirstRun;
    protected float offset;
    protected bool isBackwards;
    private WaitingBarSeparatorElement separatorElement;
    private WaitingBarTextElement textElement;
    private ProgressOrientation waitingDirection;
    private bool isWaiting;
    private WaitingBarIndicatorCollection indicators;
    private bool clearedDesignTimeItems;

    public WaitingBarContentElement()
    {
      this.offset = 0.0f;
      this.waitingDirection = ProgressOrientation.Right;
      this.WaitingIndicators.CollectionChanged += new NotifyCollectionChangedEventHandler(this.WaitingIndicators_CollectionChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.waitingFirstRun = true;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.DrawFill = false;
      this.DrawBorder = false;
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.separatorElement = new WaitingBarSeparatorElement();
      this.SeparatorElement.Class = "WaitingBarSeparator";
      this.Children.Add((RadElement) this.SeparatorElement);
      this.textElement = new WaitingBarTextElement();
      this.textElement.ZIndex = 1;
      this.Children.Add((RadElement) this.TextElement);
      this.indicators = new WaitingBarIndicatorCollection();
      this.CreateIndicators();
      foreach (RadElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
        this.Children.Add(waitingIndicator);
    }

    protected virtual void CreateIndicators()
    {
      if (this.ElementTree != null && this.ElementTree.Control != null && (this.ElementTree.Control is RadControl && (this.ElementTree.Control as RadControl).IsInitializing) && this.clearedDesignTimeItems)
        return;
      if (this.IsDesignMode)
      {
        IDesignerHost service = this.ElementTree.Control.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
        while (this.WaitingIndicators.Count > 0)
        {
          int count = this.WaitingIndicators.Count;
          BaseWaitingBarIndicatorElement waitingIndicator = this.WaitingIndicators[count - 1];
          this.WaitingIndicators.RemoveAt(count - 1);
          service.DestroyComponent((IComponent) waitingIndicator);
        }
        switch (this.WaitingStyle)
        {
          case WaitingBarStyles.Indeterminate:
          case WaitingBarStyles.Throbber:
          case WaitingBarStyles.Dash:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (WaitingBarIndicatorElement)) as WaitingBarIndicatorElement));
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (WaitingBarIndicatorElement)) as WaitingBarIndicatorElement));
            int num1 = (int) this.WaitingIndicators[0].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
            int num2 = (int) this.WaitingIndicators[1].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
            break;
          case WaitingBarStyles.DotsLine:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (DotsLineWaitingBarIndicatorElement)) as DotsLineWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.DotsSpinner:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (DotsSpinnerWaitingBarIndicatorElement)) as DotsSpinnerWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.LineRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (LineRingWaitingBarIndicatorElement)) as LineRingWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.SegmentedRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (SegmentedRingWaitingBarIndicatorElement)) as SegmentedRingWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.DotsRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (DotsRingWaitingBarIndicatorElement)) as DotsRingWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.FadingRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (FadingRingWaitingBarIndicatorElement)) as FadingRingWaitingBarIndicatorElement));
            break;
          case WaitingBarStyles.RotatingRings:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) (service.CreateComponent(typeof (RotatingRingsWaitingBarIndicatorElement)) as RotatingRingsWaitingBarIndicatorElement));
            break;
          default:
            throw new NotImplementedException("WaitingStyle: " + this.WaitingStyle.ToString() + " is not implemented.");
        }
      }
      else
      {
        this.WaitingIndicators.Clear();
        switch (this.WaitingStyle)
        {
          case WaitingBarStyles.Indeterminate:
          case WaitingBarStyles.Throbber:
          case WaitingBarStyles.Dash:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new WaitingBarIndicatorElement());
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new WaitingBarIndicatorElement());
            int num1 = (int) this.WaitingIndicators[0].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
            int num2 = (int) this.WaitingIndicators[1].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
            break;
          case WaitingBarStyles.DotsLine:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new DotsLineWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.DotsSpinner:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new DotsSpinnerWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.LineRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new LineRingWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.SegmentedRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new SegmentedRingWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.DotsRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new DotsRingWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.FadingRing:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new FadingRingWaitingBarIndicatorElement());
            break;
          case WaitingBarStyles.RotatingRings:
            this.WaitingIndicators.Add((BaseWaitingBarIndicatorElement) new RotatingRingsWaitingBarIndicatorElement());
            break;
          default:
            throw new NotImplementedException("WaitingStyle: " + this.WaitingStyle.ToString() + " is not implemented.");
        }
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WaitingBarIndicatorCollection WaitingIndicators
    {
      get
      {
        return this.indicators;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets an instance of the WaitingBarTextElement class which represents the waiting bar text element")]
    public WaitingBarTextElement TextElement
    {
      get
      {
        return this.textElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets an instance of the WaitingBarSeparatorElement class which represents the waiting bar separator element")]
    public WaitingBarSeparatorElement SeparatorElement
    {
      get
      {
        return this.separatorElement;
      }
    }

    [Description("Gets and sets the direction of waiting")]
    [DefaultValue(ProgressOrientation.Right)]
    public ProgressOrientation WaitingDirection
    {
      get
      {
        return this.waitingDirection;
      }
      set
      {
        this.waitingDirection = value;
        this.SeparatorElement.ProgressOrientation = !this.IsVertical() ? ProgressOrientation.Right : ProgressOrientation.Top;
        foreach (BaseWaitingBarIndicatorElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
          waitingIndicator.UpdateWaitingDirection(value);
      }
    }

    [Description("Indicates whether the element is currently waiting")]
    public bool IsWaiting
    {
      get
      {
        return this.isWaiting;
      }
      set
      {
        this.isWaiting = value;
      }
    }

    [Browsable(true)]
    [Description("Sets the style of the WaitingBarElement")]
    [DefaultValue(WaitingBarStyles.Indeterminate)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public WaitingBarStyles WaitingStyle
    {
      get
      {
        return (WaitingBarStyles) this.GetValue(WaitingBarContentElement.WaitingStyleProperty);
      }
      set
      {
        bool isOldWaitingStyle = this.IsOldWaitingStyle;
        WaitingBarStyles waitingStyle = this.WaitingStyle;
        int num = (int) this.SetValue(WaitingBarContentElement.WaitingStyleProperty, (object) value);
        this.SeparatorElement.Dash = this.WaitingStyle == WaitingBarStyles.Dash;
        if (waitingStyle == value)
          return;
        if (!isOldWaitingStyle || isOldWaitingStyle && !this.IsOldWaitingStyle)
          this.CreateIndicators();
        this.UpdateBorderAndFillVisibility(isOldWaitingStyle);
      }
    }

    protected virtual void UpdateBorderAndFillVisibility(bool wasPrevWaitingStyleOld)
    {
      RadWaitingBarElement ancestor = this.FindAncestor<RadWaitingBarElement>();
      if (this.IsOldWaitingStyle)
      {
        if (wasPrevWaitingStyleOld)
          return;
        int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
        if (ancestor == null)
          return;
        int num3 = (int) ancestor.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num4 = (int) ancestor.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
      }
      else
      {
        if (!wasPrevWaitingStyleOld)
          return;
        this.DrawFill = false;
        this.DrawBorder = false;
        if (ancestor == null)
          return;
        ancestor.DrawFill = false;
        ancestor.DrawBorder = false;
      }
    }

    protected internal bool IsOldWaitingStyle
    {
      get
      {
        return this.WaitingStyle == WaitingBarStyles.Indeterminate || this.WaitingStyle == WaitingBarStyles.Throbber || this.WaitingStyle == WaitingBarStyles.Dash;
      }
    }

    public ProgressOrientation GetReversedDirection(ProgressOrientation direction)
    {
      ProgressOrientation progressOrientation;
      switch (direction)
      {
        case ProgressOrientation.Top:
          progressOrientation = ProgressOrientation.Bottom;
          break;
        case ProgressOrientation.Bottom:
          progressOrientation = ProgressOrientation.Top;
          break;
        case ProgressOrientation.Left:
          progressOrientation = ProgressOrientation.Right;
          break;
        default:
          progressOrientation = ProgressOrientation.Left;
          break;
      }
      return progressOrientation;
    }

    public void IncrementOffset(int value)
    {
      this.offset += (float) value;
    }

    public bool IsVertical()
    {
      return this.WaitingDirection != ProgressOrientation.Right && this.WaitingDirection != ProgressOrientation.Left;
    }

    public virtual void ResetWaiting()
    {
      this.offset = 0.0f;
      this.isBackwards = false;
      this.waitingFirstRun = true;
      if (this.IsOldWaitingStyle)
      {
        this.SeparatorElement.InvalidateMeasure();
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].InvalidateMeasure();
        this.InvalidateMeasure();
      }
      else
      {
        foreach (BaseWaitingBarIndicatorElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
        {
          waitingIndicator.ResetAnimation();
          waitingIndicator.Invalidate();
        }
      }
    }

    protected void UpdateIndicatorStretch(BaseWaitingBarIndicatorElement indicator)
    {
      if ((object) indicator.GetType() == (object) typeof (WaitingBarIndicatorElement))
      {
        if (this.IsVertical())
        {
          indicator.StretchHorizontally = true;
          indicator.StretchVertically = false;
        }
        else
        {
          indicator.StretchHorizontally = false;
          indicator.StretchVertically = true;
        }
      }
      else
      {
        indicator.StretchHorizontally = true;
        indicator.StretchVertically = true;
      }
    }

    protected void UpdateElementsState(WaitingBarIndicatorElement indicator)
    {
      bool flag = this.IsVertical();
      int num1 = (int) indicator.SetValue(WaitingBarIndicatorElement.IsVerticalProperty, (object) flag);
      int num2 = (int) indicator.SeparatorElement.SetValue(WaitingBarSeparatorElement.IsVerticalProperty, (object) flag);
    }

    private void WaitingIndicators_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        if (this.ElementTree == null || this.ElementTree.Control == null || this.ElementTree.Control is RadControl && (this.ElementTree.Control as RadControl).IsInitializing && !this.clearedDesignTimeItems)
        {
          this.clearedDesignTimeItems = true;
          List<BaseWaitingBarIndicatorElement> indicatorElementList = new List<BaseWaitingBarIndicatorElement>();
          foreach (BaseWaitingBarIndicatorElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
          {
            if (!e.NewItems.Contains((object) waitingIndicator))
              indicatorElementList.Add(waitingIndicator);
          }
          while (indicatorElementList.Count > 0)
          {
            BaseWaitingBarIndicatorElement indicatorElement = indicatorElementList[indicatorElementList.Count - 1];
            this.WaitingIndicators.Remove(indicatorElement);
            indicatorElementList.Remove(indicatorElement);
          }
        }
        foreach (BaseWaitingBarIndicatorElement newItem in (IEnumerable) e.NewItems)
        {
          this.Children.Add((RadElement) newItem);
          if (newItem is WaitingBarIndicatorElement)
            this.UpdateElementsState(newItem as WaitingBarIndicatorElement);
          this.UpdateIndicatorStretch(newItem);
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (BaseWaitingBarIndicatorElement newItem in (IEnumerable) e.NewItems)
        {
          newItem.Invalidate();
          if (this.Children.Contains((RadElement) newItem))
            this.Children.Remove((RadElement) newItem);
        }
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Reset)
          return;
        foreach (BaseWaitingBarIndicatorElement oldItem in (IEnumerable) e.OldItems)
        {
          oldItem.Invalidate();
          if (this.Children.Contains((RadElement) oldItem))
            this.Children.Remove((RadElement) oldItem);
        }
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      this.SeparatorElement.Measure(availableSize);
      this.TextElement.Measure(availableSize);
      for (int index = 0; index < this.WaitingIndicators.Count; ++index)
        this.WaitingIndicators[index].Measure(availableSize);
      if (this.WaitingIndicators.Count == 0)
        return this.TextElement.DesiredSize;
      return this.WaitingIndicators[0].DesiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.WaitingIndicators.Count > 0)
      {
        if (this.WaitingStyle == WaitingBarStyles.Indeterminate)
          this.ArrangeIndeterminateIndicatorElements(clientRectangle);
        else if (this.WaitingStyle == WaitingBarStyles.Throbber)
          this.WaitingIndicators[0].Arrange(this.GetThrobberIndicatorElementFinalSize(this.WaitingIndicators[0] as WaitingBarIndicatorElement, clientRectangle));
        else if (this.WaitingStyle == WaitingBarStyles.Dash)
        {
          this.SeparatorElement.Arrange(this.GetDashElementFinalSize(this.SeparatorElement, clientRectangle));
        }
        else
        {
          foreach (RadElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
            waitingIndicator.Arrange(clientRectangle);
        }
        this.SetElementsVisibility(this.WaitingStyle);
      }
      this.TextElement.Arrange(clientRectangle);
      return finalSize;
    }

    protected float AddIndicatorStep(float step, int index)
    {
      if (index == 0)
        return 0.0f;
      if (this.IsVertical())
        return (float) (this.WaitingIndicators.Count - index) * (step + this.WaitingIndicators[0].DesiredSize.Height);
      return (float) (this.WaitingIndicators.Count - index) * (step + this.WaitingIndicators[0].DesiredSize.Width);
    }

    protected void ArrangeIndeterminateIndicatorElements(RectangleF clientRect)
    {
      this.UpdateOffset(clientRect);
      float indicatorStep = this.CalculateIndicatorStep(clientRect);
      for (int index = 0; index < this.WaitingIndicators.Count; ++index)
      {
        RectangleF rectangleF = this.MoveIndicatorElement(this.WaitingIndicators[index] as WaitingBarIndicatorElement, clientRect, this.WaitingDirection);
        float x = rectangleF.X;
        float y = rectangleF.Y;
        if (this.WaitingDirection == ProgressOrientation.Right && !this.RightToLeft || this.WaitingDirection == ProgressOrientation.Left && this.RightToLeft)
        {
          x += this.AddIndicatorStep(indicatorStep, index);
          if ((double) x > (double) clientRect.Width * 2.0 - (double) this.WaitingIndicators[0].DesiredSize.Width)
            x -= clientRect.Width * 2f;
          if (this.waitingFirstRun && index != 0 && (double) x > (double) this.offset)
            x = -this.WaitingIndicators[0].DesiredSize.Width;
        }
        if (this.WaitingDirection == ProgressOrientation.Bottom)
        {
          y += this.AddIndicatorStep(indicatorStep, index);
          if ((double) y > (double) clientRect.Height * 2.0 - (double) this.WaitingIndicators[0].DesiredSize.Height)
            y -= clientRect.Height * 2f;
          if (this.waitingFirstRun && index != 0 && (double) y > (double) this.offset)
            y = -this.WaitingIndicators[0].DesiredSize.Height;
        }
        if (this.WaitingDirection == ProgressOrientation.Left && !this.RightToLeft || this.WaitingDirection == ProgressOrientation.Right && this.RightToLeft)
        {
          x -= this.AddIndicatorStep(indicatorStep, index);
          if ((double) x < -(double) clientRect.Width)
            x += clientRect.Width * 2f;
          if (this.waitingFirstRun && index != 0 && (double) x < (double) clientRect.Width - (double) this.WaitingIndicators[0].DesiredSize.Width - (double) this.offset)
            x = clientRect.Width;
        }
        if (this.WaitingDirection == ProgressOrientation.Top)
        {
          y -= this.AddIndicatorStep(indicatorStep, index);
          if ((double) y < -(double) clientRect.Height)
            y += clientRect.Height * 2f;
          if (this.waitingFirstRun && index != 0 && (double) y < (double) clientRect.Height - (double) this.WaitingIndicators[0].DesiredSize.Height - (double) this.offset)
            y = -this.WaitingIndicators[0].DesiredSize.Height;
        }
        this.WaitingIndicators[index].Arrange(new RectangleF(new PointF(x, y), this.WaitingIndicators[index].DesiredSize));
      }
    }

    protected float CalculateIndicatorStep(RectangleF clientRect)
    {
      if (this.IsVertical())
        return (float) ((double) clientRect.Height * 2.0 - (double) this.WaitingIndicators[0].DesiredSize.Height * (double) this.WaitingIndicators.Count) / (float) this.WaitingIndicators.Count;
      return (float) ((double) clientRect.Width * 2.0 - (double) this.WaitingIndicators[0].DesiredSize.Width * (double) this.WaitingIndicators.Count) / (float) this.WaitingIndicators.Count;
    }

    protected RectangleF GetThrobberIndicatorElementFinalSize(
      WaitingBarIndicatorElement element,
      RectangleF clientRect)
    {
      bool flag = this.IsVertical();
      if (!flag && (double) this.offset >= (double) clientRect.Width - (double) element.DesiredSize.Width || flag && (double) this.offset >= (double) clientRect.Height - (double) element.DesiredSize.Height)
      {
        this.offset = 0.0f;
        this.isBackwards = !this.isBackwards;
      }
      if (!this.isBackwards)
        return this.MoveIndicatorElement(element, clientRect, this.WaitingDirection);
      ProgressOrientation reversedDirection = this.GetReversedDirection(this.WaitingDirection);
      return this.MoveIndicatorElement(element, clientRect, reversedDirection);
    }

    protected RectangleF GetDashElementFinalSize(
      WaitingBarSeparatorElement element,
      RectangleF clientRect)
    {
      if (!element.Dash)
        return RectangleF.Empty;
      int num = (element.StepWidth + element.SeparatorWidth) * 2;
      float x = clientRect.X;
      float y = clientRect.Y;
      float width = clientRect.Width;
      float height = clientRect.Height;
      if ((double) this.offset >= (double) (num / 2))
      {
        this.offset = 0.0f;
        return this.SetDashInitialPosition(element, clientRect);
      }
      if (this.WaitingDirection == ProgressOrientation.Right && !this.RightToLeft || this.WaitingDirection == ProgressOrientation.Left && this.RightToLeft)
      {
        x += this.offset - (float) num;
        width += (float) num;
        --y;
        ++height;
      }
      if (this.WaitingDirection == ProgressOrientation.Left && !this.RightToLeft || this.WaitingDirection == ProgressOrientation.Right && this.RightToLeft)
      {
        x -= this.offset + (float) (num / 2);
        width += (float) num;
        --y;
        ++height;
      }
      if (this.WaitingDirection == ProgressOrientation.Top)
      {
        y -= this.offset + (float) (num / 2);
        height += (float) num;
        --x;
        ++width;
      }
      if (this.WaitingDirection == ProgressOrientation.Bottom)
      {
        y += this.offset - (float) num;
        height += (float) num;
        --x;
        ++width;
      }
      return new RectangleF(new PointF(x, y), new SizeF(width, height));
    }

    protected RectangleF MoveIndicatorElement(
      WaitingBarIndicatorElement element,
      RectangleF clientRect,
      ProgressOrientation waitingDirection)
    {
      float x = clientRect.X;
      float y = clientRect.Y;
      if (waitingDirection == ProgressOrientation.Right && !this.RightToLeft || waitingDirection == ProgressOrientation.Left && this.RightToLeft)
      {
        x += this.offset;
        y += (float) (((double) clientRect.Height - (double) element.DesiredSize.Height) / 2.0);
      }
      if (waitingDirection == ProgressOrientation.Left && !this.RightToLeft || waitingDirection == ProgressOrientation.Right && this.RightToLeft)
      {
        x += clientRect.Width - element.DesiredSize.Width - this.offset;
        y += (float) (((double) clientRect.Height - (double) element.DesiredSize.Height) / 2.0);
      }
      if (waitingDirection == ProgressOrientation.Top)
      {
        y += clientRect.Height - element.DesiredSize.Height - this.offset;
        x += (float) (((double) clientRect.Width - (double) element.DesiredSize.Width) / 2.0);
      }
      if (waitingDirection == ProgressOrientation.Bottom)
      {
        y += this.offset;
        x += (float) (((double) clientRect.Width - (double) element.DesiredSize.Width) / 2.0);
      }
      return new RectangleF(new PointF(x, y), element.DesiredSize);
    }

    protected override void SetClipping(Graphics rawGraphics)
    {
      if (this.Shape != null)
      {
        GraphicsPath elementShape = this.Shape.GetElementShape((RadElement) this);
        rawGraphics.SetClip(elementShape, CombineMode.Intersect);
      }
      else
        base.SetClipping(rawGraphics);
    }

    protected void SetElementsVisibility(WaitingBarStyles style)
    {
      switch (style)
      {
        case WaitingBarStyles.Indeterminate:
          this.SetIndicatorsVisibility(ElementVisibility.Visible);
          int num1 = (int) this.SeparatorElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
          break;
        case WaitingBarStyles.Throbber:
          this.SetIndicatorsVisibility(ElementVisibility.Collapsed);
          int num2 = (int) this.SeparatorElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
          break;
        case WaitingBarStyles.Dash:
          this.SetIndicatorsVisibility(ElementVisibility.Collapsed);
          int num3 = (int) this.SeparatorElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
          break;
        default:
          int num4 = (int) this.SeparatorElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
          break;
      }
    }

    protected void SetIndicatorsVisibility(ElementVisibility visibility)
    {
      int index = 0;
      if (this.WaitingStyle == WaitingBarStyles.Throbber)
      {
        int num1 = (int) this.WaitingIndicators[index++].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      for (; index < this.WaitingIndicators.Count; ++index)
      {
        int num2 = (int) this.WaitingIndicators[index].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) visibility);
      }
    }

    protected RectangleF SetDashInitialPosition(
      WaitingBarSeparatorElement element,
      RectangleF clientRect)
    {
      int num = element.StepWidth + element.SeparatorWidth;
      if (this.IsVertical())
        return new RectangleF(new PointF(clientRect.X - 1f, clientRect.Y - (float) num), new SizeF(clientRect.Width + 1f, clientRect.Height + (float) (num * 2)));
      return new RectangleF(new PointF(clientRect.X - (float) num, clientRect.Y - 1f), new SizeF(clientRect.Width + (float) (num * 2), clientRect.Height + 1f));
    }

    protected void UpdateOffset(RectangleF clientRect)
    {
      bool flag = this.IsVertical();
      if ((flag || (double) this.offset < (double) clientRect.Width * 2.0) && (!flag || (double) this.offset < (double) clientRect.Height * 2.0))
        return;
      if (this.waitingFirstRun)
        this.waitingFirstRun = false;
      this.offset = 0.0f;
    }
  }
}
