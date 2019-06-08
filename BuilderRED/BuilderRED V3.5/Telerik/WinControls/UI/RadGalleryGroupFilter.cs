// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryGroupFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class RadGalleryGroupFilter : RadItem
  {
    private RadItemOwnerCollection items;
    private bool selected;
    private RadGalleryElement owner;

    public RadGalleryGroupFilter()
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new Type[1]
      {
        typeof (RadGalleryGroupItem)
      };
    }

    public RadGalleryGroupFilter(string text)
    {
      this.Text = text;
    }

    [Category("Data")]
    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection representing the group items contained in this gallery filter.")]
    [Editor(typeof (FilteredGalleryGroupsEditor), typeof (UITypeEditor))]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    internal RadGalleryElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    [Browsable(false)]
    [Description("Returns whether the filter is currently selected.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Selected
    {
      get
      {
        return this.selected;
      }
      set
      {
        this.selected = value;
      }
    }

    public override string ToString()
    {
      if (this.Site != null)
        return this.Site.Name;
      return base.ToString();
    }
  }
}
