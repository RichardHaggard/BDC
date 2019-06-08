// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IExpression
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public interface IExpression
  {
    object GetValue(RadObject forObject, RadProperty property);

    bool SetValue(RadObject radObject, RadProperty dp, object value);

    void OnDetach(RadObject radObject, RadProperty dp);

    void OnAttach(RadObject radObject, RadProperty dp);
  }
}
