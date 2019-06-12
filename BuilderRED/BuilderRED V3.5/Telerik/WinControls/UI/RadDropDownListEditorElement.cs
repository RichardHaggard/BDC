// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class RadDropDownListEditorElement : RadDropDownListElement
  {
    public RadDropDownListEditorElement()
    {
      this.PopupForm.FadeAnimationType = FadeAnimationType.None;
    }

    protected override AutoCompleteAppendHelper CreateAutoCompleteAppendHandler()
    {
      return (AutoCompleteAppendHelper) new RadDropDownListEditorAutoCompleteAppendHelper(this);
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadDropDownListElement);
      }
    }

    public event KeyEventHandler HandleKeyDown;

    protected override void ProcessKeyDown(object sender, KeyEventArgs e)
    {
      if (this.HandleKeyDown != null)
      {
        this.HandleKeyDown(sender, e);
        if (e.Handled)
          return;
      }
      base.ProcessKeyDown(sender, e);
    }

    public event KeyEventHandler HandleKeyUp;

    protected override void ProcessKeyUp(object sender, KeyEventArgs e)
    {
      if (this.HandleKeyUp != null)
      {
        this.HandleKeyUp(sender, e);
        if (e.Handled)
          return;
      }
      base.ProcessKeyUp(sender, e);
    }
  }
}
