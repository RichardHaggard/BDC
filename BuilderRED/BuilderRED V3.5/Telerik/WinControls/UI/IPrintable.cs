// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IPrintable
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing.Printing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IPrintable
  {
    int BeginPrint(RadPrintDocument sender, PrintEventArgs args);

    bool EndPrint(RadPrintDocument sender, PrintEventArgs args);

    bool PrintPage(int pageNumber, RadPrintDocument sender, PrintPageEventArgs args);

    Form GetSettingsDialog(RadPrintDocument document);
  }
}
