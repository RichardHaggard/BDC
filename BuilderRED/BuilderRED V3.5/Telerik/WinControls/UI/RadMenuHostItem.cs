// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuHostItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadMenuHostItem : RadMenuItemBase
  {
    private Control hostedControl;

    public RadMenuHostItem(Control control)
    {
      this.hostedControl = control;
      this.Children.Add((RadElement) new RadHostItem(this.hostedControl));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuHostItem);
      this.HandlesKeyboard = true;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Provides a reference to the ComboBox element in the menu item.")]
    public Control HostedControl
    {
      get
      {
        return this.hostedControl;
      }
    }

    protected override void CreateChildElements()
    {
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.hostedControl == null)
        return sizeF;
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      SizeF size = (SizeF) this.hostedControl.Size;
      if (parent != null)
        size.Width -= (float) parent.RightColumnPadding + parent.LeftColumnMaxPadding + (float) parent.Padding.Horizontal;
      return size;
    }
  }
}
