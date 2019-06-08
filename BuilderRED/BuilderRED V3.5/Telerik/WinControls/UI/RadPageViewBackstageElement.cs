// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewBackstageElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadPageViewBackstageElement : RadPageViewStripElement
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StripAlignment = this.RightToLeft ? StripViewAlignment.Right : StripViewAlignment.Left;
      (this.ItemsParent as RadPageViewElementBase).SetBorderAndFillOrientation(this.RightToLeft ? PageViewContentOrientation.Horizontal180 : PageViewContentOrientation.Horizontal, false);
      this.OnPropertyChanged(new RadPropertyChangedEventArgs(RadPageViewStripElement.StripAlignmentProperty, RadPageViewStripElement.StripAlignmentProperty.GetMetadata((RadObject) this), (object) StripViewAlignment.Top, (object) StripViewAlignment.Left));
      this.ItemContentOrientation = PageViewContentOrientation.Horizontal;
      this.ContentArea.BackColor = Color.White;
      this.ContentArea.Shape = (ElementShape) null;
      this.ContentArea.BorderBoxStyle = BorderBoxStyle.FourBorders;
      this.ContentArea.BorderTopWidth = 0.0f;
      this.ContentArea.BorderRightWidth = 0.0f;
      this.ContentArea.BorderBottomWidth = 0.0f;
      this.ContentArea.BorderLeftWidth = 1f;
      this.ContentArea.BorderLeftColor = Color.Black;
      this.ContentArea.DrawBorder = true;
      this.BackColor = Color.White;
      this.ItemContainer.ButtonsPanel.Visibility = ElementVisibility.Collapsed;
      this.ItemContainer.MinSize = new Size(200, 0);
      this.ItemFitMode |= StripViewItemFitMode.FillHeight;
    }

    internal override void SelectItem(RadPageViewItem item)
    {
      if (item != null && item.Page.Site == null && item.Page is RadPageViewItemPage)
        return;
      base.SelectItem(item);
    }

    protected override bool CanSelectItem(RadPageViewItem item)
    {
      if (base.CanSelectItem(item))
        return !(item.Page is RadPageViewItemPage);
      return false;
    }

    protected internal override void CloseItem(RadPageViewItem item)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(300)]
    [Browsable(true)]
    [Description("Gets or sets the width of the items area.")]
    public int ItemAreaWidth
    {
      get
      {
        return this.ItemContainer.MinSize.Width;
      }
      set
      {
        if (this.ItemAreaWidth == value)
          return;
        this.ItemContainer.MinSize = new Size(value, this.ItemContainer.MinSize.Height);
      }
    }

    public override StripViewNewItemVisibility NewItemVisibility
    {
      get
      {
        return StripViewNewItemVisibility.Hidden;
      }
      set
      {
        base.NewItemVisibility = value;
      }
    }

    protected override RadPageViewItem OnItemCreating(
      RadPageViewItemCreatingEventArgs args)
    {
      RadPageViewItemPage page = args.Page as RadPageViewItemPage;
      if (page != null && page.ItemType == PageViewItemType.GroupHeaderItem)
        args.Item = (RadPageViewItem) new RadPageViewStripGroupItem();
      return base.OnItemCreating(args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      int num = (int) this.SetDefaultValueOverride(RadPageViewStripElement.StripAlignmentProperty, (object) (StripViewAlignment) (this.RightToLeft ? 1 : 3));
      (this.ItemsParent as RadPageViewElementBase).SetBorderAndFillOrientation(this.RightToLeft ? PageViewContentOrientation.Horizontal180 : PageViewContentOrientation.Horizontal, false);
    }
  }
}
