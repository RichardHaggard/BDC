// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupLinkElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class GroupLinkElement : GridVisualElement
  {
    public static RadProperty TypeProperty = RadProperty.Register(nameof (Type), typeof (GroupLinkElement.LinkType), typeof (GroupLinkElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GroupLinkElement.LinkType.DescriptorsLink, ElementPropertyOptions.AffectsDisplay));
    private TemplateGroupsElement templateElement;
    private GridLinkItem linkPrimitive;
    private ArrowPrimitive leftArrow;
    private ArrowPrimitive rightArrow;

    public GroupLinkElement(TemplateGroupsElement templateElement)
    {
      this.templateElement = templateElement;
      this.InitializeLinkType();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BypassLayoutPolicies = true;
      this.BorderWidth = 0.0f;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.linkPrimitive = new GridLinkItem();
      this.linkPrimitive.ThemeRole = "GroupLink";
      this.linkPrimitive.LineStyle = DashStyle.Solid;
      this.Children.Add((RadElement) this.linkPrimitive);
      this.leftArrow = new ArrowPrimitive();
      this.leftArrow.SmoothingMode = SmoothingMode.None;
      this.Children.Add((RadElement) this.leftArrow);
      this.rightArrow = new ArrowPrimitive();
      this.rightArrow.SmoothingMode = SmoothingMode.None;
      this.Children.Add((RadElement) this.rightArrow);
      this.InitializePrimitives(this.Type);
    }

    public GroupLinkElement.LinkType Type
    {
      get
      {
        return (GroupLinkElement.LinkType) this.GetValue(GroupLinkElement.TypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GroupLinkElement.TypeProperty, (object) value);
      }
    }

    public TemplateGroupsElement TemplateElement
    {
      get
      {
        return this.templateElement;
      }
    }

    public ArrowPrimitive LeftArrowPrimitive
    {
      get
      {
        return this.leftArrow;
      }
    }

    public ArrowPrimitive RightArrowPrimtiive
    {
      get
      {
        return this.rightArrow;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.Type == GroupLinkElement.LinkType.NamesLink)
        sizeF = new SizeF((float) this.TemplateElement.LinkWidth, sizeF.Height);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float middleY = clientRectangle.Y + clientRectangle.Height / 2f;
      if (this.Type == GroupLinkElement.LinkType.NamesLink)
        this.ArrangeNamesLink(clientRectangle, middleY);
      else if (this.TemplateElement.LinkPosition == TemplateGroupsElement.GroupLinkPosition.Bottom)
        this.ArrangeDescriptorsBottomLink(clientRectangle);
      else
        this.ArrangeDescriptorsTopLink(clientRectangle);
      this.linkPrimitive.ArrowSize = Size.Round(this.rightArrow.DesiredSize);
      return finalSize;
    }

    protected virtual void ArrangeDescriptorsTopLink(RectangleF clientRect)
    {
      this.linkPrimitive.Arrange(clientRect);
      RectangleF empty = RectangleF.Empty;
      empty.X = clientRect.X + (float) (((double) clientRect.Width - (double) this.rightArrow.DesiredSize.Width) / 2.0);
      empty.Y = clientRect.Bottom - this.rightArrow.DesiredSize.Height;
      empty.Size = this.rightArrow.DesiredSize;
      this.rightArrow.Arrange(empty);
    }

    protected virtual void ArrangeDescriptorsBottomLink(RectangleF clientRect)
    {
      this.linkPrimitive.Arrange(clientRect);
      RectangleF empty = RectangleF.Empty;
      empty.X = !this.RightToLeft ? clientRect.Right - this.rightArrow.DesiredSize.Width : clientRect.X;
      empty.Y = clientRect.Y + (float) (((double) clientRect.Height - (double) this.rightArrow.DesiredSize.Height) / 2.0);
      empty.Size = this.rightArrow.DesiredSize;
      this.rightArrow.Arrange(empty);
    }

    protected virtual void ArrangeNamesLink(RectangleF clientRect, float middleY)
    {
      float height = (float) (int) Math.Max(this.leftArrow.DesiredSize.Height, (float) this.rightArrow.DefaultSize.Height);
      float y = middleY - height / 2f;
      RectangleF finalRect1 = new RectangleF(new PointF(clientRect.X, y), new SizeF(this.leftArrow.DesiredSize.Width, height));
      this.leftArrow.Arrange(finalRect1);
      RectangleF finalRect2 = new RectangleF(finalRect1.Right, y, clientRect.Width - this.rightArrow.DesiredSize.Width - this.leftArrow.DesiredSize.Width, height);
      this.linkPrimitive.Arrange(finalRect2);
      this.rightArrow.Arrange(new RectangleF(new PointF(finalRect2.Right, y), new SizeF(this.rightArrow.DesiredSize.Width, height)));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == LightVisualElement.BackgroundImageProperty)
      {
        this.ChangePrimitivesVisibility(e.NewValue == null);
      }
      else
      {
        if (e.Property != GroupLinkElement.TypeProperty && e.Property != RadElement.RightToLeftProperty)
          return;
        this.InitializePrimitives(this.Type);
      }
    }

    private void InitializePrimitives(GroupLinkElement.LinkType type)
    {
      if (type == GroupLinkElement.LinkType.NamesLink)
      {
        this.leftArrow.Direction = ArrowDirection.Left;
        this.leftArrow.Visibility = ElementVisibility.Visible;
        this.linkPrimitive.Type = GridLinkItem.LinkType.HorizontalLine;
        this.rightArrow.Direction = ArrowDirection.Right;
      }
      else
        this.InitializeLinkType();
    }

    private void InitializeLinkType()
    {
      if (this.templateElement != null && this.templateElement.LinkPosition == TemplateGroupsElement.GroupLinkPosition.Top)
      {
        this.linkPrimitive.Type = GridLinkItem.LinkType.LeftTopAngleShape;
        this.leftArrow.Visibility = ElementVisibility.Hidden;
        this.rightArrow.Direction = ArrowDirection.Down;
      }
      else
      {
        this.linkPrimitive.Type = GridLinkItem.LinkType.RightBottomAngleShape;
        this.leftArrow.Visibility = ElementVisibility.Hidden;
        this.rightArrow.Visibility = ElementVisibility.Visible;
        if (this.RightToLeft)
          this.rightArrow.Direction = ArrowDirection.Left;
        else
          this.rightArrow.Direction = ArrowDirection.Right;
      }
    }

    private void ChangePrimitivesVisibility(bool visible)
    {
      foreach (RadElement child in this.Children)
        child.Visibility = visible ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    public enum LinkType
    {
      DescriptorsLink,
      NamesLink,
    }
  }
}
