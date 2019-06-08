// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DescriptionContentListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class DescriptionContentListVisualItem : LightVisualElement
  {
    public static RadProperty ActiveProperty = RadProperty.Register(nameof (Active), typeof (bool), typeof (DescriptionContentListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (DescriptionContentListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static DescriptionContentListVisualItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DescriptionContentListVisualItemStateManager(), typeof (DescriptionContentListVisualItem));
    }

    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(DescriptionContentListVisualItem.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(DescriptionContentListVisualItem.SelectedProperty, (object) value);
      }
    }

    public bool Active
    {
      get
      {
        return (bool) this.GetValue(DescriptionContentListVisualItem.ActiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(DescriptionContentListVisualItem.ActiveProperty, (object) value);
      }
    }
  }
}
