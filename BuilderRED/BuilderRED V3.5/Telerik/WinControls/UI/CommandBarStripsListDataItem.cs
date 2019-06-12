// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarStripsListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CommandBarStripsListDataItem : RadListDataItem
  {
    public static readonly RadProperty VisibleProperty = RadProperty.Register(nameof (Visible), typeof (bool), typeof (CommandBarStripsListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static readonly RadProperty NameProperty = RadProperty.Register(nameof (Name), typeof (string), typeof (CommandBarStripsListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ""));

    public bool Visible
    {
      get
      {
        return (bool) this.GetValue(CommandBarStripsListDataItem.VisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(CommandBarStripsListDataItem.VisibleProperty, (object) value);
      }
    }

    public string Name
    {
      get
      {
        return (string) this.GetValue(CommandBarStripsListDataItem.NameProperty);
      }
      set
      {
        int num = (int) this.SetValue(CommandBarStripsListDataItem.NameProperty, (object) value);
      }
    }

    protected internal override void SetDataBoundItem(bool dataBinding, object value)
    {
      base.SetDataBoundItem(dataBinding, value);
      CommandBarStripElement commandBarStripElement = value as CommandBarStripElement;
      if (value == null)
        return;
      this.Name = commandBarStripElement.DisplayName;
      this.Visible = commandBarStripElement.VisibleInCommandBar;
      commandBarStripElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.stripElement_RadPropertyChanged);
    }

    private void stripElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property == CommandBarStripElement.VisibleInCommandBarProperty)
      {
        CommandBarStripElement dataBoundItem = this.DataBoundItem as CommandBarStripElement;
        if (dataBoundItem != null)
          this.Visible = dataBoundItem.VisibleInCommandBar;
      }
      if (e.Property != RadElement.NameProperty)
        return;
      CommandBarStripElement dataBoundItem1 = this.DataBoundItem as CommandBarStripElement;
      if (dataBoundItem1 == null)
        return;
      this.Name = dataBoundItem1.DisplayName;
    }
  }
}
