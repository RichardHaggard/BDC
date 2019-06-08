// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DescriptionTextListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class DescriptionTextListDataItem : RadListDataItem
  {
    public static readonly RadProperty DescriptionTextProperty = RadProperty.Register(nameof (DescriptionText), typeof (string), typeof (DescriptionTextListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty DescriptionFontProperty = RadProperty.Register(nameof (DescriptionFont), typeof (Font), typeof (DescriptionTextListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));

    public DescriptionTextListDataItem()
    {
    }

    public DescriptionTextListDataItem(string text, string descriptionText)
      : base(text)
    {
      this.DescriptionText = descriptionText;
    }

    public DescriptionTextListDataItem(string text, string descriptionText, object value)
      : base(text, value)
    {
      this.DescriptionText = descriptionText;
    }

    public string DescriptionText
    {
      get
      {
        if (this.GetValueSource(DescriptionTextListDataItem.DescriptionTextProperty) == ValueSource.Local || this.DataBoundItem == null)
          return (string) this.GetValue(DescriptionTextListDataItem.DescriptionTextProperty);
        object descriptionText = (object) this.DescriptionText;
        if (descriptionText == null)
          return this.DataBoundItem.ToString();
        return descriptionText.ToString();
      }
      set
      {
        int num = (int) this.SetValue(DescriptionTextListDataItem.DescriptionTextProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the font for this Description Text in DescriptionListDataItem instance.")]
    [Browsable(true)]
    [RadPropertyDefaultValue("Font", typeof (VisualElement))]
    public Font DescriptionFont
    {
      get
      {
        return (Font) this.GetValue(DescriptionTextListDataItem.DescriptionFontProperty);
      }
      set
      {
        int num = (int) this.SetValue(DescriptionTextListDataItem.DescriptionFontProperty, (object) value);
      }
    }

    protected internal override void SetDataBoundItem(bool dataBinding, object value)
    {
      base.SetDataBoundItem(dataBinding, value);
      this.DescriptionText = (string) this.dataLayer.GetDescriptionTextValue((RadListDataItem) this);
    }

    [Browsable(false)]
    public override RadListElement Owner
    {
      get
      {
        return base.Owner;
      }
      internal set
      {
        base.Owner = value;
        if (value == null)
          return;
        value.IsDescriptionText = true;
      }
    }
  }
}
