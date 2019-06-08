// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemValueChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemValueChangingEventArgs : ValueChangingEventArgs
  {
    private PropertyGridItemBase item;

    public PropertyGridItemValueChangingEventArgs(
      PropertyGridItemBase item,
      object newValue,
      object oldValue)
      : base(newValue, oldValue)
    {
      this.item = item;
    }

    public PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
    }

    public PropertyGridTableElement PropertyGridElement
    {
      get
      {
        return this.item.PropertyGridTableElement;
      }
    }
  }
}
