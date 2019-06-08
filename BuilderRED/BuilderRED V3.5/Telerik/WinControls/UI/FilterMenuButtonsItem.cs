// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuButtonsItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class FilterMenuButtonsItem : RadMenuItemBase
  {
    private RadButtonElement buttonOk;
    private RadButtonElement buttonCancel;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(0, 5, 4, 5);
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.buttonOk = new RadButtonElement();
      this.buttonOk.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuButtonOK");
      this.buttonOk.Padding = new Padding(20, 0, 20, 0);
      this.buttonCancel = new RadButtonElement();
      this.buttonCancel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuButtonCancel");
      this.buttonCancel.Margin = new Padding(2, 0, 2, 0);
      this.buttonCancel.Padding = new Padding(10, 0, 10, 0);
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.Alignment = ContentAlignment.MiddleRight;
      stackLayoutElement.Children.Add((RadElement) this.buttonOk);
      stackLayoutElement.Children.Add((RadElement) this.buttonCancel);
      this.Children.Add((RadElement) stackLayoutElement);
    }

    public RadButtonElement ButtonOK
    {
      get
      {
        return this.buttonOk;
      }
    }

    public RadButtonElement ButtonCancel
    {
      get
      {
        return this.buttonCancel;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (!this.RightToLeft)
        return;
      this.Margin = new Padding(3, 0, 0, 0);
    }
  }
}
