// Decompiled with JetBrains decompiler
// Type: BuilderRED.My.MySettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace BuilderRED.My
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal sealed class MySettings : ApplicationSettingsBase
  {
    private static MySettings defaultInstance = (MySettings) SettingsBase.Synchronized((SettingsBase) new MySettings());

    public static MySettings Default
    {
      get
      {
        MySettings defaultInstance = MySettings.defaultInstance;
        return defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool OpenLastFile
    {
      get
      {
        return Conversions.ToBoolean(this[nameof (OpenLastFile)]);
      }
      set
      {
        this[nameof (OpenLastFile)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool HideAlertForms
    {
      get
      {
        return Conversions.ToBoolean(this[nameof (HideAlertForms)]);
      }
      set
      {
        this[nameof (HideAlertForms)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string LastOpened
    {
      get
      {
        return Conversions.ToString(this[nameof (LastOpened)]);
      }
      set
      {
        this[nameof (LastOpened)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool AssessmentLookupsLoaded
    {
      get
      {
        return Conversions.ToBoolean(this[nameof (AssessmentLookupsLoaded)]);
      }
      set
      {
        this[nameof (AssessmentLookupsLoaded)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string ImageLinkSite
    {
      get
      {
        return Conversions.ToString(this[nameof (ImageLinkSite)]);
      }
      set
      {
        this[nameof (ImageLinkSite)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0, 0")]
    public Size AddSectionFormSize
    {
      get
      {
        object obj = this[nameof (AddSectionFormSize)];
        return obj != null ? (Size) obj : new Size();
      }
      set
      {
        this[nameof (AddSectionFormSize)] = (object) value;
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    [DefaultSettingValue("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Users\\RDCERPKI\\Documents\\SMS3\\Source\\BuilderRED\\Database\\bred.mdb\";Jet OLEDB:Database Password=fidelity")]
    public string bredConnectionString
    {
      get
      {
        return Conversions.ToString(this[nameof (bredConnectionString)]);
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    [DefaultSettingValue("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Database\\lookup.USDA.mdb;Persist Security Info=True;Jet OLEDB:Database Password=fidelity")]
    public string lookup_USDAConnectionString
    {
      get
      {
        return Conversions.ToString(this[nameof (lookup_USDAConnectionString)]);
      }
    }
  }
}
