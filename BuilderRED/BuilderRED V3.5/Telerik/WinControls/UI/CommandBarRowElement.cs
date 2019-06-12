// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadCommandBarToolstripsHolderDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class CommandBarRowElement : RadCommandBarVisualElement
  {
    private float cachedSumOfDesiredSpaces = -1f;
    private const int NOT_SET = -1;
    private CommandBarStripElementCollection strips;
    private RadCommandBarElement owner;

    public CommandBarRowElement()
    {
      this.MinSize = new Size(25, 25);
      if (this.Site == null)
        return;
      this.MinSize = new Size(20, 20);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.strips = new CommandBarStripElementCollection((RadElement) this);
      this.strips.ItemTypes = new System.Type[1]
      {
        typeof (CommandBarStripElement)
      };
      this.strips.ItemsChanged += new CommandBarStripElementCollectionItemChangedDelegate(this.strips_ItemsChanged);
      this.SetOrientationCore(this.Orientation);
    }

    public override string Text
    {
      get
      {
        return "";
      }
      set
      {
        base.Text = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual RadCommandBarElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        this.owner = value;
        if (this.owner == null)
          return;
        this.Orientation = this.owner.Orientation;
        foreach (CommandBarStripElement strip in this.Strips)
          this.owner.StripInfoHolder.AddStripInfo(strip);
      }
    }

    [Browsable(false)]
    [RadPropertyDefaultValue("Orientation", typeof (CommandBarRowElement))]
    [Category("Behavior")]
    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        if (this.Orientation == value || this.OnOrientationChanging(new CancelEventArgs()))
          return;
        this.cachedOrientation = value;
        this.SetOrientationCore(value);
        this.InvalidateMeasure(true);
        this.OnOrientationChanged(new EventArgs());
      }
    }

    public event CancelEventHandler BeginDragging;

    public event MouseEventHandler Dragging;

    public event EventHandler EndDragging;

    public event EventHandler OrientationChanged;

    public event CancelEventHandler OrientationChanging;

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (args.RoutedEvent == RadCommandBarGrip.BeginDraggingEvent)
      {
        CancelEventArgs originalEventArgs = (CancelEventArgs) args.OriginalEventArgs;
        this.OnBeginDragging((object) sender, originalEventArgs);
        args.Canceled = originalEventArgs.Cancel;
      }
      if (args.RoutedEvent == RadCommandBarGrip.EndDraggingEvent)
      {
        EventArgs originalEventArgs = args.OriginalEventArgs;
        this.OnEndDragging((object) sender, originalEventArgs);
      }
      if (args.RoutedEvent != RadCommandBarGrip.DraggingEvent)
        return;
      MouseEventArgs originalEventArgs1 = (MouseEventArgs) args.OriginalEventArgs;
      this.OnDragging((object) sender, originalEventArgs1);
    }

    protected virtual void OnBeginDragging(object sender, CancelEventArgs args)
    {
      if (this.BeginDragging == null)
        return;
      this.BeginDragging(sender, args);
    }

    protected virtual void OnEndDragging(object sender, EventArgs args)
    {
      if (this.EndDragging == null)
        return;
      this.EndDragging(sender, args);
    }

    protected virtual void OnDragging(object sender, MouseEventArgs args)
    {
      if (this.Dragging == null)
        return;
      this.Dragging(sender, args);
    }

    protected virtual void OnOrientationChanged(EventArgs e)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, e);
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs e)
    {
      if (this.OrientationChanging == null)
        return false;
      this.OrientationChanging((object) this, e);
      return e.Cancel;
    }

    private void strips_ItemsChanged(
      CommandBarStripElementCollection changed,
      CommandBarStripElement target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Set)
      {
        target.Orientation = this.Orientation;
        if (this.owner != null)
          this.owner.StripInfoHolder.AddStripInfo(target);
      }
      if ((operation == ItemsChangeOperation.Removed || operation == ItemsChangeOperation.Setting) && this.owner != null)
        this.owner.StripInfoHolder.RemoveStripInfo(target);
      if (operation == ItemsChangeOperation.Clearing && this.owner != null)
      {
        foreach (CommandBarStripElement strip in this.Strips)
          this.owner.StripInfoHolder.RemoveStripInfo(strip);
      }
      if (this.Parent == null)
        return;
      this.Parent.Invalidate();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered = this.Orientation == Orientation.Horizontal ? this.GetChildrenOrdered(new Comparison<RadCommandBarStripPanelLayoutInfo>(CommandBarRowElement.CompareCommandBarStripElementByX), availableSize, true) : this.GetChildrenOrdered(new Comparison<RadCommandBarStripPanelLayoutInfo>(CommandBarRowElement.CompareCommandBarStripElementByY), availableSize, true);
      float num = this.Orientation == Orientation.Horizontal ? availableSize.Width : availableSize.Height;
      this.cachedSumOfDesiredSpaces = this.GetSumOfDesiredSpace(elementsOrdered, availableSize);
      SizeF empty = SizeF.Empty;
      SizeF sizeF = (double) this.cachedSumOfDesiredSpaces <= (double) num || this.Site != null ? this.MeasureElementWithFreeSpace(elementsOrdered, availableSize) : this.MeasureElementsWithoutFreeSpace(elementsOrdered, availableSize);
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = sizeF.Height;
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = sizeF.Width;
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      int count = this.Strips.Count;
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered = this.Orientation != Orientation.Horizontal ? this.GetChildrenOrdered(new Comparison<RadCommandBarStripPanelLayoutInfo>(CommandBarRowElement.CompareCommandBarStripElementByY), SizeF.Empty, false) : this.GetChildrenOrdered(new Comparison<RadCommandBarStripPanelLayoutInfo>(CommandBarRowElement.CompareCommandBarStripElementByX), SizeF.Empty, false);
      this.ArrangeElements(elementsOrdered, finalSize);
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo layoutInfo = elementsOrdered[index];
        this.AdjustDesiredLocationIfEmpty(layoutInfo);
        if (this.RightToLeft && this.Orientation == Orientation.Horizontal)
          layoutInfo.ArrangeRectangle.X = finalSize.Width - layoutInfo.ArrangeRectangle.X - layoutInfo.ArrangeRectangle.Width;
        layoutInfo.commandBarStripElement.Arrange(layoutInfo.ArrangeRectangle);
      }
      return finalSize;
    }

    private SizeF MeasureElementWithFreeSpace(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF availableSize)
    {
      int count = this.Strips.Count;
      SizeF empty = SizeF.Empty;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo = elementsOrdered[index];
        if (!stripPanelLayoutInfo.commandBarStripElement.VisibleInCommandBar && this.Site == null)
          stripPanelLayoutInfo.commandBarStripElement.Measure(SizeF.Empty);
        else
          stripPanelLayoutInfo.commandBarStripElement.Measure(availableSize);
        empty.Width = Math.Max(empty.Width, stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Width);
        empty.Height = Math.Max(empty.Height, stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Height);
      }
      return empty;
    }

    private SizeF MeasureElementsWithoutFreeSpace(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF availableSize)
    {
      int count = this.Strips.Count;
      float num1 = this.Orientation == Orientation.Horizontal ? availableSize.Width : availableSize.Height;
      SizeF empty = SizeF.Empty;
      float num2 = 0.0f;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo1 = elementsOrdered[index];
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo2 = index + 1 < count ? elementsOrdered[index + 1] : (RadCommandBarStripPanelLayoutInfo) null;
        if (!stripPanelLayoutInfo1.commandBarStripElement.VisibleInCommandBar && this.Site == null)
        {
          stripPanelLayoutInfo1.commandBarStripElement.Measure(SizeF.Empty);
        }
        else
        {
          float num3 = num1;
          if (stripPanelLayoutInfo2 != null)
            num3 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo2.DesiredLocation.X : stripPanelLayoutInfo2.DesiredLocation.Y;
          float num4 = num1 - num2;
          float val1_1 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo1.ExpectedDesiredSize.Width : stripPanelLayoutInfo1.ExpectedDesiredSize.Height;
          float num5 = stripPanelLayoutInfo1.DesiredSpaceToEnd - val1_1;
          float val2_1 = this.Orientation == Orientation.Horizontal ? (float) stripPanelLayoutInfo1.commandBarStripElement.MinSize.Width : (float) stripPanelLayoutInfo1.commandBarStripElement.MinSize.Height;
          float val1_2 = Math.Min(val1_1, num3 - num2);
          if ((double) num5 < (double) num4 - (double) val1_2)
          {
            float val2_2 = Math.Min(val1_1, val1_2 + num4 - val1_2 - num5);
            val1_2 = Math.Max(val1_2, val2_2);
          }
          if (stripPanelLayoutInfo2 != null && (double) stripPanelLayoutInfo2.MinSpaceToEnd > (double) num4 - (double) val1_2)
            val1_2 = Math.Min(val1_2, val1_2 - (stripPanelLayoutInfo2.MinSpaceToEnd - num4 + val1_2));
          float num6 = Math.Max(val1_2, val2_1);
          SizeF availableSize1 = this.Orientation == Orientation.Horizontal ? new SizeF(num6, availableSize.Height) : new SizeF(availableSize.Width, num6);
          stripPanelLayoutInfo1.commandBarStripElement.Measure(availableSize1);
          float num7 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo1.commandBarStripElement.DesiredSize.Width : stripPanelLayoutInfo1.commandBarStripElement.DesiredSize.Height;
          num2 += num7;
          if (this.Orientation == Orientation.Horizontal)
          {
            empty.Width += num7;
            empty.Height = Math.Max(empty.Height, stripPanelLayoutInfo1.commandBarStripElement.DesiredSize.Height);
          }
          else
          {
            empty.Height += num7;
            empty.Width = Math.Max(empty.Width, stripPanelLayoutInfo1.commandBarStripElement.DesiredSize.Width);
          }
        }
      }
      return empty;
    }

    private float GetSumOfDesiredSpace(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF availableSize)
    {
      float num = 0.0f;
      foreach (RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo in elementsOrdered)
      {
        if (this.Orientation == Orientation.Horizontal)
          num += stripPanelLayoutInfo.ExpectedDesiredSize.Width;
        else
          num += stripPanelLayoutInfo.ExpectedDesiredSize.Height;
      }
      return num;
    }

    private void AdjustDesiredLocationIfEmpty(RadCommandBarStripPanelLayoutInfo layoutInfo)
    {
      if (this.Site != null)
        return;
      if ((double) layoutInfo.commandBarStripElement.DesiredLocation.X < 0.0 || (double) layoutInfo.commandBarStripElement.DesiredLocation.Y < 0.0 || (float.IsInfinity(layoutInfo.commandBarStripElement.DesiredLocation.X) || float.IsInfinity(layoutInfo.commandBarStripElement.DesiredLocation.Y)))
        layoutInfo.commandBarStripElement.cachedDesiredLocation = new PointF(-1f, -1f);
      if (!(layoutInfo.commandBarStripElement.DesiredLocation == new PointF(-1f, -1f)) && ((double) layoutInfo.commandBarStripElement.DesiredLocation.X != 0.0 || this.Orientation != Orientation.Horizontal) && ((double) layoutInfo.commandBarStripElement.DesiredLocation.Y != 0.0 || this.Orientation != Orientation.Vertical))
        return;
      layoutInfo.commandBarStripElement.cachedDesiredLocation = layoutInfo.ArrangeRectangle.Location;
    }

    protected internal void MoveCommandStripInOtherLine(CommandBarStripElement currentElement)
    {
      if (this.owner == null)
        return;
      if (this.Orientation == Orientation.Horizontal)
      {
        if ((double) currentElement.DesiredLocation.Y < (double) (currentElement.ControlBoundingRectangle.Top - SystemInformation.DragSize.Height))
        {
          this.ResetDesiredLocation(currentElement);
          this.owner.MoveToUpperLine(currentElement, this);
          this.Invalidate();
          return;
        }
        if ((double) currentElement.DesiredLocation.Y > (double) (currentElement.ControlBoundingRectangle.Bottom + SystemInformation.DragSize.Height))
        {
          this.ResetDesiredLocation(currentElement);
          this.owner.MoveToDownerLine(currentElement, this);
          this.Invalidate();
          return;
        }
      }
      if (this.Orientation != Orientation.Vertical)
        return;
      if ((double) currentElement.DesiredLocation.X < (double) (currentElement.ControlBoundingRectangle.Left - SystemInformation.DragSize.Height))
      {
        this.ResetDesiredLocation(currentElement);
        this.owner.MoveToUpperLine(currentElement, this);
        this.Invalidate();
      }
      else
      {
        if ((double) currentElement.DesiredLocation.X <= (double) (currentElement.ControlBoundingRectangle.Right + SystemInformation.DragSize.Height))
          return;
        this.ResetDesiredLocation(currentElement);
        this.owner.MoveToDownerLine(currentElement, this);
        this.Invalidate();
      }
    }

    private void ResetDesiredLocation(CommandBarStripElement currentElement)
    {
    }

    private void ArrangeElements(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF finalSize)
    {
      if ((double) this.cachedSumOfDesiredSpaces <= (this.Orientation == Orientation.Horizontal ? (double) finalSize.Width : (double) finalSize.Height) && this.Site == null)
      {
        this.ArrangeElementsWithFreeSpace(elementsOrdered, finalSize);
        this.ArrangeStretchedElements(elementsOrdered, finalSize);
      }
      else
        this.ArrangeElementsWithoutFreeSpace(elementsOrdered, finalSize);
    }

    private void ArrangeElementsWithoutFreeSpace(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF finalSize)
    {
      int count = elementsOrdered.Count;
      float num1 = 0.0f;
      float num2 = this.Orientation == Orientation.Horizontal ? finalSize.Width : finalSize.Height;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo = elementsOrdered[index];
        float num3 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Width : stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Height;
        PointF location = this.Orientation == Orientation.Horizontal ? new PointF(num1, 0.0f) : new PointF(0.0f, num1);
        SizeF size = this.Orientation == Orientation.Horizontal ? new SizeF(num3, finalSize.Height) : new SizeF(finalSize.Width, num3);
        if (index + 1 == count)
        {
          if (this.Orientation == Orientation.Horizontal)
            size.Width = num2 - num1;
          else
            size.Height = num2 - num1;
        }
        stripPanelLayoutInfo.ArrangeRectangle = new RectangleF(location, size);
        num1 += num3;
      }
    }

    private void ArrangeStretchedElements(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF finalSize)
    {
      float num1 = this.Orientation == Orientation.Horizontal ? finalSize.Width : finalSize.Height;
      int count = elementsOrdered.Count;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo1 = index - 1 >= 0 ? elementsOrdered[index - 1] : (RadCommandBarStripPanelLayoutInfo) null;
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo2 = elementsOrdered[index];
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo3 = index + 1 < count ? elementsOrdered[index + 1] : (RadCommandBarStripPanelLayoutInfo) null;
        if (stripPanelLayoutInfo2.commandBarStripElement.VisibleInCommandBar && stripPanelLayoutInfo2.commandBarStripElement.Visibility != ElementVisibility.Collapsed && (this.Orientation == Orientation.Horizontal && stripPanelLayoutInfo2.commandBarStripElement.StretchHorizontally || this.Orientation == Orientation.Vertical && stripPanelLayoutInfo2.commandBarStripElement.StretchVertically))
        {
          float num2 = num1;
          if (stripPanelLayoutInfo3 != null)
            num2 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo3.ArrangeRectangle.X : stripPanelLayoutInfo3.ArrangeRectangle.Y;
          float num3 = 0.0f;
          if (stripPanelLayoutInfo1 != null)
            num3 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo1.ArrangeRectangle.X + stripPanelLayoutInfo1.ArrangeRectangle.Width : stripPanelLayoutInfo1.ArrangeRectangle.Y + stripPanelLayoutInfo1.ArrangeRectangle.Height;
          float num4 = num2 - num3;
          SizeF sizeF = this.Orientation == Orientation.Horizontal ? new SizeF(num4, finalSize.Height) : new SizeF(finalSize.Width, num4);
          PointF pointF = this.Orientation == Orientation.Horizontal ? new PointF(num3, stripPanelLayoutInfo2.ArrangeRectangle.Y) : new PointF(stripPanelLayoutInfo2.ArrangeRectangle.X, num3);
          stripPanelLayoutInfo2.ArrangeRectangle.Location = pointF;
          stripPanelLayoutInfo2.ArrangeRectangle.Size = sizeF;
        }
      }
    }

    private void ArrangeElementsWithFreeSpace(
      List<RadCommandBarStripPanelLayoutInfo> elementsOrdered,
      SizeF finalSize)
    {
      float num1 = this.Orientation == Orientation.Horizontal ? finalSize.Width : finalSize.Height;
      int count = elementsOrdered.Count;
      float num2 = 0.0f;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarStripPanelLayoutInfo stripPanelLayoutInfo = elementsOrdered[index];
        PointF desiredLocation = stripPanelLayoutInfo.DesiredLocation;
        float num3 = this.Orientation == Orientation.Horizontal ? stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Width : stripPanelLayoutInfo.commandBarStripElement.DesiredSize.Height;
        float num4 = Math.Max(this.Orientation == Orientation.Horizontal ? desiredLocation.X : desiredLocation.Y, num2);
        if ((double) stripPanelLayoutInfo.IntersectionSpaceToEnd > 0.0)
        {
          float num5 = Math.Min(num4 - num2, stripPanelLayoutInfo.IntersectionSpaceToEnd);
          num4 = Math.Max(num2, num4 - num5);
        }
        float num6 = num1 - num4 - stripPanelLayoutInfo.DesiredSpaceToEnd;
        if ((double) num6 < 0.0)
          num4 += num6;
        num2 = num4 + num3;
        PointF location = this.Orientation == Orientation.Horizontal ? new PointF(num4, 0.0f) : new PointF(0.0f, num4);
        SizeF size = this.Orientation == Orientation.Horizontal ? new SizeF(num3, finalSize.Height) : new SizeF(finalSize.Width, num3);
        stripPanelLayoutInfo.ArrangeRectangle = new RectangleF(location, size);
      }
    }

    private List<RadCommandBarStripPanelLayoutInfo> GetChildrenOrdered(
      Comparison<RadCommandBarStripPanelLayoutInfo> comparison,
      SizeF availableSize,
      bool calculateExpectedSize)
    {
      int count = this.Strips.Count;
      List<RadCommandBarStripPanelLayoutInfo> elements = new List<RadCommandBarStripPanelLayoutInfo>(count);
      for (int index = 0; index < count; ++index)
        elements.Add(new RadCommandBarStripPanelLayoutInfo(this.Strips[index]));
      if (this.Site == null)
        this.StableSort(elements, comparison);
      float num1 = 0.0f;
      float num2 = 0.0f;
      int num3 = -1;
      for (int index = count - 1; index >= 0; --index)
      {
        elements[index].ExpectedDesiredSize = calculateExpectedSize ? elements[index].commandBarStripElement.GetExpectedSize(availableSize) : elements[index].commandBarStripElement.DesiredSize;
        float num4 = this.Orientation == Orientation.Horizontal ? elements[index].ExpectedDesiredSize.Width : elements[index].ExpectedDesiredSize.Height;
        float num5 = 0.0f;
        if (elements[index].commandBarStripElement.VisibleInCommandBar && elements[index].commandBarStripElement.Visibility != ElementVisibility.Collapsed)
          num5 = this.Orientation == Orientation.Horizontal ? (float) elements[index].commandBarStripElement.MinSize.Width : (float) elements[index].commandBarStripElement.MinSize.Height;
        num1 += num4;
        num2 += num5;
        elements[index].DesiredSpaceToEnd = num1;
        elements[index].MinSpaceToEnd = num2;
        if (elements[index].commandBarStripElement.IsDrag)
          num3 = index;
      }
      if (num3 == -1)
        return elements;
      for (int index = num3 - 1; index >= 0; --index)
      {
        float num4 = this.Orientation == Orientation.Horizontal ? elements[index].DesiredLocation.X : elements[index].DesiredLocation.Y;
        float num5 = this.Orientation == Orientation.Horizontal ? elements[index + 1].DesiredLocation.X : elements[index + 1].DesiredLocation.Y;
        float num6 = this.Orientation == Orientation.Horizontal ? elements[index].commandBarStripElement.DesiredSize.Width : elements[index].commandBarStripElement.DesiredSize.Height;
        elements[index].IntersectionSpaceToEnd = elements[index + 1].IntersectionSpaceToEnd + num4 + num6 - num5;
        if ((double) elements[index].IntersectionSpaceToEnd <= 0.0)
          break;
      }
      return elements;
    }

    private void StableSort(
      List<RadCommandBarStripPanelLayoutInfo> elements,
      Comparison<RadCommandBarStripPanelLayoutInfo> comparison)
    {
      int num = elements.Count - 1;
      bool flag;
      do
      {
        flag = false;
        for (int index = 0; index < num; ++index)
        {
          if (comparison(elements[index], elements[index + 1]) > 0)
          {
            flag = true;
            RadCommandBarStripPanelLayoutInfo element = elements[index];
            elements[index] = elements[index + 1];
            elements[index + 1] = element;
          }
        }
      }
      while (flag);
    }

    private static int CompareCommandBarStripElementByX(
      RadCommandBarStripPanelLayoutInfo a,
      RadCommandBarStripPanelLayoutInfo b)
    {
      if ((double) a.DesiredLocation.X > (double) b.DesiredLocation.X)
        return 1;
      return (double) a.DesiredLocation.X < (double) b.DesiredLocation.X ? -1 : 0;
    }

    private static int CompareCommandBarStripElementByY(
      RadCommandBarStripPanelLayoutInfo a,
      RadCommandBarStripPanelLayoutInfo b)
    {
      if ((double) a.DesiredLocation.Y > (double) b.DesiredLocation.Y)
        return 1;
      return (double) a.DesiredLocation.Y < (double) b.DesiredLocation.Y ? -1 : 0;
    }

    protected internal void SetOrientationCore(Orientation newOrientation)
    {
      if (newOrientation == Orientation.Vertical)
      {
        this.StretchHorizontally = false;
        this.StretchVertically = true;
      }
      else
      {
        this.StretchHorizontally = true;
        this.StretchVertically = false;
      }
      foreach (CommandBarStripElement strip in this.strips)
        strip.SetOrientationCore(newOrientation);
      this.cachedOrientation = newOrientation;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "StretchHorizontally" || property.Name == "StretchVertically")
        return new bool?(false);
      return base.ShouldSerializeProperty(property);
    }

    [RefreshProperties(RefreshProperties.All)]
    [Editor("Telerik.WinControls.UI.Design.CommandBarStripElementCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CommandBarStripElementCollection Strips
    {
      get
      {
        return this.strips;
      }
    }
  }
}
