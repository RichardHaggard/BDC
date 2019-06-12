// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBarLocalizationSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public class RibbonBarLocalizationSettings
  {
    private string showQuickAccessMenuBelowItemText = "Show Below the Ribbon";
    private string showQuickAccessMenuAboveItemText = "Show Above the Ribbon";
    private string minimizeRibbonItemText = "Minimize the Ribbon";
    private string maximizeRibbonItemText = "Maximize the Ribbon";
    private RadRibbonBarElement ribbonBarElement;

    public RibbonBarLocalizationSettings(RadRibbonBarElement ribbonBarElement)
    {
      this.ribbonBarElement = ribbonBarElement;
    }

    [DefaultValue("Show Below the Ribbon")]
    [Localizable(true)]
    public string ShowQuickAccessMenuBelowItemText
    {
      get
      {
        return this.showQuickAccessMenuBelowItemText;
      }
      set
      {
        this.showQuickAccessMenuBelowItemText = value;
      }
    }

    [DefaultValue("Show Above the Ribbon")]
    [Localizable(true)]
    public string ShowQuickAccessMenuAboveItemText
    {
      get
      {
        return this.showQuickAccessMenuAboveItemText;
      }
      set
      {
        this.showQuickAccessMenuAboveItemText = value;
      }
    }

    [Localizable(true)]
    [DefaultValue("Minimize the Ribbon")]
    public string MinimizeRibbonItemText
    {
      get
      {
        return this.minimizeRibbonItemText;
      }
      set
      {
        this.minimizeRibbonItemText = value;
      }
    }

    [DefaultValue("Maximize the Ribbon")]
    [Localizable(true)]
    public string MaximizeRibbonItemText
    {
      get
      {
        return this.maximizeRibbonItemText;
      }
      set
      {
        this.maximizeRibbonItemText = value;
      }
    }
  }
}
