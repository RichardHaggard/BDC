// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IPropertySetting
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public interface IPropertySetting
  {
    RadProperty Property { get; set; }

    object GetCurrentValue(RadObject forObject);

    void ApplyValue(RadObject element);

    void UnapplyValue(RadObject element);
  }
}
