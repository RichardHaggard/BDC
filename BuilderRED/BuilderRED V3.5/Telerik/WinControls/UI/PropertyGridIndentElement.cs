// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridIndentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridIndentElement : PropertyGridContentElement
  {
    static PropertyGridIndentElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridItemElementStateManager(), typeof (PropertyGridIndentElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Visibility = ElementVisibility.Visible;
      this.NotifyParentOnMouseInput = true;
      this.ClipDrawing = true;
      this.StretchVertically = true;
      this.StretchHorizontally = false;
      this.DrawBorder = false;
      this.DrawFill = false;
    }

    public virtual void Synchronize()
    {
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      if (visualItem == null)
        return;
      if ((visualItem.Data as PropertyGridItem).Level > 0)
        this.Visibility = ElementVisibility.Visible;
      else
        this.Visibility = ElementVisibility.Hidden;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = (SizeF) Size.Empty;
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      PropertyGridTableElement gridTableElement = this.PropertyGridTableElement;
      if (gridTableElement != null && visualItem != null)
      {
        PropertyGridItem data = visualItem.Data as PropertyGridItem;
        if (data != null)
        {
          empty.Width = (float) (gridTableElement.ItemIndent * data.Level);
          empty.Height = !float.IsPositiveInfinity(availableSize.Height) ? availableSize.Height : (float) gridTableElement.ItemHeight;
        }
      }
      return empty;
    }
  }
}
