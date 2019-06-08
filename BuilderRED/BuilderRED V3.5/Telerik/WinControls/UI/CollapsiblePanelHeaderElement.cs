// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelHeaderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelHeaderElement : StackLayoutElement
  {
    public static readonly RadProperty ShowHeaderLineProperty = RadProperty.Register(nameof (ShowHeaderLine), typeof (bool), typeof (CollapsiblePanelHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty HorizontalHeaderAlignmentProperty = RadProperty.Register(nameof (HorizontalHeaderAlignment), typeof (RadHorizontalAlignment), typeof (CollapsiblePanelHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadHorizontalAlignment.Left, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty VericalHeaderAlignmentProperty = RadProperty.Register(nameof (VerticalHeaderAlignment), typeof (RadVerticalAlignment), typeof (CollapsiblePanelHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadVerticalAlignment.Top, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty ExpandDirectionProperty = RadProperty.Register("ExpandDirection", typeof (RadDirection), typeof (CollapsiblePanelHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Down, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (CollapsiblePanelHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    private CollapsiblePanelButtonElement headerButton;
    private CollapsiblePanelTextElement headerText;
    private LinePrimitive headerLine;

    public CollapsiblePanelButtonElement HeaderButtonElement
    {
      get
      {
        return this.headerButton;
      }
    }

    public CollapsiblePanelTextElement HeaderTextElement
    {
      get
      {
        return this.headerText;
      }
    }

    public LinePrimitive HeaderLineElement
    {
      get
      {
        return this.headerLine;
      }
    }

    public bool ShowHeaderLine
    {
      get
      {
        return (bool) this.GetValue(CollapsiblePanelHeaderElement.ShowHeaderLineProperty);
      }
      set
      {
        int num = (int) this.SetValue(CollapsiblePanelHeaderElement.ShowHeaderLineProperty, (object) value);
      }
    }

    public RadHorizontalAlignment HorizontalHeaderAlignment
    {
      get
      {
        return (RadHorizontalAlignment) this.GetValue(CollapsiblePanelHeaderElement.HorizontalHeaderAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(CollapsiblePanelHeaderElement.HorizontalHeaderAlignmentProperty, (object) value);
      }
    }

    public RadVerticalAlignment VerticalHeaderAlignment
    {
      get
      {
        return (RadVerticalAlignment) this.GetValue(CollapsiblePanelHeaderElement.VericalHeaderAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(CollapsiblePanelHeaderElement.VericalHeaderAlignmentProperty, (object) value);
      }
    }

    static CollapsiblePanelHeaderElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadCollapsiblePanelElementStateManagerFactory(CollapsiblePanelHeaderElement.ExpandDirectionProperty, CollapsiblePanelHeaderElement.IsExpandedProperty), typeof (CollapsiblePanelHeaderElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Alignment = ContentAlignment.MiddleCenter;
      this.DrawFill = true;
      this.BackColor = this.BackColor2 = this.BackColor3 = this.BackColor4 = Color.Transparent;
      this.MinSize = new Size(0, 18);
    }

    protected override void CreateChildElements()
    {
      this.headerButton = this.CreateButtonElement();
      this.headerButton.StretchHorizontally = false;
      this.Children.Add((RadElement) this.headerButton);
      this.headerText = this.CreateTextElement();
      this.headerText.StretchHorizontally = false;
      this.Children.Add((RadElement) this.headerText);
      this.headerLine = this.CreateLineElement();
      this.Children.Add((RadElement) this.headerLine);
    }

    protected virtual CollapsiblePanelButtonElement CreateButtonElement()
    {
      return new CollapsiblePanelButtonElement();
    }

    protected virtual CollapsiblePanelTextElement CreateTextElement()
    {
      return new CollapsiblePanelTextElement();
    }

    protected virtual LinePrimitive CreateLineElement()
    {
      return new LinePrimitive();
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      SizeF sizeF = base.ArrangeOverride(arrangeSize);
      RadCollapsiblePanelElement parent = this.Parent as RadCollapsiblePanelElement;
      RadDirection expandDirection = parent != null ? parent.ExpandDirection : RadDirection.Down;
      if (this.Orientation == Orientation.Horizontal)
        this.ArrangeAccordingToHorizontalHeaderAlignment(arrangeSize);
      else
        this.ArrangeAccordingToVerticalHeaderAlignment(arrangeSize);
      this.ArrangeLineInMiddle(expandDirection, arrangeSize);
      return sizeF;
    }

    private void ArrangeAccordingToHorizontalHeaderAlignment(SizeF arrangeSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(arrangeSize);
      if (this.HorizontalHeaderAlignment != RadHorizontalAlignment.Center)
        return;
      float x = arrangeSize.Width / 2f + (float) this.Padding.Left - (float) this.Padding.Right;
      foreach (RadElement child in this.Children)
        x -= child.DesiredSize.Width / 2f;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement element = !this.RightToLeft ? this.Children[index] : this.Children[this.Children.Count - 1 - index];
        if (element.Visibility != ElementVisibility.Collapsed)
        {
          SizeF size = new SizeF(element.DesiredSize.Width, clientRectangle.Height);
          RectangleF finalRect = new RectangleF(new PointF(x, (float) (element.Bounds.Location.Y + this.Padding.Top - this.Padding.Bottom)), size);
          this.ArrangeElement(element, clientRectangle, finalRect, arrangeSize);
          x += size.Width;
        }
      }
    }

    private void ArrangeAccordingToVerticalHeaderAlignment(SizeF arrangeSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(arrangeSize);
      if (this.VerticalHeaderAlignment != RadVerticalAlignment.Center)
        return;
      float y = arrangeSize.Height / 2f + (float) this.Padding.Top - (float) this.Padding.Bottom;
      foreach (RadElement child in this.Children)
        y -= child.DesiredSize.Height / 2f;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          SizeF size = new SizeF(clientRectangle.Width, child.DesiredSize.Height);
          RectangleF finalRect = new RectangleF(new PointF((float) (child.Location.X + this.Padding.Top - this.Padding.Bottom), y), size);
          this.ArrangeElement(child, clientRectangle, finalRect, arrangeSize);
          y += size.Width;
        }
      }
    }

    protected override void ArrangeElement(
      RadElement element,
      RectangleF clientRect,
      RectangleF finalRect,
      SizeF finalSize)
    {
      RectangleF finalRect1 = finalRect;
      if (element.Visibility == ElementVisibility.Collapsed)
        finalRect1.Size = SizeF.Empty;
      else if (this.Orientation == Orientation.Horizontal)
      {
        if (this.HorizontalHeaderAlignment == RadHorizontalAlignment.Right)
          finalRect1.Location = new PointF(clientRect.Width - finalRect.X - finalRect.Size.Width, finalRect.Location.Y);
      }
      else if (this.VerticalHeaderAlignment == RadVerticalAlignment.Bottom)
        finalRect1.Location = new PointF(finalRect.Location.X, clientRect.Height - finalRect.Y - finalRect.Size.Height);
      base.ArrangeElement(element, clientRect, finalRect1, finalSize);
    }

    private void ArrangeLineInMiddle(RadDirection expandDirection, SizeF arrangeSize)
    {
      if (!this.ShowHeaderLine)
        return;
      PointF location = PointF.Empty;
      switch (expandDirection)
      {
        case RadDirection.Left:
        case RadDirection.Right:
          location = new PointF(arrangeSize.Width / 2f - (float) this.HeaderLineElement.LineWidth, this.HeaderLineElement.PreviousArrangeRect.Location.Y);
          break;
        case RadDirection.Up:
        case RadDirection.Down:
          location = new PointF(this.HeaderLineElement.PreviousArrangeRect.Location.X, arrangeSize.Height / 2f - (float) (this.HeaderLineElement.LineWidth / 2));
          break;
      }
      base.ArrangeElement((RadElement) this.HeaderLineElement, this.GetClientRectangle(arrangeSize), new RectangleF(location, this.HeaderLineElement.PreviousArrangeRect.Size), arrangeSize);
    }

    private void StretchAllChildren(Orientation orientation, bool reverseCurrentValues)
    {
      foreach (RadElement child in this.Children)
      {
        if (orientation == Orientation.Horizontal)
        {
          child.StretchHorizontally = child.Visibility != ElementVisibility.Collapsed;
          if (reverseCurrentValues)
            child.StretchVertically = false;
        }
        else
        {
          child.StretchVertically = child.Visibility != ElementVisibility.Collapsed;
          if (reverseCurrentValues)
            child.StretchHorizontally = false;
        }
      }
    }

    private void ResetChildrenStretch(Orientation value)
    {
      if (value == Orientation.Horizontal)
      {
        foreach (RadElement child in this.Children)
        {
          if (child == this.HeaderLineElement)
          {
            child.StretchVertically = false;
            child.StretchHorizontally = true;
          }
          else
          {
            child.StretchHorizontally = false;
            child.StretchVertically = true;
          }
        }
      }
      else
      {
        foreach (RadElement child in this.Children)
        {
          if (child == this.HeaderLineElement)
          {
            child.StretchHorizontally = false;
            child.StretchVertically = true;
          }
          else
          {
            child.StretchHorizontally = true;
            child.StretchVertically = false;
          }
        }
      }
    }

    private void UpdateChildrenStretchOnVerticalAlignment(RadVerticalAlignment? verticalAlignment)
    {
      RadVerticalAlignment? nullable1 = verticalAlignment;
      if ((nullable1.GetValueOrDefault() != RadVerticalAlignment.Stretch ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
      {
        this.StretchAllChildren(Orientation.Vertical, true);
      }
      else
      {
        RadVerticalAlignment? nullable2 = verticalAlignment;
        if ((nullable2.GetValueOrDefault() != RadVerticalAlignment.Stretch ? 1 : (!nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.ResetChildrenStretch(this.Orientation);
      }
    }

    private void UpdateChildrenStretchOnHorizontalAlignment(
      RadHorizontalAlignment? horizontalAlignment)
    {
      RadHorizontalAlignment? nullable1 = horizontalAlignment;
      if ((nullable1.GetValueOrDefault() != RadHorizontalAlignment.Stretch ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
        this.StretchAllChildren(Orientation.Horizontal, true);
      RadHorizontalAlignment? nullable2 = horizontalAlignment;
      if ((nullable2.GetValueOrDefault() != RadHorizontalAlignment.Center ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
      {
        this.headerButton.StretchHorizontally = false;
        this.headerText.StretchHorizontally = false;
      }
      else
      {
        RadHorizontalAlignment? nullable3 = horizontalAlignment;
        if ((nullable3.GetValueOrDefault() != RadHorizontalAlignment.Stretch ? 1 : (!nullable3.HasValue ? 1 : 0)) == 0)
          return;
        this.ResetChildrenStretch(this.Orientation);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == StackLayoutElement.OrientationProperty)
      {
        Orientation newValue = (Orientation) e.NewValue;
        this.HeaderLineElement.SeparatorOrientation = (SepOrientation) Enum.Parse(typeof (SepOrientation), newValue.ToString());
        this.HeaderTextElement.TextOrientation = newValue;
        this.HeaderButtonElement.TextOrientation = newValue;
        if (newValue == Orientation.Vertical)
        {
          this.UpdateChildrenStretchOnVerticalAlignment(new RadVerticalAlignment?(this.VerticalHeaderAlignment));
          this.HeaderLineElement.GradientAngle = 0.0f;
        }
        else
        {
          this.UpdateChildrenStretchOnHorizontalAlignment(new RadHorizontalAlignment?(this.HorizontalHeaderAlignment));
          int num = (int) this.HeaderLineElement.ResetValue(FillPrimitive.GradientAngleProperty, ValueResetFlags.Local);
        }
      }
      else if (e.Property == CollapsiblePanelHeaderElement.ShowHeaderLineProperty)
      {
        this.HeaderLineElement.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        if (this.Orientation == Orientation.Vertical)
          this.UpdateChildrenStretchOnVerticalAlignment(new RadVerticalAlignment?(this.VerticalHeaderAlignment));
        else
          this.UpdateChildrenStretchOnHorizontalAlignment(new RadHorizontalAlignment?(this.HorizontalHeaderAlignment));
      }
      else
      {
        if (e.Property != CollapsiblePanelHeaderElement.HorizontalHeaderAlignmentProperty && e.Property != CollapsiblePanelHeaderElement.VericalHeaderAlignmentProperty)
          return;
        RadHorizontalAlignment? newValue1 = e.NewValue as RadHorizontalAlignment?;
        if (newValue1.HasValue && this.Orientation == Orientation.Horizontal)
          this.UpdateChildrenStretchOnHorizontalAlignment(newValue1);
        RadVerticalAlignment? newValue2 = e.NewValue as RadVerticalAlignment?;
        if (!newValue2.HasValue || this.Orientation != Orientation.Vertical)
          return;
        this.UpdateChildrenStretchOnVerticalAlignment(newValue2);
      }
    }
  }
}
