// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SizeGripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class SizeGripElement : RadItem
  {
    private FillPrimitive fill;
    private BorderPrimitive border;
    private SizeGripItem gripItem;
    private SizeGripItem gripItem2;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
      this.Visibility = ElementVisibility.Collapsed;
    }

    public SizingMode SizingMode
    {
      get
      {
        if (this.gripItem.Visibility == ElementVisibility.Visible && this.gripItem2.Visibility == ElementVisibility.Visible)
          return SizingMode.UpDownAndRightBottom;
        if (this.gripItem.Visibility == ElementVisibility.Visible)
          return SizingMode.UpDown;
        return this.gripItem2.Visibility == ElementVisibility.Visible ? SizingMode.RightBottom : SizingMode.None;
      }
      set
      {
        if ((value & SizingMode.RightBottom) == SizingMode.RightBottom)
          this.gripItem2.Visibility = ElementVisibility.Visible;
        else
          this.gripItem2.Visibility = ElementVisibility.Collapsed;
        if ((value & SizingMode.UpDown) == SizingMode.UpDown)
          this.gripItem.Visibility = ElementVisibility.Visible;
        else
          this.gripItem.Visibility = ElementVisibility.Collapsed;
        if (value == SizingMode.None)
          this.Visibility = ElementVisibility.Collapsed;
        else
          this.Visibility = ElementVisibility.Visible;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool ShouldAspectRootElement
    {
      get
      {
        if (this.GripItemNS.ShouldAspectRootElement)
          return this.GripItemNSEW.ShouldAspectRootElement;
        return false;
      }
      set
      {
        this.GripItemNS.ShouldAspectRootElement = this.GripItemNSEW.ShouldAspectRootElement = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShouldAspectMinSize
    {
      get
      {
        if (this.GripItemNS.ShouldAspectMinSize)
          return this.GripItemNSEW.ShouldAspectMinSize;
        return false;
      }
      set
      {
        this.GripItemNS.ShouldAspectMinSize = this.GripItemNSEW.ShouldAspectMinSize = value;
      }
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "GripFill";
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.border.Class = "GripBorder";
      this.border.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.border);
      this.gripItem = new SizeGripItem();
      this.gripItem.StretchHorizontally = false;
      this.gripItem.StretchVertically = false;
      this.gripItem.Class = "GripNS";
      this.gripItem.Image.Class = "GripNSImage";
      this.gripItem.SizingMode = SizeGripItem.SizingModes.Vertical;
      this.gripItem.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.gripItem.Alignment = System.Drawing.ContentAlignment.BottomCenter;
      this.Children.Add((RadElement) this.gripItem);
      this.gripItem2 = new SizeGripItem();
      this.gripItem2.StretchHorizontally = false;
      this.gripItem2.StretchVertically = false;
      this.gripItem2.Class = "GripNSEW";
      this.gripItem2.Image.Class = "GripNSEWImage";
      this.gripItem2.SizingMode = SizeGripItem.SizingModes.Both;
      this.gripItem2.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.gripItem2.RightToLeft = this.RightToLeft;
      this.gripItem2.Alignment = System.Drawing.ContentAlignment.MiddleRight;
      this.Children.Add((RadElement) this.gripItem2);
    }

    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
        this.gripItem2.RightToLeft = value;
      }
    }

    public SizeGripItem GripItemNS
    {
      get
      {
        return this.gripItem;
      }
    }

    public SizeGripItem GripItemNSEW
    {
      get
      {
        return this.gripItem2;
      }
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      if (object.ReferenceEquals((object) element, (object) this.fill) || object.ReferenceEquals((object) element, (object) this.border))
      {
        bool? paintSystemSkin = this.paintSystemSkin;
        if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0)
          return false;
      }
      return base.ShouldPaintChild(element);
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.fill.Visibility = ElementVisibility.Hidden;
      this.border.Visibility = ElementVisibility.Hidden;
    }

    protected override void UnitializeSystemSkinPaint()
    {
      base.UnitializeSystemSkinPaint();
      this.fill.Visibility = ElementVisibility.Visible;
      this.border.Visibility = ElementVisibility.Visible;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      return VisualStyleElement.Status.Bar.Normal;
    }
  }
}
