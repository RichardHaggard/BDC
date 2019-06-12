// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectedPageChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class SelectedPageChangingEventArgs : CancelEventArgs
  {
    public readonly WizardPage SelectedPage;
    public readonly WizardPage NextPage;

    public SelectedPageChangingEventArgs(WizardPage selectedPage, WizardPage nextPage)
    {
      this.SelectedPage = selectedPage;
      this.NextPage = nextPage;
    }
  }
}
