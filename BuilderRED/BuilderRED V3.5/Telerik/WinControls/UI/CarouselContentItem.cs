// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselContentItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class CarouselContentItem : VisualElement
  {
    public ReflectionPrimitive reflectionPrimitive;
    private RadElement hostedItem;
    internal bool paintingReflection;
    private RadCarouselElement owner;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
    }

    public CarouselContentItem()
    {
    }

    public CarouselContentItem(RadElement hostedItem)
    {
      this.HostedItem = hostedItem;
    }

    public RadElement HostedItem
    {
      get
      {
        return this.hostedItem;
      }
      set
      {
        if (value == null)
          throw new ArgumentException("HostedItem cannot be null!");
        if (this.hostedItem == value)
          return;
        if (this.hostedItem != null && this.hostedItem.Parent == this)
          this.Children.Remove(this.hostedItem);
        this.hostedItem = value;
        this.Children.Add(this.hostedItem);
        this.reflectionPrimitive.OwnerElement = this.hostedItem;
      }
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      if (this.reflectionPrimitive != null && !this.reflectionPrimitive.IsDisposed && (!this.reflectionPrimitive.IsDisposing && changeOperation == ItemsChangeOperation.Removing) && object.ReferenceEquals((object) child, (object) this.reflectionPrimitive.OwnerElement))
        this.reflectionPrimitive.OwnerElement = (RadElement) null;
      base.OnChildrenChanged(child, changeOperation);
    }

    public RadCarouselElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public Point Center
    {
      get
      {
        if (this.HostedItem == null)
          return new Point(this.Size.Width / 2, this.Size.Height / 2);
        return new Point(this.HostedItem.BoundingRectangle.Width / 2, this.HostedItem.BoundingRectangle.Height / 2);
      }
    }

    internal void SetOwner(RadCarouselElement owner)
    {
      this.owner = owner;
      this.reflectionPrimitive.ItemReflectionPercentage = this.Owner.ItemReflectionPercentage;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.reflectionPrimitive = new ReflectionPrimitive(this.HostedItem);
      this.Children.Add((RadElement) this.reflectionPrimitive);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.HostedItem.Measure(availableSize);
      this.reflectionPrimitive.Measure(availableSize);
      SizeF desiredSize = this.HostedItem.DesiredSize;
      return new SizeF(desiredSize.Width, (float) ((double) desiredSize.Height * 2.0 + 1.0));
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.HostedItem.Arrange(new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height / 2f));
      this.reflectionPrimitive.Arrange(new RectangleF(0.0f, (float) ((double) finalSize.Height / 2.0 + 2.0), finalSize.Width, finalSize.Height / 3f));
      return finalSize;
    }

    public override bool Equals(object obj)
    {
      if (base.Equals(obj))
        return true;
      if (obj != null)
        return object.Equals((object) this.hostedItem, obj);
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected override void DisposeManagedResources()
    {
      if (this.ElementTree != null)
        ((RadControl) this.ElementTree.Control).ElementInvalidated -= new EventHandler(this.reflectionPrimitive.CarouselContentItem_ElementInvalidated);
      base.DisposeManagedResources();
    }
  }
}
