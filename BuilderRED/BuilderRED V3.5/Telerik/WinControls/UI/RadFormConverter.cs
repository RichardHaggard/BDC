// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadFormConverterComponentDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadFormConverter : Component
  {
    public RadFormConverter()
    {
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }
  }
}
