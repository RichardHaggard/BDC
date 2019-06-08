// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  public class RadPopupContainer : RadScrollablePanel
  {
    public RadPopupContainer()
    {
      this.SetStyle(ControlStyles.Selectable, true);
      this.TabStop = true;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadScrollablePanel).FullName;
      }
    }
  }
}
