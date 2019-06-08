// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarMaskedEditBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("TextChanged")]
  public class CommandBarMaskedEditBox : RadCommandBarBaseItem
  {
    private Size defaultSize = new Size(106, 22);
    private RadMaskedEditBoxElement maskedEditBoxElement;

    static CommandBarMaskedEditBox()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (CommandBarMaskedEditBox));
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [Description("Gets or sets a mask expression.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Mask
    {
      get
      {
        return this.CommandBarMaskedTextBoxElement.Mask;
      }
      set
      {
        this.CommandBarMaskedTextBoxElement.Mask = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the mask type.")]
    [DefaultValue(MaskType.None)]
    public MaskType MaskType
    {
      get
      {
        return this.CommandBarMaskedTextBoxElement.MaskType;
      }
      set
      {
        this.CommandBarMaskedTextBoxElement.MaskType = value;
      }
    }

    [Description("Gets or sets the text associated with this item.")]
    [DefaultValue("")]
    [SettingsBindable(true)]
    [Category("Behavior")]
    [Bindable(true)]
    public override string Text
    {
      get
      {
        if (this.maskedEditBoxElement != null)
          return this.maskedEditBoxElement.Text;
        return base.Text;
      }
      set
      {
        base.Text = value;
        if (this.maskedEditBoxElement == null)
          return;
        this.maskedEditBoxElement.Text = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadMaskedEditBoxElement CommandBarMaskedTextBoxElement
    {
      get
      {
        return this.maskedEditBoxElement;
      }
    }

    [Category("Appearance")]
    [Description("Indicates whether the item should be drawn in the strip.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public override bool VisibleInStrip
    {
      get
      {
        return base.VisibleInStrip;
      }
      set
      {
        base.VisibleInStrip = value;
        if (this.maskedEditBoxElement == null)
          return;
        int num = (int) this.maskedEditBoxElement.SetValue(RadElement.VisibilityProperty, (object) (ElementVisibility) (value ? 0 : 2));
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchHorizontally = this.StretchVertically = false;
      this.maskedEditBoxElement = new RadMaskedEditBoxElement();
      this.maskedEditBoxElement.Class = "RadCommandBarTextBoxElement";
      this.maskedEditBoxElement.MinSize = this.defaultSize;
      this.MinSize = this.defaultSize;
      this.maskedEditBoxElement.Alignment = ContentAlignment.MiddleLeft;
      this.Children.Add((RadElement) this.maskedEditBoxElement);
    }
  }
}
