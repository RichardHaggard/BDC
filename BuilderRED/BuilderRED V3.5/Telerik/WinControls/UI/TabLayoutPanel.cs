// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  internal class TabLayoutPanel : LayoutPanel
  {
    public static RadProperty ItemsOverlapFactorProperty = RadProperty.Register("ItemsOverlapFactor", typeof (int), typeof (TabLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -2, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty LayoutOverlapProperty = RadProperty.Register("LayoutOverlap", typeof (int), typeof (TabLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
  }
}
