// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripPinButtonItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TabStripPinButtonItem : TabStripButtonItem
  {
    public static RadProperty IsPinnedProperty = RadProperty.Register(nameof (IsPinned), typeof (bool), typeof (TabStripPinButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty IsPreviewProperty = RadProperty.Register(nameof (IsPreview), typeof (bool), typeof (TabStripPinButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsPinned
    {
      get
      {
        return (bool) this.GetValue(TabStripPinButtonItem.IsPinnedProperty);
      }
      set
      {
        int num = (int) this.SetValue(TabStripPinButtonItem.IsPinnedProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsPreview
    {
      get
      {
        return (bool) this.GetValue(TabStripPinButtonItem.IsPreviewProperty);
      }
      set
      {
        int num = (int) this.SetValue(TabStripPinButtonItem.IsPreviewProperty, (object) value);
      }
    }

    static TabStripPinButtonItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TabStripPinButtonItemStateManager(), typeof (TabStripPinButtonItem));
    }
  }
}
