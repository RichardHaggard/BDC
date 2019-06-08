// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ColorListBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI.RadColorPicker
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ColorListBox : RadListControl
  {
    public override string ThemeClassName
    {
      get
      {
        return typeof (RadListControl).FullName;
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnSelectedValueChanged(
      object sender,
      int newIndex,
      object newValue,
      object oldValue)
    {
      base.OnSelectedValueChanged(sender, newIndex, newValue, oldValue);
      if (this.ColorChanged == null || this.SelectedItem == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(Color.FromName(this.SelectedItem.Text)));
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.ListElement.ViewElement.ElementProvider = (IVirtualizedElementProvider<RadListDataItem>) new ColorListBoxElementProvider(this.ListElement);
      this.ListElement.ItemHeight = 24;
      this.EnableAnalytics = false;
    }
  }
}
