// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGroupBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadGroupBoxElement : LightVisualElement
  {
    public static RadProperty HeaderAlignmentProperty = RadProperty.Register(nameof (HeaderAlignment), typeof (HeaderAlignment), typeof (RadGroupBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) HeaderAlignment.Near, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    [Browsable(false)]
    public static RadProperty InvalidateMeasureInMainLayoutProperty = RadProperty.Register(nameof (InvalidateMeasureInMainLayout), typeof (int), typeof (RadGroupBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    private GroupBoxHeader header;
    private GroupBoxContent content;
    private GroupBoxFooter footer;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "GroupBoxElement";
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.header = new GroupBoxHeader();
      this.content = new GroupBoxContent();
      this.footer = new GroupBoxFooter();
      this.Children.Add((RadElement) this.content);
      this.Children.Add((RadElement) this.header);
      this.Children.Add((RadElement) this.footer);
    }

    public Padding HeaderMargin
    {
      get
      {
        return this.header.Margin;
      }
      set
      {
        this.header.Margin = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public string HeaderText
    {
      get
      {
        return this.header.TextPrimitive.Text;
      }
      set
      {
        this.header.TextPrimitive.Text = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public string FooterText
    {
      get
      {
        return this.footer.TextPrimitive.Text;
      }
      set
      {
        this.footer.TextPrimitive.Text = value;
      }
    }

    public Image HeaderImage
    {
      get
      {
        return this.header.ImagePrimitive.Image;
      }
      set
      {
        this.header.ImagePrimitive.Image = value;
      }
    }

    [DefaultValue(null)]
    public Image FooterImage
    {
      get
      {
        return this.footer.ImagePrimitive.Image;
      }
      set
      {
        this.footer.ImagePrimitive.Image = value;
      }
    }

    public TextImageRelation HeaderTextImageRelation
    {
      get
      {
        return this.header.ImageAndTextLayout.TextImageRelation;
      }
      set
      {
        this.header.ImageAndTextLayout.TextImageRelation = value;
      }
    }

    public TextImageRelation FooterTextImageRelation
    {
      get
      {
        return this.footer.ImageAndTextLayout.TextImageRelation;
      }
      set
      {
        this.footer.ImageAndTextLayout.TextImageRelation = value;
      }
    }

    public ContentAlignment HeaderTextAlignment
    {
      get
      {
        return this.header.ImageAndTextLayout.TextAlignment;
      }
      set
      {
        this.header.ImageAndTextLayout.TextAlignment = value;
      }
    }

    public ContentAlignment FooterTextAlignment
    {
      get
      {
        return this.footer.ImageAndTextLayout.TextAlignment;
      }
      set
      {
        this.footer.ImageAndTextLayout.TextAlignment = value;
      }
    }

    public ContentAlignment HeaderImageAlignment
    {
      get
      {
        return this.header.ImageAndTextLayout.ImageAlignment;
      }
      set
      {
        this.header.ImageAndTextLayout.ImageAlignment = value;
      }
    }

    public ContentAlignment FooterImageAlignment
    {
      get
      {
        return this.footer.ImageAndTextLayout.ImageAlignment;
      }
      set
      {
        this.footer.ImageAndTextLayout.ImageAlignment = value;
      }
    }

    public string HeaderImageKey
    {
      get
      {
        return this.header.ImagePrimitive.ImageKey;
      }
      set
      {
        this.header.ImagePrimitive.ImageKey = value;
        this.header.ImagePrimitive.InvalidateMeasure();
      }
    }

    public int HeaderImageIndex
    {
      get
      {
        return this.header.ImagePrimitive.ImageIndex;
      }
      set
      {
        this.header.ImagePrimitive.ImageIndex = value;
        this.header.ImagePrimitive.InvalidateMeasure();
      }
    }

    public string FooterImageKey
    {
      get
      {
        return this.footer.ImagePrimitive.ImageKey;
      }
      set
      {
        this.footer.ImagePrimitive.ImageKey = value;
        this.footer.ImagePrimitive.InvalidateMeasure();
      }
    }

    public int FooterImageIndex
    {
      get
      {
        return this.footer.ImagePrimitive.ImageIndex;
      }
      set
      {
        this.footer.ImagePrimitive.ImageIndex = value;
        this.footer.ImagePrimitive.InvalidateMeasure();
      }
    }

    public GroupBoxHeader Header
    {
      get
      {
        return this.header;
      }
    }

    public GroupBoxContent Content
    {
      get
      {
        return this.content;
      }
    }

    public GroupBoxFooter Footer
    {
      get
      {
        return this.footer;
      }
    }

    public HeaderAlignment HeaderAlignment
    {
      get
      {
        return (HeaderAlignment) this.GetValue(RadGroupBoxElement.HeaderAlignmentProperty);
      }
      set
      {
        if ((HeaderAlignment) this.GetValue(RadGroupBoxElement.HeaderAlignmentProperty) == value)
          return;
        int num = (int) this.SetValue(RadGroupBoxElement.HeaderAlignmentProperty, (object) value);
      }
    }

    [Browsable(false)]
    public int InvalidateMeasureInMainLayout
    {
      get
      {
        return (int) this.GetValue(RadGroupBoxElement.InvalidateMeasureInMainLayoutProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGroupBoxElement.InvalidateMeasureInMainLayoutProperty, (object) value);
      }
    }

    public HeaderPosition HeaderPosition
    {
      get
      {
        return this.Header.HeaderPosition;
      }
      set
      {
        this.Header.HeaderPosition = value;
      }
    }

    public RadGroupBoxStyle GroupBoxStyle
    {
      get
      {
        return this.header.GroupBoxStyle;
      }
      set
      {
        if (this.header.GroupBoxStyle == value)
          return;
        this.header.GroupBoxStyle = value;
      }
    }

    public ElementVisibility FooterVisibile
    {
      get
      {
        return this.footer.Visibility;
      }
      set
      {
        if (this.footer.Visibility == value)
          return;
        if (value == ElementVisibility.Hidden)
          this.footer.Visibility = ElementVisibility.Collapsed;
        else
          this.footer.Visibility = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      SizeF empty = SizeF.Empty;
      if (this.Footer.Visibility == ElementVisibility.Visible)
        empty.Height += this.Footer.DesiredSize.Height;
      if (this.HeaderPosition == HeaderPosition.Top || this.HeaderPosition == HeaderPosition.Bottom)
        empty.Height += this.Header.DesiredSize.Height;
      else
        empty.Width += this.Header.DesiredSize.Width;
      empty.Height += this.Content.DesiredSize.Height;
      empty.Width += this.Content.DesiredSize.Width;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      Padding padding = new Padding(0, 0, 0, 0);
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.GroupBoxStyle == RadGroupBoxStyle.Standard)
      {
        if (this.HeaderPosition == HeaderPosition.Top || this.HeaderPosition == HeaderPosition.Bottom)
        {
          num1 = 0.5f;
          switch (this.HeaderAlignment)
          {
            case HeaderAlignment.Near:
              if (!this.RightToLeft)
              {
                num2 = 0.0f;
                padding = new Padding(10, 0, 0, 0);
                break;
              }
              num2 = 1f;
              padding = new Padding(0, 0, 10, 0);
              break;
            case HeaderAlignment.Center:
              num2 = 0.5f;
              break;
            case HeaderAlignment.Far:
              if (!this.RightToLeft)
              {
                num2 = 1f;
                padding = new Padding(0, 0, 10, 0);
                break;
              }
              num2 = 0.0f;
              padding = new Padding(10, 0, 0, 0);
              break;
          }
        }
        else
        {
          num2 = 0.5f;
          switch (this.HeaderAlignment)
          {
            case HeaderAlignment.Near:
              num1 = 0.0f;
              padding = new Padding(0, 10, 0, 0);
              break;
            case HeaderAlignment.Center:
              num1 = 0.5f;
              break;
            case HeaderAlignment.Far:
              num1 = 1f;
              padding = new Padding(0, 0, 0, 10);
              break;
          }
        }
      }
      else if (this.HeaderPosition == HeaderPosition.Top || this.HeaderPosition == HeaderPosition.Bottom)
      {
        num1 = 1f;
        num2 = 0.0f;
      }
      else
      {
        num2 = 1f;
        num1 = 0.0f;
      }
      SizeF sizeF = SizeF.Empty;
      sizeF = new SizeF(finalSize.Width, finalSize.Height - this.footer.DesiredSize.Height);
      foreach (RadElement child in this.Children)
      {
        if (child == this.Footer)
        {
          RectangleF finalRect = new RectangleF(0.0f, finalSize.Height - this.footer.DesiredSize.Height, finalSize.Width, this.footer.DesiredSize.Height);
          child.Arrange(finalRect);
        }
        else if (child == this.Header)
        {
          float num3 = 0.0f;
          float num4 = 0.0f;
          float width = this.header.DesiredSize.Width;
          float height = this.header.DesiredSize.Height;
          if (this.GroupBoxStyle == RadGroupBoxStyle.Standard)
          {
            switch (this.HeaderPosition)
            {
              case HeaderPosition.Left:
                num4 = (sizeF.Height - child.DesiredSize.Height) * num1;
                break;
              case HeaderPosition.Top:
                num3 = (sizeF.Width - child.DesiredSize.Width) * num2;
                break;
              case HeaderPosition.Right:
                num3 = sizeF.Width - child.DesiredSize.Width;
                num4 = (sizeF.Height - child.DesiredSize.Height) * num1;
                break;
              case HeaderPosition.Bottom:
                num4 = sizeF.Height - child.DesiredSize.Height;
                num3 = (sizeF.Width - child.DesiredSize.Width) * num2;
                break;
            }
          }
          else
          {
            switch (this.HeaderPosition)
            {
              case HeaderPosition.Left:
                height = this.footer.Visibility != ElementVisibility.Collapsed ? finalSize.Height - (float) this.footer.Size.Height : finalSize.Height;
                break;
              case HeaderPosition.Top:
                width = finalSize.Width;
                break;
              case HeaderPosition.Right:
                height = this.footer.Visibility != ElementVisibility.Collapsed ? finalSize.Height - (float) this.footer.Size.Height : finalSize.Height;
                num3 = finalSize.Width - this.header.DesiredSize.Width;
                break;
              case HeaderPosition.Bottom:
                width = finalSize.Width;
                num4 = sizeF.Height - child.DesiredSize.Height;
                num3 = (sizeF.Width - child.DesiredSize.Width) * num2;
                break;
            }
          }
          RectangleF finalRect = new RectangleF(num3 + (float) padding.Left - (float) padding.Right, num4 + (float) padding.Top - (float) padding.Bottom, width, height);
          child.Arrange(finalRect);
        }
        else if (child == this.Content)
        {
          float num3 = 0.0f;
          float num4 = 0.0f;
          float width = sizeF.Width;
          float height = sizeF.Height;
          if (this.GroupBoxStyle == RadGroupBoxStyle.Standard)
          {
            switch (this.HeaderPosition)
            {
              case HeaderPosition.Left:
                num3 += num2 * this.Header.DesiredSize.Width;
                width -= num2 * this.Header.DesiredSize.Width;
                break;
              case HeaderPosition.Top:
                num4 += num1 * this.Header.DesiredSize.Height;
                height -= num1 * this.Header.DesiredSize.Height;
                break;
              case HeaderPosition.Right:
                width -= num2 * this.Header.DesiredSize.Width;
                break;
              case HeaderPosition.Bottom:
                height -= num1 * this.Header.DesiredSize.Height;
                break;
            }
          }
          else
          {
            switch (this.HeaderPosition)
            {
              case HeaderPosition.Left:
                num3 += num2 * this.header.DesiredSize.Width;
                width -= num2 * this.header.DesiredSize.Width;
                break;
              case HeaderPosition.Top:
                num4 += num1 * this.Header.DesiredSize.Height;
                height -= num1 * this.Header.DesiredSize.Height;
                break;
              case HeaderPosition.Right:
                width -= num2 * this.header.DesiredSize.Width;
                break;
              case HeaderPosition.Bottom:
                height -= num1 * this.Header.DesiredSize.Height;
                break;
            }
          }
          RectangleF finalRect = new RectangleF((float) Math.Floor((double) num3), (float) Math.Floor((double) num4), (float) Math.Floor((double) width), (float) Math.Floor((double) height));
          child.Arrange(finalRect);
        }
        else
          child.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
      }
      return finalSize;
    }
  }
}
