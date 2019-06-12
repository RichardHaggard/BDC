// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.DesignTimeInstanceFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Telerik.WinControls.XmlSerialization
{
  public class DesignTimeInstanceFactory : RuntimeInstanceFactory
  {
    private IDesignerHost designerHost;

    public DesignTimeInstanceFactory(IDesignerHost designerHost)
    {
      this.designerHost = designerHost;
      if (this.designerHost == null)
        throw new ArgumentNullException(nameof (designerHost));
    }

    public IDesignerHost DesignerHost
    {
      get
      {
        return this.designerHost;
      }
    }

    public override object CreateInstance(Type instanceType)
    {
      if (typeof (IComponent).IsAssignableFrom(instanceType))
        return (object) this.designerHost.CreateComponent(instanceType);
      return base.CreateInstance(instanceType);
    }
  }
}
