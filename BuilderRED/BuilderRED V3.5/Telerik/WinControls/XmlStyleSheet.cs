// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlStyleSheet
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class XmlStyleSheet : XmlBuilderData
  {
    private XmlPropertySettingGroupCollection propertySettingGroups;
    private string themeLocation;
    private string themeName;

    public XmlStyleSheet()
    {
    }

    public XmlStyleSheet(StyleSheet style)
    {
      this.propertySettingGroups = new XmlPropertySettingGroupCollection(style.PropertySettingGroups.Count);
      int num = 0;
      while (num < style.PropertySettingGroups.Count)
        ++num;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public XmlPropertySettingGroupCollection PropertySettingGroups
    {
      get
      {
        if (this.propertySettingGroups == null)
          this.propertySettingGroups = new XmlPropertySettingGroupCollection();
        return this.propertySettingGroups;
      }
    }

    public string ThemeLocation
    {
      get
      {
        return this.themeLocation;
      }
    }

    internal void SetThemeLocation(string themeLocation)
    {
      this.themeLocation = themeLocation;
    }

    internal void SetThemeName(string themeName)
    {
      this.themeName = themeName;
    }
  }
}
