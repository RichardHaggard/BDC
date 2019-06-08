// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadGalleryGroupItem : RadItem
  {
    private RadItemOwnerCollection items;
    private RadElement captionElement;
    private RadElement bodyElement;
    private ElementWithCaptionLayoutPanel groupLayoutPanel;
    private WrapLayoutPanel itemsLayoutPanel;
    private TextPrimitive textPrimitive;
    private RadGalleryElement owner;

    public RadGalleryGroupItem()
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.itemsLayoutPanel = new WrapLayoutPanel();
      this.textPrimitive = new TextPrimitive();
      this.captionElement = (RadElement) new FillPrimitive();
      this.bodyElement = (RadElement) new FillPrimitive();
      this.groupLayoutPanel = new ElementWithCaptionLayoutPanel();
      this.NotifyParentOnMouseInput = true;
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[1]
      {
        typeof (RadGalleryItem)
      };
    }

    public RadGalleryGroupItem(string text)
    {
      this.Text = text;
    }

    [RadEditItemsAction]
    [Editor(typeof (GroupedGalleryItemsEditor), typeof (UITypeEditor))]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WrapLayoutPanel ItemsLayoutPanel
    {
      get
      {
        return this.itemsLayoutPanel;
      }
    }

    [RadDefaultValue("ShowCaption", typeof (ElementWithCaptionLayoutPanel))]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the caption of the group is shown.")]
    public bool ShowCaption
    {
      get
      {
        return this.groupLayoutPanel.ShowCaption;
      }
      set
      {
        this.groupLayoutPanel.ShowCaption = value;
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

    public override string ToString()
    {
      if (this.Site != null)
        return this.Site.Name;
      return base.ToString();
    }

    protected override void CreateChildElements()
    {
      this.textPrimitive.Class = "GalleryGroupCaption";
      int num1 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      this.captionElement = (RadElement) new FillPrimitive();
      this.captionElement.AutoSizeMode = RadAutoSizeMode.Auto;
      int num2 = (int) this.captionElement.SetValue(ElementWithCaptionLayoutPanel.CaptionElementProperty, (object) true);
      this.captionElement.Class = "GalleryGroupCaptionFill";
      this.captionElement.Children.Add((RadElement) this.textPrimitive);
      this.bodyElement = (RadElement) new FillPrimitive();
      this.bodyElement.AutoSizeMode = RadAutoSizeMode.Auto;
      this.bodyElement.Class = "GalleryGroupBodyFill";
      this.bodyElement.Padding = new Padding(2, 2, 2, 0);
      this.bodyElement.Children.Add((RadElement) this.itemsLayoutPanel);
      this.groupLayoutPanel = new ElementWithCaptionLayoutPanel();
      this.groupLayoutPanel.CaptionOnTop = true;
      this.groupLayoutPanel.Children.Add(this.captionElement);
      this.groupLayoutPanel.Children.Add(this.bodyElement);
      this.Children.Add((RadElement) this.groupLayoutPanel);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      int keyCode = (int) e.KeyCode;
    }
  }
}
