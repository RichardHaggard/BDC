// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ReplaceRadControlProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  internal class ReplaceRadControlProvider : TypeDescriptionProvider
  {
    public ReplaceRadControlProvider()
      : base(TypeDescriptor.GetProvider(typeof (RadControl)))
    {
    }

    public override System.Type GetReflectionType(System.Type objectType, object instance)
    {
      if ((object) objectType == (object) typeof (RadControl))
        return typeof (Control);
      return base.GetReflectionType(objectType, instance);
    }

    public override object CreateInstance(
      System.IServiceProvider provider,
      System.Type objectType,
      System.Type[] argTypes,
      object[] args)
    {
      if ((object) objectType == (object) typeof (RadControl))
        objectType = typeof (Control);
      return base.CreateInstance(provider, objectType, argTypes, args);
    }
  }
}
