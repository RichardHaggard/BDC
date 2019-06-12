// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedListBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Description("Displays a flat collection of labeled items with checkbox, each represented by a ListViewDataItem.")]
  [TelerikToolboxCategory("Data Controls")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadListViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DefaultEvent("SelectedItemChanged")]
  [ComplexBindingProperties("DataSource", "DataMember")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "CurrentItem")]
  [DefaultProperty("Items")]
  public class RadCheckedListBox : RadListView
  {
    public RadCheckedListBox()
    {
      base.ShowCheckBoxes = true;
      base.AllowEdit = false;
      base.CheckOnClickMode = CheckOnClickMode.SecondClick;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadListView).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets value indicating whether checkboxes should be shown.")]
    [Browsable(false)]
    [DefaultValue(true)]
    public override bool ShowCheckBoxes
    {
      get
      {
        return base.ShowCheckBoxes;
      }
      set
      {
        base.ShowCheckBoxes = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Gets or sets value indicating whether editing is enabled.")]
    [Category("Behavior")]
    public override bool AllowEdit
    {
      get
      {
        return base.AllowEdit;
      }
      set
      {
        base.AllowEdit = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(CheckOnClickMode.SecondClick)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the item's check state changes whenever the item is clicked.")]
    public override CheckOnClickMode CheckOnClickMode
    {
      get
      {
        return base.CheckOnClickMode;
      }
      set
      {
        base.CheckOnClickMode = value;
      }
    }
  }
}
