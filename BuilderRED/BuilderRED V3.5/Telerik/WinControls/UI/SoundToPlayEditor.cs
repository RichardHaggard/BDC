// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SoundToPlayEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Media;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class SoundToPlayEditor : UITypeEditor
  {
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
      System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
      listBox.Items.Add((object) new SystemSoundItem(SystemSounds.Exclamation, "Asterisk"));
      listBox.Items.Add((object) new SystemSoundItem(SystemSounds.Exclamation, "Exclamation"));
      listBox.Items.Add((object) new SystemSoundItem(SystemSounds.Exclamation, "Hand"));
      listBox.Items.Add((object) new SystemSoundItem(SystemSounds.Exclamation, "Beep"));
      listBox.Items.Add((object) new SystemSoundItem(SystemSounds.Exclamation, "Question"));
      (provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService).DropDownControl((Control) listBox);
      if (listBox.SelectedIndex == -1)
        return (object) null;
      return (object) ((SystemSoundItem) listBox.SelectedItem).Sound;
    }
  }
}
