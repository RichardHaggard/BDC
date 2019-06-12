// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DescriptionTextCheckedListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class DescriptionTextCheckedListDataItem : RadCheckedListDataItem
  {
    public static readonly RadProperty DescriptionTextProperty = RadProperty.Register(nameof (DescriptionText), typeof (string), typeof (DescriptionTextCheckedListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty DescriptionFontProperty = RadProperty.Register(nameof (DescriptionFont), typeof (Font), typeof (DescriptionTextCheckedListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));

    public string DescriptionText
    {
      get
      {
        if (this.GetValueSource(DescriptionTextCheckedListDataItem.DescriptionTextProperty) == ValueSource.Local || this.DataBoundItem == null)
          return (string) this.GetValue(DescriptionTextCheckedListDataItem.DescriptionTextProperty);
        object descriptionText = (object) this.DescriptionText;
        if (descriptionText == null)
          return this.DataBoundItem.ToString();
        return descriptionText.ToString();
      }
      set
      {
        int num = (int) this.SetValue(DescriptionTextCheckedListDataItem.DescriptionTextProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Font", typeof (VisualElement))]
    [Description("Gets or sets the font for this Description Text in DescriptionListDataItem instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public Font DescriptionFont
    {
      get
      {
        return (Font) this.GetValue(DescriptionTextCheckedListDataItem.DescriptionFontProperty);
      }
      set
      {
        int num = (int) this.SetValue(DescriptionTextCheckedListDataItem.DescriptionFontProperty, (object) value);
      }
    }

    protected internal override void SetDataBoundItem(bool dataBinding, object value)
    {
      base.SetDataBoundItem(dataBinding, value);
      this.DescriptionText = (string) this.dataLayer.GetDescriptionTextValue((RadListDataItem) this);
    }
  }
}
