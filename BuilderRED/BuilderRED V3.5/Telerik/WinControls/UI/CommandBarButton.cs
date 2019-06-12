// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("Click")]
  public class CommandBarButton : RadCommandBarBaseItem
  {
    static CommandBarButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (CommandBarButton));
    }

    public CommandBarButton()
    {
      this.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DefaultButton;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawText = false;
      this.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DefaultButton;
    }

    protected override void OnOrientationChanged(EventArgs e)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        int num1 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) TextImageRelation.ImageAboveText);
      }
      else
      {
        int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) TextImageRelation.ImageBeforeText);
      }
      int num3 = (int) this.SetDefaultValueOverride(RadItem.TextOrientationProperty, (object) this.Orientation);
      this.InvalidateMeasure(true);
      base.OnOrientationChanged(e);
    }
  }
}
