// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarSeparator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CommandBarSeparator : RadCommandBarBaseItem
  {
    protected int cachedThikness = 2;
    public static RadProperty ThiknessProperty = RadProperty.Register(nameof (Thickness), typeof (int), typeof (CommandBarSeparator), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

    static CommandBarSeparator()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (CommandBarSeparator));
    }

    public CommandBarSeparator()
    {
      this.VisibleInOverflowMenu = false;
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(2)]
    [Description("Gets or sets the thickness of the separator item.")]
    public int Thickness
    {
      get
      {
        return this.cachedThikness;
      }
      set
      {
        this.cachedThikness = value;
        this.MinSize = new Size(this.cachedThikness, 0);
        int num = (int) this.SetValue(CommandBarSeparator.ThiknessProperty, (object) this.cachedThikness);
      }
    }

    [Browsable(false)]
    public new string Text
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    protected override void OnOrientationChanged(EventArgs e)
    {
      this.AngleTransform = this.Orientation == Orientation.Vertical ? 90f : 0.0f;
      this.StretchVertically = this.Orientation == Orientation.Horizontal;
      this.StretchHorizontally = this.Orientation == Orientation.Vertical;
      base.OnOrientationChanged(e);
    }

    protected override void CreateChildElements()
    {
      this.StretchVertically = this.Orientation == Orientation.Horizontal;
      this.StretchHorizontally = this.Orientation == Orientation.Vertical;
      int num = (int) this.SetValue(CommandBarSeparator.ThiknessProperty, (object) this.cachedThikness);
      this.DrawFill = true;
      this.DrawBorder = false;
      this.DrawText = false;
      this.MinSize = new Size(this.cachedThikness, 0);
      this.BackColor = Color.Silver;
      this.BackColor2 = Color.Silver;
    }

    protected override SizeF DpiScale(SizeF baseSize)
    {
      SizeF dpiScaledSize = RadControl.GetDpiScaledSize(baseSize);
      if (this.Orientation == Orientation.Horizontal)
        baseSize.Height = dpiScaledSize.Height;
      else
        baseSize.Width = dpiScaledSize.Width;
      return baseSize;
    }
  }
}
