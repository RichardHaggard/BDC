// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AlertWindowButtonsPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Layout;

namespace Telerik.WinControls.UI
{
  public class AlertWindowButtonsPanel : LightVisualElement
  {
    private WrapLayoutPanel buttonsLayout;
    private RadItemOwnerCollection items;

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public WrapLayoutPanel ButtonsLayoutPanel
    {
      get
      {
        return this.buttonsLayout;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Margin = new Padding(0, 3, 0, 3);
      this.buttonsLayout = new WrapLayoutPanel();
      this.buttonsLayout.Class = "ButtonsLayoutPanel";
      this.items = new RadItemOwnerCollection();
      this.items.Owner = (RadElement) this.buttonsLayout;
      this.items.ItemTypes = new System.Type[1]
      {
        typeof (RadButtonElement)
      };
      this.items.SealedTypes = new System.Type[1]
      {
        typeof (RadButtonElement)
      };
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Children.Add((RadElement) this.buttonsLayout);
    }
  }
}
