// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardPageCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.RadWizardPageCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Serializable]
  public class WizardPageCollection : ObservableCollection<WizardPage>
  {
    private RadWizardElement owner;

    public WizardPageCollection(RadWizardElement owner)
    {
      this.owner = owner;
    }

    public RadWizardElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public new void Add(WizardPage page)
    {
      if (this.Owner.Pages.Count > 0)
      {
        if (this.Owner.CompletionPage != null && this.Owner.Pages.Contains((WizardPage) this.Owner.CompletionPage))
          this.Owner.Pages.Insert(this.Owner.Pages.IndexOf((WizardPage) this.Owner.CompletionPage), page);
        else
          this.Owner.Pages.Insert(this.Owner.Pages.Count, page);
      }
      else
        this.Owner.Pages.Insert(0, page);
    }

    protected override void InsertItem(int index, WizardPage item)
    {
      item.Owner = this.owner;
      base.InsertItem(index, item);
      if (item.Owner.View == null || item.Owner.View.Children.Contains((RadElement) item))
        return;
      item.Owner.View.Children.Add((RadElement) item);
    }

    protected override void RemoveItem(int index)
    {
      WizardPage wizardPage = this[index];
      if (wizardPage.Owner != null && wizardPage.Owner.View != null && wizardPage.Owner.View.Children.Contains((RadElement) wizardPage))
        wizardPage.Owner.View.Children.Remove((RadElement) wizardPage);
      base.RemoveItem(index);
      wizardPage.Owner = (RadWizardElement) null;
    }

    protected override void SetItem(int index, WizardPage item)
    {
      base.SetItem(index, item);
      item.Owner = this.owner;
    }
  }
}
