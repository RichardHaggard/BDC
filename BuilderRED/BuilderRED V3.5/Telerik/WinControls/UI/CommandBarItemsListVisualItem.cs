// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarItemsListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CommandBarItemsListVisualItem : RadListVisualItem
  {
    private RadLabelElement label = new RadLabelElement();
    private RadCheckBoxElement checkBox = new RadCheckBoxElement();

    static CommandBarItemsListVisualItem()
    {
      RadListVisualItem.SynchronizationProperties.Add(CommandBarItemsListDataItem.VisibleProperty);
      RadListVisualItem.SynchronizationProperties.Add(CommandBarItemsListDataItem.NameProperty);
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
      RadCommandBarBaseItem dataBoundItem = this.Data.DataBoundItem as RadCommandBarBaseItem;
      if (dataBoundItem == null)
        return;
      if (property == CommandBarItemsListDataItem.VisibleProperty || property == CommandBarItemsListDataItem.NameProperty)
      {
        this.checkBox.Checked = dataBoundItem.VisibleInStrip;
        this.label.Text = dataBoundItem.DisplayName;
      }
      this.Text = "";
    }

    private void ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      RadCommandBarBaseItem dataBoundItem = this.Data.DataBoundItem as RadCommandBarBaseItem;
      if (dataBoundItem == null)
        return;
      dataBoundItem.VisibleInStrip = this.checkBox.Checked;
    }
  }
}
