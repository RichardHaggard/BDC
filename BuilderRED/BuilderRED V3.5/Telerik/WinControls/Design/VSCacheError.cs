// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.VSCacheError
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls.Design
{
  public class VSCacheError
  {
    private static string msg1 = "Visual Studio is attempting to load class instances from a different assembly than the original used to create your components. This will result in failure to load your designed component. More information that will help you to correct the problem.";
    private static string msg2 = "Please close Visual Studio, remove the errant assembly and try loading your designer again.";
    private static string msg3 = "Ensure that you do not attempt to save any designer that opens with errors, as this can result in loss of work. Note that you may receive this message multiple times, once for each component instance in your designer.";

    public static void ShowVSCacheError(Assembly componentAssembly, Assembly designerAssembly)
    {
      int num = (int) MessageBox.Show(VSCacheError.msg1 + Environment.NewLine + Environment.NewLine + "Component Assembly:" + Environment.NewLine + componentAssembly.Location + Environment.NewLine + Environment.NewLine + "Designer Assembly:" + Environment.NewLine + designerAssembly.Location + Environment.NewLine + Environment.NewLine + VSCacheError.msg2 + Environment.NewLine + Environment.NewLine + VSCacheError.msg3, "Visual Studio Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }
}
