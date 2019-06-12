// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardWelcomePage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class WizardWelcomePage : WizardPage
  {
    private Image welcomeImage;

    [Description("Gets or sets the Welcome page image.")]
    [Category("Header")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter(typeof (ImageTypeConverter))]
    [DefaultValue(null)]
    public Image WelcomeImage
    {
      get
      {
        return this.welcomeImage;
      }
      set
      {
        this.welcomeImage = value;
        if (this.Owner == null)
          return;
        this.Owner.UpdateImageElements((WizardPage) null);
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (WizardPage);
      }
    }
  }
}
