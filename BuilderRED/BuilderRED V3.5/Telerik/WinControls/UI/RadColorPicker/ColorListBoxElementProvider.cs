// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ColorListBoxElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.RadColorPicker
{
  public class ColorListBoxElementProvider : ListElementProvider
  {
    public ColorListBoxElementProvider(RadListElement listElement)
      : base(listElement)
    {
    }

    public override IVirtualizedElement<RadListDataItem> CreateElement(
      RadListDataItem data,
      object context)
    {
      return (IVirtualizedElement<RadListDataItem>) new ColorListBoxItem();
    }
  }
}
