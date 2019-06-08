// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTabStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class RadTabStripElement : RadItem
  {
    public static RadProperty SelectedItemMarginsProperty = RadProperty.Register("SelectedItemMargins", typeof (Padding), typeof (RadTabStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemMarginsProperty = RadProperty.Register("ItemMargins", typeof (Padding), typeof (RadTabStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemsOffsetProperty = RadProperty.Register("ItemsOffset", typeof (int), typeof (RadTabStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 30));
    public static RadProperty ScrollItemsOffsetProperty = RadProperty.Register("ScroollItemsOffset", typeof (int), typeof (RadTabStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
  }
}
