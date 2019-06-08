// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ListViewDataItemGroupTypeConverter))]
  public class ListViewDataItemGroup : ListViewDataItem
  {
    protected const int ExpandedState = 64;
    private ListViewGroupedItemsCollection items;
    private Telerik.WinControls.Data.Group<ListViewDataItem> dataGroup;

    public ListViewDataItemGroup()
    {
      this.items = new ListViewGroupedItemsCollection();
      this.bitState[64] = true;
    }

    public ListViewDataItemGroup(string text)
      : this()
    {
      this.Text = text;
    }

    [Browsable(false)]
    public override bool IsDataBound
    {
      get
      {
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ListViewDataItemGroup Group
    {
      get
      {
        return (ListViewDataItemGroup) null;
      }
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ListViewSubDataItemCollection SubItems
    {
      get
      {
        return (ListViewSubDataItemCollection) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ToggleState CheckState
    {
      get
      {
        return ToggleState.Off;
      }
      set
      {
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the group's items should be displayed.")]
    public bool Expanded
    {
      get
      {
        return this.bitState[64];
      }
      set
      {
        if (this.Expanded == value || this.OnNotifyPropertyChanging(nameof (Expanded)))
          return;
        this.bitState[64] = value;
        this.OnNotifyPropertyChanged(nameof (Expanded));
      }
    }

    [Browsable(false)]
    public override bool Selected
    {
      get
      {
        return base.Selected;
      }
      internal set
      {
        if (value && this.Owner.MultiSelect)
        {
          bool flag = this.Items.Count > 0;
          foreach (ListViewDataItem listViewDataItem in this.Items)
          {
            listViewDataItem.Selected = true;
            flag &= listViewDataItem.Selected;
          }
          base.Selected = flag;
        }
        else
          base.Selected = false;
      }
    }

    [Browsable(false)]
    [Description("Gets the items in this group.")]
    public ListViewGroupedItemsCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Browsable(false)]
    public Telerik.WinControls.Data.Group<ListViewDataItem> DataGroup
    {
      get
      {
        return this.dataGroup;
      }
      internal set
      {
        if (this.dataGroup == value)
          return;
        this.SetDataGroup(value);
      }
    }

    internal virtual void OnItemSelectedChanged()
    {
      if (!this.Owner.MultiSelect)
      {
        base.Selected = false;
      }
      else
      {
        bool flag = this.Items.Count > 0;
        foreach (ListViewDataItem listViewDataItem in this.Items)
          flag &= listViewDataItem.Selected;
        if (!this.Selected && flag)
          base.Selected = true;
        if (!this.Selected || flag)
          return;
        base.Selected = false;
      }
    }

    private void SetDataGroup(Telerik.WinControls.Data.Group<ListViewDataItem> value)
    {
      this.ClearUnboundItems();
      this.dataGroup = value;
      if (this.dataGroup != null)
      {
        this.items.InnerList = this.dataGroup.GetItems();
        this.Text = value.Header;
        foreach (ListViewDataItem listViewDataItem in this.items)
          listViewDataItem.SetGroupCore(this, false);
      }
      else
      {
        this.items.ResetListSource();
        this.Text = string.Empty;
      }
    }

    private void ClearUnboundItems()
    {
      while (this.items.Count > 0)
        this.items[0].Group = (ListViewDataItemGroup) null;
      this.items.InnerList.Clear();
    }

    protected override bool OnNotifyPropertyChanging(PropertyChangingEventArgsEx args)
    {
      base.OnNotifyPropertyChanging(args);
      if (args.PropertyName == "Expanded" && this.Owner != null)
        args.Cancel |= this.Owner.OnGroupExpanding(this);
      return args.Cancel;
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      if (args.PropertyName == "Owner")
      {
        foreach (ListViewDataItem listViewDataItem in this.Items)
          listViewDataItem.Owner = this.Owner;
      }
      base.OnNotifyPropertyChanged(args);
      if (!(args.PropertyName == "Expanded") || this.Owner == null)
        return;
      this.Owner.OnGroupExpanded(this);
    }

    public override void Dispose()
    {
      this.ClearUnboundItems();
      this.items.Dispose();
      base.Dispose();
    }
  }
}
