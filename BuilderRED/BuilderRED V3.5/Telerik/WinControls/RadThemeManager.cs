// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadThemeManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItemFilter("System.Windows.Forms")]
  [TelerikToolboxCategory("Themes")]
  [Designer("Telerik.WinControls.UI.Design.RadThemeManagerDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadThemeManager : Component
  {
    private ThemeSourceCollection loadedThemes;
    internal bool IsDesignMode;

    public RadThemeManager()
    {
      this.loadedThemes = new ThemeSourceCollection(this);
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    protected override void Dispose(bool disposing)
    {
      if (this.Site != null)
        return;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (ThemeSource loadedTheme in this.loadedThemes)
      {
        if (loadedTheme.theme != null && !string.IsNullOrEmpty(loadedTheme.theme.Name) && !dictionary.ContainsKey(loadedTheme.theme.Name))
          dictionary.Add(loadedTheme.theme.Name, loadedTheme.theme.Name);
      }
      foreach (string themeName in dictionary.Values)
        ThemeRepository.Remove(themeName);
      base.Dispose(disposing);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ThemeSourceCollection LoadedThemes
    {
      get
      {
        return this.loadedThemes;
      }
    }

    public override ISite Site
    {
      get
      {
        return base.Site;
      }
      set
      {
        base.Site = value;
        if (this.Site == null)
          return;
        this.IsDesignMode = this.Site.DesignMode;
      }
    }
  }
}
