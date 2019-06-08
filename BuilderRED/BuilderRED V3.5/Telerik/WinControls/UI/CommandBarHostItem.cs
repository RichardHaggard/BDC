// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarHostItem
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
  public class CommandBarHostItem : RadCommandBarBaseItem
  {
    private static SizeF defaultSize = new SizeF(15f, 15f);
    private RadElement hostedItem;
    private Control hostedControl;

    static CommandBarHostItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (CommandBarHostItem));
    }

    public CommandBarHostItem()
    {
    }

    public CommandBarHostItem(RadElement hostedItem)
    {
      this.HostedItem = hostedItem;
    }

    public CommandBarHostItem(Control hostedControl)
    {
      this.HostedItem = (RadElement) new RadHostItem(hostedControl);
      this.hostedControl = hostedControl;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadElement HostedItem
    {
      get
      {
        return this.hostedItem;
      }
      set
      {
        if (this.hostedItem == value)
          return;
        if (this.hostedItem != null)
          this.Children.Remove(this.hostedItem);
        this.hostedItem = value;
        this.hostedControl = (Control) null;
        if (this.hostedItem == null)
          return;
        this.Children.Add(this.hostedItem);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Control HostedControl
    {
      get
      {
        return this.hostedControl;
      }
      set
      {
        this.HostedItem = (RadElement) new RadHostItem(value);
        this.hostedControl = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Indicates whether the item should be drawn in the strip.")]
    public override bool VisibleInStrip
    {
      get
      {
        return base.VisibleInStrip;
      }
      set
      {
        base.VisibleInStrip = value;
        if (this.hostedItem == null)
          return;
        int num = (int) this.hostedItem.SetValue(RadElement.VisibilityProperty, (object) (ElementVisibility) (value ? 0 : 2));
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.NotifyParentOnMouseInput = false;
      this.CanFocus = false;
      int num1 = (int) this.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num2 = (int) this.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      this.Alignment = ContentAlignment.MiddleLeft;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.hostedItem == null)
        return CommandBarHostItem.defaultSize;
      availableSize = this.GetClientRectangle(availableSize).Size;
      this.hostedItem.Measure(availableSize);
      SizeF sizeF = new SizeF(Math.Min(availableSize.Width, this.hostedItem.DesiredSize.Width), Math.Min(availableSize.Height, this.hostedItem.DesiredSize.Height));
      Padding borderThickness = this.GetBorderThickness(this.DrawBorder);
      sizeF.Width += (float) (borderThickness.Left + borderThickness.Right + this.Padding.Left + this.Padding.Right);
      sizeF.Height += (float) (borderThickness.Top + borderThickness.Bottom + this.Padding.Top + this.Padding.Bottom);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.hostedItem == null)
        return finalSize;
      this.hostedItem.Arrange(clientRectangle);
      return finalSize;
    }
  }
}
