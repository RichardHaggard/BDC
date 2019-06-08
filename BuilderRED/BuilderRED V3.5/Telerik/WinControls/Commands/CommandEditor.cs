// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.Commands
{
  internal class CommandEditor : UITypeEditor
  {
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }
  }
}
