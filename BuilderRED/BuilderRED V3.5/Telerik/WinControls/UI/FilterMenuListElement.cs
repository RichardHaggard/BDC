// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class FilterMenuListElement : LightVisualElement
  {
    private RadListControl listControl;
    private RadHostItem hostItem;

    public RadListControl ListControl
    {
      get
      {
        return this.listControl;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BorderGradientStyle = GradientStyles.Solid;
      this.BorderColor = Color.FromArgb(156, 189, 232);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.listControl = new RadListControl();
      this.listControl.SelectionMode = SelectionMode.MultiSimple;
      this.hostItem = new RadHostItem((Control) this.listControl);
      this.Children.Add((RadElement) this.hostItem);
    }
  }
}
