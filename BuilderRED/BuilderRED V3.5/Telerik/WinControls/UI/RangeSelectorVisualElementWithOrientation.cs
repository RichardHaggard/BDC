// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorVisualElementWithOrientation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorVisualElementWithOrientation : RangeSelectorVisualElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (RangeSelectorVisualElementWithOrientation), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsTheme));

    static RangeSelectorVisualElementWithOrientation()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RangeSelectorVisualElementStateManager(), typeof (RangeSelectorVisualElementWithOrientation));
    }

    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(RangeSelectorVisualElementWithOrientation.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetDefaultValueOverride(RangeSelectorVisualElementWithOrientation.IsVerticalProperty, (object) value);
      }
    }
  }
}
