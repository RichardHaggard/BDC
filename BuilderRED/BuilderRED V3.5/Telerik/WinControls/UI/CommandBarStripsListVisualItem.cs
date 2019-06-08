// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarStripsListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CommandBarStripsListVisualItem : RadListVisualItem
  {
    private RadLabelElement label = new RadLabelElement();
    private RadCheckBoxElement checkBox = new RadCheckBoxElement();

    static CommandBarStripsListVisualItem()
    {
      RadListVisualItem.SynchronizationProperties.Add(CommandBarStripsListDataItem.VisibleProperty);
      RadListVisualItem.SynchronizationProperties.Add(CommandBarStripsListDataItem.NameProperty);
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListVisualItem);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBox.ToggleStateChanged += new StateChangedEventHandler(this.ToggleStateChanged);
      this.label.StretchHorizontally = true;
      this.label.Alignment = ContentAlignment.MiddleLeft;
      this.label.TextAlignment = ContentAlignment.MiddleLeft;
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Horizontal;
      stackLayoutPanel.Children.Add((RadElement) this.checkBox);
      stackLayoutPanel.Children.Add((RadElement) this.label);
      this.Children.Add((RadElement) stackLayoutPanel);
    }

    protected override void PropertySynchronized(RadProperty property)
    {
      CommandBarStripsListDataItem data = (CommandBarStripsListDataItem) this.Data;
      if (property == CommandBarStripsListDataItem.VisibleProperty || property == CommandBarStripsListDataItem.NameProperty)
      {
        this.checkBox.Checked = data.Visible;
        this.label.Text = data.Name;
      }
      this.Text = "";
    }

    private void ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      CommandBarStripElement dataBoundItem = this.Data.DataBoundItem as CommandBarStripElement;
      if (dataBoundItem == null)
        return;
      dataBoundItem.VisibleInCommandBar = this.checkBox.Checked;
    }
  }
}
