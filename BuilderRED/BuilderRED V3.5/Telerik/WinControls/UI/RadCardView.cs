// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCardView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ComplexBindingProperties("DataSource", "DataMember")]
  [Designer("Telerik.WinControls.UI.Design.RadCardViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "CurrentItem")]
  [TelerikToolboxCategory("Data Controls")]
  [ToolboxItem(true)]
  [Description("Displays a collection of labeled items, each represented by a ListViewDataItem.")]
  [DefaultProperty("Items")]
  [DefaultEvent("SelectedItemChanged")]
  public class RadCardView : RadListView
  {
    public RadCardView()
    {
      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        LicenseManager.Validate(this.GetType());
      this.AllowArbitraryItemHeight = true;
      this.CardViewElement.ViewElement.Orientation = Orientation.Horizontal;
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 250));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadCardViewElement CardViewElement
    {
      get
      {
        return (RadCardViewElement) this.ListViewElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public RadLayoutControl CardTemplate
    {
      get
      {
        return this.CardViewElement.CardTemplate;
      }
    }

    [Description("Gets or sets the default item size.")]
    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(typeof (Size), "200,250")]
    public override Size ItemSize
    {
      get
      {
        return base.ItemSize;
      }
      set
      {
        base.ItemSize = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the space between the items.")]
    [DefaultValue(10)]
    [Category("Layout")]
    public override int ItemSpacing
    {
      get
      {
        return base.ItemSpacing;
      }
      set
      {
        base.ItemSpacing = value;
      }
    }

    [Description("Gets or sets a value indicating whether the items can have different height.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    public override bool AllowArbitraryItemHeight
    {
      get
      {
        return base.AllowArbitraryItemHeight;
      }
      set
      {
        base.AllowArbitraryItemHeight = value;
      }
    }

    [DefaultValue(ListViewType.IconsView)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override ListViewType ViewType
    {
      get
      {
        return this.ListViewElement.ViewType;
      }
      set
      {
        this.ListViewElement.ViewType = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool FullRowSelect
    {
      get
      {
        return base.FullRowSelect;
      }
      set
      {
        base.FullRowSelect = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AllowColumnReorder
    {
      get
      {
        return base.AllowColumnReorder;
      }
      set
      {
        base.AllowColumnReorder = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AllowColumnResize
    {
      get
      {
        return base.AllowColumnResize;
      }
      set
      {
        base.AllowColumnResize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool ShowColumnHeaders
    {
      get
      {
        return base.ShowColumnHeaders;
      }
      set
      {
        base.ShowColumnHeaders = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override float HeaderHeight
    {
      get
      {
        return base.HeaderHeight;
      }
      set
      {
        base.HeaderHeight = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool ShowGridLines
    {
      get
      {
        return base.ShowGridLines;
      }
      set
      {
        base.ShowGridLines = value;
      }
    }

    public void ShowCustomizeDialog()
    {
      this.CardViewElement.ShowCustomizeDialog();
    }

    public void CloseCustomizeDialog()
    {
      this.CardViewElement.CloseCustomizeDialog();
    }

    protected override RadListViewElement CreateListViewElement()
    {
      return (RadListViewElement) new RadCardViewElement();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      this.CardTemplate.ContainerElement.RebuildLayoutTree(true);
      this.CardTemplate.Invalidate();
      this.CardTemplate.UpdateControlsLayout();
      this.CardTemplate.UpdateScrollbars();
      (this.CardViewElement.ViewElement as CardListViewElement).UpdateItemsLayout();
    }

    public event CardViewItemCreatingEventHandler CardViewItemCreating
    {
      add
      {
        this.CardViewElement.CardViewItemCreating += value;
      }
      remove
      {
        this.CardViewElement.CardViewItemCreating -= value;
      }
    }

    public event CardViewItemFormattingEventHandler CardViewItemFormatting
    {
      add
      {
        this.CardViewElement.CardViewItemFormatting += value;
      }
      remove
      {
        this.CardViewElement.CardViewItemFormatting -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override event ListViewCellElementCreatingEventHandler CellCreating
    {
      add
      {
        this.ListViewElement.CellCreating += value;
      }
      remove
      {
        this.ListViewElement.CellCreating -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override event ListViewCellFormattingEventHandler CellFormatting
    {
      add
      {
        this.ListViewElement.CellFormatting += value;
      }
      remove
      {
        this.ListViewElement.CellFormatting -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override event ViewTypeChangingEventHandler ViewTypeChanging
    {
      add
      {
        this.ListViewElement.ViewTypeChanging += value;
      }
      remove
      {
        this.ListViewElement.ViewTypeChanging -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override event EventHandler ViewTypeChanged
    {
      add
      {
        this.ListViewElement.ViewTypeChanged += value;
      }
      remove
      {
        this.ListViewElement.ViewTypeChanged -= value;
      }
    }
  }
}
