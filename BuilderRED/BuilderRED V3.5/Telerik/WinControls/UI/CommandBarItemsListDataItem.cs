// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarItemsListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CommandBarItemsListDataItem : RadListDataItem
  {
    public static readonly RadProperty VisibleProperty = RadProperty.Register(nameof (Visible), typeof (bool), typeof (CommandBarItemsListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static readonly RadProperty NameProperty = RadProperty.Register(nameof (Name), typeof (string), typeof (CommandBarItemsListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ""));

    public bool Visible
    {
      get
      {
        return (bool) this.GetValue(CommandBarItemsListDataItem.VisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(CommandBarItemsListDataItem.VisibleProperty, (object) value);
      }
    }

    public string Name
    {
      get
      {
        return (string) this.GetValue(CommandBarItemsListDataItem.NameProperty);
      }
      set
      {
        int num = (int) this.SetValue(CommandBarItemsListDataItem.NameProperty, (object) value);
      }
    }

    protected internal override void SetDataBoundItem(bool dataBinding, object value)
    {
      base.SetDataBoundItem(dataBinding, value);
      this.Name = (value as RadCommandBarBaseItem).DisplayName;
      this.Visible = (value as RadCommandBarBaseItem).VisibleInStrip;
      if (!(value is RadElement))
        return;
      (value as RadElement).RadPropertyChanged += new RadPropertyChangedEventHandler(this.ItemsListDataItem_RadPropertyChanged);
    }

    private void ItemsListDataItem_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadCommandBarBaseItem.VisibleInStripProperty)
        this.Visible = (this.DataBoundItem as RadCommandBarBaseItem).VisibleInStrip;
      if (e.Property != RadElement.NameProperty)
        return;
      this.Name = (this.DataBoundItem as RadCommandBarBaseItem).DisplayName;
    }
  }
}
