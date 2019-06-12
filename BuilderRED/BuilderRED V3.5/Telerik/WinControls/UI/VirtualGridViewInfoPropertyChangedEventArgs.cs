// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridViewInfoPropertyChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridViewInfoPropertyChangedEventArgs : VirtualGridEventArgs
  {
    private string propertyName;

    public VirtualGridViewInfoPropertyChangedEventArgs(
      VirtualGridViewInfo viewInfo,
      string propertyName)
      : base(viewInfo)
    {
      this.propertyName = propertyName;
    }

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }
  }
}
