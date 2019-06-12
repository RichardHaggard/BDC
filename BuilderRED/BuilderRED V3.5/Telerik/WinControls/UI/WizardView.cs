// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class WizardView : LightVisualElement
  {
    public static RadProperty CommandAreaHeightProperty = RadProperty.Register(nameof (CommandAreaHeight), typeof (float), typeof (WizardView), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1f));
    public static RadProperty PageHeaderHeightProperty = RadProperty.Register(nameof (PageHeaderHeight), typeof (float), typeof (WizardView), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1f));
    public static RadProperty HideWelcomeImageProperty = RadProperty.Register(nameof (HideWelcomeImage), typeof (bool), typeof (WizardView), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty HideCompletionImageProperty = RadProperty.Register(nameof (HideCompletionImage), typeof (bool), typeof (WizardView), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    private RadWizardElement owner;
    private WizardCommandArea commandArea;
    private WizardPageHeaderElement pageHeaderElement;
    private LightVisualElement welcomeImageElement;
    private Image welcomeImage;
    private LightVisualElement completionImageElement;
    private Image completionImage;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.pageHeaderElement = new WizardPageHeaderElement();
      this.Children.Add((RadElement) this.pageHeaderElement);
      this.welcomeImageElement = new LightVisualElement();
      this.welcomeImageElement.Class = "WelcomeImage";
      this.welcomeImageElement.ImageAlignment = ContentAlignment.TopCenter;
      this.welcomeImageElement.ImageLayout = ImageLayout.None;
      this.Children.Add((RadElement) this.welcomeImageElement);
      this.completionImageElement = new LightVisualElement();
      this.completionImageElement.Class = "CompletionImage";
      this.completionImageElement.ImageAlignment = ContentAlignment.TopCenter;
      this.completionImageElement.ImageLayout = ImageLayout.None;
      this.Children.Add((RadElement) this.completionImageElement);
    }

    public RadWizardElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    public WizardPageCollection Pages
    {
      get
      {
        return this.Owner.Pages;
      }
    }

    public WizardWelcomePage WelcomePage
    {
      get
      {
        return this.Owner.WelcomePage;
      }
    }

    public WizardCompletionPage CompletionPage
    {
      get
      {
        return this.Owner.CompletionPage;
      }
    }

    public WizardPage SelectedPage
    {
      get
      {
        return this.Owner.SelectedPage;
      }
    }

    public WizardCommandArea CommandArea
    {
      get
      {
        return this.commandArea;
      }
      internal set
      {
        this.commandArea = value;
      }
    }

    public float CommandAreaHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(WizardView.CommandAreaHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(WizardView.CommandAreaHeightProperty, (object) value);
      }
    }

    public WizardPageHeaderElement PageHeaderElement
    {
      get
      {
        return this.pageHeaderElement;
      }
    }

    public float PageHeaderHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(WizardView.PageHeaderHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(WizardView.PageHeaderHeightProperty, (object) value);
      }
    }

    public LightVisualElement WelcomeImageElement
    {
      get
      {
        return this.welcomeImageElement;
      }
    }

    public Image WelcomeImage
    {
      get
      {
        return this.welcomeImage;
      }
      set
      {
        this.welcomeImage = value;
      }
    }

    public bool HideWelcomeImage
    {
      get
      {
        return (bool) this.GetValue(WizardView.HideWelcomeImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardView.HideWelcomeImageProperty, (object) value);
      }
    }

    public ImageLayout WelcomeImageLayout
    {
      get
      {
        return this.welcomeImageElement.ImageLayout;
      }
      set
      {
        this.welcomeImageElement.ImageLayout = value;
      }
    }

    public RadImageShape WelcomeImageBackgroundShape
    {
      get
      {
        return this.welcomeImageElement.BackgroundShape;
      }
      set
      {
        this.welcomeImageElement.BackgroundShape = value;
      }
    }

    public LightVisualElement CompletionImageElement
    {
      get
      {
        return this.completionImageElement;
      }
    }

    public Image CompletionImage
    {
      get
      {
        return this.completionImage;
      }
      set
      {
        this.completionImage = value;
      }
    }

    public bool HideCompletionImage
    {
      get
      {
        return (bool) this.GetValue(WizardView.HideCompletionImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardView.HideCompletionImageProperty, (object) value);
      }
    }

    public ImageLayout CompletionImageLayout
    {
      get
      {
        return this.completionImageElement.ImageLayout;
      }
      set
      {
        this.completionImageElement.ImageLayout = value;
      }
    }

    public RadImageShape CompletionImageBackgroundShape
    {
      get
      {
        return this.completionImageElement.BackgroundShape;
      }
      set
      {
        this.completionImageElement.BackgroundShape = value;
      }
    }

    public ElementVisibility PageTitleTextVisibility
    {
      get
      {
        return this.pageHeaderElement.TitleVisibility;
      }
      set
      {
        this.pageHeaderElement.TitleVisibility = value;
      }
    }

    public ElementVisibility PageHeaderTextVisibility
    {
      get
      {
        return this.pageHeaderElement.HeaderVisibility;
      }
      set
      {
        this.pageHeaderElement.HeaderVisibility = value;
      }
    }

    public Image PageHeaderIcon
    {
      get
      {
        return this.pageHeaderElement.Icon;
      }
      set
      {
        this.pageHeaderElement.Icon = value;
      }
    }

    public ContentAlignment PageHeaderIconAlignment
    {
      get
      {
        return this.pageHeaderElement.IconAlignment;
      }
      set
      {
        this.pageHeaderElement.IconAlignment = value;
      }
    }

    public virtual RadButtonElement BackButton
    {
      get
      {
        return (RadButtonElement) null;
      }
    }

    public virtual WizardCommandAreaButtonElement NextButton
    {
      get
      {
        return (WizardCommandAreaButtonElement) null;
      }
    }

    public virtual WizardCommandAreaButtonElement CancelButton
    {
      get
      {
        return (WizardCommandAreaButtonElement) null;
      }
    }

    public virtual WizardCommandAreaButtonElement FinishButton
    {
      get
      {
        return (WizardCommandAreaButtonElement) null;
      }
    }

    public virtual LightVisualElement HelpButton
    {
      get
      {
        return (LightVisualElement) null;
      }
    }

    protected internal virtual void AddPages()
    {
      foreach (RadElement page in (Collection<WizardPage>) this.Owner.Pages)
        this.Children.Add(page);
    }

    internal float ArrangeImageElement(
      SizeF finalSize,
      LightVisualElement imageElement,
      float pageHeaderHeight)
    {
      float num1 = imageElement.BackgroundShape != null ? (float) imageElement.BackgroundShape.Image.Size.Width : 0.0f;
      float width = (imageElement.Image != null ? imageElement.DesiredSize.Width : num1) * this.DpiScaleFactor.Width;
      float x = !this.RightToLeft ? 0.0f : finalSize.Width - width;
      float num2 = (double) this.CommandAreaHeight > -1.0 ? this.CommandAreaHeight : this.commandArea.DesiredSize.Height;
      RectangleF finalRect = new RectangleF(x, pageHeaderHeight, width, finalSize.Height - pageHeaderHeight - num2);
      imageElement.Visibility = ElementVisibility.Visible;
      imageElement.Arrange(finalRect);
      return width;
    }

    internal abstract bool SelectFollowingNavigationButton();

    internal abstract bool SelectPreviousNavigationButton();

    internal abstract bool IsLastNavigationButtonFocused();

    internal abstract bool IsFirstNavigationButtonFocused();

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == WizardView.CommandAreaHeightProperty || e.Property == WizardView.PageHeaderHeightProperty || (e.Property == WizardView.HideWelcomeImageProperty || e.Property == WizardView.HideCompletionImageProperty))
        this.Owner.InvalidateMeasure(true);
      base.OnPropertyChanged(e);
    }
  }
}
