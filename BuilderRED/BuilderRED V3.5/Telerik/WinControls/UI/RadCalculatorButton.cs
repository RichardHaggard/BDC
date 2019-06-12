// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCalculatorButton : LightVisualElement
  {
    static RadCalculatorButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadCalculatorButton));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipText = true;
    }

    public RadCalculatorButton(string text)
    {
      this.Text = text;
      this.MouseLeave += new EventHandler(this.RadCalculatorButton_MouseLeave);
    }

    protected override void DisposeManagedResources()
    {
      this.MouseLeave -= new EventHandler(this.RadCalculatorButton_MouseLeave);
    }

    private void RadCalculatorButton_MouseLeave(object sender, EventArgs e)
    {
      this.IsMouseDown = false;
    }
  }
}
