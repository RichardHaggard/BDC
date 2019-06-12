// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarElementWithOrientation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TrackBarElementWithOrientation : TrackBarVisualElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (TrackBarElementWithOrientation), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsTheme));

    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(TrackBarElementWithOrientation.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetDefaultValueOverride(TrackBarElementWithOrientation.IsVerticalProperty, (object) value);
      }
    }
  }
}
