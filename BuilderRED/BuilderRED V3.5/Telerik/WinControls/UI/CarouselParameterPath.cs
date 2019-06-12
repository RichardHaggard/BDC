// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselParameterPath
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class CarouselParameterPath : ICarouselPath, INotifyPropertyChanged
  {
    private double zScale = 500.0;
    protected double zIndexScale = 1000.0;
    protected ValueMapper ranges = new ValueMapper();
    private bool enableRelativePath = true;
    protected bool isModified;
    protected bool zIndexFromPathValue;

    public CarouselParameterPath()
    {
      this.ranges.Add((ValueMapper.MapperRange) new ValueMapper.Value(double.NegativeInfinity), (ValueMapper.MapperRange) new ValueMapper.Range(true, 0.0, 1.0, false, 0.0));
      this.ranges.Add((ValueMapper.MapperRange) new ValueMapper.Range(true, 0.0, 1.0, true), (ValueMapper.MapperRange) new ValueMapper.Range(true, 1.0, 6.0, true));
      this.ranges.Add((ValueMapper.MapperRange) new ValueMapper.Value(double.PositiveInfinity), (ValueMapper.MapperRange) new ValueMapper.Range(false, 6.0, 7.0, true, 7.0));
      this.ranges.Add((ValueMapper.MapperRange) new ValueMapper.Value(double.NaN), (ValueMapper.MapperRange) new ValueMapper.Range(false, 7.0, 8.0, true));
    }

    public double ZScale
    {
      get
      {
        return this.zScale;
      }
      set
      {
        if (value == this.zScale)
          return;
        this.zScale = value;
        this.OnPropertyChanged(nameof (ZScale));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public abstract object InitialPathPoint { get; }

    public abstract object FinalPathPoint { get; }

    public virtual bool IsPathClosed
    {
      get
      {
        return object.Equals(this.InitialPathPoint, this.FinalPathPoint);
      }
    }

    public object Evaluate(VisualElement element, CarouselPathAnimationData data, object value)
    {
      double num = (double) value;
      int index = data.SpecialHandling ? 1 : this.ranges.GetIndexFromTarget(num);
      switch (index)
      {
        case -1:
          return (object) null;
        case 1:
          double unit = this.ranges[index].target.MapToUnit(num);
          return this.EvaluateByParameter(element, data, unit);
        default:
          return (object) null;
      }
    }

    public abstract object EvaluateByParameter(
      VisualElement element,
      CarouselPathAnimationData data,
      double value);

    public void CreateAnimation(
      VisualElement element,
      CarouselPathAnimationData data,
      int frames,
      int delay)
    {
      double num1;
      if (data.SpecialHandling)
      {
        double d = (double) element.GetValue(CarouselItemsContainer.CarouselLocationProperty);
        double num2 = double.IsNaN(d) ? 0.0 : d;
        ValueMapper.MapperRange target = this.ranges.GetTarget(data.To.Value);
        num1 = !data.From.HasValue || double.IsNegativeInfinity(data.From.Value) ? num2 - target.Length : num2 + target.Length;
        data.From = new double?();
      }
      else if (!data.From.HasValue)
      {
        double d = (double) element.GetValue(CarouselItemsContainer.CarouselLocationProperty);
        num1 = double.IsNaN(d) ? 0.0 : d;
      }
      else
      {
        num1 = this.ranges.MapInTarget(data.From.Value);
        data.From = new double?();
      }
      if (!data.To.HasValue)
        data.To = new double?(double.PositiveInfinity);
      double num3 = this.ranges.MapInTarget(data.To.Value);
      if (num1 != num3)
        new PropertySetting(CarouselItemsContainer.CarouselLocationProperty, (object) num1).ApplyValue((RadObject) element);
      AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(CarouselItemsContainer.CarouselLocationProperty, (object) num1, (object) num3, frames, delay);
      data.CurrentAnimation = animatedPropertySetting;
    }

    public void ApplyToElement(
      VisualElement element,
      CarouselPathAnimationData data,
      double value,
      object evaluated,
      Animations flags,
      Size controlSize)
    {
      int num1;
      double num2;
      if (data.SpecialHandling)
      {
        num1 = 1;
        num2 = 0.0;
      }
      else
      {
        num1 = this.ranges.GetIndexFromTarget(value);
        num2 = this.ranges.MapTargetToUnit(value);
      }
      switch (num1)
      {
        case 0:
          this.ApplyValueIn((RadElement) element, num2, flags, controlSize);
          break;
        case 1:
          this.ApplyValuePath(element, (Point3D) evaluated, flags, controlSize);
          break;
        case 2:
        case 3:
          this.ApplyValueOut((RadElement) element, num2, flags, controlSize);
          break;
      }
    }

    public double PositionsCount(int itemsCount)
    {
      return Math.Max(0.0, this.IsClosedPath ? (double) itemsCount + 1.0 : (double) itemsCount);
    }

    public double Step(int itemsCount)
    {
      return 1.0 / Math.Max(1.0, this.PositionsCount(itemsCount));
    }

    private SizeF ElementScale(Point3D value, double scale)
    {
      float num = (float) Math.Max(0.125, Math.Min(2.0, (1.0 + value.Z / this.zScale) * scale));
      return new SizeF(num, num);
    }

    private void ApplyValueIn(
      RadElement element,
      double value,
      Animations flags,
      Size controlSize)
    {
      float num = (float) (0.5 + value * 0.5);
      Point3D pos = this.ConvertPointIfRelative((Point3D) this.InitialPathPoint, controlSize);
      CarouselParameterPath.SetElementBounds(element, (Point) pos, flags);
      CarouselParameterPath.SetElementOpacity(element, value, flags);
      CarouselParameterPath.SetElementScale(element, this.ElementScale(pos, (double) num), flags);
      this.SetElementZIndex(element, pos, flags);
    }

    private void ApplyValueOut(
      RadElement element,
      double value,
      Animations flags,
      Size controlSize)
    {
      float num = 1f - (float) (value * 0.5);
      Point3D pos = this.ConvertPointIfRelative((Point3D) this.FinalPathPoint, controlSize);
      CarouselParameterPath.SetElementScale(element, this.ElementScale(pos, (double) num), flags);
      CarouselParameterPath.SetElementBounds(element, (Point) pos, flags);
      CarouselParameterPath.SetElementOpacity(element, 1.0 - value, flags);
      this.SetElementZIndex(element, pos, flags);
    }

    private void ApplyValuePath(
      VisualElement element,
      Point3D newPoint,
      Animations flags,
      Size controlSize)
    {
      if (newPoint == null)
        return;
      newPoint = this.ConvertPointIfRelative(newPoint, controlSize);
      CarouselParameterPath.SetElementBounds((RadElement) element, (Point) newPoint, flags);
      double num = 1.0;
      CarouselContentItem carouselContentItem = (CarouselContentItem) element;
      switch (carouselContentItem.Owner.ItemsContainer.OpacityChangeCondition)
      {
        case OpacityChangeConditions.None:
          num = 1.0;
          break;
        case OpacityChangeConditions.SelectedIndex:
          num = 1.0 - Math.Abs((double) (carouselContentItem.Owner.ItemsContainer.Items.IndexOf((RadItem) carouselContentItem.HostedItem) - carouselContentItem.Owner.SelectedIndex) / (double) carouselContentItem.Owner.ItemsContainer.VisibleItemCount);
          break;
        case OpacityChangeConditions.ZIndex:
          if (((Point3D) this.FinalPathPoint).Z != 0.0)
          {
            num = 1.0 - newPoint.Z / ((Point3D) this.FinalPathPoint).Z;
            break;
          }
          break;
      }
      if (num < carouselContentItem.Owner.ItemsContainer.MinFadeOpacity)
        num = carouselContentItem.Owner.ItemsContainer.MinFadeOpacity;
      else if (num > 1.0)
        num = 1.0;
      CarouselParameterPath.SetElementOpacity((RadElement) element, num, flags);
      CarouselParameterPath.SetElementScale((RadElement) element, this.ElementScale(newPoint, 1.0), flags);
      this.SetElementZIndex((RadElement) element, newPoint, flags);
    }

    private static void SetElementBounds(RadElement element, Point value, Animations flags)
    {
      if ((flags & Animations.Location) == Animations.None || element == null)
        return;
      Point location = value;
      Point center = ((CarouselContentItem) element).Center;
      location.Offset(-center.X, -center.Y);
      element.Bounds = new Rectangle(location, element.Bounds.Size);
      new PropertySetting(RadElement.BoundsProperty, (object) new Rectangle(value, element.Bounds.Size)).ApplyValue((RadObject) element);
    }

    private static void SetElementOpacity(RadElement element, double value, Animations flags)
    {
      if ((flags & Animations.Opacity) == Animations.None || element == null)
        return;
      new PropertySetting(VisualElement.OpacityProperty, (object) value).ApplyValue((RadObject) element);
    }

    private static void SetElementScale(RadElement element, SizeF value, Animations flags)
    {
      if (flags == Animations.None || element == null)
        return;
      RadElement hostedItem = ((CarouselContentItem) element).HostedItem;
      new PropertySetting(RadElement.ScaleTransformProperty, (object) value).ApplyValue((RadObject) hostedItem);
    }

    private void SetElementZIndex(RadElement element, Point3D pos, Animations flags)
    {
      if (!this.zIndexFromPathValue)
        return;
      int num = (int) (pos.Z * this.zIndexScale);
      new PropertySetting(RadElement.ZIndexProperty, (object) num).ApplyValue((RadObject) element);
    }

    public void InitializeItem(VisualElement element, object flags)
    {
      Animations flags1 = (Animations) flags;
      CarouselParameterPath.SetElementBounds((RadElement) element, (Point) ((Point3D) this.InitialPathPoint), Animations.Location);
      CarouselParameterPath.SetElementOpacity((RadElement) element, 0.0, flags1);
      CarouselParameterPath.SetElementScale((RadElement) element, this.ElementScale((Point3D) this.InitialPathPoint, 0.5), flags1);
      this.SetElementZIndex((RadElement) element, (Point3D) this.InitialPathPoint, flags1);
    }

    public bool ZindexFromPath()
    {
      return this.zIndexFromPathValue;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool EnableRelativePath
    {
      get
      {
        return this.enableRelativePath;
      }
      set
      {
        if (this.enableRelativePath == value)
          return;
        this.enableRelativePath = value;
      }
    }

    protected virtual bool IsClosedPath
    {
      get
      {
        return true;
      }
    }

    public Point3D ConvertFromRelativePath(Point3D point, Size ownerSize)
    {
      return new Point3D(point.X * (double) ownerSize.Width / 100.0, point.Y * (double) ownerSize.Height / 100.0, point.Z);
    }

    public Point3D ConvertToRelativePath(Point3D point, Size ownerSize)
    {
      return new Point3D(point.X * 100.0 / (double) ownerSize.Width, point.Y * 100.0 / (double) ownerSize.Height, point.Z);
    }

    private Point3D ConvertPointIfRelative(Point3D point, Size ownerSize)
    {
      if (this.EnableRelativePath)
        return new Point3D(point.X * (double) ownerSize.Width / 100.0, point.Y * (double) ownerSize.Height / 100.0, point.Z);
      return point;
    }
  }
}
