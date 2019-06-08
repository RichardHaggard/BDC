// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewOutlookOverflowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewOutlookOverflowButton : RadPageViewButtonElement
  {
    public static RadProperty OverflowMenuOpenedProperty = RadProperty.Register("OverflowMenuOpened", typeof (bool), typeof (RadPageViewOutlookOverflowButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static RadPageViewOutlookOverflowButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new OverflowButtonStateManager(), typeof (RadPageViewOutlookOverflowButton));
    }
  }
}
