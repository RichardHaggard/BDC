// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DescriptionTextListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class DescriptionTextListVisualItem : RadListVisualItem
  {
    public static RadProperty DescriptionFontProperty = RadProperty.RegisterAttached(nameof (DescriptionFont), typeof (Font), typeof (DescriptionTextListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    private static Font descriptionContentFont;
    private StackLayoutElement horizontalStackLayout;
    private LightVisualElement imageContent;
    private StackLayoutElement verticalStackLayout;
    private LightVisualElement mainContent;
    private DescriptionContentListVisualItem descriptionContent;
    private LinePrimitive separator;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Class = nameof (DescriptionTextListVisualItem);
      this.DrawText = true;
      this.Text = string.Empty;
      this.ClipDrawing = true;
      this.horizontalStackLayout = new StackLayoutElement();
      this.horizontalStackLayout.Orientation = Orientation.Horizontal;
      this.horizontalStackLayout.StretchHorizontally = true;
      this.horizontalStackLayout.StretchVertically = true;
      this.horizontalStackLayout.ShouldHandleMouseInput = false;
      this.Children.Add((RadElement) this.horizontalStackLayout);
      this.imageContent = new LightVisualElement();
      this.imageContent.DrawText = false;
      this.imageContent.StretchVertically = true;
      this.imageContent.StretchHorizontally = false;
      this.imageContent.ImageLayout = ImageLayout.None;
      this.imageContent.ImageAlignment = ContentAlignment.MiddleCenter;
      int num1 = (int) this.imageContent.BindProperty(LightVisualElement.ImageAlignmentProperty, (RadObject) this, LightVisualElement.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      this.imageContent.Class = "ImageContent";
      this.imageContent.ShouldHandleMouseInput = false;
      this.horizontalStackLayout.Children.Add((RadElement) this.imageContent);
      this.verticalStackLayout = new StackLayoutElement();
      this.verticalStackLayout.Orientation = Orientation.Vertical;
      this.verticalStackLayout.StretchHorizontally = true;
      this.verticalStackLayout.StretchVertically = false;
      this.verticalStackLayout.ShouldHandleMouseInput = false;
      this.horizontalStackLayout.Children.Add((RadElement) this.verticalStackLayout);
      this.mainContent = new LightVisualElement();
      this.mainContent.StretchVertically = false;
      this.mainContent.TextAlignment = ContentAlignment.MiddleLeft;
      this.mainContent.NotifyParentOnMouseInput = true;
      this.mainContent.TextWrap = true;
      this.mainContent.Class = "MainContent";
      this.mainContent.ShouldHandleMouseInput = false;
      this.verticalStackLayout.Children.Add((RadElement) this.mainContent);
      this.separator = new LinePrimitive();
      this.separator.NotifyParentOnMouseInput = true;
      this.separator.StretchHorizontally = true;
      this.separator.StretchVertically = false;
      this.separator.ShouldHandleMouseInput = false;
      this.verticalStackLayout.Children.Add((RadElement) this.separator);
      this.descriptionContent = new DescriptionContentListVisualItem();
      this.descriptionContent.TextAlignment = ContentAlignment.MiddleLeft;
      this.descriptionContent.ForeColor = Color.FromArgb((int) byte.MaxValue, 128, 128, 128);
      this.descriptionContent.NotifyParentOnMouseInput = true;
      this.descriptionContent.ShouldHandleMouseInput = false;
      this.descriptionContent.TextWrap = true;
      this.descriptionContent.Class = "DescriptionContent";
      int num2 = (int) this.descriptionContent.BindProperty(DescriptionContentListVisualItem.ActiveProperty, (RadObject) this, RadListVisualItem.ActiveProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.descriptionContent.BindProperty(DescriptionContentListVisualItem.SelectedProperty, (RadObject) this, RadListVisualItem.SelectedProperty, PropertyBindingOptions.OneWay);
      int num4 = (int) this.descriptionContent.BindProperty(RadElement.ContainsMouseProperty, (RadObject) this, RadElement.ContainsMouseProperty, PropertyBindingOptions.OneWay);
      this.verticalStackLayout.Children.Add((RadElement) this.descriptionContent);
      RadListVisualItem.SynchronizationProperties.Add(DescriptionTextListVisualItem.DescriptionFontProperty);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == LightVisualElement.ImageAlignmentProperty)
      {
        switch ((ContentAlignment) e.NewValue)
        {
          case ContentAlignment.TopLeft:
          case ContentAlignment.MiddleLeft:
          case ContentAlignment.BottomLeft:
            this.imageContent.StretchHorizontally = false;
            this.verticalStackLayout.StretchHorizontally = true;
            break;
          case ContentAlignment.TopCenter:
          case ContentAlignment.MiddleCenter:
          case ContentAlignment.BottomCenter:
            this.imageContent.StretchHorizontally = true;
            this.verticalStackLayout.StretchHorizontally = true;
            break;
          case ContentAlignment.TopRight:
          case ContentAlignment.MiddleRight:
          case ContentAlignment.BottomRight:
            this.imageContent.StretchHorizontally = true;
            this.verticalStackLayout.StretchHorizontally = false;
            break;
        }
      }
      if (e.Property != RadElement.ContainsMouseProperty)
        return;
      this.IsMouseOver = this.ContainsMouse;
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      DescriptionTextListDataItem data = this.Data as DescriptionTextListDataItem;
      if (data == null)
        return;
      int num1 = (int) this.SetValue(RadItem.TextProperty, (object) string.Empty);
      this.mainContent.Text = this.Data.Text;
      this.imageContent.Image = this.Data.Image;
      this.imageContent.ImageAlignment = this.Data.ImageAlignment;
      this.descriptionContent.Text = data.DescriptionText;
      if (this.Data.GetValueSource(DescriptionTextListDataItem.DescriptionFontProperty) < ValueSource.Local)
      {
        int num2 = (int) this.descriptionContent.ResetValue(DescriptionTextListDataItem.DescriptionFontProperty, ValueResetFlags.Local);
      }
      else
      {
        int num3 = (int) this.descriptionContent.SetValue(VisualElement.FontProperty, this.Data.GetValue(DescriptionTextListDataItem.DescriptionFontProperty));
      }
      this.Image = (Image) null;
      if (this.item.Owner == null || this.item.Owner.GetDefaultItemHeight() != 18)
        return;
      int num4 = (int) this.item.Owner.SetDefaultValueOverride(RadListElement.ItemHeightProperty, (object) 36);
    }

    public override bool IsCompatible(RadListDataItem data, object context)
    {
      return data is DescriptionTextListDataItem;
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListVisualItem);
      }
    }

    public virtual string DescriptionText
    {
      get
      {
        return this.descriptionContent.Text;
      }
      set
      {
        this.descriptionContent.Text = value;
      }
    }

    public virtual string MainText
    {
      get
      {
        return this.mainContent.Text;
      }
      set
      {
        this.mainContent.Text = value;
      }
    }

    public LinePrimitive Separator
    {
      get
      {
        return this.separator;
      }
    }

    public DescriptionContentListVisualItem DescriptionContent
    {
      get
      {
        return this.descriptionContent;
      }
    }

    public LightVisualElement MainContent
    {
      get
      {
        return this.mainContent;
      }
    }

    public LightVisualElement ImageContent
    {
      get
      {
        return this.imageContent;
      }
    }

    protected Font DescriptionContentFont
    {
      get
      {
        if (DescriptionTextListVisualItem.descriptionContentFont == null)
          DescriptionTextListVisualItem.descriptionContentFont = new Font(this.descriptionContent.Font, FontStyle.Italic);
        return DescriptionTextListVisualItem.descriptionContentFont;
      }
    }

    [RadPropertyDefaultValue("Font", typeof (VisualElement))]
    [Category("Appearance")]
    [Description("Font - ex. of the text of an element. The property is inheritable through the element tree.")]
    public virtual Font DescriptionFont
    {
      get
      {
        return (Font) this.GetValue(DescriptionTextListVisualItem.DescriptionFontProperty);
      }
      set
      {
        int num = (int) this.SetValue(DescriptionTextListVisualItem.DescriptionFontProperty, (object) value);
      }
    }
  }
}
