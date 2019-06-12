// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ContextualTabGroupsEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class ContextualTabGroupsEditor : UITypeEditor
  {
    private IWindowsFormsEditorService editorService;
    private List<ContextualTabGroup> contextualTabGroups;
    private bool indexChanged;

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
      this.contextualTabGroups = new List<ContextualTabGroup>();
      this.editorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      System.Windows.Forms.ListBox listBox = this.CreateListBox(context, value);
      this.indexChanged = false;
      this.editorService.DropDownControl((Control) listBox);
      if (!this.indexChanged)
        return value;
      if (listBox.SelectedIndex == 0)
        return (object) null;
      object contextualTabGroup = (object) this.contextualTabGroups[listBox.SelectedIndex - 1];
      this.contextualTabGroups.Clear();
      return contextualTabGroup;
    }

    private void listBox_SelectedValueChanged(object sender, EventArgs e)
    {
      this.indexChanged = true;
      if (this.editorService == null)
        return;
      this.editorService.CloseDropDown();
    }

    private System.Windows.Forms.ListBox CreateListBox(
      ITypeDescriptorContext context,
      object value)
    {
      System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
      listBox.SelectedValueChanged += new EventHandler(this.listBox_SelectedValueChanged);
      listBox.Dock = DockStyle.Fill;
      listBox.BorderStyle = BorderStyle.None;
      listBox.ItemHeight = 13;
      listBox.Items.Add((object) "(none)");
      foreach (ContextualTabGroup reference in ((IReferenceService) context.GetService(typeof (IReferenceService))).GetReferences(typeof (ContextualTabGroup)))
      {
        listBox.Items.Add((object) reference.Text);
        this.contextualTabGroups.Add(reference);
        if (value != null && value == reference)
          listBox.SelectedIndex = listBox.Items.Count - 1;
      }
      return listBox;
    }
  }
}
