// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.IServiceProviderHelper`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.XmlSerialization
{
  public class IServiceProviderHelper<ServiceType> where ServiceType : class
  {
    public static ServiceType GetService(IServiceProvider serviceProvider, string caller)
    {
      ServiceType service = serviceProvider.GetService(typeof (ServiceType)) as ServiceType;
      if ((object) service == null)
        throw new NotSupportedException(string.Format("{0} requires {1} service.", (object) typeof (ColorBlendExtension), (object) typeof (IProvideTargetValue)));
      return service;
    }
  }
}
