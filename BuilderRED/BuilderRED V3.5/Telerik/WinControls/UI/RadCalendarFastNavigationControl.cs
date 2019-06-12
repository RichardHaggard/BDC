// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarFastNavigationControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadCalendarFastNavigationControl : RadControl
  {
    public RadCalendarFastNavigationElement fastNavigationElement;

    public RadCalendarFastNavigationControl()
    {
      this.AutoSize = true;
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.fastNavigationElement.Items;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.fastNavigationElement = new RadCalendarFastNavigationElement();
      this.fastNavigationElement.StretchHorizontally = true;
      this.fastNavigationElement.StretchVertically = true;
      parent.Children.Add((RadElement) this.fastNavigationElement);
      parent.StretchHorizontally = true;
      parent.StretchVertically = true;
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
    }
  }
}
