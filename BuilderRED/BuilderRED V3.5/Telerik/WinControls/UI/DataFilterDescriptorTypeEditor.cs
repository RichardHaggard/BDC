// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterDescriptorTypeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class DataFilterDescriptorTypeEditor : UITypeEditor
  {
    private IWindowsFormsEditorService service;

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      this.service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
      listBox.SelectionMode = SelectionMode.One;
      listBox.SelectedValueChanged += new EventHandler(this.ListBox_SelectedValueChanged);
      listBox.DisplayMember = "Name";
      listBox.Items.Add((object) "String");
      listBox.Items.Add((object) "Int");
      listBox.Items.Add((object) "Decimal");
      listBox.Items.Add((object) "Bool");
      listBox.Items.Add((object) "DateTime");
      this.service.DropDownControl((Control) listBox);
      if (listBox.SelectedItem == null)
        return value;
      switch (listBox.SelectedItem.ToString())
      {
        case "Int":
          return (object) typeof (int);
        case "DateTime":
          return (object) typeof (DateTime);
        case "Decimal":
          return (object) typeof (Decimal);
        case "Bool":
          return (object) typeof (bool);
        default:
          return (object) typeof (string);
      }
    }

    private void ListBox_SelectedValueChanged(object sender, EventArgs e)
    {
      this.service.CloseDropDown();
    }
  }
}
