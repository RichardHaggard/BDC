// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCheckedListVisualItem : RadListVisualItem
  {
    private RadLabelElement label;
    private RadToggleButtonElement checkBox;
    private StackLayoutPanel stackLayoutPanel;

    public RadCheckedListVisualItem()
    {
      this.DrawText = false;
      this.NotifyParentOnMouseInput = false;
    }

    static RadCheckedListVisualItem()
    {
      RadListVisualItem.SynchronizationProperties.Add(RadCheckedListDataItem.CheckedProperty);
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
      this.checkBox = this.CreateCheckBoxElement();
      this.label = this.CreateLabelElement();
      this.checkBox.ToggleStateChanging += new StateChangingEventHandler(this.checkBox_ToggleStateChanging);
      this.label.StretchHorizontally = true;
      this.label.Margin = new Padding(1, 0, 0, 0);
      this.label.NotifyParentOnMouseInput = false;
      this.label.ShouldHandleMouseInput = false;
      this.label.TextAlignment = ContentAlignment.MiddleLeft;
      this.stackLayoutPanel = new StackLayoutPanel();
      this.stackLayoutPanel.Orientation = Orientation.Horizontal;
      this.stackLayoutPanel.Children.Add((RadElement) this.checkBox);
      this.stackLayoutPanel.Children.Add((RadElement) this.label);
      this.Children.Add((RadElement) this.stackLayoutPanel);
    }

    protected virtual RadLabelElement CreateLabelElement()
    {
      return new RadLabelElement();
    }

    protected virtual RadToggleButtonElement CreateCheckBoxElement()
    {
      return (RadToggleButtonElement) new RadCheckBoxElement();
    }

    private void checkBox_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      RadCheckedListDataItem data = (RadCheckedListDataItem) this.Data;
      bool flag = args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On;
      if (data.Checked == flag)
        return;
      data.Checked = flag;
      args.Cancel = data.Checked != flag;
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      if (!(this.Data is RadCheckedListDataItem))
        return;
      RadCheckedListDataItem data = (RadCheckedListDataItem) this.Data;
      this.checkBox.ReadOnly = data.Owner.ReadOnly;
      if (this.checkBox.IsChecked != data.Checked)
      {
        this.checkBox.ToggleStateChanging -= new StateChangingEventHandler(this.checkBox_ToggleStateChanging);
        this.checkBox.IsChecked = data.Checked;
        this.checkBox.ToggleStateChanging += new StateChangingEventHandler(this.checkBox_ToggleStateChanging);
      }
      this.label.Text = data.Text;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (!this.Enabled || this.checkBox.ReadOnly)
        return;
      this.checkBox.IsChecked = !this.checkBox.IsChecked;
    }

    public virtual RadLabelElement Label
    {
      get
      {
        return this.label;
      }
      set
      {
        this.label = value;
      }
    }

    public virtual RadToggleButtonElement CheckBox
    {
      get
      {
        return this.checkBox;
      }
    }

    public virtual StackLayoutPanel StackLayout
    {
      get
      {
        return this.stackLayoutPanel;
      }
    }

    public bool Checked
    {
      get
      {
        return this.checkBox.IsChecked;
      }
      set
      {
        this.checkBox.IsChecked = value;
      }
    }
  }
}
