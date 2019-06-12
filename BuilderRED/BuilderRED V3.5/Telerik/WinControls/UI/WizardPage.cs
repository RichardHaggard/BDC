// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardPage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class WizardPage : LightVisualElement
  {
    public static RadProperty TitleProperty = RadProperty.Register(nameof (Title), typeof (string), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "Page title"));
    public static RadProperty HeaderProperty = RadProperty.Register(nameof (Header), typeof (string), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "Page header"));
    public static RadProperty CustomizePageHeaderProperty = RadProperty.Register(nameof (CustomizePageHeader), typeof (bool), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty TitleVisibilityProperty = RadProperty.Register(nameof (TitleVisibility), typeof (ElementVisibility), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ElementVisibility.Visible));
    public static RadProperty HeaderVisibilityProperty = RadProperty.Register(nameof (HeaderVisibility), typeof (ElementVisibility), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ElementVisibility.Visible));
    public static RadProperty IconProperty = RadProperty.Register(nameof (Icon), typeof (Image), typeof (WizardPage), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    private RadWizardElement owner;
    private Panel contentArea;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Visibility = ElementVisibility.Collapsed;
      this.BackColor = Color.White;
    }

    public RadWizardElement Owner
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
      }
    }

    public Panel ContentArea
    {
      get
      {
        return this.contentArea;
      }
      set
      {
        this.contentArea = value;
        if (this.contentArea == null)
          return;
        if (!this.IsSelected)
          this.contentArea.Visible = false;
        this.contentArea.BackColor = this.BackColor;
        if (this.ElementTree == null)
          return;
        this.contentArea.Parent = this.ElementTree.Control;
      }
    }

    [Description("Gets or sets the page title text.")]
    [Category("Header")]
    [Localizable(true)]
    public string Title
    {
      get
      {
        return (string) this.GetValue(WizardPage.TitleProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.TitleProperty, (object) value);
      }
    }

    [Localizable(true)]
    [Category("Header")]
    [Description("Gets or sets the page header text.")]
    public string Header
    {
      get
      {
        return (string) this.GetValue(WizardPage.HeaderProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.HeaderProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether the page customizes its header settings.")]
    [Category("Header Settings")]
    [DefaultValue(false)]
    public bool CustomizePageHeader
    {
      get
      {
        return (bool) this.GetValue(WizardPage.CustomizePageHeaderProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.CustomizePageHeaderProperty, (object) value);
      }
    }

    [DefaultValue(ElementVisibility.Visible)]
    [Category("Header Settings")]
    [Description("Gets or sets the page's TitleElement visibility. Applies if CustomizePageHeader has value 'true'.")]
    public ElementVisibility TitleVisibility
    {
      get
      {
        return (ElementVisibility) this.GetValue(WizardPage.TitleVisibilityProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.TitleVisibilityProperty, (object) value);
      }
    }

    [DefaultValue(ElementVisibility.Visible)]
    [Category("Header Settings")]
    [Description("Gets or sets the page's HeaderElement visibility. Applies if CustomizePageHeader has value 'true'.")]
    public ElementVisibility HeaderVisibility
    {
      get
      {
        return (ElementVisibility) this.GetValue(WizardPage.HeaderVisibilityProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.HeaderVisibilityProperty, (object) value);
      }
    }

    [Category("Header Settings")]
    [Description("Gets or sets the page's IconElement image. Applies if CustomizePageHeader has value 'true'.")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter(typeof (ImageTypeConverter))]
    [DefaultValue(null)]
    public Image Icon
    {
      get
      {
        return (Image) this.GetValue(WizardPage.IconProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardPage.IconProperty, (object) value);
      }
    }

    public bool IsSelected
    {
      get
      {
        if (this.owner == null)
          return false;
        return this == this.owner.SelectedPage;
      }
    }

    internal virtual void LocateContentArea()
    {
      if (this.contentArea.Width == this.Size.Width && this.contentArea.Height == this.Size.Height && (this.contentArea.Location.X == this.BoundingRectangle.X && this.contentArea.Location.Y == this.BoundingRectangle.Y))
        return;
      this.contentArea.Width = this.Size.Width;
      this.contentArea.Height = this.Size.Height;
      this.contentArea.Location = new Point(this.BoundingRectangle.X, this.BoundingRectangle.Y);
    }

    private void Update()
    {
      if (this.Owner == null || this.Owner.SelectedPage != this)
        return;
      this.Owner.Refresh();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == WizardPage.TitleProperty || e.Property == WizardPage.HeaderProperty || (e.Property == WizardPage.CustomizePageHeaderProperty || e.Property == WizardPage.TitleVisibilityProperty) || (e.Property == WizardPage.HeaderVisibilityProperty || e.Property == WizardPage.IconProperty))
        this.Update();
      else if (e.Property == VisualElement.BackColorProperty && this.ContentArea != null)
        this.ContentArea.BackColor = (Color) e.NewValue;
      base.OnPropertyChanged(e);
    }

    public override string ToString()
    {
      return string.Format("{0} ({1})", (object) this.Title, (object) this.GetType().Name);
    }
  }
}
