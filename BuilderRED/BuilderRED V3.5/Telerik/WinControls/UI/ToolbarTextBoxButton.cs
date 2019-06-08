// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToolbarTextBoxButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToolbarTextBoxButton : RadButtonElement
  {
    public static RadProperty IsSearchingProperty = RadProperty.Register(nameof (IsSearching), typeof (bool), typeof (ToolbarTextBoxButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static ToolbarTextBoxButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new SearchBarTextBoxButtonStateManager(), typeof (ToolbarTextBoxButton));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(15, 15);
    }

    public bool IsSearching
    {
      get
      {
        return (bool) this.GetValue(ToolbarTextBoxButton.IsSearchingProperty);
      }
      set
      {
        int num = (int) this.SetValue(ToolbarTextBoxButton.IsSearchingProperty, (object) value);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (!this.IsSearching)
        return;
      base.OnMouseDown(e);
      ToolbarTextBoxElement ancestor = this.FindAncestor<ToolbarTextBoxElement>();
      if (ancestor == null)
        return;
      ancestor.Text = string.Empty;
    }
  }
}
