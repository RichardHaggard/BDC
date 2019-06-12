// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FastNavigationItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class FastNavigationItem : CalendarVisualElement
  {
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (SelectedProperty), typeof (bool), typeof (FastNavigationItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));

    public FastNavigationItem(RadCalendar calendar, CalendarView view)
      : base(calendar, view)
    {
    }

    static FastNavigationItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new FatNavigationItemItemStateManagerFactory(), typeof (FastNavigationItem));
    }

    [Description("Indicates that current element selected.")]
    [Category("Behavior")]
    [RadPropertyDefaultValue("SelectedProperty", typeof (FastNavigationItem))]
    public virtual bool Selected
    {
      get
      {
        return (bool) this.GetValue(FastNavigationItem.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(FastNavigationItem.SelectedProperty, (object) value);
      }
    }
  }
}
