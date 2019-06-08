// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ParameterReferenceExtension
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.XmlSerialization
{
  public class ParameterReferenceExtension : XmlSerializerExtention, IValueProvider
  {
    private string themePropertyName;
    private IPropertiesProvider styleParameterSerivce;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      IProvideTargetValue service = IServiceProviderHelper<IProvideTargetValue>.GetService(serviceProvider, typeof (ParameterReferenceExtension).ToString());
      this.styleParameterSerivce = IServiceProviderHelper<IPropertiesProvider>.GetService(serviceProvider, typeof (ParameterReferenceExtension).ToString());
      this.themePropertyName = service.SourceValue.Trim();
      if (string.IsNullOrEmpty(this.themePropertyName))
        throw new InvalidOperationException("The first argument of RelativeColor exptrssion should be the name of the ThemeProperty");
      return (object) this;
    }

    public object OriginalValue
    {
      get
      {
        return this.styleParameterSerivce.GetPropertyValue(this.themePropertyName);
      }
    }

    public object GetValue()
    {
      return this.styleParameterSerivce.GetPropertyValue(this.themePropertyName);
    }
  }
}
