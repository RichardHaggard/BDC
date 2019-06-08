// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardPageHeaderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class WizardPageHeaderElement : BaseWizardElement
  {
    public static RadProperty IconProperty = RadProperty.Register(nameof (Icon), typeof (Image), typeof (WizardPageHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata());
    private WizardTextElement titleElement;
    private WizardTextElement headerElement;
    private BaseWizardElement iconElement;
    private ElementVisibility defaultTitleVisibility;
    private bool setDefaultTitleVisibility;
    private ElementVisibility defaultHeaderVisibility;
    private bool setDefaultHeaderVisibility;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.titleElement = new WizardTextElement();
      this.titleElement.Class = "TitleElement";
      this.Children.Add((RadElement) this.titleElement);
      this.headerElement = new WizardTextElement();
      this.headerElement.Class = "HeaderElement";
      this.Children.Add((RadElement) this.headerElement);
      this.iconElement = new BaseWizardElement();
      this.iconElement.Class = "IconElement";
      this.Children.Add((RadElement) this.iconElement);
    }

    public BaseWizardElement TitleElement
    {
      get
      {
        return (BaseWizardElement) this.titleElement;
      }
    }

    public string Title
    {
      get
      {
        return this.titleElement.Text;
      }
      set
      {
        this.titleElement.Text = value;
      }
    }

    public ElementVisibility TitleVisibility
    {
      get
      {
        return this.titleElement.Visibility;
      }
      set
      {
        this.titleElement.Visibility = value;
        this.defaultTitleVisibility = value;
        this.setDefaultTitleVisibility = true;
      }
    }

    internal ElementVisibility DefaultTitleVisibility
    {
      get
      {
        return this.defaultTitleVisibility;
      }
      set
      {
        this.defaultTitleVisibility = value;
      }
    }

    internal bool SetDefaultTitleVisibility
    {
      get
      {
        return this.setDefaultTitleVisibility;
      }
      set
      {
        this.setDefaultTitleVisibility = value;
      }
    }

    public BaseWizardElement HeaderElement
    {
      get
      {
        return (BaseWizardElement) this.headerElement;
      }
    }

    public string Header
    {
      get
      {
        return this.headerElement.Text;
      }
      set
      {
        this.headerElement.Text = value;
      }
    }

    public ElementVisibility HeaderVisibility
    {
      get
      {
        return this.headerElement.Visibility;
      }
      set
      {
        this.headerElement.Visibility = value;
        this.defaultHeaderVisibility = value;
        this.setDefaultHeaderVisibility = true;
      }
    }

    internal ElementVisibility DefaultHeaderVisibility
    {
      get
      {
        return this.defaultHeaderVisibility;
      }
      set
      {
        this.defaultHeaderVisibility = value;
      }
    }

    internal bool SetDefaultHeaderVisibility
    {
      get
      {
        return this.setDefaultHeaderVisibility;
      }
      set
      {
        this.setDefaultHeaderVisibility = value;
      }
    }

    public BaseWizardElement IconElement
    {
      get
      {
        return this.iconElement;
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the WizardPageHeader icon image.")]
    public Image Icon
    {
      get
      {
        return (Image) this.GetValue(WizardPageHeaderElement.IconProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPageHeaderElement.IconProperty, (object) value);
      }
    }

    public ContentAlignment IconAlignment
    {
      get
      {
        return this.iconElement.Alignment;
      }
      set
      {
        this.iconElement.Alignment = value;
        this.InvalidateMeasure(true);
      }
    }

    public override void UpdateInfo(WizardPage page)
    {
      base.UpdateInfo(page);
      this.titleElement.UpdateInfo(page);
      this.headerElement.UpdateInfo(page);
      this.iconElement.UpdateInfo(page);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.BoundsProperty || (((Rectangle) e.NewValue).Height == ((Rectangle) e.OldValue).Height || !DWMAPI.IsCompositionEnabled || (this.IsDesignMode || this.Owner == null) || (this.Owner.Mode != WizardMode.Aero || !this.Owner.EnableAeroStyle)))
        return;
      this.UnapplyThemeStyles();
      this.Owner.ApplyAeroStyle();
    }

    internal void UnapplyThemeStyles()
    {
      if (this.IsDesignMode)
        return;
      this.DrawFill = false;
      this.BackgroundShape = (RadImageShape) null;
      WizardAeroTopElement topElement = (this.Owner.View as WizardAeroView).TopElement;
      topElement.DrawFill = false;
      topElement.BackgroundShape = (RadImageShape) null;
    }

    internal void ApplyThemeStyles()
    {
      if (this.IsDesignMode)
        return;
      int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(RadElement.BackgroundShapeProperty, ValueResetFlags.Local);
      WizardAeroTopElement topElement = (this.Owner.View as WizardAeroView).TopElement;
      int num3 = (int) topElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      int num4 = (int) topElement.ResetValue(RadElement.BackgroundShapeProperty, ValueResetFlags.Local);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) this.Owner.PageHeaderHeight > -1.0)
        return base.MeasureOverride(new SizeF(availableSize.Width, this.Owner.PageHeaderHeight));
      this.titleElement.Measure(availableSize);
      this.headerElement.Measure(availableSize);
      this.iconElement.Measure(availableSize);
      float height = this.titleElement.DesiredSize.Height + this.headerElement.DesiredSize.Height + (float) (this.Padding.Top + this.Padding.Bottom);
      return new SizeF(availableSize.Width, height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      bool flag;
      switch (this.iconElement.Alignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          flag = false;
          break;
        default:
          flag = true;
          break;
      }
      if (this.RightToLeft)
        flag = !flag;
      RectangleF finalRect1;
      float x;
      if (flag)
      {
        finalRect1 = new RectangleF(finalSize.Width - this.iconElement.DesiredSize.Width, (float) this.iconElement.Margin.Top, this.iconElement.DesiredSize.Width, this.iconElement.DesiredSize.Height);
        x = !this.RightToLeft ? (float) this.Padding.Left : finalSize.Width - this.titleElement.DesiredSize.Width - this.iconElement.DesiredSize.Width - (float) this.Padding.Right;
      }
      else
      {
        finalRect1 = new RectangleF(0.0f, (float) this.iconElement.Margin.Top, this.iconElement.DesiredSize.Width, this.iconElement.DesiredSize.Height);
        x = !this.RightToLeft ? this.iconElement.DesiredSize.Width : finalSize.Width - this.titleElement.DesiredSize.Width;
      }
      finalRect1.Height -= (float) (this.iconElement.Margin.Top + this.iconElement.Margin.Bottom);
      RectangleF finalRect2 = new RectangleF(x, (float) this.Padding.Top, this.titleElement.DesiredSize.Width, this.titleElement.DesiredSize.Height);
      RectangleF finalRect3 = new RectangleF(x, finalRect2.Height, this.headerElement.DesiredSize.Width, this.headerElement.DesiredSize.Height);
      this.titleElement.Arrange(finalRect2);
      this.headerElement.Arrange(finalRect3);
      this.iconElement.Arrange(finalRect1);
      return finalSize;
    }
  }
}
