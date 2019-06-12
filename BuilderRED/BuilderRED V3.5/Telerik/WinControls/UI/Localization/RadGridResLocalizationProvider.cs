// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Localization.RadGridResLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Globalization;
using System.Resources;

namespace Telerik.WinControls.UI.Localization
{
  public class RadGridResLocalizationProvider : RadGridLocalizationProvider
  {
    private ResourceManager resourceManager;

    public RadGridResLocalizationProvider()
    {
      this.CreateResourceManager();
    }

    protected virtual void CreateResourceManager()
    {
      if (this.resourceManager != null)
        this.resourceManager.ReleaseAllResources();
      this.resourceManager = new ResourceManager("Telerik.WinControls.UI.Code.Localization.RadGridLocalizationStrings", typeof (RadGridResLocalizationProvider).Assembly);
    }

    protected virtual ResourceManager Manager
    {
      get
      {
        return this.resourceManager;
      }
    }

    public override CultureInfo Culture
    {
      get
      {
        return CultureInfo.CurrentUICulture;
      }
    }

    public override string GetLocalizedString(string id)
    {
      return this.resourceManager.GetString("RadGridStringId." + id) ?? "";
    }
  }
}
