// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageItemsPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BackstageItemsPanelElement : BackstageVisualElement
  {
    private RadButtonElement backButtonElement;
    private RadItemOwnerCollection items;
    private BackstageViewElement owner;

    protected override void CreateChildElements()
    {
      this.MinSize = new Size(132, 0);
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[2]
      {
        typeof (BackstageTabItem),
        typeof (BackstageButtonItem)
      };
      this.items.Owner = (RadElement) this;
      this.backButtonElement = this.CreateBackButtonElement();
      this.backButtonElement.Click += new EventHandler(this.backButtonElement_Click);
      this.Children.Add((RadElement) this.backButtonElement);
      base.CreateChildElements();
    }

    private void backButtonElement_Click(object sender, EventArgs e)
    {
      ((RadRibbonBarBackstageView) this.ElementTree.Control).HidePopup();
    }

    protected virtual RadButtonElement CreateBackButtonElement()
    {
      return new RadButtonElement();
    }

    public BackstageItemsPanelElement(BackstageViewElement owner)
    {
      this.owner = owner;
      if (this.owner.IsFullScreen)
        this.backButtonElement.Visibility = ElementVisibility.Visible;
      else
        this.backButtonElement.Visibility = ElementVisibility.Collapsed;
      this.owner.PropertyChanged += new PropertyChangedEventHandler(this.owner_PropertyChanged);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.owner.PropertyChanged -= new PropertyChangedEventHandler(this.owner_PropertyChanged);
    }

    public BackstageViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    [Description("Gets a collection representing the items contained in this BackstageView.")]
    [Editor("Telerik.WinControls.UI.Design.RadRibbonBarBackstageItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public RadButtonElement BackButtonElement
    {
      get
      {
        return this.backButtonElement;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.backButtonElement.Measure(availableSize);
      int count = this.items.Count;
      float num = (float) this.MinSize.Width;
      for (int index = 0; index < count; ++index)
      {
        this.items[index].Measure(availableSize);
        num = Math.Max(num, this.items[index].DesiredSize.Width);
      }
      return new SizeF(num, availableSize.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      Padding borderThickness = this.GetBorderThickness(true);
      this.backButtonElement.Arrange(new RectangleF(clientRectangle.Location, this.backButtonElement.DesiredSize));
      int count = this.items.Count;
      float y = clientRectangle.Top + this.backButtonElement.DesiredSize.Height;
      for (int index = 0; index < count; ++index)
      {
        RadItem radItem = this.items[index];
        BackstageTabItem backstageTabItem = radItem as BackstageTabItem;
        if (backstageTabItem != null && backstageTabItem == this.owner.SelectedItem)
          backstageTabItem.Page.Visible = false;
        radItem.Arrange(new RectangleF(clientRectangle.X - (float) borderThickness.Left, y, clientRectangle.Width + (float) borderThickness.Horizontal, radItem.DesiredSize.Height));
        y += radItem.DesiredSize.Height;
      }
      return finalSize;
    }

    private void owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsFullScreen"))
        return;
      if (this.Owner.IsFullScreen)
        this.backButtonElement.Visibility = ElementVisibility.Visible;
      else
        this.backButtonElement.Visibility = ElementVisibility.Collapsed;
    }
  }
}
