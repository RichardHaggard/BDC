// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripButtonItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TabStripButtonItem : RadImageButtonElement
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (TabStripButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(TabStripButtonItem.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(TabStripButtonItem.IsSelectedProperty, (object) value);
      }
    }

    static TabStripButtonItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TabStripButtonItemStateManager(), typeof (TabStripButtonItem));
    }
  }
}
