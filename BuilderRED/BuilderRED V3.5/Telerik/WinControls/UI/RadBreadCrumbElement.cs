// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBreadCrumbElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadBreadCrumbElement : RadItem
  {
    public static RadProperty SpacingBetweenItemsProperty = RadProperty.Register(nameof (SpacingBetweenItems), typeof (int), typeof (RadBreadCrumbElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2));
    private FillPrimitive fill;
    private BorderPrimitive border;
    private ImagePrimitive imgPr;
    private StackLayoutPanel strip;
    private RadItemOwnerCollection items;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[1]
      {
        typeof (RadDropDownButtonElement)
      };
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
    }

    static RadBreadCrumbElement()
    {
      ThemeResolutionService.RegisterThemeFromStorage(ThemeStorageType.Resource, "Telerik.WinControls.UI.Resources.UIElementsThemes.BreadCrumbThemes.VistaBreadCrumb.xml");
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      this.SetSpacingBetweenItems();
    }

    [Category("Layout")]
    [Browsable(true)]
    [DefaultValue(2)]
    [Description("Gets or sets the spacing between the items in the breadcrumb")]
    public int SpacingBetweenItems
    {
      get
      {
        return (int) this.GetValue(RadBreadCrumbElement.SpacingBetweenItemsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBreadCrumbElement.SpacingBetweenItemsProperty, (object) value);
      }
    }

    [Description("Gets the collection of items which are children of the RadBreadCrumb element.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadBreadCrumbElement.SpacingBetweenItemsProperty)
        this.SetSpacingBetweenItems();
      base.OnPropertyChanged(e);
    }

    private void SetSpacingBetweenItems()
    {
      for (int index = 0; index < this.Items.Count - 1; ++index)
      {
        RadItem radItem = this.Items[index];
        radItem.Margin = new Padding(radItem.Margin.Left, radItem.Margin.Top, this.SpacingBetweenItems, radItem.Margin.Bottom);
      }
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "BreadCrumbFill";
      this.border = new BorderPrimitive();
      this.border.Class = "BreadCrumbBorder";
      this.strip = new StackLayoutPanel();
      this.imgPr = new ImagePrimitive();
      this.imgPr.Class = "BreadCrumbImage";
      this.fill.Visibility = ElementVisibility.Hidden;
      this.border.Visibility = ElementVisibility.Hidden;
      this.Children.Add((RadElement) this.fill);
      this.Children.Add((RadElement) this.border);
      this.Children.Add((RadElement) this.strip);
      this.items.Owner = (RadElement) this.strip;
    }
  }
}
