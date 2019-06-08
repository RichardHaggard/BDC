// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridRowHeaderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridRowHeaderElement : PropertyGridContentElement
  {
    public static RadProperty IsRootItemWithChildrenProperty = RadProperty.Register("IsRootItemWithChildren", typeof (bool), typeof (PropertyGridRowHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsInEditModeProperty = RadProperty.Register("IsInEditMode", typeof (bool), typeof (PropertyGridRowHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static PropertyGridRowHeaderElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridHeaderElementStateManager(), typeof (PropertyGridRowHeaderElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.DrawBorder = false;
      this.DrawFill = false;
      this.NotifyParentOnMouseInput = true;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    public virtual void Synchronize()
    {
      PropertyGridItem data = this.VisualItem.Data as PropertyGridItem;
      if (data != null)
      {
        if (data.Level == 0)
          this.Visibility = ElementVisibility.Collapsed;
        else
          this.Visibility = ElementVisibility.Visible;
      }
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      if (visualItem == null)
        return;
      int num1 = (int) this.SetValue(PropertyGridRowHeaderElement.IsRootItemWithChildrenProperty, (object) (bool) (data.GridItems.Count <= 0 ? 0 : (visualItem.Data.Level == 0 ? 1 : 0)));
      int num2 = (int) this.SetValue(PropertyGridRowHeaderElement.IsInEditModeProperty, visualItem.GetValue(PropertyGridItemElement.IsInEditModeProperty));
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = (SizeF) Size.Empty;
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      PropertyGridTableElement gridTableElement = this.PropertyGridTableElement;
      if (gridTableElement != null && visualItem != null && visualItem.Data is PropertyGridItem)
      {
        empty.Width = (float) gridTableElement.ItemIndent;
        empty.Height = !float.IsPositiveInfinity(availableSize.Height) ? availableSize.Height : (float) gridTableElement.ItemHeight;
      }
      return empty;
    }
  }
}
