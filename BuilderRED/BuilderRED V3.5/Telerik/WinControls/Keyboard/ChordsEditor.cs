// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordsEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.Keyboard
{
  [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class ChordsEditor : UITypeEditor
  {
    private ChordKeysUI chordKeysUI;

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      if (provider != null)
      {
        IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
        if (service == null)
          return value;
        if (this.chordKeysUI == null)
          this.chordKeysUI = new ChordKeysUI(this);
        this.chordKeysUI.Start(service, value);
        service.DropDownControl((Control) this.chordKeysUI);
        if (this.chordKeysUI.Value != null)
        {
          string chordKeys = (this.chordKeysUI.Value as Chord).ChordKeys;
          value = this.chordKeysUI.Value;
        }
        else if (value is Chord)
          (value as Chord).Keys = "";
        this.chordKeysUI.End();
      }
      return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }
  }
}
