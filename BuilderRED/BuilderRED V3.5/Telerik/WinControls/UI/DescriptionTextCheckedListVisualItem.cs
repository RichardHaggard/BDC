// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DescriptionTextCheckedListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DescriptionTextCheckedListVisualItem : RadCheckedListVisualItem
  {
    private LightVisualElement descriptionContent;
    private StackLayoutElement verticalStack;

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListVisualItem);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.descriptionContent = new LightVisualElement();
      this.descriptionContent.TextAlignment = ContentAlignment.MiddleLeft;
      this.descriptionContent.TextWrap = true;
      this.descriptionContent.StretchVertically = true;
      this.verticalStack = new StackLayoutElement();
      this.verticalStack.Alignment = ContentAlignment.MiddleLeft;
      this.verticalStack.FitInAvailableSize = true;
      this.verticalStack.Orientation = Orientation.Vertical;
      this.verticalStack.Children.Add((RadElement) this.Label);
      this.verticalStack.Children.Add((RadElement) this.descriptionContent);
      this.descriptionContent.ForeColor = Color.Gray;
      this.StackLayout.Children.Add((RadElement) this.verticalStack);
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      DescriptionTextCheckedListDataItem data = this.Data as DescriptionTextCheckedListDataItem;
      if (this.Data is CheckAllDataItem)
      {
        this.descriptionContent.Text = string.Empty;
        this.descriptionContent.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (data == null)
          return;
        this.descriptionContent.Visibility = ElementVisibility.Visible;
        int num1 = (int) this.SetValue(RadItem.TextProperty, (object) string.Empty);
        this.descriptionContent.Text = data.DescriptionText;
        this.descriptionContent.Font = data.DescriptionFont != null ? data.DescriptionFont : this.Data.Font;
        this.Image = (Image) null;
        if (this.item.Owner == null || this.item.Owner.GetDefaultItemHeight() != 18)
          return;
        int num2 = (int) this.item.Owner.SetDefaultValueOverride(RadListElement.ItemHeightProperty, (object) 36);
      }
    }
  }
}
